using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TableProcFee : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableProcFee(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=10;
			MaxCols=2;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,9,14);
			Heading=Lan.g("TableProcFee","Fees");
			Fields[0]=Lan.g("TableProcFee","Sched");
			Fields[1]=Lan.g("TableProcFee","Amount");
			ColAlign[1]=HorizontalAlignment.Right;
			ColWidth[0]=120;
			ColWidth[1]=60;
			LayoutTables();
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if (components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code

		private void InitializeComponent(){
			this.SuspendLayout();
			// 
			// TableProcFee
			// 
			this.Name = "TableProcFee";
			this.Load += new System.EventHandler(this.TableProcFee_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableProcFee_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}


	}
}

