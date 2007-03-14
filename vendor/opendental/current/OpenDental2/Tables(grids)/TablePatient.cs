using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{

	public class TablePatient : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TablePatient(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			InstantClasses();//for designer support
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
			// TablePatient
			// 
			this.Name = "TablePatient";
			this.Load += new System.EventHandler(this.TablePatient_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public void InstantClasses(){
			MaxRows=27;//24
			MaxCols=2;
			ShowScroll=false;
			FieldsArePresent=false;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(1,25,14);//22
			RowHeight[0]=18;
			RowHeight[26]=60;//23
			ColWidth[0]=100;
			ColWidth[1]=150;
			FontBold[0,0]=true;
			FontSize[0,0]=10f;
			IsOverflow[1,0]=true;
			SetGridColor(Color.LightGray);
			SetBackGColor(Color.White);
			Cell[0,0]=Lan.g("TablePatient","Patient Information");
			LeftBorder[1,0]=Color.White;
			TopBorder[0,1]=Color.Black;
			TopBorder[1,1]=Color.Black;
			//Cell[0,1]=Lan.g("TablePatient","Chart Num");
			//TopBorder[0,2]=Color.Black;
			Cell[0,1]=Lan.g("TablePatient","Last");
			Cell[0,2]=Lan.g("TablePatient","First");
			Cell[0,3]=Lan.g("TablePatient","Middle");
			Cell[0,4]=Lan.g("TablePatient","Preferred");
			Cell[0,5]=Lan.g("TablePatient","Salutation");
			TopBorder[0,6]=Color.Black;
			Cell[0,6]=Lan.g("TablePatient","Status");
			Cell[0,7]=Lan.g("TablePatient","Gender");
			Cell[0,8]=Lan.g("TablePatient","Position");
			TopBorder[0,9]=Color.Black;
			Cell[0,9]=Lan.g("TablePatient","Birthdate");
			Cell[0,10]=Lan.g("TablePatient","Age");
			Cell[0,11]=Lan.g("TablePatient","SS#");
			TopBorder[0,12]=Color.Black;
			Cell[0,12]=Lan.g("TablePatient","Address");
			FontBold[0,12]=true;
			Cell[0,13]=Lan.g("TablePatient","Address2");
			Cell[0,14]=Lan.g("TablePatient","City");
			Cell[0,15]=Lan.g("TablePatient","State");
			Cell[0,16]=Lan.g("TablePatient","Zip");
			TopBorder[0,17]=Color.Black;
			Cell[0,17]=Lan.g("TablePatient","Hm Phone");
			FontBold[0,17]=true;
			Cell[0,18]=Lan.g("TablePatient","Wk Phone");
			Cell[0,19]=Lan.g("TablePatient","Wireless Ph");
			Cell[0,20]=Lan.g("TablePatient","E-mail");
			Cell[0,21]=Lan.g("TablePatient","ABC0");
			Cell[0,22]=Lan.g("TablePatient","Recall Months");
			Cell[0,23]=Lan.g("TablePatient","Chart Num");
			Cell[0,24]=Lan.g("TablePatient","Billing Type");
			Cell[0,25]=Lan.g("TablePatient","Referred From");
			//TopBorder[0,2]=Color.Black;
			Cell[0,26]=Lan.g("TablePatient","Family Address and Phone Notes");//22
			IsOverflow[1,26]=true;
			TopBorder[0,26]=Color.Black;
			TopBorder[1,26]=Color.Black;
			LeftBorder[1,26]=Color.White;
			LayoutTables();
		}

		private void TablePatient_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

		





	}
}

