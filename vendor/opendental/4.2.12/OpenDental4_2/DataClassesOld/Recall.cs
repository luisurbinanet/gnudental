using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	#region notes to self 
	//Possible structure of recalltype table:
	//RecallTypeNum: primary key
	//Description
	//DefaultInterval
	//TimePattern: for scheduling the appointment
	//Procedures: what procedures to put on that appointment. comma delimited.
	
	//a procedure could be attached to one recall type, keeping the link as a field in the procedurecode table, or it could be attached to multiple recall types, requiring a linking table.  In either case, the interface will from the recall setup window.
	#endregion	
	///<summary>Corresponds to the recall table in the database. See remarks for sychronization details.</summary>
	///<remarks>A patient can only have one recall date set for each type (there is currently only one type).  The recall table stores a few dates that must be kept synchronized with other information in the database.  This is difficult.  Anytime one of the following items changes, things need to be synchronized: procedurecode.SetRecall, any procedurelog change for a patient (procs added, deleted, completed, status changed, date changed, etc), patient status changed.  There are expected to be a few bugs in the synchronization logic, so anytime a patient's recall is opened, it will also update.
	///
	///During synchronization, the program will frequently alter DateDueCalc, DateDue, and DatePrevious based on trigger procs.  The system will also add and delete recalls as necessary. But it will not delete a recall unless all values are default and there is no useful information.  When a user tries to delete a recall, they will only be successful if the trigger conditions do not apply.  Otherwise, they will have to disable the recall instead.</remarks>
	public class Recall{
		///<summary>Primary key.</summary>
		public int RecallNum;
		///<summary>Foreign key to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Not editable.  The calculated date due. Generated by the program and subject to change anytime the conditions change. It can be blank (0001-01-01) if no appropriate triggers. </summary>
		public DateTime DateDueCalc;
		///<summary>This is the date that is actually used when doing reports for recall. It will usually be the same as DateDueCalc unless user has changed it. System will only update this field if it is the same as DateDueCalc.  Otherwise, it will be left alone.  Gets cleared along with DateDueCalc when resetting recall.  When setting disabled, this field will also be cleared.  This is the field to use if converting from another software.</summary>
		public DateTime DateDue;
		///<summary>Not editable. Previous date that procedures were done to trigger this recall. It is calculated and enforced automatically.  If you want to affect this date, add a procedure to the chart with a status of C, EC, or EO.</summary>
		public DateTime DatePrevious;
		///<summary>The interval between recalls.  See the Interval struct for an explanation.</summary>
		public Interval RecallInterval;
		///<summary>Foreign key to Definition.DefNum, or 0 for none.</summary>
		public int RecallStatus;
		///<summary>An administrative note for staff use.</summary>
		public string Note;
		///<summary>If true, this recall type will be disabled (there's only one type right now). This is usually used rather deleting the recall type from the patient because the program must enforce the trigger conditions for all patients.</summary>
		public bool IsDisabled;
		//Type: RecallType, once we have multiple recall types

		///<summary>Returns a copy of this Recall.</summary>
		public Recall Copy(){
			Recall recall=new Recall();
			recall.RecallNum=RecallNum;
			recall.PatNum=PatNum;
			recall.DateDueCalc=DateDueCalc;
			recall.DateDue=DateDue;
			recall.DatePrevious=DatePrevious;
			recall.RecallInterval=RecallInterval;
			recall.RecallStatus=RecallStatus;
			recall.Note=Note;
			recall.IsDisabled=IsDisabled;
			return recall;
		}

		///<summary></summary>
		public void Insert(){
			if(Prefs.RandomKeys){
				RecallNum=MiscData.GetKey("recall","RecallNum");
			}
			string command= "INSERT INTO recall (";
			if(Prefs.RandomKeys){
				command+="RecallNum,";
			}
			command+="PatNum,DateDueCalc,DateDue,DatePrevious,"
				+"RecallInterval,RecallStatus,Note,IsDisabled"
				+") VALUES(";
			if(Prefs.RandomKeys){
				command+="'"+POut.PInt(RecallNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (PatNum)+"', "
				+"'"+POut.PDate  (DateDueCalc)+"', "
				+"'"+POut.PDate  (DateDue)+"', "
				+"'"+POut.PDate  (DatePrevious)+"', "
				+"'"+POut.PInt   (RecallInterval.ToInt())+"', "
				+"'"+POut.PInt   (RecallStatus)+"', "
				+"'"+POut.PString(Note)+"', "
				+"'"+POut.PBool  (IsDisabled)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			if(Prefs.RandomKeys){
				dcon.NonQ(command);
			}
			else{
 				dcon.NonQ(command,true);
				RecallNum=dcon.InsertID;
			}
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE recall SET "
				+"PatNum = '"          +POut.PInt   (PatNum)+"'"
				+",DateDueCalc = '"    +POut.PDate  (DateDueCalc)+"' "
				+",DateDue = '"        +POut.PDate  (DateDue)+"' "
				+",DatePrevious = '"   +POut.PDate  (DatePrevious)+"' "
				+",RecallInterval = '" +POut.PInt   (RecallInterval.ToInt())+"' "
				+",RecallStatus= '"    +POut.PInt   (RecallStatus)+"' "
				+",Note = '"           +POut.PString(Note)+"' "
				+",IsDisabled = '"     +POut.PBool  (IsDisabled)+"' "
				+" WHERE RecallNum = '"+POut.PInt (RecallNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE from recall WHERE RecallNum = '"+POut.PInt(RecallNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary>Will only return true if not disabled, date previous is empty, DateDue is same as DateDueCalc, etc.</summary>
		public bool IsAllDefault(){
			if(IsDisabled
				|| DatePrevious.Year>1880
				|| DateDue != DateDueCalc
				|| RecallInterval!=new Interval(0,0,6,0)
				|| RecallStatus!=0
				|| Note!="")
			{
				return false;
			}
			return true;
		}

		//<summary>Returns a due date based on a combination of IsDisabled, DateDueOverride, and DateDue.</summary>
		/*public DateTime GetDueDate(){
			//if(IsDisabled){
			//	return DateTime.MinValue;
			//}
			//if(DateDueOverride.Year>1880){
			//	return DateDueOverride;
			//}
			return DateTime.Today;;//might be min value
		}*/


	}

	//-------------------------------------Interval Struct-------------------------------------------

	///<summary>Currently used in recall interval. Uses all four values together to establish an interval between two dates, letting the user have total control.  Will later be used for such things as lab cases, appointment scheduling, etc.  Includes a way to combine all four values into one number to be stored in the database (as an int32).  Each value has a max of 255, except years has a max of 127.</summary>
	public struct Interval{
		///<summary></summary>
		public int Years;
		///<summary></summary>
		public int Months;
		///<summary></summary>
		public int Weeks;
		///<summary></summary>
		public int Days;

		///<summary></summary>
		public Interval(int combinedValue){
			BitVector32 bitVector=new BitVector32(combinedValue);
			BitVector32.Section sectionDays=BitVector32.CreateSection(255);
			BitVector32.Section sectionWeeks=BitVector32.CreateSection(255,sectionDays);
			BitVector32.Section sectionMonths=BitVector32.CreateSection(255,sectionWeeks);
			BitVector32.Section sectionYears=BitVector32.CreateSection(255,sectionMonths);
			Days=bitVector[sectionDays];
			Weeks=bitVector[sectionWeeks];
			Months=bitVector[sectionMonths];
			Years=bitVector[sectionYears];
		}

		///<summary></summary>
		public Interval(int days,int weeks,int months,int years){
			Days=days;
			Weeks=weeks;
			Months=months;
			Years=years;
		}

		///<summary>Define the == operator.</summary>
		public static bool operator ==(Interval a,Interval b){
			if(a.Years==b.Years
				&& a.Months==b.Months
				&& a.Weeks==b.Weeks
				&& a.Days==b.Days)
			{
				return true;
			}
			return false;
		}

		///<summary>Define the != operator.</summary>
		public static bool operator !=(Interval a,Interval b){
			if(a.Years==b.Years
				&& a.Months==b.Months
				&& a.Weeks==b.Weeks
				&& a.Days==b.Days)
			{
				return false;
			}
			return true;
		}

		///<summary>Required to override Equals since we defined == and !=</summary>
		public override bool Equals(Object o){
			try{
				return (bool)(this==(Interval)o);
      }
      catch{
				return false;
      }
		}

		///<summary>Required to override since we defined == and !=</summary>
		public override int GetHashCode(){
			return ToInt();
		}

		/// <summary>Specify a date and an interval to return a new date based on adding the interval to the original date.</summary>
		public static DateTime operator +(DateTime date,Interval interval){
			return date
				.AddYears(interval.Years)
				.AddMonths(interval.Months)
				.AddDays(interval.Weeks*7)
				.AddDays(interval.Days);
		}

		///<summary></summary>
		public int ToInt(){
			BitVector32 bitVector=new BitVector32(0);
			BitVector32.Section sectionDays=BitVector32.CreateSection(255);
			BitVector32.Section sectionWeeks=BitVector32.CreateSection(255,sectionDays);
			BitVector32.Section sectionMonths=BitVector32.CreateSection(255,sectionWeeks);
			BitVector32.Section sectionYears=BitVector32.CreateSection(255,sectionMonths);
			bitVector[sectionDays]=Days;
			bitVector[sectionWeeks]=Weeks;
			bitVector[sectionMonths]=Months;
			bitVector[sectionYears]=Years;
			return bitVector.Data;
		}

		///<summary></summary>
		public override string ToString(){
			string retVal="";
			if(Years>0){
				retVal+=Years.ToString()+"y";
			}
			if(Months>0){
				retVal+=Months.ToString()+"m";
			}
			if(Weeks>0){
				retVal+=Weeks.ToString()+"w";
			}
			if(Days>0){
				retVal+=Days.ToString()+"d";
			}
			return retVal;
		}


	}

}









