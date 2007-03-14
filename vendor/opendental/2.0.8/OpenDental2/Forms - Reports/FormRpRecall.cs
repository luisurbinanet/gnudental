using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormRpRecall : System.Windows.Forms.Form{
		private System.Windows.Forms.Label labelPatient;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.ComponentModel.Container components = null;
		private FormQuery FormQuery2;
		private string SQLselect;
		private string SQLfrom;
    private string SQLwhere;
    private string SQLstatement;
    private string[] PatFieldsSelected;     
		private ArrayList ALpatSelect;
		private ArrayList ALpatSelect2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listPatientSelect2;
		private System.Windows.Forms.ListBox listPatientSelect; 
    private int[] Apatnums;

		public FormRpRecall(){
			InitializeComponent();
      ALpatSelect=new ArrayList();
			SQLselect="";
			SQLfrom="FROM patient "; 
      SQLwhere="WHERE ";
      Fill(); 
 			Lan.C(this, new System.Windows.Forms.Control[] {
				this.labelPatient,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});  
		}
  
		public FormRpRecall(int[] array){
			InitializeComponent();
      Apatnums=array;
      ALpatSelect=new ArrayList();
			ALpatSelect2=new ArrayList();
			SQLselect="";
			SQLfrom="FROM patient ";
      SQLwhere="WHERE ";
      Fill();
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
			this.labelPatient = new System.Windows.Forms.Label();
			this.listPatientSelect = new System.Windows.Forms.ListBox();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listPatientSelect2 = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// labelPatient
			// 
			this.labelPatient.Location = new System.Drawing.Point(12, 16);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(170, 14);
			this.labelPatient.TabIndex = 6;
			this.labelPatient.Text = "Standard Fields";
			this.labelPatient.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// listPatientSelect
			// 
			this.listPatientSelect.Location = new System.Drawing.Point(12, 30);
			this.listPatientSelect.Name = "listPatientSelect";
			this.listPatientSelect.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listPatientSelect.Size = new System.Drawing.Size(170, 355);
			this.listPatientSelect.TabIndex = 5;
			this.listPatientSelect.SelectedIndexChanged += new System.EventHandler(this.ListPatientSelect_SelectedIndexChanged);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(434, 362);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 8;
			this.butCancel.Text = "Cancel";
			// 
			// butOK
			// 
			this.butOK.Enabled = false;
			this.butOK.Location = new System.Drawing.Point(434, 334);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 7;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(208, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(170, 14);
			this.label1.TabIndex = 10;
			this.label1.Text = "Other Available Fields";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// listPatientSelect2
			// 
			this.listPatientSelect2.Location = new System.Drawing.Point(208, 30);
			this.listPatientSelect2.Name = "listPatientSelect2";
			this.listPatientSelect2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listPatientSelect2.Size = new System.Drawing.Size(170, 355);
			this.listPatientSelect2.TabIndex = 9;
			this.listPatientSelect2.SelectedIndexChanged += new System.EventHandler(this.listPatientSelect2_SelectedIndexChanged);
			// 
			// FormRpRecall
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(538, 408);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listPatientSelect2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.labelPatient);
			this.Controls.Add(this.listPatientSelect);
			this.Name = "FormRpRecall";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Recall Report";
			this.ResumeLayout(false);

		}
		#endregion

		private void Fill(){
			FillALpatSelect();
			FillPatientSelectList();
		}
 
		private void FillALpatSelect(){ 
			ALpatSelect.Add(Lan.g(this,"LName"));
			ALpatSelect.Add(Lan.g(this,"FName")); 
			ALpatSelect.Add(Lan.g(this,"MiddleI"));
      ALpatSelect.Add(Lan.g(this,"Preferred"));
			ALpatSelect.Add(Lan.g(this,"Salutation")); 
      ALpatSelect.Add(Lan.g(this,"Address")); 
      ALpatSelect.Add(Lan.g(this,"Address2"));
			ALpatSelect.Add(Lan.g(this,"City")); 
			ALpatSelect.Add(Lan.g(this,"State"));
			ALpatSelect.Add(Lan.g(this,"Zip"));
			ALpatSelect.Add(Lan.g(this,"HmPhone"));
			ALpatSelect.Add(Lan.g(this,"WkPhone")); 
			ALpatSelect.Add(Lan.g(this,"WirelessPhone")); 
			ALpatSelect.Add(Lan.g(this,"Birthdate"));
      ALpatSelect.Add(Lan.g(this,"RecallStatus"));
      ALpatSelect2.Add(Lan.g(this,"BillingType"));      
      ALpatSelect2.Add(Lan.g(this,"CreditType"));
			ALpatSelect2.Add(Lan.g(this,"SSN"));  
			ALpatSelect2.Add(Lan.g(this,"ChartNumber"));   
			ALpatSelect2.Add(Lan.g(this,"FeeSched"));
			ALpatSelect2.Add(Lan.g(this,"Position")); 
			ALpatSelect2.Add(Lan.g(this,"Gender"));
			ALpatSelect2.Add(Lan.g(this,"PatStatus"));
			ALpatSelect2.Add(Lan.g(this,"PatNum")); 
      ALpatSelect2.Add(Lan.g(this,"Email"));
      ALpatSelect2.Add(Lan.g(this,"EstBalance")); 
      ALpatSelect2.Add(Lan.g(this,"AddrNote")); 
      ALpatSelect2.Add(Lan.g(this,"FamFinUrgNote")); 
      ALpatSelect2.Add(Lan.g(this,"MedUrgNote")); 
			ALpatSelect2.Add(Lan.g(this,"ApptModNote"));
      ALpatSelect2.Add(Lan.g(this,"PriPlanNum"));//Primary Carrier?
      ALpatSelect2.Add(Lan.g(this,"PriRelationship"));// ?
			ALpatSelect2.Add(Lan.g(this,"SecPlanNum"));//Secondary Carrier? 
			ALpatSelect2.Add(Lan.g(this,"SecRelationship"));// ?
      ALpatSelect2.Add(Lan.g(this,"RecallInterval")); 
			ALpatSelect2.Add(Lan.g(this,"StudentStatus"));
      ALpatSelect2.Add(Lan.g(this,"SchoolName")); 
			ALpatSelect2.Add(Lan.g(this,"PriProv")); 
      ALpatSelect2.Add(Lan.g(this,"SecProv"));
			ALpatSelect2.Add(Lan.g(this,"NextAptNum")); 
			ALpatSelect2.Add(Lan.g(this,"Guarantor")); 
			ALpatSelect2.Add(Lan.g(this,"ImageFolder"));
 		}

		private void FillPatientSelectList(){
      for(int i=0;i<ALpatSelect.Count;i++){
			  listPatientSelect.Items.Add(ALpatSelect[i]);
				listPatientSelect.SetSelected(i,true);
			}
			for(int i=0;i<ALpatSelect2.Count;i++){
			  listPatientSelect2.Items.Add(ALpatSelect2[i]);
			}
			SQLselect="";
		}

		private void FillSQLstatement(){
      SQLstatement=SQLselect+SQLfrom+SQLwhere;
		}

    private void CreateSQL(){
      CreateSQLselect();
      CreateSQLfrom();
      CreateSQLwhere();
      FillSQLstatement();
    }

    private void CreateSQLselect(){
      SQLselect="";  
      PatFieldsSelected=new string[listPatientSelect.SelectedItems.Count
				+ listPatientSelect2.SelectedItems.Count]; 
      if(listPatientSelect.SelectedItems.Count==0
				&& listPatientSelect2.SelectedItems.Count==0){
				butOK.Enabled=false;
				return;
			}
			SQLselect="SELECT ";
			listPatientSelect.SelectedItems.CopyTo(PatFieldsSelected,0);
			listPatientSelect2.SelectedItems.CopyTo(PatFieldsSelected,listPatientSelect.SelectedItems.Count);
			for(int i=0;i<PatFieldsSelected.Length;i++){
				if(i>0) SQLselect+=","; 
				SQLselect+="patient."+PatFieldsSelected[i].ToString();  
			}
			butOK.Enabled=true;
		}

    private void CreateSQLfrom(){ 
      SQLfrom=" FROM patient ";
    }

    private void CreateSQLwhere(){  
      SQLwhere="WHERE";       
      for(int i=0;i<Apatnums.Length;i++){
        if(i>0) SQLwhere+=" ||";
        SQLwhere+=" patient.patnum='"+Apatnums[i]+"'";
      }
    }

		private void ListPatientSelect_SelectedIndexChanged(object sender, System.EventArgs e) {
			CreateSQL();
		}

		private void listPatientSelect2_SelectedIndexChanged(object sender, System.EventArgs e) {
			CreateSQL();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Queries.CurReport=new Report();
			Queries.CurReport.Query=SQLstatement;
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=false;
			FormQuery2.SubmitQuery();	
      FormQuery2.textQuery.Text=Queries.CurReport.Query;					
			FormQuery2.ShowDialog();		
      DialogResult=DialogResult.OK; 
		}

		

	}
}
