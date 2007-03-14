using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the apptviewitem table in the database. Each item specifies ONE of: OpNum, ProvNum, or Element.  The other two will be 0 or "".</summary>
	public struct ApptViewItem{
		///<summary>Primary key.</summary>
		public int ApptViewItemNum;//
		///<summary>Foreign key to apptview.</summary>
		public int ApptViewNum;
		///<summary>Foreign key to operatory.OperatoryNum.</summary>
		public int OpNum;
		///<summary>Foreign key to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Must be one of the hard coded strings picked from the available list.</summary>
		public string ElementDesc;
		///<summary>If this is a row Element, then this is the 0-based order.</summary>
		public int ElementOrder;
		///<summary>If this is an element, then this is the color.</summary>
		public Color ElementColor;

		///<summary>this constructor is just used in GetForCurView when no view selected.</summary>
		public ApptViewItem(string elementDesc,int elementOrder,Color elementColor){
			ApptViewItemNum=0;
			ApptViewNum=0;
			OpNum=0;
			ProvNum=0;
			ElementDesc=elementDesc;
			ElementOrder=elementOrder;
			ElementColor=elementColor;
		}
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
		///<summary>Visible ops in appt module.  List of indices to Operatories.ListShort[ops].</summary>
		///<remarks>Also see VisProvs.  This is a subset of the available ops.  You can't include a hidden op in this list.</remarks>
		public static int[] VisOps;
		///<summary>Subset of ForCurView. Just items for rowElements. If no view is selected, then the elements are filled with default info.</summary>
		public static ApptViewItem[] ApptRows;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from apptviewitem ORDER BY ElementOrder";
			FillTable();
			List=new ApptViewItem[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i].ApptViewItemNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ApptViewNum     = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].OpNum           = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ProvNum         = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ElementDesc     = PIn.PString(table.Rows[i][4].ToString());
				List[i].ElementOrder    = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].ElementColor    = Color.FromArgb(PIn.PInt(table.Rows[i][6].ToString()));
			}
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO apptviewitem (ApptViewNum,OpNum,ProvNum,ElementDesc,"
				+"ElementOrder,ElementColor) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.ApptViewNum)+"', "
				+"'"+POut.PInt   (Cur.OpNum)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PString(Cur.ElementDesc)+"', "
				+"'"+POut.PInt   (Cur.ElementOrder)+"', "
				+"'"+POut.PInt   (Cur.ElementColor.ToArgb())+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
			//Cur.ApptViewNum=InsertID;
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE apptviewitem SET "
				+"ApptViewNum='"    +POut.PInt   (Cur.ApptViewNum)+"'"
				+",OpNum = '"       +POut.PInt   (Cur.OpNum)+"'"
				+",ProvNum = '"     +POut.PInt   (Cur.ProvNum)+"'"
				+",ElementDesc = '" +POut.PString(Cur.ElementDesc)+"'"
				+",ElementOrder = '"+POut.PInt   (Cur.ElementOrder)+"'"
				+",ElementColor = '"+POut.PInt   (Cur.ElementColor.ToArgb())+"'"
				+" WHERE ApptViewItemNum = '"+POut.PInt(Cur.ApptViewItemNum)+"'";
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText="DELETE from apptviewitem WHERE ApptViewItemNum = '"
				+POut.PInt(Cur.ApptViewItemNum)+"'";
			NonQ(false);
		}

		///<summary>Deletes all apptviewitems for the current apptView.</summary>
		public static void DeleteAllForView(){
			cmd.CommandText="DELETE from apptviewitem WHERE ApptViewNum = '"
				+POut.PInt(ApptViews.Cur.ApptViewNum)+"'";
			NonQ(false);
		}

		///<summary>Gets (list)ForCurView, VisOps, VisProvs, and ApptRows.  Also sets TwoRows. Works even if no apptview is selected.</summary>
		public static void GetForCurView(){
			ArrayList tempAL=new ArrayList();
			ArrayList ALprov=new ArrayList();
			ArrayList ALops=new ArrayList();
			ArrayList ALelements=new ArrayList();
			if(ApptViews.Cur.ApptViewNum==0){
				//MessageBox.Show("apptcategorynum:"+ApptCategories.Cur.ApptCategoryNum.ToString());
				//make visible ops exactly the same as the short ops list (all except hidden)
				for(int i=0;i<Operatories.ListShort.Length;i++){
					ALops.Add(i);
				}
				//make visible provs exactly the same as the prov list (all except hidden)
				for(int i=0;i<Providers.List.Length;i++){
					ALprov.Add(i);
				}
				//Hard coded elements showing
				ALelements.Add(new ApptViewItem("PatientName",0,Color.Black));
				ALelements.Add(new ApptViewItem("Lab",1,Color.DarkRed));
				ALelements.Add(new ApptViewItem("Procs",2,Color.Black));
				ALelements.Add(new ApptViewItem("Note",3,Color.Black));
				ContrApptSheet.RowsPerIncr=1;
			}
			else{
				int index;
				for(int i=0;i<List.Length;i++){
					if(List[i].ApptViewNum==ApptViews.Cur.ApptViewNum){
						tempAL.Add(List[i]);
						if(List[i].OpNum>0){//op
							index=Operatories.GetOrder(List[i].OpNum);
							if(index!=-1){
								ALops.Add(index);
							}
						}
						else if(List[i].ProvNum>0){//prov
							index=Providers.GetIndex(List[i].ProvNum);
							if(index!=-1){
								ALprov.Add(index);
							}
						}
						else{//element
							ALelements.Add(List[i]);
						}
					}
				}
				ContrApptSheet.RowsPerIncr=ApptViews.Cur.RowsPerIncr;
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
			ApptRows=new ApptViewItem[ALelements.Count];
			for(int i=0;i<ALelements.Count;i++){
				ApptRows[i]=(ApptViewItem)ALelements[i];
			}
		}

		///<summary>Returns the index of the provNum within VisProvs.</summary>
		public static int GetIndexProv(int provNum){
			for(int i=0;i<VisProvs.Length;i++){
				if(Providers.List[VisProvs[i]].ProvNum==provNum)
					return i;
			}		
			return -1;
		}

		///<summary>Returns the index of the opNum within VisOps.  Returns -1 if not in visOps.</summary>
		public static int GetIndexOp(int opNum){
			for(int i=0;i<VisOps.Length;i++){
				if(Operatories.ListShort[VisOps[i]].OperatoryNum==opNum)
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









