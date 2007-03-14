using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormDefinitions : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textGuide;
		private System.Windows.Forms.GroupBox groupEdit;
		private OpenDental.TableDefs tbDefs;
		private System.Windows.Forms.ListBox listCategory;
		private System.Windows.Forms.Label label13;
		private System.ComponentModel.Container components = null;
		private OpenDental.XPButton butAdd;
		private OpenDental.XPButton butUp;
		private OpenDental.XPButton butDown;
		private System.Windows.Forms.Button butHide;
		private int SelectedCat;

		///<summary></summary>
		public FormDefinitions(){
			InitializeComponent();// Required for Windows Form Designer support
			tbDefs.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbDefs_CellDoubleClicked);
			tbDefs.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbDefs_CellClicked);
			SelectedCat=-1;
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label13,
				this.label14,
				this.groupEdit,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
				butUp,
				butDown,
				butAdd,
				butHide,
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDefinitions));
			this.butClose = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.textGuide = new System.Windows.Forms.TextBox();
			this.groupEdit = new System.Windows.Forms.GroupBox();
			this.butHide = new System.Windows.Forms.Button();
			this.butDown = new OpenDental.XPButton();
			this.butUp = new OpenDental.XPButton();
			this.butAdd = new OpenDental.XPButton();
			this.tbDefs = new OpenDental.TableDefs();
			this.listCategory = new System.Windows.Forms.ListBox();
			this.label13 = new System.Windows.Forms.Label();
			this.groupEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(545, 564);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(58, 474);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100, 18);
			this.label14.TabIndex = 22;
			this.label14.Text = "Guidelines";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textGuide
			// 
			this.textGuide.Location = new System.Drawing.Point(158, 470);
			this.textGuide.Multiline = true;
			this.textGuide.Name = "textGuide";
			this.textGuide.Size = new System.Drawing.Size(460, 80);
			this.textGuide.TabIndex = 2;
			this.textGuide.Text = "";
			// 
			// groupEdit
			// 
			this.groupEdit.Controls.Add(this.butHide);
			this.groupEdit.Controls.Add(this.butDown);
			this.groupEdit.Controls.Add(this.butUp);
			this.groupEdit.Controls.Add(this.butAdd);
			this.groupEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupEdit.Location = new System.Drawing.Point(158, 402);
			this.groupEdit.Name = "groupEdit";
			this.groupEdit.Size = new System.Drawing.Size(460, 54);
			this.groupEdit.TabIndex = 1;
			this.groupEdit.TabStop = false;
			this.groupEdit.Text = "Edit Items";
			// 
			// butHide
			// 
			this.butHide.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butHide.Location = new System.Drawing.Point(140, 19);
			this.butHide.Name = "butHide";
			this.butHide.Size = new System.Drawing.Size(75, 26);
			this.butHide.TabIndex = 10;
			this.butHide.Text = "&Hide";
			this.butHide.Click += new System.EventHandler(this.butHide_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDown.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDown.Image = ((System.Drawing.Image)(resources.GetObject("butDown.Image")));
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(348, 19);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(79, 26);
			this.butDown.TabIndex = 9;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butUp.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butUp.Image = ((System.Drawing.Image)(resources.GetObject("butUp.Image")));
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(242, 19);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(79, 26);
			this.butUp.TabIndex = 8;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(34, 19);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79, 26);
			this.butAdd.TabIndex = 6;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// tbDefs
			// 
			this.tbDefs.BackColor = System.Drawing.SystemColors.Window;
			this.tbDefs.Location = new System.Drawing.Point(158, 36);
			this.tbDefs.Name = "tbDefs";
			this.tbDefs.ScrollValue = 1;
			this.tbDefs.SelectedIndices = new int[0];
			this.tbDefs.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbDefs.Size = new System.Drawing.Size(459, 356);
			this.tbDefs.TabIndex = 19;
			// 
			// listCategory
			// 
			this.listCategory.Items.AddRange(new object[] {
																											"Account Colors",
																											"Adj Types",
																											"Appointment Colors",
																											"Appt Confirmed",
																											"Appt Phone Notes",
																											"Appt Procs Quick Add",
																											"Billing Types",
																											"Chart Graphic Colors",
																											"Claim Formats",
																											"Contact Categories",
																											"Diagnosis",
																											"Discount Types",
																											"Dunning Messages",
																											"Fee Sched Names",
																											"Image Categories",
																											"Medical Notes",
																											"Misc Colors",
																											"Operatories",
																											"Payment Types",
																											"Proc Code Categories",
																											"Prog Notes Colors",
																											"Recall/Unsch Status",
																											"Service Notes",
																											"Treat\' Plan Priorities"});
			this.listCategory.Location = new System.Drawing.Point(22, 36);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(124, 316);
			this.listCategory.TabIndex = 0;
			this.listCategory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listCategory_MouseDown);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(22, 20);
			this.label13.Name = "label13";
			this.label13.TabIndex = 17;
			this.label13.Text = "Select Category:";
			// 
			// FormDefinitions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(672, 618);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.textGuide);
			this.Controls.Add(this.groupEdit);
			this.Controls.Add(this.tbDefs);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.butClose);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDefinitions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Definitions";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormDefinitions_Closing);
			this.Load += new System.EventHandler(this.FormDefinitions_Load);
			this.groupEdit.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDefinitions_Load(object sender, System.EventArgs e) {
			if(!UserPermissions.CheckUserPassword("Definitions")){
				MessageBox.Show(Lan.g(this,"You do not have permission for this feature"));
				DialogResult=DialogResult.Cancel;
				return;
			}
		}

		private void FillDefs(){
			//Defs.IsSelected=false;
			Defs.GetCatList(SelectedCat);
			tbDefs.ResetRows(Defs.List.Length);
			tbDefs.SetBackGColor(Color.White);
			for(int i=0;i<Defs.List.Length;i++){
				tbDefs.Cell[0,i]=Defs.List[i].ItemName;
				tbDefs.Cell[1,i]=Defs.List[i].ItemValue;
				if(FormDefEdit.EnableColor){
					tbDefs.BackGColor[2,i]=Defs.List[i].ItemColor;
				}
				if(Defs.List[i].IsHidden)
					tbDefs.Cell[3,i]="X";
				//else tbDefs.Cell[3,i]="";
			}
			if(Defs.IsSelected){
				tbDefs.BackGColor[0,Defs.Selected]=Color.LightGray;
				tbDefs.BackGColor[1,Defs.Selected]=Color.LightGray;
			}
			tbDefs.Fields[1]=FormDefEdit.ValueText;
			if(FormDefEdit.EnableColor){
				tbDefs.Fields[2]="Color";
			}
			else{
				tbDefs.Fields[2]="";
			}
			tbDefs.LayoutTables();
			//the following do not require a refresh of the table:
			if(FormDefEdit.CanEditName){
				groupEdit.Enabled=true;
				groupEdit.Text="Edit Items";
			}
			else{
				groupEdit.Enabled=false;
				groupEdit.Text="Not allowed";
			}
			textGuide.Text=FormDefEdit.HelpText;
		}

		private void listCategory_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			Defs.IsSelected=false;
			listCategory.SelectedIndex=listCategory.IndexFromPoint(e.X,e.Y);
			FormDefEdit.EnableColor=false;
			FormDefEdit.EnableValue=false;
			FormDefEdit.CanEditName=true;//false;
			tbDefs.Fields[1]="";
			FormDefEdit.ValueText="";
			switch(listCategory.SelectedItem.ToString()){
				case "Account Colors":
					SelectedCat=0;
					FormDefEdit.CanEditName=false;
					FormDefEdit.EnableColor=true;
					FormDefEdit.HelpText="Changes the color of text for different types of entries in Account Module";
					break;
				case "Adj Types":
					SelectedCat=1;
					FormDefEdit.ValueText="+ or -";
					FormDefEdit.EnableValue=true;
					FormDefEdit.HelpText="Plus increases the patient balance.  Minus decreases it.  Not allowed "
						+"to change value after creating new type since changes affect all patient accounts.";
					break;
				case "Appt Confirmed":
					SelectedCat=2;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText="Abbrev";
					FormDefEdit.EnableColor=true;
					//tbDefs.Fields[2]="Color";
					FormDefEdit.HelpText="Color shows in bar on left of each appointment.  Changes affect all appointments.";
					break;
				case "Appt Procs Quick Add":
					SelectedCat=3;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText="ADA Code(s)";
					FormDefEdit.HelpText="These are the procedures that you can quickly add to the treatment"
						+" plan from within the appointment editing window.  They must not require a tooth number."
						+" Multiple procedure may be separated by commas with no spaces. "
						+"  These definitions may be freely edited without affecting any patient records.";
					break;
				case "Billing Types":
					SelectedCat=4;
					FormDefEdit.HelpText="It is recommended to use as few billing types as possible.  "
						+"They can be useful when running reports to separate delinquent accounts, but "
						+"can cause 'forgotten accounts' if used without good office procedures. "
						+"Changes affect all patients.";
					break;
				case "Claim Formats":
					SelectedCat=5;
					FormDefEdit.EnableValue=false;
					FormDefEdit.ValueText="";
					FormDefEdit.HelpText=Lan.g(this,"This category is obsolete.");
					break;
				case "Dunning Messages":
					SelectedCat=6;
					FormDefEdit.CanEditName=false;
					FormDefEdit.EnableValue=false;//true;
					FormDefEdit.ValueText="Message";
					FormDefEdit.HelpText="This category is not currently being used.";
						//"Messages will automatically show up on statements with "
						//+"the specified aging.  Family is considered to have insurance if any family "
						//+"member has insurance.";
					break;
				case "Fee Sched Names":
					SelectedCat=7;
					FormDefEdit.HelpText="Fee Schedule names.  Caution: any changes to the names affect all patients. "
						+"Changing the order does not cause any problems.";
					break;
				case "Medical Notes":
					SelectedCat=8;
					FormDefEdit.HelpText="This section is no longer used. Right click on text boxes instead. See Quick Add Notes.";
					break;
				case "Operatories":
					SelectedCat=9;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText="Abbreviation";
					FormDefEdit.HelpText="Ops will be displayed in the appointment book in the "
						+"order shown above.  Max length of abbreviations is 5 characters.";
					break;
				case "Payment Types":
					SelectedCat=10;
					FormDefEdit.HelpText="Types of payments that patients might make. "
						+"Any changes will affect all patients.";
					break;
				case "Proc Code Categories":
					SelectedCat=11;
					FormDefEdit.HelpText="These are the categories for organizing procedure codes. "
						+"They do not have to follow ADA categories.  There is no relationship to "
						+"insurance categories which are setup in the Ins Categories section.  "
						+"Does not affect any patient records.";
					break;
				case "Prog Notes Colors":
					SelectedCat=12;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText="Changes color of text for different types of entries"
						+" in the Chart Module Progress Notes.";
					break;
				case "Recall/Unsch Status":
					SelectedCat=13;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText="Abbreviation";
					FormDefEdit.HelpText="Recall/Unsched Status.  Abbreviation must be 7"
						+" characters or less.  Changes affect all patients.";
					break;
				case "Service Notes":
					SelectedCat=14;
					FormDefEdit.HelpText="This section is no longer used. Right click on text boxes instead. See Quick Add Notes.";
					break;
				case "Discount Types":
					SelectedCat=15;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText="Percentage Discount";
					FormDefEdit.HelpText="Discount types will show from the payment screen.  The percentage value should be without a decimal. For example, '10' would be a 10% discount. Value may be left blank.  Changes affect all patients.";
					break;
				case "Diagnosis":
					SelectedCat=16;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText="1 or 2 letter abbreviation";
					FormDefEdit.HelpText="The diagnosis list is shown when entering a procedure.  Ones that "
						+"are less used should go lower on the list.  The abbreviation is shown in the progress"
						+" notes.  BE VERY CAREFUL.  Changes affect all patients.";
					break;
				case "Appointment Colors":
					SelectedCat=17;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText="Changes colors of background in Appointments Module, and colors for completed"
						+" appointments.";
					break;
				case "Image Categories":
					SelectedCat=18;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText="Visible in Chart module";
					FormDefEdit.HelpText="These are the categories that will be available in the image and chart modules.  If you hide a category, images in that category will be hidden, so only hide a category if you are certain it has never been used.  If you want the category to show in the Chart module, enter an x in the second column.  Affects all patient records.";
					break;
				case "Appt Phone Notes":
					SelectedCat=19;
					FormDefEdit.HelpText="This section is no longer used. Right click on text boxes instead. See Quick Add Notes.";
					break;
				case "Treat' Plan Priorities":
					SelectedCat=20;
					FormDefEdit.EnableColor=true;
					FormDefEdit.HelpText="Priorities available for selection in the Treatment Plan"
						+" module.  They can be simple numbers or descriptive abbreviations"
						+" 7 letters or less.  Changes affect all procedures where the definition is used.";
					break;
				case "Misc Colors":
					SelectedCat=21;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText="";
					break;
				case "Chart Graphic Colors":
					SelectedCat=22;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText="These colors will be used on the graphical tooth chart "
						+"to draw restorations.";
					break;
				case "Contact Categories":
					SelectedCat=(int)DefCat.ContactCategories;
					FormDefEdit.HelpText="You can add as many categories as you want.  Changes affect all current "
						+"contact records.";
					break;
			}
			FillDefs();
		}

		private void tbDefs_CellClicked(object sender, CellEventArgs e){
			//Can't move this logic into the Table control because we never want to paint on col 3
			if(Defs.IsSelected){
				if(Defs.Selected==e.Row){
					tbDefs.BackGColor[0,e.Row]=Color.White;
					tbDefs.BackGColor[1,e.Row]=Color.White;
					Defs.IsSelected=false;
				}
				else{
					tbDefs.BackGColor[0,Defs.Selected]=Color.White;
					tbDefs.BackGColor[1,Defs.Selected]=Color.White;
					tbDefs.BackGColor[0,e.Row]=Color.LightGray;
					tbDefs.BackGColor[1,e.Row]=Color.LightGray;
					Defs.Selected=e.Row;
					Defs.IsSelected=true;
				}
			}
			else{
				tbDefs.BackGColor[0,e.Row]=Color.LightGray;
				tbDefs.BackGColor[1,e.Row]=Color.LightGray;
				Defs.Selected=e.Row;
				Defs.IsSelected=true;
			}
			tbDefs.Refresh();
		}

		private void tbDefs_CellDoubleClicked(object sender, CellEventArgs e){
			tbDefs.BackGColor[0,e.Row]=SystemColors.Highlight;
			tbDefs.BackGColor[1,e.Row]=SystemColors.Highlight;
			tbDefs.Refresh();
			Defs.IsSelected=true;
			Defs.Selected=e.Row;
			FormDefEdit FormDefEdit2 = new FormDefEdit();
			Defs.Cur = Defs.List[e.Row];
			FormDefEdit2.IsNew=false;
			FormDefEdit2.ShowDialog();
			//Preferences2.GetCatList(listCategory.SelectedIndex);
			FillDefs();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			if(SelectedCat<0){
				MessageBox.Show(Lan.g(this,"Please select item first."));
				return;
			}
			FormDefEdit FormDE=new FormDefEdit();
			Defs.Cur=new Def();
			Defs.Cur.ItemOrder=Defs.List.Length;
			Defs.Cur.Category=(DefCat)SelectedCat;
			FormDE.IsNew=true;
			FormDE.ShowDialog();
			if(FormDE.DialogResult!=DialogResult.OK){
				return;
			}
			Defs.Selected=Defs.List.Length;//this is one more than allowed, but it's ok
			Defs.IsSelected=true;
			FillDefs();
		}

		private void butHide_Click(object sender, System.EventArgs e) {
			if(!Defs.IsSelected){
				MessageBox.Show(Lan.g(this,"Please select item first,"));
				return;
			}
			Defs.HideDef();
			FillDefs();
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			Defs.MoveUp();
			FillDefs();
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			Defs.MoveDown();
			FillDefs();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormDefinitions_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			DataValid.IType=InvalidType.LocalData;
			DataValid DataValid2=new DataValid();
			DataValid2.SetInvalid();
			Defs.IsSelected=false;
			SecurityLogs.MakeLogEntry("Definitions","Altered Definitions");
		}



		



	}
}
