/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormProcedures : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Button butOK;
		public FormProcMode Mode;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Panel panel1;//"select" or "view"
		public string SelectedADA;	
		private int tableCount;
		private System.Windows.Forms.ListBox listFeeSched;
		private System.Windows.Forms.Label labelFeeSched;
		private int tableLength;
		private System.Windows.Forms.Button butNew;
		private OpenDental.TableCodeList[] tb;
		private System.Windows.Forms.DataGrid dataGrid1;

		public FormProcedures(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.C(this, new System.Windows.Forms.Control[] {
				butNew,
				labelFeeSched
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butClose,
			});
		}

		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.butClose = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.listFeeSched = new System.Windows.Forms.ListBox();
			this.labelFeeSched = new System.Windows.Forms.Label();
			this.butNew = new System.Windows.Forms.Button();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.butClose.Location = new System.Drawing.Point(848, 608);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(92, 23);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "Close Window";
			this.butClose.Visible = false;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butOK
			// 
			this.butOK.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.butOK.Location = new System.Drawing.Point(866, 648);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 2;
			this.butOK.Text = "OK";
			this.butOK.Visible = false;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.butCancel.Location = new System.Drawing.Point(866, 688);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "Cancel";
			this.butCancel.Visible = false;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(820, 690);
			this.panel1.TabIndex = 10;
			// 
			// listFeeSched
			// 
			this.listFeeSched.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.listFeeSched.Location = new System.Drawing.Point(838, 52);
			this.listFeeSched.Name = "listFeeSched";
			this.listFeeSched.Size = new System.Drawing.Size(96, 303);
			this.listFeeSched.TabIndex = 11;
			this.listFeeSched.SelectedIndexChanged += new System.EventHandler(this.listFeeSched_SelectedIndexChanged);
			// 
			// labelFeeSched
			// 
			this.labelFeeSched.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.labelFeeSched.Location = new System.Drawing.Point(840, 30);
			this.labelFeeSched.Name = "labelFeeSched";
			this.labelFeeSched.TabIndex = 12;
			this.labelFeeSched.Text = "View Fee Sched:";
			// 
			// butNew
			// 
			this.butNew.Location = new System.Drawing.Point(866, 568);
			this.butNew.Name = "butNew";
			this.butNew.TabIndex = 0;
			this.butNew.Text = "New Code";
			this.butNew.Visible = false;
			this.butNew.Click += new System.EventHandler(this.butNew_Click);
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(58, 56);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(650, 620);
			this.dataGrid1.TabIndex = 14;
			this.dataGrid1.Visible = false;
			// 
			// FormProcedures
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(956, 730);
			this.ControlBox = false;
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.butNew);
			this.Controls.Add(this.labelFeeSched);
			this.Controls.Add(this.listFeeSched);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butClose);
			this.MinimizeBox = false;
			this.Name = "FormProcedures";
			this.ShowInTaskbar = false;
			this.Text = Lan.g(this,"Procedures");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormProcedures_Load);
			this.Activated += new System.EventHandler(this.FormProcedures_Activated);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProcedures_Load(object sender, System.EventArgs e) {
			panel1.Height=this.ClientSize.Height;
			panel1.Width=this.ClientSize.Width-150;
			butNew.Location=new Point(ClientSize.Width-100,ClientSize.Height-196);
			butClose.Location=new Point(ClientSize.Width-100,ClientSize.Height-156);
			butOK.Location=new Point(ClientSize.Width-100,ClientSize.Height-116);
			butCancel.Location=new Point(ClientSize.Width-100,ClientSize.Height-76);
			labelFeeSched.Location=new Point(ClientSize.Width-125,30);
			listFeeSched.Location=new Point(ClientSize.Width-125,50);
			switch(Mode){
				case FormProcMode.Edit:
					butNew.Visible=true;
					butClose.Visible=true;
				break;
				case FormProcMode.Select:
					butCancel.Visible=true;
				break;
				case FormProcMode.View:
					butClose.Visible=true;
				break;
			}
			listFeeSched.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				this.listFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
			}
			listFeeSched.SelectedIndex=0;
			//FillTables();//will be done automatically because of line above			
		}

		private void FillTables(){
			ProcCodes.GetProcList();
			panel1.Controls.Clear();
			//math to find tableCount
			tableLength=(int)Math.Floor((panel1.Height-18)/OpenDental.TableCodeList.rowHeight);
			tableCount=(int)Math.Ceiling((ProcCodes.ProcList.Length+14)/tableLength)+2;//this line needs help
			//tableCount=2;
			//tableStart=new int[tableCount];
			//tableEnd=new int[tableCount];
			this.dataGrid1.SetDataBinding(ProcCodes.tableStat,"");
			tb=new OpenDental.TableCodeList[tableCount];
			for(int n=0;n<tableCount;n++){
				tb[n]=new OpenDental.TableCodeList();
				//if(n==0)tableStart[n]=0;
				//else tableStart[n]=tableEnd[n-1]+1;
				//tableEnd[n]=tableStart[n]+tableLength;//but check for end of list
				//MessageBox.Show(tableStart[n].ToString()+" "+tableEnd[n].ToString());
				tb[n].Location=new Point(tb[0].Width*n,0);
				tb[n].Height=panel1.Height-18;
				tb[n].ResetRows(tableLength);
				tb[n].SetGridColor(Color.LightGray);
				panel1.Controls.Add(tb[n]);
				tb[n].MySelectedTable=n;
				tb[n].SelectionMode=SelectionMode.One;
				tb[n].CellClicked += new OpenDental.ContrTable.CellEventHandler(tb_CellClicked);
				tb[n].CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tb_CellDoubleClicked);
			}
			int iTable=0;
			int iRow=0;
			for(int i=0;i<ProcCodes.ProcList.Length;i++){
				if(i==0){
					tb[iTable].Cell[0,iRow]=Defs.GetName(DefCat.ProcCodeCats,ProcCodes.ProcList[i].ProcCat);
					tb[iTable].FontBold[0,iRow]=true;
					iRow++;
					if(iRow==tableLength){
						iRow=0;
						iTable++;
					}
				}
				else if(ProcCodes.ProcList[i].ProcCat!=ProcCodes.ProcList[i-1].ProcCat){
					//if(ProcCodes.ProcList[i].ProcCat==255)
					//	tb[iTable].Cell[0,iRow]="";//read only codes
					//else
						tb[iTable].Cell[0,iRow]=Defs.GetName(DefCat.ProcCodeCats,ProcCodes.ProcList[i].ProcCat);
					tb[iTable].FontBold[0,iRow]=true;
					iRow++;
					if(iRow==tableLength){
						iRow=0;
						iTable++;
					}
				}
				//if(ProcCodes.ProcList[i].ProcCat!=255){//don't print read-only codes
					tb[iTable].Cell[0,iRow]=ProcCodes.ProcList[i].AbbrDesc;
					tb[iTable].Cell[1,iRow]=Fees.GetFeeByOrder
						(ProcCodes.ProcList[i].ADACode,listFeeSched.SelectedIndex).Amount.ToString("F");
					tb[iTable].Cell[2,iRow]=ProcCodes.ProcList[i].ADACode;
				//}
				iRow++;
				if(iRow==tableLength){
					iRow=0;
					iTable++;
				}
			}
			for(int n=0;n<tableCount;n++){
				tb[n].LayoutTables();
			}
		}

		private void listFeeSched_SelectedIndexChanged(object sender, System.EventArgs e) {
			FillTables();
		}

		private void tb_CellClicked(object sender, CellEventArgs e){
			for(int n=0;n<tableCount;n++){
				if(OpenDental.TableCodeList.SelectedTable!=n){
					if(tb[n].SelectedRow==-1){
						continue;
					}	
					tb[n].ColorRow(tb[n].SelectedRow,Color.White);
					tb[n].SelectedRow=-1;
				}
			}
		}

		private void tb_CellDoubleClicked(object sender, CellEventArgs e){
			SelectedADA=tb[OpenDental.TableCodeList.SelectedTable].Cell[2,e.Row];
			if(SelectedADA=="" || SelectedADA==null){
				return;
			}
			switch(Mode){
				case FormProcMode.Edit:
					FormProcCodeEdit FormProcCodeEdit2=new FormProcCodeEdit();
					ProcCodes.Cur=ProcCodes.GetProcCode(SelectedADA);
					FormProcCodeEdit2.IsNew=false;
					FormProcCodeEdit2.ShowDialog();
					if(FormProcCodeEdit2.DialogResult==DialogResult.OK){
						//DataValid.IType=InvalidType.LocalData;
						//DataValid DataValid2=new DataValid();
						//DataValid2.SetInvalid();
						ProcCodes.Refresh();
						//Fees.Refresh();//fees were already refreshed within procCodeEdit
						FillTables();
						tb[OpenDental.TableCodeList.SelectedTable].SelectedRow=e.Row;
						tb[OpenDental.TableCodeList.SelectedTable].ColorRow(e.Row,SystemColors.Highlight);
					}
				break;
				case FormProcMode.Select:
					DialogResult=DialogResult.OK;
					Close();
				break;
				case FormProcMode.View:
				break;
			}//end switch
		}

		private void FormProcedures_Activated(object sender, System.EventArgs e){
			//nothing	
		}

		private void butOK_Click(object sender, System.EventArgs e){
			DialogResult=DialogResult.OK;
			Close();
		}

		private void butClose_Click(object sender, System.EventArgs e){
			if(Mode==FormProcMode.Edit){
				DataValid.IType=InvalidType.LocalData;
				DataValid DataValid2=new DataValid();
				DataValid2.SetInvalid();
			}
			DialogResult=DialogResult.OK;
			Close();
		}

		private void butCancel_Click(object sender, System.EventArgs e){
			DialogResult=DialogResult.Cancel;
			Close();
		}

		private void butNew_Click(object sender, System.EventArgs e) {
			FormInputBox FormInputBox2=new FormInputBox();
			FormInputBox2.label1.Text="Please enter new ADA Code:";
			FormInputBox2.textBox1.MaxLength=15;
			FormInputBox2.textBox1.Select();
			FormInputBox2.ShowDialog();
			if(FormInputBox2.DialogResult!=DialogResult.OK){
				return;
			}
			if(ProcCodes.HList.ContainsKey(FormInputBox2.textBox1.Text)){
				MessageBox.Show(Lan.g(this,"That code already exists."));
				return;
			}
			FormProcCodeEdit FormProcCodeEdit2=new FormProcCodeEdit();
			//ProcCodes.Cur=ProcCodes.GetProcCode(SelectedADA);
			FormProcCodeEdit2.NewADA=FormInputBox2.textBox1.Text;
			FormProcCodeEdit2.IsNew=true;
			FormProcCodeEdit2.ShowDialog();
			//textADACode.Text=FormInputBox2.textBox1.Text;
			if(FormProcCodeEdit2.DialogResult==DialogResult.OK){
				ProcCodes.Refresh();
				FillTables();
			}
			
		}

		

		

		
		

		







	}//end class
}//end namespace
