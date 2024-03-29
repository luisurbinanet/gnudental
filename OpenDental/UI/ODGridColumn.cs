using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDental.UI{ 

	///<summary></summary>
	//[DesignTimeVisible(false)]
	//[TypeConverter(typeof(GridColumnTypeConverter))]
	public class ODGridColumn{		
		private string heading;
		private int colWidth;
		private HorizontalAlignment textAlign;
		//private System.ComponentModel.Container components = null;

		///<summary>Creates a new ODGridcolumn.</summary>
		public ODGridColumn(){
			heading="";
			colWidth=80;
			textAlign=HorizontalAlignment.Left;
		}

		///<summary>Creates a new ODGridcolumn with the given heading and width.</summary>
		public ODGridColumn(string heading,int colWidth,HorizontalAlignment textAlign){
			this.heading=heading;
			this.colWidth=colWidth;
			this.textAlign=textAlign;
		}

		///<summary>Creates a new ODGridcolumn with the given heading and width. Alignment left</summary>
		public ODGridColumn(string heading,int colWidth){
			this.heading=heading;
			this.colWidth=colWidth;
			this.textAlign=HorizontalAlignment.Left;
		}

		///<summary></summary>
		public string Heading{
			get{
				return heading;
			}
			set{
				heading=value;
			}
		}

		///<summary></summary>
		public int ColWidth{
			get{
				return colWidth;
			}
			set{
				colWidth=value;
			}
		}

	  ///<summary></summary>
		public HorizontalAlignment TextAlign{
			get{
				return textAlign;
			}
			set{
				textAlign=value;
			}
		}   
	        

	}

	

	







}






