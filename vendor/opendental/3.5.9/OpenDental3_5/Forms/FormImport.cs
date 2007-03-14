using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormImport : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGrid grid;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button butSubstitute;
		private DataTable table;
		private System.Windows.Forms.TextBox textSubstOld;
		private System.Windows.Forms.TextBox textSubstNew;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button butFixDates;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textDateOldFormats;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox textSepChar;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button butCombine;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button butRename;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Button butMoveCol;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.RadioButton radioFirst;
		private System.Windows.Forms.RadioButton radioAfter;
		private System.Windows.Forms.Button butValidate;
		private System.Windows.Forms.Button butImport;
		private System.Windows.Forms.Button butRefresh;
		private System.Windows.Forms.TabControl tabContr;
		private System.Windows.Forms.TabPage tabLoadData;
		private System.Windows.Forms.TabPage tabEdit;
		private System.Windows.Forms.TabPage tabSpecialtyF;
		private System.Windows.Forms.TabPage tabFinalImport;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox textNewTable;
		private System.Windows.Forms.TextBox textFileName;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Button butBrowse;
		private System.Windows.Forms.Button butLoad;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TabPage tabColumns;
		private System.Windows.Forms.TabPage tabRows;
		private System.Windows.Forms.Button butDeleteRows;
		private System.Windows.Forms.Button butDeleteTable;
		private System.Windows.Forms.ComboBox comboTableName;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.ComboBox comboColSubst;
		private System.Windows.Forms.ComboBox comboColMove;
		private System.Windows.Forms.ComboBox comboColMoveAfter;
		private System.Windows.Forms.ComboBox comboColCombine1;
		private System.Windows.Forms.ComboBox comboColCombine2;
		private System.Windows.Forms.ComboBox comboColRename;
		private System.Windows.Forms.ComboBox comboColDelete;
		private System.Windows.Forms.Button butDeleteCol;
		private System.Windows.Forms.ComboBox comboColNewName;
		///<summary>The name of the column which is the primary key, or "" for no primary key.</summary>
		private string pkCol="";
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textRows;
		private System.Windows.Forms.ComboBox comboColDateSource;
		private System.Windows.Forms.ComboBox comboColDateDest;
		private System.Windows.Forms.RadioButton radioInsert;
		private System.Windows.Forms.RadioButton radioUpdate;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.Button butAddCol;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TabPage tabPK;
		private System.Windows.Forms.Button butClearPK;
		private System.Windows.Forms.ComboBox comboColPK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butSetPK;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.RadioButton radioPatients;
		private System.Windows.Forms.RadioButton radioCarriers;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.ComboBox comboColAdd;
		private System.Windows.Forms.TabPage tabQuery;
		private System.Windows.Forms.Button butQuerySubmit;
		private System.Windows.Forms.TextBox textQuery;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.ComboBox comboColClear;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Button butColClear;
		private System.Windows.Forms.TextBox textDateNewFormat;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.CheckBox checkDontUsePK;
		private System.Windows.Forms.Label label28;
		private string[] AllowedColNames;

		///<summary></summary>
		public FormImport()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormImport));
			this.grid = new System.Windows.Forms.DataGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.butRefresh = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butSubstitute = new System.Windows.Forms.Button();
			this.textSubstNew = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textSubstOld = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboColSubst = new System.Windows.Forms.ComboBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textDateNewFormat = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.comboColDateDest = new System.Windows.Forms.ComboBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.butFixDates = new System.Windows.Forms.Button();
			this.textDateOldFormats = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.comboColDateSource = new System.Windows.Forms.ComboBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.comboColCombine2 = new System.Windows.Forms.ComboBox();
			this.comboColCombine1 = new System.Windows.Forms.ComboBox();
			this.butCombine = new System.Windows.Forms.Button();
			this.textSepChar = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.comboColNewName = new System.Windows.Forms.ComboBox();
			this.comboColRename = new System.Windows.Forms.ComboBox();
			this.butRename = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.radioAfter = new System.Windows.Forms.RadioButton();
			this.radioFirst = new System.Windows.Forms.RadioButton();
			this.butMoveCol = new System.Windows.Forms.Button();
			this.label15 = new System.Windows.Forms.Label();
			this.comboColMove = new System.Windows.Forms.ComboBox();
			this.comboColMoveAfter = new System.Windows.Forms.ComboBox();
			this.butValidate = new System.Windows.Forms.Button();
			this.butImport = new System.Windows.Forms.Button();
			this.tabContr = new System.Windows.Forms.TabControl();
			this.tabLoadData = new System.Windows.Forms.TabPage();
			this.comboTableName = new System.Windows.Forms.ComboBox();
			this.butDeleteTable = new System.Windows.Forms.Button();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.butLoad = new System.Windows.Forms.Button();
			this.butBrowse = new System.Windows.Forms.Button();
			this.label20 = new System.Windows.Forms.Label();
			this.textFileName = new System.Windows.Forms.TextBox();
			this.textNewTable = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.tabPK = new System.Windows.Forms.TabPage();
			this.label25 = new System.Windows.Forms.Label();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.radioPatients = new System.Windows.Forms.RadioButton();
			this.radioCarriers = new System.Windows.Forms.RadioButton();
			this.butClearPK = new System.Windows.Forms.Button();
			this.comboColPK = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butSetPK = new System.Windows.Forms.Button();
			this.tabColumns = new System.Windows.Forms.TabPage();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.comboColAdd = new System.Windows.Forms.ComboBox();
			this.butAddCol = new System.Windows.Forms.Button();
			this.label24 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.comboColDelete = new System.Windows.Forms.ComboBox();
			this.butDeleteCol = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.tabRows = new System.Windows.Forms.TabPage();
			this.label23 = new System.Windows.Forms.Label();
			this.textRows = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.butDeleteRows = new System.Windows.Forms.Button();
			this.tabEdit = new System.Windows.Forms.TabPage();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.comboColClear = new System.Windows.Forms.ComboBox();
			this.butColClear = new System.Windows.Forms.Button();
			this.label26 = new System.Windows.Forms.Label();
			this.tabSpecialtyF = new System.Windows.Forms.TabPage();
			this.tabQuery = new System.Windows.Forms.TabPage();
			this.butQuerySubmit = new System.Windows.Forms.Button();
			this.textQuery = new System.Windows.Forms.TextBox();
			this.tabFinalImport = new System.Windows.Forms.TabPage();
			this.radioUpdate = new System.Windows.Forms.RadioButton();
			this.radioInsert = new System.Windows.Forms.RadioButton();
			this.checkDontUsePK = new System.Windows.Forms.CheckBox();
			this.label28 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.tabContr.SuspendLayout();
			this.tabLoadData.SuspendLayout();
			this.tabPK.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.tabColumns.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabRows.SuspendLayout();
			this.tabEdit.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.tabSpecialtyF.SuspendLayout();
			this.tabQuery.SuspendLayout();
			this.tabFinalImport.SuspendLayout();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.DataMember = "";
			this.grid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.grid.Location = new System.Drawing.Point(0, 244);
			this.grid.Name = "grid";
			this.grid.ReadOnly = true;
			this.grid.Size = new System.Drawing.Size(1191, 602);
			this.grid.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9, 128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Temp Table";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butRefresh
			// 
			this.butRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butRefresh.Location = new System.Drawing.Point(238, 123);
			this.butRefresh.Name = "butRefresh";
			this.butRefresh.TabIndex = 6;
			this.butRefresh.Text = "Refresh";
			this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butSubstitute);
			this.groupBox1.Controls.Add(this.textSubstNew);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textSubstOld);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.comboColSubst);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(7, 7);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(250, 128);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Substitution";
			// 
			// butSubstitute
			// 
			this.butSubstitute.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSubstitute.Location = new System.Drawing.Point(165, 95);
			this.butSubstitute.Name = "butSubstitute";
			this.butSubstitute.TabIndex = 9;
			this.butSubstitute.Text = "Substitute";
			this.butSubstitute.Click += new System.EventHandler(this.butSubstitute_Click);
			// 
			// textSubstNew
			// 
			this.textSubstNew.Location = new System.Drawing.Point(116, 68);
			this.textSubstNew.Name = "textSubstNew";
			this.textSubstNew.Size = new System.Drawing.Size(123, 20);
			this.textSubstNew.TabIndex = 8;
			this.textSubstNew.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(10, 69);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 7;
			this.label5.Text = "New Value";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSubstOld
			// 
			this.textSubstOld.Location = new System.Drawing.Point(116, 44);
			this.textSubstOld.Name = "textSubstOld";
			this.textSubstOld.Size = new System.Drawing.Size(123, 20);
			this.textSubstOld.TabIndex = 6;
			this.textSubstOld.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(11, 46);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "Old Value";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(10, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "Column";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboColSubst
			// 
			this.comboColSubst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboColSubst.Location = new System.Drawing.Point(116, 19);
			this.comboColSubst.MaxDropDownItems = 100;
			this.comboColSubst.Name = "comboColSubst";
			this.comboColSubst.Size = new System.Drawing.Size(124, 21);
			this.comboColSubst.TabIndex = 8;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textDateNewFormat);
			this.groupBox3.Controls.Add(this.label27);
			this.groupBox3.Controls.Add(this.comboColDateDest);
			this.groupBox3.Controls.Add(this.label17);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.butFixDates);
			this.groupBox3.Controls.Add(this.textDateOldFormats);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.comboColDateSource);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(8, 8);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(302, 189);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Fix Dates";
			// 
			// textDateNewFormat
			// 
			this.textDateNewFormat.Location = new System.Drawing.Point(121, 122);
			this.textDateNewFormat.Name = "textDateNewFormat";
			this.textDateNewFormat.Size = new System.Drawing.Size(170, 20);
			this.textDateNewFormat.TabIndex = 18;
			this.textDateNewFormat.Text = "MM/dd/yyyy";
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(4, 124);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(115, 16);
			this.label27.TabIndex = 17;
			this.label27.Text = "New Format";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboColDateDest
			// 
			this.comboColDateDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboColDateDest.Location = new System.Drawing.Point(121, 76);
			this.comboColDateDest.MaxDropDownItems = 100;
			this.comboColDateDest.Name = "comboColDateDest";
			this.comboColDateDest.Size = new System.Drawing.Size(170, 21);
			this.comboColDateDest.TabIndex = 16;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(14, 20);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(275, 30);
			this.label17.TabIndex = 12;
			this.label17.Text = "Very rarely used.  Might be used if month and day are switched in source file";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 80);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 10;
			this.label6.Text = "Destination Col";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butFixDates
			// 
			this.butFixDates.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butFixDates.Location = new System.Drawing.Point(218, 150);
			this.butFixDates.Name = "butFixDates";
			this.butFixDates.TabIndex = 9;
			this.butFixDates.Text = "Fix";
			this.butFixDates.Click += new System.EventHandler(this.butFixDates_Click);
			// 
			// textDateOldFormats
			// 
			this.textDateOldFormats.Location = new System.Drawing.Point(121, 100);
			this.textDateOldFormats.Name = "textDateOldFormats";
			this.textDateOldFormats.Size = new System.Drawing.Size(170, 20);
			this.textDateOldFormats.TabIndex = 6;
			this.textDateOldFormats.Text = "dd/MM/yyyy,d/M/yyyy,dd/M/yyyy,d/MM/yyyy";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(4, 102);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(115, 16);
			this.label7.TabIndex = 5;
			this.label7.Text = "Old Formats sep by ,";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 57);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 16);
			this.label8.TabIndex = 3;
			this.label8.Text = "Source Column";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboColDateSource
			// 
			this.comboColDateSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboColDateSource.Location = new System.Drawing.Point(121, 52);
			this.comboColDateSource.MaxDropDownItems = 100;
			this.comboColDateSource.Name = "comboColDateSource";
			this.comboColDateSource.Size = new System.Drawing.Size(170, 21);
			this.comboColDateSource.TabIndex = 15;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.comboColCombine2);
			this.groupBox4.Controls.Add(this.comboColCombine1);
			this.groupBox4.Controls.Add(this.butCombine);
			this.groupBox4.Controls.Add(this.textSepChar);
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.label10);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(230, 6);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(235, 128);
			this.groupBox4.TabIndex = 10;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Combine Columns";
			// 
			// comboColCombine2
			// 
			this.comboColCombine2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboColCombine2.Location = new System.Drawing.Point(101, 42);
			this.comboColCombine2.MaxDropDownItems = 100;
			this.comboColCombine2.Name = "comboColCombine2";
			this.comboColCombine2.Size = new System.Drawing.Size(123, 21);
			this.comboColCombine2.TabIndex = 16;
			// 
			// comboColCombine1
			// 
			this.comboColCombine1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboColCombine1.Location = new System.Drawing.Point(101, 16);
			this.comboColCombine1.MaxDropDownItems = 100;
			this.comboColCombine1.Name = "comboColCombine1";
			this.comboColCombine1.Size = new System.Drawing.Size(123, 21);
			this.comboColCombine1.TabIndex = 15;
			// 
			// butCombine
			// 
			this.butCombine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butCombine.Location = new System.Drawing.Point(150, 95);
			this.butCombine.Name = "butCombine";
			this.butCombine.TabIndex = 9;
			this.butCombine.Text = "Combine";
			this.butCombine.Click += new System.EventHandler(this.butCombine_Click);
			// 
			// textSepChar
			// 
			this.textSepChar.Location = new System.Drawing.Point(101, 68);
			this.textSepChar.Name = "textSepChar";
			this.textSepChar.Size = new System.Drawing.Size(42, 20);
			this.textSepChar.TabIndex = 8;
			this.textSepChar.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(12, 69);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(83, 16);
			this.label9.TabIndex = 7;
			this.label9.Text = "Sep Char";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12, 45);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(83, 16);
			this.label10.TabIndex = 5;
			this.label10.Text = "Column 2";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(12, 21);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(83, 16);
			this.label11.TabIndex = 3;
			this.label11.Text = "Column 1";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.comboColNewName);
			this.groupBox5.Controls.Add(this.comboColRename);
			this.groupBox5.Controls.Add(this.butRename);
			this.groupBox5.Controls.Add(this.label13);
			this.groupBox5.Controls.Add(this.label14);
			this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox5.Location = new System.Drawing.Point(468, 6);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(250, 128);
			this.groupBox5.TabIndex = 11;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Rename Column";
			// 
			// comboColNewName
			// 
			this.comboColNewName.Location = new System.Drawing.Point(116, 43);
			this.comboColNewName.MaxDropDownItems = 100;
			this.comboColNewName.Name = "comboColNewName";
			this.comboColNewName.Size = new System.Drawing.Size(123, 21);
			this.comboColNewName.TabIndex = 17;
			// 
			// comboColRename
			// 
			this.comboColRename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboColRename.Location = new System.Drawing.Point(116, 18);
			this.comboColRename.MaxDropDownItems = 100;
			this.comboColRename.Name = "comboColRename";
			this.comboColRename.Size = new System.Drawing.Size(123, 21);
			this.comboColRename.TabIndex = 16;
			// 
			// butRename
			// 
			this.butRename.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butRename.Location = new System.Drawing.Point(166, 95);
			this.butRename.Name = "butRename";
			this.butRename.TabIndex = 9;
			this.butRename.Text = "Rename";
			this.butRename.Click += new System.EventHandler(this.butRename_Click);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(10, 45);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(100, 16);
			this.label13.TabIndex = 5;
			this.label13.Text = "New Name";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(10, 21);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100, 16);
			this.label14.TabIndex = 3;
			this.label14.Text = "Column";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.radioAfter);
			this.groupBox6.Controls.Add(this.radioFirst);
			this.groupBox6.Controls.Add(this.butMoveCol);
			this.groupBox6.Controls.Add(this.label15);
			this.groupBox6.Controls.Add(this.comboColMove);
			this.groupBox6.Controls.Add(this.comboColMoveAfter);
			this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox6.Location = new System.Drawing.Point(7, 6);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(219, 128);
			this.groupBox6.TabIndex = 12;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Move Column";
			// 
			// radioAfter
			// 
			this.radioAfter.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioAfter.Checked = true;
			this.radioAfter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioAfter.Location = new System.Drawing.Point(8, 61);
			this.radioAfter.Name = "radioAfter";
			this.radioAfter.Size = new System.Drawing.Size(72, 19);
			this.radioAfter.TabIndex = 11;
			this.radioAfter.TabStop = true;
			this.radioAfter.Text = "After";
			this.radioAfter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioAfter.Click += new System.EventHandler(this.radioAfter_Click);
			// 
			// radioFirst
			// 
			this.radioFirst.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioFirst.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioFirst.Location = new System.Drawing.Point(7, 42);
			this.radioFirst.Name = "radioFirst";
			this.radioFirst.Size = new System.Drawing.Size(73, 19);
			this.radioFirst.TabIndex = 10;
			this.radioFirst.Text = "First";
			this.radioFirst.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioFirst.Click += new System.EventHandler(this.radioFirst_Click);
			// 
			// butMoveCol
			// 
			this.butMoveCol.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butMoveCol.Location = new System.Drawing.Point(132, 95);
			this.butMoveCol.Name = "butMoveCol";
			this.butMoveCol.TabIndex = 9;
			this.butMoveCol.Text = "Move";
			this.butMoveCol.Click += new System.EventHandler(this.butMoveCol_Click);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(11, 21);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(68, 16);
			this.label15.TabIndex = 3;
			this.label15.Text = "Column";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboColMove
			// 
			this.comboColMove.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboColMove.Location = new System.Drawing.Point(85, 18);
			this.comboColMove.MaxDropDownItems = 100;
			this.comboColMove.Name = "comboColMove";
			this.comboColMove.Size = new System.Drawing.Size(123, 21);
			this.comboColMove.TabIndex = 14;
			// 
			// comboColMoveAfter
			// 
			this.comboColMoveAfter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboColMoveAfter.Location = new System.Drawing.Point(85, 59);
			this.comboColMoveAfter.MaxDropDownItems = 100;
			this.comboColMoveAfter.Name = "comboColMoveAfter";
			this.comboColMoveAfter.Size = new System.Drawing.Size(123, 21);
			this.comboColMoveAfter.TabIndex = 15;
			// 
			// butValidate
			// 
			this.butValidate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butValidate.Location = new System.Drawing.Point(10, 67);
			this.butValidate.Name = "butValidate";
			this.butValidate.TabIndex = 9;
			this.butValidate.Text = "Validate";
			this.butValidate.Click += new System.EventHandler(this.butValidate_Click);
			// 
			// butImport
			// 
			this.butImport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butImport.Location = new System.Drawing.Point(10, 99);
			this.butImport.Name = "butImport";
			this.butImport.TabIndex = 10;
			this.butImport.Text = "Import";
			this.butImport.Click += new System.EventHandler(this.butImport_Click);
			// 
			// tabContr
			// 
			this.tabContr.Controls.Add(this.tabLoadData);
			this.tabContr.Controls.Add(this.tabPK);
			this.tabContr.Controls.Add(this.tabQuery);
			this.tabContr.Controls.Add(this.tabColumns);
			this.tabContr.Controls.Add(this.tabRows);
			this.tabContr.Controls.Add(this.tabEdit);
			this.tabContr.Controls.Add(this.tabSpecialtyF);
			this.tabContr.Controls.Add(this.tabFinalImport);
			this.tabContr.Location = new System.Drawing.Point(0, 0);
			this.tabContr.Name = "tabContr";
			this.tabContr.SelectedIndex = 0;
			this.tabContr.Size = new System.Drawing.Size(871, 243);
			this.tabContr.TabIndex = 14;
			// 
			// tabLoadData
			// 
			this.tabLoadData.Controls.Add(this.comboTableName);
			this.tabLoadData.Controls.Add(this.butDeleteTable);
			this.tabLoadData.Controls.Add(this.label22);
			this.tabLoadData.Controls.Add(this.label21);
			this.tabLoadData.Controls.Add(this.butLoad);
			this.tabLoadData.Controls.Add(this.butBrowse);
			this.tabLoadData.Controls.Add(this.label20);
			this.tabLoadData.Controls.Add(this.textFileName);
			this.tabLoadData.Controls.Add(this.textNewTable);
			this.tabLoadData.Controls.Add(this.textBox1);
			this.tabLoadData.Controls.Add(this.label19);
			this.tabLoadData.Controls.Add(this.label18);
			this.tabLoadData.Controls.Add(this.butRefresh);
			this.tabLoadData.Controls.Add(this.label2);
			this.tabLoadData.Location = new System.Drawing.Point(4, 22);
			this.tabLoadData.Name = "tabLoadData";
			this.tabLoadData.Size = new System.Drawing.Size(863, 217);
			this.tabLoadData.TabIndex = 0;
			this.tabLoadData.Text = "Load Data";
			// 
			// comboTableName
			// 
			this.comboTableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTableName.Location = new System.Drawing.Point(112, 125);
			this.comboTableName.MaxDropDownItems = 18;
			this.comboTableName.Name = "comboTableName";
			this.comboTableName.Size = new System.Drawing.Size(122, 21);
			this.comboTableName.TabIndex = 18;
			this.comboTableName.SelectedIndexChanged += new System.EventHandler(this.comboTableName_SelectedIndexChanged);
			// 
			// butDeleteTable
			// 
			this.butDeleteTable.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDeleteTable.Location = new System.Drawing.Point(323, 123);
			this.butDeleteTable.Name = "butDeleteTable";
			this.butDeleteTable.TabIndex = 17;
			this.butDeleteTable.Text = "Delete";
			this.butDeleteTable.Click += new System.EventHandler(this.butDeleteTable_Click);
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label22.Location = new System.Drawing.Point(11, 101);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(631, 19);
			this.label22.TabIndex = 16;
			this.label22.Text = "Or, if you already loaded a file earlier, you can select an existing temp table t" +
				"o work with";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label21.Location = new System.Drawing.Point(8, 169);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(477, 19);
			this.label21.TabIndex = 15;
			this.label21.Text = "Then, check settings in the Primary Key tab";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butLoad
			// 
			this.butLoad.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butLoad.Location = new System.Drawing.Point(112, 72);
			this.butLoad.Name = "butLoad";
			this.butLoad.TabIndex = 14;
			this.butLoad.Text = "Load";
			this.butLoad.Click += new System.EventHandler(this.butLoad_Click);
			// 
			// butBrowse
			// 
			this.butBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butBrowse.Location = new System.Drawing.Point(772, 21);
			this.butBrowse.Name = "butBrowse";
			this.butBrowse.TabIndex = 13;
			this.butBrowse.Text = "Browse";
			this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(8, 51);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(100, 16);
			this.label20.TabIndex = 12;
			this.label20.Text = "File Name";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFileName
			// 
			this.textFileName.Location = new System.Drawing.Point(112, 49);
			this.textFileName.Name = "textFileName";
			this.textFileName.Size = new System.Drawing.Size(736, 20);
			this.textFileName.TabIndex = 11;
			this.textFileName.Text = "";
			// 
			// textNewTable
			// 
			this.textNewTable.Location = new System.Drawing.Point(156, 27);
			this.textNewTable.Name = "textNewTable";
			this.textNewTable.Size = new System.Drawing.Size(123, 20);
			this.textNewTable.TabIndex = 10;
			this.textNewTable.Text = "";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(112, 27);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(46, 20);
			this.textBox1.TabIndex = 9;
			this.textBox1.Text = "temp";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(9, 29);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(100, 16);
			this.label19.TabIndex = 8;
			this.label19.Text = "New Table Name";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label18.Location = new System.Drawing.Point(9, 5);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(477, 19);
			this.label18.TabIndex = 7;
			this.label18.Text = "Load data from a text file into a temp table";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabPK
			// 
			this.tabPK.Controls.Add(this.checkDontUsePK);
			this.tabPK.Controls.Add(this.label25);
			this.tabPK.Controls.Add(this.groupBox7);
			this.tabPK.Controls.Add(this.butClearPK);
			this.tabPK.Controls.Add(this.comboColPK);
			this.tabPK.Controls.Add(this.label1);
			this.tabPK.Controls.Add(this.butSetPK);
			this.tabPK.Location = new System.Drawing.Point(4, 22);
			this.tabPK.Name = "tabPK";
			this.tabPK.Size = new System.Drawing.Size(863, 217);
			this.tabPK.TabIndex = 6;
			this.tabPK.Text = "Primary Key";
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(114, 125);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(599, 19);
			this.label25.TabIndex = 30;
			this.label25.Text = "If you set an empty column as the primary key, it will automatically be filled wi" +
				"th numbers";
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.radioPatients);
			this.groupBox7.Controls.Add(this.radioCarriers);
			this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox7.Location = new System.Drawing.Point(15, 11);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(200, 64);
			this.groupBox7.TabIndex = 29;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Table Type";
			// 
			// radioPatients
			// 
			this.radioPatients.Checked = true;
			this.radioPatients.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioPatients.Location = new System.Drawing.Point(16, 20);
			this.radioPatients.Name = "radioPatients";
			this.radioPatients.Size = new System.Drawing.Size(154, 18);
			this.radioPatients.TabIndex = 19;
			this.radioPatients.TabStop = true;
			this.radioPatients.Text = "Patients";
			this.radioPatients.Click += new System.EventHandler(this.radioPatients_Click);
			// 
			// radioCarriers
			// 
			this.radioCarriers.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioCarriers.Location = new System.Drawing.Point(16, 37);
			this.radioCarriers.Name = "radioCarriers";
			this.radioCarriers.Size = new System.Drawing.Size(154, 18);
			this.radioCarriers.TabIndex = 20;
			this.radioCarriers.Text = "Carriers";
			this.radioCarriers.Click += new System.EventHandler(this.radioCarriers_Click);
			// 
			// butClearPK
			// 
			this.butClearPK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butClearPK.Location = new System.Drawing.Point(319, 96);
			this.butClearPK.Name = "butClearPK";
			this.butClearPK.Size = new System.Drawing.Size(69, 23);
			this.butClearPK.TabIndex = 28;
			this.butClearPK.Text = "Clear";
			this.butClearPK.Click += new System.EventHandler(this.butClearPK_Click);
			// 
			// comboColPK
			// 
			this.comboColPK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboColPK.Location = new System.Drawing.Point(116, 97);
			this.comboColPK.MaxDropDownItems = 100;
			this.comboColPK.Name = "comboColPK";
			this.comboColPK.Size = new System.Drawing.Size(123, 21);
			this.comboColPK.TabIndex = 27;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 100);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 25;
			this.label1.Text = "Primary Key";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butSetPK
			// 
			this.butSetPK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSetPK.Location = new System.Drawing.Point(242, 96);
			this.butSetPK.Name = "butSetPK";
			this.butSetPK.Size = new System.Drawing.Size(70, 23);
			this.butSetPK.TabIndex = 26;
			this.butSetPK.Text = "Set";
			this.butSetPK.Click += new System.EventHandler(this.butSetPK_Click);
			// 
			// tabColumns
			// 
			this.tabColumns.Controls.Add(this.groupBox8);
			this.tabColumns.Controls.Add(this.groupBox2);
			this.tabColumns.Controls.Add(this.groupBox6);
			this.tabColumns.Controls.Add(this.groupBox4);
			this.tabColumns.Controls.Add(this.groupBox5);
			this.tabColumns.Location = new System.Drawing.Point(4, 22);
			this.tabColumns.Name = "tabColumns";
			this.tabColumns.Size = new System.Drawing.Size(863, 217);
			this.tabColumns.TabIndex = 1;
			this.tabColumns.Text = "Columns";
			// 
			// groupBox8
			// 
			this.groupBox8.Controls.Add(this.comboColAdd);
			this.groupBox8.Controls.Add(this.butAddCol);
			this.groupBox8.Controls.Add(this.label24);
			this.groupBox8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox8.Location = new System.Drawing.Point(280, 136);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(250, 77);
			this.groupBox8.TabIndex = 14;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Add Column";
			// 
			// comboColAdd
			// 
			this.comboColAdd.Location = new System.Drawing.Point(119, 18);
			this.comboColAdd.MaxDropDownItems = 100;
			this.comboColAdd.Name = "comboColAdd";
			this.comboColAdd.Size = new System.Drawing.Size(123, 21);
			this.comboColAdd.TabIndex = 18;
			// 
			// butAddCol
			// 
			this.butAddCol.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butAddCol.Location = new System.Drawing.Point(166, 47);
			this.butAddCol.Name = "butAddCol";
			this.butAddCol.TabIndex = 9;
			this.butAddCol.Text = "Add";
			this.butAddCol.Click += new System.EventHandler(this.butAddCol_Click);
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(10, 21);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(100, 16);
			this.label24.TabIndex = 3;
			this.label24.Text = "Name";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.comboColDelete);
			this.groupBox2.Controls.Add(this.butDeleteCol);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(7, 136);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(250, 77);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Delete Column";
			// 
			// comboColDelete
			// 
			this.comboColDelete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboColDelete.Location = new System.Drawing.Point(118, 18);
			this.comboColDelete.MaxDropDownItems = 100;
			this.comboColDelete.Name = "comboColDelete";
			this.comboColDelete.Size = new System.Drawing.Size(123, 21);
			this.comboColDelete.TabIndex = 16;
			// 
			// butDeleteCol
			// 
			this.butDeleteCol.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDeleteCol.Location = new System.Drawing.Point(166, 47);
			this.butDeleteCol.Name = "butDeleteCol";
			this.butDeleteCol.TabIndex = 9;
			this.butDeleteCol.Text = "Delete";
			this.butDeleteCol.Click += new System.EventHandler(this.butDeleteCol_Click);
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(10, 21);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(100, 16);
			this.label16.TabIndex = 3;
			this.label16.Text = "Column";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabRows
			// 
			this.tabRows.Controls.Add(this.label23);
			this.tabRows.Controls.Add(this.textRows);
			this.tabRows.Controls.Add(this.label12);
			this.tabRows.Controls.Add(this.butDeleteRows);
			this.tabRows.Location = new System.Drawing.Point(4, 22);
			this.tabRows.Name = "tabRows";
			this.tabRows.Size = new System.Drawing.Size(863, 217);
			this.tabRows.TabIndex = 5;
			this.tabRows.Text = "Rows";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(134, 15);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(377, 35);
			this.label23.TabIndex = 14;
			this.label23.Text = "Warning!  There is a bug in the delete feature.  Do not delete a row if you have " +
				"reordered any column by clicking on a column header.";
			// 
			// textRows
			// 
			this.textRows.Location = new System.Drawing.Point(119, 80);
			this.textRows.Name = "textRows";
			this.textRows.ReadOnly = true;
			this.textRows.Size = new System.Drawing.Size(70, 20);
			this.textRows.TabIndex = 13;
			this.textRows.Text = "";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(13, 80);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 19);
			this.label12.TabIndex = 12;
			this.label12.Text = "Number of Rows";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butDeleteRows
			// 
			this.butDeleteRows.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butDeleteRows.Location = new System.Drawing.Point(21, 18);
			this.butDeleteRows.Name = "butDeleteRows";
			this.butDeleteRows.Size = new System.Drawing.Size(93, 23);
			this.butDeleteRows.TabIndex = 11;
			this.butDeleteRows.Text = "Delete Row(s)";
			this.butDeleteRows.Click += new System.EventHandler(this.butDeleteRows_Click);
			// 
			// tabEdit
			// 
			this.tabEdit.Controls.Add(this.groupBox9);
			this.tabEdit.Controls.Add(this.groupBox1);
			this.tabEdit.Location = new System.Drawing.Point(4, 22);
			this.tabEdit.Name = "tabEdit";
			this.tabEdit.Size = new System.Drawing.Size(863, 217);
			this.tabEdit.TabIndex = 2;
			this.tabEdit.Text = "Edit";
			// 
			// groupBox9
			// 
			this.groupBox9.Controls.Add(this.comboColClear);
			this.groupBox9.Controls.Add(this.butColClear);
			this.groupBox9.Controls.Add(this.label26);
			this.groupBox9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox9.Location = new System.Drawing.Point(269, 7);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(250, 77);
			this.groupBox9.TabIndex = 16;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Clear all values";
			// 
			// comboColClear
			// 
			this.comboColClear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboColClear.Location = new System.Drawing.Point(119, 18);
			this.comboColClear.MaxDropDownItems = 100;
			this.comboColClear.Name = "comboColClear";
			this.comboColClear.Size = new System.Drawing.Size(123, 21);
			this.comboColClear.TabIndex = 18;
			// 
			// butColClear
			// 
			this.butColClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butColClear.Location = new System.Drawing.Point(166, 47);
			this.butColClear.Name = "butColClear";
			this.butColClear.TabIndex = 9;
			this.butColClear.Text = "Clear";
			this.butColClear.Click += new System.EventHandler(this.butColClear_Click);
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(10, 21);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(100, 16);
			this.label26.TabIndex = 3;
			this.label26.Text = "Column Name";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabSpecialtyF
			// 
			this.tabSpecialtyF.Controls.Add(this.groupBox3);
			this.tabSpecialtyF.Location = new System.Drawing.Point(4, 22);
			this.tabSpecialtyF.Name = "tabSpecialtyF";
			this.tabSpecialtyF.Size = new System.Drawing.Size(863, 217);
			this.tabSpecialtyF.TabIndex = 3;
			this.tabSpecialtyF.Text = "Specialty Functions";
			// 
			// tabQuery
			// 
			this.tabQuery.Controls.Add(this.label28);
			this.tabQuery.Controls.Add(this.butQuerySubmit);
			this.tabQuery.Controls.Add(this.textQuery);
			this.tabQuery.Location = new System.Drawing.Point(4, 22);
			this.tabQuery.Name = "tabQuery";
			this.tabQuery.Size = new System.Drawing.Size(863, 217);
			this.tabQuery.TabIndex = 7;
			this.tabQuery.Text = "Query";
			// 
			// butQuerySubmit
			// 
			this.butQuerySubmit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butQuerySubmit.Location = new System.Drawing.Point(545, 169);
			this.butQuerySubmit.Name = "butQuerySubmit";
			this.butQuerySubmit.TabIndex = 10;
			this.butQuerySubmit.Text = "Submit";
			this.butQuerySubmit.Click += new System.EventHandler(this.butQuerySubmit_Click);
			// 
			// textQuery
			// 
			this.textQuery.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textQuery.Location = new System.Drawing.Point(1, 2);
			this.textQuery.Multiline = true;
			this.textQuery.Name = "textQuery";
			this.textQuery.Size = new System.Drawing.Size(537, 190);
			this.textQuery.TabIndex = 0;
			this.textQuery.Text = "";
			// 
			// tabFinalImport
			// 
			this.tabFinalImport.Controls.Add(this.radioUpdate);
			this.tabFinalImport.Controls.Add(this.radioInsert);
			this.tabFinalImport.Controls.Add(this.butImport);
			this.tabFinalImport.Controls.Add(this.butValidate);
			this.tabFinalImport.Location = new System.Drawing.Point(4, 22);
			this.tabFinalImport.Name = "tabFinalImport";
			this.tabFinalImport.Size = new System.Drawing.Size(863, 217);
			this.tabFinalImport.TabIndex = 4;
			this.tabFinalImport.Text = "Final Import";
			// 
			// radioUpdate
			// 
			this.radioUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioUpdate.Location = new System.Drawing.Point(9, 27);
			this.radioUpdate.Name = "radioUpdate";
			this.radioUpdate.Size = new System.Drawing.Size(290, 18);
			this.radioUpdate.TabIndex = 12;
			this.radioUpdate.Text = "Update some columns for existing rows";
			// 
			// radioInsert
			// 
			this.radioInsert.Checked = true;
			this.radioInsert.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioInsert.Location = new System.Drawing.Point(9, 7);
			this.radioInsert.Name = "radioInsert";
			this.radioInsert.Size = new System.Drawing.Size(266, 18);
			this.radioInsert.TabIndex = 11;
			this.radioInsert.TabStop = true;
			this.radioInsert.Text = "Insert new rows";
			// 
			// checkDontUsePK
			// 
			this.checkDontUsePK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkDontUsePK.Location = new System.Drawing.Point(234, 14);
			this.checkDontUsePK.Name = "checkDontUsePK";
			this.checkDontUsePK.Size = new System.Drawing.Size(375, 19);
			this.checkDontUsePK.TabIndex = 31;
			this.checkDontUsePK.Text = "Do not use the primary key values in the final import";
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(545, 8);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(182, 30);
			this.label28.TabIndex = 11;
			this.label28.Text = "Should be a command, not a query to retrieve a table";
			// 
			// FormImport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1192, 848);
			this.Controls.Add(this.tabContr);
			this.Controls.Add(this.grid);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormImport";
			this.ShowInTaskbar = false;
			this.Text = "Import";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormImport_Load);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.tabContr.ResumeLayout(false);
			this.tabLoadData.ResumeLayout(false);
			this.tabPK.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.tabColumns.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tabRows.ResumeLayout(false);
			this.tabEdit.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			this.tabSpecialtyF.ResumeLayout(false);
			this.tabQuery.ResumeLayout(false);
			this.tabFinalImport.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormImport_Load(object sender, System.EventArgs e) {
			grid.Width=ClientSize.Width-3;
			grid.Height=ClientSize.Height-grid.Top-2;
			tabContr.Width=ClientSize.Width-3;
			FillTableNames();
			if(comboTableName.Items.Count>0){
				comboTableName.SelectedIndex=0;//this triggers a fillgrid
				//FillGrid();
				GetPK();
			}
			GetAllowedCols();
			MsgBox.Show(this,"Warning.  This feature is only intended for advanced users doing data conversions from other software.  It is not yet easy enough for beginners to use.");
		}

		///<summary>Depending on whether this is patients, carriers, or..., this sets a few of the comboboxes and also enables proper validation when doing the final export.</summary>
		private void GetAllowedCols(){
			if(radioPatients.Checked){
				AllowedColNames=new string[]
				{
					"Address",
					"Address2",
					"AddrNote",
					"Balance",
					"Birthdate",
					"City",
					"DateFirstVisit",
					"FName",
					"Gender",
					"Guarantor",
					"HmPhone",
					"LName",
					"MiddleI",
					"PatNum",
					"Position",
					"Preferred",
					"SSN",
					"State",
					"WkPhone",
					"Zip"
				};
			}
			else if(radioCarriers.Checked){
				AllowedColNames=new string[]
				{
					"Address",
					"Address2",
					"CarrierNum",
					"CarrierName",
					"City",
					"Phone",
					"State",
					"Zip"
				};
			}
			for(int i=0;i<AllowedColNames.Length;i++){
				comboColNewName.Items.Add(AllowedColNames[i]);
				comboColAdd.Items.Add(AllowedColNames[i]);
			}
		}
		
		private void butBrowse_Click(object sender, System.EventArgs e) {
			OpenFileDialog dlg=new OpenFileDialog();
			//dlg.InitialDirectory=textRawPath.Text;
			if(dlg.ShowDialog()==DialogResult.Cancel){
				return;
			}
			textFileName.Text=dlg.FileName;
		}

		private void butLoad_Click(object sender, System.EventArgs e) {
			if(textNewTable.Text=="" || textFileName.Text==""){
				MsgBox.Show(this,"Please enter a table name and a file name");
				return;
			}
			if(!File.Exists(textFileName.Text)){
				MsgBox.Show(this,"File does not exist.");
				return;
			}
			string newTable="temp"+textNewTable.Text;
			if(!Regex.IsMatch(newTable,@"^[a-z]+$")){
				MsgBox.Show(this,"Table name must be all lowercase letters.");
				return;
			}
			//make sure table doesn't already exist
			string command="SHOW TABLES";
			DataConnection dcon=new DataConnection();
			DataTable tempT=dcon.GetTable(command);
			for(int i=0;i<tempT.Rows.Count;i++){
				if(tempT.Rows[i][0].ToString()==newTable){
					MsgBox.Show(this,"Table already exists.");
					return;
				}
			}
			//get a list of the new column name
			//missing feature: check for duplicate column names
			string line="";
			try{
				using(StreamReader sr=new StreamReader(textFileName.Text)){
					line=sr.ReadLine(); 
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return;
			}
			line=line.Replace("\"","");
			string[] colNames=line.Split(new char[] {'\t'});
			command="CREATE TABLE "+newTable+"(";
			for(int i=0;i<colNames.Length;i++){
				colNames[i]=Regex.Replace(colNames[i],"[^a-zA-Z0-9]","");//gets rid of any character that's not a letter or number.
				command+=colNames[i]+" text NOT NULL";
				if(i<colNames.Length-1){
					command+=",";
				}
			}
			command+=")";
			dcon.NonQ(command);
			command="LOAD DATA INFILE '"+POut.PString(textFileName.Text)+"' INTO TABLE "+newTable
				+@" FIELDS TERMINATED BY '\t' ESCAPED BY '\\' LINES TERMINATED BY '\r\n'";
			dcon.NonQ(command);
			FillTableNames();
			comboTableName.SelectedItem=newTable;
			FillGrid();
			//MessageBox.Show("done");
		}

		private void FillTableNames(){
			string command="SHOW TABLES";
			DataConnection dcon=new DataConnection();
			DataTable tempT=dcon.GetTable(command);
			comboTableName.Items.Clear();
			for(int i=0;i<tempT.Rows.Count;i++){
				if(tempT.Rows[i][0].ToString().Length>4 && tempT.Rows[i][0].ToString().Substring(0,4)=="temp"){
					comboTableName.Items.Add(tempT.Rows[i][0].ToString());
				}
			}
		}

		private void FillGrid(){
			string command="SELECT * FROM "+POut.PString(comboTableName.SelectedItem.ToString());
			DataConnection dcon=new DataConnection();
 			table=dcon.GetTable(command);
			comboColSubst.Items.Clear();
			comboColMove.Items.Clear();
			comboColMoveAfter.Items.Clear();
			comboColCombine1.Items.Clear();
			comboColCombine2.Items.Clear();
			comboColRename.Items.Clear();
			comboColDelete.Items.Clear();
			comboColDateSource.Items.Clear();
			comboColDateDest.Items.Clear();
			comboColPK.Items.Clear();
			comboColClear.Items.Clear();
			for(int i=0;i<table.Columns.Count;i++){
				comboColSubst.Items.Add(table.Columns[i].ColumnName);
				comboColMove.Items.Add(table.Columns[i].ColumnName);
				comboColMoveAfter.Items.Add(table.Columns[i].ColumnName);
				comboColCombine1.Items.Add(table.Columns[i].ColumnName);
				comboColCombine2.Items.Add(table.Columns[i].ColumnName);
				comboColRename.Items.Add(table.Columns[i].ColumnName);
				comboColDelete.Items.Add(table.Columns[i].ColumnName);
				comboColDateSource.Items.Add(table.Columns[i].ColumnName);
				comboColDateDest.Items.Add(table.Columns[i].ColumnName);
				comboColPK.Items.Add(table.Columns[i].ColumnName);
				comboColClear.Items.Add(table.Columns[i].ColumnName);
			}
			grid.SetDataBinding(table,"");
			textRows.Text=table.Rows.Count.ToString();
		}

		private void GetPK(){
			string command="SHOW INDEX FROM "+POut.PString(comboTableName.SelectedItem.ToString());
			DataConnection dcon=new DataConnection();
			DataTable tempT=dcon.GetTable(command);
			if(tempT.Rows.Count==0){
				comboColPK.SelectedIndex=-1;
				pkCol="";
			}
			else{
				comboColPK.SelectedItem=tempT.Rows[0][4].ToString();
				pkCol=tempT.Rows[0][4].ToString();
			}
		}

		private void radioPatients_Click(object sender, System.EventArgs e) {
			GetAllowedCols();
		}

		private void radioCarriers_Click(object sender, System.EventArgs e) {
			GetAllowedCols();
		}
		
		private void butSetPK_Click(object sender, System.EventArgs e) {
			if(comboColPK.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column name first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string command="SHOW INDEX FROM "+POut.PString(comboTableName.SelectedItem.ToString());
			DataConnection dcon=new DataConnection();
			DataTable tempT=dcon.GetTable(command);
			if(tempT.Rows.Count!=0){//if a primary key exists
				command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" DROP PRIMARY KEY";
				dcon.NonQ(command);
			}
			pkCol=comboColPK.SelectedItem.ToString();
			//first, test to see if it's a blank column
			command="SELECT COUNT(*) FROM "+comboTableName.SelectedItem.ToString()+" WHERE "+pkCol+"!=''";
			tempT=dcon.GetTable(command);
			if(tempT.Rows[0][0].ToString()=="0"){//all blank
				command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" CHANGE "+pkCol+" "+pkCol
					+" int unsigned NOT NULL auto_increment, "
					+" ADD PRIMARY KEY ("+pkCol+")";
				dcon.NonQ(command);
				command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" DROP PRIMARY KEY,"
				//dcon.NonQ(command);
				//command="ALTER TABLE "+comboTableName.SelectedItem.ToString()
					+" CHANGE "+pkCol+" "+pkCol+" text NOT NULL,"
					+" ADD PRIMARY KEY ("+pkCol+"(10))";
				dcon.NonQ(command);
			}
			else{//primary keys already exist.  An error will show if duplicates
				command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" ADD PRIMARY KEY ("+pkCol+"(10))";
				dcon.NonQ(command);
			}
			FillGrid();
			GetPK();
			Cursor=Cursors.Default;
		}

		private void butClearPK_Click(object sender, System.EventArgs e) {
			string command="SHOW INDEX FROM "+POut.PString(comboTableName.SelectedItem.ToString());
			DataConnection dcon=new DataConnection();
			DataTable tempT=dcon.GetTable(command);
			if(tempT.Rows.Count==0){
				MsgBox.Show(this,"No primary key to clear.");
				GetPK();
			}
			else{//if a primary key exists
				command="ALTER TABLE "+POut.PString(comboTableName.SelectedItem.ToString())+" DROP PRIMARY KEY";
				dcon.NonQ(command);
				GetPK();
				MessageBox.Show("done");
			}
		}

		private void comboTableName_SelectedIndexChanged(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void butRefresh_Click(object sender, System.EventArgs e) {
			FillGrid();
			GetPK();
		}

		private void butDeleteTable_Click(object sender, System.EventArgs e) {
			if(comboTableName.SelectedIndex==-1){
				MsgBox.Show(this,"No table to delete");
				return;
			}
			if(!MsgBox.Show(this,true,"Delete the temp table?")){
				return;
			}
			string command="DROP TABLE "+comboTableName.SelectedItem;
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			FillTableNames();
			if(comboTableName.Items.Count>0){
				comboTableName.SelectedIndex=0;
			}
		}

		private void butSubstitute_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MsgBox.Show(this,"Please set a primary key first.");
				return;
			}
			if(comboColSubst.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colName=comboColSubst.SelectedItem.ToString();
			int count=0;
			string newVal;
			string command;
			DataConnection dcon=new DataConnection();
			for(int i=0;i<table.Rows.Count;i++){
				newVal=table.Rows[i][colName].ToString().Replace(textSubstOld.Text,textSubstNew.Text);
				if(newVal!=table.Rows[i][colName].ToString()){
					count++;
					command="UPDATE "+comboTableName.SelectedItem.ToString()+" SET "+colName+"='"+POut.PString(newVal)+"' "
						+"WHERE "+pkCol+"='"+POut.PString(table.Rows[i][pkCol].ToString())+"'";
					dcon.NonQ(command);
				}
			}
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show(count.ToString()+" "+Lan.g(this,"rows changed"));
		}

		private void butColClear_Click(object sender, System.EventArgs e) {
			if(comboColClear.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colName=comboColClear.SelectedItem.ToString();
			string command="UPDATE "+comboTableName.SelectedItem.ToString()+" SET "+colName+"=''";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butFixDates_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MsgBox.Show(this,"Please set a primary key first.");
				return;
			}
			if(comboColDateSource.SelectedIndex==-1 || comboColDateDest.SelectedIndex==-1){
				MsgBox.Show(this,"Please select columns first.");
				return;
			}
			if(textDateOldFormats.Text=="" || textDateNewFormat.Text==""){
				MsgBox.Show(this,"Please enter date formats first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colSource=comboColDateSource.SelectedItem.ToString();
			string colDest=comboColDateDest.SelectedItem.ToString();
			string newVal;
			DateTime date;
			string command;
			string[] formats=textDateOldFormats.Text.Split(new char[] {','});
			DataConnection dcon=new DataConnection();
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i][colSource].ToString()==""){
					continue;
				}
				try{
					date=DateTime.ParseExact(table.Rows[i][colSource].ToString(),formats,Application.CurrentCulture,DateTimeStyles.AllowWhiteSpaces);
				}
				catch{
					continue;
				}
				newVal=date.ToString(textDateNewFormat.Text);
				command="UPDATE "+POut.PString(comboTableName.SelectedItem.ToString())+" SET "+colDest+"='"+POut.PString(newVal)+"' "
					+"WHERE "+POut.PString(pkCol)+"='"+table.Rows[i][pkCol].ToString()+"'";
				dcon.NonQ(command);
			}
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		private void butDeleteRow_Click(object sender, System.EventArgs e) {
			MessageBox.Show("Not functional yet.");
			return;
		}

		private void butCombine_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MessageBox.Show("Please set a primary key first.");
				return;
			}
			if(comboColCombine1.SelectedIndex==-1 || comboColCombine1.SelectedIndex==-1){
				MsgBox.Show(this,"Please select columns first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string col1=comboColCombine1.SelectedItem.ToString();
			string col2=comboColCombine2.SelectedItem.ToString();
			string sep=textSepChar.Text;
			string newVal;
			string command;
			DataConnection dcon=new DataConnection();
			for(int i=0;i<table.Rows.Count;i++){
				newVal=table.Rows[i][col1].ToString()+sep+table.Rows[i][col2].ToString();
				command="UPDATE "+POut.PString(comboTableName.SelectedItem.ToString())+" SET "+col1+"='"+POut.PString(newVal)+"' "
					+"WHERE "+POut.PString(pkCol)+"='"+table.Rows[i][pkCol].ToString()+"'";
				dcon.NonQ(command);
			}
      command="ALTER TABLE "+POut.PString(comboTableName.SelectedItem.ToString())+" DROP "+col2;
			dcon.NonQ(command);
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		private void butRename_Click(object sender, System.EventArgs e) {
			if(comboColRename.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			if(comboColNewName.Text==""){
				MsgBox.Show(this,"Please enter a new name.");
				return;
			}
			//note that user has a list of acceptable col names to choose from, but they may also type in their own name.
			string newName=comboColNewName.Text;
			if(!Regex.IsMatch(newName,@"^[a-zA-Z0-9]+$")){
				MsgBox.Show(this,"New name must be only upper and lowercase letters and numbers with no other symbols.");
				return;
			}
			for(int i=0;i<comboColRename.Items.Count;i++){//go through the list of existing col names
				if(newName==comboColRename.Items[i].ToString()){
					MsgBox.Show(this,"The new column name duplicates an existing column name.");
					return;
				}
			}
			Cursor=Cursors.WaitCursor;
			string colName=comboColRename.SelectedItem.ToString();
			string command="ALTER TABLE "+POut.PString(comboTableName.SelectedItem.ToString())+" CHANGE "+colName
				+" "+newName+" text NOT NULL";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			FillGrid();
			GetPK();//in case the primary key column was renamed
			Cursor=Cursors.Default;
			//MessageBox.Show("done");
		}

		private void butMoveCol_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MessageBox.Show("Please set a primary key first.");
				return;
			}
			if(comboColMove.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			if(radioAfter.Checked && comboColMoveAfter.SelectedIndex==-1){
				MsgBox.Show(this,"A column must be selected to move after.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colName=comboColMove.SelectedItem.ToString();
			string colAfter="";
			if(radioAfter.Checked){
				colAfter=comboColMoveAfter.SelectedItem.ToString();
			}
			string command="ALTER TABLE "+POut.PString(comboTableName.SelectedItem.ToString())+" MODIFY "+colName+" text NOT NULL ";
			if(radioFirst.Checked){
				command+="FIRST";
			}
			else{
				command+="AFTER "+colAfter;
			}
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void radioFirst_Click(object sender, System.EventArgs e) {
			comboColMoveAfter.Enabled=false;
			comboColMoveAfter.SelectedIndex=-1;
		}

		private void radioAfter_Click(object sender, System.EventArgs e) {
			comboColMoveAfter.Enabled=true;
		}

		private void butDeleteCol_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MessageBox.Show("Please set a primary key first.");
				return;
			}
			if(comboColDelete.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string col=comboColDelete.SelectedItem.ToString();
			string command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" DROP "+col;
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			FillGrid();
			Cursor=Cursors.Default;
			//MessageBox.Show("done");
		}

		private void butAddCol_Click(object sender, System.EventArgs e) {
			if(comboColAdd.Text==""){
				MsgBox.Show(this,"Please enter a name first.");
				return;
			}
			//note that user has a list of acceptable col names to choose from, but they may also type in their own name.
			string newName=comboColAdd.Text;
			if(!Regex.IsMatch(newName,@"^[a-zA-Z0-9]+$")){
				MsgBox.Show(this,"Name must be only upper and lowercase letters and numbers with no other symbols.");
				return;
			}
			for(int i=0;i<comboColAdd.Items.Count;i++){//go through the list of existing col names
				if(newName==comboColAdd.Items[i].ToString()){
					MsgBox.Show(this,"The new column name duplicates an existing column name.");
					return;
				}
			}
			Cursor=Cursors.WaitCursor;
			string command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" ADD "+newName+" text NOT NULL";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butDeleteRows_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MsgBox.Show(this,"Please set a primary key first.");
				return;
			}
			ArrayList al=new ArrayList();			
			for(int i=0;i<table.Rows.Count;i++){
				if(grid.IsSelected(i)){
					//MessageBox.Show(((DataTable)grid.DataSource).Rows[i][pkCol].ToString());//table.Rows[i][pkCol].ToString());
					//return;
					al.Add(table.Rows[i][pkCol].ToString());
				}
			}
			if(al.Count==0){
				MsgBox.Show(this,"Please select at least one row first.");
				return;
			}
			string[] selectedRowPKs=new string[al.Count];
			al.CopyTo(selectedRowPKs);
			string command="DELETE FROM "+comboTableName.SelectedItem.ToString()+" WHERE";
			for(int i=0;i<selectedRowPKs.Length;i++){
				if(i>0){
					command+=" OR";
				}
				command+=" "+pkCol+"='"+POut.PString(selectedRowPKs[i])+"'";
			}
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			FillGrid();
		}

		private void butQuerySubmit_Click(object sender, System.EventArgs e) {
			if(textQuery.Text==""){
				MsgBox.Show(this,"Please enter a query first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string command=textQuery.Text;
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		private void butValidate_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try{
				ValidateData();
			}
			catch(Exception ex){
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			Cursor=Cursors.Default;
			MessageBox.Show("valid");
		}

		private void butImport_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try{
				ValidateData();
			}
			catch(Exception ex){
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			int prov=Prefs.GetInt("PracticeDefaultProv");
			int billType=Prefs.GetInt("PracticeDefaultBillType");
			Patient pat;
			Adjustment adj;
			int adjType=189;
			int balanceCol=ColumnIndex("Balance");
			//Need to get the adjustment type for Balance entries
			Patient patOld=new Patient();
			for(int i=0;i<table.Rows.Count;i++){
				pat=new Patient();
				Carriers.Cur=new Carrier();
				if(radioInsert.Checked){
					pat.PriProv=prov;
					pat.BillingType=billType;
				}
				for(int j=0;j<table.Columns.Count;j++){
					if(radioPatients.Checked){
						switch(table.Columns[j].ColumnName){
							case "Address":
								pat.Address=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "Address2":
								pat.Address2=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "AddrNote":
								pat.AddrNote=PIn.PString(table.Rows[i][j].ToString());
								break;
							//Balance
							case "Birthdate":
								pat.Birthdate=PIn.PDate(table.Rows[i][j].ToString());//handles "" ok
								break;
							case "City":
								pat.City=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "DateFirstVisit":
								pat.DateFirstVisit=PIn.PDate(table.Rows[i][j].ToString());
								break;
							case "FName":
								pat.FName=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "Gender":
								if(table.Rows[i][j].ToString()=="M")
									pat.Gender=PatientGender.Male;
								else//F
									pat.Gender=PatientGender.Female;
								break;
							case "Guarantor":
								pat.Guarantor=PIn.PInt(table.Rows[i][j].ToString());
								break;
							case "HmPhone":
								pat.HmPhone=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "LName":
								pat.LName=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "MiddleI":
								pat.MiddleI=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "PatNum":
								pat.PatNum=PIn.PInt(table.Rows[i][j].ToString());
								break;
							case "Position":
								if(table.Rows[i][j].ToString()=="M")
									pat.Position=PatientPosition.Married;
								else//S
									pat.Position=PatientPosition.Single;
								break;
							case "Preferred":
								pat.Preferred=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "SSN":
								pat.SSN=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "State":
								pat.State=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "WkPhone":
								pat.WkPhone=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "Zip":
								pat.Zip=PIn.PString(table.Rows[i][j].ToString());
								break;
						}
					}
					else if(radioCarriers.Checked){
						switch(table.Columns[j].ColumnName){
							case "Address":
								Carriers.Cur.Address=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "Address2":
								Carriers.Cur.Address2=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "CarrierName":
								Carriers.Cur.CarrierName=PIn.PString(table.Rows[i][j].ToString());
								break;
							//case CarrierNum
							case "City":
								Carriers.Cur.City=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "State":
								Carriers.Cur.State=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "Zip":
								Carriers.Cur.Zip=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "Phone":
								Carriers.Cur.Phone=PIn.PString(table.Rows[i][j].ToString());
								break;
						}
					}
				}//columns					
				if(radioPatients.Checked){
					if(radioInsert.Checked){
						if(checkDontUsePK.Checked){
							pat.Insert(false);
						}
						else{
							pat.Insert(true);
						}
					}
					else{
						pat.Update(patOld);
					}
				}
				else if(radioCarriers.Checked){
					if(radioInsert.Checked){
						Carriers.InsertCur();
					}
					else{
						//pat.Update(patOld);
					}
				}
				//if the balance column exists. Needs to come AFTER initial insert in case we don't have primary key to work with
				if(radioPatients.Checked && balanceCol!=-1){
					adj=new Adjustment();
					adj.PatNum=PIn.PInt(table.Rows[i][pkCol].ToString());
					adj.AdjAmt=PIn.PDouble(table.Rows[i][balanceCol].ToString());
					adj.AdjType=adjType;
					adj.AdjDate=DateTime.Today;
					adj.ProcDate=DateTime.Today;
					adj.ProvNum=prov;
					adj.Insert();
				}
			}//rows
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		///<summary>Used in various places to search through the column names for a specific match.  Returns either the index of the column, or -1 if not found.</summary>
		private int ColumnIndex(string colName){
			for(int i=0;i<table.Columns.Count;i++){
				if(table.Columns[i].ColumnName==colName){
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
		private void ValidateData(){
			if(checkDontUsePK.Checked){//For now, this only makes sense if just doing inserts.  Later, pk could be multiple fields.
				if(radioUpdate.Checked){
					throw new Exception(Lan.g(this,"Primary key must be used if doing updates."));
				}
			}
			else{//pk's will be used
				if(radioPatients.Checked && pkCol!="PatNum"){
					throw new Exception(Lan.g(this,"PatNum must be set as primary key first."));
				}
				else if(radioCarriers.Checked && pkCol!="CarrierNum"){
					throw new Exception(Lan.g(this,"CarrierNum must be set as primary key first."));
				}
			}
			string colNamesAll="";
			for(int i=0;i<AllowedColNames.Length;i++){
				if(i>0){
					colNamesAll+=", ";
				}
				colNamesAll+=AllowedColNames[i];
			}
			bool valid;
			for(int i=0;i<table.Columns.Count;i++){
				valid=false;
				for(int j=0;j<AllowedColNames.Length;j++){
					if(AllowedColNames[j]==table.Columns[i].ColumnName){
						valid=true;
						break;
					}
				}
				if(!valid){
					throw new Exception(table.Columns[i].ColumnName+Lan.g(this," is not a valid column name.  Acceptable column names are: ")
						+colNamesAll);
				}
			}
			for(int i=0;i<table.Columns.Count;i++){
				for(int r=0;r<table.Rows.Count;r++){	
					switch(table.Columns[i].ColumnName){
						case "PatNum":
						case "Guarantor":
							if(!Regex.IsMatch(table.Rows[r][i].ToString(),@"^\d+$")){
								throw new Exception(Lan.g(this,"Invalid value in column")+" "+table.Columns[i].ColumnName+": "
									+table.Rows[r][i].ToString()+". "+Lan.g(this,"Must contain only digits."));
							}
							break;
						case "Gender":
							if(table.Rows[r][i].ToString()!="M"
								&& table.Rows[r][i].ToString()!="F")
							{
								throw new Exception("Column Gender, invalid value: "+table.Rows[r][i].ToString()+".  Only valid values are M or F.");
							}
							break;
						case "Position":
							if(table.Rows[r][i].ToString()!="M"//married
								&& table.Rows[r][i].ToString()!="S")//single
							{
								throw new Exception("Column Position, invalid value: "+table.Rows[r][i].ToString()+".  Only valid values are M or S.");
							}
							break;
						case "Birthdate":
						case "DateFirstVisit":
							if(table.Rows[r][i].ToString()==""){
								continue;
							}
							try{
								DateTime.Parse(table.Rows[r][i].ToString());
							}
							catch{
								throw new Exception("Column Birthdate, unrecognizable date: "+table.Rows[r][i].ToString());
							}
							break;
						case "SSN":
							if(table.Rows[r][i].ToString()==""){
								continue;
							}
							if(!Regex.IsMatch(table.Rows[r][i].ToString(),@"^\d{9}")){
								throw new Exception("Column SSN, invalid value: "+table.Rows[r][i].ToString()+". Must be 9 digits.");
							}
							break;
					}//switch colName
				}//r rows
			}//i columns
//need to make sure all guarantor fk entries exist
			if(checkDontUsePK.Checked){
				return;
			}
			if(radioPatients.Checked){
				//make sure no PatNum already exists
				string command="SELECT PatNum FROM patient";
				DataConnection dcon=new DataConnection();
				DataTable tempT=dcon.GetTable(command);
				bool exists;
				if(radioInsert.Checked){//Insert: no duplicates allowed
					for(int j=0;j<table.Rows.Count;j++){
						for(int i=0;i<tempT.Rows.Count;i++){
							if(tempT.Rows[i][0].ToString()==table.Rows[j]["PatNum"].ToString()){
								throw new Exception("Duplicate PatNum. "+table.Rows[j]["PatNum"].ToString()+" already exists.");
							}
						}
					}
				}
				else{//Update: Every primary key must match
					for(int j=0;j<table.Rows.Count;j++){
						exists=false;
						for(int i=0;i<tempT.Rows.Count;i++){
							if(tempT.Rows[i][0].ToString()==table.Rows[j]["PatNum"].ToString()){
								exists=true;
								break;
							}
						}
						if(!exists){
							throw new Exception(Lan.g(this,"Primary key not present in database: ")+table.Rows[j]["PatNum"].ToString());
						}
					}
				}
				
			}//patients
			else if(radioCarriers.Checked){
				
			}
			
		}

		

		

		
		

		
		

		

		

		

	

		

		


	}
}





















