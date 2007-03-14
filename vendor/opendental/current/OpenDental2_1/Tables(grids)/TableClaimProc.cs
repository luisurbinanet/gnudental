using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	public class TableClaimProc : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableClaimProc(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=6;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading=Lan.g("tbClaimProc","Procedures");
			Fields[0]=Lan.g("tbClaimProc","Code");
			Fields[1]=Lan.g("tbClaimProc","Tth");
			Fields[2]=Lan.g("tbClaimProc","Description");
			Fields[3]=Lan.g("tbClaimProc","Fee");
			Fields[4]=Lan.g("tbClaimProc","Ins Est");
			Fields[5]=Lan.g("tbClaimProc","Patient");
			ColAlign[3]=HorizontalAlignment.Right;
			ColAlign[4]=HorizontalAlignment.Right;
			ColAlign[5]=HorizontalAlignment.Right;
			ColWidth[0]=46;
			ColWidth[1]=22;
			ColWidth[2]=200;
			ColWidth[3]=50;
			ColWidth[4]=50;
			ColWidth[5]=50;

			DefaultGridColor=Color.LightGray;
			LayoutTables();
		}

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
			// TableClaimProc
			// 
			this.Name = "TableClaimProc";
			this.Load += new System.EventHandler(this.TableClaimProc_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableClaimProc_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

