using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormClaimPrint : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.PrintPreviewControl Preview2;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.Button butPrint;
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public int ThisClaimNum;
		///<summary></summary>
		public int ThisPatNum;
		///<summary>This will be 0 unless the user is trying to print a batch e-claim with a predefined ClaimForm.</summary>
		public int ClaimFormNum;
		///<summary></summary>
		public bool PrintBlank;
		private System.Windows.Forms.PrintDialog printDialog2;
		///<summary></summary>
		public bool PrintImmediately;
    private string[] displayStrings;
		private ArrayList claimprocs;
		///<summary>For batch generic e-claims, this just prints the data and not the background.</summary>
		public bool HideBackground;
		private System.Windows.Forms.Label labelTotPages;
		private OpenDental.XPButton butBack;
		private OpenDental.XPButton butFwd;
		private int pagesPrinted;
		private int totalPages;

		///<summary></summary>
		public FormClaimPrint(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,
				butPrint,
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormClaimPrint));
			this.butClose = new System.Windows.Forms.Button();
			this.Preview2 = new System.Windows.Forms.PrintPreviewControl();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.butPrint = new System.Windows.Forms.Button();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.labelTotPages = new System.Windows.Forms.Label();
			this.butBack = new OpenDental.XPButton();
			this.butFwd = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(770, 768);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// Preview2
			// 
			this.Preview2.AutoZoom = false;
			this.Preview2.Location = new System.Drawing.Point(0, 0);
			this.Preview2.Name = "Preview2";
			this.Preview2.Size = new System.Drawing.Size(738, 798);
			this.Preview2.TabIndex = 1;
			this.Preview2.Zoom = 1;
			// 
			// butPrint
			// 
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butPrint.Location = new System.Drawing.Point(769, 728);
			this.butPrint.Name = "butPrint";
			this.butPrint.TabIndex = 2;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// labelTotPages
			// 
			this.labelTotPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTotPages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelTotPages.Location = new System.Drawing.Point(774, 679);
			this.labelTotPages.Name = "labelTotPages";
			this.labelTotPages.Size = new System.Drawing.Size(54, 18);
			this.labelTotPages.TabIndex = 22;
			this.labelTotPages.Text = "1 / 2";
			this.labelTotPages.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butBack
			// 
			this.butBack.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butBack.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butBack.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butBack.Image = ((System.Drawing.Image)(resources.GetObject("butBack.Image")));
			this.butBack.Location = new System.Drawing.Point(752, 676);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(18, 23);
			this.butBack.TabIndex = 23;
			this.butBack.Click += new System.EventHandler(this.butBack_Click);
			// 
			// butFwd
			// 
			this.butFwd.AdjustImageLocation = new System.Drawing.Point(1, 0);
			this.butFwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butFwd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butFwd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butFwd.Image = ((System.Drawing.Image)(resources.GetObject("butFwd.Image")));
			this.butFwd.Location = new System.Drawing.Point(830, 676);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(18, 23);
			this.butFwd.TabIndex = 24;
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// FormClaimPrint
			// 
			this.AcceptButton = this.butPrint;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(860, 816);
			this.Controls.Add(this.labelTotPages);
			this.Controls.Add(this.butBack);
			this.Controls.Add(this.butFwd);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.Preview2);
			this.Controls.Add(this.butClose);
			this.Name = "FormClaimPrint";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormClaimPrint";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormClaimPrint_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormClaimPrint_Layout);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormClaimPrint_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			Preview2.Height=ClientRectangle.Height;
			Preview2.Width=ClientRectangle.Width-160;
			//butClose.Location=new Point(ClientRectangle.Width-100,ClientRectangle.Height-70);
			//butPrint.Location=new Point(ClientRectangle.Width-100,ClientRectangle.Height-140);
		}
		private void FormClaimPrint_Load(object sender, System.EventArgs e) {
			if(PrinterSettings.InstalledPrinters.Count==0){
				MessageBox.Show(Lan.g(this,"No printer installed"));
				return;
			}
			pd2=new PrintDocument();
			pagesPrinted=0;
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			Preview2.Document=pd2;//display document
		}

		///<summary>Only called from external forms without ever loading this form.  Prints without showing any print preview.  The printer name is set externally before this method is called.  Returns true if printed successfully.</summary>
		///<param name="printerName">The printername must be determined before calling this method.  However, if the printername is not valid, then the default printer will be used.</param>
		public bool PrintImmediate(string printerName){
			pd2=new PrintDocument();
			pagesPrinted=0;
			pd2.PrinterSettings.PrinterName=printerName;
			if(!pd2.PrinterSettings.IsValid){
				pd2.PrinterSettings.PrinterName=null;
			}
			pd2.OriginAtMargins=true;
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			try{
				pd2.Print();
			}
			catch{
				return false;
			}
			return true;
		}

		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){//raised for each page to be printed.
			FillDisplayStrings();
			int procLimit=ProcLimitForFormat();
			//claimProcs is filled in FillDisplayStrings
			totalPages=(int)Math.Ceiling((double)claimprocs.Count/(double)procLimit);
			FillProcStrings(pagesPrinted*procLimit,procLimit);
			Graphics grfx=ev.Graphics;
			float xPosText;
			for(int i=0;i<ClaimFormItems.ListForForm.Length;i++){
				if(ClaimFormItems.ListForForm[i].ImageFileName==""){//field
					xPosText=ClaimFormItems.ListForForm[i].XPos+ClaimForms.Cur.OffsetX;
					if(ClaimFormItems.ListForForm[i].FieldName=="P1Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P2Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P3Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P4Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P5Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P6Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P7Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P8Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P9Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="P10Fee"
						|| ClaimFormItems.ListForForm[i].FieldName=="TotalFee")
					{
						//this aligns it to the right
						xPosText-=grfx.MeasureString(displayStrings[i]
							,new Font(ClaimForms.Cur.FontName,ClaimForms.Cur.FontSize)).Width;
					}
					grfx.DrawString(displayStrings[i]
						,new Font(ClaimForms.Cur.FontName,ClaimForms.Cur.FontSize)
						,new SolidBrush(Color.Black)
						,new RectangleF(xPosText,ClaimFormItems.ListForForm[i].YPos+ClaimForms.Cur.OffsetY
						,ClaimFormItems.ListForForm[i].Width,ClaimFormItems.ListForForm[i].Height));
				}
				else{//image
					if(!ClaimForms.Cur.PrintImages){
						continue;
					}
					if(HideBackground){
						continue;
					}
					string fileName=((Pref)Prefs.HList["DocPath"]).ValueString+@"\"
						+ClaimFormItems.ListForForm[i].ImageFileName;
					if(!File.Exists(fileName)){
						MessageBox.Show("File not found.");
						continue;
					}
					Image thisImage=Image.FromFile(fileName);
					if(fileName.Substring(fileName.Length-3)=="jpg"){
						grfx.DrawImage(thisImage
							,ClaimFormItems.ListForForm[i].XPos+ClaimForms.Cur.OffsetX
							,ClaimFormItems.ListForForm[i].YPos+ClaimForms.Cur.OffsetY
							,(int)(thisImage.Width/thisImage.HorizontalResolution*100)
							,(int)(thisImage.Height/thisImage.VerticalResolution*100));
					}
					else if(fileName.Substring(fileName.Length-3)=="gif"){
						grfx.DrawImage(thisImage
							,ClaimFormItems.ListForForm[i].XPos+ClaimForms.Cur.OffsetX
							,ClaimFormItems.ListForForm[i].YPos+ClaimForms.Cur.OffsetY
							,ClaimFormItems.ListForForm[i].Width
							,ClaimFormItems.ListForForm[i].Height);
					}
					else if(fileName.Substring(fileName.Length-3)=="emf"){
						grfx.DrawImage(thisImage
							,ClaimFormItems.ListForForm[i].XPos+ClaimForms.Cur.OffsetX
							,ClaimFormItems.ListForForm[i].YPos+ClaimForms.Cur.OffsetY
							,thisImage.Width,thisImage.Height);
					}
				}
			}
			pagesPrinted++;
			if(totalPages==pagesPrinted){
				ev.HasMorePages=false;
				labelTotPages.Text="1 / "+totalPages.ToString();
			}
			else{
				//MessageBox.Show(pagesPrinted.ToString()+","+totalPages.ToString());
				ev.HasMorePages=true;
			}
		}

		///<summary>Only used when the print button is clicked from within this form during print preview.  Unlike PrintImmediate, this method must include selecting the appropriate printer.</summary>
		public bool PrintClaim(){
			pd2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(!pd2.PrinterSettings.IsValid){
				pd2.PrinterSettings.PrinterName=null;
			}
			pd2.OriginAtMargins=true;
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			try{
				printDialog2=new PrintDialog();
				printDialog2.Document=pd2;
				if(printDialog2.ShowDialog()==DialogResult.OK){
					pd2.Print();
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available."));
				return false;
			}
			return true;
		}

		private void FillDisplayStrings(){
			if(PrintBlank){
				ClaimForms.SetCur(1);//hard coded to ADA claimform for now.
				ClaimFormItems.GetListForForm();
				displayStrings=new string[ClaimFormItems.ListForForm.Length];
				return;
			}
			Patients.GetFamily(ThisPatNum);
			Claims.Refresh();
			Claims.Cur=(Claim)Claims.HList[ThisClaimNum];
			InsPlans.Refresh();
			//get other plan first to clear up Cur
			InsPlan otherPlan;
			InsPlans.GetCur(Claims.Cur.PlanNum2);
			otherPlan=InsPlans.Cur;
			InsPlans.GetCur(Claims.Cur.PlanNum);
			Patient subsc;
			if(Patients.GetIndex(InsPlans.Cur.Subscriber)==-1){//from another family
				Patients.GetFamily(InsPlans.Cur.Subscriber);
				subsc=Patients.Cur;
				Patients.GetFamily(ThisPatNum);//return to current family
			}
			else{
				subsc=Patients.FamilyList[Patients.GetIndex(InsPlans.Cur.Subscriber)];
			}
			Patient otherSubsc=new Patient();
			if(otherPlan.PlanNum!=0){//if secondary insurance exists
				if(Patients.GetIndex(otherPlan.Subscriber)==-1){//from another family
					Patients.GetFamily(otherPlan.Subscriber);
					otherSubsc=Patients.Cur;
					Patients.GetFamily(ThisPatNum);//return to current family
				}
				else{
					otherSubsc=Patients.FamilyList[Patients.GetIndex(otherPlan.Subscriber)];
				}				
			}	
			Procedures.Refresh();
      ClaimProcs.Refresh();
      ClaimProcs.GetForClaim(); 
			claimprocs=new ArrayList();
			bool includeThis;
			for(int i=0;i<ClaimProcs.ForClaim.Length;i++){//fill the arraylist
				if(ClaimProcs.ForClaim[i].ProcNum==0){
					continue;//skip payments
				}
				includeThis=true;
				for(int j=0;j<claimprocs.Count;j++){//loop through existing claimprocs
					if(((ClaimProc)claimprocs[j]).ProcNum==ClaimProcs.ForClaim[i].ProcNum){
						includeThis=false;//skip duplicate procedures
					}
				}
				if(includeThis)
					claimprocs.Add(ClaimProcs.ForClaim[i]);						
			}
			Provider treatDent=Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvTreat)];
			//claimformitems will already be refreshed in local data.  A normal user will never change items.
			if(ClaimFormNum==0){
				ClaimForms.SetCur(InsPlans.Cur.ClaimFormNum);
			}
			else{//usually only for batch generic e-claims
				ClaimForms.SetCur(ClaimFormNum);
			}
			ClaimFormItems.GetListForForm();
			displayStrings=new string[ClaimFormItems.ListForForm.Length];
			//a value is set for every item, but not every case will have a matching claimform item.
			for(int i=0;i<ClaimFormItems.ListForForm.Length;i++){
				switch(ClaimFormItems.ListForForm[i].FieldName){
					default://image. or procedure which gets filled in FillProcStrings.
						displayStrings[i]="";
						break;
					case "IsPreAuth":
						if(Claims.Cur.ClaimType=="PreAuth")
							displayStrings[i]="X";
						break;
					case "IsStandardClaim":
						if(Claims.Cur.ClaimType!="PreAuth")
							displayStrings[i]="X";
						break;
					case "IsMedicaidClaim"://this should later be replaced with an insplan field.
						if(Patients.Cur.MedicaidID!="")
							displayStrings[i]="X";
						break;
					case "PreAuthString":
						displayStrings[i]=Claims.Cur.PreAuthString;
						break;
					case "PriInsCarrierName":
						displayStrings[i]=InsPlans.Cur.Carrier;
						break;
					case "PriInsAddress":
						displayStrings[i]=InsPlans.Cur.Address;
						break;
					case "PriInsAddress2":
						displayStrings[i]=InsPlans.Cur.Address2;
						break;
					case "PriInsCity":
						displayStrings[i]=InsPlans.Cur.City;
						break;
					case "PriInsST":
						displayStrings[i]=InsPlans.Cur.State;
						break;
					case "PriInsZip":
						displayStrings[i]=InsPlans.Cur.Zip;
						break;
					case "OtherInsExists":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]="X";
						break;
					case "OtherInsNotExists":
						if(otherPlan.PlanNum==0)
							displayStrings[i]="X";
						break;
					case "OtherInsSubscrLastFirst":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherSubsc.LName+", "+otherSubsc.FName+", "+otherSubsc.MiddleI;
						break;
					case "OtherInsSubscrDOB":
						if(otherPlan.PlanNum!=0)
							if(ClaimFormItems.ListForForm[i].FormatString=="")
								displayStrings[i]=otherSubsc.Birthdate.ToShortDateString();
							else
								displayStrings[i]=otherSubsc.Birthdate.ToString
									(ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "OtherInsSubscrIsMale":
						if(otherPlan.PlanNum!=0 && otherSubsc.Gender==PatientGender.Male)
							displayStrings[i]="X";
						break;
					case "OtherInsSubscrIsFemale":
						if(otherPlan.PlanNum!=0 && otherSubsc.Gender==PatientGender.Female)
							displayStrings[i]="X";
						break;
					case "OtherInsSubscrID":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.SubscriberID;
						break;
						//if(otherPlan.PlanNum!=0 && otherSubsc.SSN.Length==9){
						//	displayStrings[i]=otherSubsc.SSN.Substring(0,3)
						//		+"-"+otherSubsc.SSN.Substring(3,2)
						//		+"-"+otherSubsc.SSN.Substring(5);
						//}
						//break;
					case "OtherInsGroupNum":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.GroupNum;
						break;
					case "OtherInsRelatIsSelf":
						if(otherPlan.PlanNum!=0 && Claims.Cur.PatRelat2==Relat.Self)
							displayStrings[i]="X";
						break;
					case "OtherInsRelatIsSpouse":
						if(otherPlan.PlanNum!=0 && Claims.Cur.PatRelat2==Relat.Spouse)
							displayStrings[i]="X";
						break;
					case "OtherInsRelatIsChild":
						if(otherPlan.PlanNum!=0 && Claims.Cur.PatRelat2==Relat.Child)
							displayStrings[i]="X";
						break;
					case "OtherInsRelatIsOther":
						if(otherPlan.PlanNum!=0 && (
							Claims.Cur.PatRelat2==Relat.Dependent
							|| Claims.Cur.PatRelat2==Relat.Employee
							|| Claims.Cur.PatRelat2==Relat.HandicapDep
							|| Claims.Cur.PatRelat2==Relat.InjuredPlaintiff
							|| Claims.Cur.PatRelat2==Relat.LifePartner
							|| Claims.Cur.PatRelat2==Relat.SignifOther
							))
							displayStrings[i]="X";
						break;
					case "OtherInsCarrierName":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.Carrier;
						break;
					case "OtherInsAddress":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.Address;
						break;
					case "OtherInsCity":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.City;
						break;
					case "OtherInsST":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.State;
						break;
					case "OtherInsZip":
						if(otherPlan.PlanNum!=0)
							displayStrings[i]=otherPlan.Zip;
						break;
					case "SubscrLastFirst":
						displayStrings[i]=subsc.LName+", "+subsc.FName+", "+subsc.MiddleI;
						break;
					case "SubscrAddress":
						displayStrings[i]=subsc.Address;
						break;
					case "SubscrAddress2":
						displayStrings[i]=subsc.Address2;
						break;
					case "SubscrCity":
						displayStrings[i]=subsc.City;
						break;
					case "SubscrST":
						displayStrings[i]=subsc.State;
						break;
					case "SubscrZip":
						displayStrings[i]=subsc.Zip;
						break;
					case "SubscrPhone":
						displayStrings[i]=subsc.HmPhone;
						break;
					case "SubscrDOB":
						if(ClaimFormItems.ListForForm[i].FormatString=="")
							displayStrings[i]=subsc.Birthdate.ToShortDateString();//MM/dd/yyyy
						else
							displayStrings[i]=subsc.Birthdate.ToString(ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "SubscrIsMale":
						if(subsc.Gender==PatientGender.Male)
							displayStrings[i]="X";
						break;
					case "SubscrIsFemale":
						if(subsc.Gender==PatientGender.Female)
							displayStrings[i]="X";
						break;
					case "SubscrIsMarried":
						if(subsc.Position==PatientPosition.Married)
							displayStrings[i]="X";
						break;
					case "SubscrIsSingle":
						if(subsc.Position==PatientPosition.Single
							|| subsc.Position==PatientPosition.Child
							|| subsc.Position==PatientPosition.Widowed)
							displayStrings[i]="X";
						break;
					case "SubscrID":
						displayStrings[i]=InsPlans.Cur.SubscriberID;
						break;
					case "SubscrIsFTStudent":
						if(subsc.StudentStatus=="F")
							displayStrings[i]="X";
						break;
					case "SubscrIsPTStudent":
						if(subsc.StudentStatus=="P")
							displayStrings[i]="X";
						break;
					case "GroupNum":
						displayStrings[i]=InsPlans.Cur.GroupNum;
						break;
					case "EmployerName":
						displayStrings[i]=InsPlans.Cur.Employer;
						break;
					case "RelatIsSelf":
						if(Claims.Cur.PatRelat==Relat.Self)
							displayStrings[i]="X";
						break;
					case "RelatIsSpouse":
						if(Claims.Cur.PatRelat==Relat.Spouse)
							displayStrings[i]="X";
						break;
					case "RelatIsChild":
						if(Claims.Cur.PatRelat==Relat.Child)
							displayStrings[i]="X";
						break;
					case "RelatIsOther":
						if(Claims.Cur.PatRelat==Relat.Dependent
							|| Claims.Cur.PatRelat==Relat.Employee
							|| Claims.Cur.PatRelat==Relat.HandicapDep
							|| Claims.Cur.PatRelat==Relat.InjuredPlaintiff
							|| Claims.Cur.PatRelat==Relat.LifePartner
							|| Claims.Cur.PatRelat==Relat.SignifOther)
							displayStrings[i]="X";
						break;
					case "IsFTStudent":
						if(Patients.Cur.StudentStatus=="F")
							displayStrings[i]="X";
						break;
					case "IsPTStudent":
						if(Patients.Cur.StudentStatus=="P")
							displayStrings[i]="X";
						break;
					case "PatientLastFirst":
						displayStrings[i]=Patients.Cur.LName+", "+Patients.Cur.FName+", "+Patients.Cur.MiddleI;
						break;
					case "PatientAddress":
						displayStrings[i]=Patients.Cur.Address;
						break;
					case "PatientAddress2":
						displayStrings[i]=Patients.Cur.Address2;
						break;
					case "PatientCity":
						displayStrings[i]=Patients.Cur.City;
						break;
					case "PatientST":
						displayStrings[i]=Patients.Cur.State;
						break;
					case "PatientZip":
						displayStrings[i]=Patients.Cur.Zip;
						break;
					case "PatientPhone":
						displayStrings[i]=Patients.Cur.HmPhone;
						break;
					case "PatientDOB":
						if(ClaimFormItems.ListForForm[i].FormatString=="")
							displayStrings[i]=Patients.Cur.Birthdate.ToShortDateString();//MM/dd/yyyy
						else
							displayStrings[i]=Patients.Cur.Birthdate.ToString
								(ClaimFormItems.ListForForm[i].FormatString);
						break;
					/*case "PatientDOBMonth":
						displayStrings[i]=Patients.Cur.Birthdate.ToString("MM");
						break;
					case "PatientDOBDay":
						displayStrings[i]=Patients.Cur.Birthdate.ToString("dd");
						break;
					case "PatientDOByy":
						displayStrings[i]=Patients.Cur.Birthdate.ToString("yy");
						break;
					case "PatientDOByyyy":
						displayStrings[i]=Patients.Cur.Birthdate.ToString("yyyy");
						break;*/
					case "PatientIsMale":
						if(Patients.Cur.Gender==PatientGender.Male)
							displayStrings[i]="X";
						break;
					case "PatientIsFemale":
						if(Patients.Cur.Gender==PatientGender.Female)
							displayStrings[i]="X";
						break;
					case "PatientIsMarried":
						if(Patients.Cur.Position==PatientPosition.Married)
							displayStrings[i]="X";
						break;
					case "PatientIsSingle":
						if(Patients.Cur.Position==PatientPosition.Single
							|| Patients.Cur.Position==PatientPosition.Child
							|| Patients.Cur.Position==PatientPosition.Widowed)
							displayStrings[i]="X";
						break;
					case "PatientSSN":
						if(Patients.Cur.SSN.Length==9){
							displayStrings[i]=Patients.Cur.SSN.Substring(0,3)
								+"-"+Patients.Cur.SSN.Substring(3,2)
								+"-"+Patients.Cur.SSN.Substring(5);
						}
						break;
					case "PatientMedicaidID":
						displayStrings[i]=Patients.Cur.MedicaidID;
						break;
					case "PatientID-MedicaidOrSSN":
						if(Patients.Cur.MedicaidID!="")
							displayStrings[i]=Patients.Cur.MedicaidID;
						else
							displayStrings[i]=Patients.Cur.SSN;
						break;
			//this is where the procedures used to be
					case "Miss1":
						if(Procedures.MissingTeeth.Contains("1"))
							displayStrings[i]="X";
						break;
					case "Miss2":
						if(Procedures.MissingTeeth.Contains("2"))
							displayStrings[i]="X";
						break;
					case "Miss3":
						if(Procedures.MissingTeeth.Contains("3"))
							displayStrings[i]="X";
						break;
					case "Miss4":
						if(Procedures.MissingTeeth.Contains("4"))
							displayStrings[i]="X";
						break;
					case "Miss5":
						if(Procedures.MissingTeeth.Contains("5"))
							displayStrings[i]="X";
						break;
					case "Miss6":
						if(Procedures.MissingTeeth.Contains("6"))
							displayStrings[i]="X";
						break;
					case "Miss7":
						if(Procedures.MissingTeeth.Contains("7"))
							displayStrings[i]="X";
						break;
					case "Miss8":
						if(Procedures.MissingTeeth.Contains("8"))
							displayStrings[i]="X";
						break;
					case "Miss9":
						if(Procedures.MissingTeeth.Contains("9"))
							displayStrings[i]="X";
						break;
					case "Miss10":
						if(Procedures.MissingTeeth.Contains("10"))
							displayStrings[i]="X";
						break;
					case "Miss11":
						if(Procedures.MissingTeeth.Contains("11"))
							displayStrings[i]="X";
						break;
					case "Miss12":
						if(Procedures.MissingTeeth.Contains("12"))
							displayStrings[i]="X";
						break;
					case "Miss13":
						if(Procedures.MissingTeeth.Contains("13"))
							displayStrings[i]="X";
						break;
					case "Miss14":
						if(Procedures.MissingTeeth.Contains("14"))
							displayStrings[i]="X";
						break;
					case "Miss15":
						if(Procedures.MissingTeeth.Contains("15"))
							displayStrings[i]="X";
						break;
					case "Miss16":
						if(Procedures.MissingTeeth.Contains("16"))
							displayStrings[i]="X";
						break;
					case "Miss17":
						if(Procedures.MissingTeeth.Contains("17"))
							displayStrings[i]="X";
						break;
					case "Miss18":
						if(Procedures.MissingTeeth.Contains("18"))
							displayStrings[i]="X";
						break;
					case "Miss19":
						if(Procedures.MissingTeeth.Contains("19"))
							displayStrings[i]="X";
						break;
					case "Miss20":
						if(Procedures.MissingTeeth.Contains("20"))
							displayStrings[i]="X";
						break;
					case "Miss21":
						if(Procedures.MissingTeeth.Contains("21"))
							displayStrings[i]="X";
						break;
					case "Miss22":
						if(Procedures.MissingTeeth.Contains("22"))
							displayStrings[i]="X";
						break;
					case "Miss23":
						if(Procedures.MissingTeeth.Contains("23"))
							displayStrings[i]="X";
						break;
					case "Miss24":
						if(Procedures.MissingTeeth.Contains("24"))
							displayStrings[i]="X";
						break;
					case "Miss25":
						if(Procedures.MissingTeeth.Contains("25"))
							displayStrings[i]="X";
						break;
					case "Miss26":
						if(Procedures.MissingTeeth.Contains("26"))
							displayStrings[i]="X";
						break;
					case "Miss27":
						if(Procedures.MissingTeeth.Contains("27"))
							displayStrings[i]="X";
						break;
					case "Miss28":
						if(Procedures.MissingTeeth.Contains("28"))
							displayStrings[i]="X";
						break;
					case "Miss29":
						if(Procedures.MissingTeeth.Contains("29"))
							displayStrings[i]="X";
						break;
					case "Miss30":
						if(Procedures.MissingTeeth.Contains("30"))
							displayStrings[i]="X";
						break;
					case "Miss31":
						if(Procedures.MissingTeeth.Contains("31"))
							displayStrings[i]="X";
						break;
					case "Miss32":
						if(Procedures.MissingTeeth.Contains("32"))
							displayStrings[i]="X";
						break;
					case "Remarks":
						displayStrings[i]=Claims.Cur.ClaimNote;
						break;
					case "PatientRelease":
						if(InsPlans.Cur.ReleaseInfo)
							displayStrings[i]="Signature on File"; 
						break;
					case "PatientReleaseDate":
						if(InsPlans.Cur.ReleaseInfo && Claims.Cur.DateSent.Year > 1860){
							if(ClaimFormItems.ListForForm[i].FormatString=="")
								displayStrings[i]=Claims.Cur.DateSent.ToShortDateString();
							else
								displayStrings[i]=Claims.Cur.DateSent.ToString
									(ClaimFormItems.ListForForm[i].FormatString);
						} 
						break;
					case "PatientAssignment":
						if(InsPlans.Cur.AssignBen)
							displayStrings[i]="Signature on File"; 
						break;
					case "PatientAssignmentDate":
						if(InsPlans.Cur.AssignBen && Claims.Cur.DateSent.Year > 1860){
							if(ClaimFormItems.ListForForm[i].FormatString=="")
								displayStrings[i]=Claims.Cur.DateSent.ToShortDateString();
							else
								displayStrings[i]=Claims.Cur.DateSent.ToString
									(ClaimFormItems.ListForForm[i].FormatString);
						}
						break;
					case "PlaceIsOffice":
						if(Claims.Cur.PlaceService==PlaceOfService.Office)
							displayStrings[i]="X";
						break;
					case "PlaceIsHospADA2002":
						if(Claims.Cur.PlaceService==PlaceOfService.InpatHospital
							|| Claims.Cur.PlaceService==PlaceOfService.OutpatHospital)
							displayStrings[i]="X";
						break;
					case "PlaceIsExtCareFacilityADA2002":
						if(Claims.Cur.PlaceService==PlaceOfService.AdultLivCareFac
							|| Claims.Cur.PlaceService==PlaceOfService.SkilledNursFac)
							displayStrings[i]="X";
						break;
					case "PlaceIsOtherADA2002":
						if(Claims.Cur.PlaceService==PlaceOfService.PatientsHome
							|| Claims.Cur.PlaceService==PlaceOfService.OtherLocation)
							displayStrings[i]="X";
						break;
					case "PlaceIsInpatHosp":
						if(Claims.Cur.PlaceService==PlaceOfService.InpatHospital)
							displayStrings[i]="X";
						break;
					case "PlaceIsOutpatHosp":
						if(Claims.Cur.PlaceService==PlaceOfService.OutpatHospital)
							displayStrings[i]="X";
						break;
					case "PlaceIsAdultLivCareFac":
						if(Claims.Cur.PlaceService==PlaceOfService.AdultLivCareFac)
							displayStrings[i]="X";
						break;
					case "PlaceIsSkilledNursFac":
						if(Claims.Cur.PlaceService==PlaceOfService.SkilledNursFac)
							displayStrings[i]="X";
						break;
					case "PlaceIsPatientsHome":
						if(Claims.Cur.PlaceService==PlaceOfService.PatientsHome)
							displayStrings[i]="X";
						break;
					case "PlaceIsOtherLocation":
						if(Claims.Cur.PlaceService==PlaceOfService.OtherLocation)
							displayStrings[i]="X";
						break;
					case "PlaceNumericCode":
						switch(Claims.Cur.PlaceService){
							case PlaceOfService.AdultLivCareFac:
								displayStrings[i]="33";//aka Custodial care facility
								break;
							case PlaceOfService.InpatHospital:
								displayStrings[i]="21";
								break;
							case PlaceOfService.Office:
								displayStrings[i]="11";
								break;
							case PlaceOfService.OutpatHospital:
								displayStrings[i]="22";
								break;
							case PlaceOfService.PatientsHome:
								displayStrings[i]="12";
								break;
							case PlaceOfService.SkilledNursFac:
								displayStrings[i]="31";
								break;
						}
						break;
					//"RadiographsEnclosed":
					//"ImagesEnclosed":
					//"ModelsEnclosed":
					case "IsNotOrtho":
						if(!Claims.Cur.IsOrtho)
							displayStrings[i]="X";
						break;
					case "IsOrtho":
						if(Claims.Cur.IsOrtho)
							displayStrings[i]="X";
						break;
					case "DateOrthoPlaced":
						if(Claims.Cur.OrthoDate.Year > 1860){
							if(ClaimFormItems.ListForForm[i].FormatString=="")
								displayStrings[i]=Claims.Cur.OrthoDate.ToShortDateString();
							else
								displayStrings[i]=Claims.Cur.OrthoDate.ToString
									(ClaimFormItems.ListForForm[i].FormatString);
						}
						break;
					case "MonthsOrthoRemaining":
						if(Claims.Cur.OrthoRemainM > 0)
							displayStrings[i]=Claims.Cur.OrthoRemainM.ToString();
						break;
					case "IsNotProsth":
						if(Claims.Cur.IsProsthesis=="N")
							displayStrings[i]="X";
						break;
					case "IsInitialProsth":
						if(Claims.Cur.IsProsthesis=="I")
							displayStrings[i]="X";
						break;
					case "IsNotReplacementProsth":
						if(Claims.Cur.IsProsthesis!="R")//=='I'nitial or 'N'o
							displayStrings[i]="X";
						break;
					case "IsReplacementProsth":
						if(Claims.Cur.IsProsthesis=="R")
							displayStrings[i]="X";
						break;
					case "DatePriorProsthPlaced":
						if(Claims.Cur.PriorDate.Year > 1860){
							if(ClaimFormItems.ListForForm[i].FormatString=="")
								displayStrings[i]=Claims.Cur.PriorDate.ToShortDateString();
							else
								displayStrings[i]=Claims.Cur.PriorDate.ToString
									(ClaimFormItems.ListForForm[i].FormatString);
						}
						break;
					case "IsOccupational":
						if(Claims.Cur.AccidentRelated=="E")
							displayStrings[i]="X";
						break;
					case "IsNotOccupational":
						if(Claims.Cur.AccidentRelated!="E")
							displayStrings[i]="X";
						break;
					case "IsAutoAccident":
						if(Claims.Cur.AccidentRelated=="A")
							displayStrings[i]="X";
						break;
					case "IsNotAutoAccident":
						if(Claims.Cur.AccidentRelated!="A")
							displayStrings[i]="X";
						break;
					case "IsOtherAccident":
						if(Claims.Cur.AccidentRelated=="O")
							displayStrings[i]="X";
						break;
					case "IsNotOtherAccident":
						if(Claims.Cur.AccidentRelated!="O")
							displayStrings[i]="X";
						break;
					case "IsNotAccident":
						if(Claims.Cur.AccidentRelated!="O" && Claims.Cur.AccidentRelated!="A")
							displayStrings[i]="X";
						break;
					case "AccidentDate":
						if(Claims.Cur.AccidentDate.Year > 1860){
							if(ClaimFormItems.ListForForm[i].FormatString=="")
								displayStrings[i]=Claims.Cur.AccidentDate.ToShortDateString();
							else
								displayStrings[i]=Claims.Cur.AccidentDate.ToString
									(ClaimFormItems.ListForForm[i].FormatString);
						}
						break;
					case "AccidentST":
						displayStrings[i]=Claims.Cur.AccidentST;
						break;
					case "BillingDentist":
						Provider P=Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)];
						displayStrings[i]=P.FName+" "+P.MI+" "+P.LName+" "+P.Title;
						break;
					case "BillingDentistAddress":
						displayStrings[i]=((Pref)Prefs.HList["PracticeAddress"]).ValueString;
						break;
					case "BillingDentistAddress2":
						displayStrings[i]=((Pref)Prefs.HList["PracticeAddress2"]).ValueString;
						break;
					case "BillingDentistCity":
						displayStrings[i]=((Pref)Prefs.HList["PracticeCity"]).ValueString;
						break;
					case "BillingDentistST":
						displayStrings[i]=((Pref)Prefs.HList["PracticeST"]).ValueString;
						break;
					case "BillingDentistZip":
						displayStrings[i]=((Pref)Prefs.HList["PracticeZip"]).ValueString;
						break;
					case "BillingDentistMedicaidID":
						displayStrings[i]=Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)].MedicaidID;
						break;
					//case "BillingDentistProvID":
					case "BillingDentistLicenseNum":
						displayStrings[i]=Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)].StateLicense;
						break;
					case "BillingDentistSSNorTIN":
						displayStrings[i]=Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)].SSN;
						break;
					case "BillingDentistNumIsSSN":
						if(!Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)].UsingTIN)
							displayStrings[i]="X";
						break;
					case "BillingDentistNumIsTIN":
						if(Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvBill)].UsingTIN)
							displayStrings[i]="X";
						break;
					case "BillingDentistPh123":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(0,3);
						}
						break;
					case "BillingDentistPh456":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(3,3);
						}
						break;
					case "BillingDentistPh78910":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(6);
						}
						break;
					case "TreatingDentistSignature":
						if(treatDent.SigOnFile){
							displayStrings[i]=treatDent.FName+" "+treatDent.MI+" "+treatDent.LName+" "
								+treatDent.Title;
						}
						break;
					case "TreatingDentistSigDate":
						if(treatDent.SigOnFile && Claims.Cur.DateSent.Year > 1860){
							if(ClaimFormItems.ListForForm[i].FormatString=="")
								displayStrings[i]=Claims.Cur.DateSent.ToShortDateString();
							else
								displayStrings[i]=Claims.Cur.DateSent.ToString
									(ClaimFormItems.ListForForm[i].FormatString);
						}
						break;
					case "TreatingDentistMedicaidID":
						displayStrings[i]=GetProcInfo("TreatingDentistMedicaidID",0);
						break;
					//case "TreatingDentistProvID":
					case "TreatingDentistLicense":
						displayStrings[i]=treatDent.StateLicense;
						break;
					case "TreatingDentistAddress":
						displayStrings[i]=((Pref)Prefs.HList["PracticeAddress"]).ValueString;
						break;
					case "TreatingDentistCity":
						displayStrings[i]=((Pref)Prefs.HList["PracticeCity"]).ValueString;
						break;
					case "TreatingDentistST":
						displayStrings[i]=((Pref)Prefs.HList["PracticeST"]).ValueString;
						break;
					case "TreatingDentistZip":
						displayStrings[i]=((Pref)Prefs.HList["PracticeZip"]).ValueString;
						break;
					case "TreatingDentistPh123":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(0,3);
						}
						break;
					case "TreatingDentistPh456":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(3,3);
						}
						break;
					case "TreatingDentistPh78910":
						if(((Pref)Prefs.HList["PracticePhone"]).ValueString.Length==10){
							displayStrings[i]=((Pref)Prefs.HList["PracticePhone"]).ValueString.Substring(6);
						}
						break;
					case "TreatingProviderSpecialty":
						string spec="";
						switch(Providers.ListLong[Providers.GetIndexLong(Claims.Cur.ProvTreat)].Specialty){
							case DentalSpecialty.General:       spec="1223G0001X"; break;
							case DentalSpecialty.PublicHealth:  spec="1223D0001X"; break;
							case DentalSpecialty.Endodontics:   spec="1223E0200X"; break;
							case DentalSpecialty.Pathology:     spec="1223P0106X"; break;
							case DentalSpecialty.Radiology:     spec="1223D0008X"; break;
							case DentalSpecialty.Surgery:       spec="1223S0112X"; break;
							case DentalSpecialty.Ortho:         spec="1223X0400X"; break;
							case DentalSpecialty.Pediatric:     spec="1223P0221X"; break;
							case DentalSpecialty.Perio:         spec="1223P0300X"; break;
							case DentalSpecialty.Prosth:        spec="1223P0700X"; break;
							//non-dentist codes not permitted.
						}
						displayStrings[i]=spec;
						break;
				}//switch
			}//for
		}

		/// <summary></summary>
		/// <param name="startProc">For page 1, this will be 0, otherwise it might be 10, 8, 20, or whatever.  It is the 0-based index of the first proc. Depends on how many procedures this claim format can display and which page we are on.</param>
		/// <param name="totProcs">The number of procedures that can be displayed or printed per claim form.  Depends on the individual claim format. For example, 10 on the ADA2002</param>
		private void FillProcStrings(int startProc,int totProcs){
			for(int i=0;i<ClaimFormItems.ListForForm.Length;i++){
				switch(ClaimFormItems.ListForForm[i].FieldName){
					//there is no default, because any non-matches will remain as ""
					case "P1Date":
						displayStrings[i]=GetProcInfo("Date",1+startProc,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P1Area":
						displayStrings[i]=GetProcInfo("Area",1+startProc);
						break;
					case "P1System":
						displayStrings[i]=GetProcInfo("System",1+startProc);
						break;
					case "P1ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",1+startProc);
						break;
					case "P1Surface":
						displayStrings[i]=GetProcInfo("Surface",1+startProc);
						break;
					case "P1Code":
						displayStrings[i]=GetProcInfo("Code",1+startProc);
						break;
					case "P1Description":
						displayStrings[i]=GetProcInfo("Desc",1+startProc);
						break;
					case "P1Fee":
						displayStrings[i]=GetProcInfo("Fee",1+startProc);
						break;
					case "P1TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",1+startProc);
						break;
					case "P2Date":
						displayStrings[i]=GetProcInfo("Date",2+startProc,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P2Area":
						displayStrings[i]=GetProcInfo("Area",2+startProc);
						break;
					case "P2System":
						displayStrings[i]=GetProcInfo("System",2+startProc);
						break;
					case "P2ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",2+startProc);
						break;
					case "P2Surface":
						displayStrings[i]=GetProcInfo("Surface",2+startProc);
						break;
					case "P2Code":
						displayStrings[i]=GetProcInfo("Code",2+startProc);
						break;
					case "P2Description":
						displayStrings[i]=GetProcInfo("Desc",2+startProc);
						break;
					case "P2Fee":
						displayStrings[i]=GetProcInfo("Fee",2+startProc);
						break;
					case "P2TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",2+startProc);
						break;
					case "P3Date":
						displayStrings[i]=GetProcInfo("Date",3+startProc,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P3Area":
						displayStrings[i]=GetProcInfo("Area",3+startProc);
						break;
					case "P3System":
						displayStrings[i]=GetProcInfo("System",3+startProc);
						break;
					case "P3ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",3+startProc);
						break;
					case "P3Surface":
						displayStrings[i]=GetProcInfo("Surface",3+startProc);
						break;
					case "P3Code":
						displayStrings[i]=GetProcInfo("Code",3+startProc);
						break;
					case "P3Description":
						displayStrings[i]=GetProcInfo("Desc",3+startProc);
						break;
					case "P3Fee":
						displayStrings[i]=GetProcInfo("Fee",3+startProc);
						break;
					case "P3TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",3+startProc);
						break;
					case "P4Date":
						displayStrings[i]=GetProcInfo("Date",4+startProc,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P4Area":
						displayStrings[i]=GetProcInfo("Area",4+startProc);
						break;
					case "P4System":
						displayStrings[i]=GetProcInfo("System",4+startProc);
						break;
					case "P4ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",4+startProc);
						break;
					case "P4Surface":
						displayStrings[i]=GetProcInfo("Surface",4+startProc);
						break;
					case "P4Code":
						displayStrings[i]=GetProcInfo("Code",4+startProc);
						break;
					case "P4Description":
						displayStrings[i]=GetProcInfo("Desc",4+startProc);
						break;
					case "P4Fee":
						displayStrings[i]=GetProcInfo("Fee",4+startProc);
						break;
					case "P4TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",4+startProc);
						break;
					case "P5Date":
						displayStrings[i]=GetProcInfo("Date",5+startProc,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P5Area":
						displayStrings[i]=GetProcInfo("Area",5+startProc);
						break;
					case "P5System":
						displayStrings[i]=GetProcInfo("System",5+startProc);
						break;
					case "P5ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",5+startProc);
						break;
					case "P5Surface":
						displayStrings[i]=GetProcInfo("Surface",5+startProc);
						break;
					case "P5Code":
						displayStrings[i]=GetProcInfo("Code",5+startProc);
						break;
					case "P5Description":
						displayStrings[i]=GetProcInfo("Desc",5+startProc);
						break;
					case "P5Fee":
						displayStrings[i]=GetProcInfo("Fee",5+startProc);
						break;
					case "P5TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",5);
						break;
					case "P6Date":
						displayStrings[i]=GetProcInfo("Date",6+startProc,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P6Area":
						displayStrings[i]=GetProcInfo("Area",6+startProc);
						break;
					case "P6System":
						displayStrings[i]=GetProcInfo("System",6+startProc);
						break;
					case "P6ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",6+startProc);
						break;
					case "P6Surface":
						displayStrings[i]=GetProcInfo("Surface",6+startProc);
						break;
					case "P6Code":
						displayStrings[i]=GetProcInfo("Code",6+startProc);
						break;
					case "P6Description":
						displayStrings[i]=GetProcInfo("Desc",6+startProc);
						break;
					case "P6Fee":
						displayStrings[i]=GetProcInfo("Fee",6+startProc);
						break;
					case "P6TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",6+startProc);
						break;
					case "P7Date":
						displayStrings[i]=GetProcInfo("Date",7+startProc,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P7Area":
						displayStrings[i]=GetProcInfo("Area",7+startProc);
						break;
					case "P7System":
						displayStrings[i]=GetProcInfo("System",7+startProc);
						break;
					case "P7ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",7+startProc);
						break;
					case "P7Surface":
						displayStrings[i]=GetProcInfo("Surface",7+startProc);
						break;
					case "P7Code":
						displayStrings[i]=GetProcInfo("Code",7+startProc);
						break;
					case "P7Description":
						displayStrings[i]=GetProcInfo("Desc",7+startProc);
						break;
					case "P7Fee":
						displayStrings[i]=GetProcInfo("Fee",7+startProc);
						break;
					case "P7TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",7+startProc);
						break;
					case "P8Date":
						displayStrings[i]=GetProcInfo("Date",8+startProc,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P8Area":
						displayStrings[i]=GetProcInfo("Area",8+startProc);
						break;
					case "P8System":
						displayStrings[i]=GetProcInfo("System",8+startProc);
						break;
					case "P8ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",8+startProc);
						break;
					case "P8Surface":
						displayStrings[i]=GetProcInfo("Surface",8+startProc);
						break;
					case "P8Code":
						displayStrings[i]=GetProcInfo("Code",8+startProc);
						break;
					case "P8Description":
						displayStrings[i]=GetProcInfo("Desc",8+startProc);
						break;
					case "P8Fee":
						displayStrings[i]=GetProcInfo("Fee",8+startProc);
						break;
					case "P8TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",8+startProc);
						break;
					case "P9Date":
						displayStrings[i]=GetProcInfo("Date",9+startProc,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P9Area":
						displayStrings[i]=GetProcInfo("Area",9+startProc);
						break;
					case "P9System":
						displayStrings[i]=GetProcInfo("System",9+startProc);
						break;
					case "P9ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",9+startProc);
						break;
					case "P9Surface":
						displayStrings[i]=GetProcInfo("Surface",9+startProc);
						break;
					case "P9Code":
						displayStrings[i]=GetProcInfo("Code",9+startProc);
						break;
					case "P9Description":
						displayStrings[i]=GetProcInfo("Desc",9+startProc);
						break;
					case "P9Fee":
						displayStrings[i]=GetProcInfo("Fee",9+startProc);
						break;
					case "P9TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",9+startProc);
						break;
					case "P10Date":
						displayStrings[i]=GetProcInfo("Date",10+startProc,ClaimFormItems.ListForForm[i].FormatString);
						break;
					case "P10Area":
						displayStrings[i]=GetProcInfo("Area",10+startProc);
						break;
					case "P10System":
						displayStrings[i]=GetProcInfo("System",10+startProc);
						break;
					case "P10ToothNumber":
						displayStrings[i]=GetProcInfo("ToothNum",10+startProc);
						break;
					case "P10Surface":
						displayStrings[i]=GetProcInfo("Surface",10+startProc);
						break;
					case "P10Code":
						displayStrings[i]=GetProcInfo("Code",10+startProc);
						break;
					case "P10Description":
						displayStrings[i]=GetProcInfo("Desc",10+startProc);
						break;
					case "P10Fee":
						displayStrings[i]=GetProcInfo("Fee",10+startProc);
						break;
					case "P10TreatDentMedicaidID":
						displayStrings[i]=GetProcInfo("TreatDentMedicaidID",10+startProc);
						break;
					case "TotalFee":
						double fee=0;//fee only for this page. Each page is treated like a separate claim.
						for(int f=startProc;f<startProc+totProcs;f++){//eg f=0;f<10;f++
							if(f < claimprocs.Count)
								fee+=((ClaimProc)claimprocs[f]).FeeBilled;
						}
						displayStrings[i]=fee.ToString("F");
						break;
				}//switch
			}//for i
		}

		/// <summary>Uses the fee field to determine how many procedures this claim will print.</summary>
		/// <returns></returns>
		private int ProcLimitForFormat(){
			int retVal=0;
			//loop until a match is not found
			for(int i=0;i<15;i++){
				for(int ii=0;ii<ClaimFormItems.ListForForm.Length;ii++){
					if(ClaimFormItems.ListForForm[ii].FieldName=="P"+i.ToString()+"Fee"){
						retVal=i;
					}
				}//for ii
			}
			return retVal;
		}

		/// <summary>Overload that does not need a stringFormat</summary>
		/// <param name="field"></param>
		/// <param name="procIndex"></param>
		/// <returns></returns>
		private string GetProcInfo(string field,int procIndex){
			return GetProcInfo(field,procIndex,"");
		}

		/// <summary>Gets the string to be used for this field and index.</summary>
		/// <param name="field"></param>
		/// <param name="procIndex"></param>
		/// <param name="stringFormat"></param>
		/// <returns></returns>
		private string GetProcInfo(string field,int procIndex, string stringFormat){
			//remember that procIndex is 1 based, not 0 based, 
			procIndex--;//so convert to 0 based
			if(claimprocs.Count <= procIndex){
				return "";
			}
			if(field=="System")
				return "JP";
			if(field=="Code")
				return ((ClaimProc)claimprocs[procIndex]).CodeSent;
			if(field=="System")
				return "JP";
			if(field=="Fee"){
				return ((ClaimProc)claimprocs[procIndex]).FeeBilled.ToString("F");
			}
			if(field=="Desc")
				return ProcedureCodes.GetProcCode(
					((Procedure)Procedures.HList[((ClaimProc)claimprocs[procIndex]).ProcNum]).ADACode).Descript;
			if(field=="Date"){
				if(Claims.Cur.ClaimType=="PreAuth")//no date on preauth procedures
					return "";
				if(stringFormat=="")
					return ((ClaimProc)claimprocs[procIndex]).DateCP.ToShortDateString();
				return ((ClaimProc)claimprocs[procIndex]).DateCP.ToString(stringFormat);
			}
			if(field=="TreatDentMedicaidID"){
				if(((ClaimProc)claimprocs[procIndex]).ProvNum==0){
					return "";
				}
				else return
					Providers.ListLong[Providers.GetIndexLong(((ClaimProc)claimprocs[procIndex]).ProvNum)].MedicaidID;
			}
			Procedures.Cur=(Procedure)Procedures.HList[ClaimProcs.ForClaim[procIndex].ProcNum];
			string area="";
			string toothNum="";
			string surf="";
			switch(((ProcedureCode)ProcedureCodes.HList[Procedures.Cur.ADACode]).TreatArea){
				case TreatmentArea.Surf:
					//area blank
					toothNum=Procedures.Cur.ToothNum;
					surf=Procedures.Cur.Surf;
					break;
				case TreatmentArea.Tooth:
					//area blank
					toothNum=Procedures.Cur.ToothNum;
					//surf blank
					break;
				case TreatmentArea.Quad:
					area=Procedures.Cur.Surf;//area "UL" etc
					//num blank
					//surf blank
					break;
				case TreatmentArea.Sextant:
					area="S"+Procedures.Cur.Surf;//area
					//num blank
					//surf blank
					break;
				case TreatmentArea.Arch:
					area=Procedures.Cur.Surf;//area "U", etc
					//num blank
					//surf blank
					break;
				case TreatmentArea.ToothRange:
					//area blank
					toothNum="";
					for(int i=0;i<Procedures.Cur.ToothRange.Split(',').Length;i++){
						if(!Tooth.IsValidDB(Procedures.Cur.ToothRange.Split(',')[i])){
							continue;
						}
						if(i>0){
							toothNum+=",";
						}
						toothNum+=Procedures.Cur.ToothRange.Split(',')[i];
					}
					//surf blank
					break;
				default://mouth
					//area?
					break;
			}//switch treatarea
      switch(field){
				case "Area":
					return area;
				case "ToothNum":
					return toothNum;
				case "Surface":
					return surf;
			}
			MessageBox.Show("error in getprocinfo");
			return "";//should never get to here
		}

		private void butBack_Click(object sender, System.EventArgs e){
			if(Preview2.StartPage==0) return;
			Preview2.StartPage--;
			labelTotPages.Text=(Preview2.StartPage+1).ToString()
				+" / "+totalPages.ToString();
		}

		private void butFwd_Click(object sender, System.EventArgs e){
			if(Preview2.StartPage==totalPages-1) return;
			Preview2.StartPage++;
			labelTotPages.Text=(Preview2.StartPage+1).ToString()
				+" / "+totalPages.ToString();
		}

		private void butPrint_Click(object sender, System.EventArgs e){
			pagesPrinted=0;
			if(PrintClaim()){
				Claims.UpdateStatus(ThisClaimNum,"P");
				DialogResult=DialogResult.OK;
			}
			else{
				DialogResult=DialogResult.Cancel;
			}
			//Close();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}


	}

}
















