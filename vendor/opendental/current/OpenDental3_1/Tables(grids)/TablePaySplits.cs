using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TablePaySplits : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TablePaySplits(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=10;
			MaxCols=6;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,9,14);
			Heading=Lan.g("TablePaySplits","Payment Splits (optional)");
			//Fields[0]=Lan.g("TablePaySplits","Date");
			Fields[0]=Lan.g("TablePaySplits","Prov");
			Fields[1]=Lan.g("TablePaySplits","Patient");
			Fields[2]=Lan.g("TablePaySplits","Discount Type");
			Fields[3]=Lan.g("TablePaySplits","Discounts");
			Fields[4]=Lan.g("TablePaySplits","Payments");
			Fields[5]=Lan.g("TablePaySplits","New Balance");
			ColAlign[3]=HorizontalAlignment.Right;
			ColAlign[4]=HorizontalAlignment.Right;
			ColAlign[5]=HorizontalAlignment.Right;
			//ColWidth[0]=65;
			ColWidth[0]=40;
			ColWidth[1]=140;
			ColWidth[2]=120;
			ColWidth[3]=65;
			ColWidth[4]=65;
			ColWidth[5]=80;
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
			// TablePaySplits
			// 
			this.Name = "TablePaySplits";
			this.Load += new System.EventHandler(this.TablePaySplits_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TablePaySplits_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

