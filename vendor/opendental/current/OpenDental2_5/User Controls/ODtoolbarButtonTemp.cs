/*using System;

namespace OpenDental{ 
	public class ODtoolbarButton : Sysem.Windows.Forms.ToolbarButton{
		public enum State{
			//disabled is handled separately
			Normal,//but looks different if clicked
			Hover,//also looks different if clicked
			Pressed,
			//ClickedHover,
			//Clicked
		}
	  
		private State state=State.Normal;
		private Image image;
		private bool isClicked;//this is an persistant state not related to the click event.
		//private Rectangle bounds;
	  
		public Image Image{
      //this might not be necessary if I do decide to inherit from the standard toolbarbutton
      get{
        return image;
			}
			set{
				image=value;
				//bounds=
				this.Invalidate();
			}
		}
	  
		public bool IsClicked{
      get{
        return isClicked;
			}
			set{
				isClicked=value;//this is so the toolbar can ‘unclick’ a button
				this.Invalidate();
			}
		}
	  
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e){
      //this should only happen when mouse enters
      base.OnMouseMove();
      //only causes a repaint if needed
			//isClicked is not relevant here because there are only three states
			if(state==State.Normal){		
					state=State.Hover;
					this.Invalidate();
			}	
		}
	  
		protected override void OnMouseLeave(System.EventArgs e){
      //resets button appearance.  This will also deactivate the button if it has been pressed
      //but not released. Repaints only if necessary.
      //isClicked not relevant because only 3 states
      if(state==State.Hover){
        state=State.Normal;
        this.Invalidate();
			}
		}
	  
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e){
      //change the button to a pressed state.
      if((e.Button & MouseButtons.Left)==MouseButtons.Left){
        state=State.Pressed;
        isClicked=true;//move this to an event
        this.Invalidate();
			}
		}
	  
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e){
      //change button to hover state and repaint if needed.
      if((e.Button & MouseButtons.Left)!=MouseButtons.Left){
			//if the left mouse button was not raised, nothing happens
        state=State.Hover;
        this.Invalidate();
			}
		}
	  
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e){
      if(image==null){
        //Draw the text without the image.
        e.Graphics.DrawString(this.Text,this.Font,new SolidBrush(this.ForeColor),5,0);
        return;
			}
			if(this.Enabled){
				if(isClicked){
					switch(state){
        		case State.Normal:
        			e.Graphics.DrawImage(image,2,2);
        			break;
        		case State.Hover:
        			e.Graphics.DrawImage(image,2,2);
        			break;
        		case State.Pressed:
        			e.Graphics.DrawImage(image,3,3);
        			break;
					}//switch
				}//if clicked
				else{
					switch(state){
        		case State.Normal:
        			e.Graphics.DrawImage(image,2,2);
        			break;
        		case State.Hover:
        			e.Graphics.DrawImage(image,2,2);
        			break;
        		case State.Pressed:
        			e.Graphics.DrawImage(image,3,3);
        			break;
					}//switch
				}//else
			}//if enabled
			else{
			  ControlPaint.DrawImageDisabled(e.Graphics,image,2,2,this.BackColor);
			}
	  
		}//onPaint
	        
	        

	}
}*/






