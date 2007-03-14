using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the apptviewitem table in the database.</summary>
	public struct ApptViewItem{
		///<summary>Primary key.</summary>
		public int ApptViewItemNum;//
		///<summary>Foreign key to apptview.</summary>
		public int ApptViewNum;
		///<summary>Foreign key to definition.DefNum.</summary>
		public int OpNum;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
	}	

	/*=========================================================================================
	=================================== class ApptViewItems ===========================================*/
	///<summary>Handles database commands related to the apptviewitem table in the database.</summary>
	public class ApptViewItems:DataClass{
		///<summary>Current.  A single row of data.</summary>
		public static ApptViewItem Cur;
		///<summary>A list of all ApptViewItems.</summary>
		public static ApptViewItem[] List;
		///<summary>A list of the ApptViewItems for the current view.</summary>
		public static ApptViewItem[] ForCurView;
		//these two are subsets of provs and ops. You can't include hidden prov or op in this list.
		///<summary>Visible providers in appt module.  List of indices to providers.List(short).</summary>
		///<remarks>Also see VisOps.  This is a subset of the available provs.  You can't include a hidden prov in this list.</remarks>
		public static int[] VisProvs;
		///<summary>Visible ops in appt module.  List of indices to Defs.Short[ops].</summary>
		///<remarks>Also see VisProvs.  This is a subset of the available ops.  You can't include a hidden op in this list.</remarks>
		public static int[] VisOps;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from apptviewitem";
			FillTable();
			List=new ApptViewItem[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ApptViewItemNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ApptViewNum     = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].OpNum           = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ProvNum         = PIn.PInt   (table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO apptviewitem (apptviewnum,opnum,provnum) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.ApptViewNum)+"', "
				+"'"+POut.PInt   (Cur.OpNum)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
			//Cur.ApptViewNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE apptviewitem SET "
				+"apptviewnum='" +POut.PInt   (Cur.ApptViewNum)+"'"
				+",opnum = '"    +POut.PInt   (Cur.OpNum)+"'"
				+",provnum = '"  +POut.PInt   (Cur.ProvNum)+"'"
				+" WHERE apptviewitemnum = '"+POut.PInt(Cur.ApptViewItemNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE from apptviewitem WHERE apptviewitemnum = '"
				+POut.PInt(Cur.ApptViewItemNum)+"'";
			NonQ(false);
		}

		///<summary>Deletes all apptviewitems for the current apptView.</summary>
		public static void DeleteAllForView(){
			cmd.CommandText="DELETE from apptviewitem WHERE apptviewnum = '"
				+POut.PInt(ApptViews.Cur.ApptViewNum)+"'";
			NonQ(false);
		}

		/// <summary>Gets (list)ForCurView, VisOps, and VisProvs.  Works even if no apptview is selected.
		/// </summary>
		public static void GetForCurView(){
			ArrayList tempAL=new ArrayList();
			ArrayList ALprov=new ArrayList();
			ArrayList ALops=new ArrayList();
			if(ApptViews.Cur.ApptViewNum==0){
				//MessageBox.Show("apptcategorynum:"+ApptCategories.Cur.ApptCategoryNum.ToString());
				//make visible ops exactly the same as the short def list (all except hidden)
				for(int i=0;i<Defs.Short[(int)DefCat.Operatories].Length;i++){
					ALops.Add(i);
				}
				//make visible provs exactly the same as the prov list (all except hidden)
				for(int i=0;i<Providers.List.Length;i++){
					ALprov.Add(i);
				}

			}
			else{
				int index;
				for(int i=0;i<List.Length;i++){
					if(List[i].ApptViewNum==ApptViews.Cur.ApptViewNum){
						tempAL.Add(List[i]);
						if(List[i].OpNum>0){//op
							index=Defs.GetOrder(DefCat.Operatories,List[i].OpNum);
							if(index!=-1){
								ALops.Add(index);
							}
						}
						else{//prov
							index=Providers.GetIndex(List[i].ProvNum);
							if(index!=-1){
								ALprov.Add(index);
							}
						}
					}
				}
			}
			ForCurView=new ApptViewItem[tempAL.Count];
			for(int i=0;i<tempAL.Count;i++){
				ForCurView[i]=(ApptViewItem)tempAL[i];
			}
			VisOps=new int[ALops.Count];
			for(int i=0;i<ALops.Count;i++){
				VisOps[i]=(int)ALops[i];
			}
			Array.Sort(VisOps);
			VisProvs=new int[ALprov.Count];
			for(int i=0;i<ALprov.Count;i++){
				VisProvs[i]=(int)ALprov[i];
			}
			Array.Sort(VisProvs);
		}

		///<summary>Returns the index of the provNum within VisProvs.</summary>
		public static int GetIndexProv(int provNum){
			for(int i=0;i<VisProvs.Length;i++){
				if(Providers.List[VisProvs[i]].ProvNum==provNum)
					return i;
			}		
			return -1;
		}

		///<summary>Returns the index of the opNum(defNum) within VisOps.</summary>
		public static int GetIndexOp(int opNum){
			for(int i=0;i<VisOps.Length;i++){
				if(Defs.Short[(int)DefCat.Operatories][VisOps[i]].DefNum==opNum)
					return i;
			}		
			return -1;
		}

		///<summary>Only used in ApptViewItem setup. Must have run GetForCurView first.</summary>
		public static bool OpIsInView(int opNum){
			for(int i=0;i<ForCurView.Length;i++){
				if(ForCurView[i].OpNum==opNum)
					return true;
			}
			return false;
		}

		///<summary>Only used in ApptViewItem setup. Must have run GetForCurView first.</summary>
		public static bool ProvIsInView(int provNum){
			for(int i=0;i<ForCurView.Length;i++){
				if(ForCurView[i].ProvNum==provNum)
					return true;
			}
			return false;
		}


	}

	


}









