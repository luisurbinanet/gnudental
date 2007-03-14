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

	public class FormInsCovEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.RadioButton radioPriChild;
		private System.Windows.Forms.RadioButton radioPriSpouse;
		private System.Windows.Forms.RadioButton radioPriSelf;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button butOK;// Required designer variable.
		//public bool IsNew;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private OpenDental.TablePercent tbPercent1;
		private System.Windows.Forms.ListBox listPriPlan;
		private System.Windows.Forms.Button butNone;
		private System.Windows.Forms.Button butNoneSec;
		private System.Windows.Forms.ListBox listSecPlan;
		private System.Windows.Forms.Button butEditPriPlan;
		private System.Windows.Forms.Button butEditSecPlan;
		private OpenDental.TablePercent tbPercent2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboPriOther;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton radioSecSelf;
		private System.Windows.Forms.ComboBox comboSecOther;
		private System.Windows.Forms.RadioButton radioSecChild;
		private System.Windows.Forms.RadioButton radioSecSpouse;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox listPriAdj;
		private System.Windows.Forms.Button butPriAdd;
		private ArrayList ALPriAdj;
		private System.Windows.Forms.Button butSecAdd;
		private System.Windows.Forms.ListBox listSecAdj;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button butPriDelete;
		private System.Windows.Forms.Button butSecDelete;
		private ArrayList ALSecAdj;

		public FormInsCovEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			tbPercent1.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent1_CellClicked);
			tbPercent2.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent2_CellClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label13,
				this.label14,
				this.label2,
				this.label3,
				this.label4,
				this.label5,
				this.label6,
				this.label7,
				this.label8,
				this.butEditPriPlan,
				this.butEditSecPlan,
				this.butNone,
				this.butNoneSec,
				this.butPriAdd,
				this.butPriDelete,
				this.butSecAdd,
				this.butSecDelete,
				this.groupBox1,
				this.groupBox2,
				this.radioPriChild,
				this.radioPriSelf,
				this.radioPriSpouse,
				this.radioSecChild,
				this.radioSecSelf,
				this.radioSecSpouse
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butPriDelete = new System.Windows.Forms.Button();
			this.butPriAdd = new System.Windows.Forms.Button();
			this.listPriAdj = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.comboPriOther = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.butEditPriPlan = new System.Windows.Forms.Button();
			this.butNone = new System.Windows.Forms.Button();
			this.listPriPlan = new System.Windows.Forms.ListBox();
			this.tbPercent1 = new OpenDental.TablePercent();
			this.label1 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.radioPriChild = new System.Windows.Forms.RadioButton();
			this.radioPriSpouse = new System.Windows.Forms.RadioButton();
			this.radioPriSelf = new System.Windows.Forms.RadioButton();
			this.butOK = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butSecDelete = new System.Windows.Forms.Button();
			this.butSecAdd = new System.Windows.Forms.Button();
			this.listSecAdj = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboSecOther = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.radioSecChild = new System.Windows.Forms.RadioButton();
			this.radioSecSpouse = new System.Windows.Forms.RadioButton();
			this.radioSecSelf = new System.Windows.Forms.RadioButton();
			this.butEditSecPlan = new System.Windows.Forms.Button();
			this.tbPercent2 = new OpenDental.TablePercent();
			this.label2 = new System.Windows.Forms.Label();
			this.listSecPlan = new System.Windows.Forms.ListBox();
			this.butNoneSec = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.panel5.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butPriDelete);
			this.groupBox2.Controls.Add(this.butPriAdd);
			this.groupBox2.Controls.Add(this.listPriAdj);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.comboPriOther);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.butEditPriPlan);
			this.groupBox2.Controls.Add(this.butNone);
			this.groupBox2.Controls.Add(this.listPriPlan);
			this.groupBox2.Controls.Add(this.tbPercent1);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.panel5);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(8, 10);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(371, 484);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Primary";
			// 
			// butPriDelete
			// 
			this.butPriDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butPriDelete.Location = new System.Drawing.Point(297, 377);
			this.butPriDelete.Name = "butPriDelete";
			this.butPriDelete.Size = new System.Drawing.Size(59, 20);
			this.butPriDelete.TabIndex = 90;
			this.butPriDelete.Text = "Delete";
			this.butPriDelete.Click += new System.EventHandler(this.butPriDelete_Click);
			// 
			// butPriAdd
			// 
			this.butPriAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butPriAdd.Location = new System.Drawing.Point(234, 377);
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
			this.listPriAdj.Location = new System.Drawing.Point(15, 401);
			this.listPriAdj.Name = "listPriAdj";
			this.listPriAdj.Size = new System.Drawing.Size(341, 56);
			this.listPriAdj.TabIndex = 4;
			this.listPriAdj.DoubleClick += new System.EventHandler(this.listPriAdj_DoubleClick);
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(13, 380);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(218, 17);
			this.label8.TabIndex = 87;
			this.label8.Text = "Adjustments to Insurance Benefits: ";
			// 
			// comboPriOther
			// 
			this.comboPriOther.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPriOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.comboPriOther.Items.AddRange(new object[] {
																											 "Employee",
																											 "Handicapped Dependent",
																											 "Significant Other",
																											 "Injured Plaintiff",
																											 "Life Partner",
																											 "Dependent"});
			this.comboPriOther.Location = new System.Drawing.Point(232, 129);
			this.comboPriOther.Name = "comboPriOther";
			this.comboPriOther.Size = new System.Drawing.Size(125, 21);
			this.comboPriOther.TabIndex = 2;
			this.comboPriOther.Click += new System.EventHandler(this.comboPriOther_Click);
			this.comboPriOther.SelectedIndexChanged += new System.EventHandler(this.comboPriOther_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(196, 133);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(38, 14);
			this.label6.TabIndex = 86;
			this.label6.Text = "Other:";
			// 
			// butEditPriPlan
			// 
			this.butEditPriPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butEditPriPlan.Location = new System.Drawing.Point(16, 192);
			this.butEditPriPlan.Name = "butEditPriPlan";
			this.butEditPriPlan.TabIndex = 83;
			this.butEditPriPlan.Text = "Edit Plan";
			this.butEditPriPlan.Click += new System.EventHandler(this.butEditPriPlan_Click);
			// 
			// butNone
			// 
			this.butNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butNone.Location = new System.Drawing.Point(298, 23);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(58, 20);
			this.butNone.TabIndex = 82;
			this.butNone.Text = "None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// listPriPlan
			// 
			this.listPriPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listPriPlan.Location = new System.Drawing.Point(14, 48);
			this.listPriPlan.Name = "listPriPlan";
			this.listPriPlan.ScrollAlwaysVisible = true;
			this.listPriPlan.Size = new System.Drawing.Size(341, 56);
			this.listPriPlan.TabIndex = 0;
			this.listPriPlan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listPriPlan_MouseDown);
			// 
			// tbPercent1
			// 
			this.tbPercent1.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercent1.Location = new System.Drawing.Point(14, 256);
			this.tbPercent1.Name = "tbPercent1";
			this.tbPercent1.SelectedIndices = new int[0];
			this.tbPercent1.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercent1.Size = new System.Drawing.Size(242, 86);
			this.tbPercent1.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 232);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(338, 18);
			this.label1.TabIndex = 79;
			this.label1.Text = "Override percentages for patient (single click to edit):";
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label14.Location = new System.Drawing.Point(14, 27);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(256, 14);
			this.label14.TabIndex = 78;
			this.label14.Text = "Highlight an insurance plan in this list:";
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.Location = new System.Drawing.Point(12, 112);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(148, 14);
			this.label13.TabIndex = 77;
			this.label13.Text = "Relationship to Subscriber";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.radioPriChild);
			this.panel5.Controls.Add(this.radioPriSpouse);
			this.panel5.Controls.Add(this.radioPriSelf);
			this.panel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.panel5.Location = new System.Drawing.Point(14, 132);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(170, 18);
			this.panel5.TabIndex = 1;
			// 
			// radioPriChild
			// 
			this.radioPriChild.Location = new System.Drawing.Point(120, 0);
			this.radioPriChild.Name = "radioPriChild";
			this.radioPriChild.Size = new System.Drawing.Size(48, 15);
			this.radioPriChild.TabIndex = 2;
			this.radioPriChild.Text = "Child";
			this.radioPriChild.Click += new System.EventHandler(this.radioPriChild_Click);
			// 
			// radioPriSpouse
			// 
			this.radioPriSpouse.Location = new System.Drawing.Point(52, -1);
			this.radioPriSpouse.Name = "radioPriSpouse";
			this.radioPriSpouse.Size = new System.Drawing.Size(64, 17);
			this.radioPriSpouse.TabIndex = 1;
			this.radioPriSpouse.Text = "Spouse";
			this.radioPriSpouse.Click += new System.EventHandler(this.radioPriSpouse_Click);
			// 
			// radioPriSelf
			// 
			this.radioPriSelf.Location = new System.Drawing.Point(0, 0);
			this.radioPriSelf.Name = "radioPriSelf";
			this.radioPriSelf.Size = new System.Drawing.Size(46, 15);
			this.radioPriSelf.TabIndex = 0;
			this.radioPriSelf.Text = "Self";
			this.radioPriSelf.Click += new System.EventHandler(this.radioPriSelf_Click);
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(741, 528);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 101;
			this.butOK.Text = "Close";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butSecDelete);
			this.groupBox1.Controls.Add(this.butSecAdd);
			this.groupBox1.Controls.Add(this.listSecAdj);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.comboSecOther);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.panel1);
			this.groupBox1.Controls.Add(this.butEditSecPlan);
			this.groupBox1.Controls.Add(this.tbPercent2);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.listSecPlan);
			this.groupBox1.Controls.Add(this.butNoneSec);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(446, 10);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(371, 484);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Secondary";
			// 
			// butSecDelete
			// 
			this.butSecDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butSecDelete.Location = new System.Drawing.Point(296, 376);
			this.butSecDelete.Name = "butSecDelete";
			this.butSecDelete.Size = new System.Drawing.Size(59, 20);
			this.butSecDelete.TabIndex = 95;
			this.butSecDelete.Text = "Delete";
			this.butSecDelete.Click += new System.EventHandler(this.butSecDelete_Click);
			// 
			// butSecAdd
			// 
			this.butSecAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butSecAdd.Location = new System.Drawing.Point(233, 376);
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
			this.listSecAdj.Location = new System.Drawing.Point(15, 400);
			this.listSecAdj.Name = "listSecAdj";
			this.listSecAdj.Size = new System.Drawing.Size(341, 56);
			this.listSecAdj.TabIndex = 4;
			this.listSecAdj.DoubleClick += new System.EventHandler(this.listSecAdj_DoubleClick);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(13, 380);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(218, 17);
			this.label5.TabIndex = 92;
			this.label5.Text = "Adjustments to Insurance Benefits: ";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(15, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(148, 14);
			this.label4.TabIndex = 91;
			this.label4.Text = "Relationship to Subscriber";
			// 
			// comboSecOther
			// 
			this.comboSecOther.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSecOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.comboSecOther.Items.AddRange(new object[] {
																											 "Employee",
																											 "Handicapped Dependent",
																											 "Significant Other",
																											 "Injured Plaintiff",
																											 "Life Partner",
																											 "Dependent"});
			this.comboSecOther.Location = new System.Drawing.Point(235, 129);
			this.comboSecOther.Name = "comboSecOther";
			this.comboSecOther.Size = new System.Drawing.Size(125, 21);
			this.comboSecOther.TabIndex = 2;
			this.comboSecOther.Click += new System.EventHandler(this.comboSecOther_Click);
			this.comboSecOther.SelectedIndexChanged += new System.EventHandler(this.comboSecOther_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(199, 133);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(38, 14);
			this.label7.TabIndex = 90;
			this.label7.Text = "Other:";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.radioSecChild);
			this.panel1.Controls.Add(this.radioSecSpouse);
			this.panel1.Controls.Add(this.radioSecSelf);
			this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.panel1.Location = new System.Drawing.Point(17, 132);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(170, 18);
			this.panel1.TabIndex = 1;
			// 
			// radioSecChild
			// 
			this.radioSecChild.Location = new System.Drawing.Point(120, 0);
			this.radioSecChild.Name = "radioSecChild";
			this.radioSecChild.Size = new System.Drawing.Size(48, 15);
			this.radioSecChild.TabIndex = 2;
			this.radioSecChild.Text = "Child";
			this.radioSecChild.Click += new System.EventHandler(this.radioSecChild_Click);
			// 
			// radioSecSpouse
			// 
			this.radioSecSpouse.Location = new System.Drawing.Point(52, -1);
			this.radioSecSpouse.Name = "radioSecSpouse";
			this.radioSecSpouse.Size = new System.Drawing.Size(64, 17);
			this.radioSecSpouse.TabIndex = 1;
			this.radioSecSpouse.Text = "Spouse";
			this.radioSecSpouse.Click += new System.EventHandler(this.radioSecSpouse_Click);
			// 
			// radioSecSelf
			// 
			this.radioSecSelf.Location = new System.Drawing.Point(0, 0);
			this.radioSecSelf.Name = "radioSecSelf";
			this.radioSecSelf.Size = new System.Drawing.Size(46, 15);
			this.radioSecSelf.TabIndex = 0;
			this.radioSecSelf.Text = "Self";
			this.radioSecSelf.Click += new System.EventHandler(this.radioSecSelf_Click);
			// 
			// butEditSecPlan
			// 
			this.butEditSecPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butEditSecPlan.Location = new System.Drawing.Point(20, 192);
			this.butEditSecPlan.Name = "butEditSecPlan";
			this.butEditSecPlan.TabIndex = 87;
			this.butEditSecPlan.Text = "Edit Plan";
			this.butEditSecPlan.Click += new System.EventHandler(this.butEditSecPlan_Click);
			// 
			// tbPercent2
			// 
			this.tbPercent2.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercent2.Location = new System.Drawing.Point(16, 256);
			this.tbPercent2.Name = "tbPercent2";
			this.tbPercent2.SelectedIndices = new int[0];
			this.tbPercent2.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbPercent2.Size = new System.Drawing.Size(242, 86);
			this.tbPercent2.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 232);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(298, 18);
			this.label2.TabIndex = 85;
			this.label2.Text = "Override percentages for patient (single click to edit):";
			// 
			// listSecPlan
			// 
			this.listSecPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listSecPlan.Location = new System.Drawing.Point(16, 48);
			this.listSecPlan.Name = "listSecPlan";
			this.listSecPlan.ScrollAlwaysVisible = true;
			this.listSecPlan.Size = new System.Drawing.Size(341, 56);
			this.listSecPlan.TabIndex = 0;
			this.listSecPlan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listSecPlan_MouseDown);
			// 
			// butNoneSec
			// 
			this.butNoneSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butNoneSec.Location = new System.Drawing.Point(298, 25);
			this.butNoneSec.Name = "butNoneSec";
			this.butNoneSec.Size = new System.Drawing.Size(58, 20);
			this.butNoneSec.TabIndex = 83;
			this.butNoneSec.Text = "None";
			this.butNoneSec.Click += new System.EventHandler(this.butNoneSec_Click);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(14, 27);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(262, 14);
			this.label3.TabIndex = 78;
			this.label3.Text = "Highlight an insurance plan in this list:";
			// 
			// FormInsCovEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(836, 567);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupBox2);
			this.Name = "FormInsCovEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Insurance Coverage";
			this.Load += new System.EventHandler(this.FormInsCovEdit_Load);
			this.groupBox2.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsCovEdit_Load(object sender, System.EventArgs e) {
			listPriPlan.SelectedIndex=-1;
			for(int i=0;i<InsPlans.List.Length;i++){
				listPriPlan.Items.Add(InsPlans.GetDescInFam(InsPlans.List[i].PlanNum));
				if(Patients.Cur.PriPlanNum==InsPlans.List[i].PlanNum){
					listPriPlan.SelectedIndex=i;
				}
			}
			switch(Patients.Cur.PriRelationship){
				case Relat.Self: this.radioPriSelf.Checked=true; break;
				case Relat.Spouse: this.radioPriSpouse.Checked=true; break;
				case Relat.Child: this.radioPriChild.Checked=true; break;
				case Relat.Employee:         comboPriOther.SelectedIndex=0; break;
				case Relat.HandicapDep:      comboPriOther.SelectedIndex=1; break;
				case Relat.SignifOther:      comboPriOther.SelectedIndex=2; break;
				case Relat.InjuredPlaintiff: comboPriOther.SelectedIndex=3; break;
				case Relat.LifePartner:      comboPriOther.SelectedIndex=4; break;
				case Relat.Dependent:        comboPriOther.SelectedIndex=5; break;
			}
			listSecPlan.SelectedIndex=-1;
			for(int i=0;i<InsPlans.List.Length;i++){
				listSecPlan.Items.Add(InsPlans.GetDescInFam(InsPlans.List[i].PlanNum));
				if(Patients.Cur.SecPlanNum==InsPlans.List[i].PlanNum){
					listSecPlan.SelectedIndex=i;
				}
			}
			switch(Patients.Cur.SecRelationship){
				case Relat.Self: this.radioSecSelf.Checked=true; break;
				case Relat.Spouse: this.radioSecSpouse.Checked=true; break;
				case Relat.Child: this.radioSecChild.Checked=true; break;
				case Relat.Employee:         comboSecOther.SelectedIndex=0; break;
				case Relat.HandicapDep:      comboSecOther.SelectedIndex=1; break;
				case Relat.SignifOther:      comboSecOther.SelectedIndex=2; break;
				case Relat.InjuredPlaintiff: comboSecOther.SelectedIndex=3; break;
				case Relat.LifePartner:      comboSecOther.SelectedIndex=4; break;
				case Relat.Dependent:        comboSecOther.SelectedIndex=5; break;
			}
			FillTables();
			FillAdj();
		}

		private void FillTables(){
			if(Patients.Cur.SecPlanNum!=0) butNone.Enabled=false;
			else butNone.Enabled=true;
			tbPercent1.ResetRows(CovPats.PriList.Length);
			tbPercent1.SetGridColor(Color.LightGray);
			for(int i=0;i<CovCats.ListShort.Length;i++){
				tbPercent1.Cell[0,i]=CovCats.ListShort[i].Description;
				tbPercent1.Cell[1,i]="";
			}
			for(int i=0;i<CovPats.List.Length;i++){
				if(CovPats.List[i].PriPatNum==Patients.Cur.PatNum){
					tbPercent1.Cell[1,CovCats.GetOrderShort(CovPats.List[i].CovCatNum)]=CovPats.List[i].Percent.ToString();
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
				if(CovPats.List[i].SecPatNum==Patients.Cur.PatNum){
					tbPercent2.Cell[1,CovCats.GetOrderShort(CovPats.List[i].CovCatNum)]=CovPats.List[i].Percent.ToString();
				}
			}
			tbPercent2.LayoutTables();
		}

    private void FillAdj(){
			Claims.Refresh();
			//move selected claims into ALAdj
			ALPriAdj=new ArrayList();
			ALSecAdj=new ArrayList();
			for(int i=0;i<Claims.List.Length;i++){
				if(Claims.List[i].PlanNum==Patients.Cur.PriPlanNum
					&& Claims.List[i].ClaimStatus=="A"){
					ALPriAdj.Add(Claims.List[i]);
				}
				if(Claims.List[i].PlanNum==Patients.Cur.SecPlanNum
					&& Claims.List[i].ClaimStatus=="A"){
					ALSecAdj.Add(Claims.List[i]);
				}
			}
			listPriAdj.Items.Clear();
			listSecAdj.Items.Clear();
			string s;
			for(int i=0;i<ALPriAdj.Count;i++){
				s=((Claim)ALPriAdj[i]).DateService.ToShortDateString()+"       Ins Used:  "
					+((Claim)ALPriAdj[i]).InsPayAmt.ToString("F")+"       Ded Used:  "
					+((Claim)ALPriAdj[i]).DedApplied.ToString("F");
				listPriAdj.Items.Add(s);
			}
			for(int i=0;i<ALSecAdj.Count;i++){
				s=((Claim)ALSecAdj[i]).DateService.ToShortDateString()+"       Ins Used:  "
					+((Claim)ALSecAdj[i]).InsPayAmt.ToString("F")+"       Ded Used:  "
					+((Claim)ALSecAdj[i]).DedApplied.ToString("F");
				listSecAdj.Items.Add(s);
			}
		}
		
		//pri
		private void radioPriSelf_Click(object sender, System.EventArgs e) {
			Patients.Cur.PriRelationship=Relat.Self;
			comboPriOther.SelectedIndex=-1;
			Patients.UpdateCur();
		}

		private void radioPriSpouse_Click(object sender, System.EventArgs e) {
			Patients.Cur.PriRelationship=Relat.Spouse;
			comboPriOther.SelectedIndex=-1;
			Patients.UpdateCur();
		}

		private void radioPriChild_Click(object sender, System.EventArgs e) {
			Patients.Cur.PriRelationship=Relat.Child;
			comboPriOther.SelectedIndex=-1;
			Patients.UpdateCur();
		}

		private void comboPriOther_Click(object sender, System.EventArgs e) {
			radioPriSelf.Checked=false;
			radioPriSpouse.Checked=false;
			radioPriChild.Checked=false;
			if(comboPriOther.SelectedIndex==-1){
				comboPriOther.SelectedIndex=0;
			}
		}

		private void comboPriOther_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(comboPriOther.SelectedIndex==-1){
				return;
			}
			Relat prevSel=Patients.Cur.PriRelationship;
			switch(comboPriOther.SelectedIndex){
				case 0:	Patients.Cur.PriRelationship=Relat.Employee; break;
				case 1:	Patients.Cur.PriRelationship=Relat.HandicapDep; break;
				case 2:	Patients.Cur.PriRelationship=Relat.SignifOther; break;
				case 3:	Patients.Cur.PriRelationship=Relat.InjuredPlaintiff; break;
				case 4:	Patients.Cur.PriRelationship=Relat.LifePartner; break;
				case 5:	Patients.Cur.PriRelationship=Relat.Dependent; break;
			}
			if(prevSel!=Patients.Cur.PriRelationship)//prevents extra updates
				Patients.UpdateCur();
		}

		//sec:
		private void radioSecSelf_Click(object sender, System.EventArgs e) {
			Patients.Cur.SecRelationship=Relat.Self;
			comboSecOther.SelectedIndex=-1;
			Patients.UpdateCur();
		}

		private void radioSecSpouse_Click(object sender, System.EventArgs e) {
			Patients.Cur.SecRelationship=Relat.Spouse;
			comboSecOther.SelectedIndex=-1;
			Patients.UpdateCur();
		}

		private void radioSecChild_Click(object sender, System.EventArgs e) {
			Patients.Cur.SecRelationship=Relat.Child;
			comboSecOther.SelectedIndex=-1;
			Patients.UpdateCur();
		}

		private void comboSecOther_Click(object sender, System.EventArgs e) {
			radioSecSelf.Checked=false;
			radioSecSpouse.Checked=false;
			radioSecChild.Checked=false;
			if(comboSecOther.SelectedIndex==-1){
				comboSecOther.SelectedIndex=0;
			}
		}

		private void comboSecOther_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(comboSecOther.SelectedIndex==-1){
				return;
			}
			Relat prevSel=Patients.Cur.SecRelationship;
			switch(comboSecOther.SelectedIndex){
				case 0:	Patients.Cur.SecRelationship=Relat.Employee; break;
				case 1:	Patients.Cur.SecRelationship=Relat.HandicapDep; break;
				case 2:	Patients.Cur.SecRelationship=Relat.SignifOther; break;
				case 3:	Patients.Cur.SecRelationship=Relat.InjuredPlaintiff; break;
				case 4:	Patients.Cur.SecRelationship=Relat.LifePartner; break;
				case 5:	Patients.Cur.SecRelationship=Relat.Dependent; break;
			}
			if(prevSel!=Patients.Cur.SecRelationship)
				Patients.UpdateCur();
		}

		private void listPriPlan_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listPriPlan.SelectedIndex==-1){
				return;
			}
			Patients.Cur.PriPlanNum=InsPlans.List[listPriPlan.SelectedIndex].PlanNum;
			Patients.UpdateCur();
		}

		private void listSecPlan_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listSecPlan.SelectedIndex==-1){
				return;
			}
			Patients.Cur.SecPlanNum=InsPlans.List[listSecPlan.SelectedIndex].PlanNum;
			Patients.UpdateCur();
		}

		private void butNone_Click(object sender, System.EventArgs e) {
			if(Patients.Cur.SecPlanNum!=0){
				MessageBox.Show(Lan.g(this,"Not allowed to delete primary coverage if patient has secondary coverage."));
				return;//this can be cleaned up later with more intelligent analysis
			}
			listPriPlan.SelectedIndex=-1;
			radioPriSelf.Checked=true;
			Patients.Cur.PriPlanNum=0;
			Patients.Cur.PriRelationship=Relat.Self;
			Patients.UpdateCur();
		}

		private void butNoneSec_Click(object sender, System.EventArgs e) {
			listSecPlan.SelectedIndex=-1;
			radioSecSelf.Checked=true;
			Patients.Cur.SecPlanNum=0;
			Patients.Cur.SecRelationship=Relat.Self;
			Patients.UpdateCur();
		}

		private void tbPercent1_CellClicked(object sender, CellEventArgs e){
			bool isNew=true;
			//CovCats.GetOrderShort(CovPats.List[i].CovCatNum)
			for(int i=0;i<CovPats.List.Length;i++){
				if(CovPats.List[i].PriPatNum==Patients.Cur.PatNum
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
				CovPats.Cur.PriPatNum=Patients.Cur.PatNum;
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
			CovPats.Refresh();
			FillTables();
		}

		private void tbPercent2_CellClicked(object sender, CellEventArgs e){
			bool isNew=true;
			//CovCats.GetOrderShort(CovPats.List[i].CovCatNum)
			for(int i=0;i<CovPats.List.Length;i++){
				if(CovPats.List[i].SecPatNum==Patients.Cur.PatNum
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
				CovPats.Cur.SecPatNum=Patients.Cur.PatNum;
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
			CovPats.Refresh();
			FillTables();
		}

		private void butEditPriPlan_Click(object sender, System.EventArgs e) {
			//UpdateAndRefresh();			
			if(Patients.Cur.PriPlanNum==0){
				MessageBox.Show(Lan.g(this,"No plan to edit"));
				return;
			}
			FormInsPlan FormInsPLan = new FormInsPlan();
			for(int i=0;i<InsPlans.List.Length;i++){
				if(InsPlans.List[i].PlanNum==Patients.Cur.PriPlanNum){
					InsPlans.Cur=InsPlans.List[i];
				}
			}
			FormInsPLan.ShowDialog();
			//if(FormInsPLan.DialogResult==DialogResult.OK)
			DialogResult=DialogResult.OK;
		}

		private void butEditSecPlan_Click(object sender, System.EventArgs e) {
			//UpdateAndRefresh();			
			if(Patients.Cur.SecPlanNum==0){
				MessageBox.Show(Lan.g(this,"No plan to edit"));
				return;
			}
			FormInsPlan FormInsPLan = new FormInsPlan();
			for(int i=0;i<InsPlans.List.Length;i++){
				if(InsPlans.List[i].PlanNum==Patients.Cur.SecPlanNum){
					InsPlans.Cur=InsPlans.List[i];
				}
			}
			FormInsPLan.ShowDialog();
			//if(FormInsPLan.DialogResult==DialogResult.OK)
			DialogResult=DialogResult.OK;
		}

		/*private void UpdateAndRefresh(){//obsolete
		
			UpdatePlanInfo();
			Patients.GetFamily(Patients.Cur.PatNum);
			InsPlans.Refresh();
			CovPats.Refresh();
		}*/

		private void listPriAdj_DoubleClick(object sender, System.EventArgs e) {
			if(listPriAdj.SelectedIndex==-1){
				return;
			}
			Claims.Cur=((Claim)ALPriAdj[listPriAdj.SelectedIndex]);
			FormInsAdj FormIA=new FormInsAdj();
			FormIA.ShowDialog();
			if(FormIA.DialogResult==DialogResult.OK)
				FillAdj();
		}

		private void butPriAdd_Click(object sender, System.EventArgs e) {
			if(Patients.Cur.PriPlanNum==0){
				MessageBox.Show(Lan.g(this,"No Plan selected."));
				return;
			}
			Claims.Cur=new Claim();
			Claims.Cur.PatNum=Patients.Cur.PatNum;
			Claims.Cur.DateService=DateTime.Today;
			Claims.Cur.ClaimStatus="A";
			Claims.Cur.PlanNum=Patients.Cur.PriPlanNum;
			FormInsAdj FormIA=new FormInsAdj();
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
			Claims.Cur=((Claim)ALPriAdj[listPriAdj.SelectedIndex]);
			Claims.DeleteCur();
			FillAdj();
		}

		private void listSecAdj_DoubleClick(object sender, System.EventArgs e) {
			if(listSecAdj.SelectedIndex==-1){
				return;
			}
			Claims.Cur=((Claim)ALSecAdj[listSecAdj.SelectedIndex]);
			FormInsAdj FormIA=new FormInsAdj();
			FormIA.ShowDialog();
			if(FormIA.DialogResult==DialogResult.OK)
				FillAdj();
		}

		private void butSecAdd_Click(object sender, System.EventArgs e) {
			if(Patients.Cur.SecPlanNum==0){
				MessageBox.Show(Lan.g(this,"No Plan selected."));
				return;
			}
			Claims.Cur=new Claim();
			Claims.Cur.PatNum=Patients.Cur.PatNum;
			Claims.Cur.DateService=DateTime.Today;
			Claims.Cur.ClaimStatus="A";
			Claims.Cur.PlanNum=Patients.Cur.SecPlanNum;
			FormInsAdj FormIA=new FormInsAdj();
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
			Claims.Cur=((Claim)ALSecAdj[listSecAdj.SelectedIndex]);
			Claims.DeleteCur();
			FillAdj();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//UpdatePlanInfo();
			DialogResult=DialogResult.OK;
		}

	}
}