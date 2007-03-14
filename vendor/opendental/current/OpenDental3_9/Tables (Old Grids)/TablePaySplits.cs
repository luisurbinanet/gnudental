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
			Fields[0]=Lan.g("TablePaySplits","Date");
			Fields[1]=Lan.g("TablePaySplits","Prov");
			Fields[2]=Lan.g("TablePaySplits","Patient");
			Fields[3]=Lan.g("TablePaySplits","Tth");
			Fields[4]=Lan.g("TablePaySplits","Procedure");
			Fields[5]=Lan.g("TablePaySplits","Amount");
			ColAlign[3]=HorizontalAlignment.Center;
			ColAlign[5]=HorizontalAlignment.Right;
			ColWidth[0]=70;
			ColWidth[1]=45;
			ColWidth[2]=150;
			ColWidth[3]=40;
			ColWidth[4]=170;
			ColWidth[5]=65;
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

