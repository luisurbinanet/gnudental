using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental.Main_Modules
{
	///<summary></summary>
	public class TableCommLogAccount : OpenDental.ContrTable
	{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableCommLogAccount(){
			InitializeComponent();
			MaxRows=50;
			MaxCols=4;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			Heading=Lan.g("TableCommLogAccount","Communications Log");
			InstantClassesPar();
			SetRowHeight(0,49,14);
			Fields[0]=Lan.g("TableCommLogAccount","Date");
			Fields[1]=Lan.g("TableCommLogAccount","Type");
			Fields[2]=Lan.g("TableCommLogAccount","Mode");
			Fields[3]=Lan.g("TableCommLogAccount","Note");
			ColWidth[0]=70;
			ColWidth[1]=85;
			ColWidth[2]=80;
			ColWidth[3]=515;
			//ColAlign[1]=HorizontalAlignment.Right;
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
			// TableCommLogAccount
			// 
			this.Name = "TableCommLogAccount";
			this.Load += new System.EventHandler(this.TableAutoBilling_Load);

		}
		#endregion

		private void TableAutoBilling_Load(object sender, System.EventArgs e) {
		  LayoutTables();
		}
	}
}

