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
using System.Xml;
using System.Collections;
using System.Globalization;

namespace fyiReporting.RDL
{
	///<summary>
	/// Represent a report parameter (name, default value, runtime value,
	///</summary>
	[Serializable]
	internal class ReportParameter : ReportLink
	{
		Name _Name;			// Name of the parameter 
		// Note: Parameter names need only be
		// unique within the containing Parameters collection
		TypeCode _dt;	// The data type of the parameter
		bool _NumericType=false;	// true if _dt is a numeric type
		bool _Nullable;		// Indicates the value for this parameter is allowed to be Null.
		DefaultValue _DefaultValue;		// Default value to use for the parameter (if not provided by the user)
		// If no value is provided as a part of the
		//	  definition or by the user, the value is
		// null. Required if there is no Prompt and
		//  either Nullable is False or a ValidValues
		// list is provided that does not contain Null
		// (an omitted Value).
		bool _AllowBlank;	// Indicates the value for this parameter is
		// allowed to be the empty string. Ignored
		// if DataType is not string.
		string _Prompt;		// The user prompt to display when asking
		// for parameter values
		// If omitted, the user should not be
		// prompted for a value for this parameter.
		ValidValues _ValidValues; // Possible values for the parameter (for an
		//	end-user prompting interface)
		TrueFalseAutoEnum _UsedInQuery; // Enum True | False | Auto (default)
		//	Indicates whether or not the parameter is
		//	used in a query in the report. This is
		//	needed to determine if the queries need
		//	to be re-executed if the parameter
		//	changes. Auto indicates the
		//	UsedInQuery setting should be
		//	autodetected as follows: True if the
		//	parameter is referenced in any query
		//	value expression.		
		[NonSerialized] object _RuntimeValue;		// runtime value
	
		internal ReportParameter(Report r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Name=null;
			_dt = TypeCode.Object;
			_Nullable = true;
			_DefaultValue=null;
			_AllowBlank=true;
			_Prompt=null;
			_ValidValues=null;
			_UsedInQuery = TrueFalseAutoEnum.Auto;
			_RuntimeValue = null;
			// Run thru the attributes
			foreach(XmlAttribute xAttr in xNode.Attributes)
			{
				switch (xAttr.Name)
				{
					case "Name":
						_Name = new Name(xAttr.Value);
						break;
				}
			}
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DataType":
						_dt = DataType.GetStyle(xNodeLoop.InnerText, this.OwnerReport);
						_NumericType = DataType.IsNumeric(_dt);
						break;
					case "Nullable":
						_Nullable = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "DefaultValue":
						_DefaultValue = new DefaultValue(r, this, xNodeLoop);
						break;
					case "AllowBlank":
						_AllowBlank = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "Prompt":
						_Prompt = xNodeLoop.InnerText;
						break;
					case "ValidValues":
						_ValidValues = new ValidValues(r, this, xNodeLoop);
						break;
					case "UsedInQuery":
						_UsedInQuery = TrueFalseAuto.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown ReportParameter element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_Name == null)
				OwnerReport.rl.LogError(8, "ReportParameter name is required but not specified.");

			if (_dt == TypeCode.Object)
				OwnerReport.rl.LogError(8, string.Format("ReportParameter DataType is required but not specified or invalid for {0}.", _Name==null? "<unknown name>": _Name.Nm));
		}

		override internal void FinalPass()
		{
			if (_DefaultValue != null)
				_DefaultValue.FinalPass();
			if (_ValidValues != null)
				_ValidValues.FinalPass();
			return;
		}

		internal Name Name
		{
			get { return  _Name; }
			set {  _Name = value; }
		}

		internal object RuntimeValue
		{
			get 
			{ 
				if (_RuntimeValue != null)
					return _RuntimeValue; 
				if (_DefaultValue == null)
					return null;
				
				object[] result = _DefaultValue.Value;
				if (result == null)
					return null;
				object v = result[0];
				if (v is String && _NumericType)
					v = ConvertStringToNumber((string) v);

				_RuntimeValue = Convert.ChangeType(v, _dt);

				return _RuntimeValue;
			}
			set 
			{ 
				if (!AllowBlank && _dt == TypeCode.String && (string) value == "")
					throw new ArgumentException(string.Format("Empty string isn't allowed for {0}.", Name.Nm));
				try 
				{
					object v = value;
					if (v is String && _NumericType)
						v = ConvertStringToNumber((string) v);
					_RuntimeValue = Convert.ChangeType(v, _dt); 
				}
				catch (Exception e)
				{
					// illegal parameter passed
					OwnerReport.rl.LogError(4, "Illegal parameter value for '" + Name.Nm + "' provided.  Value =" + value.ToString());
					throw new ArgumentException(string.Format("Unable to convert '{0}' to {1} for {2}", value, _dt, Name.Nm),e);
				}
			}
		}

		private object ConvertStringToNumber(string newv)
		{
			// remove any commas, currency symbols (internationalized)
			NumberFormatInfo nfi = NumberFormatInfo.CurrentInfo;
			newv = newv.Replace(nfi.NumberGroupSeparator, "");
			newv = newv.Replace(nfi.CurrencySymbol, "");
			return newv;
		}

		internal TypeCode dt
		{
			get { return  _dt; }
			set {  _dt = value; }
		}

		internal bool Nullable
		{
			get { return  _Nullable; }
			set {  _Nullable = value; }
		}

		internal DefaultValue DefaultValue
		{
			get { return  _DefaultValue; }
			set {  _DefaultValue = value; }
		}

		internal bool AllowBlank
		{
			get { return  _AllowBlank; }
			set {  _AllowBlank = value; }
		}

		internal string Prompt
		{
			get { return  _Prompt; }
			set {  _Prompt = value; }
		}

		internal ValidValues ValidValues
		{
			get { return  _ValidValues; }
			set {  _ValidValues = value; }
		}
		

		internal TrueFalseAutoEnum UsedInQuery
		{
			get { return  _UsedInQuery; }
			set {  _UsedInQuery = value; }
		}
	}
/// <summary>
/// Public class used to pass user provided report parameters.
/// </summary>
	public class UserReportParameter
	{
		ReportParameter _rp;
		object[] _DefaultValue;
		string[] _DisplayValues;
		object[] _DataValues;

		internal UserReportParameter(ReportParameter rp)
		{
			_rp = rp;
		}

		public string Name
		{
			get { return  _rp.Name.Nm; }
		}

		public TypeCode dt
		{
			get { return  _rp.dt; }
		}

		public bool Nullable
		{
			get { return  _rp.Nullable; }
		}

		public object[] DefaultValue
		{
			get 
			{ 
				if (_DefaultValue == null)
				{
					if (_rp.DefaultValue != null)
						_DefaultValue = _rp.DefaultValue.ValuesCalc();
				}
				return _DefaultValue;
			}
		}

		public bool AllowBlank
		{
			get { return  _rp.AllowBlank; }
		}

		public string Prompt
		{
			get { return  _rp.Prompt; }
		}

		public string[] DisplayValues
		{
			get 
			{
				if (_DisplayValues == null)
				{
					if (_rp.ValidValues != null)
						_DisplayValues = _rp.ValidValues.DisplayValues();
				}
				return  _DisplayValues;		 
			}
		}

		public object[] DataValues
		{
			get 
			{
				if (_DataValues == null)
				{
					if (_rp.ValidValues != null)
						_DataValues = _rp.ValidValues.DataValues();
				}
				return  _DataValues;		 
			}
		}

		public object Value
		{
			get { return _rp.RuntimeValue; }
			set 
			{
				object dvalue = value;
				if (DisplayValues != null && 
					DataValues != null && 
					DisplayValues.Length == DataValues.Length)		// this should always be true
				{	// if display values are provided then we may need to 
					//  may the provided value with a display value and use
					//  the cooresponding data value
					for (int index=0; index < DisplayValues.Length; index++)
					{
						if (DisplayValues[index].CompareTo(value.ToString()) == 0)
						{
							dvalue = DataValues[index];
							break;
						}
					}
				}

				_rp.RuntimeValue = dvalue; 
			}
		}
	}
}
