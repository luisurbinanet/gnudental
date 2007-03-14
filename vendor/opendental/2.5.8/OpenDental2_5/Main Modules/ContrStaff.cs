/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace OpenDental
{
	/// <summary>
	/// Summary description for ContrStaff.
	/// </summary>
	public class ContrStaff : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Button butViewAmbr;
		private System.Windows.Forms.GroupBox groupJordan;
		private System.Windows.Forms.CheckBox checkAmbr;
		private System.Windows.Forms.CheckBox checkBonn;
		private System.Windows.Forms.CheckBox checkBrit;
		private System.Windows.Forms.CheckBox checkDust;
		private System.Windows.Forms.CheckBox checkLori;
		private System.Windows.Forms.CheckBox checkKimm;
		private System.Windows.Forms.Button butViewJordan;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button butViewBonn;
		private System.Windows.Forms.Button butViewBrit;
		private System.Windows.Forms.Button butViewDust;
		private System.Windows.Forms.Button butViewKimm;
		private System.Windows.Forms.Button butViewLori;
		private System.Windows.Forms.CheckBox checkAft;
		private System.Windows.Forms.CheckBox checkMorn;
		private System.Windows.Forms.TextBox textAft;
		private System.Windows.Forms.TextBox textMorn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;

		///<summary></summary>
		public ContrStaff(){
			InitializeComponent();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.checkAft = new System.Windows.Forms.CheckBox();
			this.checkMorn = new System.Windows.Forms.CheckBox();
			this.textAft = new System.Windows.Forms.TextBox();
			this.textMorn = new System.Windows.Forms.TextBox();
			this.butViewAmbr = new System.Windows.Forms.Button();
			this.groupJordan = new System.Windows.Forms.GroupBox();
			this.butViewJordan = new System.Windows.Forms.Button();
			this.checkKimm = new System.Windows.Forms.CheckBox();
			this.checkLori = new System.Windows.Forms.CheckBox();
			this.checkDust = new System.Windows.Forms.CheckBox();
			this.checkBrit = new System.Windows.Forms.CheckBox();
			this.checkBonn = new System.Windows.Forms.CheckBox();
			this.checkAmbr = new System.Windows.Forms.CheckBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.butViewBonn = new System.Windows.Forms.Button();
			this.butViewBrit = new System.Windows.Forms.Button();
			this.butViewDust = new System.Windows.Forms.Button();
			this.butViewKimm = new System.Windows.Forms.Button();
			this.butViewLori = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupJordan.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkAft
			// 
			this.checkAft.Location = new System.Drawing.Point(204, 176);
			this.checkAft.Name = "checkAft";
			this.checkAft.Size = new System.Drawing.Size(72, 24);
			this.checkAft.TabIndex = 4;
			this.checkAft.Text = "Afternoon";
			this.checkAft.Click += new System.EventHandler(this.checkAft_Click);
			// 
			// checkMorn
			// 
			this.checkMorn.Location = new System.Drawing.Point(204, 144);
			this.checkMorn.Name = "checkMorn";
			this.checkMorn.Size = new System.Drawing.Size(64, 24);
			this.checkMorn.TabIndex = 3;
			this.checkMorn.Text = "Morning";
			this.checkMorn.Click += new System.EventHandler(this.checkMorn_Click);
			// 
			// textAft
			// 
			this.textAft.Location = new System.Drawing.Point(128, 176);
			this.textAft.Name = "textAft";
			this.textAft.ReadOnly = true;
			this.textAft.Size = new System.Drawing.Size(60, 20);
			this.textAft.TabIndex = 2;
			this.textAft.Text = "";
			this.textAft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textMorn
			// 
			this.textMorn.Location = new System.Drawing.Point(128, 144);
			this.textMorn.Name = "textMorn";
			this.textMorn.ReadOnly = true;
			this.textMorn.Size = new System.Drawing.Size(60, 20);
			this.textMorn.TabIndex = 1;
			this.textMorn.Text = "";
			this.textMorn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// butViewAmbr
			// 
			this.butViewAmbr.Location = new System.Drawing.Point(84, 72);
			this.butViewAmbr.Name = "butViewAmbr";
			this.butViewAmbr.TabIndex = 0;
			this.butViewAmbr.Text = "Amber";
			this.butViewAmbr.Click += new System.EventHandler(this.butViewAmbr_Click);
			// 
			// groupJordan
			// 
			this.groupJordan.Controls.AddRange(new System.Windows.Forms.Control[] {
																																							this.butViewJordan,
																																							this.checkKimm,
																																							this.checkLori,
																																							this.checkDust,
																																							this.checkBrit,
																																							this.checkBonn,
																																							this.checkAmbr,
																																							this.label1});
			this.groupJordan.Location = new System.Drawing.Point(404, 48);
			this.groupJordan.Name = "groupJordan";
			this.groupJordan.Size = new System.Drawing.Size(224, 268);
			this.groupJordan.TabIndex = 10;
			this.groupJordan.TabStop = false;
			this.groupJordan.Text = "All";
			// 
			// butViewJordan
			// 
			this.butViewJordan.Location = new System.Drawing.Point(76, 232);
			this.butViewJordan.Name = "butViewJordan";
			this.butViewJordan.TabIndex = 6;
			this.butViewJordan.Text = "View";
			this.butViewJordan.Click += new System.EventHandler(this.butViewJordan_Click);
			// 
			// checkKimm
			// 
			this.checkKimm.Location = new System.Drawing.Point(76, 168);
			this.checkKimm.Name = "checkKimm";
			this.checkKimm.Size = new System.Drawing.Size(52, 24);
			this.checkKimm.TabIndex = 5;
			this.checkKimm.Text = "Kim";
			// 
			// checkLori
			// 
			this.checkLori.Location = new System.Drawing.Point(76, 192);
			this.checkLori.Name = "checkLori";
			this.checkLori.Size = new System.Drawing.Size(52, 24);
			this.checkLori.TabIndex = 4;
			this.checkLori.Text = "Lori";
			// 
			// checkDust
			// 
			this.checkDust.Location = new System.Drawing.Point(76, 144);
			this.checkDust.Name = "checkDust";
			this.checkDust.Size = new System.Drawing.Size(56, 24);
			this.checkDust.TabIndex = 3;
			this.checkDust.Text = "Dusty";
			// 
			// checkBrit
			// 
			this.checkBrit.Location = new System.Drawing.Point(76, 120);
			this.checkBrit.Name = "checkBrit";
			this.checkBrit.Size = new System.Drawing.Size(64, 24);
			this.checkBrit.TabIndex = 2;
			this.checkBrit.Text = "Brittany";
			// 
			// checkBonn
			// 
			this.checkBonn.Location = new System.Drawing.Point(76, 96);
			this.checkBonn.Name = "checkBonn";
			this.checkBonn.Size = new System.Drawing.Size(72, 24);
			this.checkBonn.TabIndex = 1;
			this.checkBonn.Text = "Bonnie";
			// 
			// checkAmbr
			// 
			this.checkAmbr.Location = new System.Drawing.Point(76, 72);
			this.checkAmbr.Name = "checkAmbr";
			this.checkAmbr.Size = new System.Drawing.Size(64, 24);
			this.checkAmbr.TabIndex = 0;
			this.checkAmbr.Text = "Amber";
			// 
			// timer1
			// 
			this.timer1.Interval = 2000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// butViewBonn
			// 
			this.butViewBonn.Location = new System.Drawing.Point(168, 72);
			this.butViewBonn.Name = "butViewBonn";
			this.butViewBonn.TabIndex = 11;
			this.butViewBonn.Text = "Bonnie";
			this.butViewBonn.Click += new System.EventHandler(this.butViewBonn_Click);
			// 
			// butViewBrit
			// 
			this.butViewBrit.Location = new System.Drawing.Point(252, 72);
			this.butViewBrit.Name = "butViewBrit";
			this.butViewBrit.TabIndex = 12;
			this.butViewBrit.Text = "Brittany";
			this.butViewBrit.Click += new System.EventHandler(this.butViewBrit_Click);
			// 
			// butViewDust
			// 
			this.butViewDust.Location = new System.Drawing.Point(84, 104);
			this.butViewDust.Name = "butViewDust";
			this.butViewDust.TabIndex = 13;
			this.butViewDust.Text = "Dusty";
			this.butViewDust.Click += new System.EventHandler(this.butViewDust_Click);
			// 
			// butViewKimm
			// 
			this.butViewKimm.Location = new System.Drawing.Point(168, 104);
			this.butViewKimm.Name = "butViewKimm";
			this.butViewKimm.TabIndex = 14;
			this.butViewKimm.Text = "Kim";
			this.butViewKimm.Click += new System.EventHandler(this.butViewKimm_Click);
			// 
			// butViewLori
			// 
			this.butViewLori.Location = new System.Drawing.Point(252, 104);
			this.butViewLori.Name = "butViewLori";
			this.butViewLori.TabIndex = 15;
			this.butViewLori.Text = "Lori";
			this.butViewLori.Click += new System.EventHandler(this.butViewLori_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(204, 40);
			this.label1.TabIndex = 16;
			this.label1.Text = "This is for everyone\'s use.  You can use the View button to see who is unavailabl" +
				"e because they are on break.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(84, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(208, 28);
			this.label2.TabIndex = 17;
			this.label2.Text = "These buttons are private.  You should not view times other than your own";
			// 
			// ContrStaff
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.label2,
																																	this.butViewLori,
																																	this.butViewKimm,
																																	this.butViewDust,
																																	this.butViewBrit,
																																	this.butViewBonn,
																																	this.groupJordan,
																																	this.butViewAmbr,
																																	this.textAft,
																																	this.textMorn,
																																	this.checkAft,
																																	this.checkMorn});
			this.Name = "ContrStaff";
			this.Size = new System.Drawing.Size(668, 536);
			this.groupJordan.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void InstantClasses(){
			//Breaks2 = new Breaks();
			//Break2 = new Break();
		}

		///<summary></summary>
		public void ModuleSelected(){

		}

		private void checkMorn_Click(object sender, System.EventArgs e) {
			/*
			timer1.Stop();
			timer1.Start();
			Break2 = Breaks2.Cur;
			TimeSpan delta;
			if (checkMorn.Checked==true){
				Break2.MornStart = DateTime.Now.ToString("HH:mm:ss");
				Break2.MornBool = true;
			}
			else{
				delta = TimeSpan.Parse(Breaks2.Cur.MornRem)
					-(DateTime.Now-DateTime.Parse(Breaks2.Cur.MornStart));
				Break2.MornRem=Breaks2.SpanToS(true,delta);
				Break2.MornBool = false;
			}
			Breaks2.Cur = Break2;
			Breaks2.Update();*/
		}

		private void checkAft_Click(object sender, System.EventArgs e) {
			/*
			timer1.Stop();
			timer1.Start();
			Break2 = Breaks2.Cur;
			TimeSpan delta;
			if (checkAft.Checked==true){//Going on break
				Break2.AftStart = DateTime.Now.ToString("HH:mm:ss");
				Break2.AftBool = true;
			}
			else{//Coming off of break
				delta = TimeSpan.Parse(Breaks2.Cur.AftRem)
					-(DateTime.Now-DateTime.Parse(Breaks2.Cur.AftStart));
				Break2.AftRem=Breaks2.SpanToS(true,delta);
				Break2.AftBool = false;
			}
			Breaks2.Cur = Break2;
			Breaks2.Update()*/
		}

		private void ViewData(string myEmp){/*
			timer1.Stop();
			timer1.Start();
			Breaks2.Refresh(myEmp);
			if (DateTime.Compare(Breaks2.Cur.EntryDate,DateTime.Now.Date)!=0){
				//if time is negative, send a report
				bool BoolNeg=false;
				if (TimeSpan.Parse(Breaks2.Cur.MornRem).CompareTo(TimeSpan.FromMinutes(-1))==-1) BoolNeg = true;
				if (TimeSpan.Parse(Breaks2.Cur.AftRem).CompareTo(TimeSpan.FromMinutes(-1))==-1) BoolNeg = true;
				if (BoolNeg==true){
					Generic Generic2 = new Generic();
					Gen Gen2 = new Gen();
					Gen2.CatNum = 3;
					Gen2.Item = Breaks2.Cur.EntryDate.ToString("d");
					Gen2.Value = myEmp+","+Breaks2.Cur.MornRem+","+Breaks2.Cur.AftRem;
					Generic2.Cur = Gen2;
					Generic2.AddItem();
				}
				//reset
				Break2 = Breaks2.Cur;
				Break2.EntryDate=DateTime.Now;
				Break2.AftBool=false;
				Break2.MornBool=false;
				Break2.MornRem="00:20:00";
				Break2.AftRem="00:10:00";
				Breaks2.Cur = Break2;
				Breaks2.Update();
			}
			//morn
			TimeSpan delta;
			if (Breaks2.Cur.MornBool==true){//if on break
				delta = TimeSpan.Parse(Breaks2.Cur.MornRem)
					-(DateTime.Now-DateTime.Parse(Breaks2.Cur.MornStart));
				textMorn.Text = Breaks2.SpanToS(false,delta);				
			}
			else{//not on break
				textMorn.Text=Breaks2.SpanToS(false,TimeSpan.Parse(Breaks2.Cur.MornRem));
			}
			//aft
			if (Breaks2.Cur.AftBool==true){//if on break
				delta = TimeSpan.Parse(Breaks2.Cur.AftRem)
					-(DateTime.Now-DateTime.Parse(Breaks2.Cur.AftStart));
				textAft.Text = Breaks2.SpanToS(false,delta);				
			}
			else{//not on break
				textAft.Text=Breaks2.SpanToS(false,TimeSpan.Parse(Breaks2.Cur.AftRem));
			}
			//checkboxes
			if (Breaks2.Cur.MornBool==true) checkMorn.Checked=true;
			else checkMorn.Checked=false;
			if (Breaks2.Cur.AftBool==true) checkAft.Checked=true;
			else checkAft.Checked=false;
			checkMorn.Enabled=true;
			checkAft.Enabled=true;*/
		}

		private void butViewAmbr_Click(object sender, System.EventArgs e) {
			ViewData("Amber");
		}

		private void butViewBonn_Click(object sender, System.EventArgs e) {
			ViewData("Bonnie");
		}

		private void butViewBrit_Click(object sender, System.EventArgs e) {
			ViewData("Brittany");
		}

		private void butViewDust_Click(object sender, System.EventArgs e) {
			ViewData("Dusty");
		}

		private void butViewKimm_Click(object sender, System.EventArgs e) {
			ViewData("Kim");
		}

		private void butViewLori_Click(object sender, System.EventArgs e) {
			ViewData("Lori");
		}

		private void butViewJordan_Click(object sender, System.EventArgs e) {/*
			Breaks2.Refresh("Amber");
			if (Breaks2.Cur.MornBool==true) checkAmbr.Checked=true;
			else if (Breaks2.Cur.AftBool==true) checkAmbr.Checked=true;
			else checkAmbr.Checked=false;
			Breaks2.Refresh("Bonnie");
			if (Breaks2.Cur.MornBool==true) checkBonn.Checked=true;
			else if (Breaks2.Cur.AftBool==true) checkBonn.Checked=true;
			else checkBonn.Checked=false;
			Breaks2.Refresh("Brittany");
			if (Breaks2.Cur.MornBool==true) checkBrit.Checked=true;
			else if (Breaks2.Cur.AftBool==true) checkBrit.Checked=true;
			else checkBrit.Checked=false;
			Breaks2.Refresh("Dusty");
			if (Breaks2.Cur.MornBool==true) checkDust.Checked=true;
			else if (Breaks2.Cur.AftBool==true) checkDust.Checked=true;
			else checkDust.Checked=false;
			Breaks2.Refresh("Kim");
			if (Breaks2.Cur.MornBool==true) checkKimm.Checked=true;
			else if (Breaks2.Cur.AftBool==true) checkKimm.Checked=true;
			else checkKimm.Checked=false;
			Breaks2.Refresh("Lori");
			if (Breaks2.Cur.MornBool==true) checkLori.Checked=true;
			else if (Breaks2.Cur.AftBool==true) checkLori.Checked=true;
			else checkLori.Checked=false;*/
		}

		private void timer1_Tick(object sender, System.EventArgs e) {
			timer1.Stop();
			textMorn.Text="";
			textAft.Text="";
			checkMorn.Checked=false;
			checkMorn.Enabled=false;
			checkAft.Checked=false;
			checkAft.Enabled=false;
		}

		

		
		




	}
}
