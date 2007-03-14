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
		private OpenDental.TablePercent tbPercent2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox listPriAdj;
		private System.Windows.Forms.Button butPriAdd;
		private ArrayList ALPriAdj;
		private System.Windows.Forms.Button butSecAdd;
		private System.Windows.Forms.ListBox listSecAdj;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button butPriDelete;
		private System.Windows.Forms.Button butSecDelete;
		private System.Windows.Forms.Label label9;
		private OpenDental.XPButton butEditPriPlan;
		private OpenDental.XPButton butEditSecPlan;
		private OpenDental.XPButton butAddPlan;
		private System.Windows.Forms.Button butExistPriPlan;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button butExistSecPlan;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ListBox listPriRelat;
		private System.Windows.Forms.ListBox listSecRelat;
		private System.Windows.Forms.Label label6;
		private ArrayList ALSecAdj;

		///<summary></summary>
		public FormInsCovEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			tbPercent1.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent1_CellClicked);
			tbPercent2.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbPercent2_CellClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label13,
				//this.label14,
				this.label2,
				this.label3,
				this.label5,
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
				this.groupBox2
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
			});

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
			this.listPriRelat = new System.Windows.Forms.ListBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.butExistPriPlan = new System.Windows.Forms.Button();
			this.butAddPlan = new OpenDental.XPButton();
			this.butEditPriPlan = new OpenDental.XPButton();
			this.label9 = new System.Windows.Forms.Label();
			this.butPriDelete = new System.Windows.Forms.Button();
			this.butPriAdd = new System.Windows.Forms.Button();
			this.listPriAdj = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.butNone = new System.Windows.Forms.Button();
			this.listPriPlan = new System.Windows.Forms.ListBox();
			this.tbPercent1 = new OpenDental.TablePercent();
			this.label1 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label11 = new System.Windows.Forms.Label();
			this.butExistSecPlan = new System.Windows.Forms.Button();
			this.butEditSecPlan = new OpenDental.XPButton();
			this.butSecDelete = new System.Windows.Forms.Button();
			this.butSecAdd = new System.Windows.Forms.Button();
			this.listSecAdj = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbPercent2 = new OpenDental.TablePercent();
			this.label2 = new System.Windows.Forms.Label();
			this.listSecPlan = new System.Windows.Forms.ListBox();
			this.butNoneSec = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.listSecRelat = new System.Windows.Forms.ListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
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
			// listPriRelat
			// 
			this.listPriRelat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listPriRelat.Location = new System.Drawing.Point(17, 233);
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
			this.butExistPriPlan.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.butAddPlan.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAddPlan.BtnStyle = OpenDental.enumType.XPStyle.Silver;
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
			this.butEditPriPlan.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butEditPriPlan.BtnStyle = OpenDental.enumType.XPStyle.Silver;
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
			this.label9.Size = new System.Drawing.Size(262, 14);
			this.label9.TabIndex = 92;
			this.label9.Text = "Highlight an insurance plan from the family list:";
			// 
			// butPriDelete
			// 
			this.butPriDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.butPriAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butPriAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butPriAdd.Location = new System.Drawing.Point(234, 450);
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
			// 
			// butNone
			// 
			this.butNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.tbPercent1.Location = new System.Drawing.Point(17, 331);
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
			this.label1.Location = new System.Drawing.Point(16, 307);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(338, 18);
			this.label1.TabIndex = 79;
			this.label1.Text = "Override percentages for patient (single click to edit):";
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.Location = new System.Drawing.Point(16, 216);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(148, 14);
			this.label13.TabIndex = 77;
			this.label13.Text = "Relationship to Subscriber";
			// 
			// butOK
			// 
			this.butOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(743, 592);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 101;
			this.butOK.Text = "&Close";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// groupBox1
			// 
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
			this.butExistSecPlan.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.butEditSecPlan.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butEditSecPlan.BtnStyle = OpenDental.enumType.XPStyle.Silver;
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
			this.butSecDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.butSecAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSecAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butSecAdd.Location = new System.Drawing.Point(233, 449);
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
			// 
			// tbPercent2
			// 
			this.tbPercent2.BackColor = System.Drawing.SystemColors.Window;
			this.tbPercent2.Location = new System.Drawing.Point(16, 331);
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
			this.label2.Location = new System.Drawing.Point(16, 307);
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
			this.listSecPlan.Size = new System.Drawing.Size(353, 56);
			this.listSecPlan.TabIndex = 0;
			this.listSecPlan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listSecPlan_MouseDown);
			// 
			// butNoneSec
			// 
			this.butNoneSec.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			this.label3.Size = new System.Drawing.Size(262, 14);
			this.label3.TabIndex = 78;
			this.label3.Text = "Highlight an insurance plan from the family list:";
			// 
			// listSecRelat
			// 
			this.listSecRelat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.listSecRelat.Location = new System.Drawing.Point(17, 233);
			this.listSecRelat.Name = "listSecRelat";
			this.listSecRelat.Size = new System.Drawing.Size(134, 43);
			this.listSecRelat.TabIndex = 99;
			this.listSecRelat.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listSecRelat_MouseUp);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(16, 216);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(148, 14);
			this.label6.TabIndex = 98;
			this.label6.Text = "Relationship to Subscriber";
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
			if(((Pref)Prefs.HList["CustomizedForPracticeWeb"]).ValueString=="1"){
				butAddPlan.Visible=true;
			}
			FillPlans();
			InitialFillRelats();
			FillPercent();
			FillAdj();
		}

		///<summary>Patients.GetFamily() and Insplans.Refresh() MUST already be done</summary>
		private void FillPlans(){
			listPriPlan.Items.Clear();
			for(int i=0;i<InsPlans.List.Length;i++){
				listPriPlan.Items.Add(InsPlans.GetDescript(InsPlans.List[i].PlanNum));
				if(Patients.Cur.PriPlanNum==InsPlans.List[i].PlanNum){
					listPriPlan.SelectedIndex=i;
				}
			}
			listSecPlan.Items.Clear();
			for(int i=0;i<InsPlans.List.Length;i++){
				listSecPlan.Items.Add(InsPlans.GetDescript(InsPlans.List[i].PlanNum));
				if(Patients.Cur.SecPlanNum==InsPlans.List[i].PlanNum){
					listSecPlan.SelectedIndex=i;
				}
			}
			if(Patients.Cur.SecPlanNum!=0) butNone.Enabled=false;//prevents deleting primary if sec exists
			else butNone.Enabled=true;
		}

		private void InitialFillRelats(){
			listPriRelat.Items.Clear();
			listPriRelat.Items.AddRange(Enum.GetNames(typeof(Relat)));
			listPriRelat.SelectedIndex=(int)Patients.Cur.PriRelationship;
			listSecRelat.Items.Clear();
			listSecRelat.Items.AddRange(Enum.GetNames(typeof(Relat)));
			listSecRelat.SelectedIndex=(int)Patients.Cur.SecRelationship;
		}

		///<summary>Covpats.refresh must already be done</summary>
		private void FillPercent(){
			tbPercent1.ResetRows(CovPats.PriList.Length);
			tbPercent1.SetGridColor(Color.LightGray);
			for(int i=0;i<CovCats.ListShort.Length;i++){
				tbPercent1.Cell[0,i]=CovCats.ListShort[i].Description;
				tbPercent1.Cell[1,i]="";
			}
			for(int i=0;i<CovPats.List.Length;i++){
				if(CovPats.List[i].PriPatNum==Patients.Cur.PatNum){
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
				if(CovPats.List[i].SecPatNum==Patients.Cur.PatNum){
					tbPercent2.Cell[1,CovCats.GetOrderShort(CovPats.List[i].CovCatNum)]
						=CovPats.List[i].Percent.ToString();
				}
			}
			tbPercent2.LayoutTables();
		}

    private void FillAdj(){
			ClaimProcs.Refresh();
			//move selected claimprocs into ALAdj
			ALPriAdj=new ArrayList();
			ALSecAdj=new ArrayList();
			for(int i=0;i<ClaimProcs.List.Length;i++){
				if(ClaimProcs.List[i].PlanNum==Patients.Cur.PriPlanNum
					&& ClaimProcs.List[i].Status==ClaimProcStatus.Adjustment){
					ALPriAdj.Add(ClaimProcs.List[i]);
				}
				if(ClaimProcs.List[i].PlanNum==Patients.Cur.SecPlanNum
					&& ClaimProcs.List[i].Status==ClaimProcStatus.Adjustment){
					ALSecAdj.Add(ClaimProcs.List[i]);
				}
			}
			listPriAdj.Items.Clear();
			listSecAdj.Items.Clear();
			string s;
			for(int i=0;i<ALPriAdj.Count;i++){
				s=((ClaimProc)ALPriAdj[i]).DateCP.ToShortDateString()+"       Ins Used:  "
					+((ClaimProc)ALPriAdj[i]).InsPayAmt.ToString("F")+"       Ded Used:  "
					+((ClaimProc)ALPriAdj[i]).DedApplied.ToString("F");
				listPriAdj.Items.Add(s);
			}
			for(int i=0;i<ALSecAdj.Count;i++){
				s=((ClaimProc)ALSecAdj[i]).DateCP.ToShortDateString()+"       Ins Used:  "
					+((ClaimProc)ALSecAdj[i]).InsPayAmt.ToString("F")+"       Ded Used:  "
					+((ClaimProc)ALSecAdj[i]).DedApplied.ToString("F");
				listSecAdj.Items.Add(s);
			}
		}

		private void listPriRelat_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			Patient PatCur=Patients.Cur;
			PatCur.PriRelationship=(Relat)listPriRelat.SelectedIndex;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			Patients.CurOld=Patients.Cur;//important since we aren't refreshing.
		}

		private void listSecRelat_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			Patient PatCur=Patients.Cur;
			PatCur.SecRelationship=(Relat)listSecRelat.SelectedIndex;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			Patients.CurOld=Patients.Cur;//important since we aren't refreshing.
		}

		private void listPriPlan_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listPriPlan.SelectedIndex==-1){
				return;
			}
			Patient PatCur=Patients.Cur;
			PatCur.PriPlanNum=InsPlans.List[listPriPlan.SelectedIndex].PlanNum;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			Patients.CurOld=Patients.Cur;//important since we aren't refreshing.
			CovPats.Refresh();
			FillPercent();
			FillAdj();
		}

		private void listSecPlan_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listSecPlan.SelectedIndex==-1){
				return;
			}
			Patient PatCur=Patients.Cur;
			PatCur.SecPlanNum=InsPlans.List[listSecPlan.SelectedIndex].PlanNum;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			Patients.CurOld=Patients.Cur;//important since we aren't refreshing.
			CovPats.Refresh();
			FillPercent();
			FillAdj();
		}

		private void butNone_Click(object sender, System.EventArgs e) {
			if(Patients.Cur.SecPlanNum!=0){
				MessageBox.Show(Lan.g(this,"Not allowed to delete primary coverage if patient has secondary coverage."));
				return;//this can be cleaned up later with more intelligent analysis
			}
			listPriPlan.SelectedIndex=-1;
			listPriRelat.SelectedIndex=0;
			Patient PatCur=Patients.Cur;
			PatCur.PriPlanNum=0;
			PatCur.PriRelationship=Relat.Self;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			Patients.CurOld=Patients.Cur;//important since we aren't refreshing.
		}

		private void butNoneSec_Click(object sender, System.EventArgs e) {
			listSecPlan.SelectedIndex=-1;
			listSecRelat.SelectedIndex=0;
			Patient PatCur=Patients.Cur;
			PatCur.SecPlanNum=0;
			PatCur.SecRelationship=Relat.Self;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			Patients.CurOld=Patients.Cur;//important since we aren't refreshing.
		}

		private void butAddPlan_Click(object sender, System.EventArgs e) {
			InsPlans.Cur=new InsPlan();
			InsPlans.Cur.Subscriber=Patients.Cur.PatNum;
			InsPlans.Cur.SubscriberID=Patients.Cur.SSN;
			InsPlans.Cur.EmployerNum=Patients.Cur.EmployerNum;
			InsPlans.Cur.AnnualMax=-1;//blank
			InsPlans.Cur.OrthoMax=-1;
			InsPlans.Cur.RenewMonth=1;
			InsPlans.Cur.Deductible=-1;
			InsPlans.Cur.FloToAge=-1;
			InsPlans.Cur.ReleaseInfo=true;
			InsPlans.Cur.AssignBen=true;
			InsPlans.InsertCur();
			FormInsPlan FormIP=new FormInsPlan();
			FormIP.IsNew=true;
			//but butDrop will not be visible.
			FormIP.ShowDialog();
			//very important to always refresh after closing the FormInsPlan
			Patients.GetFamily(Patients.Cur.PatNum);
			if(FormIP.DialogResult!=DialogResult.OK){
				return;
			}
			Patient PatCur=Patients.Cur;
			PatCur.PriPlanNum=InsPlans.Cur.PlanNum;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			Patients.GetFamily(Patients.Cur.PatNum);
			InsPlans.Refresh();
			FillPlans();
			FillPercent();
			FillAdj();
		}

		private void butExistPriPlan_Click(object sender, System.EventArgs e) {
			int curPat=Patients.Cur.PatNum;
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.SelectionModeOnly=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK){
				Patients.GetFamily(curPat);
				return;
			}
			//GetFamily and InsPlans.Refresh will be done within FormInsPlanSelect,
			//but they are for the OTHER patient, not our patient
			FormInsPlanSelect FormIPS=new FormInsPlanSelect();
			FormIPS.ShowDialog();
			if(FormIPS.DialogResult!=DialogResult.OK){//no matter what the result, we have to refresh
				Patients.GetFamily(Patients.Cur.PatNum);
				InsPlans.Refresh();
				CovPats.Refresh();
				FillPlans();
				return;
			}
			Patients.GetFamily(curPat);
			Patient PatCur=Patients.Cur;
			PatCur.PriPlanNum=InsPlans.Cur.PlanNum;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			Patients.GetFamily(curPat);
			InsPlans.Refresh();
			CovPats.Refresh();
			//claimprocs.refresh done within filladj
			FillPlans();
			FillPercent();
			FillAdj();			
		}

		private void butExistSecPlan_Click(object sender, System.EventArgs e) {
			int curPat=Patients.Cur.PatNum;
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.SelectionModeOnly=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK){
				Patients.GetFamily(curPat);
				return;
			}
			//GetFamily and InsPlans.Refresh will be done within FormInsPlanSelect,
			//but they are for the OTHER patient, not our patient
			FormInsPlanSelect FormIPS=new FormInsPlanSelect();
			FormIPS.ShowDialog();
			if(FormIPS.DialogResult!=DialogResult.OK){//no matter what the result, we have to refresh
				Patients.GetFamily(Patients.Cur.PatNum);
				InsPlans.Refresh();
				CovPats.Refresh();
				FillPlans();
				return;
			}
			Patients.GetFamily(curPat);
			Patient PatCur=Patients.Cur;
			PatCur.SecPlanNum=InsPlans.Cur.PlanNum;
			Patients.Cur=PatCur;
			Patients.UpdateCur();
			Patients.GetFamily(curPat);
			InsPlans.Refresh();
			CovPats.Refresh();
			//claimprocs.refresh done within filladj
			FillPlans();
			FillPercent();
			FillAdj();		
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
			FillPercent();
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
			FillPercent();
		}

		private void butEditPriPlan_Click(object sender, System.EventArgs e) {
			if(Patients.Cur.PriPlanNum==0){
				MessageBox.Show(Lan.g(this,"Please select an item from the list first."));
				return;
			}
			FormInsPlan FormIP=new FormInsPlan();
			for(int i=0;i<InsPlans.List.Length;i++){
				if(InsPlans.List[i].PlanNum==Patients.Cur.PriPlanNum){
					InsPlans.Cur=InsPlans.List[i];
				}
			}
			FormIP.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void butEditSecPlan_Click(object sender, System.EventArgs e) {
			if(Patients.Cur.SecPlanNum==0){
				MessageBox.Show(Lan.g(this,"Please select an item from the list first."));
				return;
			}
			FormInsPlan FormIP=new FormInsPlan();
			for(int i=0;i<InsPlans.List.Length;i++){
				if(InsPlans.List[i].PlanNum==Patients.Cur.SecPlanNum){
					InsPlans.Cur=InsPlans.List[i];
				}
			}
			FormIP.ShowDialog();
			DialogResult=DialogResult.OK;
		}

		private void listPriAdj_DoubleClick(object sender, System.EventArgs e) {
			if(listPriAdj.SelectedIndex==-1){
				return;
			}
			ClaimProcs.Cur=((ClaimProc)ALPriAdj[listPriAdj.SelectedIndex]);
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
			ClaimProcs.Cur=new ClaimProc();
			ClaimProcs.Cur.PatNum=Patients.Cur.PatNum;
			ClaimProcs.Cur.DateCP=DateTime.Today;
			ClaimProcs.Cur.Status=ClaimProcStatus.Adjustment;
			ClaimProcs.Cur.PlanNum=Patients.Cur.PriPlanNum;
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
			ClaimProcs.Cur=((ClaimProc)ALPriAdj[listPriAdj.SelectedIndex]);
			ClaimProcs.DeleteCur();
			FillAdj();
		}

		private void listSecAdj_DoubleClick(object sender, System.EventArgs e) {
			if(listSecAdj.SelectedIndex==-1){
				return;
			}
			ClaimProcs.Cur=((ClaimProc)ALSecAdj[listSecAdj.SelectedIndex]);
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
			ClaimProcs.Cur=new ClaimProc();
			ClaimProcs.Cur.PatNum=Patients.Cur.PatNum;
			ClaimProcs.Cur.DateCP=DateTime.Today;
			ClaimProcs.Cur.Status=ClaimProcStatus.Adjustment;
			ClaimProcs.Cur.PlanNum=Patients.Cur.SecPlanNum;
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
			ClaimProcs.Cur=((ClaimProc)ALSecAdj[listSecAdj.SelectedIndex]);
			ClaimProcs.DeleteCur();
			FillAdj();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//UpdatePlanInfo();
			DialogResult=DialogResult.OK;
		}

		

		

		

		

	}
}