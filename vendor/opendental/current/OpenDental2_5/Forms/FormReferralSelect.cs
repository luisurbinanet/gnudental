using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormReferralSelect : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private OpenDental.TableRefSelect tbRefSelect;
		private System.Windows.Forms.CheckBox checkHidden;
		private System.ComponentModel.Container components = null;
		public bool ViewOnly;
		private OpenDental.XPButton butEdit;
		private OpenDental.XPButton butDelete;
		private OpenDental.XPButton butAdd;//disables double click to choose referral. Hides some buttons.
    private ArrayList AList;

		public FormReferralSelect(){
			InitializeComponent();
			tbRefSelect.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbRefSelect_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				checkHidden,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butAdd,
				butDelete,
				butEdit,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormReferralSelect));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.tbRefSelect = new OpenDental.TableRefSelect();
			this.checkHidden = new System.Windows.Forms.CheckBox();
			this.butEdit = new OpenDental.XPButton();
			this.butDelete = new OpenDental.XPButton();
			this.butAdd = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			this.butCancel.Location = new System.Drawing.Point(864, 646);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76, 26);
			this.butCancel.TabIndex = 6;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(864, 614);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76, 26);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tbRefSelect
			// 
			this.tbRefSelect.BackColor = System.Drawing.SystemColors.Window;
			this.tbRefSelect.Location = new System.Drawing.Point(8, 6);
			this.tbRefSelect.Name = "tbRefSelect";
			this.tbRefSelect.SelectedIndices = new int[0];
			this.tbRefSelect.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbRefSelect.Size = new System.Drawing.Size(829, 602);
			this.tbRefSelect.TabIndex = 7;
			// 
			// checkHidden
			// 
			this.checkHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidden.Location = new System.Drawing.Point(844, 22);
			this.checkHidden.Name = "checkHidden";
			this.checkHidden.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkHidden.TabIndex = 11;
			this.checkHidden.Text = "Show Hidden  ";
			this.checkHidden.Click += new System.EventHandler(this.checkHidden_Click);
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEdit.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butEdit.Image = ((System.Drawing.Image)(resources.GetObject("butEdit.Image")));
			this.butEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEdit.Location = new System.Drawing.Point(864, 496);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(75, 26);
			this.butEdit.TabIndex = 14;
			this.butEdit.Text = "Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(864, 464);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75, 26);
			this.butDelete.TabIndex = 13;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(864, 430);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75, 26);
			this.butAdd.TabIndex = 12;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormReferralSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(962, 696);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.checkHidden);
			this.Controls.Add(this.tbRefSelect);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReferralSelect";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Referrals";
			this.Load += new System.EventHandler(this.FormReferralSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormReferralSelect_Load(object sender, System.EventArgs e) {
      if(ViewOnly){
        butEdit.Visible=false;
      }
			FillTable();
		}		

    private void FillTable(){
      Referrals.Refresh();
      AList=new ArrayList();
      if(!checkHidden.Checked){
        for(int i=0;i<Referrals.List.Length;i++){
          if(!Referrals.List[i].IsHidden){
            AList.Add(Referrals.List[i]);
          }
        } 
      }
      else{
        for(int i=0;i<Referrals.List.Length;i++){
            AList.Add(Referrals.List[i]);
        } 
      }          
			tbRefSelect.ResetRows(AList.Count);
			tbRefSelect.SetGridColor(Color.Gray);
			tbRefSelect.SetBackGColor(Color.White);      
			for(int i=0;i<AList.Count;i++){
				tbRefSelect.Cell[0,i]=((Referral)(AList[i])).LName;
        tbRefSelect.Cell[1,i]=((Referral)(AList[i])).FName;
        if(((Referral)(AList[i])).MName!=""){
          tbRefSelect.Cell[2,i]=((Referral)(AList[i])).MName.Substring(0,1).ToUpper();
        } 
				tbRefSelect.Cell[3,i]=((Referral)(AList[i])).Title;
        if(((Referral)(AList[i])).PatNum==0 && !((Referral)(AList[i])).NotPerson){
          tbRefSelect.Cell[4,i]
						=Lan.g("enumDentalSpecialty",((DentalSpecialty)(((Referral)(AList[i])).Specialty)).ToString());
        }
				if(((Referral)(AList[i])).PatNum>0)
					tbRefSelect.Cell[5,i]="X";
				else
					tbRefSelect.Cell[5,i]="";
        tbRefSelect.Cell[6,i]=((Referral)(AList[i])).Note;
				if(((Referral)(AList[i])).IsHidden)
					tbRefSelect.SetTextColorRow(i,SystemColors.GrayText);
			}
			tbRefSelect.LayoutTables();  
      //if(tbRefSelect.SelectedRow!=-1){ 
      //  tbRefSelect.ColorRow(tbRefSelect.SelectedRow,Color.Silver);
      //} 
    }

    private void tbRefSelect_CellDoubleClicked(object sender, CellEventArgs e){
			Referrals.Cur=(Referral)AList[tbRefSelect.SelectedRow]; 
      if(ViewOnly){
        FormReferralEdit FormRE2=new FormReferralEdit();
				if(Referrals.Cur.PatNum > 0){
					FormRE2.IsPatient=true;
				}
        FormRE2.ShowDialog();
        FillTable();         
      }
      else{
				DialogResult=DialogResult.OK;
      }
    }

		private void butOK_Click(object sender, System.EventArgs e) {
      if(ViewOnly){
        DialogResult=DialogResult.OK;
      }
      else{  
        if(tbRefSelect.SelectedRow==-1){
          return;
        } 
		    Referrals.Cur=(Referral)AList[tbRefSelect.SelectedRow]; 
        DialogResult=DialogResult.OK;
      }
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
		  int tempPatNum=Patients.Cur.PatNum;
      FormReferralEdit FormRE2=new FormReferralEdit();
			FormRE2.IsNew=true; 
			if (MessageBox.Show(Lan.g(this,"Is the referral source an existing patient?"),"",MessageBoxButtons.YesNo)==DialogResult.Yes){
				FormPatientSelect FormPS=new FormPatientSelect();
				FormPS.OnlyChangingFam=true;        
				FormPS.ShowDialog();
				if(FormPS.DialogResult!=DialogResult.OK){
					Patients.Cur.PatNum=tempPatNum; 
					return;  
				}
				FormRE2.IsPatient=true;
				for(int i=0;i<Referrals.List.Length;i++){
					if(Referrals.List[i].PatNum==Patients.Cur.PatNum){
						//MessageBox.Show("Patient is already Referral, Please select from List");
						Referrals.Cur=Referrals.List[i];
						FormRE2.IsNew=false;
						break;
					}
				}
			}
      FormRE2.ShowDialog();
      tbRefSelect.SelectedRow=-1;
      FillTable();
			Patients.GetFamily(tempPatNum);
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
      if(tbRefSelect.SelectedRow==-1){ 
        return; 
      }  
 		  Referrals.Cur=(Referral)AList[tbRefSelect.SelectedRow];
			if(RefAttaches.IsReferralAttached(Referrals.Cur.ReferralNum)){
				MessageBox.Show("Cannot delete Referral because it is attached to patients");
				return;
			}
      if (MessageBox.Show(Lan.g(this,"Delete Referral?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
        return;   
      }
      Referrals.DeleteCur();
      tbRefSelect.SelectedRow=-1;
      FillTable();
    }

		private void butEdit_Click(object sender, System.EventArgs e) {
      if(tbRefSelect.SelectedRow==-1){
        return;  
      }
		  Referrals.Cur=(Referral)AList[tbRefSelect.SelectedRow];
  		FormReferralEdit FormRE2=new FormReferralEdit();
			if(Referrals.Cur.PatNum > 0){
				FormRE2.IsPatient=true;
			}
      FormRE2.ShowDialog();
      FillTable();
		}

		private void checkHidden_Click(object sender, System.EventArgs e) {
      tbRefSelect.SelectedRow=-1; 
		  FillTable();
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
		
		}

	}
}
