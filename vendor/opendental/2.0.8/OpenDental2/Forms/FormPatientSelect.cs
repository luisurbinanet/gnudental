/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
//#define TRIALONLY
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


namespace OpenDental{

	public class FormPatientSelect : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		private Patients Patients;
		private System.Windows.Forms.Label label2;
		private OpenDental.TablePatientList tb2;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butAddPt;
		public bool OnlyChangingFam;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textLName;
		private System.Windows.Forms.TextBox textFName;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.TextBox textHmPhone;
		private System.Windows.Forms.Label labelMore;
		private System.Windows.Forms.GroupBox groupAddPt;

		public FormPatientSelect(){
			InitializeComponent();//required first
			tb2.CellClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellClicked);
			tb2.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tb2_CellDoubleClicked);
			Patients=new Patients();
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label2,
				label3,
				label4,
				label5,
				this.butAddPt,
				this.groupAddPt,
				this.groupBox2,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});

		}

		// Clean up any resources being used here.
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if (components != null){
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
			this.textLName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupAddPt = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butAddPt = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.tb2 = new OpenDental.TablePatientList();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textHmPhone = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textFName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelMore = new System.Windows.Forms.Label();
			this.groupAddPt.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textLName
			// 
			this.textLName.Location = new System.Drawing.Point(124, 26);
			this.textLName.Name = "textLName";
			this.textLName.Size = new System.Drawing.Size(90, 20);
			this.textLName.TabIndex = 0;
			this.textLName.Text = "";
			this.textLName.TextChanged += new System.EventHandler(this.textLName_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "Last Name:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupAddPt
			// 
			this.groupAddPt.Controls.Add(this.label2);
			this.groupAddPt.Controls.Add(this.butAddPt);
			this.groupAddPt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupAddPt.Location = new System.Drawing.Point(672, 405);
			this.groupAddPt.Name = "groupAddPt";
			this.groupAddPt.Size = new System.Drawing.Size(212, 116);
			this.groupAddPt.TabIndex = 2;
			this.groupAddPt.TabStop = false;
			this.groupAddPt.Text = "Add New Family:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(188, 40);
			this.label2.TabIndex = 11;
			this.label2.Text = "This creates a new family.   Please make sure they are not already in the system " +
				"first";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butAddPt
			// 
			this.butAddPt.Location = new System.Drawing.Point(68, 24);
			this.butAddPt.Name = "butAddPt";
			this.butAddPt.TabIndex = 0;
			this.butAddPt.Text = "Add";
			this.butAddPt.Click += new System.EventHandler(this.butAddPt_Click);
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(808, 566);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(76, 23);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(808, 602);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(76, 23);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// tb2
			// 
			this.tb2.BackColor = System.Drawing.SystemColors.Window;
			this.tb2.Location = new System.Drawing.Point(8, 8);
			this.tb2.Name = "tb2";
			this.tb2.SelectedIndices = new int[0];
			this.tb2.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tb2.Size = new System.Drawing.Size(627, 314);
			this.tb2.TabIndex = 1;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textAddress);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.textHmPhone);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.textFName);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.textLName);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(667, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(228, 176);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Search by:";
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(124, 128);
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(90, 20);
			this.textAddress.TabIndex = 3;
			this.textAddress.Text = "";
			this.textAddress.TextChanged += new System.EventHandler(this.textAddress_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 132);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(114, 12);
			this.label5.TabIndex = 9;
			this.label5.Text = "Address:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textHmPhone
			// 
			this.textHmPhone.Location = new System.Drawing.Point(124, 94);
			this.textHmPhone.Name = "textHmPhone";
			this.textHmPhone.Size = new System.Drawing.Size(90, 20);
			this.textHmPhone.TabIndex = 2;
			this.textHmPhone.Text = "";
			this.textHmPhone.TextChanged += new System.EventHandler(this.textHmPhone_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 98);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(114, 12);
			this.label4.TabIndex = 7;
			this.label4.Text = "Home Phone:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFName
			// 
			this.textFName.Location = new System.Drawing.Point(124, 60);
			this.textFName.Name = "textFName";
			this.textFName.Size = new System.Drawing.Size(90, 20);
			this.textFName.TabIndex = 1;
			this.textFName.Text = "";
			this.textFName.TextChanged += new System.EventHandler(this.textFName_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(114, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "First Name:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelMore
			// 
			this.labelMore.Location = new System.Drawing.Point(648, 622);
			this.labelMore.Name = "labelMore";
			this.labelMore.Size = new System.Drawing.Size(68, 16);
			this.labelMore.TabIndex = 5;
			this.labelMore.Text = "(more)";
			this.labelMore.Visible = false;
			// 
			// FormPatientSelect
			// 
			this.AcceptButton = this.butOK;
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(904, 646);
			this.Controls.Add(this.labelMore);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.tb2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupAddPt);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPatientSelect";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Patient";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSelectPatient_KeyDown);
			this.Load += new System.EventHandler(this.FormSelectPatient_Load);
			this.groupAddPt.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void FormSelectPatient_Load(object sender, System.EventArgs e){
			if(OnlyChangingFam){
				groupAddPt.Visible=false;
			}
			textLName.Select();
			FillList();
		}

		private void textLName_TextChanged(object sender, System.EventArgs e){
			//if(textLastName.TextLength==1 && !String.Equals(previousLetter,textLastName.Text)){
			//	previousLetter=textLastName.Text;
				//FillList();
			//}
			//ScrollList();	
			FillList();
		}

		private void textFName_TextChanged(object sender, System.EventArgs e) {
			FillList();
		}

		private void textHmPhone_TextChanged(object sender, System.EventArgs e) {
			FillList();
		}

		private void textAddress_TextChanged(object sender, System.EventArgs e) {
			FillList();
		}

		private void FillList(){
			if(Patients.GetPtList(textLName.Text,textFName.Text,textHmPhone.Text,textAddress.Text)){
				labelMore.Visible=true;//make it a pair of forward and back buttons later or scrollable
			}
			else{
				labelMore.Visible=false;
			}
			tb2.ResetRows(Patients.PtList.Length);//optimize later to handle longer lists
			tb2.SetGridColor(Color.Gray);
			for(int i=0;i<Patients.PtList.Length;i++){
				tb2.Cell[0,i]=Patients.PtList[i].LName;
				tb2.Cell[1,i]=Patients.PtList[i].FName;
				tb2.Cell[2,i]=Patients.PtList[i].MiddleI;
				tb2.Cell[3,i]=Patients.PtList[i].Preferred;
				tb2.Cell[4,i]=Patients.PtList[i].Age;
				tb2.Cell[5,i]=Patients.PtList[i].SSN;
				tb2.Cell[6,i]=Patients.PtList[i].HmPhone;
				tb2.Cell[7,i]=Patients.PtList[i].Address;
				tb2.Cell[8,i]=Lan.g(this,Patients.PtList[i].PatStatus.ToString());
			}
			//MessageBox.Show(tb2.Cell.GetLength(2).ToString());
			tb2.SelectedRow=-1;
			tb2.LayoutTables();
		}

		//private void ScrollList(){
			//if(Patients.PtList.Length==0) return;
			//int newRow=0;
			//for(int i=Patients.PtList.Length-1;i>=0;i--){
			//	if(Patients.PtList[i].LName.Length>=textLastName.Text.Length){
			//		if(String.Compare(Patients.PtList[i].LName.Substring(0,textLastName.Text.Length)
			//			,textLastName.Text,true)>=0){
			//			newRow=i;
			//		}
			//check with shorter names
			//	}
			//}
			//SetRow(newRow);
			//going to need a lot of patients in DB to test scrolling
			//if(Patients.PtList
		//}

		//private void SetRow(int row){
			//if(tb2.SelectedRow!=-1)
			//	tb2.ColorRow(tb2.SelectedRow,Color.White);
			//tb2.SelectedRow=row;
			//tb2.ColorRow(row,Color.LightGray);
		//}

		private void tb2_CellClicked(object sender, CellEventArgs e){
			//SetRow(e.Row);
			//textLastName.Select();
			//textLastName.SelectionStart=20;
			//MessageBox.Show(e.Row.ToString());
		}

		private void tb2_CellDoubleClicked(object sender, CellEventArgs e){
			PatSelected();
		}

		private void FormSelectPatient_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			//if(tb2.SelectedRow==-1) return;
			//if (e.KeyCode==Keys.Up && tb2.SelectedRow!=0){
			//	SetRow(tb2.SelectedRow-1);
			//}
			//if (e.KeyCode==Keys.Down && tb2.SelectedRow!=Patients.PtList.Length-1){
			//	SetRow(tb2.SelectedRow+1);
			//}
		}

		/*
		private void textBoxes_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(tb2.SelectedRow==-1) return;
			if (e.KeyCode==Keys.Up && tb2.SelectedRow!=0){
				SetRow(tb2.SelectedRow-1);
			}
			if (e.KeyCode==Keys.Down && tb2.SelectedRow!=Patients.PtList.Length-1){
				SetRow(tb2.SelectedRow+1);
			}
		}

		private void textBoxes_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode==Keys.Up)
				textLastName.SelectionStart=20;
		}*/

		private void butAddPt_Click(object sender, System.EventArgs e){
			#if(TRIALONLY)
				MessageBox.Show("Trial version.  Maximum 30 patients");
				if(Patients.PtList.Length>30){
					MessageBox.Show("Maximum reached");
					return;
				}
			#endif
			Patients.Cur=new Patient();
			//Later: Make it dummy proof by testing for whether patient is in DB already.
			Patients.Cur.PatStatus=PatientStatus.Patient;
			Patients.Cur.RecallInterval=6;
			FormPatientEdit FormPE=new FormPatientEdit();
			FormPE.IsNew=true;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.OK){
				DialogResult=DialogResult.OK;
			}
		}

		private void PatSelected(){
			//if(OnlyChangingFam){
			//	NewFamilyNum=Patients.PtList[tb2.SelectedRow].PatNum;
			//}
			//else{
				//MessageBox.Show("got this far");
			//	Patients.GetFamily(Patients.PtList[tb2.SelectedRow].PatNum);
			//}
			Patients.PatIsLoaded=true;
			Patients.Cur.PatNum=Patients.PtList[tb2.SelectedRow].PatNum;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e){
			if(tb2.SelectedRow!=-1){
				PatSelected();
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			//fix: need to reload the current patient that was loaded before opening this dialog
			//remember it with a variable
			DialogResult=DialogResult.Cancel;
		}

	}
}
