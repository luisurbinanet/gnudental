using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the screen table in the database. Used in public health.</summary>
	///<remarks>This screening table is meant to be general purpose.  It is compliant with the popular Basic Screening Survey.  It is also designed with minimal foreign keys and can be easily adapted to a palm or pocketPC.  This table can be used with only the screengroup table, but is more efficient if provider, school, and county tables are also available.</remarks>
	public struct Screen{
		///<summary>Primary key</summary>
		public int ScreenNum;
		///<summary>The date of the screening.</summary>
		public DateTime ScreenDate;
		///<summary>Foreign key to school.SchoolName, although it will not crash if key absent.</summary>
		public string GradeSchool;
		///<summary>Foreign key to county.CountyName, although it will not crash if key absent.</summary>
		public string County;
		///<summary>See the PlaceOfService enum.</summary>
		public PlaceOfService PlaceService;
		///<summary>Foreign key to provider.ProvNum.  ProvNAME is always entered, but ProvNum supplements it by letting user select from list.  When entering a provNum, the name will be filled in automatically. Can be 0 if the provider is not in the list, but provName is required.</summary>
		public int ProvNum;
		///<summary>Required.</summary>
		public string ProvName;
		///<summary>See the PatientGender enumeration.</summary>
		public PatientGender Gender;
		///<summary>Race and ethnicity. See the PatientRace enum.</summary>
		public PatientRace Race;
		///<summary>See the PatientGrade enumeration.</summary>
		public PatientGrade GradeLevel;
		///<summary>Age of patient at the time the screening was done. Faster than recording birthdates.</summary>
		public int Age;
		///<summary>See the TreatmentUrgency enumeration.</summary>
		public TreatmentUrgency Urgency;
		///<summary>Set to true if patient has cavities.</summary>
		public YN HasCaries;
		///<summary>Set to true if patient needs sealants.</summary>
		public YN NeedsSealants;
		///<summary></summary>
		public YN CariesExperience;
		///<summary></summary>
		public YN EarlyChildCaries;
		///<summary></summary>
		public YN ExistingSealants;
		///<summary></summary>
		public YN MissingAllTeeth;
		///<summary>Optional</summary>
		public DateTime Birthdate;
		///<summary>Foreign Key to screengroup.ScreenGroupNum.</summary>
		public int ScreenGroupNum;
		///<summary>The order of this item within its group.</summary>
		public int ScreenGroupOrder;
		///<summary></summary>
		public string Comments;
	}

	/*=========================================================================================
		=================================== class Screens ===========================================*/
  ///<summary></summary>
	public class Screens:DataClass{
		///<summary></summary>
		public static Screen Cur;
		///<summary></summary>
		public static Screen[] List;

		///<summary></summary>
		public static void Refresh(int screenGroupNum){
			cmd.CommandText =
				"SELECT * from screen "
				+"WHERE ScreenGroupNum = '"+POut.PInt(screenGroupNum)+"' "
				+"ORDER BY ScreenGroupOrder";
			FillTable();
			List=new Screen[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ScreenNum       =                  PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ScreenDate      =                  PIn.PDate  (table.Rows[i][1].ToString());
				List[i].GradeSchool     =                  PIn.PString(table.Rows[i][2].ToString());
				List[i].County          =                  PIn.PString(table.Rows[i][3].ToString());
				List[i].PlaceService    =(PlaceOfService)  PIn.PInt   (table.Rows[i][4].ToString());
				List[i].ProvNum         =                  PIn.PInt   (table.Rows[i][5].ToString());
				List[i].ProvName        =                  PIn.PString(table.Rows[i][6].ToString());
				List[i].Gender          =(PatientGender)   PIn.PInt   (table.Rows[i][7].ToString());
				List[i].Race            =(PatientRace)     PIn.PInt   (table.Rows[i][8].ToString());
				List[i].GradeLevel      =(PatientGrade)    PIn.PInt   (table.Rows[i][9].ToString());
				List[i].Age             =                  PIn.PInt   (table.Rows[i][10].ToString());
				List[i].Urgency         =(TreatmentUrgency)PIn.PInt   (table.Rows[i][11].ToString());
				List[i].HasCaries       =(YN)              PIn.PInt   (table.Rows[i][12].ToString());
				List[i].NeedsSealants   =(YN)              PIn.PInt   (table.Rows[i][13].ToString());
				List[i].CariesExperience=(YN)              PIn.PInt   (table.Rows[i][14].ToString());
				List[i].EarlyChildCaries=(YN)              PIn.PInt   (table.Rows[i][15].ToString());
				List[i].ExistingSealants=(YN)              PIn.PInt   (table.Rows[i][16].ToString());
				List[i].MissingAllTeeth =(YN)              PIn.PInt   (table.Rows[i][17].ToString());
				List[i].Birthdate       =                  PIn.PDate  (table.Rows[i][18].ToString());
				List[i].ScreenGroupNum  =                  PIn.PInt   (table.Rows[i][19].ToString());
				List[i].ScreenGroupOrder=                  PIn.PInt   (table.Rows[i][20].ToString());
				List[i].Comments        =                  PIn.PString(table.Rows[i][21].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			if(Prefs.RandomKeys){
				Cur.ScreenNum=MiscData.GetKey("screen","ScreenNum");
			}
			cmd.CommandText="INSERT INTO screen (";
			if(Prefs.RandomKeys){
				cmd.CommandText+="ScreenNum,";
			}
			cmd.CommandText+="ScreenDate,GradeSchool,County,PlaceService,"
				+"ProvNum,ProvName,Gender,Race,GradeLevel,Age,Urgency,HasCaries,NeedsSealants,"
				+"CariesExperience,EarlyChildCaries,ExistingSealants,MissingAllTeeth,Birthdate,"
				+"ScreenGroupNum,ScreenGroupOrder,Comments) VALUES(";
			if(Prefs.RandomKeys){
				cmd.CommandText+="'"+POut.PInt(Cur.ScreenNum)+"', ";
			}
			cmd.CommandText+=
				 "'"+POut.PDate  (Cur.ScreenDate)+"', "
				+"'"+POut.PString(Cur.GradeSchool)+"', "
				+"'"+POut.PString(Cur.County)+"', "
				+"'"+POut.PInt   ((int)Cur.PlaceService)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PString(Cur.ProvName)+"', "
				+"'"+POut.PInt   ((int)Cur.Gender)+"', "
				+"'"+POut.PInt   ((int)Cur.Race)+"', "
				+"'"+POut.PInt   ((int)Cur.GradeLevel)+"', "
				+"'"+POut.PInt   (Cur.Age)+"', "
				+"'"+POut.PInt   ((int)Cur.Urgency)+"', "
				+"'"+POut.PInt   ((int)Cur.HasCaries)+"', "
				+"'"+POut.PInt   ((int)Cur.NeedsSealants)+"', "
				+"'"+POut.PInt   ((int)Cur.CariesExperience)+"', "
				+"'"+POut.PInt   ((int)Cur.EarlyChildCaries)+"', "
				+"'"+POut.PInt   ((int)Cur.ExistingSealants)+"', "
				+"'"+POut.PInt   ((int)Cur.MissingAllTeeth)+"', "
				+"'"+POut.PDate  (Cur.Birthdate)+"', "
				+"'"+POut.PInt   (Cur.ScreenGroupNum)+"', "
				+"'"+POut.PInt   (Cur.ScreenGroupOrder)+"', "
				+"'"+POut.PString(Cur.Comments)+"')";
			if(Prefs.RandomKeys){
				NonQ();
			}
			else{
 				NonQ(true);
				Cur.ScreenNum=InsertID;
			}
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE screen SET "
				+"ScreenDate     ='"    +POut.PDate  (Cur.ScreenDate)+"'"
				+",GradeSchool ='"      +POut.PString(Cur.GradeSchool)+"'"
				+",County ='"           +POut.PString(Cur.County)+"'"
				+",PlaceService ='"     +POut.PInt   ((int)Cur.PlaceService)+"'"
				+",ProvNum ='"          +POut.PInt   (Cur.ProvNum)+"'"
				+",ProvName ='"         +POut.PString(Cur.ProvName)+"'"
				+",Gender ='"           +POut.PInt   ((int)Cur.Gender)+"'"
				+",Race ='"             +POut.PInt   ((int)Cur.Race)+"'"
				+",GradeLevel ='"       +POut.PInt   ((int)Cur.GradeLevel)+"'"
				+",Age ='"              +POut.PInt   (Cur.Age)+"'"
				+",Urgency ='"          +POut.PInt   ((int)Cur.Urgency)+"'"
				+",HasCaries ='"      +POut.PInt   ((int)Cur.HasCaries)+"'"
				+",NeedsSealants ='"    +POut.PInt   ((int)Cur.NeedsSealants)+"'"
				+",CariesExperience ='" +POut.PInt   ((int)Cur.CariesExperience)+"'"
				+",EarlyChildCaries ='" +POut.PInt   ((int)Cur.EarlyChildCaries)+"'"
				+",ExistingSealants ='" +POut.PInt   ((int)Cur.ExistingSealants)+"'"
				+",MissingAllTeeth ='"  +POut.PInt   ((int)Cur.MissingAllTeeth)+"'"
				+",Birthdate ='"        +POut.PDate  (Cur.Birthdate)+"'"
				+",ScreenGroupNum ='"   +POut.PInt   (Cur.ScreenGroupNum)+"'"
				+",ScreenGroupOrder ='" +POut.PInt   (Cur.ScreenGroupOrder)+"'"
				+",Comments ='"         +POut.PString(Cur.Comments)+"'"
				+" WHERE ScreenNum = '" +POut.PInt(Cur.ScreenNum)+"'";
			NonQ();
		}

		///<summary>Updates all screens for a group with the date,prov, and location info of the current group.</summary>
		public static void UpdateForGroup(){
			cmd.CommandText = "UPDATE screen SET "
				+"ScreenDate     ='"    +POut.PDate  (ScreenGroups.Cur.SGDate)+"'"
				+",GradeSchool ='"      +POut.PString(ScreenGroups.Cur.GradeSchool)+"'"
				+",County ='"           +POut.PString(ScreenGroups.Cur.County)+"'"
				+",PlaceService ='"     +POut.PInt   ((int)ScreenGroups.Cur.PlaceService)+"'"
				+",ProvNum ='"          +POut.PInt   (ScreenGroups.Cur.ProvNum)+"'"
				+",ProvName ='"         +POut.PString(ScreenGroups.Cur.ProvName)+"'"
				+" WHERE ScreenGroupNum = '" +ScreenGroups.Cur.ScreenGroupNum.ToString()+"'";
			NonQ();
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from screen WHERE ScreenNum = '"+POut.PInt(Cur.ScreenNum)+"'";
			NonQ();
		}


	}

	

}













