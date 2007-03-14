using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the scheddefault table in the database.</summary>
	public class SchedDefault{
		///<summary>Primary key.</summary>
		public int SchedDefaultNum;
		///<summary>Sun=0, Mon=1, etc.</summary>
		public int DayOfWeek;
		///<summary>Start time for this timeblock.</summary>
		public DateTime StartTime;
		///<summary>Stop time for this timeblock.</summary>
		public DateTime StopTime;
		///<summary>See the ScheduleType enumeration.  Practice, Provider, or Blockout</summary>
		public ScheduleType SchedType;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Foreign key to definition.DefNum.</summary>
		public int BlockoutType;
		///<summary>Foreign key to operatory.OperatoryNum.  Only used right now for Blockouts.  Will later add practice type.  If 0, then it applies to all ops.</summary>
		public int Op;

		///<summary></summary>
		private void Update(){
			string command= "UPDATE scheddefault SET " 
				+"DayOfWeek = '"    +POut.PInt   (DayOfWeek)+"'"
				+",StartTime = '"   +POut.PDateT (StartTime)+"'"
				+",StopTime = '"    +POut.PDateT (StopTime)+"'"
				+",SchedType = '"   +POut.PInt   ((int)SchedType)+"'"
				+",ProvNum = '"     +POut.PInt   (ProvNum)+"'"
				+",BlockoutType = '"+POut.PInt   (BlockoutType)+"'"
				+",Op = '"          +POut.PInt   (Op)+"'"
				+" WHERE SchedDefaultNum = '" +POut.PInt (SchedDefaultNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		private void Insert(){
			string command= "INSERT INTO scheddefault (DayOfWeek,StartTime,StopTime,SchedType,"
				+"ProvNum,BlockoutType,Op) VALUES("
				+"'"+POut.PInt   (DayOfWeek)+"', "
				+"'"+POut.PDateT (StartTime)+"', "
				+"'"+POut.PDateT (StopTime)+"', "
				+"'"+POut.PInt   ((int)SchedType)+"', "
				+"'"+POut.PInt   (ProvNum)+"', "
				+"'"+POut.PInt   (BlockoutType)+"', "
				+"'"+POut.PInt   (Op)+"')";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			SchedDefaultNum=dcon.InsertID;
		}

		///<summary></summary>
		public void InsertOrUpdate(bool isNew){
			if(StartTime.TimeOfDay > StopTime.TimeOfDay){
				throw new Exception(Lan.g("SchedDefault","Stop time must be later than start time."));
			}
			if(StopTime.TimeOfDay-StartTime.TimeOfDay < new TimeSpan(0,10,0)){//< 5 min
				throw new Exception(Lan.g("SchedDefault","Block is too short."));
			}
			if(Overlaps()){
				throw new Exception(Lan.g("SchedDefault","Cannot overlap another time block."));
			}
			if(isNew){
				Insert();
			}
			else{
				Update();
			}
		}

		///<summary></summary>
		private bool Overlaps(){
			SchedDefaults.Refresh();
			SchedDefault[] ListForType=SchedDefaults.GetForType(SchedType,ProvNum);
			for(int i=0;i<ListForType.Length;i++){
				//if(SchedDefaults.List[i].SchedType!=SchedType){
				//	continue;//skip if different sched type
				//}
				//if(SchedDefaults.List[i].SchedType==ScheduleType.Provider
				//	&& SchedDefaults.List[i].ProvNum!=ProvNum)
				//{
				//	continue;//skip if provider type and this is different provider
				//}
				if(ListForType[i].SchedType==ScheduleType.Blockout){
					//skip if blockout, and ops don't interfere
					if(Op!=0 && ListForType[i].Op!=0){//neither op can be zero, or they will interfere
						if(Op != ListForType[i].Op){
							continue;
						}
					}
				}
				if(SchedDefaultNum!=ListForType[i].SchedDefaultNum
					&& DayOfWeek==ListForType[i].DayOfWeek
					&& StartTime.TimeOfDay >= ListForType[i].StartTime.TimeOfDay
					&& StartTime.TimeOfDay < ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
				if(SchedDefaultNum!=ListForType[i].SchedDefaultNum
					&& DayOfWeek==ListForType[i].DayOfWeek
					&& StopTime.TimeOfDay > ListForType[i].StartTime.TimeOfDay
					&& StopTime.TimeOfDay <= ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
				if(SchedDefaultNum!=ListForType[i].SchedDefaultNum
					&& DayOfWeek==ListForType[i].DayOfWeek
					&& StartTime.TimeOfDay <= ListForType[i].StartTime.TimeOfDay
					&& StopTime.TimeOfDay >= ListForType[i].StopTime.TimeOfDay)
				{
					return true;
				}
			}
			return false;
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE from scheddefault WHERE scheddefaultnum = '"
				+POut.PInt(SchedDefaultNum)+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}


	}

	/*=========================================================================================
		=================================== class SchedDefaults ==========================================*/

	///<summary></summary>
	public class SchedDefaults{
		///<summary>All sched defaults</summary>
		public static SchedDefault[] List;
		//<summary></summary>
		//public static SchedDefault Cur;
	
		///<summary>Gets all scheddefaults and stores them in a static array.</summary>
		public static void Refresh(){
			string command=
				"SELECT * from scheddefault "
				+"ORDER BY SchedType,"//this keeps the painting in the correct order
				+"StartTime";//this helps in the monthly display
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new SchedDefault[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new SchedDefault();
				List[i].SchedDefaultNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].DayOfWeek      = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].StartTime      = PIn.PDateT (table.Rows[i][2].ToString());
				List[i].StopTime       = PIn.PDateT (table.Rows[i][3].ToString());
				List[i].SchedType      = (ScheduleType)PIn.PInt   (table.Rows[i][4].ToString());
				List[i].ProvNum        = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].BlockoutType   = PIn.PInt   (table.Rows[i][6].ToString());
				List[i].Op             = PIn.PInt   (table.Rows[i][7].ToString());
			}
		}

		///<summary>Returns an array of all schedDefaults for a single type (practice, prov, or blockout)</summary>
		public static SchedDefault[] GetForType(ScheduleType schedType,int provNum){
			ArrayList al=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].SchedType==schedType
					&& List[i].ProvNum==provNum)
				{
					al.Add(List[i]);
				}
			}
			SchedDefault[] retVal=new SchedDefault[al.Count];
			for(int i=0;i<retVal.Length;i++){
				retVal[i]=(SchedDefault)al[i];
			}
			return retVal;
		}

	
	}

	

	


}













