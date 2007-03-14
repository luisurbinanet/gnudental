using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{

	public class TableUnsched : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableUnsched(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=6;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Fields[0]=Lan.g("tbUnsched","Patient");
			Fields[1]=Lan.g("tbUnsched","Date");
			Fields[2]=Lan.g("tbUnsched","Status");
			Fields[3]=Lan.g("tbUnsched","Prov");
			Fields[4]=Lan.g("tbUnsched","Procedures");
			Fields[5]=Lan.g("tbUnsched","Notes");
			ColWidth[0]=140;
			ColWidth[1]=70;
			ColWidth[2]=140;
			ColWidth[3]=60;
			ColWidth[4]=200;
			ColWidth[5]=200;
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
			// TableUnsched
			// 
			this.Name = "TableUnsched";
			this.Load += new System.EventHandler(this.TableUnsched_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableUnsched_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

