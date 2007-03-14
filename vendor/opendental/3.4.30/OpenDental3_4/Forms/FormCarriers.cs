using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormCarriers : System.Windows.Forms.Form{
		private OpenDental.UI.Button butAdd;
		private System.ComponentModel.IContainer components;
		private OpenDental.UI.Button butEdit;
		private System.Windows.Forms.ToolTip toolTip1;
		private OpenDental.UI.Button butCombine;
		private OpenDental.Forms.TableCarriers tbCarriers;
		///<summary>Set to true if using this dialog to select a carrier.</summary>
		public bool IsSelectMode;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private bool changed;//keeps track of whether an update is necessary.

		///<summary></summary>
		public FormCarriers()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormCarriers));
			this.butOK = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butEdit = new OpenDental.UI.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.butCombine = new OpenDental.UI.Button();
			this.tbCarriers = new OpenDental.Forms.TableCarriers();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(873, 592);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(90, 26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(873, 432);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(90, 26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butEdit.Autosize = true;
			this.butEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdit.Image = ((System.Drawing.Image)(resources.GetObject("butEdit.Image")));
			this.butEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEdit.Location = new System.Drawing.Point(873, 469);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(90, 26);
			this.butEdit.TabIndex = 9;
			this.butEdit.Text = "&Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// butCombine
			// 
			this.butCombine.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCombine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCombine.Autosize = true;
			this.butCombine.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCombine.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCombine.Location = new System.Drawing.Point(873, 506);
			this.butCombine.Name = "butCombine";
			this.butCombine.Size = new System.Drawing.Size(90, 26);
			this.butCombine.TabIndex = 10;
			this.butCombine.Text = "Co&mbine";
			this.toolTip1.SetToolTip(this.butCombine, "Combines multiple Employers");
			this.butCombine.Click += new System.EventHandler(this.butCombine_Click);
			// 
			// tbCarriers
			// 
			this.tbCarriers.BackColor = System.Drawing.SystemColors.Window;
			this.tbCarriers.Location = new System.Drawing.Point(11, 14);
			this.tbCarriers.Name = "tbCarriers";
			this.tbCarriers.ScrollValue = 72;
			this.tbCarriers.SelectedIndices = new int[0];
			this.tbCarriers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbCarriers.Size = new System.Drawing.Size(839, 647);
			this.tbCarriers.TabIndex = 11;
			this.tbCarriers.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.tbCarriers_CellDoubleClicked);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.Location = new System.Drawing.Point(873, 628);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(90, 26);
			this.butCancel.TabIndex = 12;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormCarriers
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(970, 677);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.tbCarriers);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCombine);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butEdit);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCarriers";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Carriers";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormCarriers_Closing);
			this.Load += new System.EventHandler(this.FormCarriers_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormCarriers_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			Carriers.Refresh();
			tbCarriers.ResetRows(Carriers.List.Length);
			tbCarriers.SetGridColor(Color.Gray);
			tbCarriers.SetBackGColor(Color.White);
			for(int i=0;i<Carriers.List.Length;i++){
				tbCarriers.Cell[0,i]=Carriers.List[i].CarrierName;
				tbCarriers.Cell[1,i]=Carriers.List[i].Phone;
				tbCarriers.Cell[2,i]=Carriers.List[i].Address;
				tbCarriers.Cell[3,i]=Carriers.List[i].Address2;
				tbCarriers.Cell[4,i]=Carriers.List[i].City;
				tbCarriers.Cell[5,i]=Carriers.List[i].State;
				tbCarriers.Cell[6,i]=Carriers.List[i].Zip;
				tbCarriers.Cell[7,i]=Carriers.List[i].ElectID;
				if(IsSelectMode && Carriers.Cur.CarrierNum==Carriers.List[i].CarrierNum){
					tbCarriers.SetSelected(i,true);
				}
			}
			tbCarriers.LayoutTables();
			if(tbCarriers.SelectedIndices.Length>0){
				tbCarriers.ScrollToLine(tbCarriers.SelectedIndices[0]);
			}
		}

		private void tbCarriers_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
		//private void listEmp_DoubleClick(object sender, System.EventArgs e) {
			Carriers.Cur=Carriers.List[e.Row];
			if(IsSelectMode){
				DialogResult=DialogResult.OK;
				return;
			}
			FormCarrierEdit FormCE=new FormCarrierEdit();
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK)
				return;
			changed=true;
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Carriers.Cur=new Carrier();
			FormCarrierEdit FormCE=new FormCarrierEdit();
			FormCE.IsNew=true;
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK)
				return;
			changed=true;
			FillGrid();
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			if(tbCarriers.SelectedIndices.Length!=1){
				MessageBox.Show(Lan.g(this,"Please select one item first."));
				return;
			}
			Carriers.Cur=Carriers.List[tbCarriers.SelectedIndices[0]];
			FormCarrierEdit FormCE=new FormCarrierEdit();
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK)
				return;
			changed=true;
			FillGrid();
		}

		private void butCombine_Click(object sender, System.EventArgs e) {
			if(tbCarriers.SelectedIndices.Length<2){
				MessageBox.Show(Lan.g(this,"Please select multiple items first while holding down the control key."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Combine all these carriers into a single carrier? This will affect all patients using these carriers.  The next window will let you select which carrier to keep when combining."),""
				,MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			//int[] indices=new int[tbCarriers.SelectedIndices.Length];
			//tbCarriers.SelectedIndices.CopyTo(indices,0);
			FormCarrierCombine FormCB=new FormCarrierCombine();
			FormCB.ShowingIndices=tbCarriers.SelectedIndices;
			FormCB.ShowDialog();
			if(FormCB.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			int[] carrierNums=new int[tbCarriers.SelectedIndices.Length];
			for(int i=0;i<tbCarriers.SelectedIndices.Length;i++){
				carrierNums[i]=Carriers.List[tbCarriers.SelectedIndices[i]].CarrierNum;
			}
			Carriers.Combine(carrierNums,FormCB.UsingIndex);
			FillGrid();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(IsSelectMode){
				if(tbCarriers.SelectedIndices.Length!=1){
					//Employers.Cur=new Employer();
					MessageBox.Show(Lan.g(this,"Please select one item first."));
					return;
				}
				Carriers.Cur=Carriers.List[tbCarriers.SelectedIndices[0]];
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormCarriers_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//it doesn't matter whether the user hits ok, or cancel for this to happen
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Carriers);
			}
		}

		

		
		

		

		

		

		


	}
}



























