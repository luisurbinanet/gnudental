/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{

	///<summary></summary>
	public class ContrAccount : System.Windows.Forms.UserControl{
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components=null;// Required designer variable.
		private Procedure[] arrayProc;
		private ArrayList AcctLineAL;
		private OpenDental.TableAccount tbAccount;
		private OpenDental.TableAccountPat tbAcctPat;
		private PaySplit[] arrayPay;
		private Adjustment[] arrayAdj;
		private Claim[] arrayClaim;
		private Commlog[] arrayComm;
		private PayPlan[] arrayPayPlan;
		//private bool ControlDown;
		///<summary></summary>
		public static string[,] StatementA;
		private System.Windows.Forms.TextBox textFinNotes;
		private System.Windows.Forms.Label labelUrgFinNote;
		private System.Windows.Forms.TextBox textUrgFinNote;
		private System.Windows.Forms.Panel panelTotal;
		///<summary></summary>
		public bool ViewOnly=false;
		private double FamTotBal;
		private double FamInsEst;
		private double FamTotDue;
		private FormAdjust FormAdjust2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textOver90;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox text61_90;
		private System.Windows.Forms.TextBox text31_60;
		private System.Windows.Forms.TextBox text0_30;
		private System.Windows.Forms.TextBox textAgeTotal;
		private System.Windows.Forms.TextBox textAgeInsEst;
		private System.Windows.Forms.TextBox textAgeBalance;
		private FormPayment FormPayment2;
		private System.Windows.Forms.ContextMenu contextMenuIns;
		private System.Windows.Forms.MenuItem menuInsOther;
		private System.Windows.Forms.MenuItem menuInsPri;
		private System.Windows.Forms.MenuItem menuInsSec;
		private FormCommItem FormCommItem2;
		private FormPayPlan FormPayPlan2;
		private bool UrgFinNoteChanged;
		private System.Windows.Forms.CheckBox checkShowAll;
		private bool FinNoteChanged;
		private OpenDental.UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ImageList imageListMain;
		///<summary>Used only in printing reports that show subtotal only.</summary>
		private double SubTotal;
	
		///<summary></summary>
		public ContrAccount(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			tbAccount.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbAccount_CellClicked);
			tbAccount.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbAccount_CellDoubleClicked);
			tbAcctPat.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbAcctPat_CellClicked);
		}

		///<summary></summary>
		protected override void Dispose(bool disposing){
			if(disposing){
				if(components!= null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrAccount));
			this.textFinNotes = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.labelUrgFinNote = new System.Windows.Forms.Label();
			this.textUrgFinNote = new System.Windows.Forms.TextBox();
			this.tbAccount = new OpenDental.TableAccount();
			this.tbAcctPat = new OpenDental.TableAccountPat();
			this.panelTotal = new System.Windows.Forms.Panel();
			this.checkShowAll = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textOver90 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.text61_90 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.text31_60 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.text0_30 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textAgeTotal = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textAgeInsEst = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textAgeBalance = new System.Windows.Forms.TextBox();
			this.contextMenuIns = new System.Windows.Forms.ContextMenu();
			this.menuInsPri = new System.Windows.Forms.MenuItem();
			this.menuInsSec = new System.Windows.Forms.MenuItem();
			this.menuInsOther = new System.Windows.Forms.MenuItem();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.panelTotal.SuspendLayout();
			this.SuspendLayout();
			// 
			// textFinNotes
			// 
			this.textFinNotes.BackColor = System.Drawing.Color.White;
			this.textFinNotes.Location = new System.Drawing.Point(767, 253);
			this.textFinNotes.Multiline = true;
			this.textFinNotes.Name = "textFinNotes";
			this.textFinNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textFinNotes.Size = new System.Drawing.Size(163, 473);
			this.textFinNotes.TabIndex = 7;
			this.textFinNotes.Text = "";
			this.textFinNotes.TextChanged += new System.EventHandler(this.textFinNotes_TextChanged);
			this.textFinNotes.Leave += new System.EventHandler(this.textFinNotes_Leave);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(-2, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Family Financial Notes";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelUrgFinNote
			// 
			this.labelUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelUrgFinNote.Location = new System.Drawing.Point(766, 31);
			this.labelUrgFinNote.Name = "labelUrgFinNote";
			this.labelUrgFinNote.Size = new System.Drawing.Size(165, 21);
			this.labelUrgFinNote.TabIndex = 10;
			this.labelUrgFinNote.Text = "Fam Urgent Fin Note";
			this.labelUrgFinNote.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textUrgFinNote
			// 
			this.textUrgFinNote.BackColor = System.Drawing.Color.White;
			this.textUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textUrgFinNote.ForeColor = System.Drawing.Color.Red;
			this.textUrgFinNote.Location = new System.Drawing.Point(768, 53);
			this.textUrgFinNote.Multiline = true;
			this.textUrgFinNote.Name = "textUrgFinNote";
			this.textUrgFinNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textUrgFinNote.Size = new System.Drawing.Size(163, 36);
			this.textUrgFinNote.TabIndex = 11;
			this.textUrgFinNote.Text = "";
			this.textUrgFinNote.TextChanged += new System.EventHandler(this.textUrgFinNote_TextChanged);
			this.textUrgFinNote.Leave += new System.EventHandler(this.textUrgFinNote_Leave);
			// 
			// tbAccount
			// 
			this.tbAccount.BackColor = System.Drawing.SystemColors.Window;
			this.tbAccount.Location = new System.Drawing.Point(0, 65);
			this.tbAccount.Name = "tbAccount";
			this.tbAccount.ScrollValue = 1;
			this.tbAccount.SelectedIndices = new int[0];
			this.tbAccount.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbAccount.Size = new System.Drawing.Size(730, 655);
			this.tbAccount.TabIndex = 20;
			// 
			// tbAcctPat
			// 
			this.tbAcctPat.BackColor = System.Drawing.SystemColors.Window;
			this.tbAcctPat.Location = new System.Drawing.Point(768, 90);
			this.tbAcctPat.Name = "tbAcctPat";
			this.tbAcctPat.ScrollValue = 32;
			this.tbAcctPat.SelectedIndices = new int[0];
			this.tbAcctPat.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbAcctPat.Size = new System.Drawing.Size(163, 143);
			this.tbAcctPat.TabIndex = 21;
			// 
			// panelTotal
			// 
			this.panelTotal.Controls.Add(this.label1);
			this.panelTotal.Location = new System.Drawing.Point(768, 234);
			this.panelTotal.Name = "panelTotal";
			this.panelTotal.Size = new System.Drawing.Size(163, 21);
			this.panelTotal.TabIndex = 26;
			// 
			// checkShowAll
			// 
			this.checkShowAll.Checked = true;
			this.checkShowAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowAll.Location = new System.Drawing.Point(4, 48);
			this.checkShowAll.Name = "checkShowAll";
			this.checkShowAll.Size = new System.Drawing.Size(146, 16);
			this.checkShowAll.TabIndex = 29;
			this.checkShowAll.Text = "Show entire history";
			this.checkShowAll.Click += new System.EventHandler(this.checkShowAll_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(161, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 18);
			this.label2.TabIndex = 30;
			this.label2.Text = "Family Aging";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textOver90
			// 
			this.textOver90.Location = new System.Drawing.Point(443, 44);
			this.textOver90.Name = "textOver90";
			this.textOver90.ReadOnly = true;
			this.textOver90.Size = new System.Drawing.Size(60, 20);
			this.textOver90.TabIndex = 31;
			this.textOver90.Text = "";
			this.textOver90.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(443, 31);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 32;
			this.label3.Text = "over 90";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(383, 31);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 13);
			this.label5.TabIndex = 34;
			this.label5.Text = "61-90";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text61_90
			// 
			this.text61_90.Location = new System.Drawing.Point(383, 44);
			this.text61_90.Name = "text61_90";
			this.text61_90.ReadOnly = true;
			this.text61_90.Size = new System.Drawing.Size(60, 20);
			this.text61_90.TabIndex = 33;
			this.text61_90.Text = "";
			this.text61_90.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(323, 31);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 13);
			this.label6.TabIndex = 36;
			this.label6.Text = "31-60";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text31_60
			// 
			this.text31_60.Location = new System.Drawing.Point(323, 44);
			this.text31_60.Name = "text31_60";
			this.text31_60.ReadOnly = true;
			this.text31_60.Size = new System.Drawing.Size(60, 20);
			this.text31_60.TabIndex = 35;
			this.text31_60.Text = "";
			this.text31_60.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(265, 31);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 13);
			this.label7.TabIndex = 38;
			this.label7.Text = "0-30";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// text0_30
			// 
			this.text0_30.Location = new System.Drawing.Point(263, 44);
			this.text0_30.Name = "text0_30";
			this.text0_30.ReadOnly = true;
			this.text0_30.Size = new System.Drawing.Size(60, 20);
			this.text0_30.TabIndex = 37;
			this.text0_30.Text = "";
			this.text0_30.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(503, 31);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(60, 13);
			this.label8.TabIndex = 40;
			this.label8.Text = "Total";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textAgeTotal
			// 
			this.textAgeTotal.Location = new System.Drawing.Point(503, 44);
			this.textAgeTotal.Name = "textAgeTotal";
			this.textAgeTotal.ReadOnly = true;
			this.textAgeTotal.Size = new System.Drawing.Size(60, 20);
			this.textAgeTotal.TabIndex = 39;
			this.textAgeTotal.Text = "";
			this.textAgeTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(563, 31);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(60, 13);
			this.label9.TabIndex = 42;
			this.label9.Text = "- InsEst";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textAgeInsEst
			// 
			this.textAgeInsEst.Location = new System.Drawing.Point(563, 44);
			this.textAgeInsEst.Name = "textAgeInsEst";
			this.textAgeInsEst.ReadOnly = true;
			this.textAgeInsEst.Size = new System.Drawing.Size(60, 20);
			this.textAgeInsEst.TabIndex = 41;
			this.textAgeInsEst.Text = "";
			this.textAgeInsEst.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.Location = new System.Drawing.Point(626, 31);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60, 13);
			this.label10.TabIndex = 44;
			this.label10.Text = "= Balance";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textAgeBalance
			// 
			this.textAgeBalance.Location = new System.Drawing.Point(623, 44);
			this.textAgeBalance.Name = "textAgeBalance";
			this.textAgeBalance.ReadOnly = true;
			this.textAgeBalance.Size = new System.Drawing.Size(65, 20);
			this.textAgeBalance.TabIndex = 43;
			this.textAgeBalance.Text = "";
			this.textAgeBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// contextMenuIns
			// 
			this.contextMenuIns.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.menuInsPri,
																																									 this.menuInsSec,
																																									 this.menuInsOther});
			// 
			// menuInsPri
			// 
			this.menuInsPri.Index = 0;
			this.menuInsPri.Text = "Primary";
			this.menuInsPri.Click += new System.EventHandler(this.menuInsPri_Click);
			// 
			// menuInsSec
			// 
			this.menuInsSec.Index = 1;
			this.menuInsSec.Text = "Secondary";
			this.menuInsSec.Click += new System.EventHandler(this.menuInsSec_Click);
			// 
			// menuInsOther
			// 
			this.menuInsOther.Index = 2;
			this.menuInsOther.Text = "Other";
			this.menuInsOther.Click += new System.EventHandler(this.menuInsOther_Click);
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939, 29);
			this.ToolBarMain.TabIndex = 47;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// imageListMain
			// 
			this.imageListMain.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ContrAccount
			// 
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textAgeBalance);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textAgeInsEst);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textAgeTotal);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.text0_30);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.text31_60);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.text61_90);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textOver90);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.panelTotal);
			this.Controls.Add(this.tbAcctPat);
			this.Controls.Add(this.tbAccount);
			this.Controls.Add(this.textUrgFinNote);
			this.Controls.Add(this.labelUrgFinNote);
			this.Controls.Add(this.textFinNotes);
			this.Controls.Add(this.checkShowAll);
			this.Name = "ContrAccount";
			this.Size = new System.Drawing.Size(939, 732);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrAccount_Layout);
			this.panelTotal.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void InstantClasses(){
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.labelUrgFinNote,
				this.panelTotal,
				this.checkShowAll,
			});
			LayoutToolBar();
		}

		///<summary>Causes the toolbar to be laid out again.</summary>
		public void LayoutToolBar(){
			ToolBarMain.Buttons.Clear();
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Select Patient"),0,"","Patient"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Payment"),1,"","Payment"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Adjustment"),2,"","Adjustment"));
			ODToolBarButton button=new ODToolBarButton(Lan.g(this,"New Claim"),3,"","Insurance");
			button.Style=ToolBarButtonStyle.DropDownButton;
			button.DropDownMenu=contextMenuIns;
			ToolBarMain.Buttons.Add(button);
			ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Payment Plan"),-1,"","PayPlan"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Statement"),-1,"","Statement"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.AccountModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		private void ContrAccount_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			tbAccount.Height=Height-tbAccount.Location.Y;
			tbAccount.LayoutTables();
			if(Height>=446){
				textUrgFinNote.Height=72;//5 lines
			}
			else{
				textUrgFinNote.Height=46;//3 lines
			}
			tbAcctPat.Location=new Point(768,textUrgFinNote.Location.Y+textUrgFinNote.Height);
			panelTotal.Location=new Point(768,tbAcctPat.Location.Y+tbAcctPat.Height);
			textFinNotes.Location=new Point(768,panelTotal.Location.Y+panelTotal.Height);
			textFinNotes.Height=Height-textFinNotes.Location.Y-1;
		}

		///<summary></summary>
		public void ModuleSelected(){
			RefreshModuleData();
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			if(Patients.FamilyList==null)
				return;
			if(UrgFinNoteChanged){
				Patient tempPat=Patients.Cur;
				Patients.Cur=Patients.FamilyList[Patients.GuarIndex];
				Patients.Cur.FamFinUrgNote=textUrgFinNote.Text;
				Patients.UpdateCur();
				Patients.GetFamily(tempPat.PatNum);
				UrgFinNoteChanged=false;
			}
			if(FinNoteChanged){
				PatientNotes.Cur.FamFinancial=textFinNotes.Text;
				PatientNotes.UpdateCur();
				FinNoteChanged=false;
			}
			Patients.FamilyList=null;
			InsPlans.List=null;
			CovPats.List=null;
			//from FillAcctLineAL(){
			Procedures.List=null;
			Procedures.HList=null;
			Procedures.MissingTeeth=null;
			Claims.List=null;
			Adjustments.List=null;
			PaySplits.List=null;
			ClaimProcs.List=null;
			Commlogs.List=null;
			PayPlans.List=null;
		}

		private void RefreshModuleData(){
			if(Patients.PatIsLoaded){
				Patients.GetFamily(Patients.Cur.PatNum);
				InsPlans.Refresh();
				CovPats.Refresh();
				PatientNotes.Refresh();
			}
			//other tables are refreshed in the filltable
		}

		private void RefreshModuleScreen(){
			if(Patients.PatIsLoaded){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "+Patients.GetCurNameLF();
				tbAccount.Enabled=true;
				ToolBarMain.Buttons["Payment"].Enabled=true;
				ToolBarMain.Buttons["Adjustment"].Enabled=true;
				ToolBarMain.Buttons["Insurance"].Enabled=true;
				ToolBarMain.Buttons["PayPlan"].Enabled=true;
				ToolBarMain.Buttons["Statement"].Enabled=true;
				ToolBarMain.Invalidate();
				textUrgFinNote.Enabled=true;
				textFinNotes.Enabled=true;
				//butEditUrg.Enabled=true;
				//butEditFin.Enabled=true;
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				tbAccount.Enabled=false;
				ToolBarMain.Buttons["Payment"].Enabled=false;
				ToolBarMain.Buttons["Adjustment"].Enabled=false;
				ToolBarMain.Buttons["Insurance"].Enabled=false;
				ToolBarMain.Buttons["PayPlan"].Enabled=false;
				ToolBarMain.Buttons["Statement"].Enabled=false;
				ToolBarMain.Invalidate();
				textUrgFinNote.Enabled=false;
				textFinNotes.Enabled=false;
				//butEditUrg.Enabled=false;
				//butEditFin.Enabled=false;
			}
			FillMain();
			FillPats();
			FillMisc();
			FillAging();
		}

		private void FillAging(){
			if(Patients.PatIsLoaded){
				textOver90.Text=Patients.FamilyList[Patients.GuarIndex].BalOver90.ToString("F");
				text61_90.Text=Patients.FamilyList[Patients.GuarIndex].Bal_61_90.ToString("F");
				text31_60.Text=Patients.FamilyList[Patients.GuarIndex].Bal_31_60.ToString("F");
				text0_30.Text=Patients.FamilyList[Patients.GuarIndex].Bal_0_30.ToString("F");
				double total=Patients.FamilyList[Patients.GuarIndex].BalTotal;
				textAgeTotal.Text=total.ToString("F");
				textAgeInsEst.Text=Patients.FamilyList[Patients.GuarIndex].InsEst.ToString("F");
				textAgeBalance.Text=(total-Patients.FamilyList[Patients.GuarIndex].InsEst).ToString("F");
			}
			else{
				textOver90.Text="";
				text61_90.Text="";
				text31_60.Text="";
				text0_30.Text="";
				textAgeTotal.Text="";
				textAgeInsEst.Text="";
				textAgeBalance.Text="";
			}
		}

		private void FillMisc(){
			if(Patients.PatIsLoaded){
				textUrgFinNote.Text=Patients.FamilyList[Patients.GuarIndex].FamFinUrgNote;
				textFinNotes.Text=PatientNotes.Cur.FamFinancial;	
				textFinNotes.Select(textFinNotes.Text.Length+2,1);
				textFinNotes.ScrollToCaret();
				textUrgFinNote.SelectionStart=0;
				textUrgFinNote.ScrollToCaret();
			}
			else{
				textUrgFinNote.Text="";
				textFinNotes.Text="";				
			}
			UrgFinNoteChanged=false;
			FinNoteChanged=false;
			if(ViewOnly){
				textUrgFinNote.ReadOnly=true;
				textFinNotes.ReadOnly=true;		
			}
			else{
				textUrgFinNote.ReadOnly=false;
				textFinNotes.ReadOnly=false;		
			}
		}

		private void FillMain(DateTime fromDate, DateTime toDate,bool includeClaims,bool subtotalsOnly){
			//tbAccount.SelectedRowsAL=new ArrayList();
			if(!Patients.PatIsLoaded){
				tbAccount.ResetRows(0);
				tbAccount.LayoutTables();
				return;
			}
			FillAcctLineAL(fromDate,toDate,includeClaims,subtotalsOnly);
			FilltbAccount();
		}

		private void FillMain(){
			if(checkShowAll.Checked){
				FillMain(DateTime.MinValue,DateTime.Today,true,false);
			}
			else{
				FillMain(DateTime.Today.AddDays(-45),DateTime.Today,true,false);
			}
		}

		private void FillAcctLineAL(DateTime fromDate, DateTime toDate,bool includeClaims,bool subtotalsOnly){
			//Patient tempPat = new Patient();
			//tempPat.ChartNum=thisApt.ChartNum;
			//Patients.Cur=tempPat;
			Procedures.Refresh();
			Claims.Refresh();
			Adjustments.Refresh();
			PaySplits.Refresh();
			ClaimProcs.Refresh();
			Commlogs.Refresh();
			PayPlans.Refresh();
			//Ledgers2.Refresh(Patients.Cur.PatNum);//the only reason for the refresh is to compute patient bal.
			//ledgers does not have to be refreshed as often if computebalances is done less.
			Shared.ComputeBalances();//use whenever a change would affect the total
//fix: too much network traffic
			Ledgers.ComputeAging(Patients.Cur.Guarantor);
			Patients.GetFamily(Patients.Cur.PatNum);
			arrayProc = new Procedure[Procedures.List.Length];
			arrayClaim=new Claim[Claims.List.Length];
			arrayAdj = new Adjustment[Adjustments.List.Length];
			arrayPay =new PaySplit[PaySplits.List.Length];
			arrayComm =new Commlog[Commlogs.List.Length];
			arrayPayPlan=new PayPlan[PayPlans.List.Length];
			//step through all procedures for patient and move selected ones (completed) to
			//arrayProc, also arrayClaim, arrayAdj ,arrayPay, all ordered by date.
			//Pull from all 4 into AcctLineAL array for display.  Every AcctLineAL entry
			//contains type and index to access original array. Notes are handled like any
			//other line, just no numbers.(actually no notes yet)
			int countProc=0;
			int countClaim=0;
			int countAdj=0;
			int countPay=0;
			int countComm=0;
			int countPayPlan=0;
			for(int i=0;i<Procedures.List.Length;i++){
				if(Procedures.List[i].ProcStatus==ProcStat.C){//Only add if proc is Complete
					arrayProc[countProc]=Procedures.List[i];
					countProc++;
				}
			}
			for(int i=0;i<Claims.List.Length;i++){
				if(Claims.List[i].ClaimStatus!="A"//don't show ins adjustments
					&& Claims.List[i].ClaimType!="PreAuth"){//don't show preauthorizations.
					arrayClaim[countClaim]=Claims.List[i];
					countClaim++;
				}
			}
			for(int i=0;i<Adjustments.List.Length;i++){
				arrayAdj[countAdj]=Adjustments.List[i];
				countAdj++;
			}
			for(int i=0;i<PaySplits.List.Length;i++){
				arrayPay[countPay]=PaySplits.List[i];
				countPay++;
			}
			for(int i=0;i<Commlogs.List.Length;i++){
				if(Commlogs.List[i].CommType==CommItemType.StatementSent){//only show statementSents.
					arrayComm[countComm]=Commlogs.List[i];
					countComm++;
				}
			}
			for(int i=0;i<PayPlans.List.Length;i++){
				arrayPayPlan[countPayPlan]=PayPlans.List[i];
				countPayPlan++;
			}
			int tempCountProc=0;
			int tempCountClaim=0;
			int tempCountAdj=0;
			int tempCountPay=0;
			int tempCountComm=0;
			int tempCountPayPlan=0;
			AcctLineAL=new ArrayList();
			AcctLine tempAcctLine=new AcctLine();
			//This is where to transfer arrays to AcctLineAL:
			DateTime lineDate=DateTime.MinValue;
				//tempAcctLine.Description="Starting Balance";
			double runBal=0;
				//tempAcctLine.Balance=runBal.ToString("F");
				//AcctLineAL.Add(tempAcctLine);
			for(int j=0;j<countProc+countClaim+countAdj+countPay+countComm+countPayPlan+1;j++){
			//for(int i=0;i<AcctLineAL.Length;i++){
				//set lineDate to the value of the first array that is not maxed out:
				if     (tempCountProc<countProc) lineDate=arrayProc[tempCountProc].ProcDate;
				else if(tempCountClaim<countClaim) lineDate=arrayClaim[tempCountClaim].DateService;
				else if(tempCountAdj<countAdj) lineDate=arrayAdj[tempCountAdj].AdjDate;
				else if(tempCountPay<countPay) lineDate=arrayPay[tempCountPay].ProcDate;
				else if(tempCountComm<countComm) lineDate=arrayComm[tempCountComm].CommDate;
				else if(tempCountPayPlan<countPayPlan) lineDate=arrayPayPlan[tempCountPayPlan].PayPlanDate;
				//find next date
				if(tempCountProc<countProc && DateTime.Compare(arrayProc[tempCountProc].ProcDate,lineDate)<=0)
					lineDate=arrayProc[tempCountProc].ProcDate;
				if(tempCountClaim<countClaim && DateTime.Compare(arrayClaim[tempCountClaim].DateService,lineDate)<0)
					lineDate=arrayClaim[tempCountClaim].DateService;
				if(tempCountAdj<countAdj && DateTime.Compare(arrayAdj[tempCountAdj].AdjDate,lineDate)<0)
					lineDate=arrayAdj[tempCountAdj].AdjDate;
				if(tempCountPay<countPay 
					&& DateTime.Compare(arrayPay[tempCountPay].ProcDate,lineDate)<=0)
					lineDate=arrayPay[tempCountPay].ProcDate;																												if(tempCountComm<countComm 																																					&& DateTime.Compare(arrayComm[tempCountComm].CommDate,lineDate)<=0)
					lineDate=arrayComm[tempCountComm].CommDate;
				if(tempCountPayPlan<countPayPlan
					&& DateTime.Compare(arrayPayPlan[tempCountPayPlan].PayPlanDate,lineDate)<=0)
					lineDate=arrayPayPlan[tempCountPayPlan].PayPlanDate;
				//1. Procedure
				if(tempCountProc<countProc && DateTime.Compare(arrayProc[tempCountProc].ProcDate,lineDate)==0){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.Proc;
					tempAcctLine.Index=tempCountProc;
					tempAcctLine.Date=arrayProc[tempCountProc].ProcDate.ToString("d");
					tempAcctLine.Provider=Providers.GetAbbr(arrayProc[tempCountProc].ProvNum);
					tempAcctLine.Code=arrayProc[tempCountProc].ADACode;
					tempAcctLine.Tooth=Tooth.ToInternat(arrayProc[tempCountProc].ToothNum);
					tempAcctLine.Description=ProcedureCodes.GetProcCode(arrayProc[tempCountProc].ADACode).Descript;
					double fee=arrayProc[tempCountProc].ProcFee;
					Procedures.Cur=arrayProc[tempCountProc];
					double insEst=Procedures.GetEstForCur(PriSecTot.Tot);
					double pat=0;
					tempAcctLine.Fee=fee.ToString("F");
					//tempAcctLine.InsEst=insEst.ToString("F");
					if(!arrayProc[tempCountProc].IsCovIns){//not covered by ins
						tempAcctLine.InsEst="";
						tempAcctLine.InsPay="";
						pat=fee;
						tempAcctLine.Patient=pat.ToString("F");
					}
					else if(arrayProc[tempCountProc].CapCoPay!=-1){//covered by capitation
						tempAcctLine.InsEst="";
						tempAcctLine.InsPay="";
						pat=arrayProc[tempCountProc].CapCoPay;
						tempAcctLine.Patient=pat.ToString("F");
					}
					else if(arrayProc[tempCountProc].NoBillIns){//should not bill to ins
						tempAcctLine.InsEst="";
						tempAcctLine.InsPay="No Bill";
						pat=fee;
						tempAcctLine.Patient=pat.ToString("F");
					}
					else if(!ClaimProcs.ProcIsAttached(arrayProc[tempCountProc].ProcNum)){//not attached to claim
						tempAcctLine.InsEst=insEst.ToString("F");
						tempAcctLine.InsPay="Unsent";
						pat=fee;
						tempAcctLine.Patient=pat.ToString("F");
					}
					else{//attached to claim
						insEst=ClaimProcs.ProcInsEst(arrayProc[tempCountProc].ProcNum);
						tempAcctLine.InsEst=insEst.ToString("F");
						double insPay=ClaimProcs.ProcInsPay(arrayProc[tempCountProc].ProcNum);
						if(ClaimProcs.ProcIsReceived(arrayProc[tempCountProc].ProcNum)){
							tempAcctLine.InsPay=insPay.ToString("F");
							pat=fee-insPay;
						}
						else{//not received, or only received on some claims and not others
							tempAcctLine.InsPay=insPay.ToString("F");
							pat=fee-insPay
								-ClaimProcs.ProcEstNotReceived(arrayProc[tempCountProc].ProcNum);
						}
						tempAcctLine.Patient=pat.ToString("F");
					}
					if(arrayProc[tempCountProc].ProcDate >= fromDate 
						&& arrayProc[tempCountProc].ProcDate <= toDate){//within date range
						runBal+=pat;
						tempAcctLine.Balance=runBal.ToString("F");
						AcctLineAL.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=pat;//add to the running balance, but do not display it.
					}
					if(tempCountProc<countProc) tempCountProc++;
				}//end Proc
				//2. Claim
				else if(tempCountClaim<countClaim && DateTime.Compare(arrayClaim[tempCountClaim].DateService,lineDate)==0){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.Claim;
					tempAcctLine.Index=tempCountClaim;
					tempAcctLine.Date=arrayClaim[tempCountClaim].DateService.ToString("d");
					tempAcctLine.Provider=Providers.GetAbbr(arrayClaim[tempCountClaim].ProvTreat);
					tempAcctLine.Code="Ins";
					tempAcctLine.Tooth="";
					if(arrayClaim[tempCountClaim].ClaimType=="P"){
						tempAcctLine.Description="Pri Claim: ";
					}
					else if(arrayClaim[tempCountClaim].ClaimType=="S"){
						tempAcctLine.Description="Sec Claim: ";
					}
					tempAcctLine.Description+=InsPlans.GetCarrierName(arrayClaim[tempCountClaim].PlanNum);
					if(arrayClaim[tempCountClaim].DedApplied>0){
						tempAcctLine.Description+=". Ded applied $"+arrayClaim[tempCountClaim].DedApplied.ToString("F");
					}
					double fee;
					double insEst;
					double insPay;
					double subTotal=0;
					Claims.Cur=arrayClaim[tempCountClaim];
					fee=Claims.Cur.ClaimFee;
					insEst=Claims.Cur.InsPayEst;
					//insPay=Claims.Cur.InsPayAmt;
					tempAcctLine.Fee=fee.ToString("F");
					tempAcctLine.InsEst=insEst.ToString("F");
					//insPay is always subtracted from bal no matter what is displayed.
					insPay=ClaimProcs.ClaimByTotalOnly(arrayClaim[tempCountClaim].ClaimNum);
					subTotal-=insPay;
					if(arrayClaim[tempCountClaim].ClaimStatus=="R"){//if claim is received
						//tempAcctLine.InsPay=insPay.ToString("F");//show the insurance payment
						//show the insurance payment. Just for display.
						//The byTotal is the only number that affects the balance.
						tempAcctLine.InsPay=Claims.Cur.InsPayAmt.ToString("F");
						//runBal-=insPay;
					}
					else{//claim not received, so it is an estimate
						switch(arrayClaim[tempCountClaim].ClaimStatus){
							case "U":
								tempAcctLine.InsPay="Unsent";
								break;
							case "H":
								tempAcctLine.InsPay="Hold";
								break;
							case "W":
								tempAcctLine.InsPay="In Queue";
								break;
							case "P":
								tempAcctLine.InsPay="In Queue";
								break;
							case "S":
								tempAcctLine.InsPay="Sent";
								break;
						}
						//subTotal-=insEst;
						FamInsEst+=insEst;//for printing family
					}
					//runBal-=arrayClaim[tempCountClaim].WriteOff;
					if(arrayClaim[tempCountClaim].WriteOff>0){
						tempAcctLine.Adj="-"+arrayClaim[tempCountClaim].WriteOff.ToString("F");
						subTotal-=arrayClaim[tempCountClaim].WriteOff;
					}
					tempAcctLine.Patient="";
					if(arrayClaim[tempCountClaim].DateService >= fromDate
						&& arrayClaim[tempCountClaim].DateService <= toDate){//within date range
						runBal+=subTotal;
						tempAcctLine.Balance=runBal.ToString("F");
						AcctLineAL.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=subTotal;//add to the running balance, but do not display it.
					}
					//old claims that have been received only recently, or not at all:
					else if(includeClaims && arrayClaim[tempCountClaim].ClaimStatus != "R"){
						tempAcctLine.Balance="";//don't show running balance
						AcctLineAL.Add(tempAcctLine);
					}
					if(tempCountClaim<countClaim) tempCountClaim++;
				}//end Claim
				//3. Adjustment
				else if(tempCountAdj<countAdj && DateTime.Compare(arrayAdj[tempCountAdj].AdjDate,lineDate)==0){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.Adj;
					tempAcctLine.Index=tempCountAdj;
					tempAcctLine.Date=arrayAdj[tempCountAdj].AdjDate.ToString("d");
					tempAcctLine.Provider=Providers.GetAbbr(arrayAdj[tempCountAdj].ProvNum);
					tempAcctLine.Code="Adjust";
					tempAcctLine.Tooth="";
					tempAcctLine.Description=Defs.GetName(DefCat.AdjTypes,arrayAdj[tempCountAdj].AdjType);
					tempAcctLine.Fee="";
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					//can be a positive or negative number:
					tempAcctLine.Adj=arrayAdj[tempCountAdj].AdjAmt.ToString("F");
					if(arrayAdj[tempCountAdj].AdjDate >= fromDate
						&& arrayAdj[tempCountAdj].AdjDate <= toDate){
						runBal+=arrayAdj[tempCountAdj].AdjAmt;
						tempAcctLine.Balance=runBal.ToString("F");
						AcctLineAL.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=arrayAdj[tempCountAdj].AdjAmt;//add to the running balance, but do not display it.
					}
					if(tempCountAdj<countAdj) tempCountAdj++;
				}//end Adjustment
				//4. Payment or Discount:
				else if(tempCountPay<countPay && DateTime.Compare(arrayPay[tempCountPay].ProcDate,lineDate)==0){
					tempAcctLine=new AcctLine();
					if(arrayPay[tempCountPay].IsDiscount){
						tempAcctLine.Type=AcctType.Disc;
						tempAcctLine.Code="Disc't";
						tempAcctLine.Description=Defs.GetName(DefCat.DiscountTypes,arrayPay[tempCountPay].DiscountType);
					}
					else{
						tempAcctLine.Type=AcctType.Pay;
						tempAcctLine.Code="Pay";
						tempAcctLine.Description=Payments.GetInfo(arrayPay[tempCountPay].PayNum);
					}
					tempAcctLine.Index=tempCountPay;
					tempAcctLine.Date=arrayPay[tempCountPay].ProcDate.ToString("d");
					tempAcctLine.Provider="";//since payments are usually split, leave empty
					tempAcctLine.Tooth="";
					tempAcctLine.Fee="";
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					tempAcctLine.Patient="";
					if(arrayPay[tempCountPay].IsDiscount){
						tempAcctLine.Adj="-"+arrayPay[tempCountPay].SplitAmt.ToString("F");
						tempAcctLine.Paid="";
					}
					else{
						tempAcctLine.Adj="";
						tempAcctLine.Paid="-"+arrayPay[tempCountPay].SplitAmt.ToString("F");
					}
					if(arrayPay[tempCountPay].ProcDate >= fromDate
						&& arrayPay[tempCountPay].ProcDate <= toDate){
						runBal-=arrayPay[tempCountPay].SplitAmt;
						tempAcctLine.Balance=runBal.ToString("F");
						AcctLineAL.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal-=arrayPay[tempCountPay].SplitAmt;//add to the running balance, but do not display it.
					}
					if(tempCountPay<countPay) tempCountPay++;
				}//end Payment
				//5. Comm:
				else if(tempCountComm<countComm && arrayComm[tempCountComm].CommDate==lineDate){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.Comm;
					tempAcctLine.Code="Comm";
					tempAcctLine.Description="Sent Statement";
					tempAcctLine.Index=tempCountComm;
					tempAcctLine.Date=arrayComm[tempCountComm].CommDate.ToString("d");
					tempAcctLine.Provider="";
					tempAcctLine.Tooth="";
					tempAcctLine.Fee="";
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					tempAcctLine.Patient="";
					//adj
					//paid
					tempAcctLine.Balance="";
					if(arrayComm[tempCountComm].CommDate >= fromDate
						&& arrayComm[tempCountComm].CommDate <= toDate){
						AcctLineAL.Add(tempAcctLine);
					}
					if(tempCountComm<countComm) tempCountComm++;
				}//end Comm
				//6. PayPlan:
				else if(tempCountPayPlan<countPayPlan 
					&& DateTime.Compare(arrayPayPlan[tempCountPayPlan].PayPlanDate,lineDate)==0)
				{
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.PayPlan;
					tempAcctLine.Code="PayPlan";
					tempAcctLine.Description="Payment Plan";
					tempAcctLine.Index=tempCountPayPlan;
					tempAcctLine.Date=arrayPayPlan[tempCountPayPlan].PayPlanDate.ToShortDateString();
					tempAcctLine.Provider="";
					tempAcctLine.Tooth="";
					tempAcctLine.Fee=arrayPayPlan[tempCountPayPlan].TotalAmount.ToString("F");
					tempAcctLine.InsEst="";
					tempAcctLine.InsPay="";
					double subTotal=0;
					//either or both of these conditions might be true (3 possible scenarios)
					//1.If this is guarantor
					if(arrayPayPlan[tempCountPayPlan].Guarantor==Patients.Cur.PatNum){
						tempAcctLine.Patient=arrayPayPlan[tempCountPayPlan].CurrentDue.ToString("F");
						//runBal+=arrayPayPlan[tempCountPayPlan].CurrentDue;
						subTotal+=arrayPayPlan[tempCountPayPlan].CurrentDue;
					}
					//2.If this is the patient
					if(arrayPayPlan[tempCountPayPlan].PatNum==Patients.Cur.PatNum){
						//runBal-=arrayPayPlan[tempCountPayPlan].TotalAmount;
						subTotal-=arrayPayPlan[tempCountPayPlan].TotalAmount;
					}
					
					if((arrayPayPlan[tempCountPayPlan].PayPlanDate >= fromDate
						&& arrayPayPlan[tempCountPayPlan].PayPlanDate <= toDate)
						|| arrayPayPlan[tempCountPayPlan].CurrentDue>0)
					{
						runBal+=subTotal;
						tempAcctLine.Balance=runBal.ToString("F");
						AcctLineAL.Add(tempAcctLine);
					}
					else if(!subtotalsOnly){//out of date range, but show normal totals
						runBal+=subTotal;//add to the running balance, but do not display it.
					}
					if(tempCountPayPlan<countPayPlan) tempCountPayPlan++;
				}//end PayPlan
			}//end for line
			SubTotal=runBal;
			//for (int i=0;i<countProc;i++){
			//}//end for i countProc
		}//end FillAcctLineAL

		private void FilltbAccount(){
			DateTime lineDate=DateTime.MinValue;
			tbAccount.ResetRows(AcctLineAL.Count);
				for(int i=0;i<AcctLineAL.Count;i++){
					//if (AcctLineAL[i].IsProc==true){
						tbAccount.SetNormRow(i);
						try{//error catch bad dates
							if(DateTime.Compare(DateTime.Parse(((AcctLine)AcctLineAL[i]).Date),lineDate)>0){
								tbAccount.Cell[0,i]=((AcctLine)AcctLineAL[i]).Date;
								tbAccount.SetTopRow(i);
								lineDate=DateTime.Parse(((AcctLine)AcctLineAL[i]).Date);
							}
							else tbAccount.Cell[0,i]="";
							}
						catch{
							tbAccount.Cell[0,i]="";
						}
						tbAccount.Cell[1,i]=((AcctLine)AcctLineAL[i]).Provider;
						tbAccount.Cell[2,i]=((AcctLine)AcctLineAL[i]).Code;
						tbAccount.Cell[3,i]=((AcctLine)AcctLineAL[i]).Tooth;
						tbAccount.Cell[4,i]=((AcctLine)AcctLineAL[i]).Description;
						tbAccount.Cell[5,i]=((AcctLine)AcctLineAL[i]).Fee;
						tbAccount.Cell[6,i]=((AcctLine)AcctLineAL[i]).InsEst;
						tbAccount.Cell[7,i]=((AcctLine)AcctLineAL[i]).InsPay;
						tbAccount.Cell[8,i]=((AcctLine)AcctLineAL[i]).Patient;
						tbAccount.Cell[9,i]=((AcctLine)AcctLineAL[i]).Adj;
						tbAccount.Cell[10,i]=((AcctLine)AcctLineAL[i]).Paid;
						tbAccount.Cell[11,i]=((AcctLine)AcctLineAL[i]).Balance;
					//}//end if
					switch(((AcctLine)AcctLineAL[i]).Type){
						default:
							tbAccount.SetTextColorRow
								(i,Defs.Long[(int)DefCat.AccountColors][0].ItemColor);
							break;
						case AcctType.Claim:
							tbAccount.SetTextColorRow
								(i,Defs.Long[(int)DefCat.AccountColors][4].ItemColor);
							break;
						case AcctType.Adj:
							tbAccount.SetTextColorRow
								(i,Defs.Long[(int)DefCat.AccountColors][1].ItemColor);
							break;
						case AcctType.Disc:
							tbAccount.SetTextColorRow
								(i,Defs.Long[(int)DefCat.AccountColors][2].ItemColor);
							break;
						case AcctType.Pay:
							tbAccount.SetTextColorRow
								(i,Defs.Long[(int)DefCat.AccountColors][3].ItemColor);
							break;
						case AcctType.Comm:
							tbAccount.SetTextColorRow
								(i,Defs.Long[(int)DefCat.AccountColors][5].ItemColor);
							break;
						case AcctType.PayPlan:
							tbAccount.SetTextColorRow
								(i,Defs.Long[(int)DefCat.AccountColors][6].ItemColor);
							break;
					}
					//else{//else note
					//	if(AcctLineAL[i-1].IsProc==true)
					//		tbAccount.SetFirstNoteRow(i);
					//	else
					//		tbAccount.SetNoteRow(i);
					//	tbAccount.Cell[2,i]=AcctLineAL[i].Note;
					//}//end else note
				}//end for
				tbAccount.LayoutTables();
		}//end FilltbAccount

		private void FillPats(){
			if(!Patients.PatIsLoaded){
				tbAcctPat.ResetRows(0);
				tbAcctPat.LayoutTables();
				return;
			}
			//double total=0;
			tbAcctPat.ResetRows(Patients.FamilyList.Length);
			for(int i=0;i<Patients.FamilyList.Length;i++){
				tbAcctPat.Cell[0,i]=Patients.GetNameInFamLFI(i);
				tbAcctPat.Cell[1,i]=Patients.FamilyList[i].EstBalance.ToString("F");
				if(i==Patients.GuarIndex){
					tbAcctPat.FontBold[0,i]=true;
					tbAcctPat.FontBold[1,i]=true;
				}
				//total=total+Patients.FamilyList[i].EstBalance;
				if(Patients.FamilyList[i].PatNum==Patients.Cur.PatNum){
					tbAcctPat.ColorRow(i,Color.DarkSalmon);
					tbAcctPat.SelectedRow=i;
				}
			}
			tbAcctPat.LayoutTables();
		}

		private void tbAccount_CellClicked(object sender, CellEventArgs e){
			if(ViewOnly) return;
			switch (((AcctLine)AcctLineAL[e.Row]).Type){
				default://procedure
					break;
				case AcctType.Claim:
					Claims.Cur=arrayClaim[((AcctLine)AcctLineAL[e.Row]).Index];
					for(int i=0;i<AcctLineAL.Count;i++){//loop through all rows
						if(((AcctLine)AcctLineAL[i]).Type!=AcctType.Proc)
							//if not a procedure, then skip
							continue;
						for(int j=0;j<ClaimProcs.List.Length;j++){//loop through all claimprocs
							//if there is a claim proc for this procedure that matches
							if(arrayProc[((AcctLine)AcctLineAL[i]).Index].ProcNum==ClaimProcs.List[j].ProcNum
								&& ClaimProcs.List[j].ClaimNum==Claims.Cur.ClaimNum)
							{
								tbAccount.SetSelected(i,true);
							}
						}
					}
					break;
				case AcctType.Adj:
					//Adjustments.Cur=arrayAdj[((AcctLine)AcctLineAL[e.Row]).Index];
					break;
				case AcctType.Disc:
					//Payments.SetCur(arrayPay[((AcctLine)AcctLineAL[e.Row]).Index].PayNum);
					break;
				case AcctType.Pay:
					//Payments.SetCur(arrayPay[((AcctLine)AcctLineAL[e.Row]).Index].PayNum);
					break;
				case AcctType.Comm:
					//Commlogs.Cur=arrayComm[((AcctLine)AcctLineAL[e.Row]).Index];
					break;				
				case AcctType.PayPlan:
					PayPlans.Cur=arrayPayPlan[((AcctLine)AcctLineAL[e.Row]).Index];
					//ArrayList payPlanRows=new ArrayList();
					for(int i=0;i<AcctLineAL.Count;i++){
						if(((AcctLine)AcctLineAL[i]).Type==AcctType.Pay
							&& arrayPay[((AcctLine)AcctLineAL[i]).Index].PayPlanNum==PayPlans.Cur.PayPlanNum)
						{
							tbAccount.SetSelected(i,true);
						}
					}
					break;	
			}//end switch
		}

		private void tbAccount_CellDoubleClicked(object sender, CellEventArgs e){
			if(ViewOnly) return;
			switch (((AcctLine)AcctLineAL[e.Row]).Type){
				default:
					Procedures.Cur=arrayProc[((AcctLine)AcctLineAL[e.Row]).Index];
					FormProcEdit FormPE = new FormProcEdit();
					FormPE.IsNew=false;
					FormPE.ShowDialog();
					break;
				case AcctType.Claim:
					Claims.Cur=arrayClaim[((AcctLine)AcctLineAL[e.Row]).Index];
					FormClaimEdit FormClaimEdit2=new FormClaimEdit();
					FormClaimEdit2.IsNew=false;
					FormClaimEdit2.ShowDialog();
					break;
				case AcctType.Adj:
					Adjustments.Cur=arrayAdj[((AcctLine)AcctLineAL[e.Row]).Index];
					FormAdjust2 = new FormAdjust();
					FormAdjust2.IsNew=false;
					FormAdjust2.ShowDialog();
					break;
				case AcctType.Disc:
					Payments.SetCur(arrayPay[((AcctLine)AcctLineAL[e.Row]).Index].PayNum);
					FormPayment2=new FormPayment();
					FormPayment2.IsNew=false;
					FormPayment2.ShowDialog();
					break;
				case AcctType.Pay:
					Payments.SetCur(arrayPay[((AcctLine)AcctLineAL[e.Row]).Index].PayNum);
					FormPayment2=new FormPayment();
					FormPayment2.IsNew=false;
					FormPayment2.ShowDialog();
					break;
				case AcctType.Comm:
					Commlogs.Cur=arrayComm[((AcctLine)AcctLineAL[e.Row]).Index];
					FormCommItem2=new FormCommItem();
					FormCommItem2.IsNew=false;
					FormCommItem2.ShowDialog();
					break;				
				case AcctType.PayPlan:
					PayPlans.Cur=arrayPayPlan[((AcctLine)AcctLineAL[e.Row]).Index];
					FormPayPlan2=new FormPayPlan();
					FormPayPlan2.IsNew=false;
					FormPayPlan2.ShowDialog();
					if(FormPayPlan2.GotoPatNum!=0)
						Patients.Cur.PatNum=FormPayPlan2.GotoPatNum;//switches to other patient.
					break;	
			}//end switch
			//Shared.ComputeBalances();//use whenever a change would affect the total
			ModuleSelected();
		}

		private void tbAcctPat_CellClicked(object sender, CellEventArgs e){
			if(ViewOnly) return;
			tbAcctPat.ColorRow(tbAcctPat.SelectedRow,Color.White);
			tbAcctPat.SelectedRow=e.Row;
			tbAcctPat.ColorRow(e.Row,Color.DarkSalmon);
			Patients.Cur=Patients.FamilyList[e.Row];
			ModuleSelected();
		}		

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "Patient":
						OnPat_Click();
						break;
					case "Payment":
						OnPay_Click();
						break;
					case "Adjustment":
						OnAdj_Click();
						break;
					case "Insurance":
						OnIns_Click();
						break;
					case "PayPlan":
						OnPayPlan_Click();
						break;
					case "Statement":
						OnStatement_Click();
						break;
				}
			}
			else if(e.Button.Tag.GetType()==typeof(int)){
				Programs.Execute((int)e.Button.Tag);
			}
		}

		private void OnPat_Click() {
			FormPatientSelect FormPS = new FormPatientSelect();
			FormPS.ShowDialog();
			if (FormPS.DialogResult == DialogResult.OK){
				ModuleSelected();
			}
		}

		private void OnPay_Click() {
			FormPayment FormPayment2=new FormPayment();
			FormPayment2.IsNew=true;
			FormPayment2.ShowDialog();
			//Shared.ComputeBalances();
			ModuleSelected();
		}

		private void OnAdj_Click() {
			FormAdjust FormAdjust2=new FormAdjust();
			FormAdjust2.IsNew=true;
			FormAdjust2.ShowDialog();
			//Shared.ComputeBalances();
			ModuleSelected();
		}

		private void OnIns_Click() {
			if(Patients.Cur.PriPlanNum==0){
				MessageBox.Show(Lan.g(this,"Patient does not have insurance."));
				return;
			}
			if(tbAccount.SelectedIndices.Length==0){
				//autoselect procedures
				for(int i=0;i<AcctLineAL.Count;i++){//loop through every line showing on screen
					if(((AcctLine)AcctLineAL[i]).Type!=AcctType.Proc){
						continue;//ignore non-procedures
					}
					if(!ClaimProcs.ProcIsAttached(arrayProc[((AcctLine)AcctLineAL[i]).Index].ProcNum)//not attached	
						&& !arrayProc[((AcctLine)AcctLineAL[i]).Index].NoBillIns//&& not marked NoBillIns
						&& arrayProc[((AcctLine)AcctLineAL[i]).Index].IsCovIns//&& covered by ins
						&& arrayProc[((AcctLine)AcctLineAL[i]).Index].CapCoPay==-1//&& not a capitation proc
						//&& tbAccount.SelectedIndices.Length <= 10//&& not more than 10 already selected
						){
						tbAccount.SetSelected(i,true);
					}
				}
				if(tbAccount.SelectedIndices.Length==0){//if still none selected
					MessageBox.Show(Lan.g(this,"Please select procedures first."));
					return;
				}
			}
			//if(tbAccount.SelectedIndices.Length > 10){
			//	MessageBox.Show(Lan.g(this,"Maximum ten procedures per claim."));
			//	return;
			//}
			bool allAreProcedures=true;
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				if(((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Type!=AcctType.Proc){
					allAreProcedures=false;
				}
			}
			if(!allAreProcedures){
				MessageBox.Show(Lan.g(this,"You can only select procedures."));
				return;
			}
			bool noBillIns=false;
			DateTime procDate=arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[0]]).Index].ProcDate;
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				if(arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Index].NoBillIns){
					noBillIns=true;
				}
			}
			if(noBillIns){
				MessageBox.Show(Lan.g(this,"You can not send procedures that are marked 'do not bill to ins'."));
				return;
			}
			bool isAttached=false;
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				if(ClaimProcs.ProcIsAttached
					(arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Index].ProcNum)){
					isAttached=true;
				}
			}
			if(isAttached){
				MessageBox.Show(Lan.g(this,"One or more items have already been sent to insurance. Select existing claim to reprint."));				
				return;	// removed choice (OKCancel) to disallow creating second claim. SPK 4/30/04				
				//if(MessageBox.Show(Lan.g(this,"One or more items have already been sent to insurance. Create claim anyway?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				//	return;
				//}
			}
			CreateClaim("P");
			Procedures.Refresh();//because procs might have been changed very slightly in CreateClaim.
			ClaimProcs.Refresh();
			ClaimProcs.GetForClaim();
			Claims.Cur.ClaimStatus="W";
			Claims.Cur.DateSent=DateTime.Today;
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit();
			//this next line is very important.  It fills the claimprocs with fees
			//and updates the claim to the db.
			FormCE.CalculateEstimates();
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK){
				ModuleSelected();
				return;//will have already been deleted
			}
			//Claim priClaim=Claims.Cur;//for use a few lines down to display dialog
			if(Patients.Cur.SecPlanNum>0){
				CreateClaim("S");
				ClaimProcs.Refresh();
				ClaimProcs.GetForClaim();
				Claims.Cur.ClaimStatus="H";
				FormCE.CalculateEstimates();//also updates claim to db.
			}
			//ClaimProcs.Refresh();
			//Claims.Refresh();
			//Claims.Cur=priClaim;
			//FormCE.ShowDialog();
			ModuleSelected();
		}

		private void CreateClaim(string claimType){//claimType=P,S,or Other
			//all validation on procedures selected has already been done.
			//Creates and saves claim, attaching all selected procedures.
			//Does not reload any data.
			//Does not enter fee amounts.
			Claims.Cur=new Claim();
			Claims.Cur.PatNum=Patients.Cur.PatNum;
			Claims.Cur.DateService
				=arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[0]]).Index].ProcDate;
			Claims.InsertCur();//to retreive a key for new Claim.ClaimNum
			//datesent
			Claims.Cur.ClaimStatus="U";
			//datereceived
			switch(claimType){
				case "P":
					Claims.Cur.PlanNum=Patients.Cur.PriPlanNum;
					Claims.Cur.PatRelat=Patients.Cur.PriRelationship;
					Claims.Cur.ClaimType="P";
					Claims.Cur.PlanNum2=Patients.Cur.SecPlanNum;//might be 0 if no sec ins
					Claims.Cur.PatRelat2=Patients.Cur.SecRelationship;
					break;
				case "S":
					Claims.Cur.PlanNum=Patients.Cur.SecPlanNum;
					Claims.Cur.PatRelat=Patients.Cur.SecRelationship;
					Claims.Cur.ClaimType="S";
					Claims.Cur.PlanNum2=Patients.Cur.PriPlanNum;
					Claims.Cur.PatRelat2=Patients.Cur.PriRelationship;
					break;
				case "Other":
					FormInsPlanSelect FormIPS=new FormInsPlanSelect();
					FormIPS.ViewRelat=true;
					FormIPS.ShowDialog();
					if(FormIPS.DialogResult!=DialogResult.OK){
						Claims.DeleteCur();
						return;
					}
					Claims.Cur.PlanNum=InsPlans.Cur.PlanNum;
					Claims.Cur.PatRelat=FormIPS.PatRelat;
					Claims.Cur.ClaimType="Other";
					//plannum2 is not automatically filled in.
					break;
			}
			InsPlans.GetCur(Claims.Cur.PlanNum);
			if(InsPlans.Cur.PlanType=="c"){//if capitation
				Claims.Cur.ClaimType="Cap";
			}
			Claims.Cur.ProvTreat=arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[0]]).Index].ProvNum;
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				if(!Providers.GetIsSec//if not a hygeinist
					(arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Index].ProvNum))
				{
					Claims.Cur.ProvTreat
						=arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Index].ProvNum;
				}
			}
			if(Providers.GetIsSec(Claims.Cur.ProvTreat)){
				Claims.Cur.ProvTreat=Patients.Cur.PriProv;
				//OK if 0, because auto select first in list when open claim
			}
			//claimfee calcs in ClaimEdit
			//inspayest ''
			//inspayamt
			//Claims.Cur.DedApplied=0;//calcs in ClaimEdit.
			//preauthstring, etc, etc
			Claims.Cur.IsProsthesis="N";
	//change later: set ProvBill according to practice defaults
			//Claims.Cur.ProvBill=Claims.Cur.ProvTreat;//OK if zero, because it will get fixed in claim
			Claims.Cur.ProvBill=PIn.PInt(((Pref)Prefs.HList["PracticeDefaultProv"]).ValueString);
			Claims.Cur.EmployRelated=YN.No;
			//attach procedures
			for(int i=0;i<tbAccount.SelectedIndices.Length;i++){
				Procedures.Cur=arrayProc[((AcctLine)AcctLineAL[tbAccount.SelectedIndices[i]]).Index];
				if(!Procedures.Cur.IsCovIns){
					Procedures.Cur.IsCovIns=true;
					Procedures.UpdateCur();
				}
				ClaimProcs.Cur=new ClaimProc();
				ClaimProcs.Cur.ProcNum=Procedures.Cur.ProcNum;
				ClaimProcs.Cur.ClaimNum=Claims.Cur.ClaimNum;
				ClaimProcs.Cur.PatNum=Patients.Cur.PatNum;
				ClaimProcs.Cur.ProvNum=Procedures.Cur.ProvNum;
				//ClaimProcs.Cur.FeeBilled=;//handle in call to FormClaimEdit.CalculateEstimates
				//inspayest ''
				//dedapplied ''
				if(InsPlans.Cur.PlanType=="c")//if capitation
					ClaimProcs.Cur.Status=ClaimProcStatus.Capitation;
				else
					ClaimProcs.Cur.Status=ClaimProcStatus.NotReceived;
				//inspayamt=0
				//remarks
				//claimpaymentnum=0
				ClaimProcs.Cur.PlanNum=Claims.Cur.PlanNum;
				ClaimProcs.Cur.DateCP=Procedures.Cur.ProcDate;
				//writeoff
				if(InsPlans.Cur.UseAltCode 
					&& ((ProcedureCode)ProcedureCodes.HList[Procedures.Cur.ADACode]).AlternateCode1!="")
				{
					ClaimProcs.Cur.CodeSent
						=((ProcedureCode)ProcedureCodes.HList[Procedures.Cur.ADACode]).AlternateCode1;
				}
				else{
					ClaimProcs.Cur.CodeSent=Procedures.Cur.ADACode;
					if(ClaimProcs.Cur.CodeSent.Length>5 && ClaimProcs.Cur.CodeSent.Substring(0,1)=="D"){
						ClaimProcs.Cur.CodeSent=ClaimProcs.Cur.CodeSent.Substring(0,5);
					}
				}
				ClaimProcs.InsertCur();
			}//for proc
			
			
		}

		private void menuInsPri_Click(object sender, System.EventArgs e) {
			if(Patients.Cur.PriPlanNum==0){
				MessageBox.Show(Lan.g(this,"Patient does not have insurance."));
				return;
			}
			if(tbAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			CreateClaim("P");
			Procedures.Refresh();//because procs might have been changed very slightly in CreateClaim.
			ClaimProcs.Refresh();
			ClaimProcs.GetForClaim();
			Claims.Cur.ClaimStatus="W";
			Claims.Cur.DateSent=DateTime.Today;
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit();
			FormCE.CalculateEstimates();//also updates claim to db.
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected();
		}

		private void menuInsSec_Click(object sender, System.EventArgs e) {
			if(Patients.Cur.PriPlanNum==0 || Patients.Cur.SecPlanNum==0){
				MessageBox.Show(Lan.g(this,"Patient does not have insurance."));
				return;
			}
			if(tbAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			CreateClaim("S");
			Procedures.Refresh();//because procs might have been changed very slightly in CreateClaim.
			ClaimProcs.Refresh();
			ClaimProcs.GetForClaim();
			Claims.Cur.ClaimStatus="W";
			Claims.Cur.DateSent=DateTime.Today;
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit();
			FormCE.CalculateEstimates();//also updates claim to db.
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected();
		}

		private void menuInsOther_Click(object sender, System.EventArgs e) {
			if(tbAccount.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select procedures first."));
				return;
			}
			CreateClaim("Other");
			Procedures.Refresh();//because procs might have been changed very slightly in CreateClaim.
			ClaimProcs.Refresh();
			ClaimProcs.GetForClaim();
			//still have not saved some changes to the claim at this point
			FormClaimEdit FormCE=new FormClaimEdit();
			FormCE.CalculateEstimates();//also updates claim to db.
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected();
		}

		private void OnPayPlan_Click() {
			PayPlans.Cur=new PayPlan();
			PayPlans.Cur.PatNum=Patients.Cur.PatNum;
			PayPlans.Cur.Guarantor=Patients.Cur.Guarantor;
			PayPlans.Cur.PayPlanDate=DateTime.Today;
			PayPlans.Cur.DateFirstPay=DateTime.Today;
			PayPlans.Cur.TotalAmount=Patients.Cur.EstBalance;
			FormPayPlan FormPP=new FormPayPlan();
			FormPP.IsNew=true;
			FormPP.ShowDialog();
			if(FormPP.GotoPatNum!=0)
				Patients.Cur.PatNum=FormPP.GotoPatNum;//switches to other patient.
			ModuleSelected();
		}

		private void OnStatement_Click() {
			FormStatementOptions FormSO=new FormStatementOptions();
			if(checkShowAll.Checked){
				FormSO.FromDate=DateTime.MinValue;
			}
			else{
				FormSO.FromDate=DateTime.Today.AddDays(-45);
			}
			FormSO.ShowDialog();
			if(FormSO.DialogResult!=DialogResult.OK){
				return;
			}
			//FillMain(FormSO.FromDate,FormSO.ToDate,FormSO.IncludeClaims,FormSO.SubtotalsOnly);
			PrintStatement(FormSO.PatNums,FormSO.FromDate,FormSO.ToDate,FormSO.IncludeClaims
				,FormSO.SubtotalsOnly);
			ModuleSelected();
		}

		private void textUrgFinNote_TextChanged(object sender, System.EventArgs e) {
			UrgFinNoteChanged=true;
		}

		private void textFinNotes_TextChanged(object sender, System.EventArgs e) {
			FinNoteChanged=true;
		}

		private void textUrgFinNote_Leave(object sender, System.EventArgs e) {
			//need to skip this if selecting another module. Handled in ModuleUnselected due to click event
			if(Patients.FamilyList==null)
				return;
			if(UrgFinNoteChanged){
				Patient tempPat=Patients.Cur;
				Patients.Cur=Patients.FamilyList[Patients.GuarIndex];
				Patients.Cur.FamFinUrgNote=textUrgFinNote.Text;
				Patients.UpdateCur();
				Patients.GetFamily(tempPat.PatNum);
				UrgFinNoteChanged=false;
			}
		}

		private void textFinNotes_Leave(object sender, System.EventArgs e) {
			if(Patients.FamilyList==null)
				return;
			if(FinNoteChanged){
				PatientNotes.Cur.FamFinancial=textFinNotes.Text;
				PatientNotes.UpdateCur();
				FinNoteChanged=false;
			}
		}

		private void checkShowAll_Click(object sender, System.EventArgs e) {
			RefreshModuleScreen();
		}

		///<summary>Used from FormBilling to print one statement for the entire family of the current patient.PatNum which was set ahead of time.</summary>
		public void LoadAndPrint(){
			Patients.GetFamily(Patients.Cur.PatNum);
			//InsPlans.Refresh();
			//CovPats.Refresh();
			//FillMain(DateTime.Today.AddDays(-45),DateTime.Today,true,false);
			int[] patNums=new int[Patients.FamilyList.Length];
			for(int i=0;i<patNums.Length;i++){
				patNums[i]=Patients.FamilyList[i].PatNum;
			}
			PrintStatement(patNums,DateTime.Today.AddDays(-45),DateTime.Today,true,false);
		}

		/// <summary></summary>
		private void PrintStatement(int[] patNums,DateTime fromDate,DateTime toDate,bool includeClaims, bool subtotalsOnly){
			int curPatNum=Patients.Cur.PatNum;
			FamTotBal=0;
			FamInsEst=0;
			FamTotDue=0;
			ArrayList StatementAL=new ArrayList();
			/*
			int[] patNums=new int[Patients.FamilyList.Length];
			for(int i=0;i<Patients.FamilyList.Length;i++){
				patNums[i]=Patients.FamilyList[i].PatNum;
			}*/
			AcctLine tempLine;
			for(int i=0;i<patNums.Length;i++){
				Patients.Cur.PatNum=patNums[i];
				RefreshModuleData();
				FillAcctLineAL(fromDate,toDate,includeClaims,subtotalsOnly);
				FamTotDue+=Patients.Cur.EstBalance;
				tempLine=new AcctLine();
				tempLine.Description=Patients.GetCurNameLF();
				tempLine.StateType="PatName";
				StatementAL.Add(tempLine);//this adds a group heading for one patient.
				StatementAL.AddRange(AcctLineAL);//this adds the detail for one patient
				tempLine=new AcctLine();
				tempLine.Description="";
				tempLine.StateType="PatTotal";
				tempLine.Fee="";
				tempLine.InsEst="";
				tempLine.InsPay="";
				tempLine.Patient="";
				if(subtotalsOnly){
					tempLine.Balance=SubTotal.ToString("n");
				}
				else{
					tempLine.Balance=Patients.Cur.EstBalance.ToString("F");
				}
				//group footer for one patient: If subtotalsOnly, then this will actually be a subtotal
				StatementAL.Add(tempLine);
			}
			tempLine=new AcctLine();
			tempLine.Description="Grand Totals:";
			tempLine.StateType="GrandTotal";
			tempLine.Fee="";//not used
			FamTotBal=FamTotDue+FamInsEst;
			tempLine.InsEst=FamTotBal.ToString("F");
			tempLine.InsPay=FamInsEst.ToString("F");
			tempLine.Patient=FamTotDue.ToString("F");
			//tempLine.Balance="4";
			if(!subtotalsOnly){//does not print totals in page header.
				StatementAL.Add(tempLine);//adds info for the page header
			}
			StatementA=new string[12,StatementAL.Count];
			//now, move the collection of lines into a simple array to print.
			for(int i=0;i<StatementA.GetLength(1);i++){
				try{//error catch bad dates
					StatementA[0,i]=((AcctLine)StatementAL[i]).Date; 
				}
				catch{
					StatementA[0,i]="";
				}
				//StatementA[1,i]=((AcctLine)StatementAL[i]).Provider;
				StatementA[1,i]=((AcctLine)StatementAL[i]).Code;
				StatementA[2,i]=((AcctLine)StatementAL[i]).Tooth;
				StatementA[3,i]=((AcctLine)StatementAL[i]).Description;
				StatementA[4,i]=((AcctLine)StatementAL[i]).Fee;
				StatementA[5,i]=((AcctLine)StatementAL[i]).InsEst;
				StatementA[6,i]=((AcctLine)StatementAL[i]).InsPay;
				if(StatementA[6,i]=="In Queue")
					StatementA[6,i]="Sent";//to keep it simple for the patient
				StatementA[7,i]=((AcctLine)StatementAL[i]).Patient;
				StatementA[8,i]=((AcctLine)StatementAL[i]).Adj;
				StatementA[9,i]=((AcctLine)StatementAL[i]).Paid;
				StatementA[10,i]=((AcctLine)StatementAL[i]).Balance;
				StatementA[11,i]=((AcctLine)StatementAL[i]).StateType;
			}//end for
			FormRpStatement FormST=new FormRpStatement();
			//FormST.ShowDialog();
			FormST.PrintReport(false);
			Patients.Cur.PatNum=curPatNum;
			Commlogs.Cur=new Commlog();
			Commlogs.Cur.CommDate=DateTime.Today;
			Commlogs.Cur.CommType=CommItemType.StatementSent;
			Commlogs.Cur.PatNum=Patients.Cur.PatNum;
			//there is no dialog here because it is just a simple entry
			Commlogs.InsertCur();
		}

		

		

		/*private void butPendingClaims_Click(object sender, System.EventArgs e) {
			int OldPatNum=0;
			if(Patients.PatIsLoaded){
				OldPatNum=Patients.Cur.PatNum;
			}
			FormClaimsPending FormPending=new FormClaimsPending();
			FormPending.ShowDialog();
			if(Patients.PatIsLoaded){
				Patients.Cur.PatNum=OldPatNum;
				ModuleSelected();
			}
			else{
				Patients.Cur=new Patient();
			}
		}*/

	}//end class

	///<summary></summary>
	public struct AcctLine{
		///<summary></summary>
		public AcctType Type;
		//public bool IsProc;
		///<summary></summary>
		public int Index;
		///<summary></summary>
		public string Date;
		///<summary></summary>
		public string Provider;
		///<summary></summary>
		public string Code;
		///<summary></summary>
		public string Tooth;
		///<summary></summary>
		public string Description;
		///<summary></summary>
		public string Fee;
		///<summary></summary>
		public string InsEst;
		///<summary></summary>
		public string InsPay;
		///<summary></summary>
		public string Patient;
		///<summary></summary>
		public string Adj;
		///<summary></summary>
		public string Paid;
		///<summary></summary>
		public string Balance;
		///<summary>only used for statements</summary>
		public string StateType;
	}//end struct AcctLine

}//end namespace
