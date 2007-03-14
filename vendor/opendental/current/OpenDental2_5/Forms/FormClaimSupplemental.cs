using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormClaimSupplemental : System.Windows.Forms.Form{
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
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox textAccidentST;
		private OpenDental.ValidDate textAccidentDate;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.ListBox listPlaceService;
		private System.Windows.Forms.ListBox listEmployRelated;
		private System.Windows.Forms.ListBox listAccident;
		private System.Windows.Forms.Label label10;
		//public string RefNumString;
	
		public FormClaimSupplemental(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label2,
				this.label3,
				this.butNone,
				this.label10,
				this.label25,
				this.label24,
				this.label23,
				this.groupBox2
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
			this.label25 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label24 = new System.Windows.Forms.Label();
			this.textAccidentST = new System.Windows.Forms.TextBox();
			this.textAccidentDate = new OpenDental.ValidDate();
			this.label23 = new System.Windows.Forms.Label();
			this.listAccident = new System.Windows.Forms.ListBox();
			this.label10 = new System.Windows.Forms.Label();
			this.listPlaceService = new System.Windows.Forms.ListBox();
			this.listEmployRelated = new System.Windows.Forms.ListBox();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(641, 382);
			this.butCancel.Name = "butCancel";
			this.butCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(641, 342);
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
			this.label2.Location = new System.Drawing.Point(257, 83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(123, 14);
			this.label2.TabIndex = 7;
			this.label2.Text = "Referral Number";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textRefNum
			// 
			this.textRefNum.Location = new System.Drawing.Point(381, 81);
			this.textRefNum.Name = "textRefNum";
			this.textRefNum.Size = new System.Drawing.Size(150, 20);
			this.textRefNum.TabIndex = 1;
			this.textRefNum.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(276, 34);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(244, 36);
			this.label3.TabIndex = 8;
			this.label3.Text = "Only enter referring provider and referral number if required by your insurance c" +
				"arrier.";
			// 
			// butNone
			// 
			this.butNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butNone.Location = new System.Drawing.Point(34, 370);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(68, 23);
			this.butNone.TabIndex = 9;
			this.butNone.Text = "None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// butAdd
			// 
			this.butAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAdd.Location = new System.Drawing.Point(114, 370);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(70, 23);
			this.butAdd.TabIndex = 10;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(210, 221);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(170, 17);
			this.label25.TabIndex = 101;
			this.label25.Text = "Employment Related";
			this.label25.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label24);
			this.groupBox2.Controls.Add(this.textAccidentST);
			this.groupBox2.Controls.Add(this.textAccidentDate);
			this.groupBox2.Controls.Add(this.label23);
			this.groupBox2.Controls.Add(this.listAccident);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(373, 270);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(209, 85);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Accident Related";
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(90, 47);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(32, 17);
			this.label24.TabIndex = 8;
			this.label24.Text = "State";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAccidentST
			// 
			this.textAccidentST.Location = new System.Drawing.Point(124, 43);
			this.textAccidentST.Name = "textAccidentST";
			this.textAccidentST.Size = new System.Drawing.Size(30, 20);
			this.textAccidentST.TabIndex = 2;
			this.textAccidentST.Text = "";
			// 
			// textAccidentDate
			// 
			this.textAccidentDate.Location = new System.Drawing.Point(124, 20);
			this.textAccidentDate.Name = "textAccidentDate";
			this.textAccidentDate.Size = new System.Drawing.Size(75, 20);
			this.textAccidentDate.TabIndex = 1;
			this.textAccidentDate.Text = "";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(89, 24);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(32, 17);
			this.label23.TabIndex = 5;
			this.label23.Text = "Date";
			this.label23.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listAccident
			// 
			this.listAccident.Location = new System.Drawing.Point(8, 20);
			this.listAccident.Name = "listAccident";
			this.listAccident.Size = new System.Drawing.Size(77, 56);
			this.listAccident.TabIndex = 104;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(237, 114);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(141, 17);
			this.label10.TabIndex = 97;
			this.label10.Text = "Place of Service";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listPlaceService
			// 
			this.listPlaceService.Location = new System.Drawing.Point(381, 112);
			this.listPlaceService.Name = "listPlaceService";
			this.listPlaceService.Size = new System.Drawing.Size(120, 95);
			this.listPlaceService.TabIndex = 102;
			// 
			// listEmployRelated
			// 
			this.listEmployRelated.Location = new System.Drawing.Point(381, 220);
			this.listEmployRelated.Name = "listEmployRelated";
			this.listEmployRelated.Size = new System.Drawing.Size(120, 43);
			this.listEmployRelated.TabIndex = 103;
			// 
			// FormClaimSupplemental
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(752, 443);
			this.Controls.Add(this.listEmployRelated);
			this.Controls.Add(this.listPlaceService);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textRefNum);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listRef);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.groupBox2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimSupplemental";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Supplemental Info";
			this.Load += new System.EventHandler(this.FormClaimSupplemental_Load);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimSupplemental_Load(object sender, System.EventArgs e) {
			textRefNum.Text=Claims.Cur.RefNumString;
			string[] enumPlaceOfService=Enum.GetNames(typeof(PlaceOfService));
			for(int i=0;i<enumPlaceOfService.Length;i++){;
				listPlaceService.Items.Add(Lan.g("enumPlaceOfService",enumPlaceOfService[i]));
			}
			listPlaceService.SelectedIndex=(int)Claims.Cur.PlaceService;
			string[] enumYN=Enum.GetNames(typeof(YN));
			for(int i=0;i<enumYN.Length;i++){;
				listEmployRelated.Items.Add(Lan.g("enumYN",enumYN[i]));
			}
			listEmployRelated.SelectedIndex=(int)Claims.Cur.EmployRelated;
			listAccident.Items.Add(Lan.g(this,"No"));
			listAccident.Items.Add(Lan.g(this,"Auto"));
			listAccident.Items.Add(Lan.g(this,"Employment"));
			listAccident.Items.Add(Lan.g(this,"Other"));
			//MessageBox.Show(Claims.Cur.AccidentRelated);
			switch(Claims.Cur.AccidentRelated){
				case "":
					listAccident.SelectedIndex=0;
					break;
				case "A":
					listAccident.SelectedIndex=1;
					break;
				case "E":
					listAccident.SelectedIndex=2;
					break;
				case "O":
					listAccident.SelectedIndex=3;
					break;
			}
			if(Claims.Cur.AccidentDate.Year<1880){
				textAccidentDate.Text="";
			}
			else{
				textAccidentDate.Text=Claims.Cur.AccidentDate.ToShortDateString();
			}
			textAccidentST.Text=Claims.Cur.AccidentST;
			FillRefs();
		}

		private void FillRefs(){
			Referrals.Refresh();
			listRef.Items.Clear();
			for(int i=0;i<Referrals.List.Length;i++){
				listRef.Items.Add(Referrals.List[i].LName+", "+Referrals.List[i].FName);
				if(Claims.Cur.ReferringProv==Referrals.List[i].ReferralNum){
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
			if(  textAccidentDate.errorProvider1.GetError(textAccidentDate)!=""
				//|| textDateRec.errorProvider1.GetError(textDateRec)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			Claims.Cur.RefNumString=textRefNum.Text;
			if(listRef.SelectedIndex==-1)
				Claims.Cur.ReferringProv=0;
			else
				Claims.Cur.ReferringProv=Referrals.List[listRef.SelectedIndex].ReferralNum;
			Claims.Cur.PlaceService=(PlaceOfService)listPlaceService.SelectedIndex;
			Claims.Cur.EmployRelated=(YN)listEmployRelated.SelectedIndex;
			switch(listAccident.SelectedIndex){
				case 0:
					Claims.Cur.AccidentRelated="";
					break;
				case 1:
					Claims.Cur.AccidentRelated="A";
					break;
				case 2:
					Claims.Cur.AccidentRelated="E";
					break;
				case 3:
					Claims.Cur.AccidentRelated="O";
					break;
			}
			Claims.Cur.AccidentDate=PIn.PDate(textAccidentDate.Text);
			Claims.Cur.AccidentST=textAccidentST.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		
	}
}
