using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{

	public class TableApptProcs : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableApptProcs(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=5;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading="Procedures";
			Fields[0]=Lan.g("TableApptProcs","Stat");
			Fields[1]=Lan.g("TableApptProcs","Tth");
			Fields[2]=Lan.g("TableApptProcs","Surf");
			Fields[3]=Lan.g("TableApptProcs","Description");
			Fields[4]=Lan.g("TableApptProcs","Fee");
			ColAlign[4]=HorizontalAlignment.Right;
			ColWidth[0]=40;
			ColWidth[1]=30;
			ColWidth[2]=40;
			ColWidth[3]=175;
			ColWidth[4]=60;			
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
			// TableApptProcs
			// 
			this.Name = "TableApptProcs";
			this.Load += new System.EventHandler(this.TableApptProcs_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableApptProcs_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}




	}
}

