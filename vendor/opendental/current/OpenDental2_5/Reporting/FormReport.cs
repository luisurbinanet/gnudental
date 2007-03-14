using Microsoft.CSharp;
//using Microsoft.Vsa;
using System.CodeDom.Compiler;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Windows.Forms;
//using OpenDental.Reporting;

namespace OpenDental.Reporting
{
	///<summary></summary>
	public class FormReport : System.Windows.Forms.Form{
		private System.Windows.Forms.Panel panel1;
		///<summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button butClose;
		private System.Windows.Forms.Button butPrint;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.PrintPreviewControl printPreviewControl2;
		private System.Windows.Forms.PrintDialog printDialog2;
		///<summary>The report to display.</summary>
		public ODReport Report;
		private System.Windows.Forms.Button butSetup;
		private System.Windows.Forms.PageSetupDialog setupDialog2;
		///<summary>The y position printed through so far in the current section.</summary>
		//private int printedThroughYPos; For now, assume all sections must remain together.
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label labelTotPages;
		private OpenDental.XPButton butBack;
		private OpenDental.XPButton butFwd;
		private Section lastSectionPrinted;//this really only keeps track of whether the details section and the reportfooter have finished printing. This variable will be refined when groups are implemented.
		private int rowsPrinted;
		private int totalPages;
		private int pagesPrinted;

		///<summary></summary>
		public FormReport(){
			InitializeComponent();// Required for Windows Form Designer support
			
		}

		/// <summary>Clean up any resources being used.</summary>
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormReport));
			this.butClose = new System.Windows.Forms.Button();
			this.butPrint = new System.Windows.Forms.Button();
			this.printPreviewControl2 = new System.Windows.Forms.PrintPreviewControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.labelTotPages = new System.Windows.Forms.Label();
			this.butBack = new OpenDental.XPButton();
			this.butFwd = new OpenDental.XPButton();
			this.button1 = new System.Windows.Forms.Button();
			this.butSetup = new System.Windows.Forms.Button();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.setupDialog2 = new System.Windows.Forms.PageSetupDialog();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClose.Location = new System.Drawing.Point(239, 2);
			this.butClose.Name = "butClose";
			this.butClose.TabIndex = 1;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butPrint
			// 
			this.butPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butPrint.Location = new System.Drawing.Point(1, 2);
			this.butPrint.Name = "butPrint";
			this.butPrint.TabIndex = 2;
			this.butPrint.Text = "&Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// printPreviewControl2
			// 
			this.printPreviewControl2.AutoZoom = false;
			this.printPreviewControl2.Location = new System.Drawing.Point(0, 0);
			this.printPreviewControl2.Name = "printPreviewControl2";
			this.printPreviewControl2.Size = new System.Drawing.Size(831, 570);
			this.printPreviewControl2.TabIndex = 3;
			this.printPreviewControl2.Zoom = 0.3;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.labelTotPages);
			this.panel1.Controls.Add(this.butBack);
			this.panel1.Controls.Add(this.butFwd);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.butSetup);
			this.panel1.Controls.Add(this.butPrint);
			this.panel1.Controls.Add(this.butClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(831, 35);
			this.panel1.TabIndex = 4;
			// 
			// labelTotPages
			// 
			this.labelTotPages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelTotPages.Location = new System.Drawing.Point(137, 4);
			this.labelTotPages.Name = "labelTotPages";
			this.labelTotPages.Size = new System.Drawing.Size(54, 18);
			this.labelTotPages.TabIndex = 19;
			this.labelTotPages.Text = "1 / 2";
			this.labelTotPages.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// butBack
			// 
			this.butBack.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBack.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butBack.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butBack.Image = ((System.Drawing.Image)(resources.GetObject("butBack.Image")));
			this.butBack.Location = new System.Drawing.Point(115, 1);
			this.butBack.Name = "butBack";
			this.butBack.Size = new System.Drawing.Size(18, 23);
			this.butBack.TabIndex = 20;
			this.butBack.Click += new System.EventHandler(this.butBack_Click);
			// 
			// butFwd
			// 
			this.butFwd.AdjustImageLocation = new System.Drawing.Point(1, 0);
			this.butFwd.BtnShape = OpenDental.enumType.BtnShape.Rectangle;
			this.butFwd.BtnStyle = OpenDental.enumType.XPStyle.Silver;
			this.butFwd.Image = ((System.Drawing.Image)(resources.GetObject("butFwd.Image")));
			this.butFwd.Location = new System.Drawing.Point(193, 1);
			this.butFwd.Name = "butFwd";
			this.butFwd.Size = new System.Drawing.Size(18, 23);
			this.butFwd.TabIndex = 21;
			this.butFwd.Click += new System.EventHandler(this.butFwd_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(506, 2);
			this.button1.Name = "button1";
			this.button1.TabIndex = 4;
			this.button1.Text = "Test";
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// butSetup
			// 
			this.butSetup.Location = new System.Drawing.Point(590, 2);
			this.butSetup.Name = "butSetup";
			this.butSetup.TabIndex = 3;
			this.butSetup.Text = "&Setup";
			this.butSetup.Visible = false;
			this.butSetup.Click += new System.EventHandler(this.butSetup_Click);
			// 
			// FormReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(831, 570);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.printPreviewControl2);
			this.Name = "FormReport";
			this.ShowInTaskbar = false;
			this.Text = "Report";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormReport_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormReport_Layout);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormReport_Load(object sender, System.EventArgs e) {
			ResetPd2();
			labelTotPages.Text=Lan.g(this,"/ "+totalPages.ToString());
			if(Report.IsLandscape){
				printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
					/(double)pd2.DefaultPageSettings.PaperSize.Width);
			}
			else{
				printPreviewControl2.Zoom=((double)printPreviewControl2.ClientSize.Height
					/(double)pd2.DefaultPageSettings.PaperSize.Height);
			}
			printPreviewControl2.Document=pd2;
		}

		private void FormReport_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			printPreviewControl2.Location=new System.Drawing.Point(0,panel1.Height);
			printPreviewControl2.Height=ClientSize.Height-panel1.Height;
			printPreviewControl2.Width=ClientSize.Width;	
		}
		
		private void ResetPd2(){
			pd2=new PrintDocument();
			pd2.PrintPage += new PrintPageEventHandler(this.pd2_PrintPage);
			lastSectionPrinted=null;
			rowsPrinted=0;
			pagesPrinted=0;
			if(Report.IsLandscape){
				pd2.DefaultPageSettings.Landscape=true;
			}
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd2.OriginAtMargins=true;//the actual margins are taken into consideration in the printpage event, and if the user specifies 0,0 for margins, then the report will reliably print on a preprinted form. Origin is ALWAYS the corner of the paper.
		}

		///<summary></summary>
		public void PrintReport(){
			pd2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(!pd2.PrinterSettings.IsValid){
				pd2.PrinterSettings.PrinterName=null;
			}
			try{
				printDialog2=new PrintDialog();
				printDialog2.Document=pd2;
				if(printDialog2.ShowDialog()==DialogResult.OK){
					pd2.Print();
				}
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
			
		}

		///<summary>raised for each page to be printed.</summary>
		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){
			//Note that the locations of the reportObjects are not absolute.  They depend entirely upon the margins.  When the report is initially created, it is pushed up against the upper and the left.
			Graphics grfx=ev.Graphics;
			//xPos and yPos represent the upper left of current section after margins are accounted for.
			//All reportObjects are then placed relative to this origin.
			Margins currentMargins=null;
			Size paperSize;
			if(Report.IsLandscape)
				paperSize=new Size(1100,850);
			else
				paperSize=new Size(850,1100);
			if(Report.ReportMargins==null){
				if(Report.IsLandscape)
					currentMargins=new Margins(50,0,30,30);
				else
					currentMargins=new Margins(30,0,50,50);
			}
			else{
				currentMargins=Report.ReportMargins;
			}
			int xPos=currentMargins.Left;
			int yPos=currentMargins.Top;
			int printableHeight=paperSize.Height-currentMargins.Top-currentMargins.Bottom;
			int yLimit=paperSize.Height-currentMargins.Bottom;//the largest yPos allowed
			//Now calculate and layout each section in sequence.
			Section section;
			//for(int sectionIndex=0;sectionIndex<Report.Sections.Count;sectionIndex++){
			while(true){//will break out if no more room on page
				//if no sections have been printed yet, print a report header.
				if(lastSectionPrinted==null){
					section=Report.Sections.GetOfKind(AreaSectionKind.ReportHeader);
					PrintSection(grfx,section,xPos,yPos);
					lastSectionPrinted=section;
					yPos+=section.Height;
					if(section.Height>printableHeight){//this can happen if the reportHeader takes up the full page
						//if there are no other sections to print
						if(Report.ReportTable==null){
							//this will keep the second page from printing:
							lastSectionPrinted=Report.Sections.GetOfKind(AreaSectionKind.ReportFooter);
						}
						break;
					}
				}
				//If the size of pageheader+one detail+pagefooter is taller than page, then we might later display an error. But for now, they will all still be laid out, and whatever goes off the bottom edge will just not show.  This will not be an issue for normal reports:
				if(Report.Sections.GetOfKind(AreaSectionKind.PageHeader).Height
					+Report.Sections.GetOfKind(AreaSectionKind.Detail).Height
					+Report.Sections.GetOfKind(AreaSectionKind.PageFooter).Height
					>printableHeight){
					//nothing for now.
				}
				//If this is first page and not enough room to print reportheader+pageheader+detail+pagefooter.
				if(pagesPrinted==0
					&& Report.Sections.GetOfKind(AreaSectionKind.ReportHeader).Height
					+Report.Sections.GetOfKind(AreaSectionKind.PageHeader).Height
					+Report.Sections.GetOfKind(AreaSectionKind.Detail).Height
					+Report.Sections.GetOfKind(AreaSectionKind.PageFooter).Height
					>printableHeight)
				{
					break;
				}
				//always print a page header
				section=Report.Sections.GetOfKind(AreaSectionKind.PageHeader);
				PrintSection(grfx,section,xPos,yPos);
				yPos+=section.Height;
				//calculate if there is room for all elements including the reportfooter on this page.
				int rowsRemaining=0;
				if(Report.ReportTable!=null){
					rowsRemaining=Report.ReportTable.Rows.Count-rowsPrinted;
				}
				int totalDetailsHeight=rowsRemaining*Report.Sections.GetOfKind(AreaSectionKind.Detail).Height;
				bool isRoomForReportFooter=true;
				if(yLimit-yPos
					-Report.Sections.GetOfKind(AreaSectionKind.ReportFooter).Height
					-Report.Sections.GetOfKind(AreaSectionKind.PageFooter).Height
					-totalDetailsHeight < 0){
					isRoomForReportFooter=false;
				}
				//calculate how many rows of detail to print
				int rowsToPrint=rowsRemaining;
				section=Report.Sections.GetOfKind(AreaSectionKind.Detail);
				if(!isRoomForReportFooter){
					int actualDetailsHeight=yLimit-yPos
						-Report.Sections.GetOfKind(AreaSectionKind.ReportFooter).Height
						-Report.Sections.GetOfKind(AreaSectionKind.PageFooter).Height;
					rowsToPrint=(int)(actualDetailsHeight
						/Report.Sections.GetOfKind(AreaSectionKind.Detail).Height);
					if(rowsToPrint<1)
						rowsToPrint=1;//Always print at least one row.
				}
				//print the detail section
				PrintDetailsSection(grfx,section,xPos,yPos,rowsToPrint);
				if(rowsToPrint==rowsRemaining)//if all remaining rows were printed
					lastSectionPrinted=section;//mark this section as printed.
				yPos+=section.Height*rowsToPrint;
				//print the reportfooter section if there is room
				if(isRoomForReportFooter){
					section=Report.Sections.GetOfKind(AreaSectionKind.ReportFooter);
					PrintSection(grfx,section,xPos,yPos);
					lastSectionPrinted=section;//mark the reportfooter as printed.  This will prevent another loop.
					yPos+=section.Height;
				}
				//print the pagefooter
				section=Report.Sections.GetOfKind(AreaSectionKind.PageFooter);
				if(isRoomForReportFooter){
					//for the last page, this moves the pagefooter to the bottom of the page.
					yPos=yLimit-section.Height;
				}
				PrintSection(grfx,section,xPos,yPos);
				yPos+=section.Height;
				break;
			}//while			
			pagesPrinted++;
			//if the reportfooter has been printed, then there are no more pages.
			if(lastSectionPrinted==Report.Sections.GetOfKind(AreaSectionKind.ReportFooter)){
				ev.HasMorePages=false;
				totalPages=pagesPrinted;
				labelTotPages.Text="1 / "+totalPages.ToString();
			}
			else{
				ev.HasMorePages=true;
			}
		}

		/// <summary>Prints one section other than details at the specified x and y position on the page.  The math to decide whether it will fit on the current page is done ahead of time. There is no mechanism for splitting a section across multiple pages.</summary>
		/// <param name="g">The graphics object to use.</param>
		/// <param name="section">The section to print.</param>
		/// <param name="xPos">The xPos of this section.</param>
		/// <param name="yPos">The yPos of this section.</param>
		private void PrintSection(Graphics g,Section section,int xPos,int yPos){
			TextObject textObject;
			FieldObject fieldObject;
			//LineObject lineObject;
			//BoxObject boxObject;
			StringFormat strFormat;//used each time text is drawn to handle alignment issues
			//string rawText="";//the raw text for a given field as taken from the database
			string displayText="";//The formatted text to print
			foreach(ReportObject reportObject in Report.ReportObjects){
				//todo later: check for lines and boxes that span multiple sections.
				if(reportObject.SectionIndex!=Report.Sections.IndexOf(section)){
					continue;
				}
				if(reportObject.GetType()==typeof(TextObject)){
					textObject=(TextObject)reportObject;
					strFormat=ReportObject.GetStringFormat(textObject.TextAlign);
					RectangleF layoutRect=new RectangleF(xPos+textObject.Location.X
						,yPos+textObject.Location.Y
						,textObject.Size.Width,textObject.Size.Height);
					g.DrawString(textObject.Text,textObject.Font,Brushes.Black,layoutRect,strFormat);
				}
				else if(reportObject.GetType()==typeof(FieldObject)){
					fieldObject=(FieldObject)reportObject;
					strFormat=ReportObject.GetStringFormat(fieldObject.TextAlign);
					RectangleF layoutRect=new RectangleF(xPos+fieldObject.Location.X
						,yPos+fieldObject.Location.Y
						,fieldObject.Size.Width,fieldObject.Size.Height);
					displayText="";
					if(fieldObject.DataSource.Kind==FieldKind.SummaryField){
						displayText=((SummaryFieldDefinition)fieldObject.DataSource)
							.GetValue(Report.ReportTable,Report.DataFields.IndexOf
							(((SummaryFieldDefinition)fieldObject.DataSource).SummarizedField.Name))
							.ToString(fieldObject.FormatString);
					}
					else if(fieldObject.DataSource.Kind==FieldKind.SpecialVarField){
						if(((SpecialVarFieldDefinition)fieldObject.DataSource).SpecialVarType
							==SpecialVarType.PageNofM){//not functional yet
							//displayText=Lan.g(this,"Page")+" "+(pagesPrinted+1).ToString()
							//	+Lan.g(
						}
						else if(((SpecialVarFieldDefinition)fieldObject.DataSource).SpecialVarType
							==SpecialVarType.PageNumber){
							displayText=Lan.g(this,"Page")+" "+(pagesPrinted+1).ToString();
						}
					}
					g.DrawString(displayText,fieldObject.Font,Brushes.Black,layoutRect,strFormat);
				}
				//todo: else if lines
				//todo: else if boxes.
			}//foreach reportObject
			//sectionsPrinted=sectionIndex+1;//mark current section as printed.
			//MessageBox.Show(pagesPrinted.ToString()+","+sectionsPrinted.ToString());
			//yPos+=section.Height;//set current yPos to the bottom of the section just printed.
		}

		/// <summary>Prints some rows of the details section at the specified x and y position on the page.  The math to decide how many rows to print is done ahead of time.  The number of rows printed so far is kept global so that it can be used in calculating the layout of this section.</summary>
		/// <param name="g">The graphics object to use.</param>
		/// <param name="section">The section to print.</param>
		/// <param name="xPos">The xPos of this section.</param>
		/// <param name="yPos">The yPos of this section.</param>
		/// <param name="rowsToPrint">The number of rows to print.</param>
		private void PrintDetailsSection(Graphics g,Section section,int xPos,int yPos,int rowsToPrint){
			TextObject textObject;
			FieldObject fieldObject;
			//LineObject lineObject;
			//BoxObject boxObject;
			StringFormat strFormat;//used each time text is drawn to handle alignment issues
			string rawText="";//the raw text for a given field as taken from the database
			string displayText="";//The formatted text to print
			//loop through each row in the table
			for(int i=rowsPrinted;i<rowsPrinted+rowsToPrint;i++){
				foreach(ReportObject reportObject in Report.ReportObjects){
					//todo later: check for lines and boxes that span multiple sections.
					if(reportObject.SectionIndex!=Report.Sections.IndexOf(section)){
						//skip any reportObjects that are not in this section
						continue;
					}
					if(reportObject.GetType()==typeof(TextObject)){
						//not typical to print textobject in details section, but allowed
						textObject=(TextObject)reportObject;
						strFormat=ReportObject.GetStringFormat(textObject.TextAlign);
						RectangleF layoutRect=new RectangleF(xPos+textObject.Location.X
							,yPos+textObject.Location.Y
							,textObject.Size.Width,textObject.Size.Height);
						g.DrawString(textObject.Text,textObject.Font
							,new SolidBrush(textObject.TextColor),layoutRect,strFormat);
					}
					else if(reportObject.GetType()==typeof(FieldObject)){
						fieldObject=(FieldObject)reportObject;
						strFormat=ReportObject.GetStringFormat(fieldObject.TextAlign);
						RectangleF layoutRect=new RectangleF(xPos+fieldObject.Location.X,yPos+fieldObject.Location.Y,fieldObject.Size.Width,fieldObject.Size.Height);
						if(fieldObject.DataSource.Kind==FieldKind.DataTableField){
							rawText=Report.ReportTable.Rows
								[i][Report.DataFields.IndexOf(fieldObject.DataSource.Name)].ToString();
							displayText=rawText;
							//suppress if duplicate:
							if(i>0 && fieldObject.SuppressIfDuplicate && rawText==Report.ReportTable.Rows[i-1][Report.DataFields.IndexOf(fieldObject.DataSource.Name)].ToString()){
								displayText="";
							}
							else if(fieldObject.DataSource.ValueType==FieldValueType.BooleanField){
								displayText=PIn.PBool(Report.ReportTable.Rows[i][Report.DataFields.IndexOf(fieldObject.DataSource.Name)].ToString()).ToString();//(fieldObject.FormatString);
							}
							else if(fieldObject.DataSource.ValueType==FieldValueType.DateTimeField){
								displayText=PIn.PDateT(Report.ReportTable.Rows[i][Report.DataFields.IndexOf(fieldObject.DataSource.Name)].ToString()).ToString(fieldObject.FormatString);
							}
							else if(fieldObject.DataSource.ValueType==FieldValueType.DoubleField){
								displayText=PIn.PDouble(Report.ReportTable.Rows[i][Report.DataFields.IndexOf(fieldObject.DataSource.Name)].ToString()).ToString(fieldObject.FormatString);
							}
							else if(fieldObject.DataSource.ValueType==FieldValueType.IntField){
								displayText=PIn.PInt(Report.ReportTable.Rows[i][Report.DataFields.IndexOf(fieldObject.DataSource.Name)].ToString()).ToString(fieldObject.FormatString);
							}
							else if(fieldObject.DataSource.ValueType==FieldValueType.StringField){
								displayText=rawText;
							}
						}
						else if(fieldObject.DataSource.Kind==FieldKind.FormulaField){
							//can't do formulas yet
						}
						else if(fieldObject.DataSource.Kind==FieldKind.SpecialVarField){
							
						}
						else if(fieldObject.DataSource.Kind==FieldKind.SummaryField){
							
						}
						g.DrawString(displayText,fieldObject.Font
							,new SolidBrush(fieldObject.TextColor),layoutRect,strFormat);
					}
					//todo: else if lines
					//todo: else if boxes.
				}//foreach reportObject
				yPos+=section.Height;
			}//for i rows
			rowsPrinted+=rowsToPrint;
		}

		private void butSetup_Click(object sender, System.EventArgs e) {
			setupDialog2.AllowMargins=true;
			setupDialog2.AllowOrientation=true;
			setupDialog2.AllowPaper=false;
			setupDialog2.AllowPrinter=false;
			setupDialog2.Document=pd2;
			setupDialog2.ShowDialog();
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			ResetPd2();
			PrintReport();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e) {
			//ScriptEngine.FormulaCode = 
			string functionCode=
			@"using System.Windows.Forms;
				using System;
				public class Test{
					public static void Main(){
						MessageBox.Show(""This is a test"");
						Test2 two = new Test2();
						two.Stuff();
					}
				}
				public class Test2{
					public void Stuff(){

					}
				}";
			CodeDomProvider codeProvider=new CSharpCodeProvider();
			ICodeCompiler compiler = codeProvider.CreateCompiler();
			CompilerParameters compilerParams = new CompilerParameters();
			compilerParams.CompilerOptions = "/target:library /optimize";
			compilerParams.GenerateExecutable = false;
			compilerParams.GenerateInMemory = true;
			compilerParams.IncludeDebugInformation = false;
			compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
			compilerParams.ReferencedAssemblies.Add("System.dll");
			compilerParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			CompilerResults results = compiler.CompileAssemblyFromSource(
                             compilerParams,functionCode);
			if (results.Errors.Count > 0){
				MessageBox.Show(results.Errors[0].ErrorText);
				//foreach (CompilerError error in results.Errors)
				//	DotNetScriptEngine.LogAllErrMsgs("Compine Error:"+error.ErrorText); 
				return;
			}
			Assembly assembly = results.CompiledAssembly;	
			//Use reflection to call the Main function in the assembly
			ScriptEngine.RunScript(assembly, "Main");		
			

		}

		private void butBack_Click(object sender, System.EventArgs e){
			if(printPreviewControl2.StartPage==0) return;
			printPreviewControl2.StartPage--;
			labelTotPages.Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();
		}

		private void butFwd_Click(object sender, System.EventArgs e){
			if(printPreviewControl2.StartPage==totalPages-1) return;
			printPreviewControl2.StartPage++;
			labelTotPages.Text=(printPreviewControl2.StartPage+1).ToString()
				+" / "+totalPages.ToString();
		}

		

		


	}
}
