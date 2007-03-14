/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormAdjust : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.Container components = null;// Required designer variable.
		public bool IsNew;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butDelete;
		private OpenDental.ValidDouble textAmount;
		private System.Windows.Forms.ListBox listProvider;
		private System.Windows.Forms.ListBox listTypePos;
		private System.Windows.Forms.ListBox listTypeNeg;
		private ArrayList PosIndex=new ArrayList();
		private ArrayList NegIndex=new ArrayList();
		//private DateTime OriginalDate;
		//private double OriginalAmt;

		public FormAdjust(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				this.label2,
				this.label3,
				this.label4,
				this.label5,
				this.label6,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butDelete,
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
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.textDate = new OpenDental.ValidDate();
			this.label3 = new System.Windows.Forms.Label();
			this.butDelete = new System.Windows.Forms.Button();
			this.textAmount = new OpenDental.ValidDouble();
			this.listProvider = new System.Windows.Forms.ListBox();
			this.listTypePos = new System.Windows.Forms.ListBox();
			this.listTypeNeg = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(62, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(53, 341);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 16);
			this.label4.TabIndex = 3;
			this.label4.Text = "Note";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 60);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Amount";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(322, 14);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Additions";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(166, 338);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(393, 115);
			this.textNote.TabIndex = 5;
			this.textNote.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 10;
			this.label2.Text = "Provider";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(617, 437);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 6;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(617, 475);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(112, 28);
			this.textDate.Name = "textDate";
			this.textDate.TabIndex = 0;
			this.textDate.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(520, 14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 16;
			this.label3.Text = "Subtractions";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butDelete
			// 
			this.butDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDelete.Location = new System.Drawing.Point(41, 476);
			this.butDelete.Name = "butDelete";
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(112, 58);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(80, 20);
			this.textAmount.TabIndex = 1;
			this.textAmount.Text = "";
			// 
			// listProvider
			// 
			this.listProvider.Location = new System.Drawing.Point(112, 90);
			this.listProvider.Name = "listProvider";
			this.listProvider.Size = new System.Drawing.Size(100, 95);
			this.listProvider.TabIndex = 2;
			// 
			// listTypePos
			// 
			this.listTypePos.Location = new System.Drawing.Point(266, 34);
			this.listTypePos.Name = "listTypePos";
			this.listTypePos.Size = new System.Drawing.Size(202, 264);
			this.listTypePos.TabIndex = 3;
			this.listTypePos.SelectedIndexChanged += new System.EventHandler(this.listTypePos_SelectedIndexChanged);
			// 
			// listTypeNeg
			// 
			this.listTypeNeg.Location = new System.Drawing.Point(482, 34);
			this.listTypeNeg.Name = "listTypeNeg";
			this.listTypeNeg.Size = new System.Drawing.Size(206, 264);
			this.listTypeNeg.TabIndex = 4;
			this.listTypeNeg.SelectedIndexChanged += new System.EventHandler(this.listTypeNeg_SelectedIndexChanged);
			// 
			// FormAdjust
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(731, 528);
			this.Controls.Add(this.listTypeNeg);
			this.Controls.Add(this.listTypePos);
			this.Controls.Add(this.listProvider);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Name = "FormAdjust";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Adjustment";
			this.Load += new System.EventHandler(this.FormAdjust_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAdjust_Load(object sender, System.EventArgs e) {
			if(IsNew){
				Adjustments.Cur=new Adjustment();
				Adjustments.Cur.AdjDate=DateTime.Now;
				Adjustments.Cur.ProvNum=Patients.Cur.PriProv;
				Adjustments.Cur.PatNum=Patients.Cur.PatNum;
			}
			else{				
				if(!UserPermissions.CheckUserPassword("Adjustment Edit",Adjustments.Cur.AdjDate)){
					//MessageBox.Show(Lan.g(this,"You only have permission to view the Adjustment. No changes will be saved."));
					butOK.Enabled=false;
					butDelete.Enabled=false;
				}					
			}
			//OriginalDate=Adjustments.Cur.AdjDate;
			//OriginalAmt=Adjustments.Cur.AdjAmt;
			this.textDate.Text=Adjustments.Cur.AdjDate.ToString("d");
			if(Defs.GetValue(DefCat.AdjTypes,Adjustments.Cur.AdjType)=="+"){//pos
				textAmount.Text=Adjustments.Cur.AdjAmt.ToString("F");
			}
			else{//neg
				textAmount.Text=(-Adjustments.Cur.AdjAmt).ToString("F");//shows without the neg sign
			}
			for(int i=0;i<Providers.List.Length;i++){
				this.listProvider.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==Adjustments.Cur.ProvNum)
					listProvider.SelectedIndex=i;
			}				
			for(int i=0;i<Defs.Short[1].Length;i++){//temp.AdjType
				if(Defs.Short[1][i].ItemValue=="+"){
					PosIndex.Add(i);
					listTypePos.Items.Add(Defs.Short[1][i].ItemName);
					if(Defs.Short[1][i].DefNum==Adjustments.Cur.AdjType)
						listTypePos.SelectedIndex=PosIndex.Count-1;
				}
				else if(Defs.Short[1][i].ItemValue=="-"){
					NegIndex.Add(i);
					listTypeNeg.Items.Add(Defs.Short[1][i].ItemName);
					if(Defs.Short[1][i].DefNum==Adjustments.Cur.AdjType)
						listTypeNeg.SelectedIndex=NegIndex.Count-1;
				}
			}
			//this.listProvNum.SelectedIndex=(int)temp.ProvNum;
			this.textNote.Text=Adjustments.Cur.AdjNote;
		}

		private void listTypePos_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(listTypePos.SelectedIndex!=-1) listTypeNeg.SelectedIndex=-1;
		}

		private void listTypeNeg_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(listTypeNeg.SelectedIndex!=-1)	listTypePos.SelectedIndex=-1;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDate.errorProvider1.GetError(textDate)!=""
				|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			if(textAmount.Text==""){
				MessageBox.Show(Lan.g(this,"Please enter an amount."));	
				return;
			}
			Adjustments.Cur.AdjDate=PIn.PDate(textDate.Text);
			if(listProvider.SelectedIndex==-1)
				Adjustments.Cur.ProvNum=Patients.Cur.PriProv;
			else
				Adjustments.Cur.ProvNum=Providers.List[this.listProvider.SelectedIndex].ProvNum;
			if(listTypePos.SelectedIndex!=-1){
				Adjustments.Cur.AdjType
					=Defs.Short[(int)DefCat.AdjTypes][(int)PosIndex[listTypePos.SelectedIndex]].DefNum;
			}
			if(listTypeNeg.SelectedIndex!=-1){
				Adjustments.Cur.AdjType
					=Defs.Short[(int)DefCat.AdjTypes][(int)NegIndex[listTypeNeg.SelectedIndex]].DefNum;
			}
			if(listTypeNeg.SelectedIndex==-1 && listTypePos.SelectedIndex==-1){
				Adjustments.Cur.AdjType=Defs.Short[(int)DefCat.AdjTypes][0].DefNum;
			}
			if(Defs.GetValue(DefCat.AdjTypes,Adjustments.Cur.AdjType)=="+"){//pos
				Adjustments.Cur.AdjAmt=PIn.PDouble(textAmount.Text);
			}
			else{//neg
				Adjustments.Cur.AdjAmt=-PIn.PDouble(textAmount.Text);
			}
			Adjustments.Cur.AdjNote=textNote.Text;
			if(IsNew){
				Adjustments.InsertCur();
			}
			else{
				Adjustments.UpdateCur();
		  	SecurityLogs.MakeLogEntry("Adjustment Edit",Adjustments.cmd.CommandText);
			}
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				SecurityLogs.MakeLogEntry("Adjustment Edit","Delete. patNum: "+Adjustments.Cur.PatNum.ToString());
				Adjustments.DeleteCur();
				DialogResult=DialogResult.OK;
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}

	public struct AdjustmentItem{
		public string ItemText;
		public int ItemIndex;
	}

}
