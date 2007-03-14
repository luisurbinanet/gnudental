using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental
{
	///<summary></summary>
	public class TableTemplates : OpenDental.ContrTable
	{
		private System.ComponentModel.IContainer components = null;

		///<summary></summary>
		public TableTemplates(){
			InitializeComponent();
			MaxRows=20;
			MaxCols=12;
			ShowScroll=true;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading=Lan.g("TableTemplates","Insurance Plans");
			Fields[0]=Lan.g("TableTemplates","Employer");
			Fields[1]=Lan.g("TableTemplates","Carrier");
			Fields[2]=Lan.g("TableTemplates","Phone");
			Fields[3]=Lan.g("TableTemplates","Address");
			Fields[4]=Lan.g("TableTemplates","City");
			Fields[5]=Lan.g("TableTemplates","ST");
			Fields[6]=Lan.g("TableTemplates","Zip");
			Fields[7]=Lan.g("TableTemplates","Group#");
			Fields[8]=Lan.g("TableTemplates","Group Name");
			Fields[9]=Lan.g("TableTemplates","noE");
			Fields[10]=Lan.g("TableTemplates","ElectID");
			Fields[11]=Lan.g("TableTemplates","Plans");
			ColWidth[0]=140;
			ColWidth[1]=140;
			ColWidth[2]=82;
			ColWidth[3]=120;
			ColWidth[4]=80;
			ColWidth[5]=25;
			ColWidth[6]=50;
			ColWidth[7]=70;
			ColWidth[8]=90;
			ColWidth[9]=35;
			ColWidth[10]=45;
			ColWidth[11]=40;
			ScrollValue=1;
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
			// TableTemplates
			// 
			this.Name = "TableTemplates";
			this.Load += new System.EventHandler(this.TableTemplates_Load);

		}
		#endregion

		private void TableTemplates_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}
	}
}

