using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental {

	///<summary>Each row represents one question on the medical history questionnaire.  Later, other questionnaires will be allowed, but for now, all questions are on one questionnaire for the patient.  This table has no dependencies, since the question is copied when added to a patient record.  Any row can be freely deleted or altered without any problems.</summary>
	public class QuestionDef{
		///<summary>Primary key.</summary>
		public int QuestionDefNum;
		///<summary>The question as presented to the patient.</summary>
		public string Description;
		///<summary>The order that the Questions will show.</summary>
		public int ItemOrder;
		///<summary>Enum:QuestionType</summary>
		public QuestionType QuestType;

		///<summary></summary>
		public QuestionDef Copy() {
			QuestionDef q=new QuestionDef();
			q.QuestionDefNum=QuestionDefNum;
			q.Description=Description;
			q.ItemOrder=ItemOrder;
			q.QuestType=QuestType;
			return q;
		}

		///<summary></summary>
		public void Update() {
			string command="UPDATE questiondef SET " 
				+"QuestionDefNum = '"+POut.PInt   (QuestionDefNum)+"'"
				+",Description = '"  +POut.PString(Description)+"'"
				+",ItemOrder = '"    +POut.PInt   (ItemOrder)+"'"
				+",QuestType = '"    +POut.PInt   ((int)QuestType)+"'"
				+" WHERE QuestionDefNum  ='"+POut.PInt   (QuestionDefNum)+"'";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Insert() {
			string command="INSERT INTO questiondef (Description,ItemOrder,QuestType) VALUES("
				+"'"+POut.PString(Description)+"', "
				+"'"+POut.PInt   (ItemOrder)+"', "
				+"'"+POut.PInt   ((int)QuestType)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command,true);
			QuestionDefNum=dcon.InsertID;
		}

		///<summary>Ok to delete whenever, because no patients are tied to this table by any dependencies.</summary>
		public void Delete() {
			DataConnection dcon=new DataConnection();
			string command="DELETE FROM questiondef WHERE QuestionDefNum ="+POut.PInt(QuestionDefNum);
			dcon.NonQ(command);
		}

	}

	/*================================================================================================================
	==================================================== class QuestionDefs =============================================*/

	///<summary></summary>
	public class QuestionDefs {
	
		///<summary>Gets a list of all QuestionDefs.</summary>
		public static QuestionDef[] Refresh() {
			string command="SELECT * FROM questiondef ORDER BY ItemOrder";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			QuestionDef[] List=new QuestionDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new QuestionDef();
				List[i].QuestionDefNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description   = PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder     = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].QuestType     = (QuestionType)PIn.PInt  (table.Rows[i][3].ToString());
			}
			return List;
		}

		///<summary>Moves the selected item up in the list.</summary>
		public static void MoveUp(int selected,QuestionDef[] List){
			if(selected<0) {
				throw new ApplicationException(Lan.g("QuestionDefs","Please select an item first."));
			}
			if(selected==0) {//already at top
				return;
			}
			if(selected>List.Length-1){
				throw new ApplicationException(Lan.g("QuestionDefs","Invalid selection."));
			}
			SetOrder(selected-1,List[selected].ItemOrder,List);
			SetOrder(selected,List[selected].ItemOrder-1,List);
		}

		///<summary></summary>
		public static void MoveDown(int selected,QuestionDef[] List) {
			if(selected<0) {
				throw new ApplicationException(Lan.g("QuestionDefs","Please select an item first."));
			}
			if(selected==List.Length-1){//already at bottom
				return;
			}
			if(selected>List.Length-1) {
				throw new ApplicationException(Lan.g("QuestionDefs","Invalid selection."));
			}
			SetOrder(selected+1,List[selected].ItemOrder,List);
			SetOrder(selected,List[selected].ItemOrder+1,List);
			//selected+=1;
		}

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum,int myItemOrder,QuestionDef[] List) {
			QuestionDef temp=List[mySelNum];
			temp.ItemOrder=myItemOrder;
			temp.Update();
		}

		
		
	}

		



		
	

	

	


}










