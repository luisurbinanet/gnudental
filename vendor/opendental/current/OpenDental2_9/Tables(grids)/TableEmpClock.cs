using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class TableEmpClock : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableEmpClock(){
			InitializeComponent();
			MaxRows=20;
			MaxCols=2;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Fields[0]=Lan.g("TableEmpClock","Employee");
			Fields[1]=Lan.g("TableEmpClock","Status");
			//Fields[2]=Lan.g("TableBilling","-Insurance Est");
			//Fields[3]=Lan.g("TableBilling","=Amount Due");
			ColWidth[0]=180;
			ColWidth[1]=100;
			//ColWidth[2]=100;
			//ColWidth[3]=100;
			//ColAlign[1]=HorizontalAlignment.Right;
			//ColAlign[2]=HorizontalAlignment.Right;
			//ColAlign[3]=HorizontalAlignment.Right;

			DefaultGridColor=Color.LightGray;
			LayoutTables();
		}

		///<summary></summary>
		protected override void Dispose(bool disposing){
			if(disposing){
				if (components != null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Designer generated code

		private void InitializeComponent(){
			// 
			// TableAutoBilling
			// 
			this.Name = "TableAutoBilling";
			this.Load += new System.EventHandler(this.TableAutoBilling_Load);

		}
		#endregion

		
		private void TableAutoBilling_Load(object sender, System.EventArgs e) {
		  LayoutTables();
		}

	}
}

