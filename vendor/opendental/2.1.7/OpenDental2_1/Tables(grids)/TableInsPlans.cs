using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{

	public class TableInsPlans : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableInsPlans(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			InstantClasses();
			
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
			// TableInsPlans
			// 
			this.Name = "TableInsPlans";
			this.Load += new System.EventHandler(this.TableInsPlans_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public void InstantClasses(){
			MaxRows=4;
			MaxCols=5;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,3,14);
			SetGridColor(Color.LightGray);
			SetBackGColor(Color.White);
			Heading=Lan.g("TableInsPlans","Insurance Plans for Family");
			Fields[0]=Lan.g("TableInsPlans","#");
			Fields[1]=Lan.g("TableInsPlans","Subscriber");
			Fields[2]=Lan.g("TableInsPlans","Ins Carrier");
			Fields[3]=Lan.g("TableInsPlans","Date Effect.");
			Fields[4]=Lan.g("TableInsPlans","Date Term.");
			ColWidth[0]=20;
			ColWidth[1]=140;
			ColWidth[2]=100;
			ColWidth[3]=90;
			ColWidth[4]=90;
			LayoutTables();
		}

		private void TableInsPlans_Load(object sender, System.EventArgs e) {
			LayoutTables();			
		}

	}
}

