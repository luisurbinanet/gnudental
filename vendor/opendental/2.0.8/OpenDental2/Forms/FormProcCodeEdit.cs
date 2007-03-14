using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{

	public class FormProcCodeEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.ListBox listTreatArea;
		private System.Windows.Forms.ListBox listCategory;
		private System.ComponentModel.Container components = null;// Required designer variable.
		public bool IsNew;
		private System.Windows.Forms.TextBox textADACode;
		private System.Windows.Forms.TextBox textAbbrev;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.CheckBox checkRemoveTth;
		private System.Windows.Forms.CheckBox checkSetRecall;
		private System.Windows.Forms.CheckBox checkNoBillIns;
		private OpenDental.TableProcFee tbFees;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button butSlider;
		private OpenDental.TableTimeBar tbTime;
		private System.Windows.Forms.TextBox textTime2;
		public string NewADA;
		private bool mouseIsDown;
		private Point	mouseOrigin;
		private Point sliderOrigin;
		private System.Windows.Forms.Label label11;
		private StringBuilder strBTime;
		private System.Windows.Forms.CheckBox checkIsHygiene;
		private System.Windows.Forms.ListBox listGraphicType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butNone;
		private bool FeeChanged;

		public FormProcCodeEdit(){
			InitializeComponent();// Required for Windows Form Designer support
			//tbTime.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbTime_CellDoubleClicked);
			tbTime.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbTime_CellClicked);
			tbFees.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbFees_CellClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1,
				label2,
				label4,
				label5,
				label6,
				label7,
				label8,
				label10,
				checkRemoveTth,
				checkSetRecall,
				checkNoBillIns,
				label3,
				label9,
				checkIsHygiene
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
				butNone
			});
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.textADACode = new System.Windows.Forms.TextBox();
			this.textAbbrev = new System.Windows.Forms.TextBox();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.listTreatArea = new System.Windows.Forms.ListBox();
			this.checkRemoveTth = new System.Windows.Forms.CheckBox();
			this.checkSetRecall = new System.Windows.Forms.CheckBox();
			this.checkNoBillIns = new System.Windows.Forms.CheckBox();
			this.listCategory = new System.Windows.Forms.ListBox();
			this.tbFees = new OpenDental.TableProcFee();
			this.label3 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.tbTime = new OpenDental.TableTimeBar();
			this.butSlider = new System.Windows.Forms.Button();
			this.textTime2 = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.checkIsHygiene = new System.Windows.Forms.CheckBox();
			this.listGraphicType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butNone = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(56, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "ADA Code";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(339, 151);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 14);
			this.label4.TabIndex = 3;
			this.label4.Text = "Treatment Area";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(611, 12);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 14);
			this.label5.TabIndex = 4;
			this.label5.Text = "Category";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(2, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(42, 30);
			this.label6.TabIndex = 5;
			this.label6.Text = "Time Pattern";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(46, 76);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(70, 24);
			this.label7.TabIndex = 6;
			this.label7.Text = "Abbreviated Description";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(46, 48);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(70, 14);
			this.label8.TabIndex = 7;
			this.label8.Text = "Description";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(52, 342);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(78, 14);
			this.label10.TabIndex = 9;
			this.label10.Text = "Default Note";
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(54, 364);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(522, 200);
			this.textNote.TabIndex = 8;
			this.textNote.Text = "";
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(830, 569);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 10;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(830, 613);
			this.butCancel.Name = "butCancel";
			this.butCancel.TabIndex = 11;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textADACode
			// 
			this.textADACode.Location = new System.Drawing.Point(118, 12);
			this.textADACode.Name = "textADACode";
			this.textADACode.ReadOnly = true;
			this.textADACode.TabIndex = 14;
			this.textADACode.Text = "";
			// 
			// textAbbrev
			// 
			this.textAbbrev.Location = new System.Drawing.Point(118, 78);
			this.textAbbrev.MaxLength = 20;
			this.textAbbrev.Name = "textAbbrev";
			this.textAbbrev.TabIndex = 1;
			this.textAbbrev.Text = "";
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(118, 46);
			this.textDescription.MaxLength = 50;
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(330, 20);
			this.textDescription.TabIndex = 0;
			this.textDescription.Text = "";
			// 
			// listTreatArea
			// 
			this.listTreatArea.Items.AddRange(new object[] {
																											 "Surface",
																											 "Tooth",
																											 "Mouth",
																											 "Quadrant",
																											 "Sextant",
																											 "Arch",
																											 "Tooth Range"});
			this.listTreatArea.Location = new System.Drawing.Point(340, 171);
			this.listTreatArea.Name = "listTreatArea";
			this.listTreatArea.Size = new System.Drawing.Size(94, 95);
			this.listTreatArea.TabIndex = 2;
			// 
			// checkRemoveTth
			// 
			this.checkRemoveTth.Location = new System.Drawing.Point(56, 233);
			this.checkRemoveTth.Name = "checkRemoveTth";
			this.checkRemoveTth.Size = new System.Drawing.Size(268, 16);
			this.checkRemoveTth.TabIndex = 4;
			this.checkRemoveTth.Text = "Remove Tooth if marked complete";
			// 
			// checkSetRecall
			// 
			this.checkSetRecall.Location = new System.Drawing.Point(56, 258);
			this.checkSetRecall.Name = "checkSetRecall";
			this.checkSetRecall.Size = new System.Drawing.Size(217, 16);
			this.checkSetRecall.TabIndex = 5;
			this.checkSetRecall.Text = "Triggers Recall";
			// 
			// checkNoBillIns
			// 
			this.checkNoBillIns.Location = new System.Drawing.Point(56, 283);
			this.checkNoBillIns.Name = "checkNoBillIns";
			this.checkNoBillIns.Size = new System.Drawing.Size(301, 16);
			this.checkNoBillIns.TabIndex = 6;
			this.checkNoBillIns.Text = "Do not usually bill to insurance";
			// 
			// listCategory
			// 
			this.listCategory.Location = new System.Drawing.Point(611, 31);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(120, 238);
			this.listCategory.TabIndex = 3;
			// 
			// tbFees
			// 
			this.tbFees.BackColor = System.Drawing.SystemColors.Window;
			this.tbFees.Location = new System.Drawing.Point(741, 29);
			this.tbFees.Name = "tbFees";
			this.tbFees.SelectedIndices = new int[0];
			this.tbFees.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbFees.Size = new System.Drawing.Size(169, 356);
			this.tbFees.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(262, 590);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(272, 58);
			this.label3.TabIndex = 28;
			this.label3.Text = "There is no way to delete a code once created because if might have been used som" +
				"eplace.  Instead, move it to a category like \"obsolete\"";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(730, 651);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(178, 28);
			this.label9.TabIndex = 29;
			this.label9.Text = "Even if you press cancel, changes to fees will not be undone.";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbTime
			// 
			this.tbTime.BackColor = System.Drawing.SystemColors.Window;
			this.tbTime.Location = new System.Drawing.Point(14, 46);
			this.tbTime.Name = "tbTime";
			this.tbTime.SelectedIndices = new int[0];
			this.tbTime.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbTime.Size = new System.Drawing.Size(15, 561);
			this.tbTime.TabIndex = 30;
			// 
			// butSlider
			// 
			this.butSlider.BackColor = System.Drawing.SystemColors.ControlDark;
			this.butSlider.Location = new System.Drawing.Point(16, 86);
			this.butSlider.Name = "butSlider";
			this.butSlider.Size = new System.Drawing.Size(12, 15);
			this.butSlider.TabIndex = 31;
			this.butSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseUp);
			this.butSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseMove);
			this.butSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseDown);
			// 
			// textTime2
			// 
			this.textTime2.Location = new System.Drawing.Point(14, 612);
			this.textTime2.Name = "textTime2";
			this.textTime2.Size = new System.Drawing.Size(60, 20);
			this.textTime2.TabIndex = 32;
			this.textTime2.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(80, 616);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(46, 16);
			this.label11.TabIndex = 33;
			this.label11.Text = "Minutes";
			// 
			// checkIsHygiene
			// 
			this.checkIsHygiene.Location = new System.Drawing.Point(56, 308);
			this.checkIsHygiene.Name = "checkIsHygiene";
			this.checkIsHygiene.Size = new System.Drawing.Size(284, 18);
			this.checkIsHygiene.TabIndex = 7;
			this.checkIsHygiene.Text = "Is Hygiene procedure";
			// 
			// listGraphicType
			// 
			this.listGraphicType.Location = new System.Drawing.Point(474, 31);
			this.listGraphicType.Name = "listGraphicType";
			this.listGraphicType.Size = new System.Drawing.Size(118, 238);
			this.listGraphicType.TabIndex = 34;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(472, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 18);
			this.label2.TabIndex = 35;
			this.label2.Text = "Graphic Type";
			// 
			// butNone
			// 
			this.butNone.Location = new System.Drawing.Point(474, 273);
			this.butNone.Name = "butNone";
			this.butNone.TabIndex = 36;
			this.butNone.Text = "None";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// FormProcCodeEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(941, 707);
			this.ControlBox = false;
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listGraphicType);
			this.Controls.Add(this.checkIsHygiene);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textTime2);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.textAbbrev);
			this.Controls.Add(this.textADACode);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.butSlider);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbFees);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.checkNoBillIns);
			this.Controls.Add(this.checkSetRecall);
			this.Controls.Add(this.checkRemoveTth);
			this.Controls.Add(this.listTreatArea);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Name = "FormProcCodeEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Procedure Code";
			this.Load += new System.EventHandler(this.FormProcCodeEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProcCodeEdit_Load(object sender, System.EventArgs e) {
			if(IsNew){
				ProcCodes.Cur=new ProcedureCode();
				ProcCodes.Cur.ADACode=NewADA;
				ProcCodes.Cur.ProcTime="/X/";
				ProcCodes.Cur.ProcCat=Defs.Short[(int)DefCat.ProcCodeCats][0].DefNum;
				ProcCodes.InsertCur();
			}
			else{
				;
			}
			textADACode.Text=ProcCodes.Cur.ADACode;
			textDescription.Text=ProcCodes.Cur.Descript;
			textAbbrev.Text=ProcCodes.Cur.AbbrDesc;
			strBTime=new StringBuilder(ProcCodes.Cur.ProcTime);
			checkRemoveTth.Checked=ProcCodes.Cur.RemoveTooth;
			checkSetRecall.Checked=ProcCodes.Cur.SetRecall;
			checkNoBillIns.Checked=ProcCodes.Cur.NoBillIns;
			//checkIsProsth.Checked=ProcCodes.Cur.IsProsth;
			checkIsHygiene.Checked=ProcCodes.Cur.IsHygiene;
			textNote.Text=ProcCodes.Cur.DefaultNote;
			listTreatArea.SelectedIndex=(int)ProcCodes.Cur.TreatArea-1;
			if(listTreatArea.SelectedIndex==-1) listTreatArea.SelectedIndex=2;
			for(int i=0;i<GraphicTypes.List.Length;i++){
				listGraphicType.Items.Add(GraphicTypes.List[i].Description);
				if(GraphicTypes.List[i].GTypeNum==ProcCodes.Cur.GTypeNum)
					listGraphicType.SelectedIndex=i;
			}
			for(int i=0;i<Defs.Short[(int)DefCat.ProcCodeCats].Length;i++){
				listCategory.Items.Add(Defs.Short[(int)DefCat.ProcCodeCats][i].ItemName);
				if(Defs.Short[(int)DefCat.ProcCodeCats][i].DefNum==ProcCodes.Cur.ProcCat)
					listCategory.SelectedIndex=i;
			}
			if(listCategory.SelectedIndex==-1)
				listCategory.SelectedIndex=0;
			FillTime();
			FillFees();
		}

		private void FillTime(){
			for (int i=0;i<strBTime.Length;i++){
				tbTime.Cell[0,i]=strBTime.ToString(i,1);
				tbTime.BackGColor[0,i]=Color.White;
			}
			for (int i=strBTime.Length;i<tbTime.MaxRows;i++){
				tbTime.Cell[0,i]="";
				tbTime.BackGColor[0,i]=Color.FromName("Control");
			}
			tbTime.Refresh();
			butSlider.Location=new Point(tbTime.Location.X+2
				,(tbTime.Location.Y+strBTime.Length*14+1));
			textTime2.Text=strBTime.Length.ToString()+"0";
		}

		private void FillFees(){
			Fee temp;
			tbFees.ResetRows(Defs.Short[(int)DefCat.FeeSchedNames].Length);
			tbFees.SetGridColor(Color.LightGray);
			for(int i=0;i<tbFees.MaxRows;i++){
				temp=Fees.GetFeeByOrder(ProcCodes.Cur.ADACode,i);
				tbFees.Cell[0,i]=Defs.Short[(int)DefCat.FeeSchedNames][i].ItemName;
				tbFees.Cell[1,i]=temp.Amount.ToString("F");
				//if(temp.UseDefaultFee)tbFees.Cell[2,i]="X";
				//else tbFees.Cell[2,i]="";
				//if(temp.UseDefaultCov)tbFees.Cell[3,i]="X";
				//else tbFees.Cell[3,i]="";
			}
			tbFees.LayoutTables();
		}

		private void tbFees_CellClicked(object sender, CellEventArgs e){
			Fees.Cur=Fees.GetFeeByOrder(ProcCodes.Cur.ADACode,e.Row);
			tbFees.SelectedRow=e.Row;
			tbFees.ColorRow(e.Row,Color.LightGray);
			FormFeeEdit FormFeeEdit2=new FormFeeEdit();
			FormFeeEdit2.ShowDialog();
			if(FormFeeEdit2.DialogResult!=DialogResult.OK){
				tbFees.SelectedRow=-1;
				tbFees.ColorRow(e.Row,Color.White);
				tbFees.Refresh();
				return;
			}
			if(Fees.Cur.FeeNum==0){
				Fees.Cur.ADACode=ProcCodes.Cur.ADACode;
				Fees.Cur.FeeSched=Defs.Short[(int)DefCat.FeeSchedNames][e.Row].DefNum;
				//MessageBox.Show("inserting new record");
				Fees.InsertCur();
			}
			else{
				//MessageBox.Show("updating existing record");
				Fees.UpdateCur();
			}
			Fees.Refresh();
			FeeChanged=true;
			tbFees.SelectedRow=-1;
			FillFees();
		}

		private void tbTime_CellClicked(object sender, CellEventArgs e){
			if(e.Row<strBTime.Length){
				if(strBTime[e.Row]=='/'){
					strBTime.Replace('/','X',e.Row,1);
				}
				else{
					strBTime.Replace(strBTime[e.Row],'/',e.Row,1);
				}
			}
			FillTime();
		}

		private void butSlider_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=true;
			mouseOrigin=new Point(e.X+butSlider.Location.X
				,e.Y+butSlider.Location.Y);
			sliderOrigin=butSlider.Location;
			
		}

		private void butSlider_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown)return;
			//tempPoint represents the new location of button of smooth dragging.
			Point tempPoint=new Point(sliderOrigin.X
				,sliderOrigin.Y+(e.Y+butSlider.Location.Y)-mouseOrigin.Y);
			int step=(int)(Math.Round((Decimal)(tempPoint.Y-tbTime.Location.Y)/14));
			if(step==strBTime.Length)return;
			if(step<1)return;
			if(step>tbTime.MaxRows-1) return;
			if(step>strBTime.Length){
				strBTime.Append('/');
			}
			if(step<strBTime.Length){
				strBTime.Remove(step,1);
			}
			FillTime();
		}

		private void butSlider_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=false;
		}

		private void butNone_Click(object sender, System.EventArgs e) {
			listGraphicType.SelectedIndex=-1;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			ProcCodes.Cur.Descript=textDescription.Text;
			ProcCodes.Cur.AbbrDesc=textAbbrev.Text;
			ProcCodes.Cur.ProcTime=strBTime.ToString();
			ProcCodes.Cur.RemoveTooth=checkRemoveTth.Checked;
			ProcCodes.Cur.SetRecall=checkSetRecall.Checked;
			ProcCodes.Cur.NoBillIns=checkNoBillIns.Checked;
			//ProcCodes.Cur.IsProsth=checkIsProsth.Checked;
			ProcCodes.Cur.IsHygiene=checkIsHygiene.Checked;
			ProcCodes.Cur.DefaultNote=textNote.Text;
			if(listGraphicType.SelectedIndex==-1)
				ProcCodes.Cur.GTypeNum=0;
			else
				ProcCodes.Cur.GTypeNum=GraphicTypes.List[listGraphicType.SelectedIndex].GTypeNum;
			ProcCodes.Cur.TreatArea=(TreatmentArea)listTreatArea.SelectedIndex+1;
			if(listCategory.SelectedIndex!=-1)
				ProcCodes.Cur.ProcCat=Defs.Short[(int)DefCat.ProcCodeCats][listCategory.SelectedIndex].DefNum;
			if(IsNew){
				ProcCodes.UpdateCur();
			}
			else{
				ProcCodes.UpdateCur();
			}
			DialogResult=DialogResult.OK;
			Close();
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			if(FeeChanged) DialogResult=DialogResult.OK;
			else DialogResult=DialogResult.Cancel;
			Close();
		}

		

		

		

	}
}
