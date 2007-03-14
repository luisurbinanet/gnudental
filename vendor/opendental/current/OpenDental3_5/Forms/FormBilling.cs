using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormBilling : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.ContrAccount contrAccount1;
		private OpenDental.UI.Button butAll;
		private OpenDental.UI.Button butNone;
		private OpenDental.TableBilling tbBill;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butPrint;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label3;
		///<summary>Set this list externally before openning the billing window.</summary>
		public PatAging[] AgingList;

		///<summary></summary>
		public FormBilling(){
			InitializeComponent();
			Lan.F(this);
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
			this.butCancel = new OpenDental.UI.Button();
			this.butPrint = new OpenDental.UI.Button();
			this.contrAccount1 = new OpenDental.ContrAccount();
			this.butNone = new OpenDental.UI.Button();
			this.butAll = new OpenDental.UI.Button();
			this.tbBill = new OpenDental.TableBilling();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(586, 658);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 25);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "&Cancel";
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BackColor = System.Drawing.SystemColors.Control;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.Location = new System.Drawing.Point(586, 624);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75, 25);
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
			this.butNone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNone.Autosize = true;
			this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNone.Location = new System.Drawing.Point(142, 662);
			this.butNone.Name = "butNone";
			this.butNone.Size = new System.Drawing.Size(75, 25);
			this.butNone.TabIndex = 23;
			this.butNone.Text = "&None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.Location = new System.Drawing.Point(42, 662);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(75, 25);
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
			this.label2.Size = new System.Drawing.Size(506, 16);
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
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(568, 518);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(102, 102);
			this.label3.TabIndex = 27;
			this.label3.Text = "This will immediately print all selected bills";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormBilling
			// 
			this.AcceptButton = this.butPrint;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(672, 692);
			this.Controls.Add(this.label3);
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
			tbBill.ResetRows(AgingList.Length);
			tbBill.SetGridColor(Color.Gray);
			tbBill.SetBackGColor(Color.White);  
			for(int i=0;i<AgingList.Length;i++){
				tbBill.Cell[0,i]=AgingList[i].PatName;
				tbBill.Cell[1,i]=AgingList[i].BalTotal.ToString("F");
				tbBill.Cell[2,i]=AgingList[i].InsEst.ToString("F");
				tbBill.Cell[3,i]=AgingList[i].AmountDue.ToString("F");
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
			int[] guarNums=new int[tbBill.SelectedIndices.Length];
			for(int i=0;i<tbBill.SelectedIndices.Length;i++){
				guarNums[i]=AgingList[tbBill.SelectedIndices[i]].PatNum;
			}
			FormRpStatement FormS=new FormRpStatement();
			FormS.LoadAndPrint(guarNums);
			#if DEBUG
				FormS.ShowDialog();
			#endif
			MessageBox.Show(Lan.g(this,"Printing Statements Complete"));
			DialogResult=DialogResult.OK;
		}

		

	}
}
