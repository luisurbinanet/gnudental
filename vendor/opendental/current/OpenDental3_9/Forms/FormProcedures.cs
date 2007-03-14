/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormProcedures : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butOK;
		///<summary></summary>
		public FormProcMode Mode;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Panel panel1;
		///<summary></summary>
		public string SelectedADA;	
		private int tableCount;
		private System.Windows.Forms.ListBox listFeeSched;
		private System.Windows.Forms.Label labelFeeSched;
		private int tableLength;
		private OpenDental.UI.Button butNew;
		private OpenDental.TableCodeList[] tb;
		private OpenDental.UI.Button butEdit;
		private OpenDental.UI.Button butTools;
		private System.Windows.Forms.GroupBox groupFeeScheds;
		private bool changed;

		///<summary></summary>
		public FormProcedures(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this);
		}

		///<summary></summary>
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
			this.butClose = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.listFeeSched = new System.Windows.Forms.ListBox();
			this.labelFeeSched = new System.Windows.Forms.Label();
			this.butNew = new OpenDental.UI.Button();
			this.butEdit = new OpenDental.UI.Button();
			this.groupFeeScheds = new System.Windows.Forms.GroupBox();
			this.butTools = new OpenDental.UI.Button();
			this.groupFeeScheds.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.Location = new System.Drawing.Point(849, 608);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(92, 26);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "Close Window";
			this.butClose.Visible = false;
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(866, 648);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Visible = false;
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.Location = new System.Drawing.Point(866, 688);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Visible = false;
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(809, 690);
			this.panel1.TabIndex = 10;
			// 
			// listFeeSched
			// 
			this.listFeeSched.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.listFeeSched.Location = new System.Drawing.Point(813, 52);
			this.listFeeSched.Name = "listFeeSched";
			this.listFeeSched.Size = new System.Drawing.Size(137, 303);
			this.listFeeSched.TabIndex = 11;
			this.listFeeSched.DoubleClick += new System.EventHandler(this.listFeeSched_DoubleClick);
			this.listFeeSched.SelectedIndexChanged += new System.EventHandler(this.listFeeSched_SelectedIndexChanged);
			// 
			// labelFeeSched
			// 
			this.labelFeeSched.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.labelFeeSched.Location = new System.Drawing.Point(813, 34);
			this.labelFeeSched.Name = "labelFeeSched";
			this.labelFeeSched.Size = new System.Drawing.Size(132, 17);
			this.labelFeeSched.TabIndex = 12;
			this.labelFeeSched.Text = "View Fee Sched";
			this.labelFeeSched.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butNew
			// 
			this.butNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butNew.Autosize = true;
			this.butNew.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNew.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNew.Location = new System.Drawing.Point(866, 568);
			this.butNew.Name = "butNew";
			this.butNew.Size = new System.Drawing.Size(75, 25);
			this.butNew.TabIndex = 0;
			this.butNew.Text = "&New Code";
			this.butNew.Visible = false;
			this.butNew.Click += new System.EventHandler(this.butNew_Click);
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEdit.Autosize = true;
			this.butEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdit.Location = new System.Drawing.Point(12, 18);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(81, 25);
			this.butEdit.TabIndex = 13;
			this.butEdit.Text = "Edit Def";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// groupFeeScheds
			// 
			this.groupFeeScheds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupFeeScheds.Controls.Add(this.butTools);
			this.groupFeeScheds.Controls.Add(this.butEdit);
			this.groupFeeScheds.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupFeeScheds.Location = new System.Drawing.Point(814, 360);
			this.groupFeeScheds.Name = "groupFeeScheds";
			this.groupFeeScheds.Size = new System.Drawing.Size(139, 84);
			this.groupFeeScheds.TabIndex = 14;
			this.groupFeeScheds.TabStop = false;
			this.groupFeeScheds.Text = "Fee Schedules";
			// 
			// butTools
			// 
			this.butTools.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTools.Autosize = true;
			this.butTools.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTools.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTools.Location = new System.Drawing.Point(12, 50);
			this.butTools.Name = "butTools";
			this.butTools.Size = new System.Drawing.Size(81, 25);
			this.butTools.TabIndex = 14;
			this.butTools.Text = "Tools";
			this.butTools.Click += new System.EventHandler(this.butTools_Click);
			// 
			// FormProcedures
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(956, 730);
			this.Controls.Add(this.groupFeeScheds);
			this.Controls.Add(this.butNew);
			this.Controls.Add(this.labelFeeSched);
			this.Controls.Add(this.listFeeSched);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butClose);
			this.Name = "FormProcedures";
			this.ShowInTaskbar = false;
			this.Text = "Procedures";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProcedures_Closing);
			this.Load += new System.EventHandler(this.FormProcedures_Load);
			this.Activated += new System.EventHandler(this.FormProcedures_Activated);
			this.groupFeeScheds.ResumeLayout(false);
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
					//Edit mode is the only place where groupFeeScheds is visible. Security will already have been checked.
					butNew.Visible=true;
					butClose.Visible=true;
				break;
				case FormProcMode.Select:
					butCancel.Visible=true;
					groupFeeScheds.Visible=false;
				break;
				case FormProcMode.View:
					butClose.Visible=true;
					groupFeeScheds.Visible=false;
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
			Fee fee;
			ProcedureCodes.GetProcList();
			panel1.Controls.Clear();
			//math to find tableCount
			tableLength=(int)Math.Floor((panel1.Height-18)/OpenDental.TableCodeList.rowHeight);
			tableCount=(int)Math.Ceiling((ProcedureCodes.ProcList.Length+14)/tableLength)+2;//this line needs help
			//tableCount=2;
			//tableStart=new int[tableCount];
			//tableEnd=new int[tableCount];
			//this.dataGrid1.SetDataBinding(ProcedureCodes.tableStat,"");
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
			for(int i=0;i<ProcedureCodes.ProcList.Length;i++){
				if(i==0){
					tb[iTable].Cell[0,iRow]=Defs.GetName(DefCat.ProcCodeCats,ProcedureCodes.ProcList[i].ProcCat);
					tb[iTable].FontBold[0,iRow]=true;
					iRow++;
					if(iRow==tableLength){
						iRow=0;
						iTable++;
					}
				}
				else if(ProcedureCodes.ProcList[i].ProcCat!=ProcedureCodes.ProcList[i-1].ProcCat){
					//if(ProcCodes.ProcList[i].ProcCat==255)
					//	tb[iTable].Cell[0,iRow]="";//read only codes
					//else
						tb[iTable].Cell[0,iRow]=Defs.GetName(DefCat.ProcCodeCats,ProcedureCodes.ProcList[i].ProcCat);
					tb[iTable].FontBold[0,iRow]=true;
					iRow++;
					if(iRow==tableLength){
						iRow=0;
						iTable++;
					}
				}
				//if(ProcCodes.ProcList[i].ProcCat!=255){//don't print read-only codes
				tb[iTable].Cell[0,iRow]=ProcedureCodes.ProcList[i].AbbrDesc;
				tb[iTable].Cell[1,iRow]=ProcedureCodes.ProcList[i].ADACode;
				fee=Fees.GetFeeByOrder(ProcedureCodes.ProcList[i].ADACode,listFeeSched.SelectedIndex);
				if(fee!=null){
					tb[iTable].Cell[2,iRow]=fee.Amount.ToString("F");
				}
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

		private void listFeeSched_DoubleClick(object sender, System.EventArgs e) {
			if(Mode!=FormProcMode.Edit){
				return;
			}
			if(listFeeSched.SelectedIndex==-1){
				return;
			}
			//not functional yet
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			//won't even be visible if no permission
			int selectedSched=Defs.Short[(int)DefCat.FeeSchedNames][listFeeSched.SelectedIndex].DefNum;
			FormDefinitions FormD=new FormDefinitions(DefCat.FeeSchedNames);
			FormD.ShowDialog();
			DataValid.SetInvalid(InvalidTypes.Defs);
			Fees.Refresh();
			ProcedureCodes.Refresh();
			changed=true;
			listFeeSched.Items.Clear();
			for(int i=0;i<Defs.Short[(int)DefCat.FeeSchedNames].Length;i++){
				listFeeSched.Items.Add(Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(Defs.Short[(int)DefCat.FeeSchedNames][i].DefNum==selectedSched){
					listFeeSched.SelectedIndex=i;
				}
			}
			if(listFeeSched.SelectedIndex==-1){
				listFeeSched.SelectedIndex=0;
			}
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Definitions");
			//FillTables();//will be done automatically because of lines above			
		}

		private void butTools_Click(object sender, System.EventArgs e) {
			FormFeeSchedTools FormF=new FormFeeSchedTools(Defs.Short[(int)DefCat.FeeSchedNames][listFeeSched.SelectedIndex].DefNum);
			FormF.ShowDialog();
			if(FormF.DialogResult==DialogResult.Cancel){
				return;
			}
			Fees.Refresh();
			ProcedureCodes.Refresh();
			changed=true;
			FillTables();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Fee Schedule Tools");
		}

		private void tb_CellClicked(object sender, CellEventArgs e){
			for(int n=0;n<tableCount;n++){
				if(OpenDental.TableCodeList.SelectedTable!=n){//for other tables
					//deselect any rows
					if(tb[n].SelectedRow==-1){
						continue;
					}	
					tb[n].ColorRow(tb[n].SelectedRow,Color.White);
					tb[n].SelectedRow=-1;
				}
			}
		}

		private void tb_CellDoubleClicked(object sender, CellEventArgs e){
			SelectedADA=tb[OpenDental.TableCodeList.SelectedTable].Cell[1,e.Row];
			if(SelectedADA=="" || SelectedADA==null){
				return;
			}
			switch(Mode){
				case FormProcMode.Edit:
					if(e.Col==2){//if clicked on a fee
						Fee FeeCur=Fees.GetFeeByOrder(SelectedADA,listFeeSched.SelectedIndex);
						FormFeeEdit FormFE=new FormFeeEdit();
						if(FeeCur==null){
							FeeCur=new Fee();
							FeeCur.ADACode=SelectedADA;
							FeeCur.FeeSched=Defs.Short[(int)DefCat.FeeSchedNames][listFeeSched.SelectedIndex].DefNum;
							FeeCur.Insert();
							FormFE.IsNew=true;
						}
						FormFE.FeeCur=FeeCur;
						FormFE.ShowDialog();
						if(FormFE.DialogResult==DialogResult.OK){
							Fees.Refresh();
							changed=true;
							FillTables();
						}
					}
					else{//not on a fee: Edit code instead
						FormProcCodeEdit FormPCE=new FormProcCodeEdit();
						ProcedureCodes.Cur=ProcedureCodes.GetProcCode(SelectedADA);
						FormPCE.IsNew=false;
						FormPCE.ShowDialog();
						if(FormPCE.DialogResult==DialogResult.OK){
							ProcedureCodes.Refresh();
							changed=true;
							//Fees.Refresh();//fees were already refreshed within procCodeEdit
							FillTables();
						}
					}
					break;
				case FormProcMode.Select:
					DialogResult=DialogResult.OK;
					Close();
					break;
				case FormProcMode.View:
					break;
			}//end switch
			for(int n=0;n<tableCount;n++){
				for(int i=0;i<tb[n].MaxRows;i++){
					if(SelectedADA==tb[n].Cell[1,i]){
						tb[n].SetSelectedRow(i);
					}
				}
			}
		}

		private void FormProcedures_Activated(object sender, System.EventArgs e){
			//nothing	
		}

		private void butNew_Click(object sender, System.EventArgs e) {
			FormProcCodeNew FormPCN=new FormProcCodeNew();
			FormPCN.ShowDialog();
			if(FormPCN.DialogResult!=DialogResult.OK){
				return;
			}
			if(ProcedureCodes.HList.ContainsKey(FormPCN.textNewCode.Text)){
				MessageBox.Show(Lan.g(this,"That code already exists."));
				return;
			}
			FormProcCodeEdit FormProcCodeEdit2=new FormProcCodeEdit();
			//ProcCodes.Cur=ProcCodes.GetProcCode(SelectedADA);
			FormProcCodeEdit2.NewADA=FormPCN.textNewCode.Text;
			FormProcCodeEdit2.IsNew=true;
			FormProcCodeEdit2.ShowDialog();
			//textADACode.Text=FormInputBox2.textBox1.Text;
			if(FormProcCodeEdit2.DialogResult==DialogResult.OK){
				ProcedureCodes.Refresh();
				changed=true;
				FillTables();
			}
			
		}

		private void butOK_Click(object sender, System.EventArgs e){
			DialogResult=DialogResult.OK;
		}

		private void butClose_Click(object sender, System.EventArgs e){
			Close();
		}

		private void butCancel_Click(object sender, System.EventArgs e){
			DialogResult=DialogResult.Cancel;
		}

		private void FormProcedures_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(Mode==FormProcMode.Edit && changed){
				DataValid.SetInvalid(InvalidTypes.ProcCodes | InvalidTypes.Fees);
			}
		}

		


	}//end class

	///<summary></summary>
		public enum FormProcMode{
			///<summary></summary>
			Edit,
			///<summary></summary>
			View,
			///<summary></summary>
			Select
		}
}//end namespace
