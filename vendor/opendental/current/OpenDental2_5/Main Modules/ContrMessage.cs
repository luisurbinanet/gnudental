using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{

	public class ContrMessage : System.Windows.Forms.UserControl{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butSend;
		private System.Windows.Forms.TextBox textMessage;
		private System.Windows.Forms.Button butClear;
		private System.ComponentModel.Container components = null;

		public ContrMessage(){
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

		private void InitializeComponent(){
			this.label1 = new System.Windows.Forms.Label();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.butSend = new System.Windows.Forms.Button();
			this.butClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(50, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(166, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "General Message:";
			// 
			// textMessage
			// 
			this.textMessage.Location = new System.Drawing.Point(52, 54);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.Size = new System.Drawing.Size(322, 68);
			this.textMessage.TabIndex = 6;
			this.textMessage.Text = "";
			// 
			// butSend
			// 
			this.butSend.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSend.Location = new System.Drawing.Point(300, 134);
			this.butSend.Name = "butSend";
			this.butSend.TabIndex = 7;
			this.butSend.Text = "Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butClear
			// 
			this.butClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClear.Location = new System.Drawing.Point(200, 134);
			this.butClear.Name = "butClear";
			this.butClear.TabIndex = 8;
			this.butClear.Text = "Clear";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// ContrMessage
			// 
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.textMessage);
			this.Controls.Add(this.label1);
			this.Name = "ContrMessage";
			this.Size = new System.Drawing.Size(732, 548);
			this.Load += new System.EventHandler(this.ContrMessage_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void ContrMessage_Load(object sender, System.EventArgs e) {
		
		}

		public void InstantClasses(){
			Lan.C(this, new System.Windows.Forms.Control[] {
				this.butClear,
				this.butSend,
				this.label1,
			});
		}

		public void ModuleSelected(){
			textMessage.Select();
		}

		private void butSend_Click(object sender, System.EventArgs e) {
			Messages Messages=new Messages();//move this later
			Messages.ButtonsToSend=new MessageButtons();
			Messages.MessageToSend=new MessageInvalid();//because this value is tested when processing
			Messages.ButtonsToSend.Type="Text";
			Messages.ButtonsToSend.Text=textMessage.Text;
			Messages.ButtonsToSend.Row=0;
			Messages.ButtonsToSend.Col=0;
			Messages.ButtonsToSend.Pushed=false;
			Messages.SendButtons();
		}

		public void LogMsg(string text){
			textMessage.Text=text;
		}

		private void butClear_Click(object sender, System.EventArgs e) {
			textMessage.Clear();
			textMessage.Select();
		}

	}
}
