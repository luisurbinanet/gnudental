using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormClaimView : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textCarrier;
		private System.Windows.Forms.TextBox textType;
		private System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private OpenDental.TableClaimProc tbProc;
		//private bool IsPrimary=true;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton radioStatusH;
		private System.Windows.Forms.RadioButton radioStatusP;
		private System.Windows.Forms.RadioButton radioStatusW;
		private System.Windows.Forms.RadioButton radioStatusU;
		private System.Windows.Forms.RadioButton radioStatusR;
		private System.Windows.Forms.RadioButton radioStatusS;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Label label1;
		private string oldStatus="";

		public FormClaimView(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.label4,
				this.label3,
				this.label2,
				this.radioStatusH,
				this.radioStatusP,
				this.radioStatusR,
				this.radioStatusS,
				this.radioStatusU,
				this.radioStatusW,
				this.groupBox3,
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

		private void InitializeComponent(){
			this.tbProc = new OpenDental.TableClaimProc();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.textType = new System.Windows.Forms.TextBox();
			this.textName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butCancel = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.radioStatusH = new System.Windows.Forms.RadioButton();
			this.radioStatusP = new System.Windows.Forms.RadioButton();
			this.radioStatusW = new System.Windows.Forms.RadioButton();
			this.radioStatusU = new System.Windows.Forms.RadioButton();
			this.radioStatusR = new System.Windows.Forms.RadioButton();
			this.radioStatusS = new System.Windows.Forms.RadioButton();
			this.butOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbProc
			// 
			this.tbProc.BackColor = System.Drawing.SystemColors.Window;
			this.tbProc.Location = new System.Drawing.Point(21, 218);
			this.tbProc.Name = "tbProc";
			this.tbProc.SelectedIndices = new int[0];
			this.tbProc.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbProc.Size = new System.Drawing.Size(939, 356);
			this.tbProc.TabIndex = 0;
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(114, 60);
			this.textCarrier.MaxLength = 100;
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.ReadOnly = true;
			this.textCarrier.Size = new System.Drawing.Size(344, 20);
			this.textCarrier.TabIndex = 8;
			this.textCarrier.Text = "";
			// 
			// textType
			// 
			this.textType.Location = new System.Drawing.Point(114, 38);
			this.textType.MaxLength = 100;
			this.textType.Name = "textType";
			this.textType.ReadOnly = true;
			this.textType.Size = new System.Drawing.Size(344, 20);
			this.textType.TabIndex = 7;
			this.textType.Text = "";
			// 
			// textName
			// 
			this.textName.Location = new System.Drawing.Point(114, 16);
			this.textName.MaxLength = 100;
			this.textName.Name = "textName";
			this.textName.ReadOnly = true;
			this.textName.Size = new System.Drawing.Size(344, 20);
			this.textName.TabIndex = 5;
			this.textName.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(20, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(78, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Carrier Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(20, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Type of Claim";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(20, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Patient Name";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(544, 650);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 29;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.radioStatusH);
			this.groupBox3.Controls.Add(this.radioStatusP);
			this.groupBox3.Controls.Add(this.radioStatusW);
			this.groupBox3.Controls.Add(this.radioStatusU);
			this.groupBox3.Controls.Add(this.radioStatusR);
			this.groupBox3.Controls.Add(this.radioStatusS);
			this.groupBox3.Location = new System.Drawing.Point(20, 82);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(178, 128);
			this.groupBox3.TabIndex = 30;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Claim Status";
			// 
			// radioStatusH
			// 
			this.radioStatusH.Location = new System.Drawing.Point(4, 34);
			this.radioStatusH.Name = "radioStatusH";
			this.radioStatusH.Size = new System.Drawing.Size(133, 16);
			this.radioStatusH.TabIndex = 1;
			this.radioStatusH.Text = "Hold until Pri received";
			this.radioStatusH.Click += new System.EventHandler(this.radioStatusH_Click);
			// 
			// radioStatusP
			// 
			this.radioStatusP.Location = new System.Drawing.Point(4, 66);
			this.radioStatusP.Name = "radioStatusP";
			this.radioStatusP.Size = new System.Drawing.Size(106, 16);
			this.radioStatusP.TabIndex = 3;
			this.radioStatusP.Text = "Probably sent";
			this.radioStatusP.Click += new System.EventHandler(this.radioStatusP_Click);
			// 
			// radioStatusW
			// 
			this.radioStatusW.Location = new System.Drawing.Point(4, 50);
			this.radioStatusW.Name = "radioStatusW";
			this.radioStatusW.Size = new System.Drawing.Size(134, 16);
			this.radioStatusW.TabIndex = 2;
			this.radioStatusW.Text = "Waiting in Queue";
			this.radioStatusW.Click += new System.EventHandler(this.radioStatusW_Click);
			// 
			// radioStatusU
			// 
			this.radioStatusU.Location = new System.Drawing.Point(4, 18);
			this.radioStatusU.Name = "radioStatusU";
			this.radioStatusU.Size = new System.Drawing.Size(68, 16);
			this.radioStatusU.TabIndex = 0;
			this.radioStatusU.Text = "Unsent";
			this.radioStatusU.Click += new System.EventHandler(this.radioStatusU_Click);
			// 
			// radioStatusR
			// 
			this.radioStatusR.Location = new System.Drawing.Point(4, 98);
			this.radioStatusR.Name = "radioStatusR";
			this.radioStatusR.Size = new System.Drawing.Size(72, 16);
			this.radioStatusR.TabIndex = 5;
			this.radioStatusR.Text = "Received";
			this.radioStatusR.Click += new System.EventHandler(this.radioStatusR_Click);
			// 
			// radioStatusS
			// 
			this.radioStatusS.Location = new System.Drawing.Point(4, 82);
			this.radioStatusS.Name = "radioStatusS";
			this.radioStatusS.Size = new System.Drawing.Size(100, 16);
			this.radioStatusS.TabIndex = 4;
			this.radioStatusS.Text = "Sent - Verified";
			this.radioStatusS.Click += new System.EventHandler(this.radioStatusS_Click);
			// 
			// butOK
			// 
			this.butOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.butOK.Location = new System.Drawing.Point(544, 618);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 31;
			this.butOK.Text = "OK";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(218, 94);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(320, 104);
			this.label1.TabIndex = 32;
			this.label1.Text = "This form is obsolete";
			// 
			// FormClaimView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(642, 696);
			this.ControlBox = false;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.textCarrier);
			this.Controls.Add(this.textType);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbProc);
			this.Name = "FormClaimView";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Claim View";
			this.Load += new System.EventHandler(this.FormClaimsQueued_Load);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		//THIS FORM IS OBSOLETE

		private void FormClaimsQueued_Load(object sender, System.EventArgs e) {
			FillTable();
			textName.Text=Claims.CurQueue.PatName;
			//textType.Text=Defs.GetName(DefCat.ClaimFormats,Claims.CurQueue.ClaimFormat);
			textCarrier.Text=Claims.CurQueue.Carrier;
			oldStatus=Claims.CurQueue.ClaimStatus;
			switch(Claims.CurQueue.ClaimStatus){
				case "U"://unsent
					radioStatusU.Checked=true;
					break;
				case "H"://hold until pri received
					radioStatusH.Checked=true;
					break;
				case "W"://waiting to be sent
					radioStatusW.Checked=true;
					break;
				case "P"://probably sent
					radioStatusP.Checked=true;
					break;
				case "S"://sent-verified
					radioStatusS.Checked=true;
					break;
				case "R"://received
					radioStatusR.Checked=true;
					break;
			}
		}

		private void FillTable(){
			//double ClaimFee=0;
			//double priInsEst;
			//double secInsEst;
			//double PriInsPayEstSubtotal=0;
			//double SecInsPayEstSubtotal=0;

			/*tbProc.ResetRows(Claims.ProcsInClaim.Count);
			tbProc.SetGridColor(Color.Gray);
			tbProc.SetBackGColor(Color.White);  
			for(int i=0;i<Claims.ProcsInClaim.Count;i++){
				//tbProc.Cell[0,i]=((Procedure)Claims.ProcsInClaim[i]).ADACode;
				//tbProc.Cell[1,i]=((Procedure)Claims.ProcsInClaim[i]).ToothNum;
				//tbProc.Cell[2,i]=ProcCodes.GetProcCode(((Procedure)Claims.ProcsInClaim[i]).ADACode).Descript;
				//double fee=((Procedure)Claims.ProcsInClaim[i]).ProcFee;
				Procedures.Cur=(Procedure)Claims.ProcsInClaim[i];
				priInsEst=Procedures.GetEstForCur(PriSecTot.Pri);
				secInsEst=Procedures.GetEstForCur(PriSecTot.Sec);
				ClaimFee+=fee;
				PriInsPayEstSubtotal+=priInsEst;
				SecInsPayEstSubtotal+=secInsEst;
				tbProc.Cell[3,i]=fee.ToString("F");
				if(IsPrimary){
					tbProc.Cell[4,i]=priInsEst.ToString("F");
				}
				else{
					tbProc.Cell[4,i]=secInsEst.ToString("F");		
				}			
				tbProc.Cell[5,i]=(fee-priInsEst-secInsEst).ToString("F");
			}//end for
			tbProc.LayoutTables();
	    tbProc.SelectedRow=-1;		*/
		}

		private void radioStatusU_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="U";
      Claims.UpdateStatus(Claims.CurQueue.ClaimNum,"U");
		}

		private void radioStatusH_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="H";
      Claims.UpdateStatus(Claims.CurQueue.ClaimNum,"H");
		}

		private void radioStatusW_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="W";
      Claims.UpdateStatus(Claims.CurQueue.ClaimNum,"W");
		}

		private void radioStatusP_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="P";
      Claims.UpdateStatus(Claims.CurQueue.ClaimNum,"P");
		}

		private void radioStatusS_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="S";
      Claims.UpdateStatus(Claims.CurQueue.ClaimNum,"S");
		}

		private void radioStatusR_Click(object sender, System.EventArgs e) {
			Claims.Cur.ClaimStatus="R";
      Claims.UpdateStatus(Claims.CurQueue.ClaimNum,"R");
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
		  Claims.UpdateStatus(Claims.CurQueue.ClaimNum,oldStatus);
			DialogResult=DialogResult.Cancel;
		}
	}
}
