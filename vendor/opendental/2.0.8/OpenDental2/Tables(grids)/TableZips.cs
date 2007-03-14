using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{

	public class TableZips : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableZips(){
      InitializeComponent();
			MaxRows=50;
			MaxCols=4;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Fields[0]=Lan.g("tbZips","ZipCode");
			Fields[1]=Lan.g("tbZips","City");
			Fields[2]=Lan.g("tbZips","State");
			Fields[3]=Lan.g("tbZips","Frequent");

			ColWidth[0]=100;
			ColWidth[1]=200;
			ColWidth[2]=100;
			ColWidth[3]=100;

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
			// 
			// TableZips
			// 
			this.Name = "TableZips";
			this.Load += new System.EventHandler(this.TableZips_Load);

		}
		#endregion

		private void TableZips_Load(object sender, System.EventArgs e) {
		  LayoutTables();
		}
	}
}

