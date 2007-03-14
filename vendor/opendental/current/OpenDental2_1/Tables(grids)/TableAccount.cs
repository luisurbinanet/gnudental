using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{

	public class TableAccount : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableAccount(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=10;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading=Lan.g("tbAccount","Patient Account");
			Fields[0]=Lan.g("tbAccount","Date");
			Fields[1]=Lan.g("tbAccount","Prov");
			Fields[2]=Lan.g("tbAccount","Code");
			Fields[3]=Lan.g("tbAccount","Tth");
			Fields[4]=Lan.g("tbAccount","Description");
			Fields[5]=Lan.g("tbAccount","Fee");
			Fields[6]=Lan.g("tbAccount","Ins Est");
			Fields[7]=Lan.g("tbAccount","Ins Pay");
			Fields[8]=Lan.g("tbAccount","Patient");
			Fields[9]=Lan.g("tbAccount","Balance");
			ColAlign[5]=HorizontalAlignment.Right;
			ColAlign[6]=HorizontalAlignment.Right;
			ColAlign[7]=HorizontalAlignment.Right;
			ColAlign[8]=HorizontalAlignment.Right;
			ColAlign[9]=HorizontalAlignment.Right;
			ColWidth[0]=65;
			ColWidth[1]=40;
			ColWidth[2]=46;
			ColWidth[3]=22;
			ColWidth[4]=270;
			ColWidth[5]=50;
			ColWidth[6]=50;
			ColWidth[7]=50;
			ColWidth[8]=50;
			ColWidth[9]=50;
			DefaultGridColor=Color.LightGray;
			LayoutTables();
			//NoteWidth=(float)(45+22+240+28+65);
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// TableAccount
			// 
			this.Name = "TableAccount";
			this.Load += new System.EventHandler(this.TableAccount_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TableAccount_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

		public void SetNormRow(int row){
			for(int j=0;j<MaxCols;j++){
				LeftBorder[j,row]=DefaultGridColor;
				TopBorder[j,row]=DefaultGridColor;
				IsOverflow[j,row]=false;
			}
		}

		public void SetTopRow(int row){
			for(int j=0;j<MaxCols;j++){
				LeftBorder[j,row]=DefaultGridColor;
				TopBorder[j,row]=Color.Black;
				
			}
		}

		//moved to parent
		//public void SetTextColorRow(int row, Color myColor){
		//	for(int j=0;j<MaxCols;j++){
		//		FontColor[j,row]=myColor;
		//	}
		//}
		

	}
}

