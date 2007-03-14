using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{

	public class TableRxSetup : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableRxSetup(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=5;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading=Lan.g("tbRxSetup","Prescriptions");
			Fields[0]=Lan.g("tbRxSetup","Drug");
			Fields[1]=Lan.g("tbRxSetup","Sig");
			Fields[2]=Lan.g("tbRxSetup","Disp");
			Fields[3]=Lan.g("tbRxSetup","Refills");
			Fields[4]=Lan.g("tbRxSetup","Notes");
			ColWidth[0]=140;
			ColWidth[1]=320;
			ColWidth[2]=70;
			ColWidth[3]=70;
			ColWidth[4]=300;
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
			// TableRxSetup
			// 
			this.Name = "TableRxSetup";
			this.Load += new System.EventHandler(this.TableRxSetup_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableRxSetup_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}