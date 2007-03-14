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
	public class FormInsCovEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label13;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butOK;// Required designer variable.
		//public bool IsNew;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private OpenDental.TablePercent tbPercent1;
		private System.Windows.Forms.ListBox listPriPlan;
		private OpenDental.UI.Button butNone;
		private OpenDental.UI.Button butNoneSec;
		private System.Windows.Forms.ListBox listSecPlan;
		private OpenDental.TablePercent tbPercent2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox listPriAdj;
		private OpenDental.UI.Button butPriAdd;
		private ArrayList ALPriAdj;
		private OpenDental.UI.Button butSecAdd;
		private System.Windows.Forms.ListBox listSecAdj;
		private System.Windows.Forms.Label label5;
		private OpenDental.UI.Button butPriDelete;
		private OpenDental.UI.Button butSecDelete;
		private System.Windows.Forms.Label label9;
		private OpenDental.UI.Button butEditPriPlan;
		private OpenDental.UI.Button butEditSecPlan;
		private OpenDental.UI.Button butAddPlan;
		private OpenDental.UI.Button butExistPriPlan;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private OpenDental.UI.Button butExistSecPlan;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ListBox listPriRelat;
		private System.Windows.Forms.ListBox listSecRelat;
		private System.Windows.Forms.Label label6;
		private ArrayList ALSecAdj;
		private Patient PatCur;
		private Family FamCur;
		private int PatNum;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textPriPatID;
		private System.Windows.Forms.TextBox textSecPatID;//never changes
		private InsPlan[] PlanList;
		//private CovPat[] CovPatList;

		///<summary></summary>
		public FormInsCovEdit(int patNum){//Patient patCur,Family famCur,InsPlan[] planList){
			InitializeComponent();// Required for Windows Form Designer support
			PatNum=patNum;
			tbPercent1.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent1_CellClicked);
			tbPercent2.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent2_CellClicked);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormInsCovEdit));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textPriPatID = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.listPriRelat = new System.Windows.Forms.ListBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.butExistPriPlan = new OpenDental.UI.Button();
			this.butAddPlan = new OpenDental.UI.Button();
			this.butEditPriPlan = new OpenDental.UI.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.butPriDelete = new OpenDental.UI.Button();
			this.butPriAdd = new OpenDental.UI.Button();
			this.listPriAdj = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.butNone = new OpenDental.UI.Button();
			this.listPriPlan = new System.Windows.Forms.ListBox();
			this.tbPercent1 = new OpenDental.TablePercent();
			this.label1 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textSecPatID = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.listSecRelat = new System.Windows.Forms.ListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label11 = new System.Windows.Forms.Label();
			this.butExistSecPlan = new OpenDental.UI.Button();
			this.butEditSecPlan = new OpenDental.UI.Button();
			this.butSecDelete = new OpenDental.UI.Button();
			this.butSecAdd = new OpenDental.UI.Button();
			this.listSecAdj = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbPercent2 = new OpenDental.TablePercent();
			this.label2 = new System.Windows.Forms.Label();
			this.listSecPlan = new System.Windows.Forms.ListBox();
			this.butNoneSec = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textPriPatID);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.listPriRelat);
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Controls.Add(this.butEditPriPlan);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.butPriDelete);
			this.groupBox2.Controls.Add(this.butPriAdd);
			this.groupBox2.Controls.Add(this.listPriAdj);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.butNone);
			this.groupBox2.Controls.Add(this.listPriPlan);
			this.groupBox2.Controls.Add(this.tbPercent1);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(8, 10);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(385, 568);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Primary";
			// 
			// textPriPatID
			// 
			this.textPriPatID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textPriPatID.Location = new System.Drawing.Point(17, 300);
			this.textPriPatID.MaxLength = 100;
			this.textPriPatID.Name = "textPriPatID";
			this.textPriPatID.Size = new System.Drawing.Size(210, 20);
			this.textPriPatID.TabIndex = 99;
			this.textPriPatID.Text = "";
			this.textPriPatID.Leave += new System.EventHandler(this.textPriPatID_Leave);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(16, 277);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(159, 18);
			this.label4.TabIndex = 98;
			this.label4.Text = "Optional Patient ID";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listPriRelat
			// 
			this.listPriRelat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listPriRelat.Location = new System.Drawing.Point(17, 227);
			this.listPriRelat.Name = "listPriRelat";
			this.listPriRelat.Size = new System.Drawing.Size(134, 43);
			this.listPriRelat.TabIndex = 97;
			this.listPriRelat.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listPriRelat_MouseUp);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.butExistPriPlan);
			this.groupBox3.Controls.Add(this.butAddPlan);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(16, 109);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(252, 93);
			this.groupBox3.TabIndex = 96;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Add a plan to the list above";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(106, 26);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(139, 19);
			this.label10.TabIndex = 96;
			this.label10.Text = "From another family";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butExistPriPlan
			// 
			this.butExistPriPlan.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butExistPriPlan.Autosize = true;
			this.butExistPriPlan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExistPriPlan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExistPriPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butExistPriPlan.Location = new System.Drawing.Point(13, 23);
			this.butExistPriPlan.Name = "butExistPriPlan";
			this.butExistPriPlan.Size = new System.Drawing.Size(92, 26);
			this.butExistPriPlan.TabIndex = 95;
			this.butExistPriPlan.Text = "E&xisting";
			this.butExistPriPlan.Click += new System.EventHandler(this.butExistPriPlan_Click);
			// 
			// butAddPlan
			// 
			this.butAddPlan.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddPlan.Autosize = true;
			this.butAddPlan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddPlan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butAddPlan.Image = ((System.Drawing.Image)(resources.GetObject("butAddPlan.Image")));
			this.butAddPlan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddPlan.Location = new System.Drawing.Point(13, 57);
			this.butAddPlan.Name = "butAddPlan";
			this.butAddPlan.Size = new System.Drawing.Size(92, 26);
			this.butAddPlan.TabIndex = 91;
			this.butAddPlan.Text = "&New";
			this.butAddPlan.Visible = false;
			this.butAddPlan.Click += new System.EventHandler(this.butAddPlan_Click);
			// 
			// butEditPriPlan
			// 
			this.butEditPriPlan.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditPriPlan.Autosize = true;
			this.butEditPriPlan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditPriPlan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditPriPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butEditPriPlan.Image = ((System.Drawing.Image)(resources.GetObject("butEditPriPlan.Image")));
			this.butEditPriPlan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEditPriPlan.Location = new System.Drawing.Point(276, 133);
			this.butEditPriPlan.Name = "butEditPriPlan";
			this.butEditPriPlan.Size = new System.Drawing.Size(95, 26);
			this.butEditPriPlan.TabIndex = 93;
			this.butEditPriPlan.Text = "&Edit Plan";
			this.butEditPriPlan.Visible = false;
			this.butEditPriPlan.Click += new System.EventHandler(this.butEditPriPlan_Click);
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.Location = new System.Drawing.Point(14, 28);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(284, 14);
			this.label9.TabIndex = 92;
			this.label9.Text = "Highlight an insurance plan from the family list:";
			// 
			// butPriDelete
			// 
			this.butPriDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPriDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butPriDelete.Autosize = true;
			this.butPriDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPriDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPriDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butPriDelete.Location = new System.Drawing.Point(297, 450);
			this.butPriDelete.Name = "butPriDelete";
			this.butPriDelete.Size = new System.Drawing.Size(59, 20);
			this.butPriDelete.TabIndex = 90;
			this.butPriDelete.Text = "Delete";
			this.butPriDelete.Click += new System.EventHandler(this.butPriDelete_Click);
			// 
			// butPriAdd
			// 
			this.butPriAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPriAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butPriAdd.Autosize = true;
			this.butPriAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPriAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPriAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butPriAdd.Location = new System.Drawing.Point(228, 450);
			this.butPriAdd.Name = "butPriAdd";
			this.butPriAdd.Size = new System.Drawing.Size(59, 20);
			this.butPriAdd.TabIndex = 89;
			this.butPriAdd.Text = "Add";
			this.butPriAdd.Click += new System.EventHandler(this.butPriAdd_Click);
			// 
			// listPriAdj
			// 
			this.listPriAdj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listPriAdj.Items.AddRange(new object[] {
																										"03/05/2001       Ins Used:  $124.00       Ded Used:  $50.00",
																										"03/05/2002       Ins Used:  $0.00       Ded Used:  $50.00"});
			this.listPriAdj.Location = new System.Drawing.Point(15, 474);
			this.listPriAdj.Name = "listPriAdj";
			this.listPriAdj.Size = new System.Drawing.Size(341, 56);
			this.listPriAdj.TabIndex = 4;
			this.listPriAdj.DoubleClick += new System.EventHandler(this.listPriAdj_DoubleClick);
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(13, 453);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(218, 17);
			this.label8.TabIndex = 87;
			this.label8.Text = "Adjustments to Insurance Benefits: ";
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butNone
			// 
			this.butNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butNone.Autosize = true;
			this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butNone.Location = new System.Drawing.Point(306, 26);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(63, 20);
			this.butNone.TabIndex = 82;
			this.butNone.Text = "None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// listPriPlan
			// 
			this.listPriPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listPriPlan.Location = new System.Drawing.Point(16, 48);
			this.listPriPlan.Name = "listPriPlan";
			this.listPriPlan.ScrollAlwaysVisible = true;
			this.listPriPlan.Size = new System.Drawing.Size(353, 56);
			this.listPriPlan.TabIndex = 0;
			this.listPriPlan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listPriPlan_MouseDown);
			// 
			// tbPercent1
			// 
			this.tbPercent1.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercent1.Location = new System.Drawing.Point(17, 345);
			this.tbPercent1.Name = "tbPercent1";
			this.tbPercent1.ScrollValue = 1;
			this.tbPercent1.SelectedIndices = new int[0];
			this.tbPercent1.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercent1.Size = new System.Drawing.Size(242, 86);
			this.tbPercent1.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 322);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(361, 18);
			this.label1.TabIndex = 79;
			this.label1.Text = "Override percentages for patient (single click to edit):";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.Location = new System.Drawing.Point(16, 210);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(148, 14);
			this.label13.TabIndex = 77;
			this.label13.Text = "Relationship to Subscriber";
			this.label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butOK.Location = new System.Drawing.Point(743, 592);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 25);
			this.butOK.TabIndex = 101;
			this.butOK.Text = "&Close";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.textSecPatID);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.listSecRelat);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.groupBox4);
			this.groupBox1.Controls.Add(this.butEditSecPlan);
			this.groupBox1.Controls.Add(this.butSecDelete);
			this.groupBox1.Controls.Add(this.butSecAdd);
			this.groupBox1.Controls.Add(this.listSecAdj);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.tbPercent2);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.listSecPlan);
			this.groupBox1.Controls.Add(this.butNoneSec);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(446, 10);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(385, 568);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Secondary";
			// 
			// textSecPatID
			// 
			this.textSecPatID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textSecPatID.Location = new System.Drawing.Point(17, 300);
			this.textSecPatID.MaxLength = 100;
			this.textSecPatID.Name = "textSecPatID";
			this.textSecPatID.Size = new System.Drawing.Size(210, 20);
			this.textSecPatID.TabIndex = 101;
			this.textSecPatID.Text = "";
			this.textSecPatID.Leave += new System.EventHandler(this.textSecPatID_Leave);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(16, 277);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(159, 18);
			this.label7.TabIndex = 100;
			this.label7.Text = "Optional Patient ID";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listSecRelat
			// 
			this.listSecRelat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listSecRelat.Location = new System.Drawing.Point(17, 227);
			this.listSecRelat.Name = "listSecRelat";
			this.listSecRelat.Size = new System.Drawing.Size(134, 43);
			this.listSecRelat.TabIndex = 99;
			this.listSecRelat.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listSecRelat_MouseUp);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(16, 210);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(148, 14);
			this.label6.TabIndex = 98;
			this.label6.Text = "Relationship to Subscriber";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Controls.Add(this.butExistSecPlan);
			this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox4.Location = new System.Drawing.Point(16, 110);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(252, 62);
			this.groupBox4.TabIndex = 97;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Add a plan to the list above";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(107, 27);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(139, 19);
			this.label11.TabIndex = 97;
			this.label11.Text = "From another family";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butExistSecPlan
			// 
			this.butExistSecPlan.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butExistSecPlan.Autosize = true;
			this.butExistSecPlan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butExistSecPlan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butExistSecPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butExistSecPlan.Location = new System.Drawing.Point(13, 24);
			this.butExistSecPlan.Name = "butExistSecPlan";
			this.butExistSecPlan.Size = new System.Drawing.Size(92, 26);
			this.butExistSecPlan.TabIndex = 95;
			this.butExistSecPlan.Text = "Existing";
			this.butExistSecPlan.Click += new System.EventHandler(this.butExistSecPlan_Click);
			// 
			// butEditSecPlan
			// 
			this.butEditSecPlan.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEditSecPlan.Autosize = true;
			this.butEditSecPlan.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditSecPlan.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditSecPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butEditSecPlan.Image = ((System.Drawing.Image)(resources.GetObject("butEditSecPlan.Image")));
			this.butEditSecPlan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEditSecPlan.Location = new System.Drawing.Point(276, 133);
			this.butEditSecPlan.Name = "butEditSecPlan";
			this.butEditSecPlan.Size = new System.Drawing.Size(95, 26);
			this.butEditSecPlan.TabIndex = 96;
			this.butEditSecPlan.Text = "Edit Plan";
			this.butEditSecPlan.Visible = false;
			this.butEditSecPlan.Click += new System.EventHandler(this.butEditSecPlan_Click);
			// 
			// butSecDelete
			// 
			this.butSecDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSecDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSecDelete.Autosize = true;
			this.butSecDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSecDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSecDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butSecDelete.Location = new System.Drawing.Point(296, 449);
			this.butSecDelete.Name = "butSecDelete";
			this.butSecDelete.Size = new System.Drawing.Size(59, 20);
			this.butSecDelete.TabIndex = 95;
			this.butSecDelete.Text = "Delete";
			this.butSecDelete.Click += new System.EventHandler(this.butSecDelete_Click);
			// 
			// butSecAdd
			// 
			this.butSecAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSecAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butSecAdd.Autosize = true;
			this.butSecAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSecAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSecAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butSecAdd.Location = new System.Drawing.Point(227, 449);
			this.butSecAdd.Name = "butSecAdd";
			this.butSecAdd.Size = new System.Drawing.Size(59, 20);
			this.butSecAdd.TabIndex = 94;
			this.butSecAdd.Text = "Add";
			this.butSecAdd.Click += new System.EventHandler(this.butSecAdd_Click);
			// 
			// listSecAdj
			// 
			this.listSecAdj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listSecAdj.Items.AddRange(new object[] {
																										"03/05/2001       Ins Used:  $124.00       Ded Used:  $50.00",
																										"03/05/2002       Ins Used:  $0.00       Ded Used:  $50.00"});
			this.listSecAdj.Location = new System.Drawing.Point(15, 473);
			this.listSecAdj.Name = "listSecAdj";
			this.listSecAdj.Size = new System.Drawing.Size(341, 56);
			this.listSecAdj.TabIndex = 4;
			this.listSecAdj.DoubleClick += new System.EventHandler(this.listSecAdj_DoubleClick);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(13, 453);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(218, 17);
			this.label5.TabIndex = 92;
			this.label5.Text = "Adjustments to Insurance Benefits: ";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// tbPercent2
			// 
			this.tbPercent2.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercent2.Location = new System.Drawing.Point(16, 345);
			this.tbPercent2.Name = "tbPercent2";
			this.tbPercent2.ScrollValue = 1;
			this.tbPercent2.SelectedIndices = new int[0];
			this.tbPercent2.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercent2.Size = new System.Drawing.Size(242, 86);
			this.tbPercent2.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 322);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(361, 18);
			this.label2.TabIndex = 85;
			this.label2.Text = "Override percentages for patient (single click to edit):";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listSecPlan
			// 
			this.listSecPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listSecPlan.Location = new System.Drawing.Point(16, 48);
			this.listSecPlan.Name = "listSecPlan";
			this.listSecPlan.ScrollAlwaysVisible = true;
			this.listSecPlan.Size = new System.Drawing.Size(353, 56);
			this.listSecPlan.TabIndex = 0;
			this.listSecPlan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listSecPlan_MouseDown);
			// 
			// butNoneSec
			// 
			this.butNoneSec.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNoneSec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butNoneSec.Autosize = true;
			this.butNoneSec.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNoneSec.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNoneSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butNoneSec.Location = new System.Drawing.Point(306, 25);
			this.butNoneSec.Name = "butNoneSec";
			this.butNoneSec.Size = new System.Drawing.Size(63, 20);
			this.butNoneSec.TabIndex = 83;
			this.butNoneSec.Text = "None";
			this.butNoneSec.Click += new System.EventHandler(this.butNoneSec_Click);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(14, 28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(283, 14);
			this.label3.TabIndex = 78;
			this.label3.Text = "Highlight an insurance plan from the family list:";
			// 
			// FormInsCovEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butOK;
			this.ClientSize = new System.Drawing.Size(857, 639);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupBox2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsCovEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Insurance Coverage";
			this.Load += new System.EventHandler(this.FormInsCovEdit_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsCovEdit_Load(object sender, System.EventArgs e) {
			if(Prefs.GetBool("CustomizedForPracticeWeb")){
				butAddPlan.Visible=true;
			}
			FillAll();
		}

		///<summary>Refreshes Fam,Pat,InsPlans,CovPats, and Fills form again.</summary>
		private void FillAll(){
			FamCur=Patients.GetFamily(PatNum);
			PatCur=FamCur.GetPatient(PatNum);
			PlanList=InsPlans.Refresh(FamCur);
			CovPats.Refresh(PatCur,PlanList);
			FillPlans();
			InitialFillRelats();
			FillPercent();
			FillAdj();
		}

		///<summary></summary>
		private void FillPlans(){
			listPriPlan.Items.Clear();
			for(int i=0;i<PlanList.Length;i++){
				listPriPlan.Items.Add(InsPlans.GetDescript(PlanList[i].PlanNum,FamCur,PlanList));
				if(PatCur.PriPlanNum==PlanList[i].PlanNum){
					listPriPlan.SelectedIndex=i;
				}
			}
			listSecPlan.Items.Clear();
			for(int i=0;i<PlanList.Length;i++){
				listSecPlan.Items.Add(InsPlans.GetDescript(PlanList[i].PlanNum,FamCur,PlanList));
				if(PatCur.SecPlanNum==PlanList[i].PlanNum){
					listSecPlan.SelectedIndex=i;
				}
			}
			if(PatCur.SecPlanNum!=0)
				butNone.Enabled=false;//prevents deleting primary if sec exists
			else
				butNone.Enabled=true;
		}

		private void InitialFillRelats(){
			listPriRelat.Items.Clear();
      //listPriRelat.Items.AddRange(Enum.GetNames(typeof(Relat))); //*Ann
      string[] enumPriRelat=Enum.GetNames(typeof(Relat)); //*Ann
      for(int i=0;i<enumPriRelat.Length;i++){ //*Ann
				listPriRelat.Items.Add(Lan.g("enumPriRelat",enumPriRelat[i])); //*Ann
      }
      listPriRelat.SelectedIndex=(int)PatCur.PriRelationship;
      listSecRelat.Items.Clear();
      //listSecRelat.Items.AddRange(Enum.GetNames(typeof(Relat))); //*Ann
      string[] enumSecRelat=Enum.GetNames(typeof(Relat)); //*Ann
      for(int i=0;i<enumSecRelat.Length;i++){//*Ann
				listSecRelat.Items.Add(Lan.g("enumSecRelat",enumSecRelat[i])); //*Ann
      }
      listSecRelat.SelectedIndex=(int)PatCur.SecRelationship;
			textPriPatID.Text=PatCur.PriPatID;
			textSecPatID.Text=PatCur.SecPatID;
		}

		///<summary></summary>
		private void FillPercent(){
			tbPercent1.ResetRows(CovPats.PriList.Length);
			tbPercent1.SetGridColor(Color.LightGray);
			for(int i=0;i<CovCats.ListShort.Length;i++){
				tbPercent1.Cell[0,i]=CovCats.ListShort[i].Description;
				tbPercent1.Cell[1,i]="";
			}
			for(int i=0;i<CovPats.List.Length;i++){
				if(CovPats.List[i].PriPatNum==PatCur.PatNum){
					tbPercent1.Cell[1,CovCats.GetOrderShort(CovPats.List[i].CovCatNum)]
						=CovPats.List[i].Percent.ToString();
				}
			}
			tbPercent1.LayoutTables();
			//sec:
			tbPercent2.ResetRows(CovPats.SecList.Length);
			tbPercent2.SetGridColor(Color.LightGray);
			for(int i=0;i<CovCats.ListShort.Length;i++){
				tbPercent2.Cell[0,i]=CovCats.ListShort[i].Description;
				tbPercent2.Cell[1,i]="";
			}
			for(int i=0;i<CovPats.List.Length;i++){
				if(CovPats.List[i].SecPatNum==PatCur.PatNum){
					tbPercent2.Cell[1,CovCats.GetOrderShort(CovPats.List[i].CovCatNum)]
						=CovPats.List[i].Percent.ToString();
				}
			}
			tbPercent2.LayoutTables();
		}

    private void FillAdj(){
			ClaimProc[] ClaimProcList=ClaimProcs.Refresh(PatCur.PatNum);
			//move selected claimprocs into ALAdj
			ALPriAdj=new ArrayList();
			ALSecAdj=new ArrayList();
			for(int i=0;i<ClaimProcList.Length;i++){
				if(ClaimProcList[i].PlanNum==PatCur.PriPlanNum
					&& ClaimProcList[i].Status==ClaimProcStatus.Adjustment){
					ALPriAdj.Add(ClaimProcList[i]);
				}
				if(ClaimProcList[i].PlanNum==PatCur.SecPlanNum
					&& ClaimProcList[i].Status==ClaimProcStatus.Adjustment){
					ALSecAdj.Add(ClaimProcList[i]);
				}
			}
			listPriAdj.Items.Clear();
			listSecAdj.Items.Clear();
			string s;
			for(int i=0;i<ALPriAdj.Count;i++){
				s=((ClaimProc)ALPriAdj[i]).ProcDate.ToShortDateString()+"       Ins Used:  "
					+((ClaimProc)ALPriAdj[i]).InsPayAmt.ToString("F")+"       Ded Used:  "
					+((ClaimProc)ALPriAdj[i]).DedApplied.ToString("F");
				listPriAdj.Items.Add(s);
			}
			for(int i=0;i<ALSecAdj.Count;i++){
				s=((ClaimProc)ALSecAdj[i]).ProcDate.ToShortDateString()+"       Ins Used:  "
					+((ClaimProc)ALSecAdj[i]).InsPayAmt.ToString("F")+"       Ded Used:  "
					+((ClaimProc)ALSecAdj[i]).DedApplied.ToString("F");
				listSecAdj.Items.Add(s);
			}
		}

		private void listPriRelat_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			Patient PatOld=PatCur.Copy();
			PatCur.PriRelationship=(Relat)listPriRelat.SelectedIndex;
			PatCur.Update(PatOld);
			FillAll();
		}

		private void listSecRelat_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			Patient PatOld=PatCur.Copy();
			PatCur.SecRelationship=(Relat)listSecRelat.SelectedIndex;
			PatCur.Update(PatOld);
			FillAll();
		}

		private void textPriPatID_Leave(object sender, System.EventArgs e) {
			if(textPriPatID.Text!=PatCur.PriPatID){
				Patient PatOld=PatCur.Copy();
				PatCur.PriPatID=textPriPatID.Text;
				PatCur.Update(PatOld);
				FillAll();
			}
		}

		private void textSecPatID_Leave(object sender, System.EventArgs e) {
			if(textSecPatID.Text!=PatCur.SecPatID){
				Patient PatOld=PatCur.Copy();
				PatCur.SecPatID=textSecPatID.Text;
				PatCur.Update(PatOld);
				FillAll();
			}
		}

		private void listPriPlan_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listPriPlan.SelectedIndex==-1){
				return;
			}
			Patient PatOld=PatCur.Copy();
			PatCur.PriPlanNum=PlanList[listPriPlan.SelectedIndex].PlanNum;
			PatCur.Update(PatOld);
			FillAll();
		}

		private void listSecPlan_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listSecPlan.SelectedIndex==-1){
				return;
			}
			Patient PatOld=PatCur.Copy();
			PatCur.SecPlanNum=PlanList[listSecPlan.SelectedIndex].PlanNum;
			PatCur.Update(PatOld);
			FillAll();
		}

		private void butNone_Click(object sender, System.EventArgs e) {
			if(PatCur.SecPlanNum!=0){
				MessageBox.Show(Lan.g(this,"Not allowed to delete primary coverage if patient has secondary coverage."));
				return;//this can be cleaned up later with more intelligent analysis
			}
			listPriPlan.SelectedIndex=-1;
			listPriRelat.SelectedIndex=0;
			textPriPatID.Text="";
			Patient PatOld=PatCur.Copy();
			PatCur.PriPlanNum=0;
			PatCur.PriRelationship=Relat.Self;
			PatCur.PriPatID="";
			PatCur.Update(PatOld);
			FillAll();
		}

		private void butNoneSec_Click(object sender, System.EventArgs e) {
			listSecPlan.SelectedIndex=-1;
			listSecRelat.SelectedIndex=0;
			textSecPatID.Text="";
			Patient PatOld=PatCur.Copy();
			PatCur.SecPlanNum=0;
			PatCur.SecRelationship=Relat.Self;
			PatCur.SecPatID="";
			PatCur.Update(PatOld);
			//Patients.CurOld=Patients.Cur.Copy();//important since we aren't refreshing.
			FillAll();
		}

		///<summary>This button is not present in the standard version, but is only for PracticeWeb.</summary>
		private void butAddPlan_Click(object sender, System.EventArgs e) {
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
			//but butDrop will not be visible.
			FormIP.ShowDialog();
			if(FormIP.DialogResult!=DialogResult.OK){
				return;
			}
			Patient PatOld=PatCur.Copy();
			PatCur.PriPlanNum=PlanCur.PlanNum;
			PatCur.Update(PatOld);
			FillAll();
		}

		///<summary>Sets current patient.PriPlanNum to the plan number of a plan that is not already loaded into the current family.</summary>
		private void butExistPriPlan_Click(object sender, System.EventArgs e) {
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.SelectionModeOnly=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK){
				return;
			}
			FormInsPlanSelect FormIPS=new FormInsPlanSelect(FormPS.SelectedPatNum);
			FormIPS.ShowDialog();
			if(FormIPS.DialogResult!=DialogResult.OK){
				return;
			}
			Patient PatOld=PatCur.Copy();
			PatCur.PriPlanNum=FormIPS.SelectedPlan.PlanNum;
			PatCur.Update(PatOld);
			FillAll();
		}

		private void butExistSecPlan_Click(object sender, System.EventArgs e) {
			//int curPat=Patients.Cur.PatNum;
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.SelectionModeOnly=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK){
				return;
			}
			FormInsPlanSelect FormIPS=new FormInsPlanSelect(FormPS.SelectedPatNum);
			FormIPS.ShowDialog();
			if(FormIPS.DialogResult!=DialogResult.OK){
				return;
			}
			Patient PatOld=PatCur.Copy();
			PatCur.SecPlanNum=FormIPS.SelectedPlan.PlanNum;
			PatCur.Update(PatOld);
			FillAll();
		}

		private void tbPercent1_CellClicked(object sender, CellEventArgs e){
			bool isNew=true;
			//CovCats.GetOrderShort(CovPats.List[i].CovCatNum)
			for(int i=0;i<CovPats.List.Length;i++){
				if(CovPats.List[i].PriPatNum==PatCur.PatNum
					&& CovCats.GetOrderShort(CovPats.List[i].CovCatNum)==e.Row){
					isNew=false;
					CovPats.Cur=CovPats.List[i];
				}
			}
			FormPercentEdit FormPE=new FormPercentEdit();
			if(isNew){
				FormPE.RetVal=-1;
			}
			else{
				FormPE.RetVal=CovPats.Cur.Percent;
			}			
			FormPE.ShowDialog();
			if(FormPE.DialogResult!=DialogResult.OK){
				return;
			}
			if(isNew){
				if(FormPE.RetVal==-1){
					return;
				}
				CovPats.Cur=new CovPat();
				CovPats.Cur.CovCatNum=CovCats.ListShort[e.Row].CovCatNum;
				CovPats.Cur.PriPatNum=PatCur.PatNum;
				CovPats.Cur.Percent=FormPE.RetVal;
				CovPats.InsertCur();
			}
			else{
				if(FormPE.RetVal==-1){
					CovPats.DeleteCur();
				}
				else{
					CovPats.Cur.Percent=FormPE.RetVal;
					CovPats.UpdateCur();
				}
			}
			FillAll();
		}

		private void tbPercent2_CellClicked(object sender, CellEventArgs e){
			bool isNew=true;
			//CovCats.GetOrderShort(CovPats.List[i].CovCatNum)
			for(int i=0;i<CovPats.List.Length;i++){
				if(CovPats.List[i].SecPatNum==PatCur.PatNum
					&& CovCats.GetOrderShort(CovPats.List[i].CovCatNum)==e.Row){
					isNew=false;
					CovPats.Cur=CovPats.List[i];
				}
			}
			FormPercentEdit FormPE=new FormPercentEdit();
			if(isNew){
				FormPE.RetVal=-1;
			}
			else{
				FormPE.RetVal=CovPats.Cur.Percent;
			}			
			FormPE.ShowDialog();
			if(FormPE.DialogResult!=DialogResult.OK){
				return;
			}
			if(isNew){
				if(FormPE.RetVal==-1){
					return;
				}
				CovPats.Cur=new CovPat();
				CovPats.Cur.CovCatNum=CovCats.ListShort[e.Row].CovCatNum;
				CovPats.Cur.SecPatNum=PatCur.PatNum;
				CovPats.Cur.Percent=FormPE.RetVal;
				CovPats.InsertCur();
			}
			else{
				if(FormPE.RetVal==-1){
					CovPats.DeleteCur();
				}
				else{
					CovPats.Cur.Percent=FormPE.RetVal;
					CovPats.UpdateCur();
				}
			}
			FillAll();
		}

		private void butEditPriPlan_Click(object sender, System.EventArgs e) {
			if(PatCur.PriPlanNum==0){
				MessageBox.Show(Lan.g(this,"Please select an item from the list first."));
				return;
			}
			InsPlan PlanCur=new InsPlan();
			for(int i=0;i<PlanList.Length;i++){
				if(PlanList[i].PlanNum==PatCur.PriPlanNum){
					PlanCur=PlanList[i];
				}
			}
			FormInsPlan FormIP=new FormInsPlan(PlanCur,PatCur.PatNum);
			FormIP.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butEditSecPlan_Click(object sender, System.EventArgs e) {
			if(PatCur.SecPlanNum==0){
				MessageBox.Show(Lan.g(this,"Please select an item from the list first."));
				return;
			}
			InsPlan PlanCur=new InsPlan();
			for(int i=0;i<PlanList.Length;i++){
				if(PlanList[i].PlanNum==PatCur.SecPlanNum){
					PlanCur=PlanList[i];
				}
			}
			FormInsPlan FormIP=new FormInsPlan(PlanCur,PatCur.PatNum);
			FormIP.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void listPriAdj_DoubleClick(object sender, System.EventArgs e) {
			if(listPriAdj.SelectedIndex==-1){
				return;
			}
			FormInsAdj FormIA=new FormInsAdj((ClaimProc)ALPriAdj[listPriAdj.SelectedIndex]);
			FormIA.ShowDialog();
			if(FormIA.DialogResult==DialogResult.OK)
				FillAdj();
		}

		private void butPriAdd_Click(object sender, System.EventArgs e) {
			if(PatCur.PriPlanNum==0){
				MessageBox.Show(Lan.g(this,"No Plan selected."));
				return;
			}
			ClaimProc ClaimProcCur=new ClaimProc();
			ClaimProcCur.PatNum=PatCur.PatNum;
			ClaimProcCur.ProcDate=DateTime.Today;
			ClaimProcCur.Status=ClaimProcStatus.Adjustment;
			ClaimProcCur.PlanNum=PatCur.PriPlanNum;
			FormInsAdj FormIA=new FormInsAdj(ClaimProcCur);
			FormIA.IsNew=true;
			FormIA.ShowDialog();
			if(FormIA.DialogResult==DialogResult.OK)
				FillAdj();
		}

		private void butPriDelete_Click(object sender, System.EventArgs e) {
			if(listPriAdj.SelectedIndex==-1){
				MessageBox.Show(Lan.g("All","Please select an item first."));
				return;
			}
			((ClaimProc)ALPriAdj[listPriAdj.SelectedIndex]).Delete();
			FillAdj();
		}

		private void listSecAdj_DoubleClick(object sender, System.EventArgs e) {
			if(listSecAdj.SelectedIndex==-1){
				return;
			}
			FormInsAdj FormIA=new FormInsAdj((ClaimProc)ALSecAdj[listSecAdj.SelectedIndex]);
			FormIA.ShowDialog();
			if(FormIA.DialogResult==DialogResult.OK)
				FillAdj();
		}

		private void butSecAdd_Click(object sender, System.EventArgs e) {
			if(PatCur.SecPlanNum==0){
				MessageBox.Show(Lan.g(this,"No Plan selected."));
				return;
			}
			ClaimProc ClaimProcCur=new ClaimProc();
			ClaimProcCur.PatNum=PatCur.PatNum;
			ClaimProcCur.ProcDate=DateTime.Today;
			ClaimProcCur.Status=ClaimProcStatus.Adjustment;
			ClaimProcCur.PlanNum=PatCur.SecPlanNum;
			FormInsAdj FormIA=new FormInsAdj(ClaimProcCur);
			FormIA.IsNew=true;
			FormIA.ShowDialog();
			if(FormIA.DialogResult==DialogResult.OK)
				FillAdj();
		}

		private void butSecDelete_Click(object sender, System.EventArgs e) {
			if(listSecAdj.SelectedIndex==-1){
				MessageBox.Show(Lan.g("All","Please select an item first."));
				return;
			}
			((ClaimProc)ALSecAdj[listSecAdj.SelectedIndex]).Delete();
			FillAdj();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//UpdatePlanInfo();
			DialogResult=DialogResult.OK;
		}

		

		

		

		

		

		

	}
}
