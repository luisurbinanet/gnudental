using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormAutoCodeEdit : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.CheckBox checkHidden;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.TextBox textDescript;
		private System.Windows.Forms.Label label1;
		private OpenDental.TableAutoItem tbAutoItem;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private OpenDental.XPButton butDelete;
		private OpenDental.XPButton butAdd;
		///<summary></summary>
    public bool IsNew;

		///<summary></summary>
		public FormAutoCodeEdit(){
			InitializeComponent();
      tbAutoItem.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbAutoItem_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
			  this.checkHidden,
				this.label1,
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
			  butOK,
			  butCancel,
				butAdd,
				butDelete
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAutoCodeEdit));
			this.checkHidden = new System.Windows.Forms.CheckBox();
			this.butCancel = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.textDescript = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbAutoItem = new OpenDental.TableAutoItem();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.XPButton();
			this.butAdd = new OpenDental.XPButton();
			this.SuspendLayout();
			// 
			// checkHidden
			// 
			this.checkHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidden.Location = new System.Drawing.Point(454, 20);
			this.checkHidden.Name = "checkHidden";
			this.checkHidden.Size = new System.Drawing.Size(124, 18);
			this.checkHidden.TabIndex = 1;
			this.checkHidden.Text = "Hidden";
			// 
			// butCancel
			// 
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCancel.Location = new System.Drawing.Point(682, 530);
			this.butCancel.Name = "butCancel";
			this.butCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.butCancel.TabIndex = 20;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(682, 496);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 19;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textDescript
			// 
			this.textDescript.Location = new System.Drawing.Point(148, 16);
			this.textDescript.Name = "textDescript";
			this.textDescript.Size = new System.Drawing.Size(210, 20);
			this.textDescript.TabIndex = 22;
			this.textDescript.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(36, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 16);
			this.label1.TabIndex = 23;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbAutoItem
			// 
			this.tbAutoItem.BackColor = System.Drawing.SystemColors.Window;
			this.tbAutoItem.Location = new System.Drawing.Point(36, 94);
			this.tbAutoItem.Name = "tbAutoItem";
			this.tbAutoItem.ScrollValue = 1;
			this.tbAutoItem.SelectedIndices = new int[0];
			this.tbAutoItem.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbAutoItem.Size = new System.Drawing.Size(719, 356);
			this.tbAutoItem.TabIndex = 24;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(490, 530);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(172, 32);
			this.label2.TabIndex = 26;
			this.label2.Text = "Clicking Cancel does not undo changes already made to items.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(40, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(436, 18);
			this.label3.TabIndex = 27;
			this.label3.Text = "You may have duplicate ADA codes  in the following list.";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butDelete.Image = ((System.Drawing.Image)(resources.GetObject("butDelete.Image")));
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(140, 478);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(82, 26);
			this.butDelete.TabIndex = 29;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butAdd.Image = ((System.Drawing.Image)(resources.GetObject("butAdd.Image")));
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(36, 478);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(80, 26);
			this.butAdd.TabIndex = 28;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormAutoCodeEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(794, 582);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbAutoItem);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDescript);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.checkHidden);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutoCodeEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormAutoCodeEdit";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormAutoCodeEdit_Closing);
			this.Load += new System.EventHandler(this.FormAutoCodeEdit_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAutoCodeEdit_Load(object sender, System.EventArgs e) {      
      if(IsNew){
        this.Text=Lan.g(this,"Add Auto Code");
        AutoCodes.Cur=new AutoCode();
        AutoCodes.InsertCur();
      }
      else{
        this.Text=Lan.g(this,"Edit Auto Code");
        textDescript.Text=AutoCodes.Cur.Description;
        checkHidden.Checked=AutoCodes.Cur.IsHidden;
      }
		  FillTable();
		}

    private void FillTable(){
      int count=0;
      AutoCodeItems.Refresh();
      AutoCodeConds.Refresh();
      AutoCodeItems.GetListForCode(AutoCodes.Cur.AutoCodeNum);
 			tbAutoItem.ResetRows(AutoCodeItems.ListForCode.Length);
			tbAutoItem.SetGridColor(Color.Gray);
			tbAutoItem.SetBackGColor(Color.White);      
			for(int i=0;i<AutoCodeItems.ListForCode.Length;i++){
        tbAutoItem.Cell[0,i]=AutoCodeItems.ListForCode[i].ADACode;
				tbAutoItem.Cell[1,i]=ProcedureCodes.GetProcCode(AutoCodeItems.ListForCode[i].ADACode).Descript;
        count=0;
        for(int j=0;j<AutoCodeConds.List.Length;j++){
          if(AutoCodeConds.List[j].AutoCodeItemNum==AutoCodeItems.ListForCode[i].AutoCodeItemNum){
						if(count!=0){
							tbAutoItem.Cell[2,i]+=", ";
						}
						tbAutoItem.Cell[2,i]+=AutoCodeConds.List[j].Condition.ToString();
            count++;
          }
        }
			}
			tbAutoItem.LayoutTables();  
    }

    private void tbAutoItem_CellDoubleClicked(object sender, CellEventArgs e){
      AutoCodeItems.Cur=AutoCodeItems.ListForCode[tbAutoItem.SelectedRow];
      FormAutoItemEdit FormAIE=new FormAutoItemEdit();
      FormAIE.ShowDialog();
      FillTable(); 
    }

		private void butAdd_Click(object sender, System.EventArgs e) {
		  FormAutoItemEdit FormAIE=new FormAutoItemEdit();
      FormAIE.IsNew=true;
      FormAIE.ShowDialog();
      FillTable();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(tbAutoItem.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
        return;
			}
			AutoCodeItems.Cur=AutoCodeItems.ListForCode[tbAutoItem.SelectedRow];
      AutoCodeConds.DeleteForItemNum(AutoCodeItems.Cur.AutoCodeItemNum);
      AutoCodeItems.DeleteCur();
			FillTable();
		}  

		private void butOK_Click(object sender, System.EventArgs e) {
		  if(textDescript.Text==""){
        MessageBox.Show(Lan.g(this,"The Description cannot be blank"));
        return;
      }
      AutoCodes.Cur.Description=textDescript.Text;
      AutoCodes.Cur.IsHidden=checkHidden.Checked;
			AutoCodes.UpdateCur();
      DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {	
      DialogResult=DialogResult.Cancel;
		}

		private void FormAutoCodeEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
        for(int i=0;i<AutoCodeItems.ListForCode.Length;i++){
          //if(AutoCodes.Cur.AutoCodeNum==AutoCodeItems.ListForCode[i].AutoCodeNum){
          AutoCodeItems.Cur=AutoCodeItems.ListForCode[i];
          AutoCodeConds.DeleteForItemNum(AutoCodeItems.Cur.AutoCodeItemNum);
          AutoCodeItems.DeleteCur();
          //}
        }
        AutoCodes.DeleteCur();
      }
		}

		  

	}
}
