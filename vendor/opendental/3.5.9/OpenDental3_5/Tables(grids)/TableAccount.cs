using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{
///<summary></summary>
	public class TableAccount : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableAccount(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			//InstantClasses();
			MaxRows=20;
			MaxCols=12;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading=Lan.g("TableAccount","Patient Account");
			Fields[0]=Lan.g("TableAccount","Date");
			Fields[1]=Lan.g("TableAccount","Prov");
			Fields[2]=Lan.g("TableAccount","Code");
			Fields[3]=Lan.g("TableAccount","Tth");
			Fields[4]=Lan.g("TableAccount","Description");
			Fields[5]=Lan.g("TableAccount","Fee");
			Fields[6]=Lan.g("TableAccount","Ins Est");
			Fields[7]=Lan.g("TableAccount","Ins Paid");
			Fields[8]=Lan.g("TableAccount","Patient");
			Fields[9]=Lan.g("TableAccount","Adj");
			Fields[10]=Lan.g("TableAccount","Paid");
			Fields[11]=Lan.g("TableAccount","Balance");
			ColAlign[5]=HorizontalAlignment.Right;
			ColAlign[6]=HorizontalAlignment.Right;
			ColAlign[7]=HorizontalAlignment.Right;
			ColAlign[8]=HorizontalAlignment.Right;
			ColAlign[9]=HorizontalAlignment.Right;
			ColAlign[10]=HorizontalAlignment.Right;
			ColAlign[11]=HorizontalAlignment.Right;
			ColWidth[0]=65;
			ColWidth[1]=40;
			ColWidth[2]=46;
			ColWidth[3]=22;
			ColWidth[4]=241;
			ColWidth[5]=48;
			ColWidth[6]=48;
			ColWidth[7]=48;
			ColWidth[8]=48;
			ColWidth[9]=48;
			ColWidth[10]=48;
			ColWidth[11]=48;
			DefaultGridColor=Color.LightGray;
			LayoutTables();
			//NoteWidth=(float)(45+22+240+28+65);
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

		///<summary></summary>
		public void InstantClasses(){
			//can't move other logic here for some reason, or it causes problems in FormRecallListEdit
			Heading=Lan.g("TableAccount","Patient Account");
			Fields[0]=Lan.g("TableAccount","Date");
			Fields[1]=Lan.g("TableAccount","Prov");
			Fields[2]=Lan.g("TableAccount","Code");
			Fields[3]=Lan.g("TableAccount","Tth");
			Fields[4]=Lan.g("TableAccount","Description");
			Fields[5]=Lan.g("TableAccount","Fee");
			Fields[6]=Lan.g("TableAccount","Ins Est");
			Fields[7]=Lan.g("TableAccount","Ins Paid");
			Fields[8]=Lan.g("TableAccount","Patient");
			Fields[9]=Lan.g("TableAccount","Adj");
			Fields[10]=Lan.g("TableAccount","Paid");
			Fields[11]=Lan.g("TableAccount","Balance");
		}

		///<summary></summary>
		public void SetNormRow(int row){
			for(int j=0;j<MaxCols;j++){
				LeftBorder[j,row]=DefaultGridColor;
				TopBorder[j,row]=DefaultGridColor;
				IsOverflow[j,row]=false;
			}
		}

		///<summary></summary>
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
