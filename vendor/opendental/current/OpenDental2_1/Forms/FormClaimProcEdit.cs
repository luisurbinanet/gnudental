using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormClaimProcEdit.
	/// </summary>
	public class FormClaimProcEdit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Label label1;
		private OpenDental.XPButton butDelete;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private OpenDental.ValidDouble textFeeBilled;
		private OpenDental.ValidDouble textDedApplied;
		private OpenDental.ValidDouble textInsPayAmt;
		private System.Windows.Forms.TextBox textRemarks;
		private System.Windows.Forms.ListBox listStatus;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.ListBox listProv;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.GroupBox groupTotal;
		private System.Windows.Forms.Label labelFee;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidDouble textWriteOff;
		private OpenDental.ValidDouble textInsPayEst;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox textSecPercent;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textPriPercent;
		private System.Windows.Forms.TextBox textSecEst;
		private System.Windows.Forms.TextBox textPriEst;
		private System.Windows.Forms.GroupBox groupBox3;
		private OpenDental.ValidDouble textOverrideSec;
		private OpenDental.ValidDouble textOverridePri;
		private System.Windows.Forms.Label label17;
		private OpenDental.ValidDouble textAmount;
		private bool IsProc;
		private bool priChanged;
		private System.Windows.Forms.GroupBox groupProcedure;
		private System.Windows.Forms.Label labelSecEst;
		private System.Windows.Forms.Label labelPriEst;
		private System.Windows.Forms.Label labelSecOver;
		private System.Windows.Forms.Label labelPriOver;
		private bool secChanged;
		private System.Windows.Forms.Label labelAttached;
		//public bool NoSave;

		public FormClaimProcEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this,
				this.label1,
				this.label2,
				this.label3,
				this.label4,
				this.label5,
				this.label6,
				this.label7,
				this.label8,
				this.label9,
				this.label10,
				this.labelFee,
				this.groupTotal,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butDelete,
			});
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimProcEdit));
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.XPButton();
			this.labelFee = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupTotal = new System.Windows.Forms.GroupBox();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.textFeeBilled = new OpenDental.ValidDouble();
			this.textDedApplied = new OpenDental.ValidDouble();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textInsPayAmt = new OpenDental.ValidDouble();
			this.label7 = new System.Windows.Forms.Label();
			this.textRemarks = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.listStatus = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textWriteOff = new OpenDental.ValidDouble();
			this.textInsPayEst = new OpenDental.ValidDouble();
			this.label10 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupProcedure = new System.Windows.Forms.GroupBox();
			this.label17 = new System.Windows.Forms.Label();
			this.textAmount = new OpenDental.ValidDouble();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textSecPercent = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textPriPercent = new System.Windows.Forms.TextBox();
			this.textSecEst = new System.Windows.Forms.TextBox();
			this.labelSecEst = new System.Windows.Forms.Label();
			this.textPriEst = new System.Windows.Forms.TextBox();
			this.labelPriEst = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.labelSecOver = new System.Windows.Forms.Label();
			this.labelPriOver = new System.Windows.Forms.Label();
			this.textOverrideSec = new OpenDental.ValidDouble();
			this.textOverridePri = new OpenDental.ValidDouble();
			this.labelAttached = new System.Windows.Forms.Label();
			this.groupTotal.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupProcedure.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(690, 414);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 0;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(690, 450);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(36, 450);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(87, 26);
			this.butDelete.TabIndex = 3;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// labelFee
			// 
			this.labelFee.Location = new System.Drawing.Point(12, 18);
			this.labelFee.Name = "labelFee";
			this.labelFee.Size = new System.Drawing.Size(107, 17);
			this.labelFee.TabIndex = 4;
			this.labelFee.Text = "Fee Billed";
			this.labelFee.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(21, 27);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(97, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "Description";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(224, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(73, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "Provider";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupTotal
			// 
			this.groupTotal.Controls.Add(this.listProv);
			this.groupTotal.Controls.Add(this.label8);
			this.groupTotal.Controls.Add(this.textDate);
			this.groupTotal.Controls.Add(this.label4);
			this.groupTotal.Controls.Add(this.label1);
			this.groupTotal.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupTotal.Location = new System.Drawing.Point(218, 315);
			this.groupTotal.Name = "groupTotal";
			this.groupTotal.Size = new System.Drawing.Size(445, 163);
			this.groupTotal.TabIndex = 7;
			this.groupTotal.TabStop = false;
			this.groupTotal.Text = "Total Payment";
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(300, 25);
			this.listProv.Name = "listProv";
			this.listProv.Size = new System.Drawing.Size(120, 121);
			this.listProv.TabIndex = 10;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(35, 52);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(170, 69);
			this.label8.TabIndex = 9;
			this.label8.Text = "Warning!  The date should be the date of service, NOT the date of payment.";
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(89, 23);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(88, 20);
			this.textDate.TabIndex = 7;
			this.textDate.Text = "";
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(122, 25);
			this.textDescription.Name = "textDescription";
			this.textDescription.ReadOnly = true;
			this.textDescription.Size = new System.Drawing.Size(203, 20);
			this.textDescription.TabIndex = 8;
			this.textDescription.Text = "";
			// 
			// textFeeBilled
			// 
			this.textFeeBilled.Location = new System.Drawing.Point(120, 14);
			this.textFeeBilled.Name = "textFeeBilled";
			this.textFeeBilled.TabIndex = 9;
			this.textFeeBilled.Text = "";
			// 
			// textDedApplied
			// 
			this.textDedApplied.Location = new System.Drawing.Point(120, 34);
			this.textDedApplied.Name = "textDedApplied";
			this.textDedApplied.TabIndex = 10;
			this.textDedApplied.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 38);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(114, 17);
			this.label5.TabIndex = 11;
			this.label5.Text = "Deductible Applied";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 78);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(111, 17);
			this.label6.TabIndex = 13;
			this.label6.Text = "Insurance Paid";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textInsPayAmt
			// 
			this.textInsPayAmt.Location = new System.Drawing.Point(120, 74);
			this.textInsPayAmt.Name = "textInsPayAmt";
			this.textInsPayAmt.TabIndex = 12;
			this.textInsPayAmt.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(18, 116);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 17);
			this.label7.TabIndex = 14;
			this.label7.Text = "Remarks";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textRemarks
			// 
			this.textRemarks.Location = new System.Drawing.Point(120, 114);
			this.textRemarks.MaxLength = 255;
			this.textRemarks.Multiline = true;
			this.textRemarks.Name = "textRemarks";
			this.textRemarks.Size = new System.Drawing.Size(243, 98);
			this.textRemarks.TabIndex = 15;
			this.textRemarks.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(38, 212);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 17);
			this.label9.TabIndex = 16;
			this.label9.Text = "Status";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listStatus
			// 
			this.listStatus.Location = new System.Drawing.Point(120, 212);
			this.listStatus.Name = "listStatus";
			this.listStatus.Size = new System.Drawing.Size(120, 56);
			this.listStatus.TabIndex = 17;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 98);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 17);
			this.label2.TabIndex = 19;
			this.label2.Text = "Write Off";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textWriteOff
			// 
			this.textWriteOff.Location = new System.Drawing.Point(120, 94);
			this.textWriteOff.Name = "textWriteOff";
			this.textWriteOff.TabIndex = 18;
			this.textWriteOff.Text = "";
			// 
			// textInsPayEst
			// 
			this.textInsPayEst.Location = new System.Drawing.Point(120, 54);
			this.textInsPayEst.Name = "textInsPayEst";
			this.textInsPayEst.TabIndex = 20;
			this.textInsPayEst.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(6, 58);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(114, 17);
			this.label10.TabIndex = 21;
			this.label10.Text = "Insurance Estimate";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelFee);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textInsPayEst);
			this.groupBox1.Controls.Add(this.textFeeBilled);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.listStatus);
			this.groupBox1.Controls.Add(this.textRemarks);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textWriteOff);
			this.groupBox1.Controls.Add(this.textDedApplied);
			this.groupBox1.Controls.Add(this.textInsPayAmt);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(394, 22);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(374, 278);
			this.groupBox1.TabIndex = 24;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "For This Claim";
			// 
			// groupProcedure
			// 
			this.groupProcedure.Controls.Add(this.label17);
			this.groupProcedure.Controls.Add(this.textAmount);
			this.groupProcedure.Controls.Add(this.groupBox4);
			this.groupProcedure.Controls.Add(this.groupBox3);
			this.groupProcedure.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupProcedure.Location = new System.Drawing.Point(23, 66);
			this.groupProcedure.Name = "groupProcedure";
			this.groupProcedure.Size = new System.Drawing.Size(362, 234);
			this.groupProcedure.TabIndex = 25;
			this.groupProcedure.TabStop = false;
			this.groupProcedure.Text = "Procedure";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(7, 34);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(75, 16);
			this.label17.TabIndex = 43;
			this.label17.Text = "Amount:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(82, 33);
			this.textAmount.Name = "textAmount";
			this.textAmount.ReadOnly = true;
			this.textAmount.Size = new System.Drawing.Size(66, 20);
			this.textAmount.TabIndex = 44;
			this.textAmount.Text = "";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.textSecPercent);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Controls.Add(this.textPriPercent);
			this.groupBox4.Controls.Add(this.textSecEst);
			this.groupBox4.Controls.Add(this.labelSecEst);
			this.groupBox4.Controls.Add(this.textPriEst);
			this.groupBox4.Controls.Add(this.labelPriEst);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(7, 80);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(187, 96);
			this.groupBox4.TabIndex = 42;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Estimate";
			// 
			// textSecPercent
			// 
			this.textSecPercent.Location = new System.Drawing.Point(75, 63);
			this.textSecPercent.Name = "textSecPercent";
			this.textSecPercent.ReadOnly = true;
			this.textSecPercent.Size = new System.Drawing.Size(40, 20);
			this.textSecPercent.TabIndex = 6;
			this.textSecPercent.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(63, 23);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(64, 14);
			this.label11.TabIndex = 5;
			this.label11.Text = "Percent";
			this.label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textPriPercent
			// 
			this.textPriPercent.Location = new System.Drawing.Point(75, 39);
			this.textPriPercent.Name = "textPriPercent";
			this.textPriPercent.ReadOnly = true;
			this.textPriPercent.Size = new System.Drawing.Size(40, 20);
			this.textPriPercent.TabIndex = 4;
			this.textPriPercent.Text = "";
			// 
			// textSecEst
			// 
			this.textSecEst.Location = new System.Drawing.Point(120, 63);
			this.textSecEst.Name = "textSecEst";
			this.textSecEst.ReadOnly = true;
			this.textSecEst.Size = new System.Drawing.Size(60, 20);
			this.textSecEst.TabIndex = 3;
			this.textSecEst.Text = "";
			// 
			// labelSecEst
			// 
			this.labelSecEst.Location = new System.Drawing.Point(4, 65);
			this.labelSecEst.Name = "labelSecEst";
			this.labelSecEst.Size = new System.Drawing.Size(65, 16);
			this.labelSecEst.TabIndex = 2;
			this.labelSecEst.Text = "Sec. Ins";
			this.labelSecEst.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPriEst
			// 
			this.textPriEst.Location = new System.Drawing.Point(120, 39);
			this.textPriEst.Name = "textPriEst";
			this.textPriEst.ReadOnly = true;
			this.textPriEst.Size = new System.Drawing.Size(60, 20);
			this.textPriEst.TabIndex = 1;
			this.textPriEst.Text = "";
			// 
			// labelPriEst
			// 
			this.labelPriEst.Location = new System.Drawing.Point(3, 41);
			this.labelPriEst.Name = "labelPriEst";
			this.labelPriEst.Size = new System.Drawing.Size(67, 16);
			this.labelPriEst.TabIndex = 0;
			this.labelPriEst.Text = "Prim. Ins";
			this.labelPriEst.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.labelSecOver);
			this.groupBox3.Controls.Add(this.labelPriOver);
			this.groupBox3.Controls.Add(this.textOverrideSec);
			this.groupBox3.Controls.Add(this.textOverridePri);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(199, 80);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(151, 96);
			this.groupBox3.TabIndex = 41;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Override Estimate";
			// 
			// labelSecOver
			// 
			this.labelSecOver.Location = new System.Drawing.Point(3, 66);
			this.labelSecOver.Name = "labelSecOver";
			this.labelSecOver.Size = new System.Drawing.Size(66, 16);
			this.labelSecOver.TabIndex = 15;
			this.labelSecOver.Text = "Sec. Ins";
			this.labelSecOver.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelPriOver
			// 
			this.labelPriOver.Location = new System.Drawing.Point(2, 42);
			this.labelPriOver.Name = "labelPriOver";
			this.labelPriOver.Size = new System.Drawing.Size(67, 16);
			this.labelPriOver.TabIndex = 14;
			this.labelPriOver.Text = "Prim. Ins";
			this.labelPriOver.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textOverrideSec
			// 
			this.textOverrideSec.Location = new System.Drawing.Point(74, 62);
			this.textOverrideSec.Name = "textOverrideSec";
			this.textOverrideSec.Size = new System.Drawing.Size(68, 20);
			this.textOverrideSec.TabIndex = 1;
			this.textOverrideSec.Text = "";
			this.textOverrideSec.Leave += new System.EventHandler(this.textOverrideSec_Leave);
			this.textOverrideSec.TextChanged += new System.EventHandler(this.textOverrideSec_TextChanged);
			// 
			// textOverridePri
			// 
			this.textOverridePri.Location = new System.Drawing.Point(74, 38);
			this.textOverridePri.Name = "textOverridePri";
			this.textOverridePri.Size = new System.Drawing.Size(68, 20);
			this.textOverridePri.TabIndex = 0;
			this.textOverridePri.Text = "";
			this.textOverridePri.Leave += new System.EventHandler(this.textOverridePri_Leave);
			this.textOverridePri.TextChanged += new System.EventHandler(this.textOverridePri_TextChanged);
			// 
			// labelAttached
			// 
			this.labelAttached.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelAttached.Location = new System.Drawing.Point(30, 351);
			this.labelAttached.Name = "labelAttached";
			this.labelAttached.Size = new System.Drawing.Size(156, 81);
			this.labelAttached.TabIndex = 26;
			this.labelAttached.Text = "This claim procedure is attached to a payment, so certain changes are not allowed" +
				".";
			this.labelAttached.Visible = false;
			// 
			// FormClaimProcEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(779, 495);
			this.Controls.Add(this.labelAttached);
			this.Controls.Add(this.groupProcedure);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupTotal);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDescription);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimProcEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Claim Procedure";
			this.Load += new System.EventHandler(this.FormClaimProcEdit_Load);
			this.groupTotal.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupProcedure.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimProcEdit_Load(object sender, System.EventArgs e) {
			if(ClaimProcs.Cur.ProcNum==0){//total payment
				IsProc=false;
				labelFee.Visible=false;
				textFeeBilled.Visible=false;
				groupProcedure.Visible=false;
				textDescription.Text="Total Payment";
			}
			else{//procedure
				IsProc=true;
				groupTotal.Visible=false;
				butDelete.Text=Lan.g(this,"Remove");
				Procedures.Cur=(Procedure)Procedures.HList[ClaimProcs.Cur.ProcNum];
				textDescription.Text=ProcCodes.GetProcCode(Procedures.Cur.ADACode).Descript;
				textAmount.Text=Procedures.Cur.ProcFee.ToString("F");
				if(Procedures.Cur.OverridePri==-1)
					textOverridePri.Text="";
				else
					textOverridePri.Text=Procedures.Cur.OverridePri.ToString("F");
				if(Procedures.Cur.OverrideSec==-1)
					textOverrideSec.Text="";
				else 
					textOverrideSec.Text=Procedures.Cur.OverrideSec.ToString("F");
				double amt=PIn.PDouble(textAmount.Text.ToString());
				double priPercent=CovPats.GetPercent(Procedures.Cur.ADACode,PriSecTot.Pri);
				double secPercent=CovPats.GetPercent(Procedures.Cur.ADACode,PriSecTot.Sec);
				textPriPercent.Text=(priPercent*100).ToString();
				textSecPercent.Text=(secPercent*100).ToString();
				double priEst=Procedures.Cur.ProcFee*priPercent;
				double secEst=Procedures.Cur.ProcFee*secPercent;
				textPriEst.Text=(priEst).ToString("F");
				if(Procedures.Cur.ProcFee-priEst < secEst)
					secEst=Procedures.Cur.ProcFee-priEst;
				textSecEst.Text=secEst.ToString("F");
				if(Claims.Cur.ClaimType=="P"){
					labelPriEst.Font=new Font("Microsoft Sans Serif",9,FontStyle.Bold);
					labelPriOver.Font=new Font("Microsoft Sans Serif",9,FontStyle.Bold);
				}
				else if(Claims.Cur.ClaimType=="S"){
					labelSecEst.Font=new Font("Microsoft Sans Serif",9,FontStyle.Bold);
					labelSecOver.Font=new Font("Microsoft Sans Serif",9,FontStyle.Bold);
				}
			}
			textFeeBilled.Text=ClaimProcs.Cur.FeeBilled.ToString("F");
			textDedApplied.Text=ClaimProcs.Cur.DedApplied.ToString("F");
			textInsPayEst.Text=ClaimProcs.Cur.InsPayEst.ToString("F");
			textInsPayAmt.Text=ClaimProcs.Cur.InsPayAmt.ToString("F");
			textWriteOff.Text=ClaimProcs.Cur.WriteOff.ToString("F");
			textRemarks.Text=ClaimProcs.Cur.Remarks;
			listStatus.Items.Add(Lan.g(this,"Not Received"));
			listStatus.Items.Add(Lan.g(this,"Received"));
			listStatus.Items.Add(Lan.g(this,"PreAuthorization"));
			listStatus.Items.Add(Lan.g(this,"Supplemental"));
			switch(ClaimProcs.Cur.Status){
				case ClaimProcStatus.NotReceived:
					listStatus.SelectedIndex=0;
					break;
				case ClaimProcStatus.Received:
					listStatus.SelectedIndex=1;
					break;
				case ClaimProcStatus.Preauth:
					listStatus.SelectedIndex=2;
					break;
				//adjustments have a completely different user interface. Can not access from claim.
				case ClaimProcStatus.Supplemental:
					listStatus.SelectedIndex=3;
					break;
			}
			textDate.Text=ClaimProcs.Cur.DateCP.ToShortDateString();
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add(Providers.List[i].Abbr);
				if(ClaimProcs.Cur.ProvNum==Providers.List[i].ProvNum){
					listProv.SelectedIndex=i;
				}
			}
			if(ClaimProcs.Cur.ClaimPaymentNum>0){
				labelAttached.Visible=true;
				textInsPayAmt.ReadOnly=true;
				listStatus.Enabled=false;
				butDelete.Enabled=false;
			}
		}

		private void textOverridePri_TextChanged(object sender, System.EventArgs e) {
			priChanged=true;
		}

		private void textOverridePri_Leave(object sender, System.EventArgs e) {
			if(!priChanged)
				return;
			priChanged=false;
			if(  textOverridePri.errorProvider1.GetError(textOverridePri)!=""
				){
				//MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Save changes?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			if(textOverridePri.Text=="")
				Procedures.Cur.OverridePri=-1;
			else{
				Procedures.Cur.OverridePri=System.Convert.ToDouble(textOverridePri.Text);
				textOverridePri.Text=Procedures.Cur.OverridePri.ToString("F");
				priChanged=false;
			}
			if(Claims.Cur.ClaimType=="P"){
				textInsPayEst.Text=textOverridePri.Text;
			}
			Procedures.UpdateCur();
			Procedures.Refresh();//?
		}

		private void textOverrideSec_TextChanged(object sender, System.EventArgs e) {
			secChanged=true;
		}

		private void textOverrideSec_Leave(object sender, System.EventArgs e) {
			if(!secChanged)
				return;
			secChanged=false;
			if(  textOverrideSec.errorProvider1.GetError(textOverrideSec)!=""
				){
				//MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Save changes?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			if(textOverrideSec.Text=="")
				Procedures.Cur.OverrideSec=-1;
			else{
				Procedures.Cur.OverrideSec=System.Convert.ToDouble(textOverrideSec.Text);
				textOverrideSec.Text=Procedures.Cur.OverrideSec.ToString("F");
				secChanged=false;
			}
			if(Claims.Cur.ClaimType=="S"){
				textInsPayEst.Text=textOverrideSec.Text;
			}
			Procedures.UpdateCur();
			Procedures.Refresh();//?
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(ClaimProcs.Cur.ClaimPaymentNum>0){
				MessageBox.Show(Lan.g(this,"This item is already attached to a payment, so you would need to detach it from the payment first."));
				return;
			}
			if(IsProc){
				if(MessageBox.Show(Lan.g(this,"Remove this procedure from this claim?"),""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				ClaimProcs.DeleteCur();
				DialogResult=DialogResult.OK;
			}
			else{//total payment
				if(MessageBox.Show(Lan.g(this,"Delete this payment?"),""
					,MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
				ClaimProcs.DeleteCur();
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textFeeBilled.errorProvider1.GetError(textFeeBilled)!=""
				|| textDedApplied.errorProvider1.GetError(textDedApplied)!=""
				|| textInsPayEst.errorProvider1.GetError(textInsPayEst)!=""
				|| textInsPayAmt.errorProvider1.GetError(textInsPayAmt)!=""
				|| textWriteOff.errorProvider1.GetError(textWriteOff)!=""
				|| textDate.errorProvider1.GetError(textDate)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			ClaimProcs.Cur.FeeBilled=PIn.PDouble(textFeeBilled.Text);
			ClaimProcs.Cur.DedApplied=PIn.PDouble(textDedApplied.Text);
			ClaimProcs.Cur.InsPayEst=PIn.PDouble(textInsPayEst.Text);
			ClaimProcs.Cur.InsPayAmt=PIn.PDouble(textInsPayAmt.Text);
			ClaimProcs.Cur.WriteOff=PIn.PDouble(textWriteOff.Text);
			ClaimProcs.Cur.Remarks=textRemarks.Text;
			switch(listStatus.SelectedIndex){
				case 0:
					ClaimProcs.Cur.Status=ClaimProcStatus.NotReceived;
					break;
				case 1:
					ClaimProcs.Cur.Status=ClaimProcStatus.Received;
					break;
				case 2:
					ClaimProcs.Cur.Status=ClaimProcStatus.Preauth;
					break;
				case 3:
					ClaimProcs.Cur.Status=ClaimProcStatus.Supplemental;
					break;
			}
			ClaimProcs.Cur.DateCP=PIn.PDate(textDate.Text);
			if(listProv.SelectedIndex!=-1)//if no prov selected, then that prov must simply be hidden,
													//because all claimprocs are initially created with a prov.
													//So, in this case, don't change.
				ClaimProcs.Cur.ProvNum=Providers.List[listProv.SelectedIndex].ProvNum;
			ClaimProcs.UpdateCur();
			//there is no functionality here for insert cur, because all claimprocs are
			//created before editing.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	}
}

















