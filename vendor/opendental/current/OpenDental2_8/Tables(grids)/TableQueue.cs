using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class TableQueue : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableQueue(){
			InitializeComponent();
			MaxRows=20;
			MaxCols=4;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Fields[0]=Lan.g("TableQueue","Patient Name");
			Fields[1]=Lan.g("TableQueue","NoE");
			Fields[2]=Lan.g("TableQueue","Carrier Name");
			Fields[3]=Lan.g("TableQueue","Status");
			ColWidth[0]=130;
			ColWidth[1]=50;
			ColWidth[2]=170;
			ColWidth[3]=120;
			ColAlign[1]=HorizontalAlignment.Center;
			DefaultGridColor=Color.LightGray;
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
			// TableQueue
			// 
			this.Name = "TableQueue";
			this.Load += new System.EventHandler(this.TableQueue_Load);
      this.ResumeLayout(false);
		}
		#endregion

		private void TableQueue_Load(object sender, System.EventArgs e) {
		  LayoutTables();
		}

	}
}

