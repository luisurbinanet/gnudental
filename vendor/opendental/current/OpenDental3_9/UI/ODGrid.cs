using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OpenDental.UI{
	///<summary></summary>
	public delegate void ODGridClickEventHandler(object sender,ODGridClickEventArgs e);

	///<summary>A new and improved grid control to replace the inherited ContrTable that is used so extensively in the program.</summary>
	[DefaultEvent("CellDoubleClick")]
	public class ODGrid : System.Windows.Forms.UserControl{
		///<summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		private ODGridColumnCollection columns;
		///<summary></summary>
		[Category("Action"),Description("Occurs when a cell is double clicked.")]
		public event ODGridClickEventHandler CellDoubleClick=null;
		///<summary></summary>
		[Category("Action"),Description("Occurs when a cell is single clicked.")]
		public event ODGridClickEventHandler CellClick=null;
		private string title;
		private Font titleFont=new Font(FontFamily.GenericSansSerif,10,FontStyle.Bold);
		private Font headerFont=new Font(FontFamily.GenericSansSerif,8.5f,FontStyle.Bold);
		private Font cellFont=new Font(FontFamily.GenericSansSerif,8.5f);
		private int titleHeight=18;
		private int headerHeight=15;
		private Color cGridLine=Color.LightGray;
			//Color.FromArgb(192,192,192);
			//Color.FromArgb(157,157,161);
		private Color cTitleBackG=Color.FromArgb(210,210,210);
			//(224,223,227);
		private Color cBlueOutline=Color.FromArgb(119,119,146);
		private System.Windows.Forms.VScrollBar vScroll;
		private System.Windows.Forms.HScrollBar hScroll;
		private ODGridRowCollection rows;
		private bool IsUpdating;
		///<summary>The total height of the grid.</summary>
		private int GridH;
		///<summary>This array has one element for each row.  For each row, it keeps track of the vertical height of the row in pixels.</summary>
		private int[] RowHeights;
		///<summary>This array has one element for each row.  For each row, it keeps track of the vertical location at which to start drawing this row in pixels.  This makes it much easier to paint rows.</summary>
		private int[] RowLocs;
		private bool hScrollVisible;
		///<summary>Set at the very beginning of OnPaint.  Uses the ColWidth of each column to set up this array with one element for each column.  Contains the columns Pos for that column.</summary>
		private int[] ColPos;
		private ArrayList selectedIndices;
		private int MouseDownRow;
		private bool ControlIsDown;
		private bool ShiftIsDown;
		private SelectionMode selectionMode;
		private bool MouseIsDown;
		private bool MouseIsOver;//helps automate scrolling
		private string translationName;
		private Color selectedRowColor;
		private bool allowSelection;

		///<summary></summary>
		public ODGrid(){
			//InitializeComponent();// Required for Windows.Forms Class Composition Designer support
			//Add any constructor code after InitializeComponent call
			columns=new ODGridColumnCollection();
			rows=new ODGridRowCollection();
			vScroll=new VScrollBar();
			vScroll.Scroll+=new ScrollEventHandler(vScroll_Scroll);
			vScroll.MouseEnter+=new EventHandler(vScroll_MouseEnter);
			vScroll.MouseLeave+=new EventHandler(vScroll_MouseLeave);
			vScroll.MouseMove+=new MouseEventHandler(vScroll_MouseMove);
			hScroll=new HScrollBar();
			this.Controls.Add(vScroll);
			this.Controls.Add(hScroll);
			selectedIndices=new ArrayList();
			selectionMode=SelectionMode.One;
			selectedRowColor=Color.Silver;
			allowSelection=true;
		}

		///<summary>Clean up any resources being used.</summary>
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
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Properties
		///<summary>Gets the collection of ODGridColumns assigned to the ODGrid control.</summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(System.ComponentModel.Design.CollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
		public ODGridColumnCollection Columns{
			get{
				return columns;
				//Invalidate();
			}
		}

		///<summary>Gets the collection of ODGridRows assigned to the ODGrid control.</summary>
		[Browsable(false)]
		public ODGridRowCollection Rows{
			get{
				return rows;
			}
		}
		
		///<summary>The title of the grid which shows across the top.</summary>
		[Category("Appearance"),Description("The title of the grid which shows across the top.")]
		public string Title{
			get{
				return title;
			}
			set{
				title=value;
				Invalidate();
			}	
		}

		///<summary>Set true to show a horizontal scrollbar.  Vertical scrollbar always shows, but is disabled if not needed.  If hScroll is not visible, then grid will auto reset width to match width of columns.</summary>
		[Category("Appearance"),Description("Set true to show a horizontal scrollbar.")]
		public bool HScrollVisible{
			get{
				return hScrollVisible;
			}
			set{
				hScrollVisible=value;
				LayoutScrollBars();
				Invalidate();
			}	
		}

		///<summary>Gets or sets the position of the scrollbar.  Does all error checking and invalidates.</summary>
		[Browsable(false)]
		public int ScrollValue{
			get{ 
				return vScroll.Value; 
			}
			set{
				if(!vScroll.Enabled){
					return;
				}
				if(value>vScroll.Maximum-vScroll.LargeChange) 
					vScroll.Value=vScroll.Maximum-vScroll.LargeChange;
				else if(value<vScroll.Minimum)
					vScroll.Value=vScroll.Minimum;
				else
					vScroll.Value=value;
				Invalidate();
			}
		}

		///<summary>Holds the int values of the indices of the selected rows.  To set selected indices, use SetSelected().</summary>
    [Browsable(false)]
		public int[] SelectedIndices{
			get{
				int[] retVal=new int[selectedIndices.Count];
				selectedIndices.CopyTo(retVal);
				return retVal; 
			}
		}

		///<summary></summary>
		[Category("Behavior"),Description("Exactly like the listBox.SelectionMode, except no MultiSimple.")]
		[DefaultValue(typeof(SelectionMode),"One")]
		public SelectionMode SelectionMode{
			get{ 
				return selectionMode; 
			}
			set{
				if((SelectionMode)value==SelectionMode.MultiSimple){
					MessageBox.Show("MultiSimple not supported.");
					return;
				}
				selectionMode=value;
			}
		}

		///<summary></summary>
		[Category("Behavior"),Description("Set false to disable row selection when user clicks.  Row selection should then be handled by the form using the cellClick event.")]
		[DefaultValue(true)]
		public bool AllowSelection{
			get{ 
				return allowSelection; 
			}
			set{
				allowSelection=value;
			}
		}

		///<summary>Uniquely identifies the grid for translation to another language.</summary>
		[Category("Appearance"),Description("Uniquely identifies the grid for translation to another language.")]
		public string TranslationName{
			get{ 
				return translationName; 
			}
			set{ 
				translationName=value;
			}
		}

		///<summary>The background color that is used for selected rows.</summary>
		[Category("Appearance"),Description("The background color that is used for selected rows.")]
		[DefaultValue(typeof(Color),"Silver")]
		public Color SelectedRowColor{
			get{ 
				return selectedRowColor; 
			}
			set{ 
				selectedRowColor=value;
			}
		}

		#endregion

		///<summary></summary>
		protected override void OnLoad(EventArgs e) {
			base.OnLoad (e);
			this.Parent.MouseWheel+=new MouseEventHandler(Parent_MouseWheel);
			this.Parent.KeyDown+=new KeyEventHandler(Parent_KeyDown);
			this.Parent.KeyUp+=new KeyEventHandler(Parent_KeyUp);
		}

		#region Painting
		///<summary>Runs any time the control is invalidated.</summary>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e){
			if(IsUpdating) return;
			//set the position of each column.  Only need to do it once for the entire process
			ColPos=new int[columns.Count];
			for(int i=0;i<ColPos.Length;i++){
				if(i==0)
					ColPos[i]=0;
				else
					ColPos[i]=ColPos[i-1]+columns[i-1].ColWidth;
			}
			Bitmap doubleBuffer=new Bitmap(Width,Height,e.Graphics);
			Graphics g=Graphics.FromImage(doubleBuffer);
			DrawBackG(g);
			DrawRows(g);
			DrawTitleAndHeaders(g);//this will draw on top of any grid stuff
			DrawOutline(g);
			e.Graphics.DrawImageUnscaled(doubleBuffer,0,0);
			g.Dispose();
		}

		private void DrawTitleAndHeaders(Graphics g){
			//Title----------------------------------------------------------------------------------------------------
			g.FillRectangle(new LinearGradientBrush(new Rectangle(0,0,Width,titleHeight+5),
				//Color.FromArgb(172,171,196)
				Color.White,Color.FromArgb(200,200,215),LinearGradientMode.Vertical),0,0,Width,titleHeight);
			g.DrawString(title,titleFont,Brushes.Black,Width/2-g.MeasureString(title,titleFont).Width/2,2);
			//Column Headers-----------------------------------------------------------------------------------------
			g.FillRectangle(new SolidBrush(this.cTitleBackG),0,titleHeight,Width,headerHeight);//background
			g.DrawLine(new Pen(Color.FromArgb(102,102,122)),0,titleHeight,Width,titleHeight);//line between title and headers
			for(int i=0;i<columns.Count;i++){
				if(i!=0){
					//vertical lines separating column headers
					g.DrawLine(new Pen(Color.FromArgb(120,120,120)),1+ColPos[i],titleHeight+3,1+ColPos[i],titleHeight+headerHeight-2);
					g.DrawLine(new Pen(Color.White),1+ColPos[i]+1,titleHeight+3,1+ColPos[i]+1,titleHeight+headerHeight-2);
				}
				g.DrawString(columns[i].Heading,headerFont,Brushes.Black,
					ColPos[i]+columns[i].ColWidth/2-g.MeasureString(columns[i].Heading,headerFont).Width/2,titleHeight+2);
			}
			//line below headers
			g.DrawLine(new Pen(Color.FromArgb(120,120,120)),0,titleHeight+headerHeight,Width,titleHeight+headerHeight);
		}

		///<summary>Draws the background of all rows, including selected rows.</summary>
		private void DrawBackG(Graphics g){
			if(vScroll.Enabled){//all backg white, since no gray will show
				g.FillRectangle(new SolidBrush(Color.White),0,titleHeight+headerHeight+1,Width,this.Height-titleHeight-headerHeight-1);
			}
			else{
				g.FillRectangle(new SolidBrush(Color.FromArgb(224,223,227)),0,titleHeight+headerHeight+1,
					Width,this.Height-titleHeight-headerHeight-1);
			}
		}

		///<summary>Draws the lines and text for all rows</summary>
		private void DrawRows(Graphics g){
			for(int i=0;i<rows.Count;i++){
				if(-vScroll.Value+RowLocs[i]+RowHeights[i]<0){
					continue;//lower edge of row above top of grid area
				}
				if(-vScroll.Value+1+titleHeight+headerHeight+RowLocs[i]>Height){
					return;//row below lower edge of control
				}
				DrawRow(i,g);
			}
		}

		///<summary>Draws lines and text for a single row.</summary>
		private void DrawRow(int rowI,Graphics g){
			RectangleF textRect;
			if(rows[rowI].Bold){
				cellFont=new Font(cellFont,FontStyle.Bold);
			}
			else{
				cellFont=new Font(cellFont,FontStyle.Regular);				
			}
			StringFormat format=new StringFormat();
			Pen rightPen=new Pen(this.cGridLine);
			Pen lowerPen=new Pen(this.cGridLine);
			if(rowI==rows.Count-1){//last row
				lowerPen=new Pen(Color.FromArgb(120,120,120));
			}
			else if(rows[rowI].ColorLborder!=Color.Empty){
				lowerPen=new Pen(rows[rowI].ColorLborder);
			}
			SolidBrush textBrush=new SolidBrush(rows[rowI].ColorText);
			//selected row color
			if(selectedIndices.Contains(rowI)){
				g.FillRectangle(new SolidBrush(selectedRowColor),
					1,
					-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1,
					Width,
					RowHeights[rowI]-1);
			}
			//colored row background
			else if(rows[rowI].ColorBackG!=Color.White){
				g.FillRectangle(new SolidBrush(rows[rowI].ColorBackG),
					1,
					-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1,
					Width,
					RowHeights[rowI]-1);
			}
			//background row color
			else if(!vScroll.Enabled){//need to draw over the gray background
				g.FillRectangle(new SolidBrush(rows[rowI].ColorBackG),
					1,
					-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1,
					Width,
					RowHeights[rowI]-1);
			}
			for(int i=0;i<columns.Count;i++){
				//right vertical gridline
				if(rowI==0){
					g.DrawLine(rightPen,
						1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI],
						1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]);
				}
				else{
					g.DrawLine(rightPen,
						1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1,
						1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]);
				}
				//lower horizontal gridline
				if(i==0){
					g.DrawLine(lowerPen,
						1+ColPos[i],
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI],
						1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]);
				}
				else{
					g.DrawLine(lowerPen,
						1+ColPos[i]+1,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI],
						1+ColPos[i]+columns[i].ColWidth,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+RowHeights[rowI]);
				}
				//text
				if(rows[rowI].Cells.Count-1<i){
					continue;
				}
				switch(columns[i].TextAlign){
					case HorizontalAlignment.Left:
						format.Alignment=StringAlignment.Near;
						break;
					case HorizontalAlignment.Center:
						format.Alignment=StringAlignment.Center;
						break;
					case HorizontalAlignment.Right:
						format.Alignment=StringAlignment.Far;
						break;
				}
				if(columns[i].TextAlign==HorizontalAlignment.Right){
					textRect=new RectangleF(
						1+ColPos[i]-1,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1,
						columns[i].ColWidth+2,
						RowHeights[rowI]);
				}
				else{
					textRect=new RectangleF(
						1+ColPos[i]+1,
						-vScroll.Value+1+titleHeight+headerHeight+RowLocs[rowI]+1,
						columns[i].ColWidth,
						RowHeights[rowI]);
				}
				g.DrawString(rows[rowI].Cells[i].Text,cellFont,textBrush,textRect,format);
			}
			
		}

		///<summary>Draws outline around entire control.</summary>
		private void DrawOutline(Graphics g){
			if(hScroll.Visible){//for the little square at the lower right between the two scrollbars
				g.FillRectangle(new SolidBrush(Color.FromKnownColor(KnownColor.Control)),Width-vScroll.Width-1,
					Height-hScroll.Height-1,vScroll.Width,hScroll.Height);
			}
			g.DrawRectangle(new Pen(this.cBlueOutline),0,0,Width-1,Height-1);
		}
		#endregion

		///<summary></summary>
		protected void OnCellDoubleClick(int col,int row){
			ODGridClickEventArgs gArgs=new ODGridClickEventArgs(0,row);
			if(CellDoubleClick!=null)
				CellDoubleClick(this,gArgs);
		}

		///<summary></summary>
		protected override void OnDoubleClick(EventArgs e){
			base.OnDoubleClick (e);
			if(MouseDownRow==-1){
				return;//double click was in the title or header section
			}
			OnCellDoubleClick(0,MouseDownRow);
		}

		///<summary></summary>
		protected void OnCellClick(int col,int row){
			ODGridClickEventArgs gArgs=new ODGridClickEventArgs(0,row);
			if(CellClick!=null)
				CellClick(this,gArgs);
		}

		///<summary></summary>
		protected override void OnClick(EventArgs e){
			base.OnClick (e);
			if(MouseDownRow==-1){
				return;//click was in the title or header section
			}
			OnCellClick(0,MouseDownRow);
		}

		///<summary></summary>
		protected override void OnResize(EventArgs e){
			base.OnResize (e);
			if(!hScrollVisible){
				int gridW=0;
				int minGridW=0;//sum of columns widths except last one.
				for(int i=0;i<columns.Count;i++){
					gridW+=columns[i].ColWidth;
					if(i<columns.Count-1){
						minGridW+=columns[i].ColWidth;
					}
				}
				if(Width<minGridW+2+vScroll.Width+5){//trying to make it too narrow
					this.Width=minGridW
					+2//outline
					+vScroll.Width
					+5;
				}
				else if(columns.Count>0){//resize the last column automatically
					columns[columns.Count-1].ColWidth=Width-2-vScroll.Width-minGridW;
				}
			}
			LayoutScrollBars();
			Invalidate();
		}

		///<summary>Call this before adding any rows.  You would typically call Rows.Clear after this.</summary>
		public void BeginUpdate(){
			IsUpdating=true;
		}

		///<summary>Must be called after adding rows.  This computes the rows, lays out the scrollbars, clears SelectedIndices, and invalidates.</summary>
		public void EndUpdate(){
			ComputeRows();
			LayoutScrollBars();
			selectedIndices=new ArrayList();
			IsUpdating=false;
			Invalidate();
		}

		///<summary>After adding rows to the grid, this calculates the height of each row because some rows may have text wrap and will take up more than one row.</summary>
		private void ComputeRows(){
			Graphics g=this.CreateGraphics();
			RowHeights=new int[rows.Count];
			RowLocs=new int[rows.Count];
			GridH=0;
			int cellH;
			for(int i=0;i<rows.Count;i++){
				RowHeights[i]=0;//rowHeight;
				for(int j=0;j<rows[i].Cells.Count;j++){
					cellH=(int)g.MeasureString(rows[i].Cells[j].Text,this.cellFont,columns[j].ColWidth).Height+1;
					if(cellH>RowHeights[i]){
						RowHeights[i]=cellH;
					}
				}
				if(i==0)
					RowLocs[i]=0;
				else
					RowLocs[i]=RowLocs[i-1]+RowHeights[i-1];
				GridH+=RowHeights[i];
			}
			g.Dispose();
		}

		private void LayoutScrollBars(){
			vScroll.Location=new Point(this.Width-vScroll.Width-1,titleHeight+headerHeight+1);
			if(this.hScrollVisible){
				hScroll.Visible=true;
				vScroll.Height=this.Height-titleHeight-headerHeight-hScroll.Height-2;
				hScroll.Location=new Point(1,this.Height-hScroll.Height-1);
				hScroll.Width=this.Width-vScroll.Width-2;
			}
			else{
				hScroll.Visible=false;
				vScroll.Height=this.Height-titleHeight-headerHeight-2;
			}
			//hScroll support incomplete
			if(GridH<vScroll.Height){
				vScroll.Value=0;
				vScroll.Enabled=false;
			}
			else{
				vScroll.Enabled=true;
				vScroll.Minimum = 0;
				vScroll.Maximum=GridH;
				vScroll.LargeChange=vScroll.Height;
				vScroll.SmallChange=(int)(14*3.4);//it's not an even number so that it is obvious to user that rows moved
			}
		}

		private void vScroll_Scroll(object sender,System.Windows.Forms.ScrollEventArgs e){
			Invalidate();
			this.Focus();
		}

		///<summary></summary>
		protected override void OnPaintBackground(PaintEventArgs pea) {
			//base.OnPaintBackground (pea);
			//don't paint background.  This reduces flickering.
		}

		///<summary>Usually called after entering a new list to automatically scroll to the end.</summary>
		public void ScrollToEnd(){
			ScrollValue=vScroll.Maximum;//this does all error checking and invalidates
		}

		///<summary>Use to set a row selected or not.</summary>
		public void SetSelected(int index,bool setValue){
			if(setValue){//select specified index
				if(selectionMode==SelectionMode.None){
					throw new Exception("Selection mode is none.");
				}
				else if(selectionMode==SelectionMode.One){
					selectedIndices.Clear();//clear existing selection before assigning the new one.
				}
				if(!selectedIndices.Contains(index)){
					selectedIndices.Add(index);
				}
			}
			else{//unselect specified index
				if(selectedIndices.Contains(index)){
					selectedIndices.Remove(index);
				}
			}
      Invalidate();
		}

		///<summary>Allows setting multiple values all at once</summary>
		public void SetSelected(int[] iArray,bool setValue){
			if(selectionMode==SelectionMode.None){
				throw new Exception("Selection mode is none.");
			}
			if(selectionMode==SelectionMode.One){
				throw new Exception("Selection mode is one.");
			}
			for(int i=0;i<iArray.Length;i++){
				if(setValue){//select specified index
					if(!selectedIndices.Contains(iArray[i])){
						selectedIndices.Add(iArray[i]);
					}
				}
				else{//unselect specified index
					if(selectedIndices.Contains(iArray[i])){
						selectedIndices.Remove(iArray[i]);
					}
				}
			}
			Invalidate();
		}

		///<summary>Sets all rows to specified value.</summary>
		public void SetSelected(bool setValue){
			if(selectionMode==SelectionMode.None){
				throw new Exception("Selection mode is none.");
			}
			if(selectionMode==SelectionMode.One && setValue==true){
				throw new Exception("Selection mode is one.");
			}
			selectedIndices.Clear();
			if(setValue){//select all
				for(int i=0;i<rows.Count;i++){
					selectedIndices.Add(i);
				}
			}
			Invalidate();
		}

		///<summary>Returns row.  Supply the y position in pixels.  But ALWAYS make sure the point is not in the header first, because the logic only works if within the visible grid area.</summary>
		private int PointToRow(int y){
			if(y<1+titleHeight+headerHeight){
				return-1;
			}
			for(int i=0;i<rows.Count;i++){
				if(y>-vScroll.Value+1+titleHeight+headerHeight+RowLocs[i]+RowHeights[i]){
					continue;//clicked below this row.
				}
				return i;
			}
			return -1;
		}

	

		#region MouseEvents

		///<summary></summary>
		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			if(e.Button==MouseButtons.Right){
				return;
			}
			MouseIsDown=true;
			MouseDownRow=PointToRow(e.Y);
			if(MouseDownRow==-1){//mouse down was in the title or header section
				return;
			}
			if(!allowSelection){
				return;//clicks do not trigger selection of rows, but cell click event still gets fired
			}
			switch(selectionMode){
				case SelectionMode.None:
					return;
				case SelectionMode.One:
					selectedIndices.Clear();
					selectedIndices.Add(MouseDownRow);
					break;
				case SelectionMode.MultiExtended:
					if(ControlIsDown){
						if(selectedIndices.Contains(MouseDownRow)){
							selectedIndices.Remove(MouseDownRow);
						}
            else{
						  selectedIndices.Add(MouseDownRow);
            }
					}
					else if(ShiftIsDown){
						if(selectedIndices.Count==0){
							selectedIndices.Add(MouseDownRow);
						}
						else{
							int fromRow=(int)selectedIndices[0];
							selectedIndices.Clear();
							if(MouseDownRow<fromRow){//dragging down
								for(int i=MouseDownRow;i<=fromRow;i++){
									selectedIndices.Add(i);
								}
							}
							else{
								for(int i=fromRow;i<=MouseDownRow;i++){
									selectedIndices.Add(i);
								}
							}
						}
					}
					else{//ctrl or shift not down
						selectedIndices.Clear();
						selectedIndices.Add(MouseDownRow);
					}
					break;
			}
			Invalidate();
		}

		///<summary>The purpose of this is to allow dragging to select multiple rows.  Only makes sense if selectionMode==MultiExtended.  Doesn't matter whether ctrl is down, because that only affects the mouse down event.</summary>
		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			MouseIsOver=true;
			if(!MouseIsDown){
				return;
			}
			if(selectionMode!=SelectionMode.MultiExtended){
				return;
			}
			int curRow=PointToRow(e.Y);
			if(curRow==-1 || curRow==MouseDownRow){
				return;
			}
			//because mouse might have moved faster than computer could keep up, we have to loop through all rows between
			if(MouseDownRow<curRow){//dragging down
				for(int i=MouseDownRow;i<=curRow;i++){
					if(!selectedIndices.Contains(i)){
						selectedIndices.Add(i);
					}
				}
			}
			else{
				for(int i=curRow;i<=MouseDownRow;i++){
					if(!selectedIndices.Contains(i)){
						selectedIndices.Add(i);
					}
				}
			}
			Invalidate();
		}

		///<summary></summary>
		protected override void OnMouseEnter(EventArgs e) {
			base.OnMouseEnter(e);
			MouseIsOver=true;
		}

		///<summary></summary>
		protected override void OnMouseLeave(EventArgs e) {
			base.OnMouseLeave (e);
			MouseIsOver=false;
		}

		private void vScroll_MouseEnter(Object sender,EventArgs e){
			MouseIsOver=true;
		}

		private void vScroll_MouseLeave(Object sender,EventArgs e){
			MouseIsOver=false;
		}

		private void vScroll_MouseMove(Object sender,MouseEventArgs e){
			MouseIsOver=true;
		}

		///<summary></summary>
		protected override void OnMouseUp(MouseEventArgs e){
			base.OnMouseUp(e);
			if(e.Button==MouseButtons.Right){
				return;
			}
			MouseIsDown=false;
		}

		private void Parent_MouseWheel(Object sender,MouseEventArgs e){
			if(MouseIsOver){
				//this.ac
				this.Select();//?
				//this.Focus();
			}
		}

		///<summary></summary>
		protected override void OnMouseWheel(MouseEventArgs e){
			base.OnMouseWheel(e);
			ScrollValue-=e.Delta/3;
		}
		
		#endregion

		#region KeyEvents

		///<summary></summary>
		protected override void OnKeyDown(KeyEventArgs e){
			base.OnKeyDown (e);
			if(e.KeyCode==Keys.ControlKey){
				ControlIsDown=true;
			}
			if(e.KeyCode==Keys.ShiftKey){
				ShiftIsDown=true;
			}
		}

		///<summary></summary>
		protected override void OnKeyUp(KeyEventArgs e){
			base.OnKeyUp (e);
			if(e.KeyCode==Keys.ControlKey){
				ControlIsDown=false;
			}
			if(e.KeyCode==Keys.ShiftKey){
				ShiftIsDown=false;
			}
		}

		private void Parent_KeyDown(Object sender,KeyEventArgs e){
			if(e.KeyCode==Keys.ControlKey){
				ControlIsDown=true;
			}
			if(e.KeyCode==Keys.ShiftKey){
				ShiftIsDown=true;
			}
		}

		private void Parent_KeyUp(Object sender,KeyEventArgs e){
			if(e.KeyCode==Keys.ControlKey){
				ControlIsDown=false;
			}
			if(e.KeyCode==Keys.ShiftKey){
				ShiftIsDown=false;
			}
		}

		#endregion

		

		

		















	}


	///<summary></summary>
	public class ODGridClickEventArgs{
		private int col;
		private int row;

		///<summary></summary>
		public ODGridClickEventArgs(int col,int row){
			this.col=col;
			this.row=row;
		}

		///<summary></summary>
		public int Row{
			get{ 
				return row;
			}
		}

		///<summary>Not actually used for anything yet, but we will soon have inline editing, so then it's important.  Just pass 0 for nowa and ignore it.</summary>
		public int Col{
			get{ 
				return col;
			}
		}

	}

}
