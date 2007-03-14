using System;
using System.Collections;

namespace OpenDental{
	
	///<summary>Corresponds to the programproperty table in the database(does not exist yet).  Stores settings for linking to other programs.</summary>
	public struct ProgramProperty{
		///<summary>Primary key.</summary>
		public int ProgramPropertyNum;
		///<summary>Foreign key to program.ProgramNum</summary>
		public int ProgramNum;
		///<summary>The description or prompt for this property.</summary>
		public string PropertyDesc;
		///<summary>The value.</summary>
		public string PropertyValue;
	}

	/*=========================================================================================
	=================================== class ProgramProperties ==========================================*/

	///<summary></summary>
	public class ProgramProperties:DataClass{
		///<summary></summary>
		public static ProgramProperty Cur;
		///<summary></summary>
		public static ProgramProperty[] List;
		///<summary></summary>
		public static ArrayList ForProgram;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText = 
				"SELECT * from programproperty";
			FillTable();
			List=new ProgramProperty[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i].ProgramPropertyNum =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ProgramNum         =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PropertyDesc       =PIn.PString(table.Rows[i][2].ToString());
				List[i].PropertyValue      =PIn.PString(table.Rows[i][3].ToString());
				//List[i].ValueType          =(FieldValueType)PIn.PInt(table.Rows[i][4].ToString());
			}
			//MessageBox.Show();
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE programproperty SET "
				+"ProgramNum = '"     +POut.PInt   (Cur.ProgramNum)+"'"
				+",PropertyDesc  = '" +POut.PString(Cur.PropertyDesc)+"'"
				+",PropertyValue = '" +POut.PString(Cur.PropertyValue)+"'"
				+" WHERE ProgramPropertyNum = '"+POut.PInt(Cur.ProgramPropertyNum)+"'";
			NonQ();
		}

		///<summary>This can only be called from ClassConversions. Users not allowed to add properties so there is no user interface.</summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
				+") VALUES("
				+"'"+POut.PInt   (Cur.ProgramNum)+"', "
				+"'"+POut.PString(Cur.PropertyDesc)+"', "
				+"'"+POut.PString(Cur.PropertyValue)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ();
			//Cur.ProgramNum=InsertID;
		}

		
		///<summary>This can only be called from ClassConversions. Users not allowed to delete properties so there is no user interface.</summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from programproperty WHERE programpropertynum = '"+Cur.ProgramPropertyNum.ToString()+"'";
			NonQ();
		}

		///<summary>Fills ForProgram with programproperties attached to the Programs.Cur</summary>
		public static void GetForProgram(){
			ForProgram=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProgramNum==Programs.Cur.ProgramNum){
					ForProgram.Add(List[i]);
				}
			}
		}

		///<summary>After GetForProgram has been run, this gets one of those properties into Cur.</summary>
		public static void GetCur(string desc){
			for(int i=0;i<ForProgram.Count;i++){
				if(((ProgramProperty)ForProgram[i]).PropertyDesc==desc){
					Cur=(ProgramProperty)ForProgram[i];
					return;
				}
			}
		}




		
	}

	

	


}










