using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormDepositEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private System.Windows.Forms.ListBox listPayType;
		private System.Windows.Forms.Label label2;
		private Deposit DepositCur;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butDelete;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBankAccountInfo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textAmount;
		private System.Windows.Forms.GroupBox groupSelect;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.ODGrid gridPat;
		private OpenDental.UI.ODGrid gridIns;
		public bool IsNew;
		private ClaimPayment[] ClaimPayList;
		private OpenDental.ValidDate textDateStart;
		private System.Windows.Forms.Label label5;
		private OpenDental.UI.Button butRefresh;
		private Payment[] PatPayList;
		private bool changed;

		///<summary></summary>
		public FormDepositEdit(Deposit depositCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			DepositCur=depositCur;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDepositEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupSelect = new System.Windows.Forms.GroupBox();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
			this.listPayType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gridPat = new OpenDental.UI.ODGrid();
			this.butDelete = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.label3 = new System.Windows.Forms.Label();
			this.textBankAccountInfo = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textAmount = new System.Windows.Forms.TextBox();
			this.butPrint = new OpenDental.UI.Button();
			this.gridIns = new OpenDental.UI.ODGrid();
			this.textDateStart = new OpenDental.ValidDate();
			this.label5 = new System.Windows.Forms.Label();
			this.butRefresh = new OpenDental.UI.Button();
			this.groupSelect.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.Location = new System.Drawing.Point(805, 631);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(712, 631);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupSelect
			// 
			this.groupSelect.Controls.Add(this.butRefresh);
			this.groupSelect.Controls.Add(this.textDateStart);
			this.groupSelect.Controls.Add(this.label5);
			this.groupSelect.Controls.Add(this.comboClinic);
			this.groupSelect.Controls.Add(this.labelClinic);
			this.groupSelect.Controls.Add(this.listPayType);
			this.groupSelect.Controls.Add(this.label2);
			this.groupSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupSelect.Location = new System.Drawing.Point(602, 276);
			this.groupSelect.Name = "groupSelect";
			this.groupSelect.Size = new System.Drawing.Size(204, 344);
			this.groupSelect.TabIndex = 99;
			this.groupSelect.TabStop = false;
			this.groupSelect.Text = "Show";
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(14, 75);
			this.comboClinic.MaxDropDownItems = 30;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(180, 21);
			this.comboClinic.TabIndex = 94;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(14, 59);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(102, 15);
			this.labelClinic.TabIndex = 93;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listPayType
			// 
			this.listPayType.Location = new System.Drawing.Point(14, 127);
			this.listPayType.Name = "listPayType";
			this.listPayType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listPayType.Size = new System.Drawing.Size(134, 173);
			this.listPayType.TabIndex = 96;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 105);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(171, 18);
			this.label2.TabIndex = 97;
			this.label2.Text = "Patient Payment Types";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// gridPat
			// 
			this.gridPat.Columns.Add(new OpenDental.UI.ODGridColumn("Date", 80, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPat.Columns.Add(new OpenDental.UI.ODGridColumn("Patient", 130, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPat.Columns.Add(new OpenDental.UI.ODGridColumn("Type", 90, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPat.Columns.Add(new OpenDental.UI.ODGridColumn("Check Number", 95, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPat.Columns.Add(new OpenDental.UI.ODGridColumn("Bank-Branch", 80, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPat.Columns.Add(new OpenDental.UI.ODGridColumn("Amount", 90, System.Windows.Forms.HorizontalAlignment.Right));
			this.gridPat.HScrollVisible = false;
			this.gridPat.Location = new System.Drawing.Point(8, 12);
			this.gridPat.Name = "gridPat";
			this.gridPat.ScrollValue = 0;
			this.gridPat.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.gridPat.Size = new System.Drawing.Size(584, 299);
			this.gridPat.TabIndex = 100;
			this.gridPat.Title = "Patient Payments";
			this.gridPat.TranslationName = "TableDepositSlipPat";
			this.gridPat.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPat_CellClick);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(7, 631);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(85, 26);
			this.butDelete.TabIndex = 101;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(602, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 15);
			this.label1.TabIndex = 102;
			this.label1.Text = "Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(602, 25);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(94, 20);
			this.textDate.TabIndex = 103;
			this.textDate.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(602, 95);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(127, 18);
			this.label3.TabIndex = 104;
			this.label3.Text = "Bank Account Info";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textBankAccountInfo
			// 
			this.textBankAccountInfo.Location = new System.Drawing.Point(602, 116);
			this.textBankAccountInfo.Multiline = true;
			this.textBankAccountInfo.Name = "textBankAccountInfo";
			this.textBankAccountInfo.Size = new System.Drawing.Size(289, 75);
			this.textBankAccountInfo.TabIndex = 105;
			this.textBankAccountInfo.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(602, 49);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(127, 18);
			this.label4.TabIndex = 106;
			this.label4.Text = "Amount";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(602, 70);
			this.textAmount.Name = "textAmount";
			this.textAmount.ReadOnly = true;
			this.textAmount.Size = new System.Drawing.Size(94, 20);
			this.textAmount.TabIndex = 107;
			this.textAmount.Text = "";
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.Image = ((System.Drawing.Image)(resources.GetObject("butPrint.Image")));
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(583, 631);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(81, 26);
			this.butPrint.TabIndex = 108;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// gridIns
			// 
			this.gridIns.Columns.Add(new OpenDental.UI.ODGridColumn("Date", 80, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridIns.Columns.Add(new OpenDental.UI.ODGridColumn("Carrier", 220, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridIns.Columns.Add(new OpenDental.UI.ODGridColumn("Check Number", 95, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridIns.Columns.Add(new OpenDental.UI.ODGridColumn("Bank-Branch", 80, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridIns.Columns.Add(new OpenDental.UI.ODGridColumn("Amount", 90, System.Windows.Forms.HorizontalAlignment.Right));
			this.gridIns.HScrollVisible = false;
			this.gridIns.Location = new System.Drawing.Point(8, 319);
			this.gridIns.Name = "gridIns";
			this.gridIns.ScrollValue = 0;
			this.gridIns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.gridIns.Size = new System.Drawing.Size(584, 301);
			this.gridIns.TabIndex = 109;
			this.gridIns.Title = "Insurance Payments";
			this.gridIns.TranslationName = "TableDepositSlipIns";
			this.gridIns.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridIns_CellClick);
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(14, 36);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(94, 20);
			this.textDateStart.TabIndex = 105;
			this.textDateStart.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(14, 19);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(118, 15);
			this.label5.TabIndex = 104;
			this.label5.Text = "Start Date";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butRefresh
			// 
			this.butRefresh.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRefresh.Autosize = true;
			this.butRefresh.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRefresh.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRefresh.Location = new System.Drawing.Point(13, 307);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.Size = new System.Drawing.Size(75, 26);
			this.butRefresh.TabIndex = 106;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// FormDepositEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(897, 667);
			this.Controls.Add(this.gridIns);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBankAccountInfo);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.gridPat);
			this.Controls.Add(this.groupSelect);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDepositEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Deposit Slip";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormDepositEdit_Closing);
			this.Load += new System.EventHandler(this.FormDepositEdit_Load);
			this.groupSelect.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDepositEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				textDateStart.Text=PIn.PDate(Prefs.GetString("DateDepositsStarted")).ToShortDateString();
				if(Prefs.GetBool("EasyNoClinics")){
					comboClinic.Visible=false;
					labelClinic.Visible=false;
				}
				comboClinic.Items.Clear();
				comboClinic.Items.Add(Lan.g(this,"all"));
				comboClinic.SelectedIndex=0;
				for(int i=0;i<Clinics.List.Length;i++){
					comboClinic.Items.Add(Clinics.List[i].Description);
				}
				for(int i=0;i<Defs.Short[(int)DefCat.PaymentTypes].Length;i++){
					listPayType.Items.Add(Defs.Short[(int)DefCat.PaymentTypes][i].ItemName);
					listPayType.SetSelected(i,true);
				}
			}
			else{
				groupSelect.Visible=false;
				gridIns.SelectionMode=SelectionMode.None;
				gridPat.SelectionMode=SelectionMode.None;
			}
			textDate.Text=DepositCur.DateDeposit.ToShortDateString();
			textAmount.Text=DepositCur.Amount.ToString("F");
			textBankAccountInfo.Text=DepositCur.BankAccountInfo;
			FillGrids();
			if(IsNew){
				gridPat.SetSelected(true);
				gridIns.SetSelected(true);
			}
			ComputeAmt();
		}

		///<summary></summary>
		private void FillGrids(){
			if(IsNew){
				DateTime dateStart=PIn.PDate(textDateStart.Text);
				int clinicNum=0;
				if(comboClinic.SelectedIndex!=0){
					clinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
				}
				int[] payTypes=new int[listPayType.SelectedIndices.Count];
				for(int i=0;i<payTypes.Length;i++){
					payTypes[i]=Defs.Short[(int)DefCat.PaymentTypes][listPayType.SelectedIndices[i]].DefNum;
				}
				PatPayList=Payments.GetForDeposit(dateStart,clinicNum,payTypes);
				ClaimPayList=ClaimPayments.GetForDeposit(dateStart,clinicNum);
			}
			else{
				PatPayList=Payments.GetForDeposit(DepositCur.DepositNum);
				ClaimPayList=ClaimPayments.GetForDeposit(DepositCur.DepositNum);
			}
			//Fill Patient Payment Grid---------------------------------------
			ArrayList patNumAL=new ArrayList();
			for(int i=0;i<PatPayList.Length;i++){
				patNumAL.Add(PatPayList[i].PatNum);
			}
			int[] patNums=new int[patNumAL.Count];
			patNumAL.CopyTo(patNums);
			Patient[] pats=Patients.GetMultPats(patNums);
			gridPat.BeginUpdate();
			gridPat.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			for(int i=0;i<PatPayList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(PatPayList[i].PayDate.ToShortDateString());
				row.Cells.Add(Patients.GetOnePat(pats,PatPayList[i].PatNum).GetNameLF());
				row.Cells.Add(Defs.GetName(DefCat.PaymentTypes,PatPayList[i].PayType));
				row.Cells.Add(PatPayList[i].CheckNum);
				row.Cells.Add(PatPayList[i].BankBranch);
				row.Cells.Add(PatPayList[i].PayAmt.ToString("F"));
				gridPat.Rows.Add(row);
			}
			gridPat.EndUpdate();
			//Fill Insurance Payment Grid-------------------------------------
			gridIns.BeginUpdate();
			gridIns.Rows.Clear();
			for(int i=0;i<ClaimPayList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(ClaimPayList[i].CheckDate.ToShortDateString());
				row.Cells.Add(ClaimPayList[i].CarrierName);
				row.Cells.Add(ClaimPayList[i].CheckNum);
				row.Cells.Add(ClaimPayList[i].BankBranch);
				row.Cells.Add(ClaimPayList[i].CheckAmt.ToString("F"));
				gridIns.Rows.Add(row);
			}
			gridIns.EndUpdate();
		}

		///<summary>Usually run after any selected items changed. Recalculates amt based on selected items.</summary>
		private void ComputeAmt(){
			if(!IsNew){
				return;
			}
			double amount=0;
			for(int i=0;i<gridPat.SelectedIndices.Length;i++){
				amount+=PatPayList[gridPat.SelectedIndices[i]].PayAmt;
			}
			for(int i=0;i<gridIns.SelectedIndices.Length;i++){
				amount+=ClaimPayList[gridIns.SelectedIndices[i]].CheckAmt;
			}
			textAmount.Text=amount.ToString("F");
			DepositCur.Amount=amount;
		}

		private void gridPat_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			ComputeAmt();
		}

		private void gridIns_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			ComputeAmt();
		}

		///<summary>Remember that this can only happen if IsNew</summary>
		private void butRefresh_Click(object sender, System.EventArgs e) {
			if(textDateStart.errorProvider1.GetError(textDate)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			FillGrids();
			gridPat.SetSelected(true);
			gridIns.SetSelected(true);
			ComputeAmt();
			if(comboClinic.SelectedIndex==0){
				textBankAccountInfo.Text=Prefs.GetString("PracticeBankNumber");
			}
			else{
				textBankAccountInfo.Text=Clinics.List[comboClinic.SelectedIndex-1].BankNumber;
			}
			if(Prefs.UpdateString("DateDepositsStarted",POut.PDate(PIn.PDate(textDateStart.Text)))){
				changed=true;
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			DepositCur.Delete();
			DialogResult=DialogResult.OK;
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(!SaveToDB()){
				return;
			}
			//refresh the lists because some items may not be highlighted
			PatPayList=Payments.GetForDeposit(DepositCur.DepositNum);
			ClaimPayList=ClaimPayments.GetForDeposit(DepositCur.DepositNum);
			Queries.TableQ=new DataTable();
			for(int i=0;i<5;i++){ //add 5 columns
				Queries.TableQ.Columns.Add(new System.Data.DataColumn());//blank columns
			}
			Queries.CurReport=new ReportOld();
			Queries.CurReport.ColTotal=new double[Queries.TableQ.Columns.Count];
			DataRow row;
			ArrayList patNumAL=new ArrayList();
			for(int i=0;i<PatPayList.Length;i++){
				patNumAL.Add(PatPayList[i].PatNum);
			}
			int[] patNums=new int[patNumAL.Count];
			patNumAL.CopyTo(patNums);
			Patient[] pats=Patients.GetMultPats(patNums);
			for(int i=0;i<PatPayList.Length;i++){
				row=Queries.TableQ.NewRow();
				row[0]=PatPayList[i].PayDate.ToShortDateString();
				row[1]=Patients.GetOnePat(pats,PatPayList[i].PatNum).GetNameLF();
				row[2]=PatPayList[i].CheckNum;
				row[3]=PatPayList[i].BankBranch;
				row[4]=PatPayList[i].PayAmt.ToString("F");
				Queries.TableQ.Rows.Add(row);
				Queries.CurReport.ColTotal[4]+=PatPayList[i].PayAmt;
			}
			for(int i=0;i<ClaimPayList.Length;i++){
				row=Queries.TableQ.NewRow();
				row[0]=ClaimPayList[i].CheckDate.ToShortDateString();
				row[1]=ClaimPayList[i].CarrierName;
				row[2]=ClaimPayList[i].CheckNum;
				row[3]=ClaimPayList[i].BankBranch;
				row[4]=ClaimPayList[i].CheckAmt.ToString("F");
				Queries.TableQ.Rows.Add(row);
				Queries.CurReport.ColTotal[4]+=ClaimPayList[i].CheckAmt;
			}
			//done filling now set up table
			Queries.CurReport.ColWidth=new int[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColPos=new int[Queries.TableQ.Columns.Count+1];
			Queries.CurReport.ColPos[0]=0;
			Queries.CurReport.ColCaption=new string[Queries.TableQ.Columns.Count];
			Queries.CurReport.ColAlign=new HorizontalAlignment[Queries.TableQ.Columns.Count];
			FormQuery FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.ResetGrid();//necessary won't work without
			Queries.CurReport.Title="Deposit Slip";
			Queries.CurReport.SubTitle=new string[2];
			Queries.CurReport.SubTitle[0]=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=DepositCur.DateDeposit.ToShortDateString();
			Queries.CurReport.Summary=new string[1];
			Queries.CurReport.Summary[0]=DepositCur.BankAccountInfo;
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=110;
			Queries.CurReport.ColPos[2]=260;
			Queries.CurReport.ColPos[3]=350;
			Queries.CurReport.ColPos[4]=440;
			Queries.CurReport.ColPos[5]=530;
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Name";
			Queries.CurReport.ColCaption[2]="Check Number";
			Queries.CurReport.ColCaption[3]="Bank-Branch";
			Queries.CurReport.ColCaption[4]="Amount";
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		///<summary>Saves the selected rows to database.</summary>
		private bool SaveToDB(){
			if(textDate.errorProvider1.GetError(textDate)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			DepositCur.DateDeposit=PIn.PDate(textDate.Text);
			//amount already handled.
			DepositCur.BankAccountInfo=PIn.PString(textBankAccountInfo.Text);
			DepositCur.InsertOrUpdate(IsNew);
			if(IsNew){//never allowed to change attached more checks after initial creation of deposit slip
				for(int i=0;i<gridPat.SelectedIndices.Length;i++){
					PatPayList[gridPat.SelectedIndices[i]].DepositNum=DepositCur.DepositNum;
					PatPayList[gridPat.SelectedIndices[i]].InsertOrUpdate(false);
				}
				for(int i=0;i<gridIns.SelectedIndices.Length;i++){
					ClaimPayList[gridIns.SelectedIndices[i]].DepositNum=DepositCur.DepositNum;
					ClaimPayList[gridIns.SelectedIndices[i]].Update();
				}
			}
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!SaveToDB()){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DepositCur.Delete();
			}
			DialogResult=DialogResult.Cancel;
		}

		private void FormDepositEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
		}

	

		

		

		

		

		

		

		


	}
}





















