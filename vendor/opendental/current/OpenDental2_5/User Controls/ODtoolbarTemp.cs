/*using System;
using System.Windows;

namespace OpenDental{ 
	public class ODtoolbar : Sysem.Windows.Forms.UserControl{
		private ODtoolbarButtonCollection buttons=new ODtoolbarButtonCollection();
		//private 

		public ODtoolbarButtonCollection Buttons{
			get{
				return buttons;
			}
			set{
				//the client should create the collection, then set it here, causing repaint
				buttons=value;
				RebuildToolbar();
				Invalidate();
			}		
		}

		public void RebuildToolbar(){
			foreach(ODtoolbarButton button in buttons){
				button.Size=new Size(100,26);
				button.Click+=new EventHandler(but_Click);
				this.Controls.Add(button);
			}
			//might be better to add control array, or to disable refresh first.
		}

		private void but_Click(object sender, System.EventArgs e){
			ODtoolbarButton button=(ODtoobarButton)sender;
			//pass on the click event as a toolbar click event
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e){
			base.OnPaint(e);
			//paint background
			//buttons will paint themselves?
		}

	}
}*/







