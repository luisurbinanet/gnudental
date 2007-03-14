using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class TableAccountPat : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;
		//public int SelectedRow=0;

		///<summary></summary>
		public TableAccountPat(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			InstantClasses();
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
			// TableAccountPat
			// 
			this.Name = "TableAccountPat";
			this.Load += new System.EventHandler(this.TableAccountPat_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableAccountPat_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

			///<summary></summary>
		public void InstantClasses(){
			MaxRows=10;
			MaxCols=2;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,9,14);
			Heading=Lan.g("TableAccountPat","Select Patient");
			Fields[0]=Lan.g("TableAccountPat","Patient");
			Fields[1]=Lan.g("TableAccountPat","Est Bal");
			ColAlign[1]=HorizontalAlignment.Right;
			ColWidth[0]=95;
			ColWidth[1]=49;
			LayoutTables();
		}

	}
}

