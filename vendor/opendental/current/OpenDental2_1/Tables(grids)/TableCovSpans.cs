using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{

	public class TableCovSpans : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableCovSpans(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=3;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading=Lan.g("TableCovSpans","Coverage Spans");
			Fields[0]=Lan.g("TableCovSpans","From ADA");
			Fields[1]=Lan.g("TableCovSpans","To ADA");
			Fields[2]=Lan.g("TableCovSpans","Coverage Category");
			ColWidth[0]=80;
			ColWidth[1]=80;
			ColWidth[2]=150;
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
			// TableCovSpans
			// 
			this.Name = "TableCovSpans";
			this.Load += new System.EventHandler(this.TableCovSpans_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableCovSpans_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

