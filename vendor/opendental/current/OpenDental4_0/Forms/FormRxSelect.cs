using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRxSelect : System.Windows.Forms.Form{
		private OpenDental.TableRxSetup tbMain;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butBlank;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private Patient PatCur;

		///<summary></summary>
		public FormRxSelect(Patient patCur){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=patCur;
			tbMain.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellClicked);
			tbMain.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbMain_CellDoubleClicked);
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tbMain = new OpenDental.TableRxSetup();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butBlank = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// tbMain
			// 
			this.tbMain.BackColor = System.Drawing.SystemColors.Window;
			this.tbMain.Location = new System.Drawing.Point(4, 34);
			this.tbMain.Name = "tbMain";
			this.tbMain.ScrollValue = 1;
			this.tbMain.SelectedIndices = new int[0];
			this.tbMain.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbMain.Size = new System.Drawing.Size(919, 582);
			this.tbMain.TabIndex = 1;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(848, 636);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(756, 636);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(470, 16);
			this.label1.TabIndex = 15;
			this.label1.Text = "Please select a Prescription from the list or click Blank to start with a blank p" +
				"rescription.";
			// 
			// butBlank
			// 
			this.butBlank.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBlank.Autosize = true;
			this.butBlank.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBlank.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBlank.Location = new System.Drawing.Point(472, 5);
			this.butBlank.Name = "butBlank";
			this.butBlank.Size = new System.Drawing.Size(75, 26);
			this.butBlank.TabIndex = 0;
			this.butBlank.Text = "&Blank";
			this.butBlank.Click += new System.EventHandler(this.butBlank_Click);
			// 
			// FormRxSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(942, 674);
			this.Controls.Add(this.butBlank);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.tbMain);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRxSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Prescription";
			this.Load += new System.EventHandler(this.FormRxSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRxSelect_Load(object sender, System.EventArgs e) {
			FillMain();
		}

		private void FillMain(){
			RxDefs.Refresh();
			tbMain.SelectedRow=-1;
			tbMain.ResetRows(RxDefs.List.Length);
			for(int i=0;i<RxDefs.List.Length;i++){
				tbMain.Cell[0,i]=RxDefs.List[i].Drug;
				tbMain.Cell[1,i]=RxDefs.List[i].Sig;
				tbMain.Cell[2,i]=RxDefs.List[i].Disp;
				tbMain.Cell[3,i]=RxDefs.List[i].Refills;
				tbMain.Cell[4,i]=RxDefs.List[i].Notes;
			}
			tbMain.SetGridColor(Color.LightGray);
			tbMain.LayoutTables();
		}

		private void tbMain_CellClicked(object sender, CellEventArgs e){
			if(tbMain.SelectedRow!=-1)
				tbMain.ColorRow(tbMain.SelectedRow,Color.White);
			tbMain.SelectedRow=e.Row;
			tbMain.ColorRow(e.Row,Color.LightGray);
			RxDefs.Cur=RxDefs.List[tbMain.SelectedRow];
		}

		private void tbMain_CellDoubleClicked(object sender, CellEventArgs e){
			RxSelected();
		}

		private void RxSelected(){
			RxPats.Cur=new RxPat();
			RxPats.Cur.RxDate=DateTime.Today;
			RxPats.Cur.PatNum=PatCur.PatNum;
			RxPats.Cur.Drug=RxDefs.Cur.Drug;
			RxPats.Cur.Sig=RxDefs.Cur.Sig;
			RxPats.Cur.Disp=RxDefs.Cur.Disp;
			RxPats.Cur.Refills=RxDefs.Cur.Refills;
			//RxPats.Cur.Notes=RxDefs.Cur.Notes;//we don't want these kinds of notes cluttering things
			FormRxEdit FormE=new FormRxEdit(PatCur);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butBlank_Click(object sender, System.EventArgs e) {
			RxDefs.Cur=new RxDef();
			RxSelected();
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(tbMain.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select Rx first or click Blank"));
				return;
			}
			RxSelected();
		}

	}
}