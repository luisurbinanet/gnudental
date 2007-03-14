using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{

	public class TableApptsOther : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableApptsOther(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=6;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Fields[0]=Lan.g("tbApptOthers","Is Next?");
			Fields[1]=Lan.g("tbApptOthers","Appt Status");
			Fields[2]=Lan.g("tbApptOthers","Date");
			Fields[3]=Lan.g("tbApptOthers","Min");
			Fields[4]=Lan.g("tbApptOthers","Procedures");
			Fields[5]=Lan.g("tbApptOthers","Notes");
			ColWidth[0]=70;
			ColWidth[1]=100;
			ColWidth[2]=70;
			ColWidth[3]=40;
			ColWidth[4]=250;
			ColWidth[5]=320;
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
			// TableApptsOther
			// 
			this.Name = "TableApptsOther";
			this.Load += new System.EventHandler(this.TableApptsOther_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableApptsOther_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

