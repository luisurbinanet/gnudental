using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TableCoverage : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableCoverage(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			InstantClasses();
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
			// TableCoverage
			// 
			this.Name = "TableCoverage";
			this.Load += new System.EventHandler(this.TableCoverage_Load);
			this.ResumeLayout(false);

		}
		#endregion

		///<summary></summary>
		public void InstantClasses(){
			MaxRows=19;
			MaxCols=4;
			ShowScroll=false;
			FieldsArePresent=false;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(1,17,14);
			RowHeight[0]=20;
			RowHeight[3]=20;
			RowHeight[11]=29;
			RowHeight[18]=130;
			ColWidth[0]=120;
			ColWidth[1]=150;
			ColWidth[2]=120;
			ColWidth[3]=150;
			FontBold[0,0]=true;
			FontBold[2,0]=true;
			FontSize[0,0]=9.5f;
			FontSize[2,0]=9.5f;
			IsOverflow[1,0]=true;
			IsOverflow[3,0]=true;
			FontBold[0,3]=true;
			FontBold[2,3]=true;
			FontSize[0,3]=9.5f;
			FontSize[2,3]=9.5f;
			IsOverflow[1,3]=true;
			IsOverflow[3,3]=true;
			SetGridColor(Color.LightGray);
			SetBackGColor(Color.White);
			Cell[0,0]=Lan.g("TableCoverage","Primary Insurance Coverage");
			Cell[2,0]=Lan.g("TableCoverage","Secondary Insurance Coverage");
			LeftBorder[1,0]=Color.White;
			LeftBorder[2,0]=Color.Black;
			LeftBorder[3,0]=Color.White;
			TopBorder[0,1]=Color.Black;
			TopBorder[1,1]=Color.Black;
			TopBorder[2,1]=Color.Black;
			TopBorder[3,1]=Color.Black;
			LeftBorder[1,3]=Color.White;
			LeftBorder[2,3]=Color.Black;
			LeftBorder[3,3]=Color.White;
			TopBorder[0,3]=Color.Black;
			TopBorder[1,3]=Color.Black;
			TopBorder[2,3]=Color.Black;
			TopBorder[3,3]=Color.Black;
			TopBorder[0,4]=Color.Black;
			TopBorder[1,4]=Color.Black;
			TopBorder[2,4]=Color.Black;
			TopBorder[3,4]=Color.Black;
			string[] LineList=new string[] {
				Lan.g("TableCoverage","Ins Plan"),
				Lan.g("TableCoverage","Rel'ship to Sub"),
				"",//placeholder for plan
				Lan.g("TableCoverage","Annual Max $"),
				Lan.g("TableCoverage","Ortho Max $"),
				Lan.g("TableCoverage","Renewal Month"),
				Lan.g("TableCoverage","Deductible $"),
				Lan.g("TableCoverage","Waived on Prev?"),
				"",
				"",
				"",
				"",
				"",
				Lan.g("TableCoverage","Flo to Age:"),
				Lan.g("TableCoverage","Miss Tooth Excl?"),
				Lan.g("TableCoverage","Wait on Major?"),
				Lan.g("TableCoverage","Ins Plan Note")};
			for(int i=0;i<LineList.Length;i++){
				LeftBorder[2,i+1]=Color.Black;
				Cell[0,i+1]=LineList[i];
				Cell[2,i+1]=LineList[i];
			}
			Cell[0,3]=Lan.g("TableCoverage","Primary Insurance Plan");
			Cell[2,3]=Lan.g("TableCoverage","Secondary Insurance Plan");
			FontBold[0,4]=true;
			FontBold[2,4]=true;
			TopBorder[0,4]=Color.Black;
			TopBorder[2,4]=Color.Black;
			TopBorder[0,6]=Color.Black;
			TopBorder[2,6]=Color.Black;
			//TopBorder[0,8]=Color.Black;
			//TopBorder[2,8]=Color.Black;
			TopBorder[0,11]=Color.Black;
			TopBorder[2,11]=Color.Black;
			TopBorder[0,13]=Color.Black;
			TopBorder[2,13]=Color.Black;
			IsOverflow[1,17]=true;
			IsOverflow[3,17]=true;
			TopBorder[0,17]=Color.Black;
			TopBorder[1,17]=Color.Black;
			TopBorder[2,17]=Color.Black;
			TopBorder[3,17]=Color.Black;
			LeftBorder[1,17]=Color.White;
			LeftBorder[3,17]=Color.White;
			LeftBorder[2,18]=Color.Black;
			LayoutTables();
		}

		private void TableCoverage_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

