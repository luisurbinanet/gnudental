using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.IO;

namespace OpenDental{
///<summary></summary>
	public class FormTranslation : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.TableLan tbLan;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butObsolete;
		private OpenDental.UI.Button butNot;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormTranslation(){
			InitializeComponent();
      tbLan.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbLan_CellDoubleClicked);
			tbLan.Heading=Lan.CurCat+" Words";
			tbLan.Fields[2]=CultureInfo.CurrentCulture.Parent.DisplayName;
			tbLan.Fields[3]=CultureInfo.CurrentCulture.Parent.DisplayName + " Comments";
			//no need to translate much here
			Lan.C("All", new System.Windows.Forms.Control[] {
				butClose,																											
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
			this.tbLan = new OpenDental.TableLan();
			this.butClose = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butObsolete = new OpenDental.UI.Button();
			this.butNot = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// tbLan
			// 
			this.tbLan.BackColor = System.Drawing.SystemColors.Window;
			this.tbLan.Location = new System.Drawing.Point(20, 23);
			this.tbLan.Name = "tbLan";
			this.tbLan.ScrollValue = 143;
			this.tbLan.SelectedIndices = new int[0];
			this.tbLan.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tbLan.Size = new System.Drawing.Size(859, 637);
			this.tbLan.TabIndex = 0;
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(827, 671);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 26);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19, 676);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(256, 18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Obsolete translations are shown in gray.";
			// 
			// butObsolete
			// 
			this.butObsolete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butObsolete.Autosize = true;
			this.butObsolete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butObsolete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butObsolete.Location = new System.Drawing.Point(386, 669);
			this.butObsolete.Name = "butObsolete";
			this.butObsolete.Size = new System.Drawing.Size(94, 26);
			this.butObsolete.TabIndex = 5;
			this.butObsolete.Text = "Set Obsolete";
			this.butObsolete.Visible = false;
			this.butObsolete.Click += new System.EventHandler(this.butObsolete_Click);
			// 
			// butNot
			// 
			this.butNot.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butNot.Autosize = true;
			this.butNot.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNot.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNot.Location = new System.Drawing.Point(510, 669);
			this.butNot.Name = "butNot";
			this.butNot.Size = new System.Drawing.Size(64, 26);
			this.butNot.TabIndex = 6;
			this.butNot.Text = "Not";
			this.butNot.Visible = false;
			this.butNot.Click += new System.EventHandler(this.butNot_Click);
			// 
			// FormTranslation
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(958, 708);
			this.Controls.Add(this.butNot);
			this.Controls.Add(this.butObsolete);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.tbLan);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTranslation";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Translation";
			this.Load += new System.EventHandler(this.FormLanguage_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormLanguage_Load(object sender, System.EventArgs e) {
			#if(DEBUG)
				butObsolete.Visible=true;
				butNot.Visible=true;
			#endif
			FillTable();
		}

		private void FillTable(){
			Lan.GetListForCat(Lan.CurCat);
			LanguageForeigns.Refresh();
			tbLan.ResetRows(Lan.ListForCat.Length);
			tbLan.SetGridColor(Color.Gray);
			for(int i=0;i<Lan.ListForCat.Length;i++){
				if(Lan.ListForCat[i].IsObsolete){
					//MessageBox.Show(i.ToString());
					tbLan.SetTextColorRow(i,SystemColors.GrayText);
				}
				tbLan.Cell[0,i]=Lan.ListForCat[i].English;
				tbLan.Cell[1,i]=Lan.ListForCat[i].EnglishComments;
				if(LanguageForeigns.HList.ContainsKey(Lan.ListForCat[i].ClassType+Lan.ListForCat[i].English)){
					tbLan.Cell[2,i]=((LanguageForeign)LanguageForeigns.HList[Lan.ListForCat[i].ClassType+Lan.ListForCat[i].English]).Translation;
					tbLan.Cell[3,i]=((LanguageForeign)LanguageForeigns.HList[Lan.ListForCat[i].ClassType+Lan.ListForCat[i].English]).Comments;
				}
				else{
					tbLan.Cell[2,i]="";
					tbLan.Cell[3,i]="";
				}
			}
			tbLan.LayoutTables(); 
			tbLan.SelectedRow=-1;			
		}

		private void tbLan_CellDoubleClicked(object sender, CellEventArgs e){
			Lan.Cur=Lan.ListForCat[e.Row];
			FormTranslationEdit FormTE=new FormTranslationEdit();
			FormTE.ShowDialog();
			FillTable();
		}

		private void butObsolete_Click(object sender, System.EventArgs e) {
			Language[] lanList=new Language[tbLan.SelectedIndices.Length];
			for(int i=0;i<lanList.Length;i++){
				lanList[i]=Lan.ListForCat[tbLan.SelectedIndices[i]];
			}
			Lan.SetObsolete(lanList,true);
			Lan.Refresh();
			FillTable();
		}

		private void butNot_Click(object sender, System.EventArgs e) {
			Language[] lanList=new Language[tbLan.SelectedIndices.Length];
			for(int i=0;i<lanList.Length;i++){
				lanList[i]=Lan.ListForCat[tbLan.SelectedIndices[i]];
			}
			Lan.SetObsolete(lanList,false);
			Lan.Refresh();
			FillTable();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
		
		}

	}
}
