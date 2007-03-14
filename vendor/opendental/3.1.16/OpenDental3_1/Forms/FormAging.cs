using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormAging : System.Windows.Forms.Form{
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textBox1;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormAging(){
			InitializeComponent();
			Lan.F(this);
			Lan.C(this,new Control[]
				{this.textBox1});
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
			this.textDate = new OpenDental.ValidDate();
			this.label1 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(160, 208);
			this.textDate.Name = "textDate";
			this.textDate.ReadOnly = true;
			this.textDate.Size = new System.Drawing.Size(104, 20);
			this.textDate.TabIndex = 12;
			this.textDate.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 212);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(146, 14);
			this.label1.TabIndex = 13;
			this.label1.Text = "Current through:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(474, 238);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 15;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(474, 204);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 14;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(28, 16);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(468, 182);
			this.textBox1.TabIndex = 16;
			this.textBox1.Text = @"This tool recalculates aging for all patients.  It will calculate the aging as of the first of the month closest to today's date.  For instance, whether you run it on the 18th of November or on the 12th of December, the date used as a basis will be the 1st of December.  This makes billing on the first more accurate.

Since individual families are also updated automatically every time you open their account, you only need to run aging once per month to update rarely viewed accounts.

Depending on the size of your database, it could take a few minutes.   The results can be viewed in various reports.";
			// 
			// FormAging
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(566, 274);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAging";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Calculate Aging";
			this.Load += new System.EventHandler(this.FormAging_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAging_Load(object sender, System.EventArgs e) {
			textDate.Text=(PIn.PDate(((Pref)Prefs.HList["DateLastAging"]).ValueString)).ToShortDateString();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(MessageBox.Show(Lan.g(this,"Click OK to update aging.")
				,"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			Cursor=Cursors.WaitCursor;
			Patients.ResetAging();
			Ledgers.GetAllGuarantors();
			for(int i=0;i<Ledgers.AllGuarantors.Length;i++){
				Ledgers.ComputeAging(Ledgers.AllGuarantors[i],Ledgers.GetClosestFirst(DateTime.Today));
				Patients.UpdateAging(Ledgers.AllGuarantors[i],Ledgers.Bal[0],Ledgers.Bal[1],Ledgers.Bal[2]
					,Ledgers.Bal[3],Ledgers.InsEst,Ledgers.BalTotal);
			}
			Prefs.Cur=(Pref)Prefs.HList["DateLastAging"];
			Prefs.Cur.ValueString=POut.PDate(Ledgers.GetClosestFirst(DateTime.Today));
			Prefs.UpdateCur();

			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();

			Cursor=Cursors.Default;
			MessageBox.Show(Lan.g(this,"Aging Complete"));
			DialogResult=DialogResult.OK;
		}

	}
}
