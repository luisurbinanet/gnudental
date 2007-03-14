using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{

	public class TableRecall : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableRecall(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=5;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Fields[0]=Lan.g("tbRecall","Due Date");
			Fields[1]=Lan.g("tbRecall","Patient");
			Fields[2]=Lan.g("tbRecall","Age");
			Fields[3]=Lan.g("tbRecall","Interval");
			Fields[4]=Lan.g("tbRecall","Status");
			ColWidth[0]=75;
			ColWidth[1]=120;
			ColWidth[2]=30;
			ColWidth[3]=50;
			ColWidth[4]=150;
			DefaultGridColor=Color.LightGray;
			LayoutTables();
		}

		protected override void Dispose( bool disposing ){
			if( disposing ){
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code

		private void InitializeComponent()	{
			this.SuspendLayout();
			// 
			// TableRecall
			// 
			this.Name = "TableRecall";
			this.Load += new System.EventHandler(this.TableRecall_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableRecall_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

