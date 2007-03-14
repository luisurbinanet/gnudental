using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the computer table in the database.</summary>
	///<remarks>Keeps track of the computers in an office.  There is no interface to maintain this list yet.  The list will eventually become cluttered with the names of old computers that are no longer in service.  The old rows can be safely deleted; that will actually speed up the messaging system.</remarks>
	public struct Computer{//
		///<summary>Primary key.</summary>
		public int ComputerNum;
		///<summary>Name of the computer.</summary>
		public string CompName;
		///<summary>Default printer for each computer</summary>
		public string PrinterName;
	}

	/*=========================================================================================
	=================================== class Computers ==========================================*/

	///<summary></summary>
	public class Computers:DataClass{
		///<summary></summary>
		public static Computer[] List;
		///<summary></summary>
		public static Computer Cur;
		///<summary></summary>
		public static Hashtable HList;

		///<summary></summary>
		public static void Refresh(){
			cmd.CommandText =
				"SELECT * from computer "
				+"WHERE compname = '"+SystemInformation.ComputerName+"'";
			FillTable();
			if(table.Rows.Count==0){
				Cur=new Computer();
				Cur.CompName=SystemInformation.ComputerName;
				InsertCur();
			}
			cmd.CommandText =
				"SELECT * from computer";
			FillTable();
			List=new Computer[table.Rows.Count];
			HList=new Hashtable();
			for(int i=0;i<List.Length;i++){
				List[i].ComputerNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CompName    = PIn.PString(table.Rows[i][1].ToString());
				List[i].PrinterName = PIn.PString(table.Rows[i][2].ToString());
				if(SystemInformation.ComputerName==List[i].CompName){
					Cur=List[i];
				}		
				HList.Add(List[i].ComputerNum,List[i]);
			}
			/*if(!Directory.Exists(Cur.PriDocPath)){
				//MessageBox("Need to determine proper path");

				FormInputBox IBox2=new FormInputBox();
				IBox2.label1.Text="Please enter primary path for Documents";
				IBox2.textBox1.Text=Cur.PriDocPath;
				while(!Directory.Exists(IBox2.textBox1.Text)){
					IBox2.ShowDialog();
					if(IBox2.DialogResult==DialogResult.OK){
						if(!Directory.Exists(IBox2.textBox1.Text)){
							if(MessageBox.Show("Invalid path.  Quit application?","Quit?"
								,MessageBoxButtons.YesNo)==DialogResult.Yes){
								Application.Exit();
								return;
							}
						}
					}
					else{//dialogresult!=OK
						MessageBox.Show("Closing Application");
						Application.Exit();
						return;
					}
				}
				Cur.PriDocPath=IBox2.textBox1.Text;
				UpdateCur();
			}*/
		}
   

		///<summary></summary>
		public static void InsertCur(){//ONLY use this if compname is not already present
			cmd.CommandText = "INSERT INTO computer (compname,"
				+"printername) VALUES("
				+"'"+POut.PString(Cur.CompName)+"', "
				+"'"+POut.PString(Cur.PrinterName)+"')";
			NonQ(false);
		}

		///<summary></summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE computer SET "
				+"compname = '"    +POut.PString(Cur.CompName)+"', "
				+"printername = '" +POut.PString(Cur.PrinterName)+"' "
				+"WHERE computernum = '"+POut.PInt(Cur.ComputerNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			NonQ(false);
		}

		///<summary></summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE FROM computer WHERE computernum = '"+Cur.ComputerNum.ToString()+"'";
			NonQ(false);
		}

	}

	

	



}









