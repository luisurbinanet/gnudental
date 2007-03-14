using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental {

	///<summary>Always attached to covcats, this describes the span of procedure codes to which the category applies.</summary>
	public class CovSpan {
		///<summary>Primary key.</summary>
		public int CovSpanNum;
		///<summary>FK to covcat.CovCatNum.</summary>
		public int CovCatNum;
		///<summary>Lower range of the span.  Does not need to be a valid code.</summary>
		public string FromCode;
		///<summary>Upper range of the span.  Does not need to be a valid code.</summary>
		public string ToCode;

		///<summary></summary>
		public CovSpan Copy() {
			CovSpan c=new CovSpan();
			c.CovSpanNum=CovSpanNum;
			c.CovCatNum=CovCatNum;
			c.FromCode=FromCode;
			c.ToCode=ToCode;
			return c;
		}

		///<summary></summary>
		private void Update() {
			string command="UPDATE covspan SET "
				+"CovCatNum = '"+POut.PInt   (CovCatNum)+"'"
				+",FromCode = '"+POut.PString(FromCode)+"'"
				+",ToCode = '"  +POut.PString(ToCode)+"'"
				+" WHERE CovSpanNum = '"+POut.PInt(CovSpanNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert() {
			string command="INSERT INTO covspan (CovCatNum,"
				+"FromCode,ToCode) VALUES("
				+"'"+POut.PInt   (CovCatNum)+"', "
				+"'"+POut.PString(FromCode)+"', "
				+"'"+POut.PString(ToCode)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void InsertOrUpdate(bool IsNew){
			if(FromCode=="" || ToCode=="") {
				throw new ApplicationException(Lan.g("FormInsSpanEdit","Codes not allowed to be blank."));
			}
			if(String.Compare(ToCode,FromCode)<0){
				throw new ApplicationException(Lan.g("FormInsSpanEdit","From Code must be less than To Code.  Remember that the comparison is alphabetical, not numeric.  For instance, 100 would come before 2, but after 02."));
			}
			if(IsNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary></summary>
		public void Delete() {
			string command="DELETE FROM covspan"
				+" WHERE CovSpanNum = '"+POut.PInt(CovSpanNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class CovSpans ==========================================*/

	///<summary></summary>
	public class CovSpans {
		///<summary></summary>
		public static CovSpan[] List;

		///<summary></summary>
		public static void Refresh() {
			string command=
				"SELECT * from covspan"
				+" ORDER BY FromCode";
				//+" ORDER BY CovCatNum";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			List=new CovSpan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new CovSpan();
				List[i].CovSpanNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CovCatNum   = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].FromCode    = PIn.PString(table.Rows[i][2].ToString());
				List[i].ToCode      = PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static int GetCat(string myADACode){
			int retVal=0;
			for(int i=0;i<List.Length;i++){
				if(String.Compare(myADACode,List[i].FromCode)>=0
					&& String.Compare(myADACode,List[i].ToCode)<=0){
					retVal=List[i].CovCatNum;
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static CovSpan[] GetForCat(int catNum){
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].CovCatNum==catNum){
					AL.Add(List[i].Copy());
				}
			}
			CovSpan[] retVal=new CovSpan[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

	}

	


}









