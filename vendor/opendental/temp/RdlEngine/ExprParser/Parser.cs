/* ====================================================================
    Copyright (C) 2004-2005  fyiReporting Software, LLC

    This file is part of the fyiReporting RDL project.
	
    The RDL project is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

    For additional information, email info@fyireporting.com or visit
    the website www.fyiReporting.com.
*/
using System;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Xml;
using System.Reflection;
using fyiReporting.RDL;


namespace fyiReporting.RDL
{
	/// <summary>
	/// <p>Language parser.   Recursive descent parser.  Precedence of operators
	/// is handled by starting with lowest precedence and calling down recursively
	/// to the highest.
	/// AND/OR
	/// NOT
	/// relational operators, eq, ne, lt, lte, gt, gte
	/// +, -
	/// *, /, %
	/// ^ (exponentiation)
	/// unary +, -
	/// parenthesis (...)
	/// <p>
	/// In BNF the language grammar is:</p>
	///	<code>
	/// Expr: Term ExprRhs
	/// ExprRhs: PlusMinusOperator Term ExprRhs
	/// Term: Factor TermRhs
	/// TermRhs: MultDivOperator Factor TermRhs
	/// Factor: ( Expr ) | BaseType | - BaseType | - ( Expr )
	/// BaseType: FuncIdent | NUMBER | QUOTE   
	/// FuncIDent: IDENTIFIER ( [Expr] [, Expr]*) | IDENTIFIER
	/// PlusMinusOperator: + | -
	/// MultDivOperator: * | /
	///	</code>
	///	
	/// </summary>
	internal class Parser
	{
		private TokenList tokens;
		private Stack operandStack = new Stack();
		private Stack operatorStack = new Stack();
		private Token curToken=null;
		private NameLookup idLookup=null;
		private ArrayList _DataCache;
		private bool _InAggregate;
		private ArrayList _FieldResolve;
		private bool _NoAggregate=false;

		/// <summary>
		/// Parse an expression.
		/// </summary>
		internal Parser(ArrayList c) 
		{
			_DataCache = c;
		}

		/// <summary>
		/// Returns a parsed Expression
		/// </summary>
		/// <param name="expr">The expression to be parsed.</param>
		/// <returns>An expression that can be run after validation and binding.</returns>
		internal IExpr Parse(NameLookup lu, string expr)
		{
			_InAggregate = false;

			if (expr.Substring(0,1) != "=")		// if 1st char not '='
				return new Constant(expr);		//   this is a constant value

			idLookup = lu;	
			IExpr e = this.ParseExpr(new StringReader(expr));
			
			if (e == null)					// Didn't get an expression?
				e = new Constant(expr);		//  then provide a constant

			return e;
		}

		internal bool NoAggregateFunctions
		{	// marked true when in an aggregate function 
			get {return _NoAggregate;}
			set {_NoAggregate = value;}
		}

		/// <summary>
		/// Returns a parsed DPL instance.
		/// </summary>
		/// <param name="reader">The TextReader value to be parsed.</param>
		/// <returns>A parsed Program instance.</returns>
		private IExpr ParseExpr(TextReader reader)
		{
			IExpr result=null;
			Lexer lexer = new Lexer(reader);
			tokens = lexer.Lex();

			if (tokens.Peek().Type == TokenTypes.EQUAL)
			{
				tokens.Extract();		// skip over the equal
				curToken = tokens.Extract();	// set up the first token
				MatchExprAndOr(out result);	// start with lowest precedence and work up
			}

			if (curToken.Type != TokenTypes.EOF)
				throw new ParserException("End of expression expected." + "  At column " + Convert.ToString(curToken.StartCol));

			return result;
		}

		// ExprAndOr: 
		private void MatchExprAndOr(out IExpr result)
		{
			TokenTypes t;			// remember the type

			IExpr lhs;
			MatchExprNot(out lhs);
			result = lhs;			// in case we get no matches
			while ((t = curToken.Type) == TokenTypes.AND || t == TokenTypes.OR)
			{
				curToken = tokens.Extract();
				IExpr rhs;
				MatchExprNot(out rhs);
				bool bBool = (rhs.GetTypeCode() == TypeCode.Boolean &&
					lhs.GetTypeCode() == TypeCode.Boolean);
				if (!bBool)
					throw new ParserException("AND/OR operations require both sides to be boolean expressions." + "  At column " + Convert.ToString(curToken.StartCol));

				switch(t)
				{
					case TokenTypes.AND:
						result = new FunctionAnd(lhs, rhs);
						break;
					case TokenTypes.OR:
						result = new FunctionOr(lhs, rhs);
						break;
				}
				lhs = result;		// in case we have more AND/OR s
			}
		}
		
		private void MatchExprNot(out IExpr result)
		{
			TokenTypes t;			// remember the type
			t = curToken.Type;
			if (t == TokenTypes.NOT)
			{
				curToken = tokens.Extract();
			}
			MatchExprRelop(out result);
			if (t == TokenTypes.NOT)
			{
				if (result.GetTypeCode() != TypeCode.Boolean)
					throw new ParserException("NOT requires boolean expression." + "  At column " + Convert.ToString(curToken.StartCol));
				result = new FunctionNot(result);
			}
		}

		// ExprRelop: 
		private void MatchExprRelop(out IExpr result)
		{
			TokenTypes t;			// remember the type

			IExpr lhs;
			MatchExprAddSub(out lhs);
			result = lhs;			// in case we get no matches
			while ((t = curToken.Type) == TokenTypes.EQUAL ||
				t == TokenTypes.NOTEQUAL ||
				t == TokenTypes.GREATERTHAN ||
				t == TokenTypes.GREATERTHANOREQUAL ||
				t == TokenTypes.LESSTHAN ||
				t == TokenTypes.LESSTHANOREQUAL)
			{
				curToken = tokens.Extract();
				IExpr rhs;
				MatchExprAddSub(out rhs);

				switch(t)
				{
					case TokenTypes.EQUAL:
						result = new FunctionRelopEQ(lhs, rhs);
						break;
					case TokenTypes.NOTEQUAL:
						result = new FunctionRelopNE(lhs, rhs);
						break;
					case TokenTypes.GREATERTHAN:
						result = new FunctionRelopGT(lhs, rhs);
						break;
					case TokenTypes.GREATERTHANOREQUAL:
						result = new FunctionRelopGTE(lhs, rhs);
						break;
					case TokenTypes.LESSTHAN:
						result = new FunctionRelopLT(lhs, rhs);
						break;
					case TokenTypes.LESSTHANOREQUAL:
						result = new FunctionRelopLTE(lhs, rhs);
						break;
				}
				lhs = result;		// in case we continue the loop
			}
		}

		// ExprAddSub: PlusMinusOperator Term ExprRhs
		private void MatchExprAddSub(out IExpr result)
		{
			TokenTypes t;			// remember the type

			IExpr lhs;
			MatchExprMultDiv(out lhs);
			result = lhs;			// in case we get no matches
			while ((t = curToken.Type) == TokenTypes.PLUS || t == TokenTypes.PLUSSTRING || t == TokenTypes.MINUS)
			{
				curToken = tokens.Extract();
				IExpr rhs;
				MatchExprMultDiv(out rhs);
				TypeCode lt = lhs.GetTypeCode();
				TypeCode rt = rhs.GetTypeCode();
				bool bDecimal = (rt == TypeCode.Decimal &&
					lt == TypeCode.Decimal);
				bool bString = (rt == TypeCode.String ||
					lt == TypeCode.String);

				switch(t)
				{
					case TokenTypes.PLUSSTRING:
						result = new FunctionPlusString(lhs, rhs);
						break;
					case TokenTypes.PLUS:
						if (bDecimal)
							result = new FunctionPlusDecimal(lhs, rhs);
						else if (bString)
							result = new FunctionPlusString(lhs, rhs);
						else
							result = new FunctionPlus(lhs, rhs);
						break;
					case TokenTypes.MINUS:
						if (bDecimal)
							result = new FunctionMinusDecimal(lhs, rhs);
						else if (bString)
							throw new ParserException("'-' operator works only on numbers." + "  At column " + Convert.ToString(curToken.StartCol));
						else
							result = new FunctionMinus(lhs, rhs);
						break;
				}
				lhs = result;		// in case continue in the loop
			}
		}

		// TermRhs: MultDivOperator Factor TermRhs
		private void MatchExprMultDiv(out IExpr result)
		{
			TokenTypes t;			// remember the type
			IExpr lhs;
			MatchExprExp(out lhs);
			result = lhs;			// in case we get no matches
			while ((t = curToken.Type) == TokenTypes.FORWARDSLASH ||
				t == TokenTypes.STAR ||
				t == TokenTypes.MODULUS)
			{
				curToken = tokens.Extract();
				IExpr rhs;
				MatchExprExp(out rhs);
				bool bDecimal = (rhs.GetTypeCode() == TypeCode.Decimal &&
					lhs.GetTypeCode() == TypeCode.Decimal);
				switch (t)
				{
					case TokenTypes.FORWARDSLASH:
						if (bDecimal)
							result = new FunctionDivDecimal(lhs, rhs);	
						else
							result = new FunctionDiv(lhs, rhs);	
						break;
					case TokenTypes.STAR:
						if (bDecimal)
							result = new FunctionMultDecimal(lhs, rhs);
						else
							result = new FunctionMult(lhs, rhs);
						break;
					case TokenTypes.MODULUS:
						result = new FunctionModulus(lhs, rhs);
						break;
				}
				lhs = result;		// in case continue in the loop
			}
		}

		// TermRhs: ExpOperator Factor TermRhs
		private void MatchExprExp(out IExpr result)
		{
			IExpr lhs;
			MatchExprUnary(out lhs);
			if (curToken.Type == TokenTypes.EXP)
			{
				curToken = tokens.Extract();
				IExpr rhs;
				MatchExprUnary(out rhs);
				result = new FunctionExp(lhs, rhs);	
			}
			else
				result = lhs;
		}
		
		private void MatchExprUnary(out IExpr result)
		{
			TokenTypes t;			// remember the type
			t = curToken.Type;
			if (t == TokenTypes.PLUS || t == TokenTypes.MINUS)
			{
				curToken = tokens.Extract();
			}
			MatchExprParen(out result);
			if (t == TokenTypes.MINUS)
			{
				if (result.GetTypeCode() == TypeCode.Decimal)
					result = new FunctionUnaryMinusDecimal(result);
				else if (result.GetTypeCode() == TypeCode.Int32)
					result = new FunctionUnaryMinusInteger(result);
				else
					result = new FunctionUnaryMinus(result);
			}
		}
		
		// Factor: ( Expr ) | BaseType | - BaseType | - ( Expr )
		private void MatchExprParen(out IExpr result)
		{
			// Match- ( Expr )
			if (curToken.Type == TokenTypes.LPAREN)
			{	// trying to match ( Expr )
				curToken = tokens.Extract();
				MatchExprAndOr(out result);
				if (curToken.Type != TokenTypes.RPAREN)
					throw new ParserException("')' expected but not found.  Found '" + curToken.Value + "'  At column " + Convert.ToString(curToken.StartCol));
				curToken = tokens.Extract();
			}
			else
				MatchBaseType(out result);
		}

		// BaseType: FuncIdent | NUMBER | QUOTE   - note certain types are restricted in expressions
		private void MatchBaseType(out IExpr result)
		{
			if (MatchFuncIDent(out result))
				return;

			switch (curToken.Type)
			{
				case TokenTypes.NUMBER:
					result = new ConstantDecimal(curToken.Value);
					break;
				case TokenTypes.DATETIME:
					result = new ConstantDateTime(curToken.Value);
					break;
				case TokenTypes.DOUBLE:
					result = new ConstantDouble(curToken.Value);
					break;
				case TokenTypes.INTEGER:
					result = new ConstantInteger(curToken.Value);
					break;
				case TokenTypes.QUOTE:
					result = new ConstantString(curToken.Value);
					break;
				default:
					throw new ParserException("Constant or Identifier expected but not found.  Found '" + curToken.Value + "'  At column " + Convert.ToString(curToken.StartCol));
			}
			curToken = tokens.Extract();

			return;
		}

		// FuncIDent: IDENTIFIER ( [Expr] [, Expr]*) | IDENTIFIER
		private bool MatchFuncIDent(out IExpr result)
		{
			IExpr e;
			string fullname;			// will hold the full name
			string method;				// will hold method name or second part of name
			string firstPart;			// will hold the collection name
			string thirdPart;			// will hold third part of name
			bool bOnePart;				// simple name: no ! or . in name

			result = null;

			if (curToken.Type != TokenTypes.IDENTIFIER)
				return false;

			// Disentangle method calls from collection references
			method = fullname = curToken.Value;
			curToken = tokens.Extract();

			// Break the name into parts
			char[] breakChars = new char[] {'!', '.'};

			int posBreak = method.IndexOfAny(breakChars);
			if (posBreak > 0)
			{
				bOnePart = false;
				firstPart = method.Substring(0, posBreak);
				method = method.Substring(posBreak+1);		// rest of expression
			}
			else
			{
				bOnePart = true;
				firstPart = method;
			}

			posBreak = method.IndexOf('.');
			if (posBreak > 0)
			{
				thirdPart = method.Substring(posBreak+1);	// rest of expression
				method = method.Substring(0, posBreak);
			}
			else
				thirdPart = null;

			switch (firstPart)
			{
				case "Fields":
					Field f = idLookup.LookupField(method);
					if (f == null && !this._InAggregate)
						throw new ParserException("Field '" + method + "'  not found.");
					if (thirdPart == null || thirdPart == "Value")
					{
						if (f == null)
						{
							result = new FunctionField(method);
							this._FieldResolve.Add(result);
						}
						else
							result = new FunctionField(f);	
					}
					else if (thirdPart == "IsMissing")
					{
						if (f == null)
						{
							result = new FunctionFieldIsMissing(method);
							this._FieldResolve.Add(result);
						}
						else
							result = new FunctionFieldIsMissing(f);
					}
					else
						throw new ParserException("Field '" + method + "'  only supports 'Value' and 'IsMissing' properties.");
					return true;
				case "Parameters":
					ReportParameter p = idLookup.LookupParameter(method);
					if (p == null)
						throw new ParserException("Report parameter '" + method + "'  not found.");
					if (thirdPart == null || thirdPart == "Value")
						result = new FunctionReportParameter(p);
					else if (thirdPart == "Label")
						result = new FunctionReportParameterLabel(p);
					else
						throw new ParserException("Parameter '" + method + "'  only supports 'Value' and 'Label' properties.");
					return true;
				case "ReportItems":
					Textbox t = idLookup.LookupReportItem(method);
					if (t == null)
						throw new ParserException("ReportItem '" + method + "'  not found.");
					if (thirdPart != null && thirdPart != "Value")
						throw new ParserException("ReportItem '" + method + "'  only supports 'Value' property.");
					result = new FunctionTextbox(t);	
					return true;
				case "Globals":
					e = idLookup.LookupGlobal(method);
					if (e == null)
						throw new ParserException("Globals '" + method + "'  not found.");
					result = e;
					return true;
				case "User":
					e = idLookup.LookupUser(method);
					if (e == null)
						throw new ParserException("User variable '" + method + "'  not found.");
					result = e;
					return true;
				case "Recursive":	// Only valid for some aggregate functions
					result = new IdentifierKey(IdentifierKeyEnum.Recursive);
					return true;
				case "Simple":		// Only valid for some aggregate functions
					result = new IdentifierKey(IdentifierKeyEnum.Simple);
					return true;
				default:
					if (curToken.Type != TokenTypes.LPAREN)
					{
						if (!bOnePart)
							throw new ParserException(string.Format("'{0}' is an unknown identifer.", fullname));

						switch (method.ToLower())		// lexer should probably mark these
						{
							case "true":
							case "false":
								result = new ConstantBoolean(method.ToLower());
								break;
							default:
								// usually this is enum that will be used in an aggregate 
								result = new Identifier(method);
								break;
						}
						return true;
					}
					break;  // Should be a function reference
			}

			// We've got an function reference
			curToken = tokens.Extract();		// get rid of '('

			// Got a function now obtain the arguments
			int argCount=0;
			
			bool isAggregate = IsAggregate(method, bOnePart);
			if (_NoAggregate && isAggregate)
				throw new ParserException("Aggregate function '" + method + "' cannot be used within a Grouping expression.");
			if (_InAggregate && isAggregate)
				throw new ParserException("Aggregate function '" + method + "' cannot be nested in another aggregate function.");
			_InAggregate = isAggregate;
			if (_InAggregate)
				_FieldResolve = new ArrayList();

			ArrayList largs = new ArrayList();
			while(true)
			{
				if (curToken.Type == TokenTypes.RPAREN)
				{	// We've got our function
					curToken = tokens.Extract();
					break;
				}
				if (argCount == 0)
				{
					// don't need to do anything
				}
				else if (curToken.Type == TokenTypes.COMMA)
				{
					curToken = tokens.Extract();
				}
				else
					throw new ParserException("Invalid function arguments.  Found '" + curToken.Value + "'  At column " + Convert.ToString(curToken.StartCol));
				
				MatchExprAndOr(out e);
				if (e == null)
					throw new ParserException("Expecting ',' or ')'.  Found '" + curToken.Value + "'  At column " + Convert.ToString(curToken.StartCol));

				largs.Add(e);
				argCount++;
			}
			if (_InAggregate)
			{
				ResolveFields(method, this._FieldResolve, largs);
				_FieldResolve = null;
				_InAggregate = false;
			}

			IExpr[] args = new IExpr[argCount];
			for (int i = 0; i < argCount; i++)
			{
				args[i] = (IExpr) largs[i];
			}

			object scope;
			bool bSimple;
			if (!bOnePart)				
				result = ResolveMethodCall(fullname, args);	// throw exception when fails
			else switch(method.ToLower())
			{
				case "iif":
					if (args.Length != 3)
						throw new ParserException("iff function requires 3 arguments." + "  At column " + Convert.ToString(curToken.StartCol));
//  We allow any type for the first argument; it will get converted to boolean at runtime
//					if (args[0].GetTypeCode() != TypeCode.Boolean)
//						throw new ParserException("First argument to iif function must be boolean." + "  At column " + Convert.ToString(curToken.StartCol));
					result = new FunctionIif(args[0], args[1], args[2]);
					break;
				case "choose":
					if (args.Length <= 2)
						throw new ParserException("Choose function requires at least 2 arguments." + "  At column " + Convert.ToString(curToken.StartCol));
					switch (args[0].GetTypeCode())
					{
						case TypeCode.Double:
						case TypeCode.Single:
						case TypeCode.Int32:
						case TypeCode.Decimal:
						case TypeCode.Int16:
						case TypeCode.Int64:
							break;
						default:
							throw new ParserException("First argument to Choose function must be numeric." + "  At column " + Convert.ToString(curToken.StartCol));
					}
					result = new FunctionChoose(args);
					break;
				case "switch":
					if (args.Length <= 2)
						throw new ParserException("Switch function requires at least 2 arguments." + "  At column " + Convert.ToString(curToken.StartCol));
				    if (args.Length % 2 != 0)
						throw new ParserException("Switch function must have an even number of arguments." + "  At column " + Convert.ToString(curToken.StartCol));
					for (int i=0; i < args.Length; i = i+2)
					{
						if (args[i].GetTypeCode() != TypeCode.Boolean)
							throw new ParserException("Switch function must have a boolean expression every other argument." + "  At column " + Convert.ToString(curToken.StartCol));
					}
					result = new FunctionSwitch(args);
					break;
				case "format":
					if (args.Length > 2 || args.Length < 1)
						throw new ParserException("Format function requires 2 arguments." + "  At column " + Convert.ToString(curToken.StartCol));
					if (args.Length == 1)
					{
						result = new FunctionFormat(args[0], new ConstantString(""));
					}
					else
					{
						if (args[1].GetTypeCode() != TypeCode.String)
							throw new ParserException("Second argument to Format function must be a string." + "  At column " + Convert.ToString(curToken.StartCol));
						result = new FunctionFormat(args[0], args[1]);
					}
					break;
				case "sum":
					scope = ResolveAggrScope(args, 2, out bSimple);
					FunctionAggrSum aggrFS = new FunctionAggrSum(_DataCache, args[0], scope);
					aggrFS.LevelCheck = bSimple;
					result = aggrFS;
					break;
				case "avg":
					scope = ResolveAggrScope(args, 2, out bSimple);
					FunctionAggrAvg aggrFA = new FunctionAggrAvg(_DataCache, args[0], scope);
					aggrFA.LevelCheck = bSimple;
					result = aggrFA;
					break;
				case "min":
					scope = ResolveAggrScope(args, 2, out bSimple);
					FunctionAggrMin aggrFMin = new FunctionAggrMin(_DataCache, args[0], scope);
					aggrFMin.LevelCheck = bSimple;
					result = aggrFMin;
					break;
				case "max":
					scope = ResolveAggrScope(args, 2, out bSimple);
					FunctionAggrMax aggrFMax = new FunctionAggrMax(_DataCache, args[0], scope);
					aggrFMax.LevelCheck = bSimple;
					result = aggrFMax;
					break;
				case "first":
					scope = ResolveAggrScope(args, 2, out bSimple);
					result = new FunctionAggrFirst(_DataCache, args[0], scope);
					break;
				case "last":
					scope = ResolveAggrScope(args, 2, out bSimple);
					result = new FunctionAggrLast(_DataCache, args[0], scope);
					break;
				case "level":
					scope = ResolveAggrScope(args, 1, out bSimple);
					result = new FunctionAggrLevel(scope);
					break;
				case "count":
					scope = ResolveAggrScope(args, 2, out bSimple);
					FunctionAggrCount aggrFC = new FunctionAggrCount(_DataCache, args[0], scope);
					aggrFC.LevelCheck = bSimple;
					result = aggrFC;
					break;
				case "countrows":
					scope = ResolveAggrScope(args, 1, out bSimple);
					FunctionAggrCountRows aggrFCR = new FunctionAggrCountRows(scope);
					aggrFCR.LevelCheck = bSimple;
					result = aggrFCR;
					break;
				case "countdistinct":
					scope = ResolveAggrScope(args, 2, out bSimple);
					FunctionAggrCountDistinct aggrFCD = new FunctionAggrCountDistinct(_DataCache, args[0], scope);
					aggrFCD.LevelCheck = bSimple;
					result = aggrFCD;
					break;
				case "rownumber":
					scope = ResolveAggrScope(args, 1, out bSimple);
					IExpr texpr = new ConstantDouble("0");
					result = new FunctionAggrRvCount(texpr, scope);
					break;
				case "runningvalue":
					if (args.Length < 2 || args.Length > 3)
						throw new ParserException("RunningValue takes 2 or 3 arguments." + "  At column " + Convert.ToString(curToken.StartCol));
					string aggrFunc = args[1].EvaluateString(null);
					if (aggrFunc == null)
						throw new ParserException("RunningValue 'Function' argument is invalid." + "  At column " + Convert.ToString(curToken.StartCol));
					scope = ResolveAggrScope(args, 3, out bSimple);
					switch(aggrFunc.ToLower())
					{
						case "sum":
							result = new FunctionAggrRvSum(_DataCache, args[0], scope);
							break;
						case "avg":
							result = new FunctionAggrRvAvg(_DataCache, args[0], scope);
							break;
						case "count":
							result = new FunctionAggrRvCount(args[0], scope);
							break;
						case "max":
							result = new FunctionAggrRvMax(_DataCache, args[0], scope);
							break;
						case "min":
							result = new FunctionAggrRvMin(_DataCache, args[0], scope);
							break;
						case "stdev":
							result = new FunctionAggrRvStdev(args[0], scope);
							break;
						case "stdevp":
							result = new FunctionAggrRvStdevp(args[0], scope);
							break;
						case "var":
							result = new FunctionAggrRvVar(args[0], scope);
							break;
						case "varp":
							result = new FunctionAggrRvVarp(args[0], scope);
							break;
						default:
							throw new ParserException("RunningValue function '" + aggrFunc + "' is not supported.  At column " + Convert.ToString(curToken.StartCol));
					}
					break;
				case "stdev":
					scope = ResolveAggrScope(args, 2, out bSimple);
					FunctionAggrStdev aggrSDev = new FunctionAggrStdev(_DataCache, args[0], scope);
					aggrSDev.LevelCheck = bSimple;
					result = aggrSDev;
					break;
				case "stdevp":
					scope = ResolveAggrScope(args, 2, out bSimple);
					FunctionAggrStdevp aggrSDevP = new FunctionAggrStdevp(_DataCache, args[0], scope);
					aggrSDevP.LevelCheck = bSimple;
					result = aggrSDevP;
					break;
				case "var":
					scope = ResolveAggrScope(args, 2, out bSimple);
					FunctionAggrVar aggrVar = new FunctionAggrVar(_DataCache, args[0], scope);
					aggrVar.LevelCheck = bSimple;
					result = aggrVar;
					break;
				case "varp":
					scope = ResolveAggrScope(args, 2, out bSimple);
					FunctionAggrVarp aggrVarP = new FunctionAggrVarp(_DataCache, args[0], scope);
					aggrVarP.LevelCheck = bSimple;
					result = aggrVarP;
					break;
				default:
					result = ResolveMethodCall(fullname, args);		// through exception when fails
					break;
			}

			return true;
		}

		private bool IsAggregate(string method, bool onePart)
		{
			if (!onePart)
				return false;
			bool rc;
			switch(method.ToLower())
			{	// this needs to include all aggregate functions
				case "sum":
				case "avg":
				case "min":
				case "max":
				case "first":
				case "last":
				case "count":
				case "countrows":
				case "countdistinct":
				case "stdev":
				case "stdevp":
				case "var":
				case "varp":
				case "rownumber":
				case "runningvalue":
					rc = true;
					break;
				default:
					rc = false;
					break;
			}
			return rc;
		}

		private void ResolveFields(string aggr, ArrayList fargs, ArrayList args)
		{
			if (fargs == null || fargs.Count == 0)
				return;

			// get the scope argument offset 
			int argOffset = aggr.ToLower() == "countrows"? 1: 2;
			DataSet ds;
			if (args.Count == argOffset)
			{
				string dsname=null;
				IExpr e = (IExpr) args[argOffset-1];
				if (e is ConstantString)
					dsname = e.EvaluateString(null);

				if (dsname == null)
					throw new ParserException(string.Format("{0} function's scope must be a constant.", aggr));
				ds = this.idLookup.ScopeDataSet(dsname);
				if (ds == null)
					throw new ParserException(string.Format("Scope '{0}' does not reference a known DataSet.", dsname));
			}
			else
			{
				ds = this.idLookup.ScopeDataSet(null);
				if (ds == null)
					throw new ParserException(string.Format("No scope provided for aggregate function '{0}' but more than one DataSet defined.", aggr));
			}

			foreach (FunctionField f in fargs)
			{
				if (f.Fld != null)
					continue;

				Field dsf = ds.Fields[f.Name];
				if (dsf == null)
					throw new ParserException(string.Format("Field '{0}' is not in DataSet {1}.", f.Name, ds.Name.Nm));

				f.Fld = dsf;
			}
			return;
		}

		private object ResolveAggrScope(IExpr[] args, int indexOfScope, out bool bSimple)
		{
			object scope;
			
			bSimple = true;

			if (args.Length >= indexOfScope)
			{
				string n = args[indexOfScope-1].EvaluateString(null);
				scope = idLookup.LookupScope(n);
				if (scope == null)
				{
					if (n.ToLower() != "nothing")	// allow null to be used
						throw new ParserException(string.Format("Scope '{0}' is not a known Grouping, DataSet or DataRegion name.",n));
				}

				if (args.Length > indexOfScope)	// has recursive/simple been specified
				{
					IdentifierKey k = args[indexOfScope] as IdentifierKey;
					if (k == null)
						throw new ParserException("Illegal scope identifer specified.  At column " + Convert.ToString(curToken.StartCol));
					if (k.Value == IdentifierKeyEnum.Recursive)
						bSimple = false;
				}
			}
			else
			{
				scope = idLookup.LookupGrouping();
				if (scope == null)
				{
					scope = idLookup.LookupMatrix();
					if (scope == null)
						scope = idLookup.ScopeDataSet(null);
				}
			}

			return scope;
		}

		private IExpr ResolveMethodCall(string fullname, IExpr[] args)
		{
			string cls, method;
			int idx = fullname.LastIndexOf('.');
			if (idx > 0)
			{
				cls = fullname.Substring(0, idx);
				method = fullname.Substring(idx+1);
			}
			else
			{
				cls = "";
				method = fullname;
			}

			// Fill out the argument types
			Type[] argTypes = new Type[args.Length];
			for (int i=0; i < args.Length; i++)
			{
				switch (args[i].GetTypeCode())
				{
					case TypeCode.Boolean:
						argTypes[i] = Type.GetType("System.Boolean");
						break;
					case TypeCode.Byte:
						argTypes[i] = Type.GetType("System.Byte");
						break;
					case TypeCode.Char:
						argTypes[i] = Type.GetType("System.Char");
						break;
					case TypeCode.DateTime:
						argTypes[i] = Type.GetType("System.DateTime");
						break;
					case TypeCode.Decimal:
						argTypes[i] = Type.GetType("System.Decimal");
						break;
					case TypeCode.Double:
						argTypes[i] = Type.GetType("System.Double");
						break;
					case TypeCode.Int16:
						argTypes[i] = Type.GetType("System.Int16");
						break;
					case TypeCode.Int32:
						argTypes[i] = Type.GetType("System.Int32");
						break;
					case TypeCode.Int64:
						argTypes[i] = Type.GetType("System.Int64");
						break;
					case TypeCode.Object:
						argTypes[i] = Type.GetType("System.Object");
						break;
					case TypeCode.SByte:
						argTypes[i] = Type.GetType("System.SByte");
						break;
					case TypeCode.Single:
						argTypes[i] = Type.GetType("System.Single");
						break;
					case TypeCode.String:
						argTypes[i] = Type.GetType("System.String");
						break;
					case TypeCode.UInt16:
						argTypes[i] = Type.GetType("System.UInt16");
						break;
					case TypeCode.UInt32:
						argTypes[i] = Type.GetType("System.UInt32");
						break;
					case TypeCode.UInt64:
						argTypes[i] = Type.GetType("System.UInt64");
						break;
					default:
						argTypes[i] = Type.GetType("Object");
						break;
				}
			}
			ReportClass rc = idLookup.LookupInstance(cls);	// is this an instance variable name?
			Type cType=null;
			if (rc == null)
			{
				cType=idLookup.LookupType(cls);				// no, must be a static class reference
			}
			else
			{
				cType= idLookup.LookupType(rc.ClassName);	// yes, use the classname of the ReportClass
			}

			string syscls=null;

			if (cType == null)
			{	// ok try for some of the system functions

				switch(cls)
				{
					case "Math":
						syscls = "System.Math";
						break;
					case "String":
						syscls = "System.String";
						break;
					case "Convert":
						syscls = "System.Convert";
						break;
					case "Financial":
						syscls = "fyiReporting.RDL.Financial";
						break;
					default:
						syscls = "fyiReporting.RDL.VBFunctions";
						break;
				}
				if (syscls != null)
				{
					cType = Type.GetType(syscls);
				}
			}

			if (cType == null)
			{
				string err;
				if (cls == null || cls.Length == 0)
					err = String.Format("Function {0} is not known.", method);
				else
					err = String.Format("Class {0} is not known.", cls);

				throw new ParserException(err);
			}

			IExpr result=null;

//			MethodInfo mInfo = cType.GetMethod(method, BindingFlags.,binder, argTypes, modifiers);
			MethodInfo mInfo = cType.GetMethod(method, argTypes);
			if (mInfo == null)
			{
				string err;
				if (cls == null || cls.Length == 0)
					err = String.Format("Function '{0}' is not known.", method);
				else
					err = String.Format("Function '{0}' of class '{1}' is not known.", method, cls);

				throw new ParserException(err);
			}

			TypeCode tc = Type.GetTypeCode(mInfo.ReturnType);
			if (syscls != null)
				result = new FunctionSystem(syscls, method, args, tc);
			else if (rc == null)
				result = new FunctionCustomStatic(idLookup.CMS, cls, method, args, tc);
			else
				result = new FunctionCustomInstance(rc, method, args, tc);

			return result;
		}
	}

}
