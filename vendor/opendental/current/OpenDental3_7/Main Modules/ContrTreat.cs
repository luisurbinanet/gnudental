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
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private OpenDental.TableTP tbMain;
		private System.ComponentModel.IContainer components;// Required designer variable.
		///<summary>The procedures to show in the list.</summary>
		private ArrayList arrayLProc;
		private System.Windows.Forms.ListBox listSetPr;
		///<summary></summary>
		public static ArrayList TPLines2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textFee;
		private System.Windows.Forms.TextBox textPriIns;
		private System.Windows.Forms.TextBox textSecIns;
		private System.Windows.Forms.TextBox textPat;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Panel panelSide;
		private System.Windows.Forms.ListBox listViewPr;
		private OpenDental.UI.Button butSelectAll;
		//private bool[] selectedPrs;//had to use this because of deficiency in available Listbox events.
		///<summary>If set to true, procedures will show that have a priority which is hidden in Defs. This keeps procedures from disappearing permanantly because by viewing "all" it will include even hidden.</summary>
		private bool showHidden;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.ListBox listPreAuth;
		private int linesPrinted=0;
		//bool middleOfPlan=false;
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
		//private	int xTbPos=0;
	  //private	int yTbPos=0;
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
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox textToday;
		private System.Windows.Forms.ImageList imageListMain;
		private OpenDental.UI.ODToolBar ToolBarMain;
    private ArrayList ALPreAuth;
		private Procedure[] ProcList;
		private System.Windows.Forms.ContextMenu menuPatient;
		private System.Windows.Forms.CheckBox checkShowCompleted;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkShowIns;
		private ClaimProc[] ClaimProcList;
		private Family FamCur;
		private Patient PatCur;
		private System.Windows.Forms.CheckBox checkShowFees;
		private InsPlan[] PlanList;
		///<summary></summary>
		[Category("Data"),Description("Occurs when user changes current patient, usually by clicking on the Select Patient button.")]
		public event PatientSelectedEventHandler PatientSelected=null;

		///<summary></summary>
		public ContrTreat(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			tbMain.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellClicked);
			tbMain.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellDoubleClicked);

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
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tbMain = new OpenDental.TableTP();
			this.label5 = new System.Windows.Forms.Label();
			this.textFee = new System.Windows.Forms.TextBox();
			this.textPriIns = new System.Windows.Forms.TextBox();
			this.textSecIns = new System.Windows.Forms.TextBox();
			this.textPat = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.panelSide = new System.Windows.Forms.Panel();
			this.butSelectAll = new OpenDental.UI.Button();
			this.listViewPr = new System.Windows.Forms.ListBox();
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
			this.listPreAuth = new System.Windows.Forms.ListBox();
			this.label17 = new System.Windows.Forms.Label();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.textPriDedRem = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.textSecDedRem = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.textToday = new System.Windows.Forms.TextBox();
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.menuPatient = new System.Windows.Forms.ContextMenu();
			this.panelSide.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(19, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "Set";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// listSetPr
			// 
			this.listSetPr.Location = new System.Drawing.Point(26, 40);
			this.listSetPr.Name = "listSetPr";
			this.listSetPr.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listSetPr.Size = new System.Drawing.Size(66, 186);
			this.listSetPr.TabIndex = 5;
			this.listSetPr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listSetPr_MouseDown);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(111, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 18);
			this.label2.TabIndex = 6;
			this.label2.Text = "View";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(42, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(123, 17);
			this.label4.TabIndex = 15;
			this.label4.Text = "Priorities";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// tbMain
			// 
			this.tbMain.BackColor = System.Drawing.SystemColors.Window;
			this.tbMain.Location = new System.Drawing.Point(2, 32);
			this.tbMain.Name = "tbMain";
			this.tbMain.ScrollValue = 1;
			this.tbMain.SelectedIndices = new int[0];
			this.tbMain.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbMain.Size = new System.Drawing.Size(679, 548);
			this.tbMain.TabIndex = 18;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(380, 600);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(36, 14);
			this.label5.TabIndex = 19;
			this.label5.Text = "Totals";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFee
			// 
			this.textFee.Location = new System.Drawing.Point(420, 596);
			this.textFee.Name = "textFee";
			this.textFee.ReadOnly = true;
			this.textFee.Size = new System.Drawing.Size(50, 20);
			this.textFee.TabIndex = 20;
			this.textFee.Text = "";
			this.textFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPriIns
			// 
			this.textPriIns.Location = new System.Drawing.Point(470, 596);
			this.textPriIns.Name = "textPriIns";
			this.textPriIns.ReadOnly = true;
			this.textPriIns.Size = new System.Drawing.Size(50, 20);
			this.textPriIns.TabIndex = 21;
			this.textPriIns.Text = "";
			this.textPriIns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textSecIns
			// 
			this.textSecIns.Location = new System.Drawing.Point(520, 596);
			this.textSecIns.Name = "textSecIns";
			this.textSecIns.ReadOnly = true;
			this.textSecIns.Size = new System.Drawing.Size(50, 20);
			this.textSecIns.TabIndex = 22;
			this.textSecIns.Text = "";
			this.textSecIns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textPat
			// 
			this.textPat.Location = new System.Drawing.Point(570, 596);
			this.textPat.Name = "textPat";
			this.textPat.ReadOnly = true;
			this.textPat.Size = new System.Drawing.Size(50, 20);
			this.textPat.TabIndex = 23;
			this.textPat.Text = "";
			this.textPat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(620, 596);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(66, 20);
			this.textBox5.TabIndex = 24;
			this.textBox5.Text = "just for alignment";
			this.textBox5.Visible = false;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(424, 582);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 14);
			this.label6.TabIndex = 25;
			this.label6.Text = "Fee";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(574, 582);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 14);
			this.label7.TabIndex = 26;
			this.label7.Text = "Pat";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(522, 582);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(44, 14);
			this.label8.TabIndex = 27;
			this.label8.Text = "Sec Ins";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.Location = new System.Drawing.Point(470, 582);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(44, 14);
			this.label9.TabIndex = 28;
			this.label9.Text = "Pri Ins";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// panelSide
			// 
			this.panelSide.Controls.Add(this.butSelectAll);
			this.panelSide.Controls.Add(this.listViewPr);
			this.panelSide.Controls.Add(this.listSetPr);
			this.panelSide.Controls.Add(this.label1);
			this.panelSide.Controls.Add(this.label2);
			this.panelSide.Controls.Add(this.groupBox1);
			this.panelSide.Controls.Add(this.label4);
			this.panelSide.Location = new System.Drawing.Point(704, 54);
			this.panelSide.Name = "panelSide";
			this.panelSide.Size = new System.Drawing.Size(215, 337);
			this.panelSide.TabIndex = 29;
			// 
			// butSelectAll
			// 
			this.butSelectAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSelectAll.Autosize = true;
			this.butSelectAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSelectAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSelectAll.Location = new System.Drawing.Point(114, 230);
			this.butSelectAll.Name = "butSelectAll";
			this.butSelectAll.Size = new System.Drawing.Size(68, 23);
			this.butSelectAll.TabIndex = 17;
			this.butSelectAll.Text = "All";
			this.butSelectAll.Click += new System.EventHandler(this.butSelectAll_Click);
			// 
			// listViewPr
			// 
			this.listViewPr.Location = new System.Drawing.Point(114, 40);
			this.listViewPr.Name = "listViewPr";
			this.listViewPr.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listViewPr.Size = new System.Drawing.Size(68, 186);
			this.listViewPr.TabIndex = 16;
			this.listViewPr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewPr_MouseUp);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkShowFees);
			this.groupBox1.Controls.Add(this.checkShowIns);
			this.groupBox1.Controls.Add(this.checkShowCompleted);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(7, 257);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 74);
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
			this.checkShowFees.Size = new System.Drawing.Size(178, 17);
			this.checkShowFees.TabIndex = 20;
			this.checkShowFees.Text = "Fees";
			// 
			// checkShowIns
			// 
			this.checkShowIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowIns.Location = new System.Drawing.Point(15, 50);
			this.checkShowIns.Name = "checkShowIns";
			this.checkShowIns.Size = new System.Drawing.Size(178, 17);
			this.checkShowIns.TabIndex = 19;
			this.checkShowIns.Text = "Insurance Estimates";
			// 
			// checkShowCompleted
			// 
			this.checkShowCompleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowCompleted.Location = new System.Drawing.Point(15, 16);
			this.checkShowCompleted.Name = "checkShowCompleted";
			this.checkShowCompleted.Size = new System.Drawing.Size(178, 17);
			this.checkShowCompleted.TabIndex = 18;
			this.checkShowCompleted.Text = "Completed Treatment";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(788, 414);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60, 15);
			this.label10.TabIndex = 31;
			this.label10.Text = "Primary";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(708, 435);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(78, 15);
			this.label11.TabIndex = 32;
			this.label11.Text = "Annual Max";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(708, 456);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(79, 15);
			this.label12.TabIndex = 33;
			this.label12.Text = "Deductible";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(708, 498);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(79, 13);
			this.label13.TabIndex = 34;
			this.label13.Text = "Ins Used";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(708, 536);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(79, 14);
			this.label14.TabIndex = 35;
			this.label14.Text = "Remaining";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(707, 516);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(81, 14);
			this.label15.TabIndex = 36;
			this.label15.Text = "Pending";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(856, 414);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(60, 14);
			this.label16.TabIndex = 37;
			this.label16.Text = "Secondary";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textPriMax
			// 
			this.textPriMax.BackColor = System.Drawing.Color.White;
			this.textPriMax.Location = new System.Drawing.Point(788, 433);
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
			this.textSecUsed.Location = new System.Drawing.Point(856, 493);
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
			this.textSecDed.Location = new System.Drawing.Point(856, 453);
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
			this.textSecMax.Location = new System.Drawing.Point(856, 433);
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
			this.textPriRem.Location = new System.Drawing.Point(788, 533);
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
			this.textPriPend.Location = new System.Drawing.Point(788, 513);
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
			this.textPriUsed.Location = new System.Drawing.Point(788, 493);
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
			this.textPriDed.Location = new System.Drawing.Point(788, 453);
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
			this.textSecRem.Location = new System.Drawing.Point(856, 533);
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
			this.textSecPend.Location = new System.Drawing.Point(856, 513);
			this.textSecPend.Name = "textSecPend";
			this.textSecPend.ReadOnly = true;
			this.textSecPend.Size = new System.Drawing.Size(60, 20);
			this.textSecPend.TabIndex = 47;
			this.textSecPend.Text = "";
			this.textSecPend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// listPreAuth
			// 
			this.listPreAuth.Enabled = false;
			this.listPreAuth.Location = new System.Drawing.Point(692, 584);
			this.listPreAuth.Name = "listPreAuth";
			this.listPreAuth.Size = new System.Drawing.Size(246, 108);
			this.listPreAuth.TabIndex = 48;
			this.listPreAuth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listPreAuth_MouseDown);
			this.listPreAuth.DoubleClick += new System.EventHandler(this.listPreAuth_DoubleClick);
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(691, 567);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(187, 13);
			this.label17.TabIndex = 49;
			this.label17.Text = "PreAuthorizations";
			// 
			// pd2
			// 
			this.pd2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd2_PrintPage);
			// 
			// textPriDedRem
			// 
			this.textPriDedRem.BackColor = System.Drawing.Color.White;
			this.textPriDedRem.Location = new System.Drawing.Point(788, 473);
			this.textPriDedRem.Name = "textPriDedRem";
			this.textPriDedRem.ReadOnly = true;
			this.textPriDedRem.Size = new System.Drawing.Size(60, 20);
			this.textPriDedRem.TabIndex = 51;
			this.textPriDedRem.Text = "";
			this.textPriDedRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(692, 477);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(96, 15);
			this.label18.TabIndex = 50;
			this.label18.Text = "Deduct Remain";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textSecDedRem
			// 
			this.textSecDedRem.BackColor = System.Drawing.Color.White;
			this.textSecDedRem.Location = new System.Drawing.Point(856, 473);
			this.textSecDedRem.Name = "textSecDedRem";
			this.textSecDedRem.ReadOnly = true;
			this.textSecDedRem.Size = new System.Drawing.Size(60, 20);
			this.textSecDedRem.TabIndex = 52;
			this.textSecDedRem.Text = "";
			this.textSecDedRem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(802, 399);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 12);
			this.label3.TabIndex = 53;
			this.label3.Text = "Insurance";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textNote
			// 
			this.textNote.BackColor = System.Drawing.Color.White;
			this.textNote.Location = new System.Drawing.Point(4, 584);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.ReadOnly = true;
			this.textNote.Size = new System.Drawing.Size(346, 114);
			this.textNote.TabIndex = 54;
			this.textNote.Text = "";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(701, 35);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(69, 15);
			this.label19.TabIndex = 55;
			this.label19.Text = "Today:";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textToday
			// 
			this.textToday.BackColor = System.Drawing.Color.White;
			this.textToday.Location = new System.Drawing.Point(773, 33);
			this.textToday.Name = "textToday";
			this.textToday.ReadOnly = true;
			this.textToday.Size = new System.Drawing.Size(71, 20);
			this.textToday.TabIndex = 56;
			this.textToday.Text = "";
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
			// imageListMain
			// 
			this.imageListMain.ImageSize = new System.Drawing.Size(22, 22);
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ContrTreat
			// 
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.textToday);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.tbMain);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textSecDedRem);
			this.Controls.Add(this.textPriDedRem);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.listPreAuth);
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
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.textPat);
			this.Controls.Add(this.textSecIns);
			this.Controls.Add(this.textPriIns);
			this.Controls.Add(this.textFee);
			this.Controls.Add(this.label5);
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
			showHidden=true;//shows hidden priorities
			//can't use Lan.F(this);
			Lan.C(this,new Control[]
				{
				label19,
				label4,
				label1,
				label2,
				butSelectAll,
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
				label17,
				label6,
				label9,
				label8,
				label7,
				label5
				});
			LayoutToolBar();
		}

		///<summary>Called every time local data is changed from any workstation.  Refreshes priority lists and lays out the toolbar.</summary>
		public void InitializeLocalData(){
			listSetPr.Items.Clear();
			listViewPr.Items.Clear();
			listSetPr.Items.Add(Lan.g(this,"no priority"));
			listViewPr.Items.Add(Lan.g(this,"no priority"));
			listViewPr.SetSelected(0,true);
			for(int i=0;i<Defs.Short[(int)DefCat.TxPriorities].Length;i++){
				listSetPr.Items.Add(Defs.Short[(int)DefCat.TxPriorities][i].ItemName);
				listViewPr.Items.Add(Defs.Short[(int)DefCat.TxPriorities][i].ItemName);
				listViewPr.SetSelected(i+1,true);
			}
			textNote.Text=((Pref)Prefs.HList["TreatmentPlanNote"]).ValueString;
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
			//MessageBox.Show("module selected");
			RefreshModuleData(patNum);
			RefreshModuleScreen();
		}

		///<summary></summary>
		public void ModuleUnselected(){
			FamCur=null;
			PatCur=null;
			PlanList=null;
			CovPats.List=null;
			Claims.List=null;
			Claims.HList=null;
			//ClaimProcs.List=null;
			//from FillMain:
			ProcList=null;
			//Procedures.HList=null;
			//Procedures.MissingTeeth=null;
		}

		private void RefreshModuleData(int patNum){
			if(patNum!=0){
				FamCur=Patients.GetFamily(patNum);
				PatCur=FamCur.GetPatient(patNum);
				PlanList=InsPlans.Refresh(FamCur);
				CovPats.Refresh(PatCur,PlanList);
				Claims.Refresh(PatCur.PatNum);
        Fees.Refresh();
        ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			}
		}

		private void RefreshModuleScreen(){
			if(PatCur!=null){
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString+" - "
					+PatCur.GetNameLF();
				tbMain.Enabled=true;
				panelSide.Enabled=true;
				ToolBarMain.Buttons["PreAuth"].Enabled=true;
				ToolBarMain.Buttons["Update"].Enabled=true;
				ToolBarMain.Buttons["Print"].Enabled=true;
				ToolBarMain.Invalidate();
        listPreAuth.Enabled=true;
			}
			else{
				ParentForm.Text=((Pref)Prefs.HList["MainWindowTitle"]).ValueString;
				tbMain.Enabled=false;
				panelSide.Enabled=false;
				ToolBarMain.Buttons["PreAuth"].Enabled=false;
				ToolBarMain.Buttons["Update"].Enabled=false;
				ToolBarMain.Buttons["Print"].Enabled=false;
				ToolBarMain.Invalidate();
        listPreAuth.Enabled=false;
			}
			FillPatientButton();
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

		private void FillMain(){
			//tbMain.SelectedRows=new int[0];
			if(PatCur==null){
				tbMain.ResetRows(0);
				tbMain.LayoutTables();
				return;
			}
			double totFee=0;
			double totPriIns=0;
			double totSecIns=0;
			double totPat=0;
			//step through all procedures for patient and move selected ones (TP) to
			//arrayLProc ordered by priority,toothNum.
			//Pull into TPLines2 arrayList for display.  Every TPLines2 entry
			//contains an index to access original arrayList.
			//Procedures.List is never used again after initial passthrough
			//Notes will be handled like any
			//other line, just no numbers(eventually)
			ProcList=Procedures.Refresh(PatCur.PatNum);
			arrayLProc=new ArrayList();
			bool doAdd;
			int iPriority;
			int oPriority;
			int iToothInt;
			int oToothInt;
			for(int i=0;i<ProcList.Length;i++){
				doAdd=false;
				if(ProcList[i].ProcStatus==ProcStat.TP){
					//if priority is 0 && "no priority" selected
					if(ProcList[i].Priority==0){
						if(listViewPr.SelectedIndices.Contains(0))
							doAdd=true;
					}
					else{//any priority other than 0
						//if priority is hidden && showHidden
						if(Defs.GetOrder(DefCat.TxPriorities,ProcList[i].Priority)==-1 && showHidden)
							doAdd=true;
						//or if priority not hidden && priority selected
						else if(Defs.GetOrder(DefCat.TxPriorities,ProcList[i].Priority)!=-1
							//&& selectedPrs[Defs.GetOrder(DefCat.TxPriorities,Procedures.List[i].Priority)])
							&& listViewPr.SelectedIndices.Contains
							(Defs.GetOrder(DefCat.TxPriorities,ProcList[i].Priority)+1))
							doAdd=true;
					}
				}
				if(!doAdd){
					continue;
				}
				if(arrayLProc.Count==0)//first procedure simple add
					arrayLProc.Add(ProcList[i]);
				else{//after that, figure out where to place the procedure to order things properly
					iPriority=Defs.GetOrder(DefCat.TxPriorities,ProcList[i].Priority);
					if(Tooth.IsValidDB(ProcList[i].ToothNum))
						iToothInt=Tooth.ToInt(ProcList[i].ToothNum);
					else
						iToothInt=0;
					for(int o=0;o<arrayLProc.Count;o++){
						oPriority=Defs.GetOrder(DefCat.TxPriorities,((Procedure)arrayLProc[o]).Priority);
						if(Tooth.IsValidDB(((Procedure)arrayLProc[o]).ToothNum))
							oToothInt=Tooth.ToInt(((Procedure)arrayLProc[o]).ToothNum);
						else
							oToothInt=0;
						if(iPriority==oPriority){
							if(iToothInt < oToothInt){
								arrayLProc.Insert(o,ProcList[i]);
								break;
							}
						}
						if(iPriority < oPriority){
							arrayLProc.Insert(o,ProcList[i]);
							break;
						}
						if(o==arrayLProc.Count-1){
							arrayLProc.Add(ProcList[i]);
							break;
						}
					}//end for
				}//end else
			}//end for Procedures.List	
			TPLines2 = new ArrayList();
			//This is where to transfer procedures and notes to TPLines2:
			TPLine tempTPLine;
			Color lineColor;
			for(int i=0;i<arrayLProc.Count;i++){
				tempTPLine=new TPLine();
				tempTPLine.Index=i;
				lineColor=Defs.GetColor(DefCat.TxPriorities,((Procedure)arrayLProc[i]).Priority);
				if(lineColor==Color.White)
					tempTPLine.LineColor=Color.Black;
				else tempTPLine.LineColor=lineColor;
				//if(Defs.GetOrder(DefCat.TxPriorities,((Procedure)arrayLProc[i]).Priority)==-1)
				//	tempTPLine.Priority="";
				tempTPLine.Priority=Defs.GetName(DefCat.TxPriorities,((Procedure)arrayLProc[i]).Priority);
				tempTPLine.Tooth=Tooth.ToInternat(((Procedure)arrayLProc[i]).ToothNum);
				tempTPLine.Surface=((Procedure)arrayLProc[i]).Surf;
				tempTPLine.Description=ProcedureCodes.GetProcCode(((Procedure)arrayLProc[i]).ADACode).Descript;
				tempTPLine.ADACode=((Procedure)arrayLProc[i]).ADACode;
				double fee=((Procedure)arrayLProc[i]).ProcFee;
				double priIns=((Procedure)arrayLProc[i]).GetEst(ClaimProcList,PriSecTot.Pri,PatCur);
				double secIns=((Procedure)arrayLProc[i]).GetEst(ClaimProcList,PriSecTot.Sec,PatCur);
				double pat=fee-priIns-secIns
					-((Procedure)arrayLProc[i]).GetWriteOff(ClaimProcList);
				if(pat<0)
					pat=0;
				totFee+=fee;
				totPriIns+=priIns;
				totSecIns+=secIns;
				totPat+=pat;
				tempTPLine.Fee   =fee.ToString("F");
				tempTPLine.PriIns=priIns.ToString("F");
				tempTPLine.SecIns=secIns.ToString("F");
				tempTPLine.Pat   =pat.ToString("F");
				tempTPLine.PreEst="";
				TPLines2.Add(tempTPLine);
				//countLine++;
			}
			//then fill tbMain:
			tbMain.ResetRows(TPLines2.Count);
			for (int i=0;i<TPLines2.Count; i++){
				tbMain.Cell[0,i]=((TPLine)TPLines2[i]).Priority;
				tbMain.Cell[1,i]=((TPLine)TPLines2[i]).Tooth;
				tbMain.Cell[2,i]=((TPLine)TPLines2[i]).Surface;
				tbMain.Cell[3,i]=((TPLine)TPLines2[i]).ADACode;
				tbMain.Cell[4,i]=((TPLine)TPLines2[i]).Description;
				tbMain.Cell[5,i]=((TPLine)TPLines2[i]).Fee;
				tbMain.Cell[6,i]=((TPLine)TPLines2[i]).PriIns;
				tbMain.Cell[7,i]=((TPLine)TPLines2[i]).SecIns;
				tbMain.Cell[8,i]=((TPLine)TPLines2[i]).Pat;
				tbMain.Cell[9,i]=((TPLine)TPLines2[i]).PreEst;
				tbMain.SetTextColorRow(i,((TPLine)TPLines2[i]).LineColor);
			}
			tbMain.SetGridColor(Color.LightGray);
			tbMain.LayoutTables();
			textFee.Text=totFee.ToString("F");
			textPriIns.Text=totPriIns.ToString("F");
			textSecIns.Text=totSecIns.ToString("F");
			textPat.Text=totPat.ToString("F");
			//MessageBox.Show("filled");
		}

		private void FillSummary(){
			textToday.Text=DateTime.Today.ToShortDateString();
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
			double remain=0;
			double pend=0;
			double used=0;
			InsPlan PlanCur;//=new InsPlan();
			if(PatCur.PriPlanNum!=0){
				PlanCur=InsPlans.GetPlan(PatCur.PriPlanNum,PlanList);
				//pending:
				pend=InsPlans.GetPending(ClaimProcList,DateTime.Today,PatCur.PriPlanNum,PlanList);
				textPriPend.Text=pend.ToString("F");
				//max, used, and remaining:
				if(PlanCur.AnnualMax==-1){//annual max is blank
					//used cannot display because there is no way to calculate it until all the math is reworked.
					textPriMax.Text="";
					textPriRem.Text="";
					textPriUsed.Text="";
				}
				else{
					max=PlanCur.AnnualMax;
					remain=InsPlans.GetInsRem(ClaimProcList,DateTime.Today,PatCur.PriPlanNum,-1,PlanList);
					used=max-remain-pend;//math done in reverse to take advantage of GetInsRem
					//fix later to: remain=max-used-pend
					textPriMax.Text=max.ToString("F");
					textPriRem.Text=remain.ToString("F");
					textPriUsed.Text=used.ToString("F");
				}
				//deductible:
				if(PlanCur.Deductible!=-1)
					textPriDed.Text=PlanCur.Deductible.ToString("F");
				textPriDedRem.Text=InsPlans.GetDedRem(ClaimProcList,DateTime.Today,PatCur.PriPlanNum
					,-1,PlanList).ToString("F");
			}
			if(PatCur.SecPlanNum!=0){
				PlanCur=InsPlans.GetPlan(PatCur.SecPlanNum,PlanList);
				pend=InsPlans.GetPending(ClaimProcList,DateTime.Today,PatCur.SecPlanNum,PlanList);
				textSecPend.Text=pend.ToString("F");
				if(PlanCur.AnnualMax==-1){//annual max is blank
					textSecMax.Text="";
					textSecRem.Text="";
					textSecUsed.Text="";
				}
				else{
					max=PlanCur.AnnualMax;
					remain=InsPlans.GetInsRem(ClaimProcList,DateTime.Today,PatCur.SecPlanNum,-1,PlanList);
					used=max-remain-pend;
					textSecMax.Text=max.ToString("F");
					textSecRem.Text=remain.ToString("F");
					textSecUsed.Text=used.ToString("F");
				}
				if(PlanCur.Deductible!=-1)
					textSecDed.Text=PlanCur.Deductible.ToString("F");
				textSecDedRem.Text=InsPlans.GetDedRem(ClaimProcList,DateTime.Today,
					PatCur.SecPlanNum,-1,PlanList).ToString("F");
			}
		}

    private void FillPreAuth(){
      if(PatCur==null){
				listPreAuth.Items.Clear();
				return;
			}
      ALPreAuth=new ArrayList();			
      for(int i=0;i<Claims.List.Length;i++){
        if(Claims.List[i].ClaimType=="PreAuth"){
          ALPreAuth.Add(Claims.List[i]);
        }
      }
      listPreAuth.Items.Clear();
			//selectedPreAuth=-1;
      for(int i=0;i<ALPreAuth.Count;i++){
				InsPlan PlanCur=InsPlans.GetPlan(((Claim)ALPreAuth[i]).PlanNum,PlanList);
        string itemText;
				if(((Claim)ALPreAuth[i]).DateSent.Year<1880)
					itemText=Carriers.GetName(PlanCur.CarrierNum)
						+"("+((Claim)ALPreAuth[i]).ClaimStatus.ToString()+")";
				else
					itemText=((Claim)ALPreAuth[i]).DateSent.ToShortDateString()+" "
						+Carriers.GetName(PlanCur.CarrierNum)
						+"("+((Claim)ALPreAuth[i]).ClaimStatus.ToString()+")"; 
        listPreAuth.Items.Add(itemText);
      } 
    }

		private void tbMain_CellClicked(object sender, CellEventArgs e){
			listPreAuth.SelectedIndex=-1;//is this a desirable behavior? 
		}

		private void tbMain_CellDoubleClicked(object sender, CellEventArgs e){
			Procedure ProcCur=(Procedure)arrayLProc[((TPLine)TPLines2[e.Row]).Index];
			FormProcEdit FormPE=new FormProcEdit(ProcCur,PatCur,FamCur,PlanList);
			FormPE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}


		private void listSetPr_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			int clickedRow=listSetPr.IndexFromPoint(e.X,e.Y);
			if(clickedRow==-1)
				return;
			Procedure ProcCur;
			Procedure ProcOld;
			for(int i=0;i<tbMain.SelectedIndices.Length;i++){//loop through the main list of selected procs
				ProcCur=(Procedure)arrayLProc[((TPLine)TPLines2[tbMain.SelectedIndices[i]]).Index];
				ProcOld=ProcCur.Copy();
				if(clickedRow==0)//set priority to "no priority"
					ProcCur.Priority=0;
				else
					ProcCur.Priority=Defs.Short[(int)DefCat.TxPriorities][clickedRow-1].DefNum;
				try{
					ProcCur.InsertOrUpdate(ProcOld,false);//no recall synch required
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
					continue;
				}
				//ProcCur.Update(ProcOld);
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void listViewPr_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listViewPr.SelectedIndices.Count==0){
				//all items were unselected
				showHidden=false;
			}
			ModuleSelected(PatCur.PatNum);
		}

		private void butSelectAll_Click(object sender, System.EventArgs e){
			//listViewPr.SetSelected(0,false);
			for(int i=0;i<listViewPr.Items.Count;i++){
				listViewPr.SuspendLayout();
				listViewPr.SetSelected(i,true);
				listViewPr.ResumeLayout();
				//selectedPrs[i-1]=true;
			}
			showHidden=true;
			ModuleSelected(PatCur.PatNum);
		}

		///<summary>Preview is only used for debugging.</summary>
		public void PrintReport(bool justPreview){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			PrintDocument tempPD = new PrintDocument();
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
				string heading=Lan.g(this,"PROPOSED TREATMENT PLAN");
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
					}
					//then add whatever is showing on the list
					for(int i=0;i<arrayLProc.Count;i++){
						procAL.Add((Procedure)arrayLProc[i]);
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
			if(checkShowIns.Checked){
				colPos=new int[11];
				colAlign=new HorizontalAlignment[10];
				ColCaption=new string[10];
				ColCaption[6]=Lan.g(this,"Pri Ins");
				ColCaption[7]=Lan.g(this,"Sec Ins");
				ColCaption[8]=Lan.g(this,"Pat Pay");
				ColCaption[9]=Lan.g(this,"Pre Est");
				colPos[0]=25;   colAlign[0]=HorizontalAlignment.Left;//priority
				colPos[1]=95;		colAlign[1]=HorizontalAlignment.Left;//tooth
				colPos[2]=140;  colAlign[2]=HorizontalAlignment.Left;//surf
				colPos[3]=200;  colAlign[3]=HorizontalAlignment.Left;//adacode
				colPos[4]=270;  colAlign[4]=HorizontalAlignment.Left;//description
				colPos[5]=485;  colAlign[5]=HorizontalAlignment.Right;//fee
				colPos[6]=545;  colAlign[6]=HorizontalAlignment.Right;//pri ins
				colPos[7]=595;  colAlign[7]=HorizontalAlignment.Right;//sec ins
				colPos[8]=655;  colAlign[8]=HorizontalAlignment.Right;//pat Pay
				colPos[9]=720;  colAlign[9]=HorizontalAlignment.Right;//pre est
				colPos[10]=780;
			}
			else{//don't show ins
				colPos=new int[7];
				colAlign=new HorizontalAlignment[6];
				ColCaption=new string[6];
				colPos[0]=145;  colAlign[0]=HorizontalAlignment.Left;//priority
				colPos[1]=215;	colAlign[1]=HorizontalAlignment.Left;//tooth
				colPos[2]=260;  colAlign[2]=HorizontalAlignment.Left;//surf
				colPos[3]=320;  colAlign[3]=HorizontalAlignment.Left;//adacode
				colPos[4]=390;  colAlign[4]=HorizontalAlignment.Left;//description
				colPos[5]=595;  colAlign[5]=HorizontalAlignment.Right;//fee
				colPos[6]=665;
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
				//this might be starting somewhere int the middle of the table
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
						e.Graphics.DrawString(Lan.g(this,ColCaption[i]),totalFont,Brushes.Black,colPos[i]+2,yPos+1);

				}
				yPos+=18;
				while(yPos < e.MarginBounds.Height-16 && linesPrinted < TPLines2.Count){
					for(int i=0;i<colPos.Length-1;i++){
						if(i==5 && !checkShowFees.Checked){
							continue;
						}
  					else if(colAlign[i]==HorizontalAlignment.Right){
							e.Graphics.DrawString
								(tbMain.Cell[i,linesPrinted].ToString()
								,bodyFont,new SolidBrush(tbMain.FontColor[i,linesPrinted])//Brushes.Black
								,colPos[i+1]-e.Graphics.MeasureString
								(tbMain.Cell[i,linesPrinted].ToString(),bodyFont).Width-1
								,yPos);
						}
						else{
							e.Graphics.DrawString(tbMain.Cell[i,linesPrinted].ToString(),bodyFont
								,new SolidBrush(tbMain.FontColor[i,linesPrinted])
								,new RectangleF(colPos[i]+2,yPos
								,colPos[i+1]-colPos[i]-5,bodyFont.GetHeight(e.Graphics)));
						}
						if(i>4)
							ColTotal[i]+=(float)(PIn.PDouble(tbMain.Cell[i,linesPrinted].ToString()));
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
				if(linesPrinted==TPLines2.Count){
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
			if(benefitsPrinted && !notePrinted && yPos < e.MarginBounds.Height-16*4){//1037
				e.Graphics.DrawRectangle(Pens.Gray,50,yPos,700,16*4);
				e.Graphics.DrawString(Lan.g(this,((Pref)Prefs.HList["TreatmentPlanNote"]).ValueString),bodyFont
					,Brushes.Black,new RectangleF(55f,yPos+5,695f,54f));
				notePrinted=true;
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
		  if(!MsgBox.Show(this,true,"Update all fees and insurance estimates on this treatment plan to the current fees for this patient?")){
        return;   
      }
			//if(PatCur.PriPlanNum>0){//has primary ins
			//	InsPlans.GetCur(PatCur.PriPlanNum);//in preparation for the capcopay changes
			//}
			Procedure procCur;
			Procedure procOld;
      for(int i=0;i<arrayLProc.Count;i++){
				procCur=(Procedure)arrayLProc[i];
				procOld=procCur.Copy();
				//first the fees
				procCur.ProcFee=Fees.GetAmount0(procCur.ADACode,Fees.GetFeeSched(PatCur,PlanList));
				procCur.ComputeEstimates(PatCur.PatNum,PatCur.PriPlanNum
					,PatCur.SecPlanNum,ClaimProcList,false,PatCur,PlanList);
				try{
					procCur.InsertOrUpdate(procOld,false);//no recall synch required 
				}
				catch(Exception ex){
					MessageBox.Show(ex.Message);
					continue;
				}
				//procCur.Update(procOld);
      }
      ModuleSelected(PatCur.PatNum);
		}

		private void OnPreAuth_Click() {
		  if(tbMain.SelectedIndices.Length==0){
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
			Claims.Cur.ProvTreat=((Procedure)arrayLProc[tbMain.SelectedIndices[0]]).ProvNum;
			for(int i=0;i<tbMain.SelectedIndices.Length;i++){
				if(!Providers.GetIsSec(((Procedure)arrayLProc[tbMain.SelectedIndices[i]]).ProvNum)){
					Claims.Cur.ProvTreat=((Procedure)arrayLProc[tbMain.SelectedIndices[i]]).ProvNum;
				}
			}
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
			//this loop adds the claimprocs but does not add any fees
			Procedure ProcCur;
			//Procedure ProcOld;
			ClaimProc ClaimProcCur;
			for(int i=0;i<tbMain.SelectedIndices.Length;i++){
				ProcCur=((Procedure)arrayLProc[tbMain.SelectedIndices[i]]);
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
			Claims.CalculateAndUpdate(ClaimProcList,ProcList,PatCur,PlanList,Claims.Cur);
			FormClaimEdit FormCE=new FormClaimEdit(PatCur,FamCur);
			//FormCE.CalculateEstimates(
			FormCE.IsNew=true;//this causes it to delete the claim if cancelling.
			FormCE.ShowDialog();
			ModuleSelected(PatCur.PatNum);
		}

		private void listPreAuth_DoubleClick(object sender, System.EventArgs e) {
			if(listPreAuth.SelectedIndex==-1){
				return;
			}
      Claims.Cur=(Claim)ALPreAuth[listPreAuth.SelectedIndex];
 			FormClaimEdit FormClaimEdit2=new FormClaimEdit(PatCur,FamCur);
      //FormClaimEdit2.IsPreAuth=true;
			FormClaimEdit2.ShowDialog();
			if(FormClaimEdit2.DialogResult!=DialogResult.OK){
				return;
			}
			ModuleSelected(PatCur.PatNum);     
		}


		private void listPreAuth_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listPreAuth.IndexFromPoint(e.X,e.Y)==-1){
				listPreAuth.SelectedIndex=-1;
				tbMain.SetSelected(false);
				return;
			}
			tbMain.SetSelected(false);
      Claims.Cur=(Claim)ALPreAuth[listPreAuth.SelectedIndex];
      ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,Claims.Cur.ClaimNum);
      for(int i=0;i<arrayLProc.Count;i++){
        for(int j=0;j<ClaimProcsForClaim.Length;j++){
          if(((Procedure)arrayLProc[i]).ProcNum==ClaimProcsForClaim[j].ProcNum){
						tbMain.SetSelected(i,true);
          }
        }
      }
		}

		

		


		
	}

	///<summary></summary>
	public struct TPLine{
		//public AcctType Type;
		///<summary></summary>
		public bool IsNote;
		///<summary></summary>
		public int Index;
		///<summary></summary>
		public string Priority;
		///<summary></summary>
		public string Tooth;
		///<summary></summary>
		public string Surface;
		///<summary></summary>
		public string Description;
		///<summary></summary>
		public string Fee;
		///<summary></summary>
		public string PriIns;
		///<summary></summary>
		public string SecIns;
		///<summary></summary>
		public string Pat;
		///<summary></summary>
		public string PreEst;
		///<summary></summary>
		public Color LineColor;
		///<summary></summary>
		public string ADACode;
	}
}
