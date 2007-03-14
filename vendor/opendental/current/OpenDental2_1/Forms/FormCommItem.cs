/*=============================================================================================================
FreeDental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormCommItem : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.ComponentModel.Container components = null;// Required designer variable.
		public bool IsNew;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private OpenDental.ValidDate textDate;
		private System.Windows.Forms.Button butDelete;
		//private ArrayList PosIndex=new ArrayList();
		private System.Windows.Forms.ListBox listType;
		//private ArrayList NegIndex=new ArrayList();
		//private DateTime OriginalDate;
		//private double OriginalAmt;

		public FormCommItem(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
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
			this.label6 = new System.Windows.Forms.Label();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.textDate = new OpenDental.ValidDate();
			this.butDelete = new System.Windows.Forms.Button();
			this.listType = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(265, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Type";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(390, 184);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 6;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(390, 222);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(88, 31);
			this.textDate.Name = "textDate";
			this.textDate.TabIndex = 0;
			this.textDate.Text = "";
			// 
			// butDelete
			// 
			this.butDelete.Location = new System.Drawing.Point(64, 224);
			this.butDelete.Name = "butDelete";
			this.butDelete.TabIndex = 17;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// listType
			// 
			this.listType.Items.AddRange(new object[] {
																									"StatementSent"});
			this.listType.Location = new System.Drawing.Point(266, 34);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(200, 82);
			this.listType.TabIndex = 3;
			// 
			// FormCommItem
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(507, 279);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label1);
			this.Name = "FormCommItem";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Communication Item";
			this.Load += new System.EventHandler(this.FormCommItem_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormCommItem_Load(object sender, System.EventArgs e) {
			if(IsNew){
				Commlogs.Cur=new Commlog();
				Commlogs.Cur.CommDate=DateTime.Today;
				Commlogs.Cur.CommType=1;
				Commlogs.Cur.PatNum=Patients.Cur.PatNum;
			}
			/*else{				
				if(!UserPermissions.CheckUserPassword("Adjustment Edit",Adjustments.Cur.AdjDate)){
					//MessageBox.Show(Lan.g(this,"You only have permission to view the Adjustment. No changes will be saved."));
					butOK.Enabled=false;
					butDelete.Enabled=false;
				}					
			}*/
			textDate.Text=Commlogs.Cur.CommDate.ToShortDateString();
			listType.SelectedIndex=0;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDate.errorProvider1.GetError(textDate)!=""
				//|| textAmount.errorProvider1.GetError(textAmount)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			Commlogs.Cur.CommDate=PIn.PDate(textDate.Text);
			if(IsNew){
				Commlogs.InsertCur();
			}
			else{
				Commlogs.UpdateCur();
		  	//SecurityLogs.MakeLogEntry("Adjustment Edit",Adjustments.cmd.CommandText);
			}
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				//SecurityLogs.MakeLogEntry("Adjustment Edit","Delete. patNum: "+Adjustments.Cur.PatNum.ToString());
				Commlogs.DeleteCur();
				DialogResult=DialogResult.OK;
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}

}
