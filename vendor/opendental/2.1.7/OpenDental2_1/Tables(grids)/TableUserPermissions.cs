using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{

	public class TableUserPermissions : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableUserPermissions(){
			InitializeComponent();
			MaxRows=20;
			MaxCols=3;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Fields[0]=Lan.g("TableUserPermissions","Name");
			Fields[1]=Lan.g("TableUserPermissions","Has Permission");
			Fields[2]=Lan.g("TableUserPermissions","IsLogged");
			ColAlign[1]=HorizontalAlignment.Center;
			ColAlign[2]=HorizontalAlignment.Center;
			ColWidth[0]=150;
			ColWidth[1]=100;
			ColWidth[2]=100;
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
			// TableUserPermissions
			// 
			this.Name = "TableUserPermissions";
			this.Load += new System.EventHandler(this.TableUserPermissions_Load);

		}
		#endregion

		private void TableUserPermissions_Load(object sender, System.EventArgs e) {
		  LayoutTables();
		}
	}
}

