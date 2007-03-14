using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormClaimReferral : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butNone;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.ListBox listRef;
		private System.Windows.Forms.TextBox textRefNum;
		private System.ComponentModel.Container components = null;
		public int ReferringProv;
		public string RefNumString;
	
		public FormClaimReferral(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label2,
				this.label3,
				this.butNone,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butAdd,
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
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.listRef = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textRefNum = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.butNone = new System.Windows.Forms.Button();
			this.butAdd = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(396, 370);
			this.butCancel.Name = "butCancel";
			this.butCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(396, 330);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 2;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listRef
			// 
			this.listRef.Location = new System.Drawing.Point(34, 54);
			this.listRef.Name = "listRef";
			this.listRef.Size = new System.Drawing.Size(150, 303);
			this.listRef.TabIndex = 0;
			this.listRef.DoubleClick += new System.EventHandler(this.listRef_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Referring Provider";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(230, 166);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 14);
			this.label2.TabIndex = 7;
			this.label2.Text = "Referral Number";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textRefNum
			// 
			this.textRefNum.Location = new System.Drawing.Point(320, 162);
			this.textRefNum.Name = "textRefNum";
			this.textRefNum.Size = new System.Drawing.Size(150, 20);
			this.textRefNum.TabIndex = 1;
			this.textRefNum.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(232, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(244, 36);
			this.label3.TabIndex = 8;
			this.label3.Text = "Only enter referring provider and referral number if required by your insurance c" +
				"arrier.";
			// 
			// butNone
			// 
			this.butNone.Location = new System.Drawing.Point(34, 370);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(68, 23);
			this.butNone.TabIndex = 9;
			this.butNone.Text = "None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// butAdd
			// 
			this.butAdd.Location = new System.Drawing.Point(114, 370);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(70, 23);
			this.butAdd.TabIndex = 10;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormClaimReferral
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(490, 410);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textRefNum);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listRef);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Name = "FormClaimReferral";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Lan.g(this,"Referring Provider");
			this.Load += new System.EventHandler(this.FormClaimReferral_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimReferral_Load(object sender, System.EventArgs e) {
			textRefNum.Text=RefNumString;
			FillRefs();
		}

		private void FillRefs(){
			Referrals.Refresh();
			listRef.Items.Clear();
			for(int i=0;i<Referrals.List.Length;i++){
				listRef.Items.Add(Referrals.List[i].LName+", "+Referrals.List[i].FName);
				if(ReferringProv==Referrals.List[i].ReferralNum){
					listRef.SelectedIndex=i;
				}
			}
		}

		private void butNone_Click(object sender, System.EventArgs e) {
			listRef.SelectedIndex=-1;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormReferralEdit FormREdit=new FormReferralEdit();
			FormREdit.IsNew=true;
			Referrals.Cur=new Referral();
			FormREdit.ShowDialog();
			FillRefs();
		}

		private void listRef_DoubleClick(object sender, System.EventArgs e) {
			FormReferralEdit FormREdit=new FormReferralEdit();
			FormREdit.IsNew=false;
			Referrals.Cur=Referrals.List[listRef.SelectedIndex];
			FormREdit.ShowDialog();
			FillRefs();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			RefNumString=textRefNum.Text;
			if(listRef.SelectedIndex==-1)
				ReferringProv=0;
			else
				ReferringProv=Referrals.List[listRef.SelectedIndex].ReferralNum;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		
	}
}
