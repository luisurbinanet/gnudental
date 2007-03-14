using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental.UI{ 

	///<summary></summary>
	public class ODGridCell{		
		private string text;
		
		///<summary>Creates a new ODGridCell.</summary>
		public ODGridCell(){
			
		}

		///<summary>Creates a new ODGridCell.</summary>
		public ODGridCell(string myText){
			text=myText;
		}

		///<summary></summary>
		public string Text{
			get{
				return text;
			}
			set{
				text=value;
			}
		}

	        
	        

	}

	









}






