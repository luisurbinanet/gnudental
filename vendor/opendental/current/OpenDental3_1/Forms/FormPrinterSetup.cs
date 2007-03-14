using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormPrinterSetup : System.Windows.Forms.Form{
		private System.Windows.Forms.ListBox listPrinters;
		private OpenDental.UI.Button butDefault;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormPrinterSetup(){
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
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
			this.listPrinters = new System.Windows.Forms.ListBox();
			this.butDefault = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// listPrinters
			// 
			this.listPrinters.Location = new System.Drawing.Point(24, 88);
			this.listPrinters.Name = "listPrinters";
			this.listPrinters.Size = new System.Drawing.Size(442, 160);
			this.listPrinters.TabIndex = 0;
			// 
			// butDefault
			// 
			this.butDefault.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDefault.Autosize = true;
			this.butDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefault.Location = new System.Drawing.Point(24, 58);
			this.butDefault.Name = "butDefault";
			this.butDefault.Size = new System.Drawing.Size(88, 25);
			this.butDefault.TabIndex = 1;
			this.butDefault.Text = "&Use Default";
			this.butDefault.Click += new System.EventHandler(this.butDefault_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(250, 14);
			this.label1.TabIndex = 2;
			this.label1.Text = "Select printer to use from this workstation:";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(406, 280);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(406, 314);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormPrinterSetup
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(510, 356);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDefault);
			this.Controls.Add(this.listPrinters);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPrinterSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Printer Setup";
			this.Load += new System.EventHandler(this.FormPrinterSetup_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPrinterSetup_Load(object sender, System.EventArgs e) {
			foreach(String ip in PrinterSettings.InstalledPrinters){
				listPrinters.Items.Add(ip);
      }
			listPrinters.SelectedIndex=-1;
			for(int i=0;i<listPrinters.Items.Count;i++){
				if(listPrinters.Items[i].ToString()==Computers.Cur.PrinterName){
					listPrinters.SelectedIndex=i;
				}
			}
		}
		
		private void butDefault_Click(object sender, System.EventArgs e) {
			listPrinters.SelectedIndex=-1;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listPrinters.SelectedIndex==-1){
				Computers.Cur.PrinterName="";
			}
			else{
				Computers.Cur.PrinterName=listPrinters.SelectedItem.ToString();
			}
			//MessageBox.Show(Computers.Cur.PrinterName);
			Computers.UpdateCur();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
		
		}

	}
}
