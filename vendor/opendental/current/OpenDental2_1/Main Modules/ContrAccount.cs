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

namespace OpenDental{

	public class ContrAccount : System.Windows.Forms.UserControl{
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components=null;// Required designer variable.
		private Procedure[] arrayProc;
		private ArrayList AcctLineAL;
		private System.Windows.Forms.Button butPat;
		private OpenDental.TableAccount tbAccount;
		private OpenDental.TableAccountPat tbAcctPat;
		private System.Windows.Forms.Button butPayment;
		private System.Windows.Forms.Button butInsurance;
		private System.Windows.Forms.Button butStatement;
		private System.Windows.Forms.Button butAdjustment;
		private PaySplit[] arrayPay;
		private Adjustment[] arrayAdj;
		private Claim[] arrayClaim;
		private Commlog[] arrayComm;
		//private bool ControlDown;
		public static string[,] StatementA;
		private System.Windows.Forms.TextBox textFinNotes;
		private System.Windows.Forms.Label labelUrgFinNote;
		private System.Windows.Forms.TextBox textUrgFinNote;
		private System.Windows.Forms.Panel panelTotal;
		private System.Windows.Forms.Button butEditUrg;
		private System.Windows.Forms.Button butEditFin;
		public bool ViewOnly=false;
		public System.Windows.Forms.CheckBox checkShowAll;
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
		private System.Windows.Forms.Button butClaimMenu;
		private System.Windows.Forms.MenuItem menuInsOther;
		private System.Windows.Forms.MenuItem menuInsPri;
		private System.Windows.Forms.MenuItem menuInsSec;
		private FormCommItem FormCommItem2;
	
		public ContrAccount(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			tbAccount.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbAccount_CellClicked);
			tbAccount.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbAccount_CellDoubleClicked);
			tbAcctPat.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbAcctPat_CellClicked);
		}

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrAccount));
			this.textFinNotes = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.labelUrgFinNote = new System.Windows.Forms.Label();
			this.textUrgFinNote = new System.Windows.Forms.TextBox();
			this.butPat = new System.Windows.Forms.Button();
			this.butPayment = new System.Windows.Forms.Button();
			this.butInsurance = new System.Windows.Forms.Button();
			this.butStatement = new System.Windows.Forms.Button();
			this.butAdjustment = new System.Windows.Forms.Button();
			this.tbAccount = new OpenDental.TableAccount();
			this.tbAcctPat = new OpenDental.TableAccountPat();
			this.panelTotal = new System.Windows.Forms.Panel();
			this.butEditFin = new System.Windows.Forms.Button();
			this.butEditUrg = new System.Windows.Forms.Button();
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
			this.butClaimMenu = new System.Windows.Forms.Button();
			this.panelTotal.SuspendLayout();
			this.SuspendLayout();
			// 
			// textFinNotes
			// 
			this.textFinNotes.BackColor = System.Drawing.Color.White;
			this.textFinNotes.Location = new System.Drawing.Point(714, 253);
			this.textFinNotes.Multiline = true;
			this.textFinNotes.Name = "textFinNotes";
			this.textFinNotes.ReadOnly = true;
			this.textFinNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textFinNotes.Size = new System.Drawing.Size(198, 473);
			this.textFinNotes.TabIndex = 7;
			this.textFinNotes.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(1, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(158, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Fam Financial Notes";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelUrgFinNote
			// 
			this.labelUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelUrgFinNote.Location = new System.Drawing.Point(714, 33);
			this.labelUrgFinNote.Name = "labelUrgFinNote";
			this.labelUrgFinNote.Size = new System.Drawing.Size(142, 23);
			this.labelUrgFinNote.TabIndex = 10;
			this.labelUrgFinNote.Text = "Fam Urgent Fin Note";
			// 
			// textUrgFinNote
			// 
			this.textUrgFinNote.BackColor = System.Drawing.Color.White;
			this.textUrgFinNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textUrgFinNote.ForeColor = System.Drawing.Color.Red;
			this.textUrgFinNote.Location = new System.Drawing.Point(714, 53);
			this.textUrgFinNote.Multiline = true;
			this.textUrgFinNote.Name = "textUrgFinNote";
			this.textUrgFinNote.ReadOnly = true;
			this.textUrgFinNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textUrgFinNote.Size = new System.Drawing.Size(198, 36);
			this.textUrgFinNote.TabIndex = 11;
			this.textUrgFinNote.Text = "";
			// 
			// butPat
			// 
			this.butPat.Image = ((System.Drawing.Image)(resources.GetObject("butPat.Image")));
			this.butPat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPat.Location = new System.Drawing.Point(2, 2);
			this.butPat.Name = "butPat";
			this.butPat.Size = new System.Drawing.Size(117, 26);
			this.butPat.TabIndex = 15;
			this.butPat.Text = "Select Patient";
			this.butPat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butPat.Click += new System.EventHandler(this.butPat_Click);
			// 
			// butPayment
			// 
			this.butPayment.Image = ((System.Drawing.Image)(resources.GetObject("butPayment.Image")));
			this.butPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPayment.Location = new System.Drawing.Point(171, 2);
			this.butPayment.Name = "butPayment";
			this.butPayment.Size = new System.Drawing.Size(90, 26);
			this.butPayment.TabIndex = 16;
			this.butPayment.Text = "Payment";
			this.butPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butPayment.Click += new System.EventHandler(this.butPayment_Click);
			// 
			// butInsurance
			// 
			this.butInsurance.Image = ((System.Drawing.Image)(resources.GetObject("butInsurance.Image")));
			this.butInsurance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butInsurance.Location = new System.Drawing.Point(363, 2);
			this.butInsurance.Name = "butInsurance";
			this.butInsurance.Size = new System.Drawing.Size(102, 26);
			this.butInsurance.TabIndex = 17;
			this.butInsurance.Text = "New Claim";
			this.butInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butInsurance.Click += new System.EventHandler(this.butInsurance_Click);
			// 
			// butStatement
			// 
			this.butStatement.Image = ((System.Drawing.Image)(resources.GetObject("butStatement.Image")));
			this.butStatement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butStatement.Location = new System.Drawing.Point(519, 2);
			this.butStatement.Name = "butStatement";
			this.butStatement.Size = new System.Drawing.Size(96, 26);
			this.butStatement.TabIndex = 18;
			this.butStatement.Text = "Statement";
			this.butStatement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butStatement.Click += new System.EventHandler(this.butStatement_Click);
			// 
			// butAdjustment
			// 
			this.butAdjustment.Image = ((System.Drawing.Image)(resources.GetObject("butAdjustment.Image")));
			this.butAdjustment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdjustment.Location = new System.Drawing.Point(261, 2);
			this.butAdjustment.Name = "butAdjustment";
			this.butAdjustment.Size = new System.Drawing.Size(102, 26);
			this.butAdjustment.TabIndex = 19;
			this.butAdjustment.Text = "Adjustment";
			this.butAdjustment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butAdjustment.Click += new System.EventHandler(this.butAdjustment_Click);
			// 
			// tbAccount
			// 
			this.tbAccount.BackColor = System.Drawing.SystemColors.Window;
			this.tbAccount.Location = new System.Drawing.Point(0, 65);
			this.tbAccount.Name = "tbAccount";
			this.tbAccount.SelectedIndices = new int[0];
			this.tbAccount.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbAccount.Size = new System.Drawing.Size(712, 655);
			this.tbAccount.TabIndex = 20;
			// 
			// tbAcctPat
			// 
			this.tbAcctPat.BackColor = System.Drawing.SystemColors.Window;
			this.tbAcctPat.Location = new System.Drawing.Point(714, 90);
			this.tbAcctPat.Name = "tbAcctPat";
			this.tbAcctPat.SelectedIndices = new int[0];
			this.tbAcctPat.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbAcctPat.Size = new System.Drawing.Size(199, 143);
			this.tbAcctPat.TabIndex = 21;
			// 
			// panelTotal
			// 
			this.panelTotal.Controls.Add(this.butEditFin);
			this.panelTotal.Controls.Add(this.label1);
			this.panelTotal.Location = new System.Drawing.Point(714, 234);
			this.panelTotal.Name = "panelTotal";
			this.panelTotal.Size = new System.Drawing.Size(198, 21);
			this.panelTotal.TabIndex = 26;
			// 
			// butEditFin
			// 
			this.butEditFin.Location = new System.Drawing.Point(156, 2);
			this.butEditFin.Name = "butEditFin";
			this.butEditFin.Size = new System.Drawing.Size(42, 18);
			this.butEditFin.TabIndex = 28;
			this.butEditFin.Text = "Edit";
			this.butEditFin.Click += new System.EventHandler(this.butEditFin_Click);
			// 
			// butEditUrg
			// 
			this.butEditUrg.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butEditUrg.Location = new System.Drawing.Point(868, 33);
			this.butEditUrg.Name = "butEditUrg";
			this.butEditUrg.Size = new System.Drawing.Size(42, 18);
			this.butEditUrg.TabIndex = 27;
			this.butEditUrg.Text = "Edit";
			this.butEditUrg.Click += new System.EventHandler(this.butEditUrg_Click);
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
			// butClaimMenu
			// 
			this.butClaimMenu.Image = ((System.Drawing.Image)(resources.GetObject("butClaimMenu.Image")));
			this.butClaimMenu.Location = new System.Drawing.Point(466, 2);
			this.butClaimMenu.Name = "butClaimMenu";
			this.butClaimMenu.Size = new System.Drawing.Size(13, 26);
			this.butClaimMenu.TabIndex = 45;
			this.butClaimMenu.Click += new System.EventHandler(this.butClaimMenu_Click);
			// 
			// ContrAccount
			// 
			this.Controls.Add(this.butClaimMenu);
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
			this.Controls.Add(this.butEditUrg);
			this.Controls.Add(this.panelTotal);
			this.Controls.Add(this.tbAcctPat);
			this.Controls.Add(this.tbAccount);
			this.Controls.Add(this.butAdjustment);
			this.Controls.Add(this.butStatement);
			this.Controls.Add(this.butInsurance);
			this.Controls.Add(this.butPayment);
			this.Controls.Add(this.butPat);
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

		public void InstantClasses(){
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.butAdjustment,
				this.butEditFin,
				this.butEditUrg,
				this.butInsurance,
				this.butPat,
				this.butPayment,
				this.butStatement,
				this.label1,
				this.labelUrgFinNote,
				this.panelTotal,
				this.checkShowAll,
			});
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
			tbAcctPat.Location=new Point(714,textUrgFinNote.Location.Y+textUrgFinNote.Height);
			panelTotal.Location=new Point(714,tbAcctPat.Location.Y+tbAcctPat.Height);
			textFinNotes.Location=new Point(714,panelTotal.Location.Y+panelTotal.Height);
			textFinNotes.Height=Height-textFinNotes.Location.Y-1;
		}

		public void ModuleSelected(){
			RefreshModuleData();
			RefreshModuleScreen();
		}

		public void ModuleUnselected(){
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
		}

		private void RefreshModuleData(){
			if (Patients.PatIsLoaded){
				Patients.GetFamily(Patients.Cur.PatNum);
				InsPlans.Refresh();
				CovPats.Refresh();
				PatientNotes.Refresh();
			}
			//other tables are refreshed in the filltable
		}

		private void RefreshModuleScreen(){
			if (Patients.PatIsLoaded){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "+Patients.GetCurNameLF();
				tbAccount.Enabled=true;
				butPayment.Enabled=true;
				butAdjustment.Enabled=true;
				butInsurance.Enabled=true;
				butStatement.Enabled=true;
				butEditUrg.Enabled=true;
				butEditFin.Enabled=true;
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				tbAccount.Enabled=false;
				butPayment.Enabled=false;
				butAdjustment.Enabled=false;
				butInsurance.Enabled=false;
				butStatement.Enabled=false;
				butEditUrg.Enabled=false;
				butEditFin.Enabled=false;
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
				double total=Patients.FamilyList[Patients.GuarIndex].BalOver90
					+Patients.FamilyList[Patients.GuarIndex].Bal_61_90
					+Patients.FamilyList[Patients.GuarIndex].Bal_31_60
					+Patients.FamilyList[Patients.GuarIndex].Bal_0_30;
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
			if (Patients.PatIsLoaded){
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
		}

		private void butPat_Click(object sender, System.EventArgs e) {
			FormPatientSelect FormPS = new FormPatientSelect();
			FormPS.ShowDialog();
			if (FormPS.DialogResult == DialogResult.OK){
				ModuleSelected();
			}
		}

		private void FillMain(){
			//tbAccount.SelectedRowsAL=new ArrayList();
			if(!Patients.PatIsLoaded){
				tbAccount.ResetRows(0);
				tbAccount.LayoutTables();
				return;
			}
			FillAcctLineAL();
			FilltbAccount();
		}

		private void FillAcctLineAL(){
			//Patient tempPat = new Patient();
			//tempPat.ChartNum=thisApt.ChartNum;
			//Patients.Cur=tempPat;
			Procedures.Refresh();
			Claims.Refresh();
			Adjustments.Refresh();
			PaySplits.Refresh();
			ClaimProcs.Refresh();
			Commlogs.Refresh();
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
			for (int i = 0; i<Procedures.List.Length; i++){
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
				arrayComm[countComm]=Commlogs.List[i];
				countComm++;
			}
			int tempCountProc=0;
			int tempCountClaim=0;
			int tempCountAdj=0;
			int tempCountPay=0;
			int tempCountComm=0;
			AcctLineAL=new ArrayList();
			AcctLine tempAcctLine=new AcctLine();
			//This is where to transfer arrays to AcctLineAL:
			DateTime lineDate=DateTime.MinValue;
				//tempAcctLine.Description="Starting Balance";
			double runBal=0;
				//tempAcctLine.Balance=runBal.ToString("F");
				//AcctLineAL.Add(tempAcctLine);
			for(int j=0;j<countProc+countClaim+countAdj+countPay+countComm+1;j++){
			//for(int i=0;i<AcctLineAL.Length;i++){
				//set lineDate to the value of the first array that is not maxed out:
				if(tempCountProc<countProc) lineDate=arrayProc[tempCountProc].ProcDate;
				else if(tempCountClaim<countClaim) lineDate=arrayClaim[tempCountClaim].DateService;
				else if(tempCountAdj<countAdj) lineDate=arrayAdj[tempCountAdj].AdjDate;
				else if(tempCountPay<countPay) lineDate=arrayPay[tempCountPay].ProcDate;
				else if(tempCountComm<countComm) lineDate=arrayComm[tempCountComm].CommDate;
				//find next date
				if(tempCountProc<countProc && DateTime.Compare(arrayProc[tempCountProc].ProcDate,lineDate)<=0)
					lineDate=arrayProc[tempCountProc].ProcDate;
				if(tempCountClaim<countClaim && DateTime.Compare(arrayClaim[tempCountClaim].DateService,lineDate)<0)
					lineDate=arrayClaim[tempCountClaim].DateService;
				if(tempCountAdj<countAdj && DateTime.Compare(arrayAdj[tempCountAdj].AdjDate,lineDate)<0)
					lineDate=arrayAdj[tempCountAdj].AdjDate;
				if(tempCountPay<countPay && DateTime.Compare(arrayPay[tempCountPay].ProcDate,lineDate)<=0)
					lineDate=arrayPay[tempCountPay].ProcDate;																																if(tempCountComm<countComm && DateTime.Compare(arrayComm[tempCountComm].CommDate,lineDate)<=0)
					lineDate=arrayComm[tempCountComm].CommDate;
				//1. Procedure
				if(tempCountProc<countProc && DateTime.Compare(arrayProc[tempCountProc].ProcDate,lineDate)==0){
					tempAcctLine=new AcctLine();
					tempAcctLine.Type=AcctType.Proc;
					tempAcctLine.Index=tempCountProc;
					tempAcctLine.Date=arrayProc[tempCountProc].ProcDate.ToString("d");
					tempAcctLine.Provider=Providers.GetAbbr(arrayProc[tempCountProc].ProvNum);
					tempAcctLine.Code=arrayProc[tempCountProc].ADACode;
					tempAcctLine.Tooth=arrayProc[tempCountProc].ToothNum;
					tempAcctLine.Description=ProcCodes.GetProcCode(arrayProc[tempCountProc].ADACode).Descript;
					double fee=arrayProc[tempCountProc].ProcFee;
					Procedures.Cur=arrayProc[tempCountProc];
					double insEst=Procedures.GetEstForCur(PriSecTot.Tot);
					double pat=0;
					tempAcctLine.Fee=fee.ToString("F");
					tempAcctLine.InsEst=insEst.ToString("F");
					if(!arrayProc[tempCountProc].IsCovIns){//not covered by ins
						tempAcctLine.InsEst="";
						tempAcctLine.InsPay="";
						pat=fee;
						tempAcctLine.Patient=pat.ToString("F");
						runBal+=pat;
					}
					else if(arrayProc[tempCountProc].NoBillIns){//should not bill to ins
						tempAcctLine.InsEst="";
						tempAcctLine.InsPay="No Bill";
						pat=fee;
						tempAcctLine.Patient=pat.ToString("F");
						runBal+=pat;
					}
					else if(!ClaimProcs.ProcIsAttached(arrayProc[tempCountProc].ProcNum)){//not attached to claim
						tempAcctLine.InsEst=insEst.ToString("F");
						tempAcctLine.InsPay="Unsent";
						pat=fee;
						tempAcctLine.Patient=pat.ToString("F");
						runBal+=pat;
					}
					else{//attached to claim
						tempAcctLine.InsEst=insEst.ToString("F");
						tempAcctLine.InsPay="";
						pat=fee;
						tempAcctLine.Patient="";//pat.ToString("F");
						runBal+=pat;
					}
					tempAcctLine.Balance=runBal.ToString("F");
					if(checkShowAll.Checked){
						AcctLineAL.Add(tempAcctLine);
					}
					else if(DateTime.Today.AddDays(-45).CompareTo(arrayProc[tempCountProc].ProcDate)<0){
						AcctLineAL.Add(tempAcctLine);
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
					tempAcctLine.Description+=InsPlans.GetCarrier(arrayClaim[tempCountClaim].PlanNum);
					if(arrayClaim[tempCountClaim].DedApplied>0){
						tempAcctLine.Description+=". Ded applied $"+arrayClaim[tempCountClaim].DedApplied.ToString("F");
					}
					double fee;
					double insEst;
					double insPay;
					Claims.Cur=arrayClaim[tempCountClaim];
					//double pat=0;
					fee=Claims.Cur.ClaimFee;
					insEst=Claims.Cur.InsPayEst;
					insPay=Claims.Cur.InsPayAmt;
					tempAcctLine.Fee=fee.ToString("F");
					tempAcctLine.InsEst=insEst.ToString("F");
					if(arrayClaim[tempCountClaim].ClaimStatus=="R"){
						tempAcctLine.InsPay=insPay.ToString("F");
						//pat=-insPay;
						runBal-=insPay;
					}
					else{
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
						//pat=-insEst;
						runBal-=insEst;
						FamInsEst+=insEst;//for printing family
					}
					runBal-=arrayClaim[tempCountClaim].WriteOff;
					//the following section has been changed in v2.1
					tempAcctLine.Patient="";//pat.ToString("F");
					tempAcctLine.Balance=runBal.ToString("F");
					/*this is the way it used to be:
					  if(arrayClaim[tempCountClaim].ClaimNum==arrayClaim[tempCountClaim].PriClaimNum){//is pri claim
						if(arrayClaim[tempCountClaim].SecClaimNum!=0){//secclaim exists
							tempAcctLine.Patient="";
							tempAcctLine.Balance="";
						}
						else{//no sec claim
							//as original:
							tempAcctLine.Patient=pat.ToString("F");
							runBal+=pat;
							tempAcctLine.Balance=runBal.ToString("F");
						}
					}
					else{//sec claim
						if(((Claim)Claims.HList[Claims.Cur.PriClaimNum]).ClaimStatus=="R")
							pat-=((Claim)Claims.HList[Claims.Cur.PriClaimNum]).InsPayAmt;
						else
							pat-=((Claim)Claims.HList[Claims.Cur.PriClaimNum]).InsPayEst;
						tempAcctLine.Patient=pat.ToString("F");
						runBal+=pat;
						tempAcctLine.Balance=runBal.ToString("F");
					}	*/
					if(checkShowAll.Checked){
						AcctLineAL.Add(tempAcctLine);
					}
					else if(DateTime.Today.AddDays(-45).CompareTo(arrayClaim[tempCountClaim].DateService)<0){
						AcctLineAL.Add(tempAcctLine);
					}
					//old claims that have been received only recently, or not at all:
					else if(DateTime.Today.AddDays(-45).CompareTo(arrayClaim[tempCountClaim].DateReceived)<0
						|| arrayClaim[tempCountClaim].ClaimStatus != "R"){
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
					//if(Defs.GetValue(DefCat.AdjTypes,arrayAdj[tempCountAdj].AdjType)=="+"){
					//can be a positive or negative number:
						tempAcctLine.Patient=arrayAdj[tempCountAdj].AdjAmt.ToString("F");
						runBal+=arrayAdj[tempCountAdj].AdjAmt;
					//}
					//else if(Defs.GetValue(DefCat.AdjTypes,arrayAdj[tempCountAdj].AdjType)=="-"){
					//	tempAcctLine.Patient="-"+arrayAdj[tempCountAdj].AdjAmt.ToString("F");
					//	runBal-=arrayAdj[tempCountAdj].AdjAmt;
					//}
					//else default??
					tempAcctLine.Balance=runBal.ToString("F");
					if(checkShowAll.Checked){
						AcctLineAL.Add(tempAcctLine);
					}
					else if(DateTime.Today.AddDays(-45).CompareTo(arrayAdj[tempCountAdj].AdjDate)<0){
						AcctLineAL.Add(tempAcctLine);
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
					tempAcctLine.Patient="-"+arrayPay[tempCountPay].SplitAmt.ToString("F");
					runBal-=arrayPay[tempCountPay].SplitAmt;
					tempAcctLine.Balance=runBal.ToString("F");
					if(checkShowAll.Checked){
						AcctLineAL.Add(tempAcctLine);
					}
					else if(DateTime.Today.AddDays(-45).CompareTo(arrayPay[tempCountPay].ProcDate)<0){
						AcctLineAL.Add(tempAcctLine);
					}
					if(tempCountPay<countPay) tempCountPay++;
				}//end Payment
				//5. Comm:
				else if(tempCountComm<countComm && DateTime.Compare(arrayComm[tempCountComm].CommDate,lineDate)==0){
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
					tempAcctLine.Balance="";
					if(checkShowAll.Checked){
						AcctLineAL.Add(tempAcctLine);
					}
					else if(DateTime.Today.AddDays(-45).CompareTo(arrayComm[tempCountComm].CommDate)<0){
						AcctLineAL.Add(tempAcctLine);
					}
					if(tempCountComm<countComm) tempCountComm++;
				}//end Comm
			}//end for line
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
						tbAccount.Cell[9,i]=((AcctLine)AcctLineAL[i]).Balance;
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

		private void butPayment_Click(object sender, System.EventArgs e) {
			FormPayment FormPayment2=new FormPayment();
			FormPayment2.IsNew=true;
			FormPayment2.ShowDialog();
			//Shared.ComputeBalances();
			ModuleSelected();
		}

		private void butAdjustment_Click(object sender, System.EventArgs e) {
			FormAdjust FormAdjust2=new FormAdjust();
			FormAdjust2.IsNew=true;
			FormAdjust2.ShowDialog();
			//Shared.ComputeBalances();
			ModuleSelected();
		}

		private void butInsurance_Click(object sender, System.EventArgs e) {
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
						&& tbAccount.SelectedIndices.Length <= 10//&& not more than 10 already selected
						){
						tbAccount.SetSelected(i,true);
					}
				}
				if(tbAccount.SelectedIndices.Length==0){//if still none selected
					MessageBox.Show(Lan.g(this,"Please select procedures first."));
					return;
				}
			}
			if(tbAccount.SelectedIndices.Length > 10){
				MessageBox.Show(Lan.g(this,"Maximum ten procedures per claim."));
				return;
			}
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
				if(MessageBox.Show(Lan.g(this,"One or more items have already been sent to insurance. Create claim anyway?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
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
			Claims.Cur.ProvBill=Claims.Cur.ProvTreat;//OK if zero, because it will get fixed in claim
			Claims.Cur.EmployRelated=YN.No;
			InsPlans.GetCur(Claims.Cur.PlanNum);
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
				ClaimProcs.Cur.Status=ClaimProcStatus.NotReceived;
				//inspayamt=0
				//remarks
				//claimpaymentnum=0
				ClaimProcs.Cur.PlanNum=Claims.Cur.PlanNum;
				ClaimProcs.Cur.DateCP=Procedures.Cur.ProcDate;
				//writeoff
				if(InsPlans.Cur.UseAltCode)
					ClaimProcs.Cur.CodeSent=((ProcedureCode)ProcCodes.HList[Procedures.Cur.ADACode]).AlternateCode1;
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

		private void butClaimMenu_Click(object sender, System.EventArgs e) {
			contextMenuIns.Show(butInsurance,new Point(0,26));
		}
		
		//private void tbAccount_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
		//	if (e.KeyCode==Keys.ControlKey){
		//		ControlDown=true;
		//	}
		//}

		//private void tbAccount_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
		//	if (e.KeyCode==Keys.ControlKey){
		//		ControlDown=false;
		//	}
		//}

		private void butEditUrg_Click(object sender, System.EventArgs e) {
			if(ViewOnly) return;
			FormNoteFinUrg FormNFU=new FormNoteFinUrg();
			FormNFU.ShowDialog();
			FillMisc();
		}

		private void butEditFin_Click(object sender, System.EventArgs e) {
			if(ViewOnly) return;
			FormNoteFinancial FormNF=new FormNoteFinancial();
			FormNF.ShowDialog();
			FillMisc();
		}

		private void checkShowAll_Click(object sender, System.EventArgs e) {
			RefreshModuleScreen();
		}

		private void butStatement_Click(object sender, System.EventArgs e) {
			PrintStatement();
			ModuleSelected();
			//RefreshModuleData();
			//FillAcctLineAL();
		}

		public void LoadAndPrint(){
			//ModuleSelected();
			Patients.GetFamily(Patients.Cur.PatNum);
			InsPlans.Refresh();
			CovPats.Refresh();
			FillMain();
			PrintStatement();
		}

		private void PrintStatement(){
			int curPatNum=Patients.Cur.PatNum;
			FamTotBal=0;
			FamInsEst=0;
			FamTotDue=0;
			ArrayList StatementAL=new ArrayList();
			
			int[] patNums=new int[Patients.FamilyList.Length];
			for(int i=0;i<Patients.FamilyList.Length;i++){
				patNums[i]=Patients.FamilyList[i].PatNum;
			}
			AcctLine tempLine;
			for(int i=0;i<patNums.Length;i++){
				Patients.Cur.PatNum=patNums[i];
				RefreshModuleData();
				FillAcctLineAL();
				FamTotDue+=Patients.Cur.EstBalance;
				tempLine=new AcctLine();
				tempLine.Description=Patients.GetCurNameLF();
				tempLine.StateType="PatName";
				StatementAL.Add(tempLine);
				StatementAL.AddRange(AcctLineAL);
				tempLine=new AcctLine();
				tempLine.Description="";
				tempLine.StateType="PatTotal";
				tempLine.Fee="";
				tempLine.InsEst="";
				tempLine.InsPay="";
				tempLine.Patient="";
				tempLine.Balance=Patients.Cur.EstBalance.ToString("F");
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
			StatementAL.Add(tempLine);
			StatementA=new string[10,StatementAL.Count];
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
				StatementA[8,i]=((AcctLine)StatementAL[i]).Balance;
				StatementA[9,i]=((AcctLine)StatementAL[i]).StateType;
			}//end for
			FormRpStatement FormST=new FormRpStatement();
			//FormST.ShowDialog();
			FormST.PrintReport(false);
			Patients.Cur.PatNum=curPatNum;
			Commlogs.Cur=new Commlog();
			Commlogs.Cur.CommDate=DateTime.Today;
			Commlogs.Cur.CommType=1;
			Commlogs.Cur.PatNum=Patients.Cur.PatNum;
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

	public struct AcctLine{
		public AcctType Type;
		//public bool IsProc;
		public int Index;
		public string Date;
		public string Provider;
		public string Code;
		public string Tooth;
		public string Description;
		public string Fee;
		public string InsEst;
		public string InsPay;
		public string Patient;
		public string Balance;
		public string StateType;//only used for statements
	}//end struct AcctLine

}//end namespace
