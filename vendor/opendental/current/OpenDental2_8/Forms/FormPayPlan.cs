using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDental.Reporting;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormPayPlan : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butChange;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.ValidDate textDate;
		private OpenDental.ValidDouble textAmount;
		private OpenDental.ValidDate textDateFirstPay;
		private OpenDental.ValidDouble textAPR;
		private OpenDental.ValidDouble textMonthlyPayment;
		private OpenDental.ValidNum textTerm;
		private System.Windows.Forms.Button butPrint;
		private System.Windows.Forms.TextBox textGuarantor;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.Button butGoToGuar;
		private System.Windows.Forms.Button butGoToPat;
		private System.Windows.Forms.TextBox textPatient;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.GroupBox groupBox3;
		private OpenDental.ValidDouble textDownPayment;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.PrintDialog printDialog2;
		private System.Windows.Forms.Label label3;
		private OpenDental.ValidDouble textLastPayment;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textAmtPaid;
		/// <summary>Go to the specified patnum.  Upon dialog close, if this number is not 0, 
		/// then patients.Cur will be changed to this new patnum, and Account refreshed to the 
		/// new patient.</summary>
		public int GotoPatNum;
		private System.Windows.Forms.Label label13;
		//private double amtPaid;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textTotalCost;
		private System.Windows.Forms.TextBox textCurrentDue;
		private System.Windows.Forms.TextBox textMonthsDue;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label10;
		private OpenDental.XPButton butDelete;
		private System.Windows.Forms.Button butCopyTerms;
		private System.Windows.Forms.Label label14;

		///<summary></summary>
		public FormPayPlan()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormPayPlan));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textGuarantor = new System.Windows.Forms.TextBox();
			this.butChange = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.label4 = new System.Windows.Forms.Label();
			this.textAmount = new OpenDental.ValidDouble();
			this.textDateFirstPay = new OpenDental.ValidDate();
			this.label5 = new System.Windows.Forms.Label();
			this.textAPR = new OpenDental.ValidDouble();
			this.label6 = new System.Windows.Forms.Label();
			this.textMonthlyPayment = new OpenDental.ValidDouble();
			this.label7 = new System.Windows.Forms.Label();
			this.textTerm = new OpenDental.ValidNum();
			this.label8 = new System.Windows.Forms.Label();
			this.butPrint = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textTotalCost = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.textLastPayment = new OpenDental.ValidDouble();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textDownPayment = new OpenDental.ValidDouble();
			this.label11 = new System.Windows.Forms.Label();
			this.butGoToGuar = new System.Windows.Forms.Button();
			this.butGoToPat = new System.Windows.Forms.Button();
			this.textPatient = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.label12 = new System.Windows.Forms.Label();
			this.textAmtPaid = new System.Windows.Forms.TextBox();
			this.textCurrentDue = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.textMonthsDue = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.XPButton();
			this.butCopyTerms = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(572, 516);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(479, 516);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(86, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Guarantor";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textGuarantor
			// 
			this.textGuarantor.Location = new System.Drawing.Point(187, 42);
			this.textGuarantor.Name = "textGuarantor";
			this.textGuarantor.ReadOnly = true;
			this.textGuarantor.Size = new System.Drawing.Size(179, 20);
			this.textGuarantor.TabIndex = 3;
			this.textGuarantor.Text = "";
			// 
			// butChange
			// 
			this.butChange.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butChange.Location = new System.Drawing.Point(464, 41);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(75, 22);
			this.butChange.TabIndex = 4;
			this.butChange.Text = "C&hange";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(30, 65);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(158, 17);
			this.label2.TabIndex = 5;
			this.label2.Text = "Date of Agreement";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(187, 64);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(85, 20);
			this.textDate.TabIndex = 7;
			this.textDate.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(39, 87);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(148, 17);
			this.label4.TabIndex = 10;
			this.label4.Text = "Total Amount";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(187, 86);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(73, 20);
			this.textAmount.TabIndex = 11;
			this.textAmount.Text = "";
			this.textAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textAmount_KeyUp);
			// 
			// textDateFirstPay
			// 
			this.textDateFirstPay.Location = new System.Drawing.Point(165, 13);
			this.textDateFirstPay.Name = "textDateFirstPay";
			this.textDateFirstPay.Size = new System.Drawing.Size(83, 20);
			this.textDateFirstPay.TabIndex = 13;
			this.textDateFirstPay.Text = "";
			this.textDateFirstPay.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textDateFirstPay_KeyUp);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(15, 15);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(149, 17);
			this.label5.TabIndex = 12;
			this.label5.Text = "Date of first payment";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAPR
			// 
			this.textAPR.Location = new System.Drawing.Point(165, 57);
			this.textAPR.Name = "textAPR";
			this.textAPR.Size = new System.Drawing.Size(58, 20);
			this.textAPR.TabIndex = 15;
			this.textAPR.Text = "";
			this.textAPR.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textAPR_KeyUp);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(20, 59);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(148, 17);
			this.label6.TabIndex = 14;
			this.label6.Text = "APR (for example 0 or 18)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMonthlyPayment
			// 
			this.textMonthlyPayment.Location = new System.Drawing.Point(154, 40);
			this.textMonthlyPayment.Name = "textMonthlyPayment";
			this.textMonthlyPayment.Size = new System.Drawing.Size(72, 20);
			this.textMonthlyPayment.TabIndex = 17;
			this.textMonthlyPayment.Text = "";
			this.textMonthlyPayment.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textMonthlyPayment_KeyUp);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(17, 41);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(134, 17);
			this.label7.TabIndex = 16;
			this.label7.Text = "Monthly Payment";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTerm
			// 
			this.textTerm.Location = new System.Drawing.Point(154, 17);
			this.textTerm.Name = "textTerm";
			this.textTerm.Size = new System.Drawing.Size(43, 20);
			this.textTerm.TabIndex = 18;
			this.textTerm.Text = "";
			this.textTerm.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textTerm_KeyUp);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(15, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(137, 17);
			this.label8.TabIndex = 19;
			this.label8.Text = "Number of Payments";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butPrint
			// 
			this.butPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butPrint.Location = new System.Drawing.Point(36, 25);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(80, 26);
			this.butPrint.TabIndex = 20;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butPrint);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(486, 436);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(155, 58);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Truth in Lending Disclosure";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textTotalCost);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.textAPR);
			this.groupBox2.Controls.Add(this.textLastPayment);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Controls.Add(this.textDownPayment);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.textDateFirstPay);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(22, 104);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(300, 204);
			this.groupBox2.TabIndex = 22;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Terms";
			// 
			// textTotalCost
			// 
			this.textTotalCost.Location = new System.Drawing.Point(165, 177);
			this.textTotalCost.Name = "textTotalCost";
			this.textTotalCost.ReadOnly = true;
			this.textTotalCost.Size = new System.Drawing.Size(73, 20);
			this.textTotalCost.TabIndex = 35;
			this.textTotalCost.Text = "";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(13, 179);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(152, 17);
			this.label15.TabIndex = 34;
			this.label15.Text = "Total Cost of Loan";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textLastPayment
			// 
			this.textLastPayment.Location = new System.Drawing.Point(165, 155);
			this.textLastPayment.Name = "textLastPayment";
			this.textLastPayment.ReadOnly = true;
			this.textLastPayment.Size = new System.Drawing.Size(73, 20);
			this.textLastPayment.TabIndex = 25;
			this.textLastPayment.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(15, 157);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(148, 17);
			this.label3.TabIndex = 24;
			this.label3.Text = "Plus Last Payment";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.textMonthlyPayment);
			this.groupBox3.Controls.Add(this.textTerm);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(11, 82);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(242, 68);
			this.groupBox3.TabIndex = 23;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Either";
			// 
			// textDownPayment
			// 
			this.textDownPayment.Location = new System.Drawing.Point(165, 35);
			this.textDownPayment.Name = "textDownPayment";
			this.textDownPayment.Size = new System.Drawing.Size(73, 20);
			this.textDownPayment.TabIndex = 22;
			this.textDownPayment.Text = "";
			this.textDownPayment.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textDownPayment_KeyUp);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(17, 39);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(148, 17);
			this.label11.TabIndex = 21;
			this.label11.Text = "Down Payment";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butGoToGuar
			// 
			this.butGoToGuar.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butGoToGuar.Location = new System.Drawing.Point(373, 41);
			this.butGoToGuar.Name = "butGoToGuar";
			this.butGoToGuar.Size = new System.Drawing.Size(75, 22);
			this.butGoToGuar.TabIndex = 23;
			this.butGoToGuar.Text = "Go &To";
			this.butGoToGuar.Click += new System.EventHandler(this.butGoTo_Click);
			// 
			// butGoToPat
			// 
			this.butGoToPat.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butGoToPat.Location = new System.Drawing.Point(373, 19);
			this.butGoToPat.Name = "butGoToPat";
			this.butGoToPat.Size = new System.Drawing.Size(75, 22);
			this.butGoToPat.TabIndex = 27;
			this.butGoToPat.Text = "&Go To";
			this.butGoToPat.Click += new System.EventHandler(this.butGoToPat_Click);
			// 
			// textPatient
			// 
			this.textPatient.Location = new System.Drawing.Point(187, 20);
			this.textPatient.Name = "textPatient";
			this.textPatient.ReadOnly = true;
			this.textPatient.Size = new System.Drawing.Size(179, 20);
			this.textPatient.TabIndex = 25;
			this.textPatient.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(34, 20);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(151, 17);
			this.label9.TabIndex = 24;
			this.label9.Text = "Patient";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(39, 358);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(148, 17);
			this.label12.TabIndex = 30;
			this.label12.Text = "Paid so far";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAmtPaid
			// 
			this.textAmtPaid.Location = new System.Drawing.Point(187, 356);
			this.textAmtPaid.Name = "textAmtPaid";
			this.textAmtPaid.ReadOnly = true;
			this.textAmtPaid.Size = new System.Drawing.Size(73, 20);
			this.textAmtPaid.TabIndex = 31;
			this.textAmtPaid.Text = "";
			// 
			// textCurrentDue
			// 
			this.textCurrentDue.Location = new System.Drawing.Point(187, 334);
			this.textCurrentDue.Name = "textCurrentDue";
			this.textCurrentDue.ReadOnly = true;
			this.textCurrentDue.Size = new System.Drawing.Size(73, 20);
			this.textCurrentDue.TabIndex = 33;
			this.textCurrentDue.Text = "";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(39, 336);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(148, 17);
			this.label13.TabIndex = 32;
			this.label13.Text = "Current Due";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textMonthsDue
			// 
			this.textMonthsDue.Location = new System.Drawing.Point(187, 312);
			this.textMonthsDue.Name = "textMonthsDue";
			this.textMonthsDue.ReadOnly = true;
			this.textMonthsDue.Size = new System.Drawing.Size(73, 20);
			this.textMonthsDue.TabIndex = 35;
			this.textMonthsDue.Text = "";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(39, 314);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(148, 17);
			this.label14.TabIndex = 34;
			this.label14.Text = "Months Due";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(23, 397);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(430, 108);
			this.textNote.TabIndex = 36;
			this.textNote.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(23, 378);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(148, 17);
			this.label10.TabIndex = 37;
			this.label10.Text = "Note";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(22, 516);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(84, 26);
			this.butDelete.TabIndex = 38;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butCopyTerms
			// 
			this.butCopyTerms.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCopyTerms.Location = new System.Drawing.Point(495, 398);
			this.butCopyTerms.Name = "butCopyTerms";
			this.butCopyTerms.Size = new System.Drawing.Size(140, 26);
			this.butCopyTerms.TabIndex = 39;
			this.butCopyTerms.Text = "Copy Terms to &Note";
			this.butCopyTerms.Click += new System.EventHandler(this.butCopyTerms_Click);
			// 
			// FormPayPlan
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(666, 554);
			this.Controls.Add(this.butCopyTerms);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textMonthsDue);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.textCurrentDue);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.textAmtPaid);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.butGoToPat);
			this.Controls.Add(this.textPatient);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.butGoToGuar);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butChange);
			this.Controls.Add(this.textGuarantor);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPayPlan";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Payment Plan";
			this.Load += new System.EventHandler(this.FormPayPlan_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPayPlan_Load(object sender, System.EventArgs e) {
			Patients.GetLim(PayPlans.Cur.PatNum);
			textPatient.Text=Patients.LimName;
			Patients.GetLim(PayPlans.Cur.Guarantor);
			textGuarantor.Text=Patients.LimName;
			textDate.Text=PayPlans.Cur.PayPlanDate.ToShortDateString();
			textAmount.Text=PayPlans.Cur.TotalAmount.ToString("n");
			textCurrentDue.Text=PayPlans.Cur.CurrentDue.ToString("n");
			textDateFirstPay.Text=PayPlans.Cur.DateFirstPay.ToShortDateString();
			textDownPayment.Text=PayPlans.Cur.DownPayment.ToString("n");
			textAPR.Text=PayPlans.Cur.APR.ToString();
			textMonthlyPayment.Text=PayPlans.Cur.MonthlyPayment.ToString("n");
			textTerm.Text=PayPlans.Cur.Term.ToString();
			textAmtPaid.Text=PayPlans.GetAmtPaid(PayPlans.Cur.PayPlanNum).ToString("n");
			FillCurrentDue();
			textNote.Text=PayPlans.Cur.Note;
		}

		private void butGoToPat_Click(object sender, System.EventArgs e) {
			if(!DataIsValid())
				return;
			SetCurFromText();
			SaveData();
			GotoPatNum=PayPlans.Cur.PatNum;
			DialogResult=DialogResult.OK;
		}

		private void butGoTo_Click(object sender, System.EventArgs e) {
			if(!DataIsValid())
				return;
			SetCurFromText();
			SaveData();
			GotoPatNum=PayPlans.Cur.Guarantor;
			DialogResult=DialogResult.OK;
		}

		private void butChange_Click(object sender, System.EventArgs e) {
			if(textAmtPaid.Text!="0.00"){
				MessageBox.Show(Lan.g(this,"Not allowed to change the guarantor because payments are attached."));
				return;
			}
			int patNum=Patients.Cur.PatNum;
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.OnlyChangingFam=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK){
				return;
			}
			PayPlans.Cur.Guarantor=Patients.Cur.PatNum;
			Patients.Cur.PatNum=patNum;//return patnum to original value.
			Patients.GetLim(PayPlans.Cur.Guarantor);
			textGuarantor.Text=Patients.LimName;
		}

		private void FillCurrentDue(){
			//also fills total cost.  Must be run after other blanks filled in
			if(!DataIsValid())
				return;
			textMonthsDue.Text=PayPlans.GetMonthsDue().ToString();
			textTotalCost.Text=(PayPlans.Cur.DownPayment+PayPlans.Cur.Term*PayPlans.Cur.MonthlyPayment
				+PIn.PDouble(textLastPayment.Text)).ToString("n");
			PayPlans.Cur.CurrentDue=PayPlans.GetAmtDue();
			textCurrentDue.Text=PayPlans.Cur.CurrentDue.ToString("n");
		}

		private void textAmount_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			FillMonthlyPayment();
		}

		private void textDateFirstPay_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			FillMonthlyPayment();
		}

		private void textDownPayment_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			FillMonthlyPayment();
		}

		private void textAPR_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			FillMonthlyPayment();
		}

		private void textTerm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			FillMonthlyPayment();
		}

		private void FillMonthlyPayment(){
			if(!DataIsValid())
				return;
			double term=PIn.PDouble(textTerm.Text);
			double principal=PIn.PDouble(textAmount.Text)-PIn.PDouble(textDownPayment.Text);
			if(principal==0 || term<1){
				textMonthlyPayment.Text="";
				textLastPayment.Text="";
				textTerm.Text="";
				return;
			}
			double APR=PIn.PDouble(textAPR.Text);
			double monthlyRate;
			double monthlyPayment;
			if(APR==0){
				monthlyRate=0;
				monthlyPayment=principal/term;
			}
			else{
				monthlyRate=APR/100/12;
				monthlyPayment=principal*monthlyRate/(1-Math.Pow(1+monthlyRate,-term));
			}
			textMonthlyPayment.Text=monthlyPayment.ToString("n");
			textLastPayment.Text="";
			SetCurFromText();
			FillCurrentDue();
		}

		private void textMonthlyPayment_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(!DataIsValid())
				return;
			double monthlyPayment=PIn.PDouble(textMonthlyPayment.Text);
			double principal=PIn.PDouble(textAmount.Text)-PIn.PDouble(textDownPayment.Text);
			double monthlyRate=PIn.PDouble(textAPR.Text)/100/12;
			double term=0;//=-(Math.Log(1-(principal/monthlyPayment)*(monthlyRate)))/Math.Log(1+(monthlyRate));
				//-(LN(1-(B/m)*(r/q)))/LN(1+(r/q))
			double tempP=principal;//the principal which will be decreased to zero in the loop.
			double currentI;//current month's interest.
			double currentP;//current month's principal.
			while(tempP>0 && term<100){//the 100 limit prevents infinite loop
				currentI=tempP*monthlyRate;
				currentP=monthlyPayment-currentI;
				tempP-=currentP;
				term++;
			}
			//tempP will be negative.
			//term includes this last overpayment.
			double lastPayment=monthlyPayment+tempP;
			if(lastPayment-monthlyPayment<.02 && lastPayment-monthlyPayment>-.02){
				//negligible difference
				term++;
				textLastPayment.Text="";
			}
			else{
				textLastPayment.Text=lastPayment.ToString("n");
			}
			textTerm.Text=(term-1).ToString();
			SetCurFromText();
			FillCurrentDue();
		}

		private void butCopyTerms_Click(object sender, System.EventArgs e) {
			textNote.Text+=DateTime.Today.ToShortDateString()
				+" - Date of Agreement: "+textDate.Text
				+", Total Amount: "+textAmount.Text
				+", Date of First Payment: "+textDateFirstPay.Text
				+", Down Payment: "+textDownPayment.Text
				+", APR: "+textAPR.Text
				+", Number of Payments: "+textTerm.Text
				+", Monthly Payment: "+textMonthlyPayment.Text
				+", Plus Last Payment: "+textLastPayment.Text
				+", Total Cost of Loan: "+textTotalCost.Text;
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(!DataIsValid())
				return;
			SetCurFromText();
			Report report=new Report();
			report.AddTitle("Truth in Lending Statement");
			report.AddSubTitle(((Pref)Prefs.HList["PracticeTitle"]).ValueString);
			report.AddSubTitle(DateTime.Today.ToString("d"));
			string sectName="Report Header";
			Section section=report.Sections["Report Header"];
			section.Height=1100;
			//int sectIndex=report.Sections.GetIndexOfKind(AreaSectionKind.ReportHeader);
			Size size=new Size(300,20);//big enough for any text
			Font font=new Font("Tahoma",9);
			ContentAlignment alignL=ContentAlignment.MiddleLeft;
			ContentAlignment alignR=ContentAlignment.MiddleRight;
			int yPos=160;
			int space=40;
			int x1=175;
			int x2=275;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Guarantor",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,textGuarantor.Text,font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Date of Agreement",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,PayPlans.Cur.PayPlanDate.ToString("d"),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Total Amount",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,PayPlans.Cur.TotalAmount.ToString("c"),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"First Payment",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,PayPlans.Cur.DateFirstPay.ToString("d"),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Down Payment",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,PayPlans.Cur.DownPayment.ToString("c"),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Annual Percentage Rate",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,PayPlans.Cur.APR.ToString("f1"),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Number of Regular Monthly Payments",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,PayPlans.Cur.Term.ToString(),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Monthly Payment Amount",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,PayPlans.Cur.MonthlyPayment.ToString("c"),font,alignR));
			yPos+=space;
			string lastPayment=((double)0).ToString("c");
			if(textLastPayment.Text!=""){
				lastPayment=CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol+textLastPayment.Text;
			}
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Plus Last Payment",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,lastPayment,font,alignR));
			yPos+=space;
			double totalCost=0;
			if(textTotalCost.Text!=""){
				totalCost=PIn.PDouble(textTotalCost.Text);
			}
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Total Finance Charges",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,
				(totalCost-PayPlans.Cur.TotalAmount).ToString("c"),font,alignR));
			yPos+=space;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Total Cost of Loan",font,alignL));
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x2,yPos),size,totalCost.ToString("c"),font,alignR));
			yPos+=60;
			report.ReportObjects.Add(new ReportObject
				(sectName,new Point(x1,yPos),size,"Signature of Guarantor:",font,alignL));
			FormReport FormR=new FormReport();
			FormR.MyReport=report;
			FormR.ShowDialog();
			/*
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			PrintDocument tempPD = new PrintDocument();
			tempPD.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(tempPD.PrinterSettings.IsValid){
				pd2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			}
			//uses default printer if selected printer not valid
			tempPD.Dispose();
			try{	
				printDialog2=new PrintDialog();
				printDialog2.Document=pd2;
				if(printDialog2.ShowDialog()==DialogResult.OK){
					pd2.Print();
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}*/
		}

		private void pd2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			int xPos=15;//starting pos
			int yPos=(int)27.5;//starting pos
			e.Graphics.DrawString("Payment Plan Truth in Lending Statement"
				,new Font("Arial",8),Brushes.Black,(float)xPos,(float)yPos);
      //e.Graphics.DrawImage(imageTemp,xPos,yPos);
		}

		private bool DataIsValid(){
			if(  textDate.errorProvider1.GetError(textDate)!=""
				|| textAmount.errorProvider1.GetError(textAmount)!=""
				|| textDownPayment.errorProvider1.GetError(textDownPayment)!=""
				|| textDateFirstPay.errorProvider1.GetError(textDateFirstPay)!=""
				|| textAPR.errorProvider1.GetError(textAPR)!=""
				|| textMonthlyPayment.errorProvider1.GetError(textMonthlyPayment)!=""
				|| textTerm.errorProvider1.GetError(textTerm)!=""
				){
				//MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return false;
			}
			if(textAmount.Text==""){
				textAmount.Text="0";
			}
			return true;
		}

		private void SetCurFromText(){
			PayPlans.Cur.PayPlanDate=PIn.PDate(textDate.Text);
			PayPlans.Cur.TotalAmount=PIn.PDouble(textAmount.Text);
			PayPlans.Cur.CurrentDue=PIn.PDouble(textCurrentDue.Text);
			PayPlans.Cur.DownPayment=PIn.PDouble(textDownPayment.Text);
			PayPlans.Cur.DateFirstPay=PIn.PDate(textDateFirstPay.Text);
			PayPlans.Cur.APR=PIn.PDouble(textAPR.Text);
			PayPlans.Cur.MonthlyPayment=PIn.PDouble(textMonthlyPayment.Text);
			PayPlans.Cur.Term=PIn.PInt(textTerm.Text);
			PayPlans.Cur.Note=textNote.Text;
		}

		///<summary>This can only be called ONCE, so after calling, the window must close.</summary>
		private void SaveData(){
			if(IsNew)
				PayPlans.InsertCur();
			else
				PayPlans.UpdateCur();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(textAmtPaid.Text!=((double)0).ToString("n")){//technically, someone could still break this by having a payment of $0, but the chances are very remote.  Might simply not allow $0 paysplits.
				MessageBox.Show(Lan.g(this,"Not allowed to delete a payment plan with payments attached."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete payment plan?"),""
				,MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			PayPlans.DeleteCur();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!DataIsValid()){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			SetCurFromText();
			SaveData();
      DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		
		

		

		

		

		

		
	

		

		


	}
}





















