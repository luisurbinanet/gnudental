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
///<summary></summary>
	public class FormInsTemplates : System.Windows.Forms.Form{
		private OpenDental.ContrTable Table2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.Container components = null;// Required designer variable.
		//private InsTemplates InsTemplates;
		private System.Windows.Forms.Button butBlank;
		private OpenDental.XPButton butDelete;
		private OpenDental.XPButton butAdd;
		private OpenDental.XPButton butEdit;
		private System.Windows.Forms.Button butCancel;
		///<summary>when not selecting a template for a new plan</summary>
		public bool ViewOnly;

		///<summary></summary>
		public FormInsTemplates(){
			InitializeComponent();// Required for Windows Form Designer support
			Table2.CellClicked += new OpenDental.ContrTable.CellEventHandler(Table2_CellClicked);
			Table2.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(Table2_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label1,
				label2,
				label3,
				butBlank,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butAdd,
				butDelete,
				butEdit,
				butCancel
			});
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormInsTemplates));
			this.Table2 = new OpenDental.ContrTable();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butCancel = new System.Windows.Forms.Button();
			this.butBlank = new System.Windows.Forms.Button();
			this.butDelete = new OpenDental.XPButton();
			this.butAdd = new OpenDental.XPButton();
			this.butEdit = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// Table2
			// 
			this.Table2.BackColor = System.Drawing.SystemColors.Window;
			this.Table2.Location = new System.Drawing.Point(0, 24);
			this.Table2.Name = "Table2";
			this.Table2.ScrollValue = 150;
			this.Table2.SelectedIndices = new int[0];
			this.Table2.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.Table2.Size = new System.Drawing.Size(440, 428);
			this.Table2.TabIndex = 0;
			this.Table2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Table2_KeyPress);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(220, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Shortcut: Press a letter on your keyboard";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(300, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(184, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "Double Click on List to Select Plan";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(219, 564);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "Edit List:";
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(757, 556);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(86, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butBlank
			// 
			this.butBlank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butBlank.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butBlank.Location = new System.Drawing.Point(613, 556);
			this.butBlank.Name = "butBlank";
			this.butBlank.Size = new System.Drawing.Size(87, 26);
			this.butBlank.TabIndex = 3;
			this.butBlank.Text = "&Blank Plan";
			this.butBlank.Click += new System.EventHandler(this.butBlank_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(362, 556);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(85, 26);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(278, 556);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(85, 26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butEdit.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butEdit.Image = ((System.Drawing.Image)(resources.GetObject("butEdit.Image")));
			this.butEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEdit.Location = new System.Drawing.Point(447, 556);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(85, 26);
			this.butEdit.TabIndex = 9;
			this.butEdit.Text = "&Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// FormInsTemplates
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(848, 590);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butBlank);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Table2);
			this.Name = "FormInsTemplates";
			this.ShowInTaskbar = false;
			this.Text = "Insurance Templates";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormInsTemplates_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsTemplates_Load(object sender, System.EventArgs e) {
			FillTable();
		}

		private void FillTable(){	
			//Table2.
			Table2.MaxRows=20;
			Table2.MaxCols=9;
			Table2.ShowScroll=true;
			Table2.FieldsArePresent=true;
			Table2.HeadingIsPresent=true;
			Table2.InstantClassesPar();
			Table2.SetRowHeight(0,19,14);
			Table2.Heading=Lan.g(this,"Insurance Templates");
			Table2.Fields[0]=Lan.g(this,"Carrier");
			Table2.Fields[1]=Lan.g(this,"Address");
			Table2.Fields[2]=Lan.g(this,"City");
			Table2.Fields[3]=Lan.g(this,"ST");
			Table2.Fields[4]=Lan.g(this,"Zip");
			Table2.Fields[5]=Lan.g(this,"Phone");
			Table2.Fields[6]=Lan.g(this,"noE");
			Table2.Fields[7]=Lan.g(this,"ElectID");
			Table2.Fields[8]=Lan.g(this,"Note");
			//Table2.ColAlign[6]=2;
			Table2.ColWidth[0]=160;
			Table2.ColWidth[1]=120;
			Table2.ColWidth[2]=80;
			Table2.ColWidth[3]=25;
			Table2.ColWidth[4]=50;
			Table2.ColWidth[5]=75;
			Table2.ColWidth[6]=35;
			Table2.ColWidth[7]=45;
			//Table2.ColWidth[8]=300;
			//InsTemplates=new InsTemplates();
			InsTemplates.Refresh();
			Table2.ResetRows(InsTemplates.List.Length);
			Table2.SetGridColor(Color.Gray);
			//fill with data:
			for (int i=0;i<InsTemplates.List.Length;i++){
				Table2.Cell[0,i]=InsTemplates.List[i].Carrier;
				Table2.Cell[1,i]=InsTemplates.List[i].Address;
				Table2.Cell[2,i]=InsTemplates.List[i].City;
				Table2.Cell[3,i]=InsTemplates.List[i].State;
				Table2.Cell[4,i]=InsTemplates.List[i].Zip;
				Table2.Cell[5,i]=InsTemplates.List[i].Phone;
				if(InsTemplates.List[i].NoSendElect)
					Table2.Cell[6,i]="X";
				Table2.Cell[7,i]=InsTemplates.List[i].ElectID;
				Table2.Cell[8,i]=InsTemplates.List[i].Note;
				
			}
			Table2.ColWidth[8]=Width-160-120-80-25-50-75-35-45-25;//25 is for scroll bar
			Table2.Height=Height-100;
			Table2.LayoutTables();
			InsTemplates.IsSelected=false;
			Table2.Select();
		}

		private void Table2_CellClicked(object sender, CellEventArgs e){
			Table2.ColorRow(InsTemplates.Selected,Color.White);
			Table2.ColorRow(e.Row,Color.LightGray);
			InsTemplates.Selected=e.Row;
			InsTemplates.IsSelected=true;
		}

		private void Table2_CellDoubleClicked(object sender, CellEventArgs e){
			if(ViewOnly){
				return;
			}
			InsPlans.Cur = new InsPlan();
			InsPlans.Cur.Subscriber =Patients.Cur.PatNum;
			InsPlans.Cur.SubscriberID=Patients.Cur.SSN;
			InsPlans.Cur.Carrier    =InsTemplates.List[e.Row].Carrier;
			InsPlans.Cur.Phone      =InsTemplates.List[e.Row].Phone;
			InsPlans.Cur.Address    =InsTemplates.List[e.Row].Address;
			InsPlans.Cur.Address2   =InsTemplates.List[e.Row].Address2;
			InsPlans.Cur.City       =InsTemplates.List[e.Row].City;
			InsPlans.Cur.State      =InsTemplates.List[e.Row].State;
			InsPlans.Cur.Zip        =InsTemplates.List[e.Row].Zip;
			InsPlans.Cur.NoSendElect=InsTemplates.List[e.Row].NoSendElect;
			InsPlans.Cur.ElectID    =InsTemplates.List[e.Row].ElectID;
			//InsPlans.Cur.PlanNote =InsTemplates.List[e.Row].Note;//don't need this. Just clutter.
			InsPlans.Cur.PlanType   =InsTemplates.List[e.Row].PlanType;
			InsPlans.Cur.ClaimFormNum =InsTemplates.List[e.Row].ClaimFormNum;
			InsPlans.Cur.UseAltCode   =InsTemplates.List[e.Row].UseAltCode;
			InsPlans.Cur.ClaimsUseUCR =InsTemplates.List[e.Row].ClaimsUseUCR;
			InsPlans.Cur.FeeSched     =InsTemplates.List[e.Row].FeeSched;
			InsPlans.Cur.IsWrittenOff =InsTemplates.List[e.Row].IsWrittenOff;
			InsPlans.Cur.CopayFeeSched=InsTemplates.List[e.Row].CopayFeeSched;
			InsPlans.Cur.AnnualMax=-1;
			InsPlans.Cur.OrthoMax=-1;
			InsPlans.Cur.RenewMonth=1;
			InsPlans.Cur.Deductible=-1;
			InsPlans.Cur.FloToAge=-1;
			InsPlans.Cur.ReleaseInfo=true;
			InsPlans.Cur.AssignBen=true;
			InsPlans.InsertCur();
			FormInsPlan FormIP=new FormInsPlan();
			FormIP.IsNew=true;
			FormIP.ShowDialog();
			if(FormIP.DialogResult==DialogResult.OK)
				DialogResult=DialogResult.OK;
		}

		private void butBlank_Click(object sender, System.EventArgs e) {
			if(ViewOnly==true){
				return;
			}
			InsPlans.Cur = new InsPlan();
			InsPlans.Cur.Subscriber=Patients.Cur.PatNum;
			InsPlans.Cur.AnnualMax=-1;//blank
			InsPlans.Cur.OrthoMax=-1;
			InsPlans.Cur.RenewMonth=1;
			InsPlans.Cur.Deductible=-1;
			InsPlans.Cur.FloToAge=-1;
			InsPlans.Cur.ReleaseInfo=true;
			InsPlans.Cur.AssignBen=true;
			InsPlans.InsertCur();
			FormInsPlan FormIP=new FormInsPlan();
			FormIP.IsNew=true;
			FormIP.ShowDialog();
			if(FormIP.DialogResult==DialogResult.OK)
				DialogResult=DialogResult.OK;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormInsTemplatesEdit FormITE=new FormInsTemplatesEdit();
			FormITE.IsNew=true;
			InsTemplates.Cur=new InsTemplate();
			FormITE.ShowDialog();
			FillTable();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(InsTemplates.IsSelected==false){
				MessageBox.Show(Lan.g(this,"Please select template first"));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Selected Template?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
				return;
			InsTemplates.DeleteSelected();
			FillTable();
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			if(!InsTemplates.IsSelected){
				MessageBox.Show(Lan.g(this,"Please select template first"));
				return;
			}
			FormInsTemplatesEdit FormITE=new FormInsTemplatesEdit();
			FormITE.IsNew=false;
			InsTemplates.Cur=InsTemplates.List[InsTemplates.Selected];
			FormITE.ShowDialog();
			FillTable();
		}

		private void Table2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			//e.?
		}

		private void Table2_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			int moveTo=Table2.MaxRows;//in case there are no y's or z's
			for(int i=0;i<InsTemplates.List.Length;i++){
				if(String.Compare(e.KeyChar.ToString(),InsTemplates.List[i].Carrier,true)<=0){
					moveTo=i;
					break;
				}
			}
			
			Table2.ScrollToLine(moveTo);
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
