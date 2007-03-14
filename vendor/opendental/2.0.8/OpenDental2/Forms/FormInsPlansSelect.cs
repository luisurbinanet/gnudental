using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormInsPlansSelect : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listRelat;
		private OpenDental.TableInsPlans tbPlans;
		public Relat PatRelat;

		public FormInsPlansSelect(){
			InitializeComponent();
			tbPlans.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbPlans_CellDoubleClicked);
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				this,
				label1
			});
		}

		protected override void Dispose(bool disposing){
			if(disposing){
				if(components!=null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.tbPlans = new OpenDental.TableInsPlans();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.listRelat = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// tbPlans
			// 
			this.tbPlans.BackColor = System.Drawing.SystemColors.Window;
			this.tbPlans.Location = new System.Drawing.Point(20, 54);
			this.tbPlans.Name = "tbPlans";
			this.tbPlans.SelectedIndices = new int[0];
			this.tbPlans.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbPlans.Size = new System.Drawing.Size(459, 356);
			this.tbPlans.TabIndex = 0;
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(620, 452);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76, 23);
			this.butCancel.TabIndex = 6;
			this.butCancel.Text = "Cancel";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(620, 416);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76, 23);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(22, 14);
			this.label1.Name = "label1";
			this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label1.Size = new System.Drawing.Size(456, 26);
			this.label1.TabIndex = 7;
			this.label1.Text = "Please select the insurance plan to send the preauthorization to.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(514, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(206, 20);
			this.label2.TabIndex = 8;
			this.label2.Text = "Relationship to Subscriber";
			// 
			// listRelat
			// 
			this.listRelat.Location = new System.Drawing.Point(514, 54);
			this.listRelat.Name = "listRelat";
			this.listRelat.Size = new System.Drawing.Size(180, 264);
			this.listRelat.TabIndex = 9;
			// 
			// FormInsPlansSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(724, 498);
			this.Controls.Add(this.listRelat);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.tbPlans);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsPlansSelect";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Insurance Plan";
			this.Load += new System.EventHandler(this.FormInsPlansSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsPlansSelect_Load(object sender, System.EventArgs e) {
      InsPlans.Refresh();
			FillPlanData();
    }

		private void FillPlanData(){
			tbPlans.ResetRows(InsPlans.List.Length);
			tbPlans.SetGridColor(Color.Gray);
			tbPlans.SetBackGColor(Color.White);
			for(int i=0;i<InsPlans.List.Length;i++){
				tbPlans.Cell[0,i]=(i+1).ToString();
				tbPlans.Cell[1,i]=Patients.GetNameInFamLF(InsPlans.List[i].Subscriber);
				tbPlans.Cell[2,i]=InsPlans.List[i].Carrier;
				if(InsPlans.List[i].DateEffective.Year < 1870)
					tbPlans.Cell[3,i]="";
				else
					tbPlans.Cell[3,i]=InsPlans.List[i].DateEffective.ToString("d");
				if(InsPlans.List[i].DateTerm.Year < 1870)
					tbPlans.Cell[4,i]="";
				else
					tbPlans.Cell[4,i]=InsPlans.List[i].DateTerm.ToString("d");
			}
			tbPlans.SelectedRow=-1;
			tbPlans.LayoutTables();
			for(int i=0;i<Enum.GetNames(typeof(Relat)).Length;i++){
				listRelat.Items.Add(Lan.g("enumRelat",Enum.GetNames(typeof(Relat))[i]));
			}
		}

    private void tbPlans_CellDoubleClicked(object sender, CellEventArgs e){
			InsPlans.Cur=InsPlans.List[e.Row];
      DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(tbPlans.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select a plan first."));
				return;
			}
			if(listRelat.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a relationship first."));
				return;
			}
			InsPlans.Cur=InsPlans.List[tbPlans.SelectedRow];
			PatRelat=(Relat)listRelat.SelectedIndex;
      DialogResult=DialogResult.OK;		
		}
	}
}