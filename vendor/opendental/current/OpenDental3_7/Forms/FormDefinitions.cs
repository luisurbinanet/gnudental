using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormDefinitions : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textGuide;
		private System.Windows.Forms.GroupBox groupEdit;
		private OpenDental.TableDefs tbDefs;
		private System.Windows.Forms.ListBox listCategory;
		private System.Windows.Forms.Label label13;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butHide;
		//<summary>This is the index of the selected cat.</summary>
		//private int InitialCat;
		///<summary>this is (int)DefCat, not the index of the selected Cat.</summary>
		private int SelectedCat;
		private bool changed;
		///<summary>Gives the DefCat for each item in the list.</summary>
		private DefCat[] lookupCat;
		//private User user;

		///<summary></summary>
		public FormDefinitions(DefCat selectedCat){
			InitializeComponent();// Required for Windows Form Designer support
			SelectedCat=(int)selectedCat;
			tbDefs.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbDefs_CellDoubleClicked);
			tbDefs.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbDefs_CellClicked);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDefinitions));
			this.butClose = new OpenDental.UI.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.textGuide = new System.Windows.Forms.TextBox();
			this.groupEdit = new System.Windows.Forms.GroupBox();
			this.butHide = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.tbDefs = new OpenDental.TableDefs();
			this.listCategory = new System.Windows.Forms.ListBox();
			this.label13 = new System.Windows.Forms.Label();
			this.groupEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(545, 564);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(57, 474);
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
			this.butHide.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butHide.Autosize = true;
			this.butHide.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHide.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
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
																											"Blockout Types",
																											"Chart Graphic Colors",
																											"Claim Formats",
																											"Contact Categories",
																											"Diagnosis",
																											"Discount Types",
																											"Dunning Messages",
																											"Fee Sched Names",
																											"Image Categories",
																											"Letter Merge Cats",
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
			this.listCategory.Size = new System.Drawing.Size(124, 342);
			this.listCategory.TabIndex = 0;
			this.listCategory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listCategory_MouseDown);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(22, 18);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(162, 17);
			this.label13.TabIndex = 17;
			this.label13.Text = "Select Category:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormDefinitions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(672, 618);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.textGuide);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.groupEdit);
			this.Controls.Add(this.tbDefs);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.label13);
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
			/*if(PermissionsOld.AuthorizationRequired("Definitions")){
				user=Users.Authenticate("Definitions");
				if(!UserPermissions.IsAuthorized("Definitions",user)){
					MsgBox.Show(this,"You do not have permission for this feature");
					DialogResult=DialogResult.Cancel;
					return;
				}	
			}*/
			lookupCat=new DefCat[listCategory.Items.Count];
			lookupCat[0]=DefCat.AccountColors;
			lookupCat[1]=DefCat.AdjTypes;
			lookupCat[2]=DefCat.AppointmentColors;
			lookupCat[3]=DefCat.ApptConfirmed;
			lookupCat[4]=DefCat.ApptPhoneNotes;
			lookupCat[5]=DefCat.ApptProcsQuickAdd;
			lookupCat[6]=DefCat.BillingTypes;
			lookupCat[7]=DefCat.BlockoutTypes;
			lookupCat[8]=DefCat.ChartGraphicColors;
			lookupCat[9]=DefCat.ClaimFormats;
			lookupCat[10]=DefCat.ContactCategories;
			lookupCat[11]=DefCat.Diagnosis;
			lookupCat[12]=DefCat.DiscountTypes;
			lookupCat[13]=DefCat.DunningMessages;
			lookupCat[14]=DefCat.FeeSchedNames;
			lookupCat[15]=DefCat.ImageCats;
			lookupCat[16]=DefCat.LetterMergeCats;
			lookupCat[17]=DefCat.MedicalNotes;
			lookupCat[18]=DefCat.MiscColors;
			lookupCat[19]=DefCat.OperatoriesOld;
			lookupCat[20]=DefCat.PaymentTypes;
			lookupCat[21]=DefCat.ProcCodeCats;
			lookupCat[22]=DefCat.ProgNoteColors;
			lookupCat[23]=DefCat.RecallUnschedStatus;
			lookupCat[24]=DefCat.ServiceNotes;
			lookupCat[25]=DefCat.TxPriorities;
			for(int i=0;i<listCategory.Items.Count;i++){
				listCategory.Items[i]=Lan.g(this,(string)listCategory.Items[i]);
				if((int)lookupCat[i]==SelectedCat){
					listCategory.SelectedIndex=i;
				}
			}
			FillCats();
		}

		private void listCategory_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			listCategory.SelectedIndex=listCategory.IndexFromPoint(e.X,e.Y);
			//test for -1 only necessary if there is whitespace, which there is not.
			SelectedCat=(int)lookupCat[listCategory.SelectedIndex];
			FillCats();
		}

		private void FillCats(){
			//a category is ALWAYS selected; never -1.
			Defs.IsSelected=false;
			FormDefEdit.EnableColor=false;
			FormDefEdit.EnableValue=false;
			FormDefEdit.CanEditName=true;//false;
			tbDefs.Fields[1]="";
			FormDefEdit.ValueText="";
			switch(listCategory.SelectedIndex){
				case 0://"Account Colors":
					//SelectedCat=0;
					FormDefEdit.CanEditName=false;
					FormDefEdit.EnableColor=true;
					FormDefEdit.HelpText=Lan.g(this,"Changes the color of text for different types of entries in Account Module");
					break;
				case 1://"Adj Types":
					//SelectedCat=1;
					FormDefEdit.ValueText=Lan.g(this,"+ or -");
					FormDefEdit.EnableValue=true;
					FormDefEdit.HelpText=Lan.g(this,"Plus increases the patient balance.  Minus decreases it.  Not allowed to change value after creating new type since changes affect all patient accounts.");
					break;
				case 2://"Appointment Colors":
					//SelectedCat=17;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText=Lan.g(this,"Changes colors of background in Appointments Module, and colors for completed appointments.");
					break;
				case 3://"Appt Confirmed":
					//SelectedCat=2;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"Abbrev");
					FormDefEdit.EnableColor=true;
					//tbDefs.Fields[2]="Color";
					FormDefEdit.HelpText=Lan.g(this,"Color shows in bar on left of each appointment.  Changes affect all appointments.");
					break;
				case 4://"Appt Phone Notes":
					//SelectedCat=19;
					FormDefEdit.HelpText=Lan.g(this,"This section is no longer used. Right click on text boxes instead. See Quick Add Notes.");
					break;
				case 5://"Appt Procs Quick Add":
					//SelectedCat=3;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"ADA Code(s)");
					FormDefEdit.HelpText=Lan.g(this,"These are the procedures that you can quickly add to the treatment plan from within the appointment editing window.  They must not require a tooth number. Multiple procedures may be separated by commas with no spaces. These definitions may be freely edited without affecting any patient records.");
					break;
				case 6://"Billing Types":
					//SelectedCat=4;
					FormDefEdit.HelpText=Lan.g(this,"It is recommended to use as few billing types as possible.  They can be useful when running reports to separate delinquent accounts, but can cause 'forgotten accounts' if used without good office procedures. Changes affect all patients.");
					break;
				case 7://"Blockout Types":
					FormDefEdit.EnableColor=true;
					FormDefEdit.EnableValue=false;
					FormDefEdit.HelpText=Lan.g(this,"Blockout types are used in the appointments module.");
					break;
				case 8://"Chart Graphic Colors":
					//SelectedCat=22;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText=Lan.g(this,"These colors will be used on the graphical tooth chart to draw restorations.");
					break;
				case 9://"Claim Formats":
					//SelectedCat=5;
					FormDefEdit.EnableValue=false;
					FormDefEdit.ValueText="";
					FormDefEdit.HelpText=Lan.g(this,"This category is obsolete.");
					break;
				case 10://"Contact Categories":
					//SelectedCat=(int)DefCat.ContactCategories;
					FormDefEdit.HelpText=Lan.g(this,"You can add as many categories as you want.  Changes affect all current contact records.");
					break;
				case 11://"Diagnosis":
					//SelectedCat=16;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"1 or 2 letter abbreviation");
					FormDefEdit.HelpText=Lan.g(this,"The diagnosis list is shown when entering a procedure.  Ones that are less used should go lower on the list.  The abbreviation is shown in the progress notes.  BE VERY CAREFUL.  Changes affect all patients.");
					break;
				case 12://"Discount Types":
					//SelectedCat=15;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"Percentage Discount");
					FormDefEdit.HelpText=Lan.g(this,"This category is no longer used.");
					break;
				case 13://"Dunning Messages":
					//SelectedCat=6;
					FormDefEdit.CanEditName=false;
					FormDefEdit.EnableValue=false;//true;
					FormDefEdit.ValueText=Lan.g(this,"Message");
					FormDefEdit.HelpText=Lan.g(this,"This category is not currently being used.");
						//"Messages will automatically show up on statements with "
						//+"the specified aging.  Family is considered to have insurance if any family "
						//+"member has insurance.";
					break;
				case 14://"Fee Sched Names":
					//SelectedCat=7;
					FormDefEdit.HelpText=Lan.g(this,"Fee Schedule names.  Caution: any changes to the names affect all patients. Changing the order does not cause any problems.");
					break;
				case 15://"Image Categories":
					//SelectedCat=18;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"X=Chart,P=Patient Picture");
					FormDefEdit.HelpText=Lan.g(this,"These are the categories that will be available in the image and chart modules.  If you hide a category, images in that category will be hidden, so only hide a category if you are certain it has never been used.  If you want the category to show in the Chart module, enter an X in the second column.  One category can be used for patient pictures, marked with P.  Affects all patient records.");
					break;
				case 16://"Letter Merge Cats"
					//SelectedCat=(int)DefCat.LetterMergeCats;
					FormDefEdit.HelpText=Lan.g(this,"Categories for Letter Merge.  You can safely make any changes you want.");
					break;
				case 17://"Medical Notes":
					//SelectedCat=8;
					FormDefEdit.HelpText=Lan.g(this,"This section is no longer used. Right click on text boxes instead. See Quick Add Notes.");
					break;
				case 18://"Misc Colors":
					//SelectedCat=21;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText="";
					break;
				case 19://"Operatories":
					//SelectedCat=9;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"Abbreviation");
					FormDefEdit.HelpText=Lan.g(this,"This category is obsolete. Operatories have their own setup screen now.");
					break;
				case 20://"Payment Types":
					//SelectedCat=10;
					FormDefEdit.HelpText=Lan.g(this,"Types of payments that patients might make. Any changes will affect all patients.");
					break;
				case 21://"Proc Code Categories":
					//SelectedCat=11;
					FormDefEdit.HelpText=Lan.g(this,"These are the categories for organizing procedure codes. They do not have to follow ADA categories.  There is no relationship to insurance categories which are setup in the Ins Categories section.  Does not affect any patient records.");
					break;
				case 22://"Prog Notes Colors":
					//SelectedCat=12;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText=Lan.g(this,"Changes color of text for different types of entries in the Chart Module Progress Notes.");
					break;
				case 23://"Recall/Unsch Status":
					//SelectedCat=13;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"Abbreviation");
					FormDefEdit.HelpText=Lan.g(this,"Recall/Unsched Status.  Abbreviation must be 7 characters or less.  Changes affect all patients.");
					break;
				case 24://"Service Notes":
					//SelectedCat=14;
					FormDefEdit.HelpText=Lan.g(this,"This section is no longer used. Right click on text boxes instead. See Quick Add Notes.");
					break;
				case 25://"Treat' Plan Priorities":
					//SelectedCat=20;
					FormDefEdit.EnableColor=true;
					FormDefEdit.HelpText=Lan.g(this,"Priorities available for selection in the Treatment Plan module.  They can be simple numbers or descriptive abbreviations 7 letters or less.  Changes affect all procedures where the definition is used.");
					break;
			}
			FillDefs();
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
			changed=true;
			FillDefs();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			//if(SelectedCat==-1){//never -1.
			//	MessageBox.Show(Lan.g(this,"Please select item first."));
			//	return;
			//}
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
			changed=true;
			FillDefs();
		}

		private void butHide_Click(object sender, System.EventArgs e) {
			if(!Defs.IsSelected){
				MessageBox.Show(Lan.g(this,"Please select item first,"));
				return;
			}
			Defs.HideDef();
			changed=true;
			FillDefs();
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			Defs.MoveUp();
			changed=true;
			FillDefs();
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			Defs.MoveDown();
			changed=true;
			FillDefs();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormDefinitions_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Defs);
			}
			Defs.IsSelected=false;
			//if(user!=null){
				//SecurityLogs.MakeLogEntry("Definitions","Altered Definitions",user);
			//}
		}



		



	}
}
