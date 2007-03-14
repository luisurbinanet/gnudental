using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormContacts : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listCategory;
		private System.Windows.Forms.Button butAdd;
		private OpenDental.TableContacts tbContacts;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormContacts()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			tbContacts.CellDoubleClicked
				+= new OpenDental.ContrTable.CellEventHandler(tbContacts_CellDoubleClicked);
			Lan.C(this, new System.Windows.Forms.Control[] {
				label1
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butAdd
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormContacts));
			this.butOK = new System.Windows.Forms.Button();
			this.listCategory = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbContacts = new OpenDental.TableContacts();
			this.butAdd = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butOK.Location = new System.Drawing.Point(799, 646);
			this.butOK.Name = "butOK";
			this.butOK.TabIndex = 1;
			this.butOK.Text = "Close";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listCategory
			// 
			this.listCategory.Location = new System.Drawing.Point(5, 37);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(101, 264);
			this.listCategory.TabIndex = 2;
			this.listCategory.SelectedIndexChanged += new System.EventHandler(this.listCategory_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Category";
			// 
			// tbContacts
			// 
			this.tbContacts.BackColor = System.Drawing.SystemColors.Window;
			this.tbContacts.Location = new System.Drawing.Point(117, 6);
			this.tbContacts.Name = "tbContacts";
			this.tbContacts.SelectedIndices = new int[0];
			this.tbContacts.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbContacts.Size = new System.Drawing.Size(669, 681);
			this.tbContacts.TabIndex = 4;
			// 
			// butAdd
			// 
			this.butAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAdd.Location = new System.Drawing.Point(798, 525);
			this.butAdd.Name = "butAdd";
			this.butAdd.TabIndex = 5;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormContacts
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(883, 691);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.tbContacts);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormContacts";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Contacts";
			this.Load += new System.EventHandler(this.FormContacts_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormContacts_Load(object sender, System.EventArgs e) {
			for(int i=0;i<Defs.Short[(int)DefCat.ContactCategories].Length;i++){
				listCategory.Items.Add(Defs.Short[(int)DefCat.ContactCategories][i].ItemName);
			}
			if(listCategory.Items.Count>0)
				listCategory.SelectedIndex=0;
		}

		private void listCategory_SelectedIndexChanged(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			Contacts.Refresh(Defs.Short[(int)DefCat.ContactCategories][listCategory.SelectedIndex].DefNum);
			tbContacts.ResetRows(Contacts.List.Length);
			tbContacts.SetGridColor(Color.Gray);
			tbContacts.SetBackGColor(Color.White);      
			for(int i=0;i<Contacts.List.Length;i++){
				tbContacts.Cell[0,i]=Contacts.List[i].LName;
				tbContacts.Cell[1,i]=Contacts.List[i].FName;  
				tbContacts.Cell[2,i]=Contacts.List[i].WkPhone;   
				tbContacts.Cell[3,i]=Contacts.List[i].Fax;   
				tbContacts.Cell[4,i]=Contacts.List[i].Notes;   
			}
			tbContacts.LayoutTables();  
		}

		private void tbContacts_CellDoubleClicked(object sender, CellEventArgs e){
			Contacts.Cur=Contacts.List[e.Row];
			FormContactEdit FormCE=new FormContactEdit();
			FormCE.ShowDialog();
			if(FormCE.DialogResult==DialogResult.OK)
				FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Contacts.Cur=new Contact();
			Contacts.Cur.Category=Defs.Short[(int)DefCat.ContactCategories][listCategory.SelectedIndex].DefNum;
			FormContactEdit FormCE=new FormContactEdit();
			FormCE.IsNew=true;
			FormCE.ShowDialog();
			if(FormCE.DialogResult==DialogResult.OK)
				FillGrid();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


		


	}
}





















