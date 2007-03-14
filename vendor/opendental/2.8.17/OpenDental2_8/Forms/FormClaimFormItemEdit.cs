using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormClaimFormItemEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelImageFileName;
		private System.Windows.Forms.TextBox textImageFileName;
		private System.Windows.Forms.Label labelFieldName;
		private System.Windows.Forms.ListBox listFieldName;
		private System.Windows.Forms.TextBox textFormatString;
		private System.Windows.Forms.Label labelFormatString;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public string[] FieldNames;
		private System.Windows.Forms.Button butDelete;
		///<summary></summary>
		public bool IsNew;

		///<summary></summary>
		public FormClaimFormItemEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
			});
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimFormItemEdit));
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.labelImageFileName = new System.Windows.Forms.Label();
			this.textImageFileName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.labelFieldName = new System.Windows.Forms.Label();
			this.listFieldName = new System.Windows.Forms.ListBox();
			this.textFormatString = new System.Windows.Forms.TextBox();
			this.labelFormatString = new System.Windows.Forms.Label();
			this.butDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(838, 605);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(838, 564);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// labelImageFileName
			// 
			this.labelImageFileName.Location = new System.Drawing.Point(25, 22);
			this.labelImageFileName.Name = "labelImageFileName";
			this.labelImageFileName.Size = new System.Drawing.Size(156, 16);
			this.labelImageFileName.TabIndex = 2;
			this.labelImageFileName.Text = "Image File Name";
			// 
			// textImageFileName
			// 
			this.textImageFileName.Location = new System.Drawing.Point(26, 40);
			this.textImageFileName.Name = "textImageFileName";
			this.textImageFileName.Size = new System.Drawing.Size(211, 20);
			this.textImageFileName.TabIndex = 3;
			this.textImageFileName.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(25, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(209, 57);
			this.label2.TabIndex = 4;
			this.label2.Text = "This file must be present in the OpenDentalData folder.  It should be a jpg, gif," +
				" or emf.";
			// 
			// labelFieldName
			// 
			this.labelFieldName.Location = new System.Drawing.Point(255, 14);
			this.labelFieldName.Name = "labelFieldName";
			this.labelFieldName.Size = new System.Drawing.Size(156, 16);
			this.labelFieldName.TabIndex = 5;
			this.labelFieldName.Text = "or Field Name";
			// 
			// listFieldName
			// 
			this.listFieldName.Location = new System.Drawing.Point(254, 31);
			this.listFieldName.MultiColumn = true;
			this.listFieldName.Name = "listFieldName";
			this.listFieldName.Size = new System.Drawing.Size(560, 602);
			this.listFieldName.TabIndex = 6;
			this.listFieldName.DoubleClick += new System.EventHandler(this.listFieldName_DoubleClick);
			// 
			// textFormatString
			// 
			this.textFormatString.Location = new System.Drawing.Point(24, 208);
			this.textFormatString.Name = "textFormatString";
			this.textFormatString.Size = new System.Drawing.Size(211, 20);
			this.textFormatString.TabIndex = 8;
			this.textFormatString.Text = "";
			// 
			// labelFormatString
			// 
			this.labelFormatString.Location = new System.Drawing.Point(24, 135);
			this.labelFormatString.Name = "labelFormatString";
			this.labelFormatString.Size = new System.Drawing.Size(210, 68);
			this.labelFormatString.TabIndex = 7;
			this.labelFormatString.Text = "Format String.  All dates must have a format.  Valid entries would include MM/dd/" +
				"yyyy, MM-dd-yy, and M d y as examples.";
			this.labelFormatString.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butDelete
			// 
			this.butDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDelete.Location = new System.Drawing.Point(29, 600);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(80, 23);
			this.butDelete.TabIndex = 9;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormClaimFormItemEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(939, 646);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textFormatString);
			this.Controls.Add(this.textImageFileName);
			this.Controls.Add(this.labelFormatString);
			this.Controls.Add(this.listFieldName);
			this.Controls.Add(this.labelFieldName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelImageFileName);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimFormItemEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Claim Form Item";
			this.Load += new System.EventHandler(this.FormClaimFormItemEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimFormItemEdit_Load(object sender, System.EventArgs e) {
			FillFieldNames();
			FillForm();
		}

		///<summary>This is called externally from Renaissance to error check each of the supplied fieldNames</summary>
		public void FillFieldNames(){
			FieldNames=new string[]
			{
				"IsPreAuth",
        "IsStandardClaim",
				"IsMedicaidClaim",
				"PreAuthString",
				"PriInsCarrierName",
				"PriInsAddress",
				"PriInsAddress2",
				"PriInsAddressComplete",
				"PriInsCity",
				"PriInsST",
				"PriInsZip",
				"OtherInsExists",
				"OtherInsNotExists",
				"OtherInsSubscrLastFirst",
				"OtherInsSubscrDOB",
				"OtherInsSubscrIsMale",
				"OtherInsSubscrIsFemale",
				"OtherInsSubscrID",
				"OtherInsGroupNum",
				"OtherInsRelatIsSelf",
				"OtherInsRelatIsSpouse",
				"OtherInsRelatIsChild",
				"OtherInsRelatIsOther",
				"OtherInsCarrierName",
				"OtherInsAddress",
				"OtherInsCity",
				"OtherInsST",
				"OtherInsZip",
				"SubscrLastFirst",
				"SubscrAddress",
				"SubscrAddress2",
				"SubscrAddressComplete",
				"SubscrCity",
				"SubscrST",
				"SubscrZip",
				"SubscrPhone",
				"SubscrDOB",
				"SubscrIsMale",
				"SubscrIsFemale",
				"SubscrIsMarried",
				"SubscrIsSingle",
				"SubscrID",
				"SubscrIsFTStudent",
				"SubscrIsPTStudent",
				"GroupNum",
				"EmployerName",
				"RelatIsSelf",
				"RelatIsSpouse",
				"RelatIsChild",
				"RelatIsOther",
				"IsFTStudent",
				"IsPTStudent",
				"PatientLastFirst",
				"PatientAddress",
				"PatientAddress2",
				"PatientAddressComplete",
				"PatientCity",
				"PatientST",
				"PatientZip",
				"PatientPhone",
				"PatientDOB",
				"PatientIsMale",
				"PatientIsFemale",
				"PatientIsMarried",
				"PatientIsSingle",
				"PatientSSN",
				"PatientMedicaidID",
				"PatientID-MedicaidOrSSN",
				"P1Date",
				"P1Area",
				"P1System",
				"P1ToothNumber",
				"P1Surface",
				"P1Code",
				"P1Description",
				"P1TreatDentMedicaidID",
				"P1Fee",
				"P2Date",
				"P2Area",
				"P2System",
				"P2ToothNumber",
				"P2Surface",
				"P2Code",
				"P2Description",
				"P2Fee",
				"P2TreatDentMedicaidID",
				"P3Date",
				"P3Area",
				"P3System",
				"P3ToothNumber",
				"P3Surface",
				"P3Code",
				"P3Description",
				"P3Fee",
				"P3TreatDentMedicaidID",
				"P4Date",
				"P4Area",
				"P4System",
				"P4ToothNumber",
				"P4Surface",
				"P4Code",
				"P4Description",
				"P4Fee",
				"P4TreatDentMedicaidID",
				"P5Date",
				"P5Area",
				"P5System",
				"P5ToothNumber",
				"P5Surface",
				"P5Code",
				"P5Description",
				"P5Fee",
				"P5TreatDentMedicaidID",
				"P6Date",
				"P6Area",
				"P6System",
				"P6ToothNumber",
				"P6Surface",
				"P6Code",
				"P6Description",
				"P6Fee",
				"P6TreatDentMedicaidID",
				"P7Date",
				"P7Area",
				"P7System",
				"P7ToothNumber",
				"P7Surface",
				"P7Code",
				"P7Description",
				"P7Fee",
				"P7TreatDentMedicaidID",
				"P8Date",
				"P8Area",
				"P8System",
				"P8ToothNumber",
				"P8Surface",
				"P8Code",
				"P8Description",
				"P8Fee",
				"P8TreatDentMedicaidID",
				"P9Date",
				"P9Area",
				"P9System",
				"P9ToothNumber",
				"P9Surface",
				"P9Code",
				"P9Description",
				"P9Fee",
				"P9TreatDentMedicaidID",
				"P10Date",
				"P10Area",
				"P10System",
				"P10ToothNumber",
				"P10Surface",
				"P10Code",
				"P10Description",
				"P10Fee",
				"P10TreatDentMedicaidID",
				"TotalFee",
				"Miss1",
				"Miss2",
				"Miss3",
				"Miss4",
				"Miss5",
				"Miss6",
				"Miss7",
				"Miss8",
				"Miss9",
				"Miss10",
				"Miss11",
				"Miss12",
				"Miss13",
				"Miss14",
				"Miss15",
				"Miss16",
				"Miss17",
				"Miss18",
				"Miss19",
				"Miss20",
				"Miss21",
				"Miss22",
				"Miss23",
				"Miss24",
				"Miss25",
				"Miss26",
				"Miss27",
				"Miss28",
				"Miss29",
				"Miss30",
				"Miss31",
				"Miss32",
				"Remarks",
				"PatientRelease",
				"PatientReleaseDate",
				"PatientAssignment",
				"PatientAssignmentDate",
				"PlaceIsOffice",
				"PlaceIsHospADA2002",
				"PlaceIsExtCareFacilityADA2002",
				"PlaceIsOtherADA2002",
				"PlaceIsInpatHosp",
				"PlaceIsOutpatHosp",
				"PlaceIsAdultLivCareFac",
				"PlaceIsSkilledNursFac",
				"PlaceIsPatientsHome",
				"PlaceIsOtherLocation",
				"PlaceNumericCode",
				"IsRadiographsAttached",
				"RadiographsNumAttached",
				"RadiographsNotAttached",
				//"ImagesEnclosed",
				//"ModelsEnclosed",
				"IsNotOrtho",
				"IsOrtho",
				"DateOrthoPlaced",
				"MonthsOrthoRemaining",
				"IsNotProsth",
				"IsInitialProsth",
				"IsNotReplacementProsth",
				"IsReplacementProsth",
				"DatePriorProsthPlaced",
				//reason for replacement (ADA2000)
				"IsOccupational",
				"IsNotOccupational",
				"IsAutoAccident",
				"IsNotAutoAccident",
				"IsOtherAccident",
				"IsNotOtherAccident",
				"IsNotAccident",
				"AccidentDate",
				"AccidentST",
				"BillingDentist",
				"BillingDentistAddress",
				"BillingDentistAddress2",
				"BillingDentistCity",
				"BillingDentistST",
				"BillingDentistZip",
				"BillingDentistMedicaidID",
				//"BillingDentistProvID",
				"BillingDentistLicenseNum",
				"BillingDentistSSNorTIN",
				"BillingDentistNumIsSSN",
				"BillingDentistNumIsTIN",
				"BillingDentistPh123",
				"BillingDentistPh456",
				"BillingDentistPh78910",
				"BillingDentistPhoneFormatted",
				"TreatingDentistSignature",
				"TreatingDentistSigDate",
				"DateService",
				"TreatingDentistMedicaidID",
				//"TreatingDentistProvID",
				"TreatingDentistLicense",
				"TreatingDentistAddress",
				"TreatingDentistCity",
				"TreatingDentistST",
				"TreatingDentistZip",
				"TreatingDentistPh123",
				"TreatingDentistPh456",
				"TreatingDentistPh78910",
				"TreatingProviderSpecialty"
			};
		}

		private void FillForm(){
			textImageFileName.Text=ClaimFormItems.Cur.ImageFileName;
			textFormatString.Text=ClaimFormItems.Cur.FormatString;
			listFieldName.Items.Clear();
			for(int i=0;i<FieldNames.Length;i++){
				listFieldName.Items.Add(FieldNames[i]);
				if(FieldNames[i]==ClaimFormItems.Cur.FieldName){
					listFieldName.SelectedIndex=i;
				}
			}
		}

		private void listFieldName_DoubleClick(object sender, System.EventArgs e) {
			SaveAndClose();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			ClaimFormItems.DeleteCur();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			SaveAndClose();
		}

		private void SaveAndClose(){
			ClaimFormItems.Cur.ImageFileName=textImageFileName.Text;
			ClaimFormItems.Cur.FormatString=textFormatString.Text;
			if(listFieldName.SelectedIndex==-1){
				ClaimFormItems.Cur.FieldName="";
			}
			else{
				ClaimFormItems.Cur.FieldName=FieldNames[listFieldName.SelectedIndex];
			}
			if(IsNew)
				ClaimFormItems.InsertCur();
			else
				ClaimFormItems.UpdateCur();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		

		

		


	}
}





















