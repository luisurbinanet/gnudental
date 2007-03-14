using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormInsSpanEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listCovCats;
		private System.Windows.Forms.TextBox textFrom;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.TextBox textTo;
		private System.ComponentModel.Container components = null;
		public bool IsNew;

		public FormInsSpanEdit(){
			InitializeComponent();// Required for Windows Form Designer support
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

		private void InitializeComponent(){
			this.label1 = new System.Windows.Forms.Label();
			this.listCovCats = new System.Windows.Forms.ListBox();
			this.textTo = new System.Windows.Forms.TextBox();
			this.textFrom = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(286, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(134, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Coverage Category";
			// 
			// listCovCats
			// 
			this.listCovCats.Location = new System.Drawing.Point(286, 68);
			this.listCovCats.Name = "listCovCats";
			this.listCovCats.Size = new System.Drawing.Size(120, 199);
			this.listCovCats.TabIndex = 2;
			// 
			// textTo
			// 
			this.textTo.Location = new System.Drawing.Point(150, 70);
			this.textTo.Name = "textTo";
			this.textTo.Size = new System.Drawing.Size(101, 20);
			this.textTo.TabIndex = 1;
			this.textTo.Text = "";
			// 
			// textFrom
			// 
			this.textFrom.Location = new System.Drawing.Point(34, 70);
			this.textFrom.Name = "textFrom";
			this.textFrom.TabIndex = 0;
			this.textFrom.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(150, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "To ADA";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(32, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "From ADA";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(332, 342);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(332, 378);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormInsSpanEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(434, 422);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textFrom);
			this.Controls.Add(this.textTo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listCovCats);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsSpanEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Ins Coverage Span";
			this.Load += new System.EventHandler(this.FormInsSpanEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsSpanEdit_Load(object sender, System.EventArgs e) {
			for(int i=0;i<CovCats.List.Length;i++){
				if(CovCats.List[i].IsHidden)
					listCovCats.Items.Add(CovCats.List[i].Description+"(hidden)");
				else listCovCats.Items.Add(CovCats.List[i].Description);
				if(CovSpans.Cur.CovCatNum==CovCats.List[i].CovCatNum){
					listCovCats.SelectedIndex=i;
				}
			}
			textFrom.Text=CovSpans.Cur.FromCode;
			textTo.Text=CovSpans.Cur.ToCode;

		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//if(ProcCodes.GetProcCode(textFrom.Text).ADACode==null) return;
			bool codeIsValid=true;
			if(textFrom.Text.Length<5 || textTo.Text.Length<5){
				codeIsValid=false;
			}
			else if(textFrom.Text.Substring(0,1)!="D" || textTo.Text.Substring(0,1)!="D"){
				codeIsValid=false;
			}
			else for(int i=1;i<5;i++){
				if(!Char.IsNumber(textFrom.Text,i)
					|| !Char.IsNumber(textTo.Text,i)){
					codeIsValid=false;
					break;
				}
			}
			if(!codeIsValid){
				if(MessageBox.Show(Lan.g(this,"One of the codes is not a standard ADA code.  Use anyway?"),"",MessageBoxButtons.OKCancel)
					!=DialogResult.OK){
				return;
				}
			}
			//test for overlap of spans:
			for(int i=0;i<CovSpans.List.Length;i++){
				if(CovSpans.Cur.CovSpanNum==CovSpans.List[i].CovSpanNum) continue;
				if(String.Compare(CovSpans.List[i].FromCode,textFrom.Text,true) <=0
					&& String.Compare(CovSpans.List[i].ToCode,textFrom.Text,true) >=0){
					MessageBox.Show(Lan.g(this,"Spans not allowed to overlap (From code)"));
					return;
				}
				if(String.Compare(CovSpans.List[i].FromCode,textTo.Text,true) <=0
					&& String.Compare(CovSpans.List[i].ToCode,textTo.Text,true) >=0){
					MessageBox.Show(Lan.g(this,"Spans not allowed to overlap (To code)"));
					return;
				}
				if(String.Compare(CovSpans.List[i].FromCode,textFrom.Text,true) >=0
					&& String.Compare(CovSpans.List[i].ToCode,textTo.Text,true) <=0){
					MessageBox.Show(Lan.g(this,"Spans not allowed to overlap (both codes)"));
					return;
				}
			}
			if(listCovCats.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a coverage category"));
				return;
			}
			CovSpans.Cur.FromCode=textFrom.Text;
			CovSpans.Cur.ToCode=textTo.Text;
			for(int i=0;i<CovCats.List.Length;i++){//can't use CovCats.GetCovCatNum
				if(CovCats.List[i].CovOrder==listCovCats.SelectedIndex){
					CovSpans.Cur.CovCatNum=CovCats.List[i].CovCatNum;
				}
			}
			if(IsNew){
				CovSpans.InsertCur();
			}
			else{
				CovSpans.UpdateCur();
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
