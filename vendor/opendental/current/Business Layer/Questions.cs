using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental {

	/// <summary>Corresponds to the Question table in the database.  Each row is one Question for one patient.  If a patient has never filled out a questionnaire, then they will have no rows in this table.</summary>
	public class Question{
		///<summary>Primary key.</summary>
		public int QuestionNum;
		///<summary>fk to patient.PatNum</summary>
		public int PatNum;
		///<summary>The order that this question shows in the list.</summary>
		public int ItemOrder;
		///<summary>The original question.</summary>
		public string Description;
		///<summary>The answer to the question in text form.</summary>
		public string Answer;

		///<summary></summary>
		public Question Copy() {
			Question q=new Question();
			q.QuestionNum=QuestionNum;
			q.PatNum=PatNum;
			q.ItemOrder=ItemOrder;
			q.Description=Description;
			q.Answer=Answer;
			return q;
		}

		///<summary></summary>
		public void Update() {
			string command="UPDATE question SET " 
				+"PatNum = '"      +POut.PInt   (PatNum)+"'"
				+",ItemOrder = '"  +POut.PInt   (ItemOrder)+"'"
				+",Description = '"+POut.PString(Description)+"'"
				+",Answer = '"     +POut.PString(Answer)+"'"
				+" WHERE QuestionNum  ='"+POut.PInt   (QuestionNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			if(Prefs.RandomKeys) {
				QuestionNum=MiscData.GetKey("question","QuestionNum");
			}
			string command="INSERT INTO question (";
			if(Prefs.RandomKeys) {
				command+="QuestionNum,";
			}
			command+="PatNum,ItemOrder,Description,Answer) VALUES(";
			if(Prefs.RandomKeys) {
				command+="'"+POut.PInt(QuestionNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PInt   (ItemOrder)+"', "
				+"'"+POut.PString(Description)+"', "
				+"'"+POut.PString(Answer)+"')";
			DataConnection dcon=new DataConnection();
			if(Prefs.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				QuestionNum=dcon.InsertID;
			}
		}

		//<summary>I can't see how this could ever be used.</summary>
		/*public void Delete() {
			string command="DELETE FROM question WHERE QuestionNum ="+POut.PInt(QuestionNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}*/

	}

	/*================================================================================================================
	==================================================== class Questions =============================================*/

	///<summary></summary>
	public class Questions {
		///<summary>Gets a list of all Questions for a given patient.  Sorted by ItemOrder.</summary>
		public static Question[] Refresh(int patNum) {
			string command="SELECT * FROM question WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY ItemOrder";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			Question[] List=new Question[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Question();
				List[i].QuestionNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum     = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ItemOrder  = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].Description= PIn.PString(table.Rows[i][3].ToString());
				List[i].Answer     = PIn.PString(table.Rows[i][4].ToString());
			}
			return List;
		}

		///<summary>Checks the database to see if the specified patient has previously answered a questionnaire.</summary>
		public static bool PatHasQuest(int patNum){
			string command="SELECT COUNT(*) FROM question WHERE PatNum="+POut.PInt(patNum);
			DataConnection dcon=new DataConnection();
			if(dcon.GetCount(command)=="0"){
				return false;			
			}
			return true;
		}

		///<summary>Deletes all questions for this patient.</summary>
		public static void DeleteAllForPat(int patNum) {
			string command="DELETE FROM question WHERE PatNum ="+POut.PInt(patNum);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}
	
		
		
	}

		



		
	

	

	


}










