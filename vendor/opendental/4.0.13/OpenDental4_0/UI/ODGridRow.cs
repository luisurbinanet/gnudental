using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental.UI{ 

	///<summary></summary>
	public class ODGridRow{		
		private ODGridCellCollection cells;
		private Color colorBackG;
		private bool bold;
		private Color colorText;
		private Color colorLborder;
		private object tag;
		
		///<summary>Creates a new ODGridRow.</summary>
		public ODGridRow(){
			cells=new ODGridCellCollection();
			colorBackG=Color.White;
			bold=false;
			colorText=Color.Black;
			colorLborder=Color.Empty;
			tag=null;
		}

		///<summary></summary>
		public ODGridCellCollection Cells{
			get{
				return cells;
			}
		}

	  ///<summary></summary>
		public Color ColorBackG{
			get{
				return colorBackG;
			}
			set{
				colorBackG=value;
			}
		}

		///<summary></summary>
		public bool Bold{
			get{
				return bold;
			}
			set{
				bold=value;
			}
		} 
  
		///<summary>This sets the text color for the whole row.  Each gridCell also has a colorText property that will override this if set.</summary>
		public Color ColorText{
			get{
				return colorText;
			}
			set{
				colorText=value;
			}
		}

		///<summary></summary>
		public Color ColorLborder{
			get{
				return colorLborder;
			}
			set{
				colorLborder=value;
			}
		}

		///<summary>Used to store any kind of object that is associated with the row.</summary>
		public object Tag{
			get{
				return tag;
			}
			set{
				tag=value;
			}
		}
	        

	}

	









}






