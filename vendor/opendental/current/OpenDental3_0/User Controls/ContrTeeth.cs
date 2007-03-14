using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace OpenDental
{
	/// <summary>
	/// Summary description for ContrTeeth.
	/// </summary>
	public class ContrTeeth : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public float BigToothWidthP=200;
		///<summary></summary>
		public float BigToothWidthA=145;
		///<summary></summary>
		public float BigToothHeight=715;
		///<summary>adjusted for zoom</summary>
		public int TWidthP;
		///<summary></summary>
		public int TWidthA;
		///<summary></summary>
		public int THeight;
		private float zoom;
		///<summary>valid values are "1" to "32"</summary>
		private ArrayList PrimaryTeeth;
		///<summary>valid values are "1" to "32", and "A" to "Z"</summary>
		private ArrayList MissingTeeth;
		private Bitmap BackShadow;
		///<summary>valid values are "1" to "32", and "A" to "Z"</summary>
		public string[] SelectedTeeth;
		///<summary>valid values are 1 to 32 (int)</summary>
		private ArrayList ALSelectedTeeth;
		private Bitmap Shadow;
		private Color drawColor;
		private int xPos;
		private int yPos;
		private int xPosPrev;
		private int yPosPrev;
		private Point[] poly;
		private bool ControlIsDown;
	
		///<summary></summary>
		public ContrTeeth()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			SelectedTeeth=new string[0];
			//SelectedTeeth[0]="4";
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
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
			// 
			// ContrTeeth
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Name = "ContrTeeth";
			this.Size = new System.Drawing.Size(533, 323);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ContrTeeth_Paint);
			this.MouseHover += new System.EventHandler(this.ContrTeeth_MouseHover);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ContrTeeth_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContrTeeth_KeyDown);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContrTeeth_MouseDown);

		}
		#endregion

		///<summary></summary>
		[Category("AAA Custom"),
			Description("Zoom level. Level one is very large.")
		]
		public float Zoom{
			get{ 
				return zoom; 
			}
			set{ 
				zoom = value;
			}
		}

		//public override 
		///<summary>Sets the zoom according to the desired width.</summary>
		public void SetWidth(int width){
			zoom=(float)(width-17-2)/(BigToothWidthP*6+BigToothWidthA*10);
			ResetSize();
			//RefreshBackShadow();
		}

		//public void RefreshBackShadow(){//only used temporarily for design purposes.
								//Once this is complete, outside code should call ClearProcs() to get a refresh;
		//	CreateBackShadow();
		//	ClearProcs();
		//}

		///<summary>Only to be called once upon startup.</summary>
		public void CreateBackShadow(){//
			BackShadow=new Bitmap(Width,Height);
			Graphics grfx=Graphics.FromImage(BackShadow);
			grfx.SmoothingMode=SmoothingMode.HighQuality;
			grfx.FillRectangle(Brushes.White,0,0,Width,Height);
			//grfx.DrawLine(Pens.LightGray,0,THeight+3,Width,THeight+3);
			//grfx.DrawLine(Pens.LightGray,0,THeight+3+19+1+19,Width,THeight+3+19+1+19);
			//for(int i=1;i<17;i++){
			//	DrawSelected(i,false);
				//grfx.DrawLine(Pens.LightGray,(int)(i*zoom*ToothWidth),0,(int)(i*zoom*ToothWidth),Height/2-19);
				//grfx.DrawLine(Pens.LightGray,(int)(i*zoom*ToothWidth)
				//	,Height/2+19,(int)(i*zoom*ToothWidth),Height);
			//}
			grfx.DrawLine(Pens.Black,0,Height/2,Width,Height/2);
			grfx.DrawRectangle(Pens.Black,0,0,Width-1,Height-1);
			drawColor=Color.DarkSlateGray;
			//bool isMirror;
			//GraphicElements GraphicElements2=new GraphicElements();
			//GraphicShapes GraphicShapes2=new GraphicShapes();
			//GraphicPoints GraphicPoints2=new GraphicPoints();
			for(int c=1;c<=32;c++){
				for(int a=0;a<GraphicTypes.List.Length;a++){
					if(GraphicTypes.List[a].GTypeNum==8){
						if(IsMirror(c))
							GraphicElements.GetSublist(a,GetMirror(c));
						else
							GraphicElements.GetSublist(a,Tooth.FromInt(c));
						break;
					}
				}
				for(int b=0;b<GraphicElements.Sublist.Length;b++){
					//if(listViewTypes.SelectedIndices.Contains(a) 
					//|| listViewElements.SelectedIndices.Contains(b)){
					GraphicShapes.GetSublist(GraphicElements.Sublist[b].GElementNum);
					for(int i=0;i<GraphicShapes.Sublist.Length;i++){
						GraphicPoints.GetSublist(GraphicShapes.Sublist[i].GShapeNum);
						poly=new Point[GraphicPoints.Sublist.Length];
						for(int j=0;j<GraphicPoints.Sublist.Length;j++){
							if(IsMirror(c)){
								xPos=(int)(GetXLoc(c)+GetWidthTooth(c)-GraphicPoints.Sublist[j].Xpos*zoom);
							}
							else{
								xPos=(int)(GetXLoc(c)+GraphicPoints.Sublist[j].Xpos*zoom);
							}
							yPos=(int)(GetYLoc(c)+GraphicPoints.Sublist[j].Ypos*zoom);
							if(GraphicShapes.Sublist[i].ShapeType=="L"){
								if(j>0){
									if(IsMirror(c)){
										xPosPrev=(int)(GetXLoc(c)+GetWidthTooth(c)-GraphicPoints.Sublist[j-1].Xpos*zoom);
									}
									else{
										xPosPrev=(int)(GetXLoc(c)+GraphicPoints.Sublist[j-1].Xpos*zoom);
									}
									yPosPrev=(int)(GetYLoc(c)+GraphicPoints.Sublist[j-1].Ypos*zoom);
									grfx.DrawLine(new Pen(drawColor)
										,xPosPrev,yPosPrev
										,xPos,yPos);
								}
							}
							else{//polygon
								poly[j]=new Point((int)(xPos),(int)(yPos));
							}
						}//end for Points
						if(GraphicPoints.Sublist.Length<2)
							continue;
						if(GraphicShapes.Sublist[i].ShapeType=="F"){
							grfx.FillPolygon(new SolidBrush(Color.Ivory),poly);
						}
						if(GraphicShapes.Sublist[i].ShapeType=="G"){
							grfx.FillPolygon(new SolidBrush(Color.Ivory),poly);
							grfx.DrawPolygon(new Pen(drawColor),poly);
						}
					}
					//}
				}//b
			}
			grfx.Dispose();
		}

		private void DrawToothNum(int intTooth){
			string toothNum=Tooth.FromInt(intTooth);
			if(PrimaryTeeth.Contains(toothNum)){
				toothNum=Tooth.PermToPri(toothNum);
			}
			Font numFont=new Font("Arial",8);
			Graphics grfx=Graphics.FromImage(Shadow);
			if(intTooth<=16){
				grfx.DrawString(Tooth.ToInternat(toothNum),numFont,Brushes.Black
					,GetXLoc(intTooth)+GetWidthTooth(intTooth)/2
					-grfx.MeasureString(Tooth.ToInternat(toothNum),numFont).Width/2
					,Height/2-15);
			}
			else{
				grfx.DrawString(Tooth.ToInternat(toothNum),numFont,Brushes.Black
					,GetXLoc(intTooth)+GetWidthTooth(intTooth)/2
					-grfx.MeasureString(Tooth.ToInternat(toothNum),numFont).Width/2
					,Height/2+4);
			}
			grfx.Dispose();
		}

		private void DrawSelected(int intTooth, bool isSelected){
			if(Shadow==null)
				return;
			Graphics grfx=Graphics.FromImage(Shadow);
			Color drawColor;
			Color fillColor;
			if(isSelected){
				drawColor=Color.Red;
				fillColor=Color.FromName("Highlight");
			}
			else{
				drawColor=Color.White;
				fillColor=Color.White;
			}
			grfx.DrawRectangle(new Pen(drawColor),GetXLoc(intTooth)-1,GetYLoc(intTooth)-1
				,GetWidthTooth(intTooth)+1,THeight+1);
			if(intTooth<=16){
				grfx.FillRectangle(new SolidBrush(fillColor),GetXLoc(intTooth)-1,THeight+3
					,GetWidthTooth(intTooth)+2,19);
			}
			else{
				grfx.FillRectangle(new SolidBrush(fillColor),GetXLoc(intTooth)-1,THeight+19+4
					,GetWidthTooth(intTooth)+2,19);
			}
			DrawToothNum(intTooth);
			grfx.Dispose();
			//DrawShadow when done or user will not see the change
		}

		private int GetWidthTooth(string toothNum){
			if(Tooth.IsMolar(toothNum)){
				return (int)(TWidthP);
			}
			else{
				return (int)(TWidthA);
			}
		}

		private int GetWidthTooth(int intTooth){
			return GetWidthTooth(Tooth.FromInt(intTooth));
		}

		///<summary>Also clears selected. Does not DrawShadow</summary>
		public void ClearProcs(){//
			if(BackShadow==null)
				return;
			ALSelectedTeeth=new ArrayList();
			SelectedTeeth=new string[0];
			PrimaryTeeth=new ArrayList();
			MissingTeeth=new ArrayList();
			Shadow=new Bitmap(Width,Height);
			Graphics grfx=Graphics.FromImage(Shadow);
			grfx.DrawImage(BackShadow,0,0);
			for(int i=1;i<=32;i++){
				DrawToothNum(i);
			}
			grfx.Dispose();
			//next, usually draw series of Elements.
			//(maybe?) DrawSelected (DrawSelected(Tooth.ToInt(SelectedTeeth[0]),true);)
			//then, DrawShadow
		}

		///<summary></summary>
		public void DrawShadow(){
			if(Shadow==null)
				return;
			Graphics grfx=this.CreateGraphics();
			grfx.DrawImage(Shadow,0,0);
			grfx.Dispose();
		}

		///<summary>Draws the tooth chart onto the specified graphics object at the specified location.</summary>
		public void PrintChart(Graphics g,int x,int y){
			if(Shadow==null)
				return;
			g.DrawImage(Shadow,x,y);
		}

		///<summary></summary>
		public bool IsMirror(int intTooth){
			if(intTooth >= 1 && intTooth <= 8)
				return false;
			else if(intTooth >= 9 && intTooth <= 16)
				return true;
			else if(intTooth >= 17 && intTooth <= 24)
				return true;
			else
				return false;
		}

		///<summary></summary>
		public bool IsMirror(string toothNum){
			return IsMirror(Tooth.ToInt(toothNum));
		}

		///<summary></summary>
		public string GetMirror(string toothNum){
			switch(toothNum){
				case "9": return "8";
				case "10": return "7";
				case "11": return "6";
				case "12": return "5";
				case "13": return "4";
				case "14": return "3";
				case "15": return "2";
				case "16": return "1";
				case "17": return "32";
				case "18": return "31";
				case "19": return "30";
				case "20": return "29";
				case "21": return "28";
				case "22": return "27";
				case "23": return "26";
				case "24": return "25";
				case "F": return "E";
				case "G": return "D";
				case "H": return "C";
				case "I": return "B";
				case "J": return "A";
				case "K": return "T";
				case "L": return "S";
				case "M": return "R";
				case "N": return "Q";
				case "O": return "P";
				default: return toothNum;
			}
		}

		///<summary></summary>
		public string GetMirror(int intTooth){
			return GetMirror(Tooth.FromInt(intTooth));
		}

		private int GetMesial(int intTooth){
			//will always return a valid tooth num
			if(intTooth <=8){
				return intTooth+1;
			}
			else if(intTooth <=16){
				return intTooth-1;
			}
			else if(intTooth <=24){
				return intTooth+1;
			}
			else{
				return intTooth-1;
			}
		}

		private string GetMesial(string toothNum){
			//always returns a permanent tooth.  No reason to return primary.
			return Tooth.FromInt(GetMesial(Tooth.ToInt(toothNum)));
		}

		private int GetDistal(int intTooth){
			if(intTooth==1 || intTooth==16 || intTooth==17 || intTooth==32){
				return -1;
			}
			else if(intTooth <=8){
				return intTooth-1;
			}
			else if(intTooth <=16){
				return intTooth+1;
			}
			else if(intTooth <=24){
				return intTooth-1;
			}
			else{
				return intTooth+1;
			}
		}

		private string GetDistal(string toothNum){
			//always returns a permanent tooth or a -1.  No reason to return primary.
			int intTooth=GetDistal(Tooth.ToInt(toothNum));
			if(intTooth==-1){
				return "";
			}
			else return Tooth.FromInt(intTooth);
		}

		private int GetXLoc(string toothNum){
			return GetXLoc(Tooth.ToInt(toothNum));
		}

		private int GetYLoc(string toothNum){
			return GetYLoc(Tooth.ToInt(toothNum));
		}

		private int GetXLoc(int intTooth){
			if(intTooth <= 3){
				return (intTooth-1)*TWidthP+intTooth+1;
			}
			else if(intTooth <= 13){
				return (3)*TWidthP
					+(intTooth-4)*TWidthA+intTooth+1;
			}
			else if(intTooth <= 16){
				return 3*TWidthP
					+10*TWidthA
					+(intTooth-14)*TWidthP+intTooth+1;
			}
			else if(intTooth <= 19){
				return 3*TWidthP+10*TWidthA+(19-intTooth)*TWidthP+(34-intTooth);
			}
			else if(intTooth <= 29){
				return 3*TWidthP +(29-intTooth)*TWidthA+(34-intTooth);
			}
			else{//(intTooth >= 30){
				return (32-intTooth)*TWidthP+(34-intTooth);
			}
		}

		private int GetYLoc(int intTooth){
			if(intTooth<=16){
				return 2;
			}
			else return THeight+38+5;
		}

		private void ResetSize(){
			THeight=(int)(BigToothHeight*zoom);
			TWidthA=(int)(BigToothWidthA*zoom);
			TWidthP=(int)(BigToothWidthP*zoom);
			Height=(int)(THeight*2)+38+7;
			Width=(int)(TWidthP*6+TWidthA*10)+17+3;

		}

		//public void ClearData(){
			
		//}

		private void ContrTeeth_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			if(Shadow!=null)
				e.Graphics.DrawImage(Shadow,0,0);
		}

		private void ContrTeeth_MouseHover(object sender, System.EventArgs e) {
			//trigger hover event with tooth number if certain conditions are met
		}

		private void ContrTeeth_Click(object sender, System.EventArgs e) {
			
		}

		/*public void SetElement(string toothNum,int gTypeNum,Color color,string surf){
			TeethElement te=new TeethElement();
			te.toothNum=toothNum;
			te.gTypeNum=gTypeNum;
			te.color=color;
			te.surf=surf;
			ALelements.Add(te);
		}*/

		///<summary></summary>
		public void DrawProcs(ArrayList ALprocs){
			Procedure[] procList=new Procedure[ALprocs.Count];
			int[] procOrder=new int[ALprocs.Count];
			for(int i=0;i<ALprocs.Count;i++){
				procList[i]=(Procedure)ALprocs[i];
				procOrder[i]=ProcedureCodes.GetProcCode(((Procedure)ALprocs[i]).ADACode).GTypeNum;
			}
			Array.Sort(procOrder,procList);
			//pri teeth
			string[] priTeeth=Patients.Cur.PrimaryTeeth.Split(',');
			for(int i=0;i<priTeeth.Length;i++){
				SetPrimary(priTeeth[i]);
			}
			//missing teeth
			for(int i=0;i<procList.Length;i++){
				//if(procList[i].HideGraphical){
				//	continue;
				//}
				if(ProcedureCodes.GetProcCode(procList[i].ADACode).RemoveTooth && (
					procList[i].ProcStatus==ProcStat.C
					|| procList[i].ProcStatus==ProcStat.EC
					|| procList[i].ProcStatus==ProcStat.EO))
				{
					if(Tooth.IsPrimary(procList[i].ToothNum)){
						if(PrimaryTeeth.Contains(Tooth.PriToPerm(procList[i].ToothNum))){
							SetMissing(procList[i].ToothNum);
						}
						else{
							MissingTeeth.Add(procList[i].ToothNum);
							//the primary tooth will just be covered up by the permanent, so no need to draw rect
						}
					}
					else{
						SetMissing(procList[i].ToothNum);
					}
				}
			}
			Color elemColor;
			bool doDraw;
			int gTypeNum;
			int intTooth;
			for(int i=0;i<procList.Length;i++){
				doDraw=true;
				switch(procList[i].ProcStatus){
					case ProcStat.C:
						elemColor=Defs.Short[(int)DefCat.ChartGraphicColors][1].ItemColor;
						break;
					case ProcStat.TP:
						elemColor=Defs.Short[(int)DefCat.ChartGraphicColors][0].ItemColor;
						break;
					case ProcStat.EC:
						elemColor=Defs.Short[(int)DefCat.ChartGraphicColors][2].ItemColor;
						break;
					case ProcStat.EO:
						elemColor=Defs.Short[(int)DefCat.ChartGraphicColors][3].ItemColor;
						break;
					case ProcStat.R:
						elemColor=Defs.Short[(int)DefCat.ChartGraphicColors][4].ItemColor;
						break;
					default:
						elemColor=Color.Transparent;
						doDraw=false;
						break;
				}
				if(procList[i].HideGraphical){
					doDraw=false;
				}
				if(ProcedureCodes.GetProcCode(procList[i].ADACode).RemoveTooth && (
					procList[i].ProcStatus==ProcStat.C
					|| procList[i].ProcStatus==ProcStat.EC
					|| procList[i].ProcStatus==ProcStat.EO
					)){
					doDraw=false;//prevents the red X. Missing teeth already handled.
				}
				//if a primary filling, and primary tooth not present, don't draw
				if(Tooth.IsPrimary(procList[i].ToothNum)){
					if(PrimaryTeeth.Contains(Tooth.PriToPerm(procList[i].ToothNum))){
						if(MissingTeeth.Contains(procList[i].ToothNum)){
							doDraw=false;
						}
					}
					else{
						doDraw=false;
					}
				}
				gTypeNum=ProcedureCodes.GetProcCode(procList[i].ADACode).GTypeNum;
				if(gTypeNum==0){
					doDraw=false;
				}
				if(doDraw){
					if(GraphicTypes.GetSpecialType(ProcedureCodes.GetProcCode(procList[i].ADACode).GTypeNum)
						=="bridge"){
						if(!Tooth.IsValidDB(procList[i].ToothNum))
							intTooth=-1;
						else
							intTooth=Tooth.ToInt(procList[i].ToothNum);
						for(int j=0;j<procList.Length;j++){
							if(GetMesial(procList[i].ToothNum)==procList[j].ToothNum
								&& GraphicTypes.GetSpecialType
									(ProcedureCodes.GetProcCode(procList[j].ADACode).GTypeNum)=="bridge"){
								DrawConnector(true,intTooth,elemColor);
							}
							if(GetDistal(procList[i].ToothNum)==procList[j].ToothNum
								&& GraphicTypes.GetSpecialType
									(ProcedureCodes.GetProcCode(procList[j].ADACode).GTypeNum)=="bridge"){
								DrawConnector(false,intTooth,elemColor);
							}
						}
						
					}
					if(GraphicTypes.GetSpecialType(ProcedureCodes.GetProcCode(procList[i].ADACode).GTypeNum)
						=="denture"){
						string[] toothNums;
						if(ProcedureCodes.GetProcCode(procList[i].ADACode).TreatArea==TreatmentArea.Arch){
							if(procList[i].Surf=="U"){
								toothNums=new string[] {"2","3","4","5","6","7","8","9","10","11","12","13","14","15"};
							}
							else if(procList[i].Surf=="L"){
								toothNums=new string[] {"18","19","20","21","22","23","24"
									,"25","26","27","28","29","30","31"};
							}
							else{
								toothNums=new string[0];
							}
						}
						else if(ProcedureCodes.GetProcCode(procList[i].ADACode).TreatArea==TreatmentArea.ToothRange){
							toothNums=procList[i].ToothRange.Split(',');
						}
						else{
							toothNums=new string[0];
						}
						for(int j=0;j<toothNums.Length;j++){
							DrawElement(toothNums[j]
								,gTypeNum
								,elemColor
								,"");
						}
					}
					else{
						DrawElement(procList[i].ToothNum
							,gTypeNum
							,elemColor
							,procList[i].Surf);
					}
				}
			}//for proclist
			//DrawConnector(true,23,Color.Red);
			//DrawConnector(false,9,Color.Red);
		}

		private void DrawConnector(bool isM,int intTooth,Color color){
			if(PrimaryTeeth.Contains(Tooth.FromInt(intTooth))){
				return;//only works for permanent teeth
			}
			Graphics grfx=Graphics.FromImage(Shadow);
			grfx.SmoothingMode=SmoothingMode.HighQuality;
			//int lineWidth=2;
			int[,] points=new int[6,2];//x,y, and every 2 points represents a line; total 3 lines.
			//set center x's:
			xPos=(int)(GetXLoc(intTooth)+GetWidthTooth(intTooth)/2);
			points[0,0]=xPos;
			points[2,0]=xPos;
			points[4,0]=xPos;
			//set all y's:
			if(Tooth.IsMaxillary(intTooth)){
				yPos=(int)(GetYLoc(intTooth)+340*zoom);
				points[0,1]=yPos;
				points[1,1]=yPos;
				yPos=(int)(GetYLoc(intTooth)+390*zoom);
				points[2,1]=yPos;
				points[3,1]=yPos;
				yPos=(int)(GetYLoc(intTooth)+470*zoom);
				points[4,1]=yPos;
				points[5,1]=yPos;
			}
			else{
				yPos=(int)(GetYLoc(intTooth)+250*zoom);
				points[0,1]=yPos;
				points[1,1]=yPos;
				yPos=(int)(GetYLoc(intTooth)+320*zoom);
				points[2,1]=yPos;
				points[3,1]=yPos;
				yPos=(int)(GetYLoc(intTooth)+370*zoom);
				points[4,1]=yPos;
				points[5,1]=yPos;
			}
			//decide right or left:
			bool isRight;
			if(intTooth<=8){
				if(isM) isRight=true;
				else isRight=false;
			}
			else if(intTooth<=16){
				if(isM) isRight=false;
				else isRight=true;
			}
			else if(intTooth<=24){
				if(isM) isRight=false;
				else isRight=true;
			}
			else{
				if(isM) isRight=true;
				else isRight=false;
			}
			//set outside x's:
			if(isRight){
				xPos=(int)(GetXLoc(intTooth)+GetWidthTooth(intTooth));
				points[1,0]=xPos;
				points[3,0]=xPos;
				points[5,0]=xPos;
				grfx.FillRectangle(new SolidBrush(color)
					,points[0,0],points[0,1]
					,points[5,0]-points[0,0],points[5,1]-points[0,1]);
			}
			else{
				xPos=(int)(GetXLoc(intTooth))-1;
				points[1,0]=xPos;
				points[3,0]=xPos;
				points[5,0]=xPos;
				grfx.FillRectangle(new SolidBrush(color)
					,points[1,0],points[1,1]
					,points[4,0]-points[1,0],points[4,1]-points[1,1]);
			}
			
			//draw:
			//for(int i=0;i<3;i++){
			//	grfx.DrawLine(new Pen(color,lineWidth)
			//		,points[2*i,0],points[2*i,1]
			//		,points[2*i+1,0],points[2*i+1,1]);
			//}
			grfx.Dispose();
		}

		private void DrawElement(string toothNum,int gTypeNum,Color color,string surf){
			int intTooth;
			if(!Tooth.IsValidDB(toothNum))
				intTooth=-1;
			else
				intTooth=Tooth.ToInt(toothNum);
			int ordinalTooth=Tooth.ToOrdinal(toothNum);
			if(!Tooth.IsPrimary(toothNum)
				&& PrimaryTeeth.Contains(toothNum)){
				return;//don't draw permanent procedures on primary teeth
			}
			GraphicTypes.GetType(gTypeNum);
			if(GraphicTypes.Cur.IsSameEachTooth){
				if(Tooth.IsMaxillary(toothNum)){
					GraphicElements.GetSublist(GraphicTypes.GetIndex(gTypeNum),"4");
				}
				else{
					GraphicElements.GetSublist(GraphicTypes.GetIndex(gTypeNum),"29");
				}
			}
			else if(GraphicTypes.Cur.SpecialType=="filling"){
				//GraphicTypes.GetType(2);
				if(IsMirror(intTooth))
					GraphicElements.GetSublistForFilling(GetMirror(toothNum),surf);
				else
					GraphicElements.GetSublistForFilling(toothNum,surf);
			}
			else if(GraphicTypes.Cur.SpecialType=="crown"){
				//MessageBox.Show(GraphicTypes.Cur.GTypeNum.ToString());
				if(IsMirror(intTooth))
					GraphicElements.GetSublist(GraphicTypes.GetIndex(6),GetMirror(toothNum));
				else
					GraphicElements.GetSublist(GraphicTypes.GetIndex(6),toothNum);
			}
			else if(GraphicTypes.Cur.SpecialType=="bridge"){
				//MessageBox.Show(GraphicTypes.Cur.GTypeNum.ToString());
				if(IsMirror(intTooth))
					GraphicElements.GetSublist(GraphicTypes.GetIndex(12),GetMirror(toothNum));
				else
					GraphicElements.GetSublist(GraphicTypes.GetIndex(12),toothNum);
			}
			else if(GraphicTypes.Cur.SpecialType=="denture"){
				//MessageBox.Show(GraphicTypes.Cur.GTypeNum.ToString());
				if(IsMirror(intTooth))
					GraphicElements.GetSublist(GraphicTypes.GetIndex(24),GetMirror(toothNum));
				else
					GraphicElements.GetSublist(GraphicTypes.GetIndex(24),toothNum);
			}
			else{
				if(IsMirror(intTooth))
					GraphicElements.GetSublist(GraphicTypes.GetIndex(gTypeNum),GetMirror(toothNum));
				else
					GraphicElements.GetSublist(GraphicTypes.GetIndex(gTypeNum),toothNum);
			}
			Graphics grfx=Graphics.FromImage(Shadow);
			grfx.SmoothingMode=SmoothingMode.HighQuality;
			int lineWidth=2;
			//MessageBox.Show(GraphicElements.Sublist.Length.ToString());
			for(int b=0;b<GraphicElements.Sublist.Length;b++){	
				GraphicShapes.GetSublist(GraphicElements.Sublist[b].GElementNum);
				for(int i=0;i<GraphicShapes.Sublist.Length;i++){
					GraphicPoints.GetSublist(GraphicShapes.Sublist[i].GShapeNum);
					poly=new Point[GraphicPoints.Sublist.Length];
					for(int j=0;j<GraphicPoints.Sublist.Length;j++){
						if(GraphicTypes.Cur.GTypeNum==11){
							xPos=(int)(GetXLoc(intTooth)+GraphicPoints.Sublist[j].Xpos*zoom);
						}
						else if(IsMirror(intTooth)){
							xPos=(int)(GetXLoc(intTooth)+GetWidthTooth(intTooth)-GraphicPoints.Sublist[j].Xpos*zoom);
						}
						else{
							xPos=(int)(GetXLoc(intTooth)+GraphicPoints.Sublist[j].Xpos*zoom);
						}
						if(GraphicTypes.Cur.IsSameEachTooth
							&& Tooth.IsMolar(intTooth)){
							if(GraphicTypes.Cur.GTypeNum==11){
								xPos+=(TWidthP-TWidthA)/2;
							}
							else if(IsMirror(intTooth)){
								xPos-=(TWidthP-TWidthA)/2;
							}
							else{
								//MessageBox.Show(TWidthP.ToString()+","+TWidthA.ToString());
								xPos+=(TWidthP-TWidthA)/2;
							}

						}
						yPos=(int)(GetYLoc(intTooth)+GraphicPoints.Sublist[j].Ypos*zoom);
						if(GraphicShapes.Sublist[i].ShapeType=="L"){
							if(j>0){
								if(GraphicTypes.Cur.GTypeNum==11){
									xPosPrev=(int)(GetXLoc(intTooth)+GraphicPoints.Sublist[j-1].Xpos*zoom);
								}
								else if(IsMirror(intTooth)){
									xPosPrev=(int)(GetXLoc(intTooth)+GetWidthTooth(intTooth)
										-GraphicPoints.Sublist[j-1].Xpos*zoom);
								}
								else{
									xPosPrev=(int)(GetXLoc(intTooth)+GraphicPoints.Sublist[j-1].Xpos*zoom);
								}
								if(GraphicTypes.Cur.IsSameEachTooth
									&& Tooth.IsMolar(intTooth)){
									if(GraphicTypes.Cur.GTypeNum==11){
										xPosPrev+=(TWidthP-TWidthA)/2;
									}
									else if(IsMirror(intTooth)){
										xPosPrev-=(TWidthP-TWidthA)/2;
									}
									else{
										xPosPrev+=(TWidthP-TWidthA)/2;
									}
								}
								yPosPrev=(int)(GetYLoc(intTooth)+GraphicPoints.Sublist[j-1].Ypos*zoom);
								grfx.DrawLine(new Pen(color,lineWidth)
									,xPosPrev,yPosPrev
									,xPos,yPos);
							}
						}
						else{//polygon
							poly[j]=new Point((int)(xPos),(int)(yPos));
						}
					}//end for Points
					if(GraphicPoints.Sublist.Length<2)
						continue;
					if(GraphicShapes.Sublist[i].ShapeType=="G"){
						switch(GraphicTypes.Cur.BrushType){
							case "solid":
								grfx.FillPolygon(new SolidBrush(color),poly);
								break;
							case "diagonal":
								grfx.FillPolygon(new HatchBrush(HatchStyle.LightUpwardDiagonal,color,Color.Ivory),poly);
								break;
							case "light":
								grfx.FillPolygon(new HatchBrush(HatchStyle.Percent50,color,Color.Ivory),poly);
								break;
							case "outline":
								grfx.FillPolygon(new SolidBrush(Color.White),poly);
								grfx.DrawPolygon(new Pen(color,lineWidth),poly);
								break;
							case "hatchoutline":
								grfx.FillPolygon(new HatchBrush(HatchStyle.SmallGrid,color,Color.Ivory),poly);
								grfx.DrawPolygon(new Pen(color,lineWidth),poly);
								break;
							case "diagonaloutline":
								grfx.FillPolygon(new HatchBrush(HatchStyle.LightUpwardDiagonal,color,Color.Ivory),poly);
								grfx.DrawPolygon(new Pen(color,lineWidth),poly);
								break;
							case "lightoutline":
								grfx.FillPolygon(new HatchBrush(HatchStyle.Percent50,color,Color.Ivory),poly);
								grfx.DrawPolygon(new Pen(color,lineWidth),poly);
								break;
							
						}
						//LightUpwardDiagonal-diagonal
						//SmallGrid-hatch
						//Percent50-light
						//Solid
						//Outline
					}
				}
			}//b
			grfx.Dispose();
		}

		private void ContrTeeth_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			int toothClicked=1;
			if(e.Y < THeight+19+4){//max
				if(e.X < TWidthP*3+4){
					toothClicked=(int)((e.X-1)/(TWidthP+1))+1;
				}
				else if(e.X < TWidthP*3+4 + TWidthA*10+10){
					//SelectedTeeth[0]=Tooth.FromInt((int)((e.X-TWidthP*3-4)/(TWidthA+1))+4);
					toothClicked=(int)((e.X-TWidthP*3-4)/(TWidthA+1))+4;
				}
				else{
					toothClicked=(int)((e.X-TWidthP*3-TWidthA*10-14)/(TWidthP+1))+14;
				}
			}
			else{//mand
				if(e.X < TWidthP*3+4){
					toothClicked=32-(int)(e.X-1)/(TWidthP+1);
				}
				else if(e.X < TWidthP*3+4 + TWidthA*10+10){
					toothClicked=29-(int)(e.X-TWidthP*3-4)/(TWidthA+1);
				}
				else{
					toothClicked=19-(int)(e.X-TWidthP*3-TWidthA*10-14)/(TWidthP+1);
				}
			}
			if(ControlIsDown){
				if(ALSelectedTeeth.Contains(toothClicked)){
					ALSelectedTeeth.Remove(toothClicked);
					DrawSelected(toothClicked,false);
				}
				else{
					ALSelectedTeeth.Add(toothClicked);
					DrawSelected(toothClicked,true);
				}
			}
			else{//control not down
				for(int i=0;i<ALSelectedTeeth.Count;i++){
					DrawSelected((int)ALSelectedTeeth[i],false);
				}
				ALSelectedTeeth.Clear();
				ALSelectedTeeth.Add(toothClicked);
				DrawSelected(toothClicked,true);
			}
			if(ALSelectedTeeth.Count==0){
				SelectedTeeth=new string[0];
			}
			else{
				SelectedTeeth=new string[ALSelectedTeeth.Count];
				for(int i=0;i<ALSelectedTeeth.Count;i++){
					SelectedTeeth[i]=Tooth.FromInt((int)ALSelectedTeeth[i]);
					DrawSelected((int)ALSelectedTeeth[i],true);//redraws
					if(PrimaryTeeth.Contains(SelectedTeeth[i])
						&& Tooth.PermToPri(toothClicked)!=""){
						SelectedTeeth[i]=Tooth.PermToPri(SelectedTeeth[i]);
					}
				}
			}
			DrawShadow();
		}

		///<summary>Adds the tooth number to MissingTeeth and draws a white rectangle over that tooth.</summary>
		public void SetMissing(string toothNum){//valid "1"-"32" and "A"-"Z"
			MissingTeeth.Add(toothNum);
			int intTooth=Tooth.ToInt(toothNum);
			Graphics grfx=Graphics.FromImage(Shadow);
			grfx.FillRectangle(Brushes.White,GetXLoc(intTooth),GetYLoc(intTooth)
				,GetWidthTooth(intTooth),THeight);
			grfx.Dispose();
		}

		///<summary>Valid "1"-"32".  Adds the tooth to PrimaryTeeth and also draws the primary tooth.</summary>
		public void SetPrimary(string toothNum){
			string priToothNum=Tooth.PermToPri(toothNum);
			int intTooth=Tooth.ToInt(toothNum);
			PrimaryTeeth.Add(toothNum);
			Color fillColor=Color.White;
			Graphics grfx=Graphics.FromImage(Shadow);
			grfx.SmoothingMode=SmoothingMode.HighQuality;
			if(intTooth<=16){
				grfx.FillRectangle(new SolidBrush(fillColor),GetXLoc(intTooth)-1,GetYLoc(intTooth)
					,GetWidthTooth(intTooth)+2,THeight+19);
			}
			else{
				grfx.FillRectangle(new SolidBrush(fillColor),GetXLoc(intTooth)-1,THeight+19+4
					,GetWidthTooth(intTooth)+2,THeight+19+1);
			}
			DrawToothNum(intTooth);
			if(priToothNum==""){
				return;
			}
			drawColor=Color.DarkSlateGray;
			for(int a=0;a<GraphicTypes.List.Length;a++){
				if(GraphicTypes.List[a].GTypeNum==8){
					if(IsMirror(intTooth))
						GraphicElements.GetSublist(a,GetMirror(priToothNum));
					else
						GraphicElements.GetSublist(a,priToothNum);
					break;
				}
			}
			for(int b=0;b<GraphicElements.Sublist.Length;b++){
				//if(listViewTypes.SelectedIndices.Contains(a) 
				//|| listViewElements.SelectedIndices.Contains(b)){
				GraphicShapes.GetSublist(GraphicElements.Sublist[b].GElementNum);
				for(int i=0;i<GraphicShapes.Sublist.Length;i++){
					GraphicPoints.GetSublist(GraphicShapes.Sublist[i].GShapeNum);
					poly=new Point[GraphicPoints.Sublist.Length];
					for(int j=0;j<GraphicPoints.Sublist.Length;j++){
						if(IsMirror(intTooth)){
							xPos=(int)(GetXLoc(intTooth)+GetWidthTooth(intTooth)-GraphicPoints.Sublist[j].Xpos*zoom);
						}
						else{
							xPos=(int)(GetXLoc(intTooth)+GraphicPoints.Sublist[j].Xpos*zoom);
						}
						yPos=(int)(GetYLoc(intTooth)+GraphicPoints.Sublist[j].Ypos*zoom);
						if(GraphicShapes.Sublist[i].ShapeType=="L"){
							if(j>0){
								if(IsMirror(intTooth)){
									xPosPrev=(int)(GetXLoc(intTooth)+GetWidthTooth(intTooth)
										-GraphicPoints.Sublist[j-1].Xpos*zoom);
								}
								else{
									xPosPrev=(int)(GetXLoc(intTooth)+GraphicPoints.Sublist[j-1].Xpos*zoom);
								}
								yPosPrev=(int)(GetYLoc(intTooth)+GraphicPoints.Sublist[j-1].Ypos*zoom);
								grfx.DrawLine(new Pen(drawColor)
									,xPosPrev,yPosPrev
									,xPos,yPos);
							}
						}
						else{//polygon
							poly[j]=new Point((int)(xPos),(int)(yPos));
						}
					}//end for Points
					if(GraphicPoints.Sublist.Length<2)
						continue;
					if(GraphicShapes.Sublist[i].ShapeType=="F"){
						grfx.FillPolygon(new SolidBrush(Color.Ivory),poly);
					}
					if(GraphicShapes.Sublist[i].ShapeType=="G"){
						grfx.FillPolygon(new SolidBrush(Color.Ivory),poly);
						grfx.DrawPolygon(new Pen(drawColor),poly);
					}
				}
			}//b
			grfx.Dispose();
		}

		///<summary></summary>
		public void ContrTeeth_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			//this.OnKeyDown(e);
			if(e.KeyCode==Keys.ControlKey){
				ControlIsDown=true;
			}
		}

		///<summary></summary>
		public void ContrTeeth_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			//this.OnKeyUp(e);
			if(e.KeyCode==Keys.ControlKey){
				ControlIsDown=false;
			}
		}


	}

	/*public struct TeethElement{
		public string toothNum;
		public int gTypeNum;
		public Color color;
		public string surf;
	}*/

}




