using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental.Forms{
	///<summary></summary>
	public class TableTimeCard : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableTimeCard(){
			InitializeComponent();
			MaxRows=20;
			MaxCols=8;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=false;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Fields[0]=Lan.g("TableEmpClock","Date");
			Fields[1]=Lan.g("TableEmpClock","Altered");
			Fields[2]=Lan.g("TableEmpClock","Status");
			Fields[3]=Lan.g("TableEmpClock","In/Out");
			Fields[4]=Lan.g("TableEmpClock","Time");
			Fields[5]=Lan.g("TableEmpClock","Hours");
			Fields[6]=Lan.g("TableEmpClock","Daily");
			Fields[7]=Lan.g("TableEmpClock","Note");
			ColWidth[0]=70;
			ColWidth[1]=50;
			ColWidth[2]=50;
			ColWidth[3]=60;
			ColWidth[4]=60;
			ColWidth[5]=50;
			ColWidth[6]=50;
			ColWidth[7]=250;
			ColAlign[1]=HorizontalAlignment.Right;
			ColAlign[3]=HorizontalAlignment.Right;
			ColAlign[4]=HorizontalAlignment.Right;
			ColAlign[5]=HorizontalAlignment.Right;
			ColAlign[6]=HorizontalAlignment.Right;
			DefaultGridColor=Color.LightGray;
			LayoutTables();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
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
			// 
			// TableTimeCard
			// 
			this.Name = "TableTimeCard";
			this.Load += new System.EventHandler(this.TableTimeCard_Load);

		}
		#endregion

		private void TableTimeCard_Load(object sender, System.EventArgs e) {
		  LayoutTables();
		}



	}
}

