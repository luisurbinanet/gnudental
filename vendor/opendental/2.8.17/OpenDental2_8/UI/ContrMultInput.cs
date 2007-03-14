using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDental.Reporting;

namespace OpenDental.UI
{
	///<summary>This control can be used anywhere that the user can enter a series of values.</summary>
	///<remarks>Typically used in reports to let the user specify the parameters.  Will also be used in the Patient Edit window for custom fields, etc.  Each item in the list can be of a different input type, and this control will dynamically arrange them from the top down.  It adapts to many different widths.  If the input fields extend beyond the lower edge of the control, then a scrollbar is added.  This control can also be used for a series of values that are not to be displayed, but not altered.</remarks>
	public class ContrMultInput : System.Windows.Forms.UserControl{
		///<summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		private MultInputItemCollection multInputItems;
		private Label[] labels;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.VScrollBar vScrollBar2;
		private System.Windows.Forms.Panel panelSlide;
		private Control[] inputs;

		///<summary></summary>
		public ContrMultInput(){
			InitializeComponent();// This call is required by the Windows.Forms Form Designer.
			multInputItems=new MultInputItemCollection();
			panelSlide.MouseWheel += new System.Windows.Forms.MouseEventHandler(panelSlide_MouseWheel);
			vScrollBar2.MouseWheel += new System.Windows.Forms.MouseEventHandler(panelSlide_MouseWheel);
		}

		///<summary> Clean up any resources being used.</summary>
		protected override void Dispose( bool disposing ){
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
			this.panelMain = new System.Windows.Forms.Panel();
			this.vScrollBar2 = new System.Windows.Forms.VScrollBar();
			this.panelSlide = new System.Windows.Forms.Panel();
			this.panelMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.panelSlide);
			this.panelMain.Controls.Add(this.vScrollBar2);
			this.panelMain.Location = new System.Drawing.Point(1, 1);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(262, 313);
			this.panelMain.TabIndex = 0;
			// 
			// vScrollBar2
			// 
			this.vScrollBar2.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScrollBar2.Location = new System.Drawing.Point(245, 0);
			this.vScrollBar2.Name = "vScrollBar2";
			this.vScrollBar2.Size = new System.Drawing.Size(17, 313);
			this.vScrollBar2.TabIndex = 1;
			this.vScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar2_Scroll);
			// 
			// panelSlide
			// 
			this.panelSlide.Location = new System.Drawing.Point(0, 0);
			this.panelSlide.Name = "panelSlide";
			this.panelSlide.Size = new System.Drawing.Size(224, 276);
			this.panelSlide.TabIndex = 2;
			this.panelSlide.Click += new System.EventHandler(this.panelSlide_Click);
			// 
			// ContrMultInput
			// 
			this.Controls.Add(this.panelMain);
			this.Name = "ContrMultInput";
			this.Size = new System.Drawing.Size(272, 321);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ContrMultInput_Paint);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ContrMultInput_Layout);
			this.panelMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/*
		///<summary></summary>
		public MultInputItemCollection MultInputItems{
			get{
				if(!RetrieveData())
					return null;//can't retrieve any values if there is an error or if certain fields are blank.
				return multInputItems;
			}
			set{
				multInputItems=value;
			}
		}*/

		private void ContrMultInput_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			//MessageBox.Show("layout");
			if(multInputItems==null)
				return;
			Graphics g=this.CreateGraphics();
			this.SuspendLayout();
			panelMain.Width=Width-2;
			panelMain.Height=Height-2;
			vScrollBar2.Visible=false;
			panelSlide.Width=Width-2;
			if(!ArrangeControls(g)){//if the controls were too big to fit
				vScrollBar2.Visible=true;
				panelSlide.Width-=vScrollBar2.Width;
				ArrangeControls(g);//then layout again
			}
			g.Dispose();
			ResumeLayout();
		}

		private bool ArrangeControls(Graphics g){
			//calculate width of input section
			int inputW=300;//the widest allowed for the input section on the right.
			if(panelSlide.Width<600){
				inputW=panelSlide.Width/2;
			}
			int promptW=panelSlide.Width-inputW;
			panelSlide.Controls.Clear();
			int yPos=5;
			int itemH=0;//item height
			labels=new Label[multInputItems.Count];
			inputs=new Control[multInputItems.Count];
			for(int i=0;i<multInputItems.Count;i++){
				//Calculate height
				itemH=(int)g.MeasureString(((MultInputItem)multInputItems[i]).PromptingText,Font,promptW).Height;
				if(itemH<20)
					itemH=20;
				//promptingText
				labels[i]=new Label();
				labels[i].Location=new Point(5,yPos);
				//labels[i].Name="Label"+i.ToString();
				labels[i].Size=new Size(promptW-5,itemH);
				labels[i].Text=multInputItems[i].PromptingText;
				labels[i].TextAlign=ContentAlignment.MiddleRight;
				//labels[i].BorderStyle=BorderStyle.FixedSingle;//just used in debugging layout
				panelSlide.Controls.Add(labels[i]);
				if(multInputItems[i].ValueType==FieldValueType.Boolean){
					//add a checkbox
					inputs[i]=new CheckBox();
					inputs[i].Location=new Point(promptW,yPos+(itemH-20)/2);
					inputs[i].Size=new Size(inputW-5,20);
					if(multInputItems[i].CurrentValues.Count==0)
						((CheckBox)inputs[i]).Checked=false;
					else
						((CheckBox)inputs[i]).Checked=true;
					((CheckBox)inputs[i]).FlatStyle=FlatStyle.System;
					panelSlide.Controls.Add(inputs[i]);
				}
				else if(multInputItems[i].ValueType==FieldValueType.Date){
					//add a validDate box
					inputs[i]=new ValidDate();
					inputs[i].Location=new Point(promptW,yPos+(itemH-20)/2);
					if(inputW<100){//not enough room for a fullsize box
						inputs[i].Size=new Size(inputW-20,20);
					}
					else{
						inputs[i].Size=new Size(75,20);
					}
					;
					if(multInputItems[i].CurrentValues.Count>0){
						DateTime myDate=(DateTime)multInputItems[i].CurrentValues[0];
						inputs[i].Text=myDate.ToShortDateString();
					}
					panelSlide.Controls.Add(inputs[i]);
				}
				else if(multInputItems[i].ValueType==FieldValueType.Def){
					//add a psuedo combobox filled with visible defs for one category
					inputs[i]=new ComboBoxMulti();
					for(int j=0;j<Defs.Short[(int)multInputItems[i].DefCategory].Length;j++){
						((ComboBoxMulti)inputs[i]).Items.Add(Defs.Short[(int)multInputItems[i].DefCategory][j].ItemName);
						if(multInputItems[i].CurrentValues.Count > 0
							&& multInputItems[i].CurrentValues
							.Contains(Defs.Short[(int)multInputItems[i].DefCategory][j].DefNum))
						{
							((ComboBoxMulti)inputs[i]).SetSelected(j,true);
						}
					}
					inputs[i].Location=new Point(promptW,yPos+(itemH-20)/2);
					inputs[i].Size=new Size(inputW-5,20);
					panelSlide.Controls.Add(inputs[i]);
				}
				else if(multInputItems[i].ValueType==FieldValueType.Enum){
					//add a psuedo combobox filled with values for one enumeration
					inputs[i]=new ComboBoxMulti();
					Type eType=Type.GetType("OpenDental."+multInputItems[i].EnumerationType.ToString());
					for(int j=0;j<Enum.GetNames(eType).Length;j++){
						((ComboBoxMulti)inputs[i]).Items.Add(Enum.GetNames(eType)[j]);
						if(multInputItems[i].CurrentValues.Count > 0
							&& multInputItems[i].CurrentValues
							.Contains((int)(Enum.Parse(eType,Enum.GetNames(eType)[j])))  )
						{
							((ComboBoxMulti)inputs[i]).SetSelected(j,true);
						}
					}
					inputs[i].Location=new Point(promptW,yPos+(itemH-20)/2);
					inputs[i].Size=new Size(inputW-5,20);
					panelSlide.Controls.Add(inputs[i]);
				}
				else if(multInputItems[i].ValueType==FieldValueType.Integer){
					//add a validNumber box
					inputs[i]=new ValidNumber();
					inputs[i].Location=new Point(promptW,yPos+(itemH-20)/2);
					if(inputW<100){//not enough room for a fullsize box
						inputs[i].Size=new Size(inputW-20,20);
					}
					else{
						inputs[i].Size=new Size(75,20);
					}
					if(multInputItems[i].CurrentValues.Count>0){
						inputs[i].Text=((int)multInputItems[i].CurrentValues[0]).ToString();
					}
					panelSlide.Controls.Add(inputs[i]);
				}
				else if(multInputItems[i].ValueType==FieldValueType.Number){
					//add a validDouble box
					inputs[i]=new ValidDouble();
					inputs[i].Location=new Point(promptW,yPos+(itemH-20)/2);
					if(inputW<100){//not enough room for a fullsize box
						inputs[i].Size=new Size(inputW-20,20);
					}
					else{
						inputs[i].Size=new Size(75,20);
					}
					if(multInputItems[i].CurrentValues.Count>0){
						inputs[i].Text=((double)multInputItems[i].CurrentValues[0]).ToString("n");
					}
					panelSlide.Controls.Add(inputs[i]);
				}
				else if(multInputItems[i].ValueType==FieldValueType.String){
					//add a textbox
					inputs[i]=new TextBox();
					inputs[i].Location=new Point(promptW,yPos+(itemH-20)/2);
					//inputs[i].Name=
					inputs[i].Size=new Size(inputW-5,20);
					if(multInputItems[i].CurrentValues.Count>0){
						inputs[i].Text=multInputItems[i].CurrentValues[0].ToString();
					}
					panelSlide.Controls.Add(inputs[i]);
				}
				yPos+=itemH+5;
				if(yPos>panelMain.Height && !vScrollBar2.Visible)
					return false;//There's not enough room, so stop and make the scrollbar visible.
			}
			panelSlide.Height=yPos;
			vScrollBar2.Maximum=panelSlide.Height;
			vScrollBar2.Minimum=0;
			vScrollBar2.LargeChange=panelMain.Height;
			vScrollBar2.SmallChange=5;
			return true;
		}

		private void vScrollBar2_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e) {
			panelSlide.Location=new Point(0,-vScrollBar2.Value);
			if(ActiveControl==null){//only activate scroll if no control within this control is active
				vScrollBar2.Select();
			}
		}

		private void panelSlide_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e){
			if(!vScrollBar2.Visible){
				return;
			}
			int max=vScrollBar2.Maximum-vScrollBar2.LargeChange;
			int newScrollVal=vScrollBar2.Value-e.Delta/8;
			if(newScrollVal > max){
				vScrollBar2.Value=max;
			}
			else if(newScrollVal < vScrollBar2.Minimum){
				vScrollBar2.Value=vScrollBar2.Minimum;
			}
			else{
				vScrollBar2.Value=newScrollVal;
			}
			panelSlide.Location=new Point(0,-vScrollBar2.Value);
    }

		/// <summary></summary>
		public void AddInputItem(string promptingText,FieldValueType valueType,ArrayList currentValues,EnumType enumerationType,DefCat defCategory){
			multInputItems.Add(new MultInputItem(promptingText,valueType,currentValues,enumerationType,defCategory));
		}

		/// <summary>Overload for just one simple value.</summary>
		public void AddInputItem(string promptingText,FieldValueType valueType,object currentValue){
			ArrayList currentValues=new ArrayList();
			currentValues.Add(currentValue);
			//the enumtype and defcat are completely arbitrary.
			multInputItems.Add(new MultInputItem(promptingText,valueType,currentValues,EnumType.ApptStatus,DefCat.AccountColors));
		}

		/// <summary>Overload for using DefCat.</summary>
		public void AddInputItem(string promptingText,FieldValueType valueType,ArrayList currentValues,DefCat defCategory){
			if(currentValues==null)
				currentValues=new ArrayList();
			multInputItems.Add(new MultInputItem(promptingText,valueType,currentValues,EnumType.ApptStatus,defCategory));
		}

		/// <summary>Overload for using Enum.</summary>
		public void AddInputItem(string promptingText,FieldValueType valueType,ArrayList currentValues,EnumType enumerationType){
			if(currentValues==null)
				currentValues=new ArrayList();
			multInputItems.Add(new MultInputItem(promptingText,valueType,currentValues,enumerationType,DefCat.AccountColors));
		}

		private void ContrMultInput_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			e.Graphics.DrawRectangle(new Pen(Color.FromArgb(127,157,185))//blue
				,0,0,Width-1,Height-1);
		}

		private void panelSlide_Click(object sender, System.EventArgs e) {
			panelSlide.Select();
		}

		///<summary>This is called from the parent form to retrieve the data that the user entered.  Returns an arraylist.  For most fields, the length of the arraylist will be 0 or 1.</summary>
		public ArrayList GetCurrentValues(int item){
			ArrayList retVal=new ArrayList();
			if(multInputItems[item].ValueType==FieldValueType.Boolean){
				if(((CheckBox)inputs[item]).Checked){
					retVal.Add(true);
				}
			}
			else if(multInputItems[item].ValueType==FieldValueType.Date){
				if(((ValidDate)inputs[item]).errorProvider1.GetError(inputs[item])!=""){
					MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				}
				else if(inputs[item].Text!=""){
					retVal.Add(PIn.PDate(inputs[item].Text));
				}
			}
			else if(multInputItems[item].ValueType==FieldValueType.Def){
				ComboBoxMulti comboBox=(ComboBoxMulti)inputs[item];
				for(int j=0;j<comboBox.SelectedIndices.Count;j++){
					retVal.Add(
						Defs.Short[(int)multInputItems[item].DefCategory]
						[(int)comboBox.SelectedIndices[j]].DefNum);
				}
			}
			else if(multInputItems[item].ValueType==FieldValueType.Enum){
				ComboBoxMulti comboBox=(ComboBoxMulti)inputs[item];
				Type eType=Type.GetType("OpenDental."+multInputItems[item].EnumerationType.ToString());
				for(int j=0;j<comboBox.SelectedIndices.Count;j++){
					retVal.Add(
						(int)(Enum.Parse(eType,Enum.GetNames(eType)[(int)comboBox.SelectedIndices[j]])));
				}
			}
			else if(multInputItems[item].ValueType==FieldValueType.Integer){
				if(((ValidNumber)inputs[item]).errorProvider1.GetError(inputs[item])!=""){
					MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				}
				else if(inputs[item].Text!=""){
					retVal.Add(PIn.PInt(inputs[item].Text));
				}
			}
			else if(multInputItems[item].ValueType==FieldValueType.Number){
				if(((ValidDouble)inputs[item]).errorProvider1.GetError(inputs[item])!=""){
					MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				}
				else if(inputs[item].Text!=""){
					retVal.Add(PIn.PDouble(inputs[item].Text));
				}
			}
			else if(multInputItems[item].ValueType==FieldValueType.String){
				if(inputs[item].Text!=""){
					//the text is first stripped of any ?'s
					retVal.Add(Regex.Replace(inputs[item].Text,@"\?",""));
				}
			}
			//MessageBox.Show(multInputItems[1].CurrentValues.Count.ToString());
			return retVal;
		}

		



	}

	///<summary>A single input item in the ContrMultInput control.</summary>
	public struct MultInputItem{
		/// <summary></summary>
		public MultInputItem(string promptingText,FieldValueType valueType,ArrayList currentValues,EnumType enumerationType,DefCat defCategory){
			PromptingText=promptingText;
			ValueType=valueType;
			CurrentValues=currentValues;
			EnumerationType=enumerationType;
			DefCategory=defCategory;
		}

		///<summary>The text that prompts the user what information to enter.</summary>
		public string PromptingText;
		///<summary>The type of input that this item accepts.  Each input type displays a different input control for this item.</summary>
		public FieldValueType ValueType;
		///<summary>A collection of the actual values of this item, not just the displayed text.  Any supported type is allowed including string, int, double, bool, datetime, etc. The length of the ArrayList can be set to 0 ahead of time if there are no default values to fill in the input field with.  The result is that the field will initially be blank.  After the user input, if the field is still blank, then the count will still be 0.  If the count is 0, then this parameter will not be included as a filter in the query.</summary>
		public ArrayList CurrentValues;
		///<summary>If the ValueKind is EnumField, then this specifies which type of enum.</summary>
		public EnumType EnumerationType;
		///<summary>If ValueKind is DefParameter, then this specifies which DefCat.</summary>
		public DefCat DefCategory;
	}

	///<summary>Strongly typed collection of type MultInputItems.</summary>
	public class MultInputItemCollection:CollectionBase{
			///<summary>Returns the MenuInputItem with the given index.</summary>
		public MultInputItem this[int index]{
      get{
				return((MultInputItem)List[index]);
      }
      set{
				List[index]=value;
      }
		}

		///<summary></summary>
		public int Add(MultInputItem value){
			return(List.Add(value));
		}

		///<summary></summary>
		public int IndexOf(MultInputItem value){
			return(List.IndexOf(value));
		}

		///<summary></summary>
		public void Insert(int index,MultInputItem value){
			List.Insert(index,value);
		}

	}





}






























