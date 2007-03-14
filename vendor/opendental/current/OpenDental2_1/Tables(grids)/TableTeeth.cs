using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{

	public class TableTeeth : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableTeeth(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=32;
			MaxCols=6;
			ShowScroll=false;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,31,14);
			Heading=Lan.g("tbTeeth","Tooth Chart");
			Fields[0]=Lan.g("tbTeeth","Th");
			Fields[1]=Lan.g("tbTeeth","Th");
			Fields[2]=Lan.g("tbTeeth","Surf");
			Fields[3]=Lan.g("tbTeeth","Dx");
			Fields[4]=Lan.g("tbTeeth","TP");
			Fields[5]=Lan.g("tbTeeth","Complete/Existing");
			ColWidth[0]=20;
			ColWidth[1]=20;
			ColWidth[2]=40;
			ColWidth[3]=23;
			ColWidth[4]=110;
			ColWidth[5]=110;
			for(int i=0;i<32;i++){
				Cell[0,i]=(i+1).ToString();
			}
			for(int i=0;i<10;i++){
				Cell[1,i+3]=Convert.ToChar(65+i).ToString();
			}
			for(int i=0;i<10;i++){
				Cell[1,i+19]=Convert.ToChar(75+i).ToString();
			}
			SetGridColor(Color.LightGray);
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
			// TableTeeth
			// 
			this.Name = "TableTeeth";
			this.Load += new System.EventHandler(this.TableTeeth_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableTeeth_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

