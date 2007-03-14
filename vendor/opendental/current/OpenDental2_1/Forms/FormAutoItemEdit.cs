using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{

	public class FormAutoItemEdit : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textADA;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listConditions;
		private System.Windows.Forms.Button butChange;
    public bool IsNew;

		public FormAutoItemEdit(){
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[] {
			  this.label1,
				this.label2,
				butChange,
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
			this.textADA = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listConditions = new System.Windows.Forms.ListBox();
			this.butChange = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textADA
			// 
			this.textADA.Location = new System.Drawing.Point(82, 54);
			this.textADA.Name = "textADA";
			this.textADA.ReadOnly = true;
			this.textADA.TabIndex = 0;
			this.textADA.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(20, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "ADA Code";
			// 
			// listConditions
			// 
			this.listConditions.Location = new System.Drawing.Point(306, 56);
			this.listConditions.Name = "listConditions";
			this.listConditions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listConditions.Size = new System.Drawing.Size(166, 407);
			this.listConditions.TabIndex = 2;
			// 
			// butChange
			// 
			this.butChange.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butChange.Location = new System.Drawing.Point(186, 54);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(94, 20);
			this.butChange.TabIndex = 24;
			this.butChange.Text = "Change";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(490, 438);
			this.butCancel.Name = "butCancel";
			this.butCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.butCancel.TabIndex = 23;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(490, 404);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 22;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(360, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 12);
			this.label2.TabIndex = 25;
			this.label2.Text = "Conditions";
			// 
			// FormAutoItemEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(592, 490);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butChange);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.listConditions);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textADA);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutoItemEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormAutoItemEdit";
			this.Load += new System.EventHandler(this.FormAutoItemEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

 		private void FormAutoItemEdit_Load(object sender, System.EventArgs e) { 
      AutoCodeConds.Refresh();    
			if(IsNew){
        AutoCodeItems.Cur=new AutoCodeItem();
        AutoCodeItems.Cur.AutoCodeNum=AutoCodes.Cur.AutoCodeNum;
				this.Text=Lan.g(this,"Add Auto Code Item");  
			}
			else{ 
				this.Text=Lan.g(this,"Edit Auto Code Item");
				textADA.Text=AutoCodeItems.Cur.ADACode;    
			}
			FillList();
		}

    private void FillList(){
      listConditions.Items.Clear();
      foreach(string s in Enum.GetNames(typeof(AutoCondition))){
         listConditions.Items.Add(Lan.g("enumAutoConditions",s));
      }  
			for(int i=0;i<AutoCodeConds.List.Length;i++){
        if(AutoCodeConds.List[i].AutoCodeItemNum==AutoCodeItems.Cur.AutoCodeItemNum){
          listConditions.SetSelected((int)AutoCodeConds.List[i].Condition,true);
        }   
      }
    } 

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textADA.Text==""){
			  MessageBox.Show(Lan.g(this,"ADA Code cannot be left blank."));
        listConditions.SelectedIndex=-1;
				FillList();
				return;
      }
      AutoCodeItems.Cur.ADACode=textADA.Text;
      if(IsNew){
        AutoCodeItems.InsertCur();
      }
      else{
        AutoCodeItems.UpdateCur();
      } 
      AutoCodeConds.DeleteForItemNum(AutoCodeItems.Cur.AutoCodeItemNum);
      for(int i=0;i<listConditions.SelectedIndices.Count;i++){
        AutoCodeConds.Cur=new AutoCodeCond();
        AutoCodeConds.Cur.AutoCodeItemNum=AutoCodeItems.Cur.AutoCodeItemNum;
        AutoCodeConds.Cur.Condition=(AutoCondition)listConditions.SelectedIndices[i];
        AutoCodeConds.InsertCur(); 
      }
      DialogResult=DialogResult.OK;
		}

		private void butChange_Click(object sender, System.EventArgs e) {
			FormProcedures FormP=new FormProcedures();
      FormP.Mode=FormProcMode.Select;
      FormP.ShowDialog();
      if(FormP.DialogResult==DialogResult.Cancel){
        textADA.Text=AutoCodeItems.Cur.ADACode; 
      }
      else{
				if(AutoCodeItems.HList.ContainsKey(FormP.SelectedADA)
					&& (int)AutoCodeItems.HList[FormP.SelectedADA] != AutoCodes.Cur.AutoCodeNum){
					MessageBox.Show(Lan.g(this,"That ADA code is already in use in a different Auto Code.  Not allowed to use it here."));
					textADA.Text=AutoCodeItems.Cur.ADACode;
				}
				else{
					textADA.Text=FormP.SelectedADA;
				}
      }
		}

	}
}










