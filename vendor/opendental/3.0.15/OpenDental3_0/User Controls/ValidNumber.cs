using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
///<summary>This differs slightly from ValidNum. Use this to allow a blank entry instead of defaulting to 0.
///</summary>
	public class ValidNumber : System.Windows.Forms.TextBox{
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public int MaxVal=255;
		///<summary></summary>
		public System.Windows.Forms.ErrorProvider errorProvider1;
		///<summary></summary>
		public int MinVal=0;

		///<summary></summary>
		public ValidNumber(){
			InitializeComponent();
		}

		///<summary></summary>
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
			// ValidNumber
			// 
			this.Validating += new System.ComponentModel.CancelEventHandler(this.ValidNumber_Validating);

		}
		#endregion

		private void ValidNumber_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			string myMessage="";
			if(Text==""){
				errorProvider1.SetError(this,myMessage);//sets no error message. (empty is OK)
				return;
			}
			try{
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
			}
			errorProvider1.SetError(this,myMessage);
		}

	}
}
