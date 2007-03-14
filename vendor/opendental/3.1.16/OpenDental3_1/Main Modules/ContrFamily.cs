/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{

	///<summary></summary>
	public class ContrFamily : System.Windows.Forms.UserControl{
		private System.Windows.Forms.ImageList imageListToolBar;
		private System.ComponentModel.IContainer components;
		private OpenDental.TableInsPlans tbPlans;
		private OpenDental.TablePatient tbPatient;
		private OpenDental.TableFamily tbFamily;
		private System.Windows.Forms.TextBox textAddrNotes;
		private OpenDental.TableCoverage tbCoverage;
		private System.Windows.Forms.TextBox textPriPlanNote;
		private System.Windows.Forms.TextBox textSecPlanNote;
		private System.Windows.Forms.Panel panelFamily;
		private OpenDental.TablePercent tbPercent1;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.PictureBox picturePatient;
		private OpenDental.UI.Button butPatEdit;
		private OpenDental.UI.Button butEditPriCov;
		private OpenDental.UI.Button butEditPriPlan;
		private OpenDental.UI.Button butEditSecCov;
		private OpenDental.UI.Button butEditSecPlan;
		private OpenDental.UI.Button butAddPt;
		private OpenDental.UI.Button butDeletePt;
		private OpenDental.UI.Button butSetGuar;
		private OpenDental.UI.Button butMovePat;
		private System.Windows.Forms.CheckBox checkPriPending;
		private System.Windows.Forms.CheckBox checkSecPending;
		private System.Windows.Forms.ContextMenu menuPatient;
		private OpenDental.TablePercent tbPercent2;
		///<summary>All recalls for this entire family.</summary>
		private Recall[] RecallList;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		private Patient PatCur;
		private Family FamCur;
		private InsPlan[] PlanList;
		//private int CurPatNum;

		///<summary></summary>
		public ContrFamily(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			tbPlans.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPlans_CellClicked);
			tbPlans.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPlans_CellDoubleClicked);
			tbFamily.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbFamily_CellClicked);
			tbPatient.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPatient_CellDoubleClicked);
			tbCoverage.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbCoverage_CellDoubleClicked);
			tbPercent1.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent1_CellDoubleClicked);
			tbPercent2.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent2_CellDoubleClicked);
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

		#region Component Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrFamily));
			this.imageListToolBar = new System.Windows.Forms.ImageList(this.components);
			this.tbPlans = new OpenDental.TableInsPlans();
			this.tbPatient = new OpenDental.TablePatient();
			this.textAddrNotes = new System.Windows.Forms.TextBox();
			this.tbFamily = new OpenDental.TableFamily();
			this.tbCoverage = new OpenDental.TableCoverage();
			this.textPriPlanNote = new System.Windows.Forms.TextBox();
			this.textSecPlanNote = new System.Windows.Forms.TextBox();
			this.panelFamily = new System.Windows.Forms.Panel();
			this.butMovePat = new OpenDental.UI.Button();
			this.butSetGuar = new OpenDental.UI.Button();
			this.butDeletePt = new OpenDental.UI.Button();
			this.butAddPt = new OpenDental.UI.Button();
			this.tbPercent1 = new OpenDental.TablePercent();
			this.tbPercent2 = new OpenDental.TablePercent();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.picturePatient = new System.Windows.Forms.PictureBox();
			this.butPatEdit = new OpenDental.UI.Button();
			this.butEditPriCov = new OpenDental.UI.Button();
			this.butEditPriPlan = new OpenDental.UI.Button();
			this.butEditSecCov = new OpenDental.UI.Button();
			this.butEditSecPlan = new OpenDental.UI.Button();
			this.checkPriPending = new System.Windows.Forms.CheckBox();
			this.checkSecPending = new System.Windows.Forms.CheckBox();
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.panelFamily.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageListToolBar
			// 
			this.imageListToolBar.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListToolBar.ImageStream")));
			this.imageListToolBar.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tbPlans
			// 
			this.tbPlans.BackColor = System.Drawing.SystemColors.Window;
			this.tbPlans.Location = new System.Drawing.Point(0, 602);
			this.tbPlans.Name = "tbPlans";
			this.tbPlans.ScrollValue = 1;
			this.tbPlans.SelectedIndices = new int[0];
			this.tbPlans.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbPlans.Size = new System.Drawing.Size(459, 100);
			this.tbPlans.TabIndex = 1;
			// 
			// tbPatient
			// 
			this.tbPatient.BackColor = System.Drawing.SystemColors.Window;
			this.tbPatient.Location = new System.Drawing.Point(0, 32);
			this.tbPatient.Name = "tbPatient";
			this.tbPatient.ScrollValue = 150;
			this.tbPatient.SelectedIndices = new int[0];
			this.tbPatient.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPatient.Size = new System.Drawing.Size(252, 417);
			this.tbPatient.TabIndex = 2;
			// 
			// textAddrNotes
			// 
			this.textAddrNotes.BackColor = System.Drawing.Color.White;
			this.textAddrNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textAddrNotes.ForeColor = System.Drawing.Color.Red;
			this.textAddrNotes.Location = new System.Drawing.Point(2, 388);
			this.textAddrNotes.Multiline = true;
			this.textAddrNotes.Name = "textAddrNotes";
			this.textAddrNotes.ReadOnly = true;
			this.textAddrNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textAddrNotes.Size = new System.Drawing.Size(248, 59);
			this.textAddrNotes.TabIndex = 3;
			this.textAddrNotes.Text = "";
			this.textAddrNotes.DoubleClick += new System.EventHandler(this.textAddrNotes_DoubleClick);
			// 
			// tbFamily
			// 
			this.tbFamily.BackColor = System.Drawing.SystemColors.Window;
			this.tbFamily.Location = new System.Drawing.Point(0, 26);
			this.tbFamily.Name = "tbFamily";
			this.tbFamily.ScrollValue = 1;
			this.tbFamily.SelectedIndices = new int[0];
			this.tbFamily.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbFamily.Size = new System.Drawing.Size(489, 99);
			this.tbFamily.TabIndex = 7;
			// 
			// tbCoverage
			// 
			this.tbCoverage.BackColor = System.Drawing.SystemColors.Window;
			this.tbCoverage.Location = new System.Drawing.Point(254, 32);
			this.tbCoverage.Name = "tbCoverage";
			this.tbCoverage.ScrollValue = 150;
			this.tbCoverage.SelectedIndices = new int[0];
			this.tbCoverage.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbCoverage.Size = new System.Drawing.Size(542, 424);
			this.tbCoverage.TabIndex = 8;
			// 
			// textPriPlanNote
			// 
			this.textPriPlanNote.BackColor = System.Drawing.Color.White;
			this.textPriPlanNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textPriPlanNote.ForeColor = System.Drawing.Color.Red;
			this.textPriPlanNote.Location = new System.Drawing.Point(256, 325);
			this.textPriPlanNote.Multiline = true;
			this.textPriPlanNote.Name = "textPriPlanNote";
			this.textPriPlanNote.ReadOnly = true;
			this.textPriPlanNote.Size = new System.Drawing.Size(270, 129);
			this.textPriPlanNote.TabIndex = 9;
			this.textPriPlanNote.Text = "";
			this.textPriPlanNote.DoubleClick += new System.EventHandler(this.textPriPlanNote_DoubleClick);
			// 
			// textSecPlanNote
			// 
			this.textSecPlanNote.BackColor = System.Drawing.Color.White;
			this.textSecPlanNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textSecPlanNote.ForeColor = System.Drawing.Color.Red;
			this.textSecPlanNote.Location = new System.Drawing.Point(526, 325);
			this.textSecPlanNote.Multiline = true;
			this.textSecPlanNote.Name = "textSecPlanNote";
			this.textSecPlanNote.ReadOnly = true;
			this.textSecPlanNote.Size = new System.Drawing.Size(267, 129);
			this.textSecPlanNote.TabIndex = 10;
			this.textSecPlanNote.Text = "";
			this.textSecPlanNote.DoubleClick += new System.EventHandler(this.textSecPlanNote_DoubleClick);
			// 
			// panelFamily
			// 
			this.panelFamily.Controls.Add(this.butMovePat);
			this.panelFamily.Controls.Add(this.butSetGuar);
			this.panelFamily.Controls.Add(this.butDeletePt);
			this.panelFamily.Controls.Add(this.butAddPt);
			this.panelFamily.Controls.Add(this.tbFamily);
			this.panelFamily.Location = new System.Drawing.Point(0, 466);
			this.panelFamily.Name = "panelFamily";
			this.panelFamily.Size = new System.Drawing.Size(490, 129);
			this.panelFamily.TabIndex = 15;
			// 
			// butMovePat
			// 
			this.butMovePat.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butMovePat.Autosize = true;
			this.butMovePat.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMovePat.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMovePat.Image = ((System.Drawing.Image)(resources.GetObject("butMovePat.Image")));
			this.butMovePat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butMovePat.Location = new System.Drawing.Point(309, 0);
			this.butMovePat.Name = "butMovePat";
			this.butMovePat.Size = new System.Drawing.Size(100, 26);
			this.butMovePat.TabIndex = 19;
			this.butMovePat.Text = "Move";
			this.butMovePat.Click += new System.EventHandler(this.butMovePat_Click);
			// 
			// butSetGuar
			// 
			this.butSetGuar.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSetGuar.Autosize = true;
			this.butSetGuar.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetGuar.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetGuar.Image = ((System.Drawing.Image)(resources.GetObject("butSetGuar.Image")));
			this.butSetGuar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSetGuar.Location = new System.Drawing.Point(206, 0);
			this.butSetGuar.Name = "butSetGuar";
			this.butSetGuar.Size = new System.Drawing.Size(100, 26);
			this.butSetGuar.TabIndex = 18;
			this.butSetGuar.Text = "Guarantor";
			this.butSetGuar.Click += new System.EventHandler(this.butSetGuar_Click);
			// 
			// butDeletePt
			// 
			this.butDeletePt.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDeletePt.Autosize = true;
			this.butDeletePt.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDeletePt.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDeletePt.Image = ((System.Drawing.Image)(resources.GetObject("butDeletePt.Image")));
			this.butDeletePt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeletePt.Location = new System.Drawing.Point(103, 0);
			this.butDeletePt.Name = "butDeletePt";
			this.butDeletePt.Size = new System.Drawing.Size(100, 26);
			this.butDeletePt.TabIndex = 17;
			this.butDeletePt.Text = "Delete";
			this.butDeletePt.Click += new System.EventHandler(this.butDeletePt_Click);
			// 
			// butAddPt
			// 
			this.butAddPt.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddPt.Autosize = true;
			this.butAddPt.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddPt.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddPt.Image = ((System.Drawing.Image)(resources.GetObject("butAddPt.Image")));
			this.butAddPt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddPt.Location = new System.Drawing.Point(0, 0);
			this.butAddPt.Name = "butAddPt";
			this.butAddPt.Size = new System.Drawing.Size(100, 26);
			this.butAddPt.TabIndex = 16;
			this.butAddPt.Text = "Add";
			this.butAddPt.Click += new System.EventHandler(this.butAddPt_Click);
			// 
			// tbPercent1
			// 
			this.tbPercent1.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercent1.Location = new System.Drawing.Point(254, 185);
			this.tbPercent1.Name = "tbPercent1";
			this.tbPercent1.ScrollValue = 1;
			this.tbPercent1.SelectedIndices = new int[0];
			this.tbPercent1.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercent1.Size = new System.Drawing.Size(272, 86);
			this.tbPercent1.TabIndex = 16;
			// 
			// tbPercent2
			// 
			this.tbPercent2.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercent2.Location = new System.Drawing.Point(525, 185);
			this.tbPercent2.Name = "tbPercent2";
			this.tbPercent2.ScrollValue = 1;
			this.tbPercent2.SelectedIndices = new int[0];
			this.tbPercent2.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercent2.Size = new System.Drawing.Size(269, 86);
			this.tbPercent2.TabIndex = 17;
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListToolBar;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939, 29);
			this.ToolBarMain.TabIndex = 19;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// picturePatient
			// 
			this.picturePatient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picturePatient.Location = new System.Drawing.Point(651, 459);
			this.picturePatient.Name = "picturePatient";
			this.picturePatient.Size = new System.Drawing.Size(145, 149);
			this.picturePatient.TabIndex = 20;
			this.picturePatient.TabStop = false;
			this.picturePatient.Visible = false;
			// 
			// butPatEdit
			// 
			this.butPatEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPatEdit.Autosize = true;
			this.butPatEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPatEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPatEdit.Location = new System.Drawing.Point(180, 31);
			this.butPatEdit.Name = "butPatEdit";
			this.butPatEdit.Size = new System.Drawing.Size(70, 23);
			this.butPatEdit.TabIndex = 21;
			this.butPatEdit.Text = "Edit";
			this.butPatEdit.Click += new System.EventHandler(this.butPatEdit_Click);
			// 
			// butEditPriCov
			// 
			this.butEditPriCov.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditPriCov.Autosize = true;
			this.butEditPriCov.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditPriCov.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditPriCov.Location = new System.Drawing.Point(456, 31);
			this.butEditPriCov.Name = "butEditPriCov";
			this.butEditPriCov.Size = new System.Drawing.Size(70, 23);
			this.butEditPriCov.TabIndex = 22;
			this.butEditPriCov.Text = "Edit";
			this.butEditPriCov.Click += new System.EventHandler(this.butEditPriCov_Click);
			// 
			// butEditPriPlan
			// 
			this.butEditPriPlan.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditPriPlan.Autosize = true;
			this.butEditPriPlan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditPriPlan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditPriPlan.Location = new System.Drawing.Point(456, 93);
			this.butEditPriPlan.Name = "butEditPriPlan";
			this.butEditPriPlan.Size = new System.Drawing.Size(70, 23);
			this.butEditPriPlan.TabIndex = 23;
			this.butEditPriPlan.Text = "Edit";
			this.butEditPriPlan.Click += new System.EventHandler(this.butEditPriPlan_Click);
			// 
			// butEditSecCov
			// 
			this.butEditSecCov.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditSecCov.Autosize = true;
			this.butEditSecCov.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditSecCov.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditSecCov.Location = new System.Drawing.Point(724, 31);
			this.butEditSecCov.Name = "butEditSecCov";
			this.butEditSecCov.Size = new System.Drawing.Size(70, 23);
			this.butEditSecCov.TabIndex = 24;
			this.butEditSecCov.Text = "Edit";
			this.butEditSecCov.Click += new System.EventHandler(this.butEditSecCov_Click);
			// 
			// butEditSecPlan
			// 
			this.butEditSecPlan.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditSecPlan.Autosize = true;
			this.butEditSecPlan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditSecPlan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditSecPlan.Location = new System.Drawing.Point(724, 93);
			this.butEditSecPlan.Name = "butEditSecPlan";
			this.butEditSecPlan.Size = new System.Drawing.Size(70, 23);
			this.butEditSecPlan.TabIndex = 25;
			this.butEditSecPlan.Text = "Edit";
			this.butEditSecPlan.Click += new System.EventHandler(this.butEditSecPlan_Click);
			// 
			// checkPriPending
			// 
			this.checkPriPending.BackColor = System.Drawing.SystemColors.Window;
			this.checkPriPending.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkPriPending.Location = new System.Drawing.Point(377, 81);
			this.checkPriPending.Name = "checkPriPending";
			this.checkPriPending.Size = new System.Drawing.Size(13, 13);
			this.checkPriPending.TabIndex = 26;
			this.checkPriPending.Click += new System.EventHandler(this.checkPriPending_Click);
			// 
			// checkSecPending
			// 
			this.checkSecPending.BackColor = System.Drawing.SystemColors.Window;
			this.checkSecPending.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSecPending.Location = new System.Drawing.Point(647, 81);
			this.checkSecPending.Name = "checkSecPending";
			this.checkSecPending.Size = new System.Drawing.Size(13, 13);
			this.checkSecPending.TabIndex = 27;
			this.checkSecPending.Click += new System.EventHandler(this.checkSecPending_Click);
			// 
			// ContrFamily
			// 
			this.Controls.Add(this.checkSecPending);
			this.Controls.Add(this.checkPriPending);
			this.Controls.Add(this.butEditSecPlan);
			this.Controls.Add(this.butEditSecCov);
			this.Controls.Add(this.butEditPriPlan);
			this.Controls.Add(this.butEditPriCov);
			this.Controls.Add(this.butPatEdit);
			this.Controls.Add(this.picturePatient);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.tbPercent1);
			this.Controls.Add(this.tbPercent2);
			this.Controls.Add(this.panelFamily);
			this.Controls.Add(this.textSecPlanNote);
			this.Controls.Add(this.textPriPlanNote);
			this.Controls.Add(this.tbCoverage);
			this.Controls.Add(this.textAddrNotes);
			this.Controls.Add(this.tbPatient);
			this.Controls.Add(this.tbPlans);
			this.Name = "ContrFamily";
			this.Size = new System.Drawing.Size(939, 708);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrFamily_Layout);
			this.panelFamily.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void ModuleSelected(int patNum){
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			FamCur=null;
			PlanList=null;
			CovPats.List=null;
			//RefAttaches.List=null;
			//RefAttaches.HList=null;
		}

		private void RefreshModuleData(int patNum){
			if(patNum==0){
				PatCur=null;
				FamCur=null;
				return;
			}
			FamCur=Patients.GetFamily(patNum);
			PatCur=FamCur.GetPatient(patNum);
			PlanList=InsPlans.Refresh(FamCur);
			CovPats.Refresh(PatCur,PlanList);
			//RefAttaches.Refresh();
			RecallList=Recalls.GetList(FamCur.List);
		}

		private void RefreshModuleScreen(){
			if(PatCur!=null){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "
					+PatCur.GetNameLF();
				tbPatient.Enabled=true;
				tbCoverage.Enabled=true;
				ToolBarMain.Buttons["Recall"].Enabled=true;
				ToolBarMain.Invalidate();
				panelFamily.Enabled=true;              
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				tbPatient.Enabled=false;
				tbCoverage.Enabled=false;
				ToolBarMain.Buttons["Recall"].Enabled=false;
				ToolBarMain.Invalidate();
				panelFamily.Enabled=false;
				//Patients.Cur=new Patient();
			}
			FillPatientButton();
			FillPatientData();
			FillFamilyData();
			FillPlanData();
			FillCoverageData();
		} 

		private void FillPatientButton(){
			Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCur,FamCur);
		}

		private void menuPatient_Click(object sender,System.EventArgs e) {
			int newPatNum=Patients.ButtonSelect(menuPatient,sender,FamCur);
			OnPatientSelected(newPatNum);
			ModuleSelected(newPatNum);
		}

		///<summary></summary>
		public void InstantClasses(){
			tbPatient.InstantClasses();
			tbCoverage.InstantClasses();
			tbPlans.InstantClasses();
			tbFamily.InstantClasses();
			//cannot use Lan.F(this);
			Lan.C(this,new Control[]
				{
				butAddPt,
				butDeletePt,
				butSetGuar,
				butMovePat,
				butPatEdit,
				butEditPriCov,
				butEditPriPlan,
				butEditSecCov,
				butEditSecPlan
				});
			LayoutToolBar();
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ODToolBarButton button;
			button=new ODToolBarButton(Lan.g(this,"Select Patient"),0,"","Patient");
			button.Style=ODToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=menuPatient;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Recall"),1,"","Recall"));
			//ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Add Family Member"),1,"",""));
			//ToolBarMain.Buttons.Add(new ODToolBarButton("",2,"",Lan.g(this,"Delete Family Member")));
			//ToolBarMain.Buttons.Add(new ODToolBarButton("",3,"",Lan.g(this,"Set as Guarantor")));
			//ToolBarMain.Buttons.Add(new ODToolBarButton("",4,"",Lan.g(this,"Move to Another Family")));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.FamilyModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		private void ContrFamily_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			tbPlans.LayoutTables();
		}		

		//private void butOutlook_Click(object sender, System.EventArgs e) {
			/*Process[] procsOutlook = Process.GetProcessesByName("outlook");
			if(procsOutlook.Length==0){
				try{
					Process.Start("Outlook");
				}
				catch{}
			}*/
		//}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "Patient":
						OnPat_Click();
						break;
					case "Recall":
						OnRecall_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)){
				Programs.Execute((int)e.Button.Tag,PatCur);
			}
		}

		private void OnPat_Click() {
			FormPatientSelect formPS=new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult==DialogResult.OK){
				OnPatientSelected(formPS.SelectedPatNum);
				ModuleSelected(formPS.SelectedPatNum);
			}
		}

		///<summary></summary>
		private void OnPatientSelected(int patNum){
			PatientSelectedEventArgs eArgs=new OpenDental.PatientSelectedEventArgs(patNum);
			if(PatientSelected!=null)
				PatientSelected(this,eArgs);
		}

		private void OnRecall_Click() {
			//patient may or may not have an existing recall.
			Recall recallCur=null;
			for(int i=0;i<RecallList.Length;i++){
				if(RecallList[i].PatNum==PatCur.PatNum){
					recallCur=RecallList[i];
				}
			}
			//for testing purposes and because synchronization might have bugs, always synch here:
			//This might add a recall.
			Recalls.Synch(PatCur.PatNum,recallCur);			
			FormRecallEdit FormRE=new FormRecallEdit(PatCur);
			if(recallCur==null){
				recallCur=new Recall();
				recallCur.PatNum=PatCur.PatNum;
				recallCur.RecallInterval=new Interval(0,0,6,0);
				FormRE.IsNew=true;
			}
			FormRE.RecallCur=recallCur;
			FormRE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		#region tbPatient

		private void butPatEdit_Click(object sender, System.EventArgs e) {
			FormPatientEdit FormPatientEdit2=new FormPatientEdit(PatCur,FamCur);
			FormPatientEdit2.IsNew=false;
			FormPatientEdit2.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void tbPatient_CellDoubleClicked(object sender, CellEventArgs e){
			FormPatientEdit FormPatientEdit2=new FormPatientEdit(PatCur,FamCur);
			FormPatientEdit2.IsNew=false;
			FormPatientEdit2.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void textAddrNotes_DoubleClick(object sender, System.EventArgs e) {
			FormPatientEdit FormPatientEdit2=new FormPatientEdit(PatCur,FamCur);
			FormPatientEdit2.IsNew=false;
			FormPatientEdit2.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void FillPatientData(){
			if(PatCur==null){
				return;
			}
			tbPatient.Cell[1,1]=PatCur.LName;
			tbPatient.Cell[1,2]=PatCur.FName;
			tbPatient.Cell[1,3]=PatCur.MiddleI;
			tbPatient.Cell[1,4]=PatCur.Preferred;
			tbPatient.Cell[1,5]=PatCur.Salutation;
			tbPatient.Cell[1,6]=Lan.g("enum PatientStatus",PatCur.PatStatus.ToString());
			tbPatient.Cell[1,7]=Lan.g("enum PatientGender",PatCur.Gender.ToString());
			tbPatient.Cell[1,8]=Lan.g("enum PatientPosition",PatCur.Position.ToString());
			if(PatCur.Birthdate.Year < 1880)
				tbPatient.Cell[1,9]="";
			else
				tbPatient.Cell[1,9]=PatCur.Birthdate.ToString("d");
			tbPatient.Cell[1,10]=Shared.AgeToString(PatCur.Age);
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="US"){
				if(PatCur.SSN !=null && PatCur.SSN.Length==9)
					tbPatient.Cell[1,11]=PatCur.SSN.Substring(0,3)+"-"
						+PatCur.SSN.Substring(3,2)+"-"+PatCur.SSN.Substring(5,4);
				else tbPatient.Cell[1,11]=PatCur.SSN;
			}
			else{
				tbPatient.Cell[1,11]=PatCur.SSN;
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){
				tbPatient.Cell[0,11]="SIN";
				tbPatient.Cell[0,16]="Postal Code";
				tbPatient.Cell[0,15]="Province";
			}
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="GB"){
				tbPatient.Cell[0,11]="";
				tbPatient.Cell[0,16]="Postcode";
				tbPatient.Cell[0,15]="";
			}
			tbPatient.Cell[1,12]=PatCur.Address;
			tbPatient.Cell[1,13]=PatCur.Address2;
			tbPatient.Cell[1,14]=PatCur.City;
			tbPatient.Cell[1,15]=PatCur.State;
			tbPatient.Cell[1,16]=PatCur.Zip;
			tbPatient.Cell[1,17]=PatCur.HmPhone;
			tbPatient.Cell[1,18]=PatCur.WkPhone;
			tbPatient.Cell[1,19]=PatCur.WirelessPhone;
			tbPatient.Cell[1,20]=PatCur.Email;
			tbPatient.Cell[1,21]=PatCur.CreditType;
			//tbPatient.Cell[1,22]=PatCur.RecallInterval.ToString();
			tbPatient.Cell[1,22]=PatCur.ChartNumber;
			tbPatient.Cell[1,23]=Defs.GetName(DefCat.BillingTypes,PatCur.BillingType);
			tbPatient.Cell[1,24]="";
			if(PatCur!=null){
				butPatEdit.Enabled=true;
				RefAttach[] RefList=RefAttaches.Refresh(PatCur.PatNum);
				for(int i=0;i<RefList.Length;i++){
					if(RefList[i].IsFrom){
						tbPatient.Cell[1,24]=Referrals.GetReferral(RefList[i].ReferralNum).GetName();
						break;
					}				
				}
				textAddrNotes.Text=PatCur.AddrNote;
			}
			else{
				butPatEdit.Enabled=false;
				textAddrNotes.Text="";
				tbPatient.Cell[1,6]="";
				tbPatient.Cell[1,7]="";
				tbPatient.Cell[1,8]="";
				tbPatient.Cell[1,9]="";
			}
			tbPatient.Refresh();
		}

		#endregion tbPatient 

		#region tbPlans
		private void FillPlanData(){
			if(PatCur==null){
				tbPlans.ResetRows(0);
				tbPlans.LayoutTables();
				return;
			}
			//InsPlans.Refresh();
			tbPlans.ResetRows(PlanList.Length);
			tbPlans.SetGridColor(Color.Gray);
			tbPlans.SetBackGColor(Color.White);
			for(int i=0;i<PlanList.Length;i++){
				tbPlans.Cell[0,i]=(i+1).ToString();
				tbPlans.Cell[1,i]=FamCur.GetNameInFamLF(PlanList[i].Subscriber);
				if(tbPlans.Cell[1,i]==""){//subscriber from another family
					tbPlans.Cell[1,i]=Patients.GetLim(PlanList[i].Subscriber).GetNameLF();
				}
				tbPlans.Cell[2,i]=Carriers.GetName(PlanList[i].CarrierNum);
				if(PlanList[i].DateEffective.Year<1880)
					tbPlans.Cell[3,i]="";
				else
					tbPlans.Cell[3,i]=PlanList[i].DateEffective.ToString("d");
				if(PlanList[i].DateTerm.Year<1880)
					tbPlans.Cell[4,i]="";
				else
					tbPlans.Cell[4,i]=PlanList[i].DateTerm.ToString("d");
				//tbPlans.Cell[5,i]=PlanList[i].PlanNote;
			}
			tbPlans.LayoutTables();
			
			
		}

		/*private void butAddInsPlan_Click(object sender, System.EventArgs e) {//obsolete
			FormInsTemplates FormInsTemplates = new FormInsTemplates();
			//FormInsTemplates.IsPrimary
			FormInsTemplates.ShowDialog();
			ModuleSelected();
		}*/

		/*private void butDeletePlan_Click(object sender, System.EventArgs e) {
			if(InsPlans.Selected==-1){
				MessageBox.Show(Lan.g(this,"Please select a plan first."));
				return;
			}
			if (MessageBox.Show(Lan.g(this,"Delete Plan?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			InsPlans.DeleteSelected();//checks dependencies first
			InsPlans.Selected=-1;
			ModuleSelected();
		}*/

		

		private void tbPlans_CellClicked(object sender, CellEventArgs e){
			/*if(InsPlans.Selected==e.Row){
				tbPlans.ColorRow(e.Row,Color.White);
				InsPlans.Selected=-1;
			}
			else{
				if(InsPlans.Selected!=-1){
					tbPlans.ColorRow(InsPlans.Selected,Color.White);
				}
				tbPlans.ColorRow(e.Row,Color.LightGray);
				InsPlans.Selected=e.Row;
			}*/
		}

		private void tbPlans_CellDoubleClicked(object sender, CellEventArgs e){
			InsPlan PlanCur=PlanList[e.Row];
			FormInsPlan FormIP=new FormInsPlan(PlanCur,PatCur.PatNum);
			FormIP.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		

		#endregion

		#region tbFamily

		private void FillFamilyData(){
			if(PatCur==null){
				tbFamily.SelectedRow=-1;
				tbFamily.ResetRows(0);
				tbFamily.LayoutTables();
				return;
			}
			tbFamily.ResetRows(FamCur.List.Length);
			tbFamily.SetGridColor(Color.Gray);
			tbFamily.SetBackGColor(Color.White);
			for(int i=0;i<FamCur.List.Length; i++){
				if(FamCur.List[i].PatNum==FamCur.List[i].Guarantor){
					for(int j=0;j<5;j++){
						tbFamily.FontBold[j,i]=true;
					}
					//tbFamily.Cell[0,i]=Lan.g(this,"Guar");
				}
				tbFamily.Cell[0,i]=FamCur.GetNameInFamLFI(i);
				tbFamily.Cell[1,i]=Lan.g("enum PatientPosition",FamCur.List[i].Position.ToString());
				tbFamily.Cell[2,i]=Lan.g("enum PatientGender",FamCur.List[i].Gender.ToString());
				tbFamily.Cell[3,i]=Lan.g("enum PatientStatus",FamCur.List[i].PatStatus.ToString());
				tbFamily.Cell[4,i]=Shared.AgeToString(FamCur.List[i].Age);
				for(int j=0;j<RecallList.Length;j++){
					if(RecallList[j].PatNum==FamCur.List[i].PatNum){
						if(RecallList[j].DateDue.Year>1880){
							tbFamily.Cell[5,i]=RecallList[j].DateDue.ToShortDateString();
						}
					}
				}
				if(FamCur.List[i].PatNum==PatCur.PatNum){
					tbFamily.SelectedRow=i;
					tbFamily.ColorRow(i,Color.DarkSalmon);
				}
			}//end for
			tbFamily.LayoutTables();
		}//end FillFamilyData

		private void tbFamily_CellClicked(object sender, CellEventArgs e){
			if (tbFamily.SelectedRow != -1){
				tbFamily.ColorRow(tbFamily.SelectedRow,Color.White);
			}
			tbFamily.SelectedRow=e.Row;
			tbFamily.ColorRow(e.Row,Color.DarkSalmon);
			OnPatientSelected(FamCur.List[e.Row].PatNum);
			ModuleSelected(FamCur.List[e.Row].PatNum);
		}

		private void butAddPt_Click(object sender, System.EventArgs e) {
			Patient tempPat=new Patient();
			tempPat.LName      =PatCur.LName;
			tempPat.PatStatus  =PatientStatus.Patient;
			tempPat.Address    =PatCur.Address;
			tempPat.Address2   =PatCur.Address2;
			tempPat.City       =PatCur.City;
			tempPat.State      =PatCur.State;
			tempPat.Zip        =PatCur.Zip;
			tempPat.HmPhone    =PatCur.HmPhone;
			tempPat.Guarantor  =PatCur.Guarantor;
			tempPat.CreditType =PatCur.CreditType;
			tempPat.PriProv    =PatCur.PriProv;
			tempPat.SecProv    =PatCur.SecProv;
			tempPat.FeeSched   =PatCur.FeeSched;
			tempPat.BillingType=PatCur.BillingType;
			tempPat.AddrNote   =PatCur.AddrNote;
			tempPat.Insert();
			FormPatientEdit FormPE=new FormPatientEdit(tempPat,FamCur);
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.OK){
				ModuleSelected(tempPat.PatNum);
			}
			else{
				ModuleSelected(PatCur.PatNum);
			}
		}

		private void butDeletePt_Click(object sender, System.EventArgs e) {
			//this doesn't actually delete the patient, just changes their status
			//and they will never show again in the patient selection list.
			//check for plans, appointments, procedures, etc.
			Procedure[] procList=Procedures.Refresh(PatCur.PatNum);
			Claims.Refresh(PatCur.PatNum);
			Adjustments.Refresh(PatCur.PatNum);
			PaySplits.Refresh(PatCur.PatNum);
			ClaimProc[] claimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			Commlogs.Refresh(PatCur.PatNum);
			PayPlans.Refresh(PatCur.Guarantor,PatCur.PatNum);
			InsPlan[] planList=InsPlans.Refresh(FamCur);
			CovPats.Refresh(PatCur,planList);
			RefAttach[] RefAttachList=RefAttaches.Refresh(PatCur.PatNum);
			bool hasProcs=procList.Length>0;
			bool hasClaims=Claims.List.Length>0;
			bool hasAdj=Adjustments.List.Length>0;
			bool hasPay=PaySplits.List.Length>0;
			bool hasClaimProcs=claimProcList.Length>0;
			bool hasComm=Commlogs.List.Length>0;
			bool hasPayPlans=PayPlans.List.Length>0;
			bool hasInsPlans=false;
			for(int i=0;i<planList.Length;i++){
				if(planList[i].Subscriber==PatCur.PatNum){
					hasInsPlans=true;
				}
			}
			bool hasRef=RefAttachList.Length>0;
			if(hasProcs || hasClaims || hasAdj || hasPay || hasClaimProcs || hasComm || hasPayPlans
				|| hasInsPlans || hasRef)
			{
				string message=Lan.g(this,
					"You cannot delete this patient without first deleting the following data:")+"\r";
				if(hasProcs)
					message+=Lan.g(this,"Procedures")+"\r";
				if(hasClaims)
					message+=Lan.g(this,"Claims")+"\r";
				if(hasAdj)
					message+=Lan.g(this,"Adjustments")+"\r";
				if(hasPay)
					message+=Lan.g(this,"Payments")+"\r";
				if(hasClaimProcs)
					message+=Lan.g(this,"Procedures attached to claims")+"\r";
				if(hasComm)
					message+=Lan.g(this,"Commlog entries")+"\r";
				if(hasPayPlans)
					message+=Lan.g(this,"Payment plans")+"\r";
				if(hasInsPlans)
					message+=Lan.g(this,"Insurance plans")+"\r";
				if(hasRef)
					message+=Lan.g(this,"References")+"\r";
				MessageBox.Show(message);
				return;
			}
			Patient PatOld=PatCur.Copy();
			if(PatCur.PatNum==PatCur.Guarantor){//if selecting guarantor
				if(FamCur.List.Length==1){
					if(!MsgBox.Show(this,true,"Delete Patient?"))
						return;
					PatCur.PatStatus=PatientStatus.Deleted;
					PatCur.ChartNumber="";
					PatCur.Update(PatOld);
					for(int i=0;i<RecallList.Length;i++){
						if(RecallList[i].PatNum==PatCur.PatNum){
							RecallList[i].IsDisabled=true;
							RecallList[i].DateDue=DateTime.MinValue;
							RecallList[i].Update();
						}
					}
					ModuleSelected(0);
					//does not delete notes or plans, etc.
				}
				else{
					MessageBox.Show(Lan.g(this,"You cannot delete the guarantor if there are other family members. You would have to make a different family member the guarantor first."));
				}
			}
			else{//not selecting guarantor
				if(!MsgBox.Show(this,true,"Delete Patient?"))
					return;
				PatCur.PatStatus=PatientStatus.Deleted;
				PatCur.ChartNumber="";
				PatCur.Guarantor=PatCur.PatNum;
				PatCur.Update(PatOld);
				for(int i=0;i<RecallList.Length;i++){
					if(RecallList[i].PatNum==PatCur.PatNum){
						RecallList[i].IsDisabled=true;
						RecallList[i].DateDue=DateTime.MinValue;
						RecallList[i].Update();
					}
				}
				ModuleSelected(PatOld.Guarantor);
			}
		}

		private void butSetGuar_Click(object sender,System.EventArgs e){     
			//Patient tempPat=PatCur;
			if(PatCur.PatNum==PatCur.Guarantor){
				MessageBox.Show(Lan.g(this
					,"Patient is already the guarantor.  Please select a different family member."));
			}
			else{
				if(MessageBox.Show(Lan.g(this,"Make the selected patient the guarantor?")
					,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
						return;
				Patients.ChangeGuarantorToCur(FamCur,PatCur);
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void butMovePat_Click(object sender, System.EventArgs e) {
			Patient PatOld=PatCur.Copy();
			//Patient PatCur;
			if(PatCur.PatNum==PatCur.Guarantor){//if guarantor selected
				if(FamCur.List.Length==1){//and no other family members
					//no need to check insurance.  It will follow.
					if(!MsgBox.Show(this,true,"Moving the guarantor will cause two families to be combined.  The financial notes for both families will be combined and may need to be edited.  The address notes will also be combined and may need to be edited. Do you wish to continue?"))
						return;
					if(!MsgBox.Show(this,true,"Select the family to move this patient to from the list that will come up next."))
						return;
					FormPatientSelect FormPS=new FormPatientSelect();
					FormPS.SelectionModeOnly=true;
					FormPS.ShowDialog();
					if(FormPS.DialogResult!=DialogResult.OK){
						return;
					}
					Patient Lim=Patients.GetLim(FormPS.SelectedPatNum);
					PatCur.Guarantor=Lim.Guarantor;
					PatCur.Update(PatOld);
					FamCur=Patients.GetFamily(PatCur.PatNum);
					Patients.CombineGuarantors(FamCur,PatCur);
				}
				else{//there are other family members
					MessageBox.Show(Lan.g(this,"You cannot move the guarantor.  If you wish to move the guarantor, you must make another family member the guarantor first."));
				}
			}
			else{//guarantor not selected
				if(!MsgBox.Show(this,true,"Preparing to move family member.  Financial notes and address notes will not be transferred.  Proceed to next step?")){
					return;
				}
				switch(MessageBox.Show(Lan.g(this,"Create new family instead of moving to an existing family?")
					,"",MessageBoxButtons.YesNoCancel)){
					case DialogResult.Cancel:
						return;
					case DialogResult.Yes://new family
						PatCur.Guarantor=PatCur.PatNum;
						PatCur.Update(PatOld);
						break;
					case DialogResult.No://move to an existing family
						if(!MsgBox.Show(this,true,"Select the family to move this patient to from the list that will come up next.")){
							return;
						}
						FormPatientSelect FormPS=new FormPatientSelect();
						FormPS.SelectionModeOnly=true;
						FormPS.ShowDialog();
						if(FormPS.DialogResult!=DialogResult.OK){
							return;
						}
						Patient Lim=Patients.GetLim(FormPS.SelectedPatNum);
						PatCur.Guarantor=Lim.Guarantor;
						PatCur.Update(PatOld);
						break;
				}//end switch
			}//end guarantor not selected
			ModuleSelected(PatCur.PatNum);
		}

		#endregion
		
		#region tbCoverage

		private void butEditPriCov_Click(object sender, System.EventArgs e) {
			OpenCovEdit();
		}

		private void butEditPriPlan_Click(object sender, System.EventArgs e) {
			//this button has two different functions:
			//New
			if(PatCur.PriPlanNum==0){
				InsPlan PlanCur=new InsPlan();
				PlanCur.Subscriber=PatCur.PatNum;
				PlanCur.SubscriberID=PatCur.SSN;
				PlanCur.EmployerNum=PatCur.EmployerNum;
				PlanCur.AnnualMax=-1;//blank
				PlanCur.OrthoMax=-1;
				PlanCur.RenewMonth=1;
				PlanCur.Deductible=-1;
				PlanCur.FloToAge=-1;
				PlanCur.ReleaseInfo=true;
				PlanCur.AssignBen=true;
				PlanCur.Insert();
				FormInsPlan FormIP=new FormInsPlan(PlanCur,PatCur.PatNum);
				FormIP.IsNew=true;
				FormIP.ShowDialog();//this updates estimates also
				if(FormIP.DialogResult==DialogResult.OK
					&& PlanCur.PlanNum!=0)
				{
					Patient PatOld=PatCur.Copy();
					PatCur.PriPlanNum=PlanCur.PlanNum;
					PatCur.Update(PatOld);
				}
				ModuleSelected(PatCur.PatNum);
			}
			//edit
			else{
				OpenPriPlanEdit();
			}
		}

		private void butEditSecCov_Click(object sender, System.EventArgs e) {
			OpenCovEdit();
		}

		private void butEditSecPlan_Click(object sender, System.EventArgs e) {
			OpenSecPlanEdit();
		}

		private void checkPriPending_Click(object sender, System.EventArgs e) {
			Patient PatOld=PatCur.Copy();
			PatCur.PriPending=checkPriPending.Checked;
			//Patients.Cur=PatCur;
			PatCur.Update(PatOld);
			ModuleSelected(PatCur.PatNum);
		}

		private void checkSecPending_Click(object sender, System.EventArgs e) {
			Patient PatOld=PatCur.Copy();
			PatCur.SecPending=checkSecPending.Checked;
			PatCur.Update(PatOld);
			ModuleSelected(PatCur.PatNum);
		}

		private void tbCoverage_CellDoubleClicked(object sender, CellEventArgs e){
			if(e.Row>=0 && e.Row<=2){
				OpenCovEdit();
			}
			else{
				if(e.Col==0 || e.Col==1){
					OpenPriPlanEdit();
				}
				else if(e.Col==2 || e.Col==3){
					OpenSecPlanEdit();
				}
			}
			//ModuleSelected();//already handled
		}

		private void textPriPlanNote_DoubleClick(object sender, System.EventArgs e) {
			OpenPriPlanEdit();
		}

		private void textSecPlanNote_DoubleClick(object sender, System.EventArgs e) {
			OpenSecPlanEdit();
		}

		private void tbPercent1_CellDoubleClicked(object sender, CellEventArgs e){
			OpenPriPlanEdit();
		}
		
		private void tbPercent2_CellDoubleClicked(object sender, CellEventArgs e){
			OpenSecPlanEdit();
		}

		private void OpenPriPlanEdit() {
			if(PatCur.PriPlanNum==0){
				return;
			}
			InsPlan PlanCur=InsPlans.GetPlan(PatCur.PriPlanNum,PlanList);
			if(PlanCur.PlanNum==0){//if corrupted
				Patient PatOld=PatCur.Copy();
				PatCur.PriPlanNum=0;
				PatCur.Update(PatOld);
			}
			else{//normal
				FormInsPlan FormIP=new FormInsPlan(PlanCur,PatCur.PatNum);
				FormIP.DropButVisible=true;
				FormIP.ShowDialog();
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void OpenSecPlanEdit() {
			if(PatCur.SecPlanNum==0){
				return;
			}
			InsPlan PlanCur=InsPlans.GetPlan(PatCur.SecPlanNum,PlanList);
			if(PlanCur.PlanNum==0){//if corrupted
				Patient PatOld=PatCur.Copy();
				PatCur.SecPlanNum=0;
				PatCur.Update(PatOld);
			}
			else{//normal
				FormInsPlan FormIP=new FormInsPlan(PlanCur,PatCur.PatNum);
				FormIP.DropButVisible=true;
				FormIP.ShowDialog();
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void OpenCovEdit(){
			FormInsCovEdit FormInsCovEdit2=new FormInsCovEdit(PatCur.PatNum);
			FormInsCovEdit2.ShowDialog();
			//Patients.GetFamily(PatCur.PatNum);
			ClaimProc[] claimProcs=ClaimProcs.Refresh(PatCur.PatNum);
			Procedure[] procs=Procedures.Refresh(PatCur.PatNum);
			Procedures.ComputeEstimatesForAll(PatCur.PatNum,PatCur.PriPlanNum,
				PatCur.SecPlanNum,claimProcs,procs,PatCur,PlanList);
			ModuleSelected(PatCur.PatNum);
		}

		private void FillCoverageData(){
			Color covColor=//Color.FromArgb(212,232,199);
				Defs.Long[(int)DefCat.MiscColors][0].ItemColor; //Color.FromName("Highlight");//
			for(int i=0;i<4;i++){
				tbCoverage.SetBackColorRow(i,covColor);
			}
			if(PatCur==null){
				checkPriPending.Checked=false;
				checkSecPending.Checked=false;
				checkPriPending.Enabled=false;
				checkSecPending.Enabled=false;
				butEditPriCov.Enabled=false;
				butEditSecCov.Enabled=false;
				butEditPriPlan.Enabled=false;
				butEditSecPlan.Enabled=false;
				for(int i=1;i<18;i++){
					tbCoverage.Cell[1,i]="";
					tbCoverage.Cell[3,i]="";
				}
				tbCoverage.Refresh();
				textPriPlanNote.Text="";
				tbPercent1.ResetRows(6);
				tbPercent1.LayoutTables();
				tbPercent2.ResetRows(6);
				tbPercent2.LayoutTables();
				return;
			}
			checkPriPending.Checked=PatCur.PriPending;
			checkSecPending.Checked=PatCur.SecPending;
			checkPriPending.Enabled=true;
			checkSecPending.Enabled=true;
			butEditPriPlan.Enabled=true;
			butEditPriCov.Enabled=true;
			butEditSecCov.Enabled=true;
			//CovPats CovPats=new CovPats();
			tbPercent1.ResetRows(CovCats.ListShort.Length);
			tbPercent1.SetGridColor(Color.LightGray);
			for(int i=0;i<CovCats.ListShort.Length;i++){
				tbPercent1.Cell[0,i]=CovCats.ListShort[i].Description;
				if(CovPats.PriList[i]==-1)
					tbPercent1.Cell[1,i]="";
				else
					tbPercent1.Cell[1,i]=CovPats.PriList[i].ToString();
			}
			tbPercent1.LayoutTables();
			tbPercent2.ResetRows(CovCats.ListShort.Length);
			tbPercent2.SetGridColor(Color.LightGray);
			for(int i=0;i<CovCats.ListShort.Length;i++){
				tbPercent2.Cell[0,i]=CovCats.ListShort[i].Description;
				if(CovPats.SecList[i]==-1)
					tbPercent2.Cell[1,i]="";
				else
					tbPercent2.Cell[1,i]=CovPats.SecList[i].ToString();
			}
			tbPercent2.LayoutTables();
			InsPlan PlanCur=new InsPlan();
			if(PatCur.PriPlanNum==0){
				butEditPriPlan.Text=Lan.g(this,"New");
				for(int i=1;i<18;i++){
					tbCoverage.Cell[1,i]="";
				}
				textPriPlanNote.Text="";
			}
			else{
				butEditPriPlan.Text=Lan.g(this,"Edit");
				for(int i=0;i<PlanList.Length;i++){
					if(PlanList[i].PlanNum==PatCur.PriPlanNum){
						PlanCur=PlanList[i];
					}
				}
				tbCoverage.Cell[1,1]=InsPlans.GetDescript(PatCur.PriPlanNum,FamCur,PlanList);
				tbCoverage.Cell[1,2]=PatCur.PriRelationship.ToString();
				if(PlanCur.AnnualMax==-1)
					tbCoverage.Cell[1,5]="";
				else
					tbCoverage.Cell[1,5]=PlanCur.AnnualMax.ToString();
				if(PlanCur.OrthoMax==-1)
					tbCoverage.Cell[1,6]="";
				else
					tbCoverage.Cell[1,6]=PlanCur.OrthoMax.ToString();
				if(PlanCur.RenewMonth==-1)
					tbCoverage.Cell[1,7]="";
				else
					tbCoverage.Cell[1,7]=PlanCur.RenewMonth.ToString();
				if(PlanCur.Deductible==-1)
					tbCoverage.Cell[1,8]="";
				else
					tbCoverage.Cell[1,8]=PlanCur.Deductible.ToString();
				if(PlanCur.DeductWaivPrev==YN.Unknown)
					tbCoverage.Cell[1,9]="";
				else
					tbCoverage.Cell[1,9]=PlanCur.DeductWaivPrev.ToString();
				if(PlanCur.FloToAge==-1)
					tbCoverage.Cell[1,15]="";
				else
					tbCoverage.Cell[1,15]=PlanCur.FloToAge.ToString();
				if(PlanCur.MissToothExcl==YN.Unknown)
					tbCoverage.Cell[1,16]="";
				else
					tbCoverage.Cell[1,16]=PlanCur.MissToothExcl.ToString();
				if(PlanCur.MajorWait==YN.Unknown)
					tbCoverage.Cell[1,17]="";
				else
					tbCoverage.Cell[1,17]=PlanCur.MajorWait.ToString();
				textPriPlanNote.Text=PlanCur.PlanNote;
			}
			if(PatCur.SecPlanNum==0){
				butEditSecPlan.Enabled=false;
				for(int i=1;i<18;i++){
					tbCoverage.Cell[3,i]="";
				}
				textSecPlanNote.Text="";
			}
			else{
				butEditSecPlan.Enabled=true;
				for(int i=0;i<PlanList.Length;i++){
					if(PlanList[i].PlanNum==PatCur.SecPlanNum){
						PlanCur=PlanList[i];
					}
				}
				tbCoverage.Cell[3,1]=InsPlans.GetDescript(PatCur.SecPlanNum,FamCur,PlanList);
				tbCoverage.Cell[3,2]=PatCur.SecRelationship.ToString();
				if(PlanCur.AnnualMax==-1)
					tbCoverage.Cell[3,5]="";
				else
					tbCoverage.Cell[3,5]=PlanCur.AnnualMax.ToString();
				if(PlanCur.OrthoMax==-1)
					tbCoverage.Cell[3,6]="";
				else
					tbCoverage.Cell[3,6]=PlanCur.OrthoMax.ToString();
				if(PlanCur.RenewMonth==-1)
					tbCoverage.Cell[3,7]="";
				else
					tbCoverage.Cell[3,7]=PlanCur.RenewMonth.ToString();
				if(PlanCur.Deductible==-1)
					tbCoverage.Cell[3,8]="";
				else
					tbCoverage.Cell[3,8]=PlanCur.Deductible.ToString();
				if(PlanCur.DeductWaivPrev==YN.Unknown)
					tbCoverage.Cell[3,9]="";
				else
					tbCoverage.Cell[3,9]=PlanCur.DeductWaivPrev.ToString();
				if(PlanCur.FloToAge==-1)
					tbCoverage.Cell[3,15]="";
				else
					tbCoverage.Cell[3,15]=PlanCur.FloToAge.ToString();
				if(PlanCur.MissToothExcl==YN.Unknown)
					tbCoverage.Cell[3,16]="";
				else
					tbCoverage.Cell[3,16]=PlanCur.MissToothExcl.ToString();
				if(PlanCur.MajorWait==YN.Unknown)
					tbCoverage.Cell[3,17]="";
				else
					tbCoverage.Cell[3,17]=PlanCur.MajorWait.ToString();
				//MessageBox.Show(PlanCur.PlanNum.ToString());
				textSecPlanNote.Text=PlanCur.PlanNote;
			}
			tbCoverage.Refresh();
		}

		

		#endregion tbCoverage

		

		

		

		

		
		

		

		
	}
}
