using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace OpenDental{

	public class FormRpPatients : System.Windows.Forms.Form{
		private System.Windows.Forms.TabControl tabPatients;
		private System.Windows.Forms.TabPage tabFilters;
		private System.Windows.Forms.TabPage tabData;
		private System.Windows.Forms.ListBox ComboBox;
		private System.Windows.Forms.ListBox ListPatientSelect;
		private System.Windows.Forms.ListBox ListPrerequisites;
		private System.Windows.Forms.ListBox ListReferredFromSelect;
		private System.Windows.Forms.ListBox ListReferredToSelect;
		private System.Windows.Forms.ListBox ListConditions;
		private System.Windows.Forms.ComboBox DropListFilter;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butAddFilter;
		private System.Windows.Forms.Button butDeleteFilter;
		private System.Windows.Forms.TextBox TextSQL;
		private System.Windows.Forms.TextBox TextBox;
		private OpenDental.ValidNumber TextValidAge;
		private OpenDental.ValidDate TextDate;
		private System.Windows.Forms.Label labelPatient;
		private System.Windows.Forms.Label labelReferredTo;
		private System.Windows.Forms.Label labelReferredFrom;
		private System.Windows.Forms.Label labelHelp;  //fields used in SELECT 
		private System.ComponentModel.IContainer components=null;
		private FormQuery FormQuery2;
		private string SQLselect;
		private string SQLfrom;
		private string SQLwhereComparison;
    private string SQLwhereRelation;
    private string SQLgroup;
		private string sItem;//just used in local loops
    private string ProcLogLastDate;
    private string ProcLogFirstDate;
    private string[] PatFieldsSelected;     
    private string[] RefToFieldsSelected;   
    private string[]  RefFromFieldsSelected; 
		private ArrayList ALpatFilter;
		private ArrayList ALpatSelect;
    private ArrayList ALrefToSelect;
    private ArrayList ALrefFromSelect;
    private ArrayList UsingInsPlans;
    private ArrayList UsingProcLogFirst;
    private ArrayList UsingProcLogLast;
    private ArrayList UsingRefDent;
    private ArrayList UsingRefPat;    
		private bool IsText;
		private bool IsDate;
		private bool IsDropDown;
    private bool NeedInsPlan=false;
    private bool NeedRefDent=false;
    private bool NeedRefPat=false;
    private bool NeedProcLogLast=false;
    private bool NeedProcLogFirst=false; 
    private bool IsWhereRelation=false;  
    private bool PatSel;
    private bool RefToSel;
		private System.Windows.Forms.TextBox textBox1;
    private bool RefFromSel;  

		public FormRpPatients(){
			InitializeComponent();
      ALpatSelect=new ArrayList();
			ALpatFilter=new ArrayList();
      ALrefToSelect=new ArrayList();
      ALrefFromSelect=new ArrayList();
      UsingInsPlans=new ArrayList();
      UsingRefDent=new ArrayList();
      UsingRefPat=new ArrayList();
      UsingProcLogFirst=new ArrayList();
      UsingProcLogLast=new ArrayList();
      Fill();
			SQLselect="";
			SQLfrom="FROM patient ";
			SQLwhereComparison="";
      SQLwhereRelation="";
      SQLgroup=""; 
			ListConditions.SelectedIndex=0;
			IsText=false;
			IsDate=false;
			IsDropDown=false;
			TextValidAge.MinVal=0;
			TextValidAge.MaxVal=125;
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.butAddFilter,
				this.butDeleteFilter,
				this.tabData,
				this.tabFilters,
				this.labelHelp,
				this.labelPatient,
				this.labelReferredFrom,
				this.labelReferredTo,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
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

		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormRpPatients));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.tabPatients = new System.Windows.Forms.TabControl();
			this.tabData = new System.Windows.Forms.TabPage();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.labelReferredFrom = new System.Windows.Forms.Label();
			this.ListReferredFromSelect = new System.Windows.Forms.ListBox();
			this.labelReferredTo = new System.Windows.Forms.Label();
			this.ListReferredToSelect = new System.Windows.Forms.ListBox();
			this.labelPatient = new System.Windows.Forms.Label();
			this.ListPatientSelect = new System.Windows.Forms.ListBox();
			this.tabFilters = new System.Windows.Forms.TabPage();
			this.labelHelp = new System.Windows.Forms.Label();
			this.ComboBox = new System.Windows.Forms.ListBox();
			this.TextDate = new OpenDental.ValidDate();
			this.TextValidAge = new OpenDental.ValidNumber();
			this.butDeleteFilter = new System.Windows.Forms.Button();
			this.ListPrerequisites = new System.Windows.Forms.ListBox();
			this.butAddFilter = new System.Windows.Forms.Button();
			this.ListConditions = new System.Windows.Forms.ListBox();
			this.TextBox = new System.Windows.Forms.TextBox();
			this.DropListFilter = new System.Windows.Forms.ComboBox();
			this.TextSQL = new System.Windows.Forms.TextBox();
			this.tabPatients.SuspendLayout();
			this.tabData.SuspendLayout();
			this.tabFilters.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(876, 664);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Enabled = false;
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(876, 636);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 2;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tabPatients
			// 
			this.tabPatients.Controls.Add(this.tabData);
			this.tabPatients.Controls.Add(this.tabFilters);
			this.tabPatients.Location = new System.Drawing.Point(16, 6);
			this.tabPatients.Name = "tabPatients";
			this.tabPatients.SelectedIndex = 0;
			this.tabPatients.Size = new System.Drawing.Size(840, 544);
			this.tabPatients.TabIndex = 1;
			// 
			// tabData
			// 
			this.tabData.Controls.Add(this.textBox1);
			this.tabData.Controls.Add(this.labelReferredFrom);
			this.tabData.Controls.Add(this.ListReferredFromSelect);
			this.tabData.Controls.Add(this.labelReferredTo);
			this.tabData.Controls.Add(this.ListReferredToSelect);
			this.tabData.Controls.Add(this.labelPatient);
			this.tabData.Controls.Add(this.ListPatientSelect);
			this.tabData.Location = new System.Drawing.Point(4, 22);
			this.tabData.Name = "tabData";
			this.tabData.Size = new System.Drawing.Size(832, 518);
			this.tabData.TabIndex = 1;
			this.tabData.Text = "SELECT";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(220, 20);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(556, 38);
			this.textBox1.TabIndex = 13;
			this.textBox1.Text = "Hint: Use the Ctrl key when clicking.  Also you can simply drag the pointer acros" +
				"s muliple rows to select quickly.\r\nFor Referrals: If you select referral items, " +
				"it will exclude all patients without referrals.";
			// 
			// labelReferredFrom
			// 
			this.labelReferredFrom.Location = new System.Drawing.Point(438, 72);
			this.labelReferredFrom.Name = "labelReferredFrom";
			this.labelReferredFrom.Size = new System.Drawing.Size(170, 14);
			this.labelReferredFrom.TabIndex = 12;
			this.labelReferredFrom.Text = "Referred From";
			this.labelReferredFrom.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// ListReferredFromSelect
			// 
			this.ListReferredFromSelect.Location = new System.Drawing.Point(438, 86);
			this.ListReferredFromSelect.Name = "ListReferredFromSelect";
			this.ListReferredFromSelect.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ListReferredFromSelect.Size = new System.Drawing.Size(170, 420);
			this.ListReferredFromSelect.TabIndex = 11;
			this.ListReferredFromSelect.SelectedIndexChanged += new System.EventHandler(this.ListReferredFromSelect_SelectedIndexChanged);
			// 
			// labelReferredTo
			// 
			this.labelReferredTo.Location = new System.Drawing.Point(220, 72);
			this.labelReferredTo.Name = "labelReferredTo";
			this.labelReferredTo.Size = new System.Drawing.Size(168, 14);
			this.labelReferredTo.TabIndex = 8;
			this.labelReferredTo.Text = "Referred To";
			this.labelReferredTo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// ListReferredToSelect
			// 
			this.ListReferredToSelect.Location = new System.Drawing.Point(220, 86);
			this.ListReferredToSelect.Name = "ListReferredToSelect";
			this.ListReferredToSelect.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ListReferredToSelect.Size = new System.Drawing.Size(168, 420);
			this.ListReferredToSelect.TabIndex = 7;
			this.ListReferredToSelect.SelectedIndexChanged += new System.EventHandler(this.ListReferredToSelect_SelectedIndexChanged);
			// 
			// labelPatient
			// 
			this.labelPatient.Location = new System.Drawing.Point(8, 8);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(170, 14);
			this.labelPatient.TabIndex = 4;
			this.labelPatient.Text = "Patient";
			this.labelPatient.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// ListPatientSelect
			// 
			this.ListPatientSelect.Location = new System.Drawing.Point(8, 22);
			this.ListPatientSelect.Name = "ListPatientSelect";
			this.ListPatientSelect.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ListPatientSelect.Size = new System.Drawing.Size(170, 485);
			this.ListPatientSelect.TabIndex = 3;
			this.ListPatientSelect.SelectedIndexChanged += new System.EventHandler(this.ListPatientSelect_SelectedIndexChanged);
			// 
			// tabFilters
			// 
			this.tabFilters.Controls.Add(this.labelHelp);
			this.tabFilters.Controls.Add(this.ComboBox);
			this.tabFilters.Controls.Add(this.TextDate);
			this.tabFilters.Controls.Add(this.TextValidAge);
			this.tabFilters.Controls.Add(this.butDeleteFilter);
			this.tabFilters.Controls.Add(this.ListPrerequisites);
			this.tabFilters.Controls.Add(this.butAddFilter);
			this.tabFilters.Controls.Add(this.ListConditions);
			this.tabFilters.Controls.Add(this.TextBox);
			this.tabFilters.Controls.Add(this.DropListFilter);
			this.tabFilters.Location = new System.Drawing.Point(4, 22);
			this.tabFilters.Name = "tabFilters";
			this.tabFilters.Size = new System.Drawing.Size(832, 518);
			this.tabFilters.TabIndex = 0;
			this.tabFilters.Text = "WHERE";
			// 
			// labelHelp
			// 
			this.labelHelp.Location = new System.Drawing.Point(360, 14);
			this.labelHelp.Name = "labelHelp";
			this.labelHelp.Size = new System.Drawing.Size(262, 18);
			this.labelHelp.TabIndex = 13;
			// 
			// ComboBox
			// 
			this.ComboBox.Location = new System.Drawing.Point(360, 40);
			this.ComboBox.Name = "ComboBox";
			this.ComboBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ComboBox.Size = new System.Drawing.Size(262, 121);
			this.ComboBox.TabIndex = 12;
			this.ComboBox.Visible = false;
			this.ComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// TextDate
			// 
			this.TextDate.Location = new System.Drawing.Point(360, 40);
			this.TextDate.Name = "TextDate";
			this.TextDate.Size = new System.Drawing.Size(262, 20);
			this.TextDate.TabIndex = 11;
			this.TextDate.Text = "";
			// 
			// TextValidAge
			// 
			this.TextValidAge.Location = new System.Drawing.Point(360, 40);
			this.TextValidAge.Name = "TextValidAge";
			this.TextValidAge.Size = new System.Drawing.Size(262, 20);
			this.TextValidAge.TabIndex = 10;
			this.TextValidAge.Text = "";
			this.TextValidAge.Visible = false;
			// 
			// butDeleteFilter
			// 
			this.butDeleteFilter.Enabled = false;
			this.butDeleteFilter.Image = ((System.Drawing.Image)(resources.GetObject("butDeleteFilter.Image")));
			this.butDeleteFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDeleteFilter.Location = new System.Drawing.Point(10, 420);
			this.butDeleteFilter.Name = "butDeleteFilter";
			this.butDeleteFilter.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.butDeleteFilter.Size = new System.Drawing.Size(102, 24);
			this.butDeleteFilter.TabIndex = 8;
			this.butDeleteFilter.Text = "      Delete Row";
			this.butDeleteFilter.Click += new System.EventHandler(this.butDeleteFilter_Click);
			// 
			// ListPrerequisites
			// 
			this.ListPrerequisites.Location = new System.Drawing.Point(10, 200);
			this.ListPrerequisites.Name = "ListPrerequisites";
			this.ListPrerequisites.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ListPrerequisites.Size = new System.Drawing.Size(608, 212);
			this.ListPrerequisites.TabIndex = 7;
			this.ListPrerequisites.SelectedIndexChanged += new System.EventHandler(this.ListPrerequisites_SelectedIndexChanged);
			// 
			// butAddFilter
			// 
			this.butAddFilter.Enabled = false;
			this.butAddFilter.Location = new System.Drawing.Point(664, 40);
			this.butAddFilter.Name = "butAddFilter";
			this.butAddFilter.Size = new System.Drawing.Size(75, 24);
			this.butAddFilter.TabIndex = 6;
			this.butAddFilter.Text = "Add";
			this.butAddFilter.Click += new System.EventHandler(this.butAddFilter_Click);
			// 
			// ListConditions
			// 
			this.ListConditions.Items.AddRange(new object[] {
																												"LIKE",
																												"=",
																												">",
																												"<",
																												">=",
																												"<=",
																												"<>"});
			this.ListConditions.Location = new System.Drawing.Point(232, 40);
			this.ListConditions.Name = "ListConditions";
			this.ListConditions.Size = new System.Drawing.Size(78, 95);
			this.ListConditions.TabIndex = 5;
			// 
			// TextBox
			// 
			this.TextBox.Location = new System.Drawing.Point(360, 40);
			this.TextBox.Name = "TextBox";
			this.TextBox.Size = new System.Drawing.Size(262, 20);
			this.TextBox.TabIndex = 2;
			this.TextBox.Text = "";
			this.TextBox.Visible = false;
			// 
			// DropListFilter
			// 
			this.DropListFilter.Location = new System.Drawing.Point(8, 40);
			this.DropListFilter.MaxDropDownItems = 45;
			this.DropListFilter.Name = "DropListFilter";
			this.DropListFilter.Size = new System.Drawing.Size(172, 21);
			this.DropListFilter.TabIndex = 1;
			this.DropListFilter.Text = "WHERE";
			this.DropListFilter.SelectedIndexChanged += new System.EventHandler(this.DropListFilter_SelectedIndexChanged);
			// 
			// TextSQL
			// 
			this.TextSQL.Location = new System.Drawing.Point(18, 560);
			this.TextSQL.Multiline = true;
			this.TextSQL.Name = "TextSQL";
			this.TextSQL.ReadOnly = true;
			this.TextSQL.Size = new System.Drawing.Size(840, 128);
			this.TextSQL.TabIndex = 38;
			this.TextSQL.Text = "";
			// 
			// FormRpPatients
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(962, 700);
			this.Controls.Add(this.tabPatients);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.TextSQL);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpPatients";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Patients Report";
			this.tabPatients.ResumeLayout(false);
			this.tabData.ResumeLayout(false);
			this.tabFilters.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

    #region Fill

		private void Fill(){
			FillALpatSelect();
			FillALpatFilter();
      FillALrefToSelect();      
      FillALrefFromSelect();
      FillRefToSelectList();
      FillRefFromSelectList();
			FillPatientSelectList();
			FillFilterDropList();
		}
 
		private void FillALpatSelect(){ 
			ALpatSelect.Add(Lan.g(this,"PatNum")); 
			ALpatSelect.Add(Lan.g(this,"LName"));
			ALpatSelect.Add(Lan.g(this,"FName"));
			ALpatSelect.Add(Lan.g(this,"MiddleI")); 
      ALpatSelect.Add(Lan.g(this,"Preferred"));
      ALpatSelect.Add(Lan.g(this,"Salutation")); 
      ALpatSelect.Add(Lan.g(this,"Address")); 
      ALpatSelect.Add(Lan.g(this,"Address2"));
      ALpatSelect.Add(Lan.g(this,"City")); 
      ALpatSelect.Add(Lan.g(this,"State"));   
      ALpatSelect.Add(Lan.g(this,"Zip"));
      ALpatSelect.Add(Lan.g(this,"HmPhone"));
      ALpatSelect.Add(Lan.g(this,"WkPhone")); 
      ALpatSelect.Add(Lan.g(this,"WirelessPhone")); 
      ALpatSelect.Add(Lan.g(this,"Birthdate"));
      ALpatSelect.Add(Lan.g(this,"Email"));
      ALpatSelect.Add(Lan.g(this,"SSN"));
      ALpatSelect.Add(Lan.g(this,"Gender"));
      ALpatSelect.Add(Lan.g(this,"PatStatus"));
      ALpatSelect.Add(Lan.g(this,"Position"));  
      ALpatSelect.Add(Lan.g(this,"CreditType"));
      ALpatSelect.Add(Lan.g(this,"BillingType")); 
      ALpatSelect.Add(Lan.g(this,"ChartNumber"));   
      ALpatSelect.Add(Lan.g(this,"PriProv")); 
      ALpatSelect.Add(Lan.g(this,"SecProv"));
      ALpatSelect.Add(Lan.g(this,"FeeSched")); 
      ALpatSelect.Add(Lan.g(this,"ApptModNote"));
      ALpatSelect.Add(Lan.g(this,"AddrNote")); 
      ALpatSelect.Add(Lan.g(this,"EstBalance")); 
      ALpatSelect.Add(Lan.g(this,"FamFinUrgNote")); 
      ALpatSelect.Add(Lan.g(this,"Guarantor"));   
      ALpatSelect.Add(Lan.g(this,"ImageFolder"));
      ALpatSelect.Add(Lan.g(this,"MedUrgNote")); 
      ALpatSelect.Add(Lan.g(this,"NextAptNum")); 
      ALpatSelect.Add(Lan.g(this,"PriPlanNum"));//Primary Carrier?
      ALpatSelect.Add(Lan.g(this,"PriRelationship"));// ?
			ALpatSelect.Add(Lan.g(this,"SecPlanNum"));//Secondary Carrier? 
      ALpatSelect.Add(Lan.g(this,"SecRelationship"));// ?
			ALpatSelect.Add(Lan.g(this,"RecallInterval")); 
      ALpatSelect.Add(Lan.g(this,"RecallStatus"));  
      ALpatSelect.Add(Lan.g(this,"SchoolName")); 
      ALpatSelect.Add(Lan.g(this,"StudentStatus"));
 		}

    private void FillALrefToSelect(){
      ALrefToSelect.Add(Lan.g(this,"LName"));
      ALrefToSelect.Add(Lan.g(this,"FName"));
      ALrefToSelect.Add(Lan.g(this,"MName"));       
      ALrefToSelect.Add(Lan.g(this,"Title")); 
      ALrefToSelect.Add(Lan.g(this,"Address")); 
      ALrefToSelect.Add(Lan.g(this,"Address2"));
      ALrefToSelect.Add(Lan.g(this,"City"));
      ALrefToSelect.Add(Lan.g(this,"ST"));
      ALrefToSelect.Add(Lan.g(this,"Zip"));
      ALrefToSelect.Add(Lan.g(this,"Telephone"));
      ALrefToSelect.Add(Lan.g(this,"Phone2"));
      ALrefToSelect.Add(Lan.g(this,"Email"));
      ALrefToSelect.Add(Lan.g(this,"IsHidden")); 
      ALrefToSelect.Add(Lan.g(this,"NotPerson"));  
      ALrefToSelect.Add(Lan.g(this,"PatNum")); 
			ALrefToSelect.Add(Lan.g(this,"ReferralNum"));
      ALrefToSelect.Add(Lan.g(this,"Specialty")); 
      ALrefToSelect.Add(Lan.g(this,"SSN"));
      ALrefToSelect.Add(Lan.g(this,"UsingTIN")); 
      ALrefToSelect.Add(Lan.g(this,"Note"));

    }

    private void FillALrefFromSelect(){
      ALrefFromSelect.Add(Lan.g(this,"LName"));
      ALrefFromSelect.Add(Lan.g(this,"FName"));
      ALrefFromSelect.Add(Lan.g(this,"MName"));       
      ALrefFromSelect.Add(Lan.g(this,"Title")); 
      ALrefFromSelect.Add(Lan.g(this,"Address")); 
      ALrefFromSelect.Add(Lan.g(this,"Address2"));
      ALrefFromSelect.Add(Lan.g(this,"City"));
      ALrefFromSelect.Add(Lan.g(this,"ST"));
      ALrefFromSelect.Add(Lan.g(this,"Zip"));
      ALrefFromSelect.Add(Lan.g(this,"Telephone"));
      ALrefFromSelect.Add(Lan.g(this,"Phone2")); 
      ALrefFromSelect.Add(Lan.g(this,"Email"));
      ALrefFromSelect.Add(Lan.g(this,"IsHidden")); 
      ALrefFromSelect.Add(Lan.g(this,"NotPerson"));  
      ALrefFromSelect.Add(Lan.g(this,"PatNum")); 
			ALrefFromSelect.Add(Lan.g(this,"ReferralNum"));
      ALrefFromSelect.Add(Lan.g(this,"Specialty")); 
      ALrefFromSelect.Add(Lan.g(this,"SSN"));
      ALrefFromSelect.Add(Lan.g(this,"UsingTIN")); 
      ALrefFromSelect.Add(Lan.g(this,"Note"));
    }

		private void FillALpatFilter(){//FillALpatFilter
      ALpatFilter.Add(Lan.g(this,"Address")); 
      ALpatFilter.Add(Lan.g(this,"Address2"));
			ALpatFilter.Add(Lan.g(this,"Age"));
      ALpatFilter.Add(Lan.g(this,"ApptModNote")); 
      ALpatFilter.Add(Lan.g(this,"BillingType"));  
      ALpatFilter.Add(Lan.g(this,"Birthdate"));
			ALpatFilter.Add(Lan.g(this,"Birthday"));//new, need to add functionality
      ALpatFilter.Add(Lan.g(this,"City"));  
      ALpatFilter.Add(Lan.g(this,"ChartNumber")); 
      ALpatFilter.Add(Lan.g(this,"CreditType"));  
      ALpatFilter.Add(Lan.g(this,"Email"));
			ALpatFilter.Add(Lan.g(this,"EstBalance"));
      ALpatFilter.Add(Lan.g(this,"FamAddrNote")); 
      ALpatFilter.Add(Lan.g(this,"FamFinUrgNote")); 
      ALpatFilter.Add(Lan.g(this,"FeeSched"));
			ALpatFilter.Add(Lan.g(this,"First Visit Date"));//new, need to add functionality  
      ALpatFilter.Add(Lan.g(this,"FName"));
      ALpatFilter.Add(Lan.g(this,"Gender")); 
      ALpatFilter.Add(Lan.g(this,"HmPhone")); 
			ALpatFilter.Add(Lan.g(this,"Last Visit Date"));//new, need to add functionality 
			ALpatFilter.Add(Lan.g(this,"LName")); 
      ALpatFilter.Add(Lan.g(this,"MedUrgNote")); 
      ALpatFilter.Add(Lan.g(this,"MiddleI"));
      ALpatFilter.Add(Lan.g(this,"NextAptNum"));
			ALpatFilter.Add(Lan.g(this,"PatNum"));
      ALpatFilter.Add(Lan.g(this,"PatStatus")); 
      ALpatFilter.Add(Lan.g(this,"Position"));  
      ALpatFilter.Add(Lan.g(this,"Preferred")); 
      ALpatFilter.Add(Lan.g(this,"Primary Carrier")); 
      ALpatFilter.Add(Lan.g(this,"PriProv")); 
      ALpatFilter.Add(Lan.g(this,"PriRelationship")); 
      ALpatFilter.Add(Lan.g(this,"RecallInterval")); 
      ALpatFilter.Add(Lan.g(this,"RecallStatus"));
			ALpatFilter.Add(Lan.g(this,"Referred From Dentist"));//new, need to add functionality
			ALpatFilter.Add(Lan.g(this,"Referred From Patient"));//new, need to add functionality 
      ALpatFilter.Add(Lan.g(this,"Salutation")); 
      ALpatFilter.Add(Lan.g(this,"Secondary Carrier"));
      ALpatFilter.Add(Lan.g(this,"SecProv")); 
      ALpatFilter.Add(Lan.g(this,"SecRelationship")); 
      ALpatFilter.Add(Lan.g(this,"SchoolName")); 
      ALpatFilter.Add(Lan.g(this,"SSN")); 
      ALpatFilter.Add(Lan.g(this,"State"));
      ALpatFilter.Add(Lan.g(this,"StudentStatus"));
      ALpatFilter.Add(Lan.g(this,"WirelessPhone")); 
      ALpatFilter.Add(Lan.g(this,"WkPhone"));
      ALpatFilter.Add(Lan.g(this,"Zip"));   
		}
	  
		private void FillPatientSelectList(){
      for(int i=0;i<ALpatSelect.Count;i++){
			  ListPatientSelect.Items.Add(ALpatSelect[i]);
			}
			SQLselect="";
		}

      private void FillRefToSelectList(){
				for(int i=0;i<ALrefToSelect.Count;i++){
					ListReferredToSelect.Items.Add(ALrefToSelect[i]);
				}
					SQLselect="";
      }

      private void FillRefFromSelectList(){
				for(int i=0;i<ALrefFromSelect.Count;i++){
					ListReferredFromSelect.Items.Add(ALrefFromSelect[i]);
				}
					SQLselect="";
      }

		private void FillFilterDropList(){
      for(int i=0;i<ALpatFilter.Count;i++){
			  DropListFilter.Items.Add(ALpatFilter[i]);
			}
		}

		private void FillSQLbox(){
      TextSQL.Text=SQLselect+SQLfrom+SQLwhereRelation+SQLwhereComparison+SQLgroup;
		}

    #endregion 

    #region CreateSQL    

    private void CreateSQL(){
      GetTablesNeeded();
      CreateSQLselect();
      CreateSQLfrom();
      CreateSQLwhereRelation();
			CreateSQLwhereComparison();
      CreateSQLgroup();
      FillSQLbox();
    }

    private void GetTablesNeeded(){
      IsWhereRelation=false;
      NeedInsPlan=false;
      NeedRefDent=false;
      NeedRefPat=false;
      NeedProcLogFirst=false;
      NeedProcLogLast=false;
      for(int i=0;i<UsingInsPlans.Count;i++){
				if((bool)UsingInsPlans[i]){
					NeedInsPlan=true;
          IsWhereRelation=true;
        }
        else if((bool)UsingRefDent[i]){
					NeedRefDent=true;
          IsWhereRelation=true;
        }
        else if((bool)UsingRefPat[i]){
					NeedRefPat=true;
          IsWhereRelation=true;
        }
        else if((bool)UsingProcLogFirst[i]){
					NeedProcLogFirst=true; 
          IsWhereRelation=true;
        }
        else if((bool)UsingProcLogLast[i]){
          NeedProcLogLast=true;          
          IsWhereRelation=true;
        }
			}//end for  
    }

    private void CreateSQLselect(){
      PatSel=false;
      RefToSel=false;
      RefFromSel=false;
      SQLselect="";  

      PatFieldsSelected     = new string[ListPatientSelect.SelectedItems.Count]; 
      RefToFieldsSelected   = new string[ListReferredToSelect.SelectedItems.Count]; 
      RefFromFieldsSelected = new string[ListReferredFromSelect.SelectedItems.Count]; 
      if(ListPatientSelect.SelectedItems.Count > 0){
        PatSel=true;
				SQLselect="SELECT ";
				ListPatientSelect.SelectedItems.CopyTo(PatFieldsSelected,0);
				for(int i=0;i<PatFieldsSelected.Length;i++){
					if(i!=PatFieldsSelected.Length-1){
						SQLselect+="patient."+PatFieldsSelected[i].ToString()+",";
          }  
					else{
						SQLselect+="patient."+PatFieldsSelected[i].ToString()+" ";
          }          
				}
				butOK.Enabled=true;
			}
			else{
		    SQLselect="";
				butOK.Enabled=false;
			}
      if(ListReferredToSelect.SelectedItems.Count > 0){
        RefToSel=true;
        if(PatSel==false){
          SQLselect="SELECT ";   
        }
        else{
          SQLselect+=",";
        }
				ListReferredToSelect.SelectedItems.CopyTo(RefToFieldsSelected,0);
				for(int i=0;i<RefToFieldsSelected.Length;i++){
					if(i!=RefToFieldsSelected.Length-1){
						SQLselect+="referral."+RefToFieldsSelected[i].ToString()+" as "+RefToFieldsSelected[i].ToString()+"_1,";
          }
					else{
						SQLselect+="referral."+RefToFieldsSelected[i].ToString()+" as "+RefToFieldsSelected[i].ToString()+"_1 ";
          }
				}
				butOK.Enabled=true;
			}
      if(ListReferredFromSelect.SelectedItems.Count > 0){
        RefFromSel=true;
         if(PatSel==false && RefToSel==false){
          SQLselect="SELECT ";   
        }
        else{
          SQLselect+=",";
        }        
				ListReferredFromSelect.SelectedItems.CopyTo(RefFromFieldsSelected,0);
				for(int i=0;i<RefFromFieldsSelected.Length;i++){
					if(i!=RefFromFieldsSelected.Length-1){
						SQLselect+="referral."+RefFromFieldsSelected[i].ToString()+" as "+RefFromFieldsSelected[i].ToString()+"_2,";
          }
					else{
						SQLselect+="referral."+RefFromFieldsSelected[i].ToString()+" as "+RefFromFieldsSelected[i].ToString()+"_2 ";
          }
				}
				butOK.Enabled=true;
			}
		}
	
    private void CreateSQLfrom(){ 
      SQLfrom="";
      
      if(RefToSel || RefFromSel || NeedRefPat || NeedRefDent){
        SQLfrom="FROM patient,referral,refattach";
      }
      else{
			 SQLfrom="FROM patient";        
      }
      if(NeedInsPlan){
        SQLfrom+=",insplan";
      }
      if(NeedProcLogFirst || NeedProcLogLast){
        SQLfrom+=",procedurelog";
      }
      SQLfrom+=" ";
    }

    private void CreateSQLwhereRelation(){
      bool needAnd=false;

      if(!IsWhereRelation && !RefToSel && !RefFromSel){
        SQLwhereRelation="";
      }
      else{
        SQLwhereRelation="WHERE ";
      }
      
      if(RefToSel || RefFromSel){
        if(RefToSel){
          SQLwhereRelation+="patient.patnum=refattach.patnum && referral.referralnum=refattach.referralnum && refattach.isfrom='0' ";
        }
        else{
          SQLwhereRelation+="patient.patnum=refattach.patnum && referral.referralnum=refattach.referralnum && refattach.isfrom='1' ";
        }
        needAnd=true;
      }
			if(NeedRefPat || NeedRefDent){
  		  if(!RefToSel && !RefFromSel){
          SQLwhereRelation+="patient.patnum=refattach.patnum && refattach.referralnum=referral.referralnum ";
        }        
        needAnd=true;
      }
      if(NeedInsPlan){
        if(needAnd){
          SQLwhereRelation+="&& (patient.priplannum=insplan.plannum OR patient.secplannum=insplan.plannum) ";
        }
        else{
          SQLwhereRelation+="(patient.priplannum=insplan.plannum OR patient.secplannum=insplan.plannum) ";
        }
        needAnd=true;
      }
      if(NeedProcLogFirst || NeedProcLogLast){
        if(needAnd){
				  SQLwhereRelation+="&& procedurelog.patnum=patient.patnum ";
        }
        else{
          SQLwhereRelation+="procedurelog.patnum=patient.patnum "; 
        }
      }
    }

    private void CreateSQLwhereComparison(){
      int count=0;
      if(!IsWhereRelation && !RefToSel && !RefFromSel && ListPrerequisites.Items.Count > 0){
        SQLwhereComparison="WHERE ";
      }
      else{
        SQLwhereComparison="";
      }
			
			for(int i=0;i<ListPrerequisites.Items.Count;i++){
				if(ListPrerequisites.Items[i].ToString().Substring(0,1)=="*"){
	        
				}
				else{ 					
          if(count==0 && !IsWhereRelation){
            SQLwhereComparison+=ListPrerequisites.Items[i].ToString();
          }
					else if(ListPrerequisites.Items[i].ToString().Substring(0,2)=="OR"){
						SQLwhereComparison+=" "+ListPrerequisites.Items[i].ToString();
					}
					else{
						SQLwhereComparison+=" AND "+ListPrerequisites.Items[i].ToString();
					}
          count++;
        }
			}
			if(ListPatientSelect.SelectedItems.Count > 0 || ListReferredToSelect.SelectedItems.Count > 0 || ListReferredFromSelect.SelectedItems.Count > 0){
				butOK.Enabled=true;
      }
		}

    private void CreateSQLgroup(){
			if(NeedProcLogLast && !NeedProcLogFirst){
				SQLgroup=" GROUP BY procedurelog.patnum HAVING "+ProcLogLastDate;
      }
			else if(NeedProcLogLast && NeedProcLogFirst){
				SQLgroup=" GROUP BY procedurelog.patnum HAVING "+ProcLogLastDate+" AND "+ProcLogFirstDate;
      }
      else if(NeedProcLogFirst && !NeedProcLogLast){
        SQLgroup=" GROUP BY procedurelog.patnum HAVING "+ProcLogFirstDate;
      }
      else{
        SQLgroup="";
      }       
    }

    #endregion

    #region SetConditions

		private void SetListBoxConditions(){
			ComboBox.Visible=true;
			TextDate.Visible=false;
      TextBox.Visible=false;
			TextValidAge.Visible=false;
			IsDropDown=true;
			IsDate=false;					
			IsText=false;
			ListConditions.Enabled=true;
			ComboBox.SelectedIndex=-1;
			butAddFilter.Enabled=false;
      labelHelp.Visible=false;
		}

    private void SetTextBoxConditions(){
			TextBox.Clear();
			ListConditions.Enabled=true;
      TextBox.Visible=true;
			TextDate.Visible=false;
			ComboBox.Visible=false;
			TextValidAge.Visible=false;
			TextBox.Select();
			IsText=true;
			IsDate=false;
			IsDropDown=false;
    	butAddFilter.Enabled=true;
      labelHelp.Visible=false;
    }

    private void SetDateConditions(){
      TextDate.Visible=true;
      TextBox.Visible=false;
			ComboBox.Visible=false;
			TextValidAge.Visible=false;
			IsDate=true;					
			IsText=false;
			IsDropDown=false;
			TextDate.Clear();
			TextDate.Select();
			ListConditions.Enabled=true;
			butAddFilter.Enabled=true;
      labelHelp.Visible=true;
    }

    #endregion

    #region Selected Index Changes

    private void DropListFilter_SelectedIndexChanged(object sender, System.EventArgs e) {
			switch(DropListFilter.SelectedItem.ToString()){
   		  case "Address":
   		  case "Address2":
   		  case "ApptModNote":
   		  case "ChartNumber":
   		  case "City":
   		  case "CreditType":
   		  case "Email":
				case "EstBalance":
   		  case "FamAddrNote":
   		  case "FamFinUrgNote":
        case "FName":
   		  case "HmPhone":
  		  case "LName":
   		  case "MedUrgNote":
   		  case "MiddleI":
   		  case "NextAptNum":
				case "PatNum":
   		  case "Preferred":
   		  case "RecallInterval":
				case "Salutation":
   		  case "SchoolName":
   		  case "State":
   		  case "StudentStatus":
   		  case "WirelessPhone":
   		  case "WkPhone":
   		  case "Zip":
          SetTextBoxConditions();
 					break;
   		  case "SSN":
          SetTextBoxConditions();
          labelHelp.Visible=true;
          labelHelp.Text="Type in SSN as 123456789";
          break;
				case "Primary Carrier":
				case "Secondary Carrier":
          SetTextBoxConditions();
          labelHelp.Visible=true;
          labelHelp.Text="Type in Name of Insurance Company";
          break;
        case "Referred From Dentist":
          labelHelp.Visible=true;
          SetTextBoxConditions();
          labelHelp.Text="Type in last name of dentist"; 
 					break;    		
        case "Referred From Patient":
          SetTextBoxConditions();
          labelHelp.Visible=true;
          labelHelp.Text="Type in last name of patient"; 
 					break; 
        case "Age":
					TextValidAge.Clear();
			    ListConditions.Enabled=true;
          TextBox.Visible=false;
					TextDate.Visible=false;
					ComboBox.Visible=false;
					TextValidAge.Visible=true;
					TextValidAge.Select();
					IsText=false;
					IsDate=false;
					IsDropDown=false;
					butAddFilter.Enabled=true;
          labelHelp.Text="Please Input a number"; 
					break;
   	    case "Birthdate":
    		case "Last Visit Date":
    		case "First Visit Date":
          SetDateConditions();
          labelHelp.Text="Type Date as dd/mm/yyyy"; 
 					break;
        case "Birthday":
          SetDateConditions();
          labelHelp.Text="Type Date as dd/mm"; 
 					break; 
   		  case "PatStatus":
          SetListBoxConditions();
          ComboBox.Items.Clear();
					ComboBox.Items.Add("Patient");
					ComboBox.Items.Add("NonPatient");
					ComboBox.Items.Add("Inactive");
					ComboBox.Items.Add("Archived");
					ComboBox.Items.Add("Deleted");
					break;
   		  case "Gender":
          SetListBoxConditions();
					ComboBox.Items.Clear();
					ComboBox.Items.Add("Male");
					ComboBox.Items.Add("Female");
					ComboBox.Items.Add("Unknown");
					break;
   		  case "Position":
          SetListBoxConditions();
					ListConditions.SelectedIndex=1;
					ListConditions.Enabled=false;
					ComboBox.Items.Clear();
					ComboBox.Items.Add("Single");
					ComboBox.Items.Add("Married");
					ComboBox.Items.Add("Child");
					break;
   		  case "FeeSched":
          SetListBoxConditions();
					ComboBox.Items.Clear();
          for(int i=0;i<Defs.Long[(int)DefCat.FeeSchedNames].Length;i++){
						sItem=Defs.Long[(int)DefCat.FeeSchedNames][i].ItemName.ToString();
						if(Defs.Long[(int)DefCat.FeeSchedNames][i].IsHidden)
							sItem+="(hidden)";
            ComboBox.Items.Add(sItem);
					}
					break;
   		  case "BillingType":
          SetListBoxConditions();
					ComboBox.Items.Clear();
          for(int i=0;i<Defs.Long[(int)DefCat.BillingTypes].Length;i++){
						sItem=Defs.Long[(int)DefCat.BillingTypes][i].ItemName.ToString();
						if(Defs.Long[(int)DefCat.BillingTypes][i].IsHidden)
							sItem+="(hidden)";
            ComboBox.Items.Add(sItem);
					}
					break;
   		  case "RecallStatus":
          SetListBoxConditions();
					ComboBox.Items.Clear();
          for(int i=0;i<Defs.Long[(int)DefCat.RecallUnschedStatus	].Length;i++){
						sItem=Defs.Long[(int)DefCat.RecallUnschedStatus][i].ItemName.ToString();
						if(Defs.Long[(int)DefCat.RecallUnschedStatus][i].IsHidden)
							sItem+="(hidden)";
            ComboBox.Items.Add(sItem);
					}
					break;
        case "PriProv":		
        case "SecProv":
          SetListBoxConditions();
					ComboBox.Items.Clear();
          for(int i=0;i<Providers.ListLong.Length;i++){
						sItem=Providers.ListLong[i].LName+", "
							+Providers.ListLong[i].MI+" "+Providers.ListLong[i].FName;
						if(Providers.ListLong[i].IsHidden)
							sItem+="(hidden)";
            ComboBox.Items.Add(sItem);
					}	
					break;
        case "PriRelationship": 
				case "SecRelationship":
          SetListBoxConditions();
					ComboBox.Items.Clear();
					ComboBox.Items.Add("Self");
					ComboBox.Items.Add("Spouse");
					ComboBox.Items.Add("Child");	
					ComboBox.Items.Add("Employee");
					ComboBox.Items.Add("HandicapDep");
					ComboBox.Items.Add("SignifOther");	
					ComboBox.Items.Add("InjuredPlantiff");
					ComboBox.Items.Add("LifePartner");
					ComboBox.Items.Add("Dependent");
					break;
			}
		}

		private void ComboBox_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(ComboBox.SelectedItems.Count > 0)
				butAddFilter.Enabled=true;
			else
				butAddFilter.Enabled=false;
		}

		private void ListPrerequisites_SelectedIndexChanged(object sender, System.EventArgs e) {
			butDeleteFilter.Enabled=false;
			if(ListPrerequisites.Items.Count > 0 && ListPrerequisites.SelectedItems.Count > 0){
				butDeleteFilter.Enabled=true;
			}
		}

		private void ListPatientSelect_SelectedIndexChanged(object sender, System.EventArgs e) {
		  CreateSQL();
		}

		private void ListReferredToSelect_SelectedIndexChanged(object sender, System.EventArgs e) {
		  CreateSQL();		
		}

		private void ListReferredFromSelect_SelectedIndexChanged(object sender, System.EventArgs e) {
		  CreateSQL();		
		}

    #endregion

    #region Button Clicks

		private void butAddFilter_Click(object sender, System.EventArgs e) {
			if(  TextDate.errorProvider1.GetError(TextDate)!=""
				  || TextValidAge.errorProvider1.GetError(TextValidAge)!=""
          || (TextValidAge.Text=="" &&  DropListFilter.SelectedItem.ToString()=="Age")
          || (TextDate.Text=="" &&  IsDate)        
				){
				MessageBox.Show("Please fix data entry errors first.");
				return;
			}
      UsingInsPlans.Add(false);
      UsingRefDent.Add(false);
      UsingRefPat.Add(false);
      UsingProcLogFirst.Add(false);
      UsingProcLogLast.Add(false);      

			if(IsText){
				if(DropListFilter.SelectedItem.ToString()=="Primary Carrier"){
					if(ListConditions.SelectedIndex==0){
					//spaces around = are necessary
						ListPrerequisites.Items.Add("insplan.Carrier LIKE '%"+TextBox.Text+"%'");
					}
 					else{
					  ListPrerequisites.Items.Add("insplan.Carrier "+ListConditions.SelectedItem.ToString()+" '"+TextBox.Text+"'"); 
					}
          UsingInsPlans[UsingInsPlans.Count-1]=true;
				}//end if(DropListFilter.SelectedItem.ToString()=="Primary Carrier")
				else if(DropListFilter.SelectedItem.ToString()=="Secondary Carrier"){
					if(ListConditions.SelectedIndex==0){
					  ListPrerequisites.Items.Add("insplan.Carrier LIKE '%"+TextBox.Text+"%'");    
					}
					else{
  					 ListPrerequisites.Items.Add("insplan.Carrier "
						   +ListConditions.SelectedItem.ToString()+" '"+TextBox.Text+"'"); 
					}
          UsingInsPlans[UsingInsPlans.Count-1]=true;
				}//	end	else if(DropListFilter.SelectedItem.ToString()=="Secondary Carrier"){
				else if(DropListFilter.SelectedItem.ToString()=="Referred From Dentist"){
					if(ListConditions.SelectedIndex==0){
					  ListPrerequisites.Items.Add("referral.patnum=0 && referral.LName LIKE '%" +TextBox.Text+"%'");    
					}
					else{
  					 ListPrerequisites.Items.Add("referral.patnum=0 && referral.LName "+ListConditions.SelectedItem.ToString()
              +" '"+TextBox.Text+"'"); 
					}
          UsingRefDent[UsingInsPlans.Count-1]=true;
        }
        else if(DropListFilter.SelectedItem.ToString()=="Referred From Patient"){
					if(ListConditions.SelectedIndex==0){
					  ListPrerequisites.Items.Add("referral.patnum > '0' && referral.LName LIKE '%" +TextBox.Text+"%'");    
					}
					else{
  					 ListPrerequisites.Items.Add("referral.patnum > '0' && referral.LName "+ListConditions.SelectedItem.ToString()
              +" '"+TextBox.Text+"'"); 
					}
          UsingRefPat[UsingInsPlans.Count-1]=true;
        }
				else{
					if(ListConditions.SelectedIndex==0){
					  ListPrerequisites.Items.Add
							("patient."+DropListFilter.SelectedItem.ToString()+" LIKE '%"+TextBox.Text+"%'");
					}
					else{
						ListPrerequisites.Items.Add("patient."+DropListFilter.SelectedItem.ToString()+" "
							+ListConditions.SelectedItem.ToString()+" '"+TextBox.Text+"'");
					}
        } 
  		}//end if(isText)
 			else if(DropListFilter.SelectedItem.ToString()=="Age"){
        if(ListConditions.SelectedIndex==0){
         ListPrerequisites.Items.Add("patient.BirthDate LIKE '%"
					 +DateTime.Now.AddYears(-Convert.ToInt32(TextValidAge.Text)).ToString("yyyy-MM-dd")+"%'"); 
				}
				else if(ListConditions.SelectedItem.ToString()=="<"){
          ListPrerequisites.Items.Add("patient.Birthdate > '"
						+DateTime.Now.AddYears(-Convert.ToInt32(TextValidAge.Text)).ToString("yyyy-MM-dd")+"'");
				}
				else if(ListConditions.SelectedItem.ToString()==">"){
          ListPrerequisites.Items.Add("patient.Birthdate < '"
						+DateTime.Now.AddYears(-Convert.ToInt32(TextValidAge.Text)).ToString("yyyy-MM-dd")+"'");
				}
				else if(ListConditions.SelectedItem.ToString()=="<="){
          ListPrerequisites.Items.Add("patient.Birthdate >= '"
						+DateTime.Now.AddYears(-Convert.ToInt32(TextValidAge.Text)).ToString("yyyy-MM-dd")+"'");
				}
				else if(ListConditions.SelectedItem.ToString()==">="){
          ListPrerequisites.Items.Add("patient.Birthdate <= '"
						+DateTime.Now.AddYears(-Convert.ToInt32(TextValidAge.Text)).ToString("yyyy-MM-dd")+"'");
				}
				else{
          ListPrerequisites.Items.Add("patient.Birthdate "+ListConditions.SelectedItem.ToString()+" '"
						+DateTime.Now.AddYears(-Convert.ToInt32(TextValidAge.Text)).ToString("yyyy-MM-dd")+"'");
				}
			}
 			else if(IsDate){
        if(DropListFilter.SelectedItem.ToString()=="First Visit Date"){
 					if(ListConditions.SelectedIndex==0){
					  ListPrerequisites.Items.Add("*HAVING MIN(procdate) LIKE '%"+POut.PDate(DateTime.Parse(TextDate.Text))+"%'");   
            ProcLogFirstDate="MIN(procdate) LIKE '%"+POut.PDate(DateTime.Parse(TextDate.Text))+"%'";
					}
					else{
  					ListPrerequisites.Items.Add("*HAVING MIN(procdate) "+ListConditions.SelectedItem.ToString()
              +" '"+POut.PDate(DateTime.Parse(TextDate.Text))+"'"); 
            ProcLogFirstDate="MIN(procdate) "+ListConditions.SelectedItem.ToString()
              +" '"+POut.PDate(DateTime.Parse(TextDate.Text))+"'";
					}          
          UsingProcLogFirst[UsingInsPlans.Count-1]=true;
        }
        else if(DropListFilter.SelectedItem.ToString()=="Last Visit Date"){
          if(ListConditions.SelectedIndex==0){
					  ListPrerequisites.Items.Add("*HAVING MAX(procdate) LIKE '%"+POut.PDate(DateTime.Parse(TextDate.Text))+"%'"); 
            ProcLogLastDate="MAX(procdate) LIKE '%"+POut.PDate(DateTime.Parse(TextDate.Text))+"%'";
					}
					else{
  					 ListPrerequisites.Items.Add("*HAVING MAX(procdate) "+ListConditions.SelectedItem.ToString()
               +" '"+POut.PDate(DateTime.Parse(TextDate.Text))+"'"); 
             ProcLogLastDate="MAX(procdate) "+ListConditions.SelectedItem.ToString()
               +" '"+POut.PDate(DateTime.Parse(TextDate.Text))+"'"; 
					}
          UsingProcLogLast[UsingInsPlans.Count-1]=true;
        }
        else if(DropListFilter.SelectedItem.ToString()=="Birthday"){
          if(ListConditions.SelectedIndex==0){
					  ListPrerequisites.Items.Add("MONTH(Birthdate) LIKE '%"+POut.PDate(DateTime.Parse(TextDate.Text))
              +"%' && DAYOFMONTH(Birthdate) LIKE '%"+POut.PDate(DateTime.Parse(TextDate.Text))+"%' ");    
					}
					else{
  					 ListPrerequisites.Items.Add("MONTH(Birthdate) "+ListConditions.SelectedItem.ToString()+" '"
             +POut.PDate(DateTime.Parse(TextDate.Text))+"' && DAYOFMONTH(Birthdate) "+ListConditions.SelectedItem.ToString()+" '"
             +POut.PDate(DateTime.Parse(TextDate.Text))+"' "); 
					}
        }
        else{  
					if(ListConditions.SelectedIndex==0){
					ListPrerequisites.Items.Add(DropListFilter.SelectedItem.ToString()
						+" Like '%"+POut.PDate(DateTime.Parse(TextDate.Text))+"%'"); 
					}
					else{
						ListPrerequisites.Items.Add(DropListFilter.SelectedItem.ToString()+" "
							+ListConditions.SelectedItem.ToString()+" '"+POut.PDate(DateTime.Parse(TextDate.Text))+"'");      
				  }
        }
			}//end else if(isDate)
			else if(IsDropDown){
				if(DropListFilter.SelectedItem.ToString()=="FeeSched"){
					sItem="";
					for(int i=0;i<ComboBox.SelectedIndices.Count;i++){
						if(i==0){
              sItem="(";
            }
						else{ 
              sItem="OR ";
            }  
						sItem+="patient.FeeSched "+ListConditions.SelectedItem.ToString()+" '"
							+Defs.Long[(int)DefCat.FeeSchedNames][ComboBox.SelectedIndices[i]].DefNum.ToString()+"'"; 
						if(i==ComboBox.SelectedIndices.Count-1){
							sItem+=")";
            }  
						ListPrerequisites.Items.Add(sItem);
					}
				}//end if
				else if(DropListFilter.SelectedItem.ToString()=="BillingType"){
					sItem="";
					for(int i=0;i<ComboBox.SelectedIndices.Count;i++){
						if(i==0){ 
              sItem="(";
            }
						else{ 
              sItem="OR ";
            }
						sItem+="patient.BillingType "+ListConditions.SelectedItem.ToString()+" '"
							+Defs.Long[(int)DefCat.BillingTypes][ComboBox.SelectedIndices[i]].DefNum.ToString()+"'"; 
						if(i==ComboBox.SelectedIndices.Count-1){
							sItem+=")";
            }
						ListPrerequisites.Items.Add(sItem);
					}
				}
				else if(DropListFilter.SelectedItem.ToString()=="RecallStatus"){
					sItem="";
					for(int i=0;i<ComboBox.SelectedIndices.Count;i++){
						if(i==0){ 
              sItem="(";
            } 
						else{ 
              sItem="OR ";
            }
						sItem+="patient.RecallStatus "+ListConditions.SelectedItem.ToString()+" '"
							+Defs.Long[(int)DefCat.RecallUnschedStatus][ComboBox.SelectedIndices[i]]
							.DefNum.ToString()+"'"; 
						if(i==ComboBox.SelectedIndices.Count-1){
							sItem+=")";
            } 
						ListPrerequisites.Items.Add(sItem);
					}
				}
				else if(DropListFilter.SelectedItem.ToString()=="PriProv"){
					sItem="";
					for(int i=0;i<ComboBox.SelectedIndices.Count;i++){
						if(i==0){ 
              sItem="(";
            }  
						else{ 
              sItem="OR ";
            }
						sItem+="patient.PriProv "+ListConditions.SelectedItem.ToString()+" '"
							+Providers.ListLong[ComboBox.SelectedIndices[i]].ProvNum.ToString()+"'"; 
						if(i==ComboBox.SelectedIndices.Count-1){
							sItem+=")";
            }
						ListPrerequisites.Items.Add(sItem);
					}
				}
				else if(DropListFilter.SelectedItem.ToString()=="SecProv"){
					sItem="";
					for(int i=0;i<ComboBox.SelectedIndices.Count;i++){
						if(i==0){
              sItem="(";
            } 
						else{ 
              sItem="OR ";
            } 
						sItem+="patient.SecProv "+ListConditions.SelectedItem.ToString()+" '"
							+Providers.ListLong[ComboBox.SelectedIndices[i]].ProvNum.ToString()+"'"; 
						if(i==ComboBox.SelectedIndices.Count-1){  
							sItem+=")";
            }  
						ListPrerequisites.Items.Add(sItem);
					}
				}
				else{
					//PatStatus
					//Gender
					//Position
					//PriRelationship
					//SecRelationship
          for(int i=0;i<ComboBox.SelectedItems.Count;i++){
						if(ListConditions.SelectedIndex==0){ 
							ListPrerequisites.Items.Add(DropListFilter.SelectedItem.ToString()+" LIKE '%"
								+ComboBox.SelectedIndices[i].ToString()+"%'");  
						}
						else{
							ListPrerequisites.Items.Add(DropListFilter.SelectedItem.ToString()+" "
								+ListConditions.SelectedItem.ToString()+" '"
								+ComboBox.SelectedIndices[i].ToString()+"'");   
						}
					}//end for
 				}
				ComboBox.SelectedIndex=-1;
				butAddFilter.Enabled=false;
			}//end else if(isDropDown)
      CreateSQL();
      FillSQLbox();
			ListConditions.Enabled=true;
			TextBox.Clear();
			TextDate.Clear();
			TextValidAge.Clear();
		}

		private void butDeleteFilter_Click(object sender, System.EventArgs e){ 
      while(ListPrerequisites.SelectedIndices.Count > 0){ 
        UsingInsPlans.RemoveAt(ListPrerequisites.SelectedIndices[0]);
        UsingRefDent.RemoveAt(ListPrerequisites.SelectedIndices[0]);
        UsingRefPat.RemoveAt(ListPrerequisites.SelectedIndices[0]);
        UsingProcLogFirst.RemoveAt(ListPrerequisites.SelectedIndices[0]);
        UsingProcLogLast.RemoveAt(ListPrerequisites.SelectedIndices[0]);
				ListPrerequisites.Items.RemoveAt(ListPrerequisites.SelectedIndices[0]);
			}
      CreateSQL();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Queries.CurReport=new Report();
			Queries.CurReport.Query=TextSQL.Text;
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=false;
			FormQuery2.SubmitQuery();	
      FormQuery2.textQuery.Text=Queries.CurReport.Query;					
			FormQuery2.ShowDialog();
		}

    #endregion

		private void butCancel_Click(object sender, System.EventArgs e) {
		
		}

	}
}
