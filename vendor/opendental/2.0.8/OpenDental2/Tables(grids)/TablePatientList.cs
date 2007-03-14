using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace OpenDental{

	public class TablePatientList : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TablePatientList(){
			InitializeComponent();// This call is required by the Windows Form Designer.
			MaxRows=20;
			MaxCols=9;
			ShowScroll=false;
			FieldsArePresent=true;
			HeadingIsPresent=true;
			InstantClassesPar();
			SetRowHeight(0,19,14);
			Heading=Lan.g("tbPatientList","Patient List");
			Fields[0]=Lan.g("tbPatientList","Last Name");
			Fields[1]=Lan.g("tbPatientList","First Name");
			Fields[2]=Lan.g("tbPatientList","MI");
			Fields[3]=Lan.g("tbPatientList","Pref'd Name");
			Fields[4]=Lan.g("tbPatientList","Age");
			Fields[5]=Lan.g("tbPatientList","SSN");
			Fields[6]=Lan.g("tbPatientList","Phone");
			Fields[7]=Lan.g("tbPatientList","Address");
			Fields[8]=Lan.g("tbPatientList","Status");
			ColWidth[0]=80;
			ColWidth[1]=80;
			ColWidth[2]=25;
			ColWidth[3]=80;
			ColWidth[4]=40;
			ColWidth[5]=65;	
			ColWidth[6]=90;
			ColWidth[7]=100;
			ColWidth[8]=65;	
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
			// TablePatientList
			// 
			this.Name = "TablePatientList";
			this.Load += new System.EventHandler(this.TablePatientList_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void TablePatientList_Load(object sender, System.EventArgs e) {
			LayoutTables();
		}

	}
}
