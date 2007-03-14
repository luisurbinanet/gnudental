using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormBilling : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private OpenDental.ContrAccount contrAccount1;
		private System.Windows.Forms.Button butAll;
		private System.Windows.Forms.Button butNone;
		private OpenDental.TableBilling tbBill;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butPrint;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormBilling(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				butAll,
				butNone
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butPrint,
				butCancel,
			});
		}

		///<summary></summary>
		protected override void Dispose(bool disposing){
			if(disposing){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.butCancel = new System.Windows.Forms.Button();
			this.butPrint = new System.Windows.Forms.Button();
			this.contrAccount1 = new OpenDental.ContrAccount();
			this.butNone = new System.Windows.Forms.Button();
			this.butAll = new System.Windows.Forms.Button();
			this.tbBill = new OpenDental.TableBilling();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(576, 662);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "&Cancel";
			// 
			// butPrint
			// 
			this.butPrint.BackColor = System.Drawing.SystemColors.Control;
			this.butPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butPrint.Location = new System.Drawing.Point(576, 628);
			this.butPrint.Name = "butPrint";
			this.butPrint.TabIndex = 0;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// contrAccount1
			// 
			this.contrAccount1.Location = new System.Drawing.Point(-56, 102);
			this.contrAccount1.Name = "contrAccount1";
			this.contrAccount1.Size = new System.Drawing.Size(916, 494);
			this.contrAccount1.TabIndex = 20;
			this.contrAccount1.Visible = false;
			// 
			// butNone
			// 
			this.butNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butNone.Location = new System.Drawing.Point(142, 662);
			this.butNone.Name = "butNone";
			this.butNone.TabIndex = 23;
			this.butNone.Text = "&None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// butAll
			// 
			this.butAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAll.Location = new System.Drawing.Point(42, 662);
			this.butAll.Name = "butAll";
			this.butAll.TabIndex = 22;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// tbBill
			// 
			this.tbBill.BackColor = System.Drawing.SystemColors.Window;
			this.tbBill.Location = new System.Drawing.Point(42, 46);
			this.tbBill.Name = "tbBill";
			this.tbBill.ScrollValue = 113;
			this.tbBill.SelectedIndices = new int[0];
			this.tbBill.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbBill.Size = new System.Drawing.Size(499, 606);
			this.tbBill.TabIndex = 24;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(42, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(360, 16);
			this.label2.TabIndex = 25;
			this.label2.Text = "(hint: hold down the control key when making selections)";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(44, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(500, 14);
			this.label1.TabIndex = 26;
			this.label1.Text = "Unhighlight any bills you don\'t want to print.";
			// 
			// FormBilling
			// 
			this.AcceptButton = this.butPrint;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(672, 692);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbBill);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.contrAccount1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butPrint);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBilling";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Billing";
			this.Load += new System.EventHandler(this.FormBilling_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormBilling_Load(object sender, System.EventArgs e) {
			//contrAccount1.checkShowAll.Checked=false;
			//textDate.Text=Ledgers.GetClosestFirst(DateTime.Today).ToShortDateString();
			//Patients.GetAgingList();
			FillTable();
			tbBill.SetSelected(true);	
		}

		private void FillTable(){
			tbBill.ResetRows(Patients.AgingList.Length);
			tbBill.SetGridColor(Color.Gray);
			tbBill.SetBackGColor(Color.White);  
			for(int i=0;i<Patients.AgingList.Length;i++){
				tbBill.Cell[0,i]=Patients.AgingList[i].PatName;
				tbBill.Cell[1,i]=Patients.AgingList[i].BalTotal.ToString("F");
				tbBill.Cell[2,i]=Patients.AgingList[i].InsEst.ToString("F");
				tbBill.Cell[3,i]=Patients.AgingList[i].AmountDue.ToString("F");
			}
			tbBill.LayoutTables();
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			tbBill.SetSelected(true);
		}

		private void butNone_Click(object sender, System.EventArgs e) {	
			tbBill.SetSelected(false);
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			if(tbBill.SelectedIndices.Length==0){
				MessageBox.Show(Lan.g(this,"Please select items first."));
				return;
			}
			for(int i=0;i<tbBill.SelectedIndices.Length;i++){
				Patient PatCur=new Patient();
				PatCur.PatNum=Patients.AgingList[tbBill.SelectedIndices[i]].PatNum;
				Patients.Cur=PatCur;
				Patients.PatIsLoaded=true;
				contrAccount1.LoadAndPrint();
			}
			MessageBox.Show(Lan.g(this,"Printing Statements Complete"));
			Patients.PatIsLoaded=false;
			DialogResult=DialogResult.OK;
		}

		

	}
}
