using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormInsAdj : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private OpenDental.ValidDate textDate;
		private OpenDental.ValidDouble textInsUsed;
		private OpenDental.ValidDouble textDedUsed;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.ComponentModel.Container components = null;
		public bool IsNew;

		public FormInsAdj(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label2,
				label3,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.textInsUsed = new OpenDental.ValidDouble();
			this.textDedUsed = new OpenDental.ValidDouble();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(26, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 14);
			this.label2.TabIndex = 1;
			this.label2.Text = "Deductible Used";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(26, 46);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 14);
			this.label3.TabIndex = 2;
			this.label3.Text = "Insurance Used";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(128, 15);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(75, 20);
			this.textDate.TabIndex = 0;
			this.textDate.Text = "";
			// 
			// textInsUsed
			// 
			this.textInsUsed.Location = new System.Drawing.Point(128, 43);
			this.textInsUsed.Name = "textInsUsed";
			this.textInsUsed.Size = new System.Drawing.Size(69, 20);
			this.textInsUsed.TabIndex = 1;
			this.textInsUsed.Text = "";
			// 
			// textDedUsed
			// 
			this.textDedUsed.Location = new System.Drawing.Point(128, 71);
			this.textDedUsed.Name = "textDedUsed";
			this.textDedUsed.Size = new System.Drawing.Size(69, 20);
			this.textDedUsed.TabIndex = 2;
			this.textDedUsed.Text = "";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(259, 126);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(259, 162);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormInsAdj
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(354, 208);
			this.ControlBox = false;
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textDedUsed);
			this.Controls.Add(this.textInsUsed);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "FormInsAdj";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Edit Insurance Adjustments");
			this.Load += new System.EventHandler(this.FormInsAdj_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsAdj_Load(object sender, System.EventArgs e) {
			textDate.Text=Claims.Cur.DateService.ToShortDateString();
			textInsUsed.Text=Claims.Cur.InsPayAmt.ToString("F");
			textDedUsed.Text=Claims.Cur.DedApplied.ToString("F");
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDate.errorProvider1.GetError(textDate)!=""
				|| textInsUsed.errorProvider1.GetError(textInsUsed)!=""
				|| textDedUsed.errorProvider1.GetError(textDedUsed)!=""
				){
				MessageBox.Show(Lan.g("All","Please fix data entry errors first."));
				return;
			}
			Claims.Cur.DateService=PIn.PDate(textDate.Text);
			Claims.Cur.InsPayAmt=PIn.PDouble(textInsUsed.Text);
			Claims.Cur.DedApplied=PIn.PDouble(textDedUsed.Text);
			if(IsNew){
				Claims.InsertCur();
			}
			else{
				Claims.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		
	}
}
