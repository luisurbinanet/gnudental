using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	public class ValidNum : System.Windows.Forms.TextBox{
		private System.ComponentModel.Container components = null;
		public System.Windows.Forms.ErrorProvider errorProvider1;
		public int MaxVal=255;
		public int MinVal=0;

		public ValidNum(){
			InitializeComponent();
  	}

		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			// 
			// errorProvider1
			// 
			this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			// 
			// ValidNum
			// 
			this.Validating += new System.ComponentModel.CancelEventHandler(this.ValidNum_Validating);
			this.Validated += new System.EventHandler(this.ValidNum_Validated);

		}
		#endregion

		private void ValidNum_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			//this differs from ValidNumber slightly.
			//Use this when default is 0.
			string myMessage="";
			try{
				if(Text==""){
					Text="0";
				}
				if(System.Convert.ToInt32(this.Text)>MaxVal)
					throw new Exception("Number must be less than "+(MaxVal+1).ToString());
				if(System.Convert.ToInt32(this.Text)<MinVal)
					throw new Exception("Number must be greater than or equal to "+(MinVal).ToString());
				errorProvider1.SetError(this,"");
			}
			catch(Exception ex){
				if(ex.Message=="Input string was not in a correct format."){
					myMessage="Must be a number. No letters or symbols allowed";
				}
				else{
					myMessage=ex.Message;
				}
				this.errorProvider1.SetError(this,myMessage);
			}
		}

		private void ValidNum_Validated(object sender, System.EventArgs e) {			
			//FormValid=true;
		}


	}
}
