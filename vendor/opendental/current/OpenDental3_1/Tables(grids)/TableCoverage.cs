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
			MaxRows=20;
			MaxCols=4;
			ShowScroll=false;
			FieldsArePresent=false;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(1,18,14);
			RowHeight[0]=20;
			//RowHeight[3]=16;
			RowHeight[4]=20;
			RowHeight[12]=29;
			RowHeight[19]=130;
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
			FontBold[0,4]=true;
			FontBold[2,4]=true;
			FontSize[0,4]=9.5f;
			FontSize[2,4]=9.5f;
			IsOverflow[1,4]=true;
			IsOverflow[3,4]=true;
			SetGridColor(Color.LightGray);
			SetBackGColor(Color.White);
			Cell[0,0]=Lan.g("TableCoverage","Primary Insurance");
			Cell[2,0]=Lan.g("TableCoverage","Secondary Insurance");
			LeftBorder[1,0]=Color.White;
			LeftBorder[2,0]=Color.Black;
			LeftBorder[3,0]=Color.White;
			TopBorder[0,1]=Color.Black;
			TopBorder[1,1]=Color.Black;
			TopBorder[2,1]=Color.Black;
			TopBorder[3,1]=Color.Black;
			LeftBorder[1,4]=Color.White;
			LeftBorder[2,4]=Color.Black;
			LeftBorder[3,4]=Color.White;
			TopBorder[0,4]=Color.Black;
			TopBorder[1,4]=Color.Black;
			TopBorder[2,4]=Color.Black;
			TopBorder[3,4]=Color.Black;
			TopBorder[0,5]=Color.Black;
			TopBorder[1,5]=Color.Black;
			TopBorder[2,5]=Color.Black;
			TopBorder[3,5]=Color.Black;
			string[] LineList=new string[]
			{
				Lan.g("TableCoverage","Ins Plan"),
				Lan.g("TableCoverage","Rel'ship to Sub"),
				"Pending",//placeholder for pending checkbox
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
			Cell[0,4]=Lan.g("TableCoverage","Primary Insurance Plan");
			Cell[2,4]=Lan.g("TableCoverage","Secondary Insurance Plan");
			FontBold[0,4]=true;
			FontBold[2,4]=true;
			//TopBorder[0,4]=Color.Black;
			//TopBorder[2,4]=Color.Black;
			TopBorder[0,7]=Color.Black;
			TopBorder[2,7]=Color.Black;
			//TopBorder[0,8]=Color.Black;
			//TopBorder[2,8]=Color.Black;
			TopBorder[0,12]=Color.Black;
			TopBorder[2,12]=Color.Black;
			TopBorder[0,14]=Color.Black;
			TopBorder[2,14]=Color.Black;
			IsOverflow[1,18]=true;
			IsOverflow[3,18]=true;
			TopBorder[0,18]=Color.Black;
			TopBorder[1,18]=Color.Black;
			TopBorder[2,18]=Color.Black;
			TopBorder[3,18]=Color.Black;
			LeftBorder[1,18]=Color.White;
			LeftBorder[3,18]=Color.White;
			LeftBorder[2,19]=Color.Black;
			LayoutTables();
		}

		private void TableCoverage_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}

