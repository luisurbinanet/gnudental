using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace OpenDental{
	public class TableTreat : OpenDental.ContrTable{
		private System.ComponentModel.IContainer components = null;

		public TableTreat(){
			InitializeComponent();
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
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

