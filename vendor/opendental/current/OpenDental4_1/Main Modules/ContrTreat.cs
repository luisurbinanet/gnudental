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
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Printing;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class ContrTreat : System.Windows.Forms.UserControl{
		//private AxFPSpread.AxvaSpread axvaSpread2;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components;// Required designer variable.
		private System.Windows.Forms.ListBox listSetPr;
		//<summary></summary>
		//public static ArrayList TPLines2;
		private System.Windows.Forms.Panel panelSide;
		//private bool[] selectedPrs;//had to use this because of deficiency in available Listbox events.
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private int linesPrinted=0;
		///<summary></summary>
    public FormRpPrintPreview pView;
//		private System.Windows.Forms.PrintDialog printDialog2;
		private bool headingPrinted;
		private bool graphicsPrinted;
		private bool mainPrinted;
		private bool benefitsPrinted;
		private bool notePrinted;
		private double[] ColTotal;
		private Font bodyFont=new Font("Arial",9);
		private Font nameFont=new Font("Arial",9,FontStyle.Bold);
		private Font headingFont=new Font("Arial",13,FontStyle.Bold);
		private Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
		private System.Drawing.Printing.PrintDocument pd2;
		private Font totalFont=new Font("Arial",9,FontStyle.Bold);
		private int yPos=938;
	  private	int xPos=25;
		private System.Windows.Forms.TextBox textPriMax;
		private System.Windows.Forms.TextBox textSecUsed;
		private System.Windows.Forms.TextBox textSecDed;
		private System.Windows.Forms.TextBox textSecMax;
		private System.Windows.Forms.TextBox textPriRem;
		private System.Windows.Forms.TextBox textPriPend;
		private System.Windows.Forms.TextBox textPriUsed;
		private System.Windows.Forms.TextBox textPriDed;
		private System.Windows.Forms.TextBox textSecRem;
		private System.Windows.Forms.TextBox textSecPend;
		private System.Windows.Forms.TextBox textPriDedRem;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox textSecDedRem;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.ODToolBar ToolBarMain;
    private ArrayList ALPreAuth;
		///<summary>This is a list of all procedures for the patient.</summary>
		private Procedure[] ProcList;
		///<summary>This is a filtered list containing only TP procedures.  It's also already sorted by priority and tooth number.</summary>
		private Procedure[] ProcListTP;
		private System.Windows.Forms.ContextMenu menuPatient;
		private System.Windows.Forms.CheckBox checkShowCompleted;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkShowIns;
		private ClaimProc[] ClaimProcList;
		private Family FamCur;
		private Patient PatCur;
		private System.Windows.Forms.CheckBox checkShowFees;
		private OpenDental.UI.ODGrid gridMain;
		private OpenDental.UI.ODGrid gridPreAuth;
		private InsPlan[] InsPlanList;
		private OpenDental.UI.ODGrid gridPlans;
		private TreatPlan[] PlanList;
		///<summary>A list of all ProcTP objects for this patient.</summary>
		private ProcTP[] ProcTPList;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.ImageList imageListMain;
		///<summary>A list of all ProcTP objects for the selected tp.</summary>
		private ProcTP[] ProcTPSelectList;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;
		private PatPlan[] PatPlanList;
		private Benefit[] BenefitList;

		///<summary></summary>
		public ContrTreat(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContrTreat));
			this.label1 = new System.Windows.Forms.Label();
			this.listSetPr = new System.Windows.Forms.ListBox();
			this.panelSide = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkShowFees = new System.Windows.Forms.CheckBox();
			this.checkShowIns = new System.Windows.Forms.CheckBox();
			this.checkShowCompleted = new System.Windows.Forms.CheckBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.textPriMax = new System.Windows.Forms.TextBox();
			this.textSecUsed = new System.Windows.Forms.TextBox();
			this.textSecDed = new System.Windows.Forms.TextBox();
			this.textSecMax = new System.Windows.Forms.TextBox();
			this.textPriRem = new System.Windows.Forms.TextBox();
			this.textPriPend = new System.Windows.Forms.TextBox();
			this.textPriUsed = new System.Windows.Forms.TextBox();
			this.textPriDed = new System.Windows.Forms.TextBox();
			this.textSecRem = new System.Windows.Forms.TextBox();
			this.textSecPend = new System.Windows.Forms.TextBox();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.textPriDedRem = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.textSecDedRem = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.gridPlans = new OpenDental.UI.ODGrid();
			this.gridPreAuth = new OpenDental.UI.ODGrid();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.panelSide.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(22, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "Set Priority";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listSetPr
			// 
			this.listSetPr.Location = new System.Drawing.Point(24, 20);
			this.listSetPr.Name = "listSetPr";
			this.listSetPr.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listSetPr.Size = new System.Drawing.Size(70, 225);
			this.listSetPr.TabIndex = 5;
			this.listSetPr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listSetPr_MouseDown);
			// 
			// panelSide
			// 
			this.panelSide.Controls.Add(this.listSetPr);
			this.panelSide.Controls.Add(this.label1);
			this.panelSide.Controls.Add(this.groupBox1);
			this.panelSide.Location = new System.Drawing.Point(711, 153);
			this.panelSide.Name = "panelSide";
			this.panelSide.Size = new System.Drawing.Size(190, 337);
			this.panelSide.TabIndex = 29;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkShowFees);
			this.groupBox1.Controls.Add(this.checkShowIns);
			this.groupBox1.Controls.Add(this.checkShowCompleted);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(10, 256);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(173, 74);
			this.groupBox1.TabIndex = 59;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Show on printout";
			// 
			// checkShowFees
			// 
			this.checkShowFees.Checked = true;
			this.checkShowFees.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowFees.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowFees.Location = new System.Drawing.Point(15, 33);
			this.checkShowFees.Name = "checkShowFees";
			this.checkShowFees.Size = new System.Drawing.Size(146, 17);
			this.checkShowFees.TabIndex = 20;
			this.checkShowFees.Text = "Fees";
			// 
			// checkShowIns
			// 
			this.checkShowIns.Checked = true;
			this.checkShowIns.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowIns.Location = new System.Drawing.Point(15, 50);
			this.checkShowIns.Name = "checkShowIns";
			this.checkShowIns.Size = new System.Drawing.Size(148, 17);
			this.checkShowIns.TabIndex = 19;
			this.checkShowIns.Text = "Insurance Estimates";
			// 
			// checkShowCompleted
			// 
			this.checkShowCompleted.Checked = true;
			this.checkShowCompleted.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkShowCompleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowCompleted.Location = new System.Drawing.Point(15, 16);
			this.checkShowCompleted.Name = "checkShowCompleted";
			this.checkShowCompleted.Size = new System.Drawing.Size(154, 17);
			this.checkShowCompleted.TabIndex = 18;
			this.checkShowCompleted.Text = "Completed Treatment";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(806, 513);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60, 15);
			this.label10.TabIndex = 31;
			this.label10.Text = "Primary";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(725, 534);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(78, 15);
			this.label11.TabIndex = 32;
			this.label11.Text = "Annual Max";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(724, 555);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(79, 15);
			this.label12.TabIndex = 33;
			this.label12.Text = "Deductible";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(724, 597);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(79, 13);
			this.label13.TabIndex = 34;
			this.label13.Text = "Ins Used";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(725, 635);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(79, 14);
			this.label14.TabIndex = 35;
			this.label14.Text = "Remaining";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(723, 615);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(81, 14);
			this.label15.TabIndex = 36;
			this.label15.Text = "Pending";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(874, 513);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(60, 14);
			this.label16.TabIndex = 37;
			this.label16.Text = "Secondary";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textPriMax
			// 
			this.textPriMax.BackColor = System.Drawing.Color.White;
			this.textPriMax.Location = new System.Drawing.Point(806, 532);
			this.textPriMax.Name = "textPriMax";
			this.textPriMax.ReadOnly = true;
			this.textPriMax.Size = new System.Drawing.Size(60, 20);
			this.textPriMax.TabIndex = 38;
			this.textPriMax.Text = "";
			this.textPriMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecUsed
			// 
			this.textSecUsed.BackColor = System.Drawing.Color.White;
			this.textSecUsed.Location = new System.Drawing.Point(874, 592);
			this.textSecUsed.Name = "textSecUsed";
			this.textSecUsed.ReadOnly = true;
			this.textSecUsed.Size = new System.Drawing.Size(60, 20);
			this.textSecUsed.TabIndex = 39;
			this.textSecUsed.Text = "";
			this.textSecUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecDed
			// 
			this.textSecDed.BackColor = System.Drawing.Color.White;
			this.textSecDed.Location = new System.Drawing.Point(874, 552);
			this.textSecDed.Name = "textSecDed";
			this.textSecDed.ReadOnly = true;
			this.textSecDed.Size = new System.Drawing.Size(60, 20);
			this.textSecDed.TabIndex = 40;
			this.textSecDed.Text = "";
			this.textSecDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecMax
			// 
			this.textSecMax.BackColor = System.Drawing.Color.White;
			this.textSecMax.Location = new System.Drawing.Point(874, 532);
			this.textSecMax.Name = "textSecMax";
			this.textSecMax.ReadOnly = true;
			this.textSecMax.Size = new System.Drawing.Size(60, 20);
			this.textSecMax.TabIndex = 41;
			this.textSecMax.Text = "";
			this.textSecMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriRem
			// 
			this.textPriRem.BackColor = System.Drawing.Color.White;
			this.textPriRem.Location = new System.Drawing.Point(806, 632);
			this.textPriRem.Name = "textPriRem";
			this.textPriRem.ReadOnly = true;
			this.textPriRem.Size = new System.Drawing.Size(60, 20);
			this.textPriRem.TabIndex = 42;
			this.textPriRem.Text = "";
			this.textPriRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriPend
			// 
			this.textPriPend.BackColor = System.Drawing.Color.White;
			this.textPriPend.Location = new System.Drawing.Point(806, 612);
			this.textPriPend.Name = "textPriPend";
			this.textPriPend.ReadOnly = true;
			this.textPriPend.Size = new System.Drawing.Size(60, 20);
			this.textPriPend.TabIndex = 43;
			this.textPriPend.Text = "";
			this.textPriPend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriUsed
			// 
			this.textPriUsed.BackColor = System.Drawing.Color.White;
			this.textPriUsed.Location = new System.Drawing.Point(806, 592);
			this.textPriUsed.Name = "textPriUsed";
			this.textPriUsed.ReadOnly = true;
			this.textPriUsed.Size = new System.Drawing.Size(60, 20);
			this.textPriUsed.TabIndex = 44;
			this.textPriUsed.Text = "";
			this.textPriUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriDed
			// 
			this.textPriDed.BackColor = System.Drawing.Color.White;
			this.textPriDed.Location = new System.Drawing.Point(806, 552);
			this.textPriDed.Name = "textPriDed";
			this.textPriDed.ReadOnly = true;
			this.textPriDed.Size = new System.Drawing.Size(60, 20);
			this.textPriDed.TabIndex = 45;
			this.textPriDed.Text = "";
			this.textPriDed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecRem
			// 
			this.textSecRem.BackColor = System.Drawing.Color.White;
			this.textSecRem.Location = new System.Drawing.Point(874, 632);
			this.textSecRem.Name = "textSecRem";
			this.textSecRem.ReadOnly = true;
			this.textSecRem.Size = new System.Drawing.Size(60, 20);
			this.textSecRem.TabIndex = 46;
			this.textSecRem.Text = "";
			this.textSecRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecPend
			// 
			this.textSecPend.BackColor = System.Drawing.Color.White;
			this.textSecPend.Location = new System.Drawing.Point(874, 612);
			this.textSecPend.Name = "textSecPend";
			this.textSecPend.ReadOnly = true;
			this.textSecPend.Size = new System.Drawing.Size(60, 20);
			this.textSecPend.TabIndex = 47;
			this.textSecPend.Text = "";
			this.textSecPend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// pd2
			// 
			this.pd2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd2_PrintPage);
			// 
			// textPriDedRem
			// 
			this.textPriDedRem.BackColor = System.Drawing.Color.White;
			this.textPriDedRem.Location = new System.Drawing.Point(806, 572);
			this.textPriDedRem.Name = "textPriDedRem";
			this.textPriDedRem.ReadOnly = true;
			this.textPriDedRem.Size = new System.Drawing.Size(60, 20);
			this.textPriDedRem.TabIndex = 51;
			this.textPriDedRem.Text = "";
			this.textPriDedRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(709, 576);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(96, 15);
			this.label18.TabIndex = 50;
			this.label18.Text = "Deduct Remain";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSecDedRem
			// 
			this.textSecDedRem.BackColor = System.Drawing.Color.White;
			this.textSecDedRem.Location = new System.Drawing.Point(874, 572);
			this.textSecDedRem.Name = "textSecDedRem";
			this.textSecDedRem.ReadOnly = true;
			this.textSecDedRem.Size = new System.Drawing.Size(60, 20);
			this.textSecDedRem.TabIndex = 52;
			this.textSecDedRem.Text = "";
			this.textSecDedRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(820, 498);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 12);
			this.label3.TabIndex = 53;
			this.label3.Text = "Insurance";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textNote
			// 
			this.textNote.BackColor = System.Drawing.Color.White;
			this.textNote.Location = new System.Drawing.Point(0, 587);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.ReadOnly = true;
			this.textNote.Size = new System.Drawing.Size(707, 52);
			this.textNote.TabIndex = 54;
			this.textNote.Text = "";
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0, 0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(939, 29);
			this.ToolBarMain.TabIndex = 58;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// gridMain
			// 
			this.gridMain.Columns.Add(new OpenDental.UI.ODGridColumn("Priority", 55, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridMain.Columns.Add(new OpenDental.UI.ODGridColumn("Tth", 40, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridMain.Columns.Add(new OpenDental.UI.ODGridColumn("Surf", 40, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridMain.Columns.Add(new OpenDental.UI.ODGridColumn("Code", 50, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridMain.Columns.Add(new OpenDental.UI.ODGridColumn("Description", 250, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridMain.Columns.Add(new OpenDental.UI.ODGridColumn("Fee", 50, System.Windows.Forms.HorizontalAlignment.Right));
			this.gridMain.Columns.Add(new OpenDental.UI.ODGridColumn("Pri Ins", 50, System.Windows.Forms.HorizontalAlignment.Right));
			this.gridMain.Columns.Add(new OpenDental.UI.ODGridColumn("Sec Ins", 50, System.Windows.Forms.HorizontalAlignment.Right));
			this.gridMain.Columns.Add(new OpenDental.UI.ODGridColumn("Pat", 50, System.Windows.Forms.HorizontalAlignment.Right));
			this.gridMain.Columns.Add(new OpenDental.UI.ODGridColumn("Current", 45, System.Windows.Forms.HorizontalAlignment.Center));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(0, 173);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(698, 411);
			this.gridMain.TabIndex = 59;
			this.gridMain.Title = "Procedures";
			this.gridMain.TranslationName = "TableTP";
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// gridPlans
			// 
			this.gridPlans.Columns.Add(new OpenDental.UI.ODGridColumn("Date", 80, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPlans.Columns.Add(new OpenDental.UI.ODGridColumn("Heading", 360, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPlans.Columns.Add(new OpenDental.UI.ODGridColumn("Fee", 50, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPlans.Columns.Add(new OpenDental.UI.ODGridColumn("Pri Ins", 50, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPlans.Columns.Add(new OpenDental.UI.ODGridColumn("Sec Ins", 50, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPlans.Columns.Add(new OpenDental.UI.ODGridColumn("Pat", 50, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPlans.HScrollVisible = false;
			this.gridPlans.Location = new System.Drawing.Point(0, 33);
			this.gridPlans.Name = "gridPlans";
			this.gridPlans.ScrollValue = 0;
			this.gridPlans.Size = new System.Drawing.Size(658, 134);
			this.gridPlans.TabIndex = 60;
			this.gridPlans.Title = "Treatment Plans";
			this.gridPlans.TranslationName = null;
			this.gridPlans.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPlans_CellClick);
			this.gridPlans.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPlans_CellDoubleClick);
			// 
			// gridPreAuth
			// 
			this.gridPreAuth.Columns.Add(new OpenDental.UI.ODGridColumn("Date Sent", 80, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPreAuth.Columns.Add(new OpenDental.UI.ODGridColumn("Carrier", 100, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPreAuth.Columns.Add(new OpenDental.UI.ODGridColumn("Status", 54, System.Windows.Forms.HorizontalAlignment.Left));
			this.gridPreAuth.HScrollVisible = false;
			this.gridPreAuth.Location = new System.Drawing.Point(676, 33);
			this.gridPreAuth.Name = "gridPreAuth";
			this.gridPreAuth.ScrollValue = 0;
			this.gridPreAuth.Size = new System.Drawing.Size(252, 119);
			this.gridPreAuth.TabIndex = 62;
			this.gridPreAuth.Title = "Pre Authorizations";
			this.gridPreAuth.TranslationName = null;
			this.gridPreAuth.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPreAuth_CellClick);
			this.gridPreAuth.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridPreAuth_CellDoubleClick);
			// 
			// imageListMain
			// 
			this.imageListMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageListMain.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ContrTreat
			// 
			this.Controls.Add(this.gridPreAuth);
			this.Controls.Add(this.gridPlans);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textSecDedRem);
			this.Controls.Add(this.textPriDedRem);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.textSecPend);
			this.Controls.Add(this.textSecRem);
			this.Controls.Add(this.textPriDed);
			this.Controls.Add(this.textPriUsed);
			this.Controls.Add(this.textPriPend);
			this.Controls.Add(this.textPriRem);
			this.Controls.Add(this.textSecMax);
			this.Controls.Add(this.textSecDed);
			this.Controls.Add(this.textSecUsed);
			this.Controls.Add(this.textPriMax);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.panelSide);
			this.Name = "ContrTreat";
			this.Size = new System.Drawing.Size(939, 708);
			this.panelSide.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary>Only called on startup, but after local data loaded from db.</summary>
		public void InitializeOnStartup(){
			checkShowCompleted.Checked=Prefs.GetBool("TreatPlanShowCompleted");
			checkShowIns.Checked=Prefs.GetBool("TreatPlanShowIns");
			//showHidden=true;//shows hidden priorities
			//can't use Lan.F(this);
			Lan.C(this,new Control[]
				{
				label1,
				groupBox1,
				checkShowCompleted,
				checkShowIns,
				label3,
				label10,
				label16,
				label11,
				label12,
				label18,
				label13,
				label15,
				label14,
				});
			LayoutToolBar();
		}

		///<summary>Called every time local data is changed from any workstation.  Refreshes priority lists and lays out the toolbar.</summary>
		public void InitializeLocalData(){
			listSetPr.Items.Clear();
			//listViewPr.Items.Clear();
			listSetPr.Items.Add(Lan.g(this,"no priority"));
			//listViewPr.Items.Add(Lan.g(this,"no priority"));
			//listViewPr.SetSelected(0,true);
			for(int i=0;i<Defs.Short[(int)DefCat.TxPriorities].Length;i++){
				listSetPr.Items.Add(Defs.Short[(int)DefCat.TxPriorities][i].ItemName);
				//listViewPr.Items.Add(Defs.Short[(int)DefCat.TxPriorities][i].ItemName);
				//listViewPr.SetSelected(i+1,true);
			}
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
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"PreAuthorization"),-1,"","PreAuth"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Update Fees"),1,"","Update"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Create TP"),3,"","Create"));
			ToolBarMain.Buttons.Add(new ODToolBarButton(Lan.g(this,"Print TP"),2,"","Print"));
			ArrayList toolButItems=ToolButItems.GetForToolBar(ToolBarsAvail.TreatmentPlanModule);
			for(int i=0;i<toolButItems.Count;i++){
				ToolBarMain.Buttons.Add(new ODToolBarButton(ODToolBarButtonStyle.Separator));
				ToolBarMain.Buttons.Add(new ODToolBarButton(((ToolButItem)toolButItems[i]).ButtonText
					,-1,"",((ToolButItem)toolButItems[i]).ProgramNum));
			}
			ToolBarMain.Invalidate();
		}

		///<summary></summary>
		public void ModuleSelected(int patNum){
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			FamCur=null;
			PatCur=null;
			InsPlanList=null;
			Claims.List=null;
			Claims.HList=null;
			//ClaimProcs.List=null;
			//from FillMain:
			ProcList=null;
			ProcListTP=null;
			//Procedures.HList=null;
			//Procedures.MissingTeeth=null;
		}

		private void RefreshModuleData(int patNum){
			if(patNum!=0){
				FamCur=Patients.GetFamily(patNum);
				PatCur=FamCur.GetPatient(patNum);
				InsPlanList=InsPlans.Refresh(FamCur);
				PatPlanList=PatPlans.Refresh(patNum);
				BenefitList=Benefits.Refresh(PatPlanList);
				//CovPats.Refresh(InsPlanList,PatPlanList);
				Claims.Refresh(PatCur.PatNum);
        //Fees.Refresh();
        ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			}
		}

		private void RefreshModuleScreen(){
			ParentForm.Text=Patients.GetMainTitle(PatCur);
			if(PatCur!=null){
				gridMain.Enabled=true;
				panelSide.Enabled=true;
				ToolBarMain.Buttons["PreAuth"].Enabled=true;
				ToolBarMain.Buttons["Update"].Enabled=true;
				ToolBarMain.Buttons["Print"].Enabled=true;
				ToolBarMain.Invalidate();
        //listPreAuth.Enabled=true;
			}
			else{
				gridMain.Enabled=false;
				panelSide.Enabled=false;
				ToolBarMain.Buttons["PreAuth"].Enabled=false;
				ToolBarMain.Buttons["Update"].Enabled=false;
				ToolBarMain.Buttons["Print"].Enabled=false;
				ToolBarMain.Invalidate();
        //listPreAuth.Enabled=false;
			}
			FillPatientButton();
			FillPlans();
			FillMain();
			FillSummary();
      FillPreAuth();
			//FillMisc();
		}

		private void FillPatientButton(){
			Patients.AddPatsToMenu(menuPatient,new EventHandler(menuPatient_Click),PatCur,FamCur);
		}

		private void menuPatient_Click(object sender,System.EventArgs e) {
			int newPatNum=Patients.ButtonSelect(menuPatient,sender,FamCur);
			OnPatientSelected(newPatNum);
			ModuleSelected(newPatNum);
		}

		private void ToolBarMain_ButtonClick(object sender, OpenDental.UI.ODToolBarButtonClickEventArgs e) {
			if(e.Button.Tag.GetType()==typeof(string)){
				//standard predefined button
				switch(e.Button.Tag.ToString()){
					case "Patient":
						OnPat_Click();
						break;
					case "PreAuth":
						OnPreAuth_Click();
						break;
					case "Update":
						OnUpdate_Click();
						break;
					case "Create":
						OnCreate_Click();
						break;
					case "Print":
						OnPrint_Click();
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

		private void FillPlans(){
			gridPlans.BeginUpdate();
			gridPlans.Rows.Clear();
			if(PatCur==null){
				gridPlans.EndUpdate();
				return;
			}
			ProcList=Procedures.Refresh(PatCur.PatNum);
			ProcListTP=Procedures.GetListTP(ProcList);
			PlanList=TreatPlans.Refresh(PatCur.PatNum);
			ProcTPList=ProcTPs.Refresh(PatCur.PatNum);
			//fill the first line
			double fee;
			double priIns;
			double secIns;
			double pat;
			double totFee=0;
			double totPriIns=0;
			double totSecIns=0;
			double totPat=0;
			for(int i=0;i<ProcListTP.Length;i++){
				fee=ProcListTP[i].ProcFee;
				priIns=ProcListTP[i].GetEst(ClaimProcList,PriSecTot.Pri,PatPlanList);
				secIns=ProcListTP[i].GetEst(ClaimProcList,PriSecTot.Sec,PatPlanList);
				pat=fee-priIns-secIns-ProcListTP[i].GetWriteOff(ClaimProcList);
				if(pat<0)
					pat=0;
				totFee+=fee;
				totPriIns+=priIns;
				totSecIns+=secIns;
				totPat+=pat;
			}
			OpenDental.UI.ODGridRow row;
			row=new ODGridRow();
			row.Cells.Add("");//date empty
			row.Cells.Add(Lan.g(this,"current"));
			row.Cells.Add(totFee.ToString("F"));
			row.Cells.Add(totPriIns.ToString("F"));
			row.Cells.Add(totSecIns.ToString("F"));
			row.Cells.Add(totPat.ToString("F"));
			gridPlans.Rows.Add(row);
			for(int i=0;i<PlanList.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(PlanList[i].DateTP.ToShortDateString());
				row.Cells.Add(PlanList[i].Heading);
				totFee=0;
				totPriIns=0;
				totSecIns=0;
				totPat=0;
				for(int j=0;j<ProcTPList.Length;j++){
					if(ProcTPList[j].TreatPlanNum!=PlanList[i].TreatPlanNum){
						continue;
					}
					totFee+=ProcTPList[j].FeeAmt;
					totPriIns+=ProcTPList[j].PriInsAmt;
					totSecIns+=ProcTPList[j].SecInsAmt;
					totPat+=ProcTPList[j].PatAmt;
				}
				row.Cells.Add(totFee.ToString("F"));
				row.Cells.Add(totPriIns.ToString("F"));
				row.Cells.Add(totSecIns.ToString("F"));
				row.Cells.Add(totPat.ToString("F"));
				gridPlans.Rows.Add(row);
			}
			gridPlans.EndUpdate();
			gridPlans.SetSelected(0,true);
		}

		private void FillMain(){
			gridMain.BeginUpdate();
			gridMain.Rows.Clear();
			if(PatCur==null){
				gridMain.EndUpdate();
				return;
			}
			double fee;
			double priIns;
			double secIns;
			double pat;
			OpenDental.UI.ODGridRow row;
			if(gridPlans.SelectedIndices[0]==0){//current treatplan selected
				for(int i=0;i<ProcListTP.Length;i++){
					row=new ODGridRow();
					row.Cells.Add(Defs.GetName(DefCat.TxPriorities,ProcListTP[i].Priority));
					row.Cells.Add(Tooth.ToInternat(ProcListTP[i].ToothNum));
					row.Cells.Add(ProcListTP[i].Surf);
					row.Cells.Add(ProcListTP[i].ADACode);
					row.Cells.Add(ProcedureCodes.GetProcCode(ProcListTP[i].ADACode).Descript);
					fee=ProcListTP[i].ProcFee;
					priIns=ProcListTP[i].GetEst(ClaimProcList,PriSecTot.Pri,PatPlanList);
					secIns=ProcListTP[i].GetEst(ClaimProcList,PriSecTot.Sec,PatPlanList);
					pat=fee-priIns-secIns-ProcListTP[i].GetWriteOff(ClaimProcList);
					if(pat<0)
						pat=0;
					row.Cells.Add(fee.ToString("F"));
					row.Cells.Add(priIns.ToString("F"));
					row.Cells.Add(secIns.ToString("F"));
					row.Cells.Add(pat.ToString("F"));
					row.Cells.Add("X");
					row.ColorText=Defs.GetColor(DefCat.TxPriorities,ProcListTP[i].Priority);
					if(row.ColorText==Color.White){
						row.ColorText=Color.Black;
					}
					gridMain.Rows.Add(row);
				}
				textNote.Text=Prefs.GetString("TreatmentPlanNote");
			}
			else{//any except current tp selected
				ProcTPSelectList=ProcTPs.GetListForTP(PlanList[gridPlans.SelectedIndices[0]-1].TreatPlanNum,ProcTPList);
				bool isCurrent;
				for(int i=0;i<ProcTPSelectList.Length;i++){
					row=new ODGridRow();
					row.Cells.Add(Defs.GetName(DefCat.TxPriorities,ProcTPSelectList[i].Priority));
					row.Cells.Add(ProcTPSelectList[i].ToothNumTP);
					row.Cells.Add(ProcTPSelectList[i].Surf);
					row.Cells.Add(ProcTPSelectList[i].ADACode);
					row.Cells.Add(ProcTPSelectList[i].Descript);
					row.Cells.Add(ProcTPSelectList[i].FeeAmt.ToString("F"));
					row.Cells.Add(ProcTPSelectList[i].PriInsAmt.ToString("F"));
					row.Cells.Add(ProcTPSelectList[i].SecInsAmt.ToString("F"));
					row.Cells.Add(ProcTPSelectList[i].PatAmt.ToString("F"));
					isCurrent=false;
					for(int j=0;j<ProcListTP.Length;j++){
						if(ProcListTP[j].ProcNum==ProcTPSelectList[i].ProcNumOrig){
							isCurrent=true;
						}
					}
					if(isCurrent){
						row.Cells.Add("X");
					}
					else{
						row.Cells.Add("");
					}		
					row.ColorText=Defs.GetColor(DefCat.TxPriorities,ProcTPSelectList[i].Priority);
					if(row.ColorText==Color.White){
						row.ColorText=Color.Black;
					}
					gridMain.Rows.Add(row);
				}
				textNote.Text=PlanList[gridPlans.SelectedIndices[0]-1].Note;
			}
			gridMain.EndUpdate();
		}

		private void FillSummary(){
			textPriMax.Text="";
			textPriDed.Text="";
			textPriDedRem.Text="";
			textPriUsed.Text="";
			textPriPend.Text="";
			textPriRem.Text="";
			textSecMax.Text="";
			textSecDed.Text="";
			textSecDedRem.Text="";
			textSecUsed.Text="";
			textSecPend.Text="";
			textSecRem.Text="";
			if(PatCur==null){
				return;
			}
			double max=0;
			double ded=0;
			double dedUsed=0;
			double remain=0;
			double pend=0;
			double used=0;
			InsPlan PlanCur;//=new InsPlan();
			if(PatPlanList.Length>0){
				PlanCur=InsPlans.GetPlan(PatPlanList[0].PlanNum,InsPlanList);
				pend=InsPlans.GetPending
					(ClaimProcList,DateTime.Today,PatPlanList[0].PlanNum,PatPlanList[0].PatPlanNum,InsPlanList,BenefitList);
				textPriPend.Text=pend.ToString("F");
				used=InsPlans.GetInsUsed
					(ClaimProcList,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList,BenefitList);
				textPriUsed.Text=used.ToString("F");
				max=Benefits.GetAnnualMax(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum);
				if(max==-1){//if annual max is blank
					textPriMax.Text="";
					textPriRem.Text="";
				}
				else{
					//remain=max-used-pend
					//remain=InsPlans.GetInsRem(ClaimProcList,DateTime.Today,PatPlans.GetPlanNum(PatPlanList,1),-1,InsPlanList);
					remain=max-used-pend;
					if(remain<0) {
						remain=0;
					}
					textPriMax.Text=max.ToString("F");
					textPriRem.Text=remain.ToString("F");
				}
				//deductible:
				ded=Benefits.GetDeductible(BenefitList,PlanCur.PlanNum,PatPlanList[0].PatPlanNum);
				if(ded!=-1){
					textPriDed.Text=ded.ToString("F");
					dedUsed=InsPlans.GetDedUsed
						(ClaimProcList,DateTime.Today,PlanCur.PlanNum,PatPlanList[0].PatPlanNum,-1,InsPlanList,BenefitList);
					textPriDedRem.Text=(ded-dedUsed).ToString("F");
				}
			}
			if(PatPlanList.Length>1){
				PlanCur=InsPlans.GetPlan(PatPlanList[1].PlanNum,InsPlanList);
				pend=InsPlans.GetPending
					(ClaimProcList,DateTime.Today,PatPlanList[1].PlanNum,PatPlanList[1].PatPlanNum,InsPlanList,BenefitList);
				textSecPend.Text=pend.ToString("F");
				used=InsPlans.GetInsUsed
									(ClaimProcList,DateTime.Today,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,-1,InsPlanList,BenefitList);
				textSecUsed.Text=used.ToString("F");
				max=Benefits.GetAnnualMax(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum);
				if(max==-1){
					textSecMax.Text="";
					textSecRem.Text="";
				}
				else{
					remain=max-used-pend;
					if(remain<0) {
						remain=0;
					}
					textSecMax.Text=max.ToString("F");
					textSecRem.Text=remain.ToString("F");
				}
				ded=Benefits.GetDeductible(BenefitList,PlanCur.PlanNum,PatPlanList[1].PatPlanNum);
				if(ded!=-1){
					textSecDed.Text=ded.ToString("F");
					dedUsed=InsPlans.GetDedUsed
						(ClaimProcList,DateTime.Today,PlanCur.PlanNum,PatPlanList[1].PatPlanNum,-1,InsPlanList,BenefitList);
					textSecDedRem.Text=(ded-dedUsed).ToString("F");
				}
			}
		}

    private void FillPreAuth(){
			gridPreAuth.BeginUpdate();
			gridPreAuth.Rows.Clear();
      if(PatCur==null){
				gridPreAuth.EndUpdate();
				return;
			}
      ALPreAuth=new ArrayList();			
      for(int i=0;i<Claims.List.Length;i++){
        if(Claims.List[i].ClaimType=="PreAuth"){
          ALPreAuth.Add(Claims.List[i]);
        }
      }
			OpenDental.UI.ODGridRow row;
      for(int i=0;i<ALPreAuth.Count;i++){
				InsPlan PlanCur=InsPlans.GetPlan(((Claim)ALPreAuth[i]).PlanNum,InsPlanList);
				row=new ODGridRow();
				if(((Claim)ALPreAuth[i]).DateSent.Year<1880){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(((Claim)ALPreAuth[i]).DateSent.ToShortDateString());
				}
				row.Cells.Add(Carriers.GetName(PlanCur.CarrierNum));
				row.Cells.Add(((Claim)ALPreAuth[i]).ClaimStatus.ToString());
				gridPreAuth.Rows.Add(row);
      }
			gridPreAuth.EndUpdate();
    }

		private void gridMain_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			gridPreAuth.SetSelected(false);//is this a desirable behavior?
		}

		private void gridMain_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(gridPlans.SelectedIndices[0]==0){//current plan
				Procedure ProcCur=ProcListTP[e.Row];
				FormProcEdit FormPE=new FormProcEdit(ProcCur,PatCur,FamCur,InsPlanList);
				FormPE.ShowDialog();
				ModuleSelected(PatCur.PatNum);
				for(int i=0;i<ProcListTP.Length;i++){
					if(ProcListTP[i].ProcNum==ProcCur.ProcNum){
						gridMain.SetSelected(i,true);
					}
				}
				return;
			}
			//any other TP
			ProcTP procT=ProcTPSelectList[e.Row];
			FormProcTPEdit FormP=new FormProcTPEdit(procT);
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.Cancel){
				return;
			}
			int selectedPlan=gridPlans.SelectedIndices[0];
			int selectedProc=procT.ProcTPNum;
			ModuleSelected(PatCur.PatNum);
			gridPlans.SetSelected(selectedPlan,true);
			FillMain();
			for(int i=0;i<ProcTPSelectList.Length;i++){
				if(ProcTPSelectList[i].ProcTPNum==selectedProc){
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void gridPlans_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FillMain();
			gridPreAuth.SetSelected(false);
		}

		private void gridPlans_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(e.Row==0){
				return;//there is nothing to edit if user clicks on current.
			}
			int tpNum=PlanList[e.Row-1].TreatPlanNum;
			FormTreatPlanEdit FormT=new FormTreatPlanEdit(PlanList[e.Row-1]);
			FormT.ShowDialog();
			ModuleSelected(PatCur.PatNum);
			for(int i=0;i<PlanList.Length;i++){
				if(PlanList[i].TreatPlanNum==tpNum){
					gridPlans.SetSelected(i+1,true);
				}
			}
			FillMain();
		}

		private void listSetPr_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			if(gridMain.SelectedIndices.Length==0){
				return;
			}
			int clickedRow=listSetPr.IndexFromPoint(e.X,e.Y);
			if(clickedRow==-1)
				return;
			if(gridPlans.SelectedIndices[0]==0){//current TP
				Procedure ProcCur;
				Procedure ProcOld;
				for(int i=0;i<gridMain.SelectedIndices.Length;i++){//loop through the main list of selected procs
					ProcCur=ProcListTP[gridMain.SelectedIndices[i]];
					ProcOld=ProcCur.Copy();
					if(clickedRow==0)//set priority to "no priority"
						ProcCur.Priority=0;
					else
						ProcCur.Priority=Defs.Short[(int)DefCat.TxPriorities][clickedRow-1].DefNum;
					ProcCur.Update(ProcOld);//no recall synch required
				}
				ModuleSelected(PatCur.PatNum);
			}
			else{//any other TP
				int selectedTP=gridPlans.SelectedIndices[0];
				ProcTP proc;
				for(int i=0;i<gridMain.SelectedIndices.Length;i++){//loop through the main list of selected procTPs
					proc=ProcTPSelectList[gridMain.SelectedIndices[i]];
					if(clickedRow==0)//set priority to "no priority"
						proc.Priority=0;
					else
						proc.Priority=Defs.Short[(int)DefCat.TxPriorities][clickedRow-1].DefNum;
					proc.InsertOrUpdate(false);
				}
				ModuleSelected(PatCur.PatNum);
				gridPlans.SetSelected(selectedTP,true);
				FillMain();
			}
		}

		///<summary>Preview is only used for debugging.</summary>
		public void PrintReport(bool justPreview){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.DefaultPageSettings.Margins=new Margins(50,50,40,25);
			try{
				if(justPreview){
					pView = new FormRpPrintPreview();
					pView.printPreviewControl2.Document=pd2;
					pView.ShowDialog();				
			  }
				else{
					if(Printers.SetPrinter(pd2,PrintSituation.TPPerio)){
						pd2.Print();
					}
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		private void OnPrint_Click() {
			linesPrinted=0;
			ColTotal = new double[10];
			headingPrinted=false;
			graphicsPrinted=false;
			mainPrinted=false;
			benefitsPrinted=false;
			notePrinted=false;
			#if DEBUG
				PrintReport(true);
			#else
				PrintReport(false);	
			#endif
		}

		private void pd2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			//printable area is w=800.
			//Height is 1035 for standard paper.  Some printers can handle up to 1042. was 1037
			//MessageBox.Show(e.MarginBounds.Height.ToString());
			yPos=44;
			xPos=25;
			#region printHeading
			if(!headingPrinted){
				string heading;
				if(gridPlans.SelectedIndices[0]==0){//current TP
					heading=Lan.g(this,"Proposed Treatment Plan");
				}
				else{
					heading=PlanList[gridPlans.SelectedIndices[0]-1].Heading;
				}
				string practice=((Pref)Prefs.HList["PracticeTitle"]).ValueString;
				string name=PatCur.FName+" "+PatCur.MiddleI+" "+PatCur.LName;
				string date=DateTime.Today.ToShortDateString();
				if(PatCur.Preferred!="")
					name="'"+PatCur.Preferred+"' "+name;
				e.Graphics.DrawString(heading,headingFont,Brushes.Black
					,400-e.Graphics.MeasureString(heading,headingFont).Width/2,yPos);
				yPos+=(int)e.Graphics.MeasureString(heading,headingFont).Height;
				e.Graphics.DrawString(practice,subHeadingFont,Brushes.Black
					,400-e.Graphics.MeasureString(practice,subHeadingFont).Width/2,yPos);
				yPos+=(int)e.Graphics.MeasureString(heading,headingFont).Height;
				e.Graphics.DrawString(date,subHeadingFont,Brushes.Black
					,400-e.Graphics.MeasureString(date,subHeadingFont).Width/2,yPos);
				yPos+=30;
				e.Graphics.DrawString(name,subHeadingFont,Brushes.Black
					,400-e.Graphics.MeasureString(name,subHeadingFont).Width/2,yPos);
				yPos+=30;
				headingPrinted=true;
			}
			#endregion
			#region PrintGraphics
			if(headingPrinted && !graphicsPrinted){
				if(Prefs.GetBool("TreatPlanShowGraphics")){
					//prints the graphical tooth chart and legend
					ContrTeeth cTeeth=new ContrTeeth();
					cTeeth.PatCur=PatCur;
					cTeeth.SetWidth(589);//this is not very precise
					//MessageBox.Show(cTeeth.Width.ToString());
					cTeeth.CreateBackShadow();
					cTeeth.ClearProcs();
					//ProcList already refreshed
					ArrayList procAL = new ArrayList();
					//arrayLProc already filled
					//first, add all completed work. C,EC,EO
					//and Referred
					for(int i=0;i<ProcList.Length;i++){
						if(ProcList[i].ProcStatus==ProcStat.C
							|| ProcList[i].ProcStatus==ProcStat.EC
							|| ProcList[i].ProcStatus==ProcStat.EO)
						{
							//always show missing teeth
							if(ProcedureCodes.GetProcCode(ProcList[i].ADACode).RemoveTooth){
								procAL.Add(ProcList[i]);
							}
							else if(checkShowCompleted.Checked){
								procAL.Add(ProcList[i]);
							}
						}
						if(ProcList[i].ProcStatus==ProcStat.R){//always show all referred
							procAL.Add(ProcList[i]);
						}
					}//for
					//then add whatever is showing on the selected TP
					if(gridPlans.SelectedIndices[0]==0){//current plan
						for(int i=0;i<ProcListTP.Length;i++){
							procAL.Add(ProcListTP[i]);
						}
					}
					else{
						Procedure procDummy;//not a real procedure.  Just used to help display on graphical chart
						for(int i=0;i<ProcTPSelectList.Length;i++){
							procDummy=new Procedure();
							//this next loop is a way to get missing fields like tooth range.  Could be improved.
							for(int j=0;j<ProcList.Length;j++){
								if(ProcList[j].ProcNum==ProcTPSelectList[i].ProcNumOrig){
									//but remember that even if the procedure is found, Status might have been altered
									procDummy=ProcList[j].Copy();
								}
							}
							if(Tooth.IsValidEntry(ProcTPSelectList[i].ToothNumTP)){
								procDummy.ToothNum=Tooth.FromInternat(ProcTPSelectList[i].ToothNumTP);
							}
							procDummy.Surf=ProcTPSelectList[i].Surf;
							//procDummy.HideGraphical??
							procDummy.ProcStatus=ProcStat.TP;
							procDummy.ADACode=ProcTPSelectList[i].ADACode;
							procAL.Add(procDummy);
						}
					}
					cTeeth.DrawProcs(procAL);
					//xPos and yPos will be the upper left of this entire section.
					//some arbitrary adjustments to alignment made here because cTeeth.Width not accurate
					e.Graphics.DrawString(Lan.g(this,"Your Right"),bodyFont,Brushes.Black,
						new RectangleF(400-cTeeth.Width/2-60,(float)yPos+cTeeth.Height/2-10,50,200));
					cTeeth.PrintChart(e.Graphics,400-cTeeth.Width/2-10,yPos);
					e.Graphics.DrawString(Lan.g(this,"Your Left"),bodyFont,Brushes.Black,
						new RectangleF(400+cTeeth.Width/2+25,(float)yPos+cTeeth.Height/2-10,50,200));
					yPos+=cTeeth.Height;
					if(checkShowCompleted.Checked){
						yPos+=15;
						xPos=190;
						e.Graphics.FillRectangle
							(new SolidBrush(Defs.Short[(int)DefCat.ChartGraphicColors][3].ItemColor),
							xPos,yPos,14,14);
						xPos+=15;
						e.Graphics.DrawString(Lan.g(this,"Existing"),bodyFont,Brushes.Black,xPos,yPos);
						xPos+=(int)e.Graphics.MeasureString(Lan.g(this,"Existing"),bodyFont).Width+23;
						//The Complete work is actually a combination of EC and C. Usually same color.
						//But just in case they are different, this will show it.
						e.Graphics.FillRectangle
							(new SolidBrush(Defs.Short[(int)DefCat.ChartGraphicColors][2].ItemColor),
							xPos,yPos,7,14);
						xPos+=7;
						e.Graphics.FillRectangle
							(new SolidBrush(Defs.Short[(int)DefCat.ChartGraphicColors][1].ItemColor),
							xPos,yPos,7,14);
						xPos+=8;
						e.Graphics.DrawString(Lan.g(this,"Complete"),bodyFont,Brushes.Black,xPos,yPos);
						xPos+=(int)e.Graphics.MeasureString(Lan.g(this,"Complete"),bodyFont).Width+23;
						e.Graphics.FillRectangle
							(new SolidBrush(Defs.Short[(int)DefCat.ChartGraphicColors][4].ItemColor),
							xPos,yPos,14,14);
						xPos+=15;
						e.Graphics.DrawString(Lan.g(this,"Referred Out"),bodyFont,Brushes.Black,xPos,yPos);
						xPos+=(int)e.Graphics.MeasureString(Lan.g(this,"Referred Out"),bodyFont).Width+23;
						e.Graphics.FillRectangle
							(new SolidBrush(Defs.Short[(int)DefCat.ChartGraphicColors][0].ItemColor),
							xPos,yPos,14,14);
						xPos+=15;
						e.Graphics.DrawString(Lan.g(this,"Treatment Planned"),bodyFont,Brushes.Black,xPos,yPos);
					}
					yPos+=40;
					xPos=25;
				}//if(Prefs.GetBool("TreatPlanShowGraphics"))
				graphicsPrinted=true;
			}
			#endregion
			#region DefineColumns
			//no condition, so runs again on each page
			int[] colPos;
			HorizontalAlignment[] colAlign;
			string[] ColCaption;
			int[] colW;
			if(checkShowIns.Checked){
				colPos=new int[10];
				colAlign=new HorizontalAlignment[9];
				ColCaption=new string[9];
				ColCaption[6]=Lan.g(this,"Pri Ins");
				ColCaption[7]=Lan.g(this,"Sec Ins");
				ColCaption[8]=Lan.g(this,"Pat Pay");
				colW=new int[9];
				colW[0]=70;  colAlign[0]=HorizontalAlignment.Left;//priority
				colW[1]=45;	 colAlign[1]=HorizontalAlignment.Left;//tooth
				colW[2]=60;  colAlign[2]=HorizontalAlignment.Left;//surf
				colW[3]=70;  colAlign[3]=HorizontalAlignment.Left;//adacode
				colW[4]=215; colAlign[4]=HorizontalAlignment.Left;//description
				colW[5]=60;  colAlign[5]=HorizontalAlignment.Right;//fee
				colW[6]=50;  colAlign[6]=HorizontalAlignment.Right;//pri ins
				colW[7]=60;  colAlign[7]=HorizontalAlignment.Right;//sec ins
				colW[8]=65;  colAlign[8]=HorizontalAlignment.Right;//pat Pay
				colPos=new int[colW.Length+1];
				colPos[0]=55;
				for(int i=1;i<colPos.Length;i++){
					colPos[i]=colPos[i-1]+colW[i-1];
				}
			}
			else{//don't show ins
				colW=new int[6];
				colAlign=new HorizontalAlignment[6];
				ColCaption=new string[6];
				colW[0]=70;  colAlign[0]=HorizontalAlignment.Left;//priority
				colW[1]=45;	 colAlign[1]=HorizontalAlignment.Left;//tooth
				colW[2]=60;  colAlign[2]=HorizontalAlignment.Left;//surf
				colW[3]=70;  colAlign[3]=HorizontalAlignment.Left;//adacode
				colW[4]=205; colAlign[4]=HorizontalAlignment.Left;//description
				colW[5]=70;  colAlign[5]=HorizontalAlignment.Right;//fee
				colPos=new int[colW.Length+1];
				colPos[0]=145;
				for(int i=1;i<colPos.Length;i++){
					colPos[i]=colPos[i-1]+colW[i-1];
				}
			}
			ColCaption[0]=Lan.g(this,"Priority");
			ColCaption[1]=Lan.g(this,"Tooth");
			ColCaption[2]=Lan.g(this,"Surf");
			ColCaption[3]=Lan.g(this,"ADA Code");
			ColCaption[4]=Lan.g(this,"Description");
			if(checkShowFees.Checked){
				ColCaption[5]=Lan.g(this,"Fee");
			}
			#endregion
			#region MainTable
			if(graphicsPrinted && !mainPrinted && yPos < e.MarginBounds.Height-40){
				//this might be starting somewhere in the middle of the table
				e.Graphics.FillRectangle(Brushes.LightGray,colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],18);
				e.Graphics.DrawRectangle
					(new Pen(Color.Black),colPos[0],yPos,colPos[colPos.Length-1]-colPos[0],18);  
				for(int i=1;i<colPos.Length;i++) 
					e.Graphics.DrawLine(new Pen(Color.Black),colPos[i],yPos,colPos[i],yPos+18);
				//Prints the Column Titles
				for(int i=0;i<ColCaption.Length;i++){ 
					if(colAlign[i]==HorizontalAlignment.Right){
						e.Graphics.DrawString(ColCaption[i],totalFont,Brushes.Black,
							colPos[i+1]-e.Graphics.MeasureString(ColCaption[i],totalFont).Width-1,yPos+1);
					}
					else 
						e.Graphics.DrawString(ColCaption[i],totalFont,Brushes.Black,colPos[i]+2,yPos+1);

				}
				yPos+=18;
				while(yPos < e.MarginBounds.Height-16 && linesPrinted < gridMain.Rows.Count){
					for(int i=0;i<colPos.Length-1;i++){
						if(i==5 && !checkShowFees.Checked){
							continue;
						}
  					else if(colAlign[i]==HorizontalAlignment.Right){
							e.Graphics.DrawString
								(gridMain.Rows[linesPrinted].Cells[i].Text
								,bodyFont,new SolidBrush(gridMain.Rows[linesPrinted].ColorText)
								,colPos[i+1]-e.Graphics.MeasureString
								(gridMain.Rows[linesPrinted].Cells[i].Text,bodyFont).Width-1
								,yPos);
						}
						else{
							e.Graphics.DrawString(gridMain.Rows[linesPrinted].Cells[i].Text,bodyFont
								,new SolidBrush(gridMain.Rows[linesPrinted].ColorText)
								,new RectangleF(colPos[i]+2,yPos
								,colPos[i+1]-colPos[i]-5,bodyFont.GetHeight(e.Graphics)));
						}
						if(i>4)
							ColTotal[i]+=(float)(PIn.PDouble(gridMain.Rows[linesPrinted].Cells[i].Text));
					} 
					//Column lines		
					for(int i=0;i<colPos.Length;i++){ 
	  				e.Graphics.DrawLine(new Pen(Color.Gray),colPos[i],yPos+16,colPos[i],yPos);
					}
					//e.Graphics.DrawLine(new Pen(Color.Gray),colPos[0],yPos,colPos[9],yPos);
					linesPrinted++;
					yPos+=16;
				}//end while 
				//bottom line
				e.Graphics.DrawLine(new Pen(Color.Gray),colPos[0],yPos,colPos[colPos.Length-1],yPos);
				if(linesPrinted==gridMain.Rows.Count){
					mainPrinted=true;
					if(checkShowFees.Checked || checkShowIns.Checked){
						e.Graphics.DrawString(Lan.g(this,"Total"),totalFont,Brushes.Black
							,colPos[5]-e.Graphics.MeasureString(Lan.g(this,"Total"),totalFont).Width,yPos);
					}
          for(int i=5;i<colPos.Length-1;i++){
						if(i==5 && !checkShowFees.Checked){
							continue;
						}
				    e.Graphics.DrawString(ColTotal[i].ToString("F"),totalFont,Brushes.Black,
				    colPos[i+1]-e.Graphics.MeasureString(ColTotal[i].ToString("F"),totalFont).Width-1,yPos);
          }
				}
				yPos+=40;
			}//main table
			#endregion
			#region printBenefits
			if(mainPrinted && !benefitsPrinted && yPos < e.MarginBounds.Height-16*8){//1037
				if(checkShowIns.Checked){
					int[] insColX=new int[4];
					insColX[0]=250;
					insColX[1]=400;
					insColX[2]=475;
					insColX[3]=550;//right edge
					int lineSpacing=16;
					e.Graphics.FillRectangle(Brushes.LightGray,insColX[0],yPos,insColX[3]-insColX[0],16);
					e.Graphics.DrawRectangle(new Pen(Color.Black),insColX[0],yPos-1,insColX[3]-insColX[0],16); 
					string insTitle="Dental Insurance Benefits";
					e.Graphics.DrawString(Lan.g(this,insTitle),totalFont,Brushes.Black
							,insColX[1]-e.Graphics.MeasureString(insTitle,totalFont).Width/2,yPos);
					yPos+=lineSpacing;
					for(int i=0;i<4;i++) 
						e.Graphics.DrawLine(new Pen(Color.Gray),insColX[i],yPos,insColX[i],yPos+lineSpacing*7);
					e.Graphics.DrawLine(new Pen(Color.Gray),insColX[0],yPos+lineSpacing
						,insColX[3],yPos+lineSpacing);
					e.Graphics.DrawLine(new Pen(Color.Gray),insColX[0],yPos+lineSpacing*7
						,insColX[3],yPos+lineSpacing*7);
					Font insFont=new Font("Arial",9);
					string insHead="";
					string insPri="";
					string insSec="";
					for(int i=0;i<7;i++){
						switch(i)  {
							case 0:
								insHead="";
								insPri="Primary";
								insSec="Secondary";
								break;
							case 1:
								insHead="Annual Maximum";
								insPri=textPriMax.Text;
								insSec=textSecMax.Text;
								break;
							case 2:
								insHead="Deductible";
								insPri=textPriDed.Text;
								insSec=textSecDed.Text;
								break;
							case 3:
								insHead="Deductible Remaining";
								insPri=textPriDedRem.Text;
								insSec=textSecDedRem.Text;
								break;
							case 4:
								insHead="Insurance Used";
								insPri=textPriUsed.Text;
								insSec=textSecUsed.Text;
								break;
							case 5:
								insHead="Pending";
								insPri=textPriPend.Text;
								insSec=textSecPend.Text;
								break;
							case 6:
								insHead="Remaining";
								insPri=textPriRem.Text;
								insSec=textSecRem.Text;
    						break;
						}//end switch
						e.Graphics.DrawString(Lan.g(this,insHead),insFont,Brushes.Black,insColX[0]+2,yPos+1);
						if(i==0){
							//float xHead=(float)();
							e.Graphics.DrawString(Lan.g(this,insPri),insFont,Brushes.Black,insColX[2]
								-e.Graphics.MeasureString(insPri,insFont).Width-1,yPos+1);
							//xHead=(float)();
							e.Graphics.DrawString(Lan.g(this,insSec),insFont,Brushes.Black,insColX[3]
								-e.Graphics.MeasureString(insSec,insFont).Width-1,yPos+1);					
						}
						else{
							e.Graphics.DrawString(Lan.g(this,insPri),insFont,Brushes.Black,insColX[2]
								-e.Graphics.MeasureString(insPri,insFont).Width-1,yPos+1);
							e.Graphics.DrawString(Lan.g(this,insSec),insFont,Brushes.Black,insColX[3]
								-e.Graphics.MeasureString(insSec,insFont).Width-1,yPos+1);
						}
						yPos+=lineSpacing;
					}//end for 0-7
					yPos+=20;
				}//if(checkShowIns.checked)
				benefitsPrinted=true;	
			}
			#endregion
			#region printNote
			if(benefitsPrinted && !notePrinted){
				string note="";
				if(gridPlans.SelectedIndices[0]==0){//current TP
					note=Prefs.GetString("TreatmentPlanNote");
				}
				else{
					note=PlanList[gridPlans.SelectedIndices[0]-1].Note;
				}
				float noteH=e.Graphics.MeasureString(note,bodyFont,695).Height;
				if(yPos < e.MarginBounds.Height-noteH){//1037
					e.Graphics.DrawRectangle(Pens.Gray,50,yPos,700,noteH+8);
					e.Graphics.DrawString(note,bodyFont,Brushes.Black,new RectangleF(55f,yPos+5,695f,noteH));
					notePrinted=true;
				}
			}
			#endregion
			if(!notePrinted){
				e.HasMorePages=true;
			}
			else{
				e.HasMorePages=false;
			}
		}

		private void OnUpdate_Click() {
			if(gridPlans.SelectedIndices[0]!=0){
				MsgBox.Show(this,"The update fee utility only works on the current treatment plan, not any saved plans.");
				return;
			}
		  if(!MsgBox.Show(this,true,"Update all fees and insurance estimates on this treatment plan to the current fees for this patient?")){
        return;   
      }
			//if(PatCur.PriPlanNum>0){//has primary ins
			//	InsPlans.GetCur(PatCur.PriPlanNum);//in preparation for the capcopay changes
			//}
			Procedure procCur;
			Procedure procOld;
      for(int i=0;i<ProcListTP.Length;i++){
				procCur=ProcListTP[i];
				procOld=procCur.Copy();
				//first the fees
				procCur.ProcFee=Fees.GetAmount0(procCur.ADACode,Fees.GetFeeSched(PatCur,InsPlanList,PatPlanList));
				procCur.ComputeEstimates(PatCur.PatNum,ClaimProcList,false,InsPlanList,PatPlanList,BenefitList);
				procCur.Update(procOld);//no recall synch required 
      }
      ModuleSelected(PatCur.PatNum);
		}

		private void OnCreate_Click(){
			if(gridPlans.SelectedIndices[0]!=0){
				MsgBox.Show(this,"The current TP must be selected before creating a TP.");
				return;
			}
			if(gridMain.SelectedIndices.Length==0){
				gridMain.SetSelected(true);
			}
			TreatPlan tp=new TreatPlan();
			tp.Heading=Lan.g(this,"Proposed Treatment Plan");
			tp.DateTP=DateTime.Today;
			tp.PatNum=PatCur.PatNum;
			tp.Note=Prefs.GetString("TreatmentPlanNote");
			tp.InsertOrUpdate(true);
			ProcTP procTP;
			double fee;
			double priIns;
			double secIns;
			double pat;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				procTP=new ProcTP();
				procTP.TreatPlanNum=tp.TreatPlanNum;
				procTP.PatNum=PatCur.PatNum;
				procTP.ProcNumOrig=ProcListTP[gridMain.SelectedIndices[i]].ProcNum;
				procTP.ItemOrder=i;
				procTP.Priority=ProcListTP[gridMain.SelectedIndices[i]].Priority;
				procTP.ToothNumTP=Tooth.ToInternat(ProcListTP[gridMain.SelectedIndices[i]].ToothNum);
				procTP.Surf=ProcListTP[gridMain.SelectedIndices[i]].Surf;
				procTP.ADACode=ProcListTP[gridMain.SelectedIndices[i]].ADACode;
				procTP.Descript=ProcedureCodes.GetProcCode(ProcListTP[gridMain.SelectedIndices[i]].ADACode).Descript;
				fee=ProcListTP[gridMain.SelectedIndices[i]].ProcFee;
				priIns=ProcListTP[gridMain.SelectedIndices[i]].GetEst(ClaimProcList,PriSecTot.Pri,PatPlanList);
				secIns=ProcListTP[gridMain.SelectedIndices[i]].GetEst(ClaimProcList,PriSecTot.Sec,PatPlanList);
				pat=fee-priIns-secIns-ProcListTP[gridMain.SelectedIndices[i]].GetWriteOff(ClaimProcList);
				if(pat<0)
					pat=0;
				procTP.FeeAmt=fee;
				procTP.PriInsAmt=priIns;
				procTP.SecInsAmt=secIns;
				procTP.PatAmt=pat;
				procTP.InsertOrUpdate(true);
			}
			ModuleSelected(PatCur.PatNum);
			for(int i=0;i<PlanList.Length;i++){
				if(PlanList[i].TreatPlanNum==tp.TreatPlanNum){
					gridPlans.SetSelected(i+1,true);
					FillMain();
				}
			}
		}

		private void OnPreAuth_Click() {
			if(gridPlans.SelectedIndices[0]!=0){
				MsgBox.Show(this,"You can only send a preauth from the current TP, not a saved TP.");
				return;
			}
		  if(gridMain.SelectedIndices.Length==0){
        MessageBox.Show(Lan.g(this,"Please select procedures first."));
        return;
      }
      FormInsPlanSelect FormIPS=new FormInsPlanSelect(PatCur.PatNum); 
			FormIPS.ViewRelat=true;
      FormIPS.ShowDialog();
      if(FormIPS.DialogResult!=DialogResult.OK){
        return;
      }
			Claims.Cur=new Claim();
			Claims.Cur.PatNum=PatCur.PatNum;
			Claims.Cur.ClaimStatus="W";
			Claims.Cur.DateSent=DateTime.Today;
			Claims.Cur.PlanNum=FormIPS.SelectedPlan.PlanNum;
			Claims.Cur.ProvTreat=ProcListTP[gridMain.SelectedIndices[0]].ProvNum;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				if(!Providers.GetIsSec(ProcListTP[gridMain.SelectedIndices[i]].ProvNum)){
					Claims.Cur.ProvTreat=ProcListTP[gridMain.SelectedIndices[i]].ProvNum;
				}
			}
			Claims.Cur.ClinicNum=PatCur.ClinicNum;
			if(Providers.GetIsSec(Claims.Cur.ProvTreat)){
				Claims.Cur.ProvTreat=PatCur.PriProv;
				//OK if 0, because auto select first in list when open claim
			}
			//Claims.Cur.DedApplied=0;//calcs in ClaimEdit.
			if(Prefs.GetInt("InsBillingProv")==0){//this can later be extended to include a 3rd option
				//default=0
				Claims.Cur.ProvBill=Prefs.GetInt("PracticeDefaultProv");
			}
			else{
				//treat=1
				Claims.Cur.ProvBill=Claims.Cur.ProvTreat;//OK if zero, because it will get fixed in claim
			}
			Claims.Cur.EmployRelated=YN.No;
      Claims.Cur.ClaimType="PreAuth";
			//this could be a little better if we automate figuring out the patrelat
			//instead of making the user enter it:
			Claims.Cur.PatRelat=FormIPS.PatRelat;
			Claims.InsertCur();
			Procedure ProcCur;
			//Procedure ProcOld;
			ClaimProc ClaimProcCur;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				ProcCur=ProcListTP[gridMain.SelectedIndices[i]];
				//ProcOld=ProcCur.Copy();
        ClaimProcCur=new ClaimProc();
				ClaimProcCur.ProcNum=ProcCur.ProcNum;
        ClaimProcCur.ClaimNum=Claims.Cur.ClaimNum;
        ClaimProcCur.PatNum=PatCur.PatNum;
        ClaimProcCur.ProvNum=ProcCur.ProvNum;
				ClaimProcCur.Status=ClaimProcStatus.Preauth;
				ClaimProcCur.FeeBilled=ProcCur.ProcFee;
				ClaimProcCur.PlanNum=FormIPS.SelectedPlan.PlanNum;
				if(FormIPS.SelectedPlan.UseAltCode)
					ClaimProcCur.CodeSent=((ProcedureCode)ProcedureCodes.HList[ProcCur.ADACode]).AlternateCode1;
				else{
					ClaimProcCur.CodeSent=ProcCur.ADACode;
					if(ClaimProcCur.CodeSent.Length>5 && ClaimProcCur.CodeSent.Substring(0,1)=="D"){
						ClaimProcCur.CodeSent=ClaimProcCur.CodeSent.Substring(0,5);
					}
				}
        ClaimProcCur.Insert();
				//ProcCur.Update(ProcOld);
			}
			ProcList=Procedures.Refresh(PatCur.PatNum);
			ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,Claims.Cur.ClaimNum);
			Claims.CalculateAndUpdate(ClaimProcList,ProcList,InsPlanList,Claims.Cur,PatPlanList,BenefitList);
			FormClaimEdit FormCE=new FormClaimEdit(PatCur,FamCur);
			//FormCE.CalculateEstimates(
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void gridPreAuth_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
      Claims.Cur=(Claim)ALPreAuth[e.Row];
 			FormClaimEdit FormC=new FormClaimEdit(PatCur,FamCur);
      //FormClaimEdit2.IsPreAuth=true;
			FormC.ShowDialog();
			if(FormC.DialogResult!=DialogResult.OK){
				return;
			}
			ModuleSelected(PatCur.PatNum);    
		}

		private void gridPreAuth_CellClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			if(gridPlans.SelectedIndices[0]!=0){
				return;
			}
			gridMain.SetSelected(false);
			Claims.Cur=(Claim)ALPreAuth[e.Row];
			ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,Claims.Cur.ClaimNum);
			for(int i=0;i<ProcListTP.Length;i++){
				for(int j=0;j<ClaimProcsForClaim.Length;j++){
					if(ProcListTP[i].ProcNum==ClaimProcsForClaim[j].ProcNum){
						gridMain.SetSelected(i,true);
					}
				}
			}
		}

		

		

		


		
	}

	
}
