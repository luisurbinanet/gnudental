/*=============================================================================================================
FreeDental is a dental practice management program.
Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  http://www.docsparks.com

This program is free software; you can redistribute it and/or modify it under the terms of the
GNU General Public License as published by the Free Software Foundation; either version 2 of the License,
or (at your option) any later version.

This program is distributed in the hope that it will be useful, but without any warranty. See the GNU General Public License
for more details, available at http://www.opensource.org/licenses/gpl-license.php

Any changes to this program must follow the guidelines of the GPL license if a modified version is to be
redistributed.
===============================================================================================================*/
//For now, all screens are assumed to have available 990x734.  That would be a screen resolution of 1024x768 with a single width taskbar docked to any one of the four sides of the screen.  Later, the forms will be dynamic to adjust automatically down to about 12" screens.

//The 7 main controls are slightly narrower due to menu bar on left of 42.  Max size 948x708

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{
	
	public class FormOpenDental : System.Windows.Forms.Form{
		private System.Windows.Forms.Button butAccount;
		private System.Windows.Forms.Button butTreat;
		private System.Windows.Forms.Button butChart;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button butDoc;
		private OpenDental.ContrAppt ContrAppt2;
		private System.Windows.Forms.Button button1;
		private System.ComponentModel.IContainer components;
		private OpenDental.ContrStaff ContrStaff2;
		private System.Windows.Forms.Button butAppt;
		private System.Windows.Forms.Label labelAccount;
		private System.Windows.Forms.Label labelFam;
		private System.Windows.Forms.Label labelAppt;
		private System.Windows.Forms.Label labelTreat;
		private OpenDental.ContrFamily ContrFamily2;
		private System.Windows.Forms.Button butFamily;
		private System.Windows.Forms.Panel panelLeft;
		private OpenDental.ContrTreat ContrTreat2;
		private OpenDental.ContrChart ContrChart2;
		private OpenDental.ContrAccount ContrAccount2;
		private System.Windows.Forms.Label labelChart;
		private Thread Thread2;
		private System.Windows.Forms.Button butStaff;
		private System.Windows.Forms.Button butMessage;
		private OpenDental.ContrMessage ContrMessage2;
		private TcpListener TcpListener2;
		private System.Windows.Forms.PictureBox pictButtons;
		private System.Windows.Forms.ImageList imageList2x6;
		private bool[,] buttonDown=new bool[2,6];
		private System.Windows.Forms.Label labelDocs;
		private System.Windows.Forms.Label labelMsg;
		private System.Windows.Forms.Label labelStaff;
		private System.Windows.Forms.Timer timerTimeIndic;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItemSettings;
		private System.Windows.Forms.MenuItem menuItemReports;
		private System.Windows.Forms.MenuItem menuItemPrinter;
		private System.Windows.Forms.MenuItem menuItemScanner;
		private System.Windows.Forms.MenuItem menuItemDataPath;
		private System.Windows.Forms.MenuItem menuItemConfig;
		private System.Windows.Forms.MenuItem menuItemAutoCodes;
		private System.Windows.Forms.MenuItem menuItemDefinitions;
		private System.Windows.Forms.MenuItem menuItemInsCats;
		private System.Windows.Forms.MenuItem menuItemLinks;
		private System.Windows.Forms.MenuItem menuItemRecall;
		private System.Windows.Forms.MenuItem menuItemEmployees;
		private System.Windows.Forms.MenuItem menuItemPractice;
		private System.Windows.Forms.MenuItem menuItemPrescriptions;
		private System.Windows.Forms.MenuItem menuItemProviders;
		private System.Windows.Forms.MenuItem menuItemProcCode;
		private System.Windows.Forms.MenuItem menuItemViewCode;
		private System.Windows.Forms.MenuItem menuItemEditCode;
		private System.Windows.Forms.MenuItem menuItemPracDef;
		private System.Windows.Forms.MenuItem menuItemPracSched;
		private System.Windows.Forms.MenuItem menuItemPrintScreen;
		private System.Windows.Forms.MenuItem menuItemFinanceCharge;
		private System.Windows.Forms.MenuItem menuItemAging;
		private System.Windows.Forms.MenuItem menuItemBilling;
		private System.Windows.Forms.MenuItem menuItemDaily;
		private System.Windows.Forms.MenuItem menuItemRpProc;
		private System.Windows.Forms.MenuItem menuItemRpPay;
		private System.Windows.Forms.MenuItem menuItemRpAdj;
		private System.Windows.Forms.MenuItem menuItemMonthly;
		private System.Windows.Forms.MenuItem menuItemRpOutInsClaims;
		private System.Windows.Forms.MenuItem menuItemSched;
		private System.Windows.Forms.MenuItem menuItemRpDepSlip;
		private System.Windows.Forms.MenuItem menuItemRpAging;
		private System.Windows.Forms.MenuItem menuItemRpProcNoBilled;
		private System.Windows.Forms.MenuItem menuItemRpClaimsNotSent;
		private System.Windows.Forms.MenuItem menuItemRpProduction;
		private System.Windows.Forms.MenuItem menuItemRpFinanceCharge;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItemUserQuery;
		private System.Windows.Forms.MenuItem menuItemList;
		private System.Windows.Forms.MenuItem menuItemTranslation;
		private System.Windows.Forms.MenuItem menuItemPatList;
		private System.Windows.Forms.MenuItem menuItemInsCo;
		private System.Windows.Forms.MenuItem menuItemProcCodes;
		private System.Windows.Forms.MenuItem menuItemRxs;
		private System.Windows.Forms.MenuItem menuItemRefs;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItemLists;
		private System.Windows.Forms.MenuItem menuItemTools;
		private System.Windows.Forms.MenuItem menuItemInsTemplates;
		private System.Windows.Forms.MenuItem menuItemReferrals;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItemPermissions;
		private System.Windows.Forms.MenuItem menuItemPrefs;
		private System.Windows.Forms.MenuItem menuItemCheckDatabase;
		private System.Windows.Forms.MenuItem menuItemProcedureButtons;
		public System.Windows.Forms.MenuItem menuItemZipCodes;
		private System.Windows.Forms.MenuItem menuItem1;
		public System.Windows.Forms.MenuItem menuTelephone;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItemClaims;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private OpenDental.ContrDocs ContrDocs2;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItemHelp;
		private System.Windows.Forms.MenuItem menuItemHelpIndex;
		private Image buttonsShadow;
		public static bool TimeBarIsDisabled=false;

		public FormOpenDental(){
			InitializeComponent();
		}

		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormOpenDental));
			this.panelLeft = new System.Windows.Forms.Panel();
			this.labelStaff = new System.Windows.Forms.Label();
			this.labelMsg = new System.Windows.Forms.Label();
			this.labelDocs = new System.Windows.Forms.Label();
			this.butMessage = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.labelChart = new System.Windows.Forms.Label();
			this.labelAccount = new System.Windows.Forms.Label();
			this.labelFam = new System.Windows.Forms.Label();
			this.labelAppt = new System.Windows.Forms.Label();
			this.butFamily = new System.Windows.Forms.Button();
			this.butAccount = new System.Windows.Forms.Button();
			this.butChart = new System.Windows.Forms.Button();
			this.butDoc = new System.Windows.Forms.Button();
			this.butStaff = new System.Windows.Forms.Button();
			this.butAppt = new System.Windows.Forms.Button();
			this.labelTreat = new System.Windows.Forms.Label();
			this.butTreat = new System.Windows.Forms.Button();
			this.pictButtons = new System.Windows.Forms.PictureBox();
			this.ContrMessage2 = new OpenDental.ContrMessage();
			this.ContrTreat2 = new OpenDental.ContrTreat();
			this.ContrChart2 = new OpenDental.ContrChart();
			this.ContrDocs2 = new OpenDental.ContrDocs();
			this.ContrAppt2 = new OpenDental.ContrAppt();
			this.ContrAccount2 = new OpenDental.ContrAccount();
			this.button1 = new System.Windows.Forms.Button();
			this.ContrStaff2 = new OpenDental.ContrStaff();
			this.ContrFamily2 = new OpenDental.ContrFamily();
			this.imageList2x6 = new System.Windows.Forms.ImageList(this.components);
			this.timerTimeIndic = new System.Windows.Forms.Timer(this.components);
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItemFile = new System.Windows.Forms.MenuItem();
			this.menuItemPrinter = new System.Windows.Forms.MenuItem();
			this.menuItemScanner = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItemConfig = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.menuItemSettings = new System.Windows.Forms.MenuItem();
			this.menuItemAutoCodes = new System.Windows.Forms.MenuItem();
			this.menuItemProcedureButtons = new System.Windows.Forms.MenuItem();
			this.menuItemDefinitions = new System.Windows.Forms.MenuItem();
			this.menuItemInsCats = new System.Windows.Forms.MenuItem();
			this.menuItemPermissions = new System.Windows.Forms.MenuItem();
			this.menuItemLinks = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItemSched = new System.Windows.Forms.MenuItem();
			this.menuItemPracDef = new System.Windows.Forms.MenuItem();
			this.menuItemPracSched = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItemPrefs = new System.Windows.Forms.MenuItem();
			this.menuItemDataPath = new System.Windows.Forms.MenuItem();
			this.menuItemPractice = new System.Windows.Forms.MenuItem();
			this.menuItemRecall = new System.Windows.Forms.MenuItem();
			this.menuItemLists = new System.Windows.Forms.MenuItem();
			this.menuItemEmployees = new System.Windows.Forms.MenuItem();
			this.menuItemInsTemplates = new System.Windows.Forms.MenuItem();
			this.menuItemProviders = new System.Windows.Forms.MenuItem();
			this.menuItemPrescriptions = new System.Windows.Forms.MenuItem();
			this.menuItemReferrals = new System.Windows.Forms.MenuItem();
			this.menuItemZipCodes = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItemProcCode = new System.Windows.Forms.MenuItem();
			this.menuItemEditCode = new System.Windows.Forms.MenuItem();
			this.menuItemViewCode = new System.Windows.Forms.MenuItem();
			this.menuItemReports = new System.Windows.Forms.MenuItem();
			this.menuItemUserQuery = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemDaily = new System.Windows.Forms.MenuItem();
			this.menuItemRpAdj = new System.Windows.Forms.MenuItem();
			this.menuItemRpDepSlip = new System.Windows.Forms.MenuItem();
			this.menuItemRpPay = new System.Windows.Forms.MenuItem();
			this.menuItemRpProc = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItemMonthly = new System.Windows.Forms.MenuItem();
			this.menuItemRpAging = new System.Windows.Forms.MenuItem();
			this.menuItemRpClaimsNotSent = new System.Windows.Forms.MenuItem();
			this.menuItemRpFinanceCharge = new System.Windows.Forms.MenuItem();
			this.menuItemRpOutInsClaims = new System.Windows.Forms.MenuItem();
			this.menuItemRpProcNoBilled = new System.Windows.Forms.MenuItem();
			this.menuItemRpProduction = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItemList = new System.Windows.Forms.MenuItem();
			this.menuItemInsCo = new System.Windows.Forms.MenuItem();
			this.menuItemPatList = new System.Windows.Forms.MenuItem();
			this.menuItemRxs = new System.Windows.Forms.MenuItem();
			this.menuItemProcCodes = new System.Windows.Forms.MenuItem();
			this.menuItemRefs = new System.Windows.Forms.MenuItem();
			this.menuItemTools = new System.Windows.Forms.MenuItem();
			this.menuItemPrintScreen = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemCheckDatabase = new System.Windows.Forms.MenuItem();
			this.menuItemTranslation = new System.Windows.Forms.MenuItem();
			this.menuTelephone = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItemAging = new System.Windows.Forms.MenuItem();
			this.menuItemFinanceCharge = new System.Windows.Forms.MenuItem();
			this.menuItemBilling = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItemClaims = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItemHelp = new System.Windows.Forms.MenuItem();
			this.menuItemHelpIndex = new System.Windows.Forms.MenuItem();
			this.menuItemAbout = new System.Windows.Forms.MenuItem();
			this.panelLeft.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelLeft
			// 
			this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelLeft.Controls.Add(this.labelStaff);
			this.panelLeft.Controls.Add(this.labelMsg);
			this.panelLeft.Controls.Add(this.labelDocs);
			this.panelLeft.Controls.Add(this.butMessage);
			this.panelLeft.Controls.Add(this.labelChart);
			this.panelLeft.Controls.Add(this.labelAccount);
			this.panelLeft.Controls.Add(this.labelFam);
			this.panelLeft.Controls.Add(this.labelAppt);
			this.panelLeft.Controls.Add(this.butFamily);
			this.panelLeft.Controls.Add(this.butAccount);
			this.panelLeft.Controls.Add(this.butChart);
			this.panelLeft.Controls.Add(this.butDoc);
			this.panelLeft.Controls.Add(this.butStaff);
			this.panelLeft.Controls.Add(this.butAppt);
			this.panelLeft.Controls.Add(this.labelTreat);
			this.panelLeft.Controls.Add(this.butTreat);
			this.panelLeft.Controls.Add(this.pictButtons);
			this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelLeft.Location = new System.Drawing.Point(0, 0);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new System.Drawing.Size(42, 690);
			this.panelLeft.TabIndex = 1;
			// 
			// labelStaff
			// 
			this.labelStaff.Location = new System.Drawing.Point(1, 656);
			this.labelStaff.Name = "labelStaff";
			this.labelStaff.Size = new System.Drawing.Size(35, 24);
			this.labelStaff.TabIndex = 26;
			this.labelStaff.Text = "Staff";
			this.labelStaff.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelStaff.Visible = false;
			this.labelStaff.Click += new System.EventHandler(this.butStaff_Click);
			// 
			// labelMsg
			// 
			this.labelMsg.Location = new System.Drawing.Point(1, 409);
			this.labelMsg.Name = "labelMsg";
			this.labelMsg.Size = new System.Drawing.Size(35, 24);
			this.labelMsg.TabIndex = 25;
			this.labelMsg.Text = "Msg";
			this.labelMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelMsg.Click += new System.EventHandler(this.butMessage_Click);
			// 
			// labelDocs
			// 
			this.labelDocs.Location = new System.Drawing.Point(1, 335);
			this.labelDocs.Name = "labelDocs";
			this.labelDocs.Size = new System.Drawing.Size(35, 24);
			this.labelDocs.TabIndex = 24;
			this.labelDocs.Text = "Docs";
			this.labelDocs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelDocs.Click += new System.EventHandler(this.butDoc_Click);
			// 
			// butMessage
			// 
			this.butMessage.BackColor = System.Drawing.SystemColors.Control;
			this.butMessage.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.butMessage.ImageIndex = 8;
			this.butMessage.ImageList = this.imageList1;
			this.butMessage.Location = new System.Drawing.Point(0, 383);
			this.butMessage.Name = "butMessage";
			this.butMessage.Size = new System.Drawing.Size(38, 54);
			this.butMessage.TabIndex = 7;
			this.butMessage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butMessage.Click += new System.EventHandler(this.butMessage_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(22, 22);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// labelChart
			// 
			this.labelChart.Location = new System.Drawing.Point(1, 281);
			this.labelChart.Name = "labelChart";
			this.labelChart.Size = new System.Drawing.Size(35, 24);
			this.labelChart.TabIndex = 20;
			this.labelChart.Text = "Chart";
			this.labelChart.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelChart.Click += new System.EventHandler(this.butChart_Click);
			// 
			// labelAccount
			// 
			this.labelAccount.Location = new System.Drawing.Point(1, 173);
			this.labelAccount.Name = "labelAccount";
			this.labelAccount.Size = new System.Drawing.Size(35, 24);
			this.labelAccount.TabIndex = 19;
			this.labelAccount.Text = "Acct";
			this.labelAccount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelAccount.Click += new System.EventHandler(this.butAccount_Click);
			// 
			// labelFam
			// 
			this.labelFam.Location = new System.Drawing.Point(1, 119);
			this.labelFam.Name = "labelFam";
			this.labelFam.Size = new System.Drawing.Size(35, 24);
			this.labelFam.TabIndex = 19;
			this.labelFam.Text = "Fam";
			this.labelFam.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelFam.Click += new System.EventHandler(this.butFamily_Click);
			// 
			// labelAppt
			// 
			this.labelAppt.BackColor = System.Drawing.SystemColors.Control;
			this.labelAppt.Location = new System.Drawing.Point(1, 48);
			this.labelAppt.Name = "labelAppt";
			this.labelAppt.Size = new System.Drawing.Size(35, 24);
			this.labelAppt.TabIndex = 19;
			this.labelAppt.Text = "Appts";
			this.labelAppt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.labelAppt.Click += new System.EventHandler(this.butAppt_Click);
			// 
			// butFamily
			// 
			this.butFamily.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.butFamily.ImageIndex = 2;
			this.butFamily.ImageList = this.imageList1;
			this.butFamily.Location = new System.Drawing.Point(0, 92);
			this.butFamily.Name = "butFamily";
			this.butFamily.Size = new System.Drawing.Size(38, 54);
			this.butFamily.TabIndex = 1;
			this.butFamily.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butFamily.Click += new System.EventHandler(this.butFamily_Click);
			// 
			// butAccount
			// 
			this.butAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.butAccount.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.butAccount.ImageIndex = 4;
			this.butAccount.ImageList = this.imageList1;
			this.butAccount.Location = new System.Drawing.Point(0, 146);
			this.butAccount.Name = "butAccount";
			this.butAccount.Size = new System.Drawing.Size(38, 54);
			this.butAccount.TabIndex = 2;
			this.butAccount.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butAccount.Click += new System.EventHandler(this.butAccount_Click);
			// 
			// butChart
			// 
			this.butChart.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.butChart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.butChart.ImageIndex = 1;
			this.butChart.ImageList = this.imageList1;
			this.butChart.Location = new System.Drawing.Point(0, 254);
			this.butChart.Name = "butChart";
			this.butChart.Size = new System.Drawing.Size(38, 54);
			this.butChart.TabIndex = 4;
			this.butChart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butChart.Click += new System.EventHandler(this.butChart_Click);
			// 
			// butDoc
			// 
			this.butDoc.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.butDoc.ImageIndex = 6;
			this.butDoc.ImageList = this.imageList1;
			this.butDoc.Location = new System.Drawing.Point(0, 308);
			this.butDoc.Name = "butDoc";
			this.butDoc.Size = new System.Drawing.Size(38, 54);
			this.butDoc.TabIndex = 5;
			this.butDoc.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butDoc.Click += new System.EventHandler(this.butDoc_Click);
			// 
			// butStaff
			// 
			this.butStaff.BackColor = System.Drawing.SystemColors.Control;
			this.butStaff.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.butStaff.ImageIndex = 7;
			this.butStaff.ImageList = this.imageList1;
			this.butStaff.Location = new System.Drawing.Point(0, 623);
			this.butStaff.Name = "butStaff";
			this.butStaff.Size = new System.Drawing.Size(38, 54);
			this.butStaff.TabIndex = 15;
			this.butStaff.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butStaff.Visible = false;
			this.butStaff.Click += new System.EventHandler(this.butStaff_Click);
			// 
			// butAppt
			// 
			this.butAppt.BackColor = System.Drawing.SystemColors.Control;
			this.butAppt.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.butAppt.ImageIndex = 0;
			this.butAppt.ImageList = this.imageList1;
			this.butAppt.Location = new System.Drawing.Point(0, 20);
			this.butAppt.Name = "butAppt";
			this.butAppt.Size = new System.Drawing.Size(38, 54);
			this.butAppt.TabIndex = 0;
			this.butAppt.Click += new System.EventHandler(this.butAppt_Click);
			// 
			// labelTreat
			// 
			this.labelTreat.Location = new System.Drawing.Point(0, 228);
			this.labelTreat.Name = "labelTreat";
			this.labelTreat.Size = new System.Drawing.Size(36, 24);
			this.labelTreat.TabIndex = 16;
			this.labelTreat.Text = "Treat\' Plan";
			this.labelTreat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.labelTreat.Click += new System.EventHandler(this.butTreat_Click);
			// 
			// butTreat
			// 
			this.butTreat.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.butTreat.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.butTreat.ImageIndex = 5;
			this.butTreat.ImageList = this.imageList1;
			this.butTreat.Location = new System.Drawing.Point(0, 200);
			this.butTreat.Name = "butTreat";
			this.butTreat.Size = new System.Drawing.Size(38, 54);
			this.butTreat.TabIndex = 3;
			this.butTreat.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.butTreat.Click += new System.EventHandler(this.butTreat_Click);
			// 
			// pictButtons
			// 
			this.pictButtons.Image = ((System.Drawing.Image)(resources.GetObject("pictButtons.Image")));
			this.pictButtons.Location = new System.Drawing.Point(0, 437);
			this.pictButtons.Name = "pictButtons";
			this.pictButtons.Size = new System.Drawing.Size(37, 109);
			this.pictButtons.TabIndex = 17;
			this.pictButtons.TabStop = false;
			this.pictButtons.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictButtons_MouseDown);
			// 
			// ContrMessage2
			// 
			this.ContrMessage2.Location = new System.Drawing.Point(50, 0);
			this.ContrMessage2.Name = "ContrMessage2";
			this.ContrMessage2.Size = new System.Drawing.Size(803, 643);
			this.ContrMessage2.TabIndex = 17;
			// 
			// ContrTreat2
			// 
			this.ContrTreat2.Location = new System.Drawing.Point(47, 0);
			this.ContrTreat2.Name = "ContrTreat2";
			this.ContrTreat2.Size = new System.Drawing.Size(880, 690);
			this.ContrTreat2.TabIndex = 5;
			// 
			// ContrChart2
			// 
			this.ContrChart2.DataValid = false;
			this.ContrChart2.Location = new System.Drawing.Point(47, 0);
			this.ContrChart2.Name = "ContrChart2";
			this.ContrChart2.Size = new System.Drawing.Size(880, 690);
			this.ContrChart2.TabIndex = 6;
			this.ContrChart2.Visible = false;
			// 
			// ContrDocs2
			// 
			this.ContrDocs2.Location = new System.Drawing.Point(47, 0);
			this.ContrDocs2.Name = "ContrDocs2";
			this.ContrDocs2.Size = new System.Drawing.Size(812, 678);
			this.ContrDocs2.TabIndex = 8;
			this.ContrDocs2.Visible = false;
			// 
			// ContrAppt2
			// 
			this.ContrAppt2.DockPadding.Left = 56;
			this.ContrAppt2.Location = new System.Drawing.Point(0, 0);
			this.ContrAppt2.Name = "ContrAppt2";
			this.ContrAppt2.Size = new System.Drawing.Size(880, 690);
			this.ContrAppt2.TabIndex = 9;
			// 
			// ContrAccount2
			// 
			this.ContrAccount2.Location = new System.Drawing.Point(47, 0);
			this.ContrAccount2.Name = "ContrAccount2";
			this.ContrAccount2.Size = new System.Drawing.Size(880, 690);
			this.ContrAccount2.TabIndex = 11;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(740, 244);
			this.button1.Name = "button1";
			this.button1.TabIndex = 14;
			this.button1.Text = "Winsock";
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// ContrStaff2
			// 
			this.ContrStaff2.Location = new System.Drawing.Point(0, 0);
			this.ContrStaff2.Name = "ContrStaff2";
			this.ContrStaff2.Size = new System.Drawing.Size(880, 690);
			this.ContrStaff2.TabIndex = 15;
			// 
			// ContrFamily2
			// 
			this.ContrFamily2.Location = new System.Drawing.Point(47, 0);
			this.ContrFamily2.Name = "ContrFamily2";
			this.ContrFamily2.Size = new System.Drawing.Size(976, 740);
			this.ContrFamily2.TabIndex = 16;
			this.ContrFamily2.Visible = false;
			// 
			// imageList2x6
			// 
			this.imageList2x6.ImageSize = new System.Drawing.Size(37, 109);
			this.imageList2x6.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2x6.ImageStream")));
			this.imageList2x6.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// timerTimeIndic
			// 
			this.timerTimeIndic.Interval = 60000;
			this.timerTimeIndic.Tick += new System.EventHandler(this.timerTimeIndic_Tick);
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																						 this.menuItemFile,
																																						 this.menuItemSettings,
																																						 this.menuItemLists,
																																						 this.menuItemReports,
																																						 this.menuItemTools,
																																						 this.menuItem12});
			// 
			// menuItemFile
			// 
			this.menuItemFile.Index = 0;
			this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.menuItemPrinter,
																																								 this.menuItemScanner,
																																								 this.menuItem6,
																																								 this.menuItemConfig,
																																								 this.menuItem7,
																																								 this.menuItemExit});
			this.menuItemFile.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuItemFile.Text = "&File";
			// 
			// menuItemPrinter
			// 
			this.menuItemPrinter.Index = 0;
			this.menuItemPrinter.Text = "&Printer";
			this.menuItemPrinter.Click += new System.EventHandler(this.menuItemPrinter_Click);
			// 
			// menuItemScanner
			// 
			this.menuItemScanner.Index = 1;
			this.menuItemScanner.Text = "&Scanner";
			this.menuItemScanner.Click += new System.EventHandler(this.menuItemScanner_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.Text = "-";
			// 
			// menuItemConfig
			// 
			this.menuItemConfig.Index = 3;
			this.menuItemConfig.Text = "&MySQL Configuration";
			this.menuItemConfig.Click += new System.EventHandler(this.menuItemConfig_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 4;
			this.menuItem7.Text = "-";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 5;
			this.menuItemExit.ShowShortcut = false;
			this.menuItemExit.Text = "E&xit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// menuItemSettings
			// 
			this.menuItemSettings.Index = 1;
			this.menuItemSettings.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										 this.menuItemAutoCodes,
																																										 this.menuItemProcedureButtons,
																																										 this.menuItemDefinitions,
																																										 this.menuItemInsCats,
																																										 this.menuItemPermissions,
																																										 this.menuItemLinks,
																																										 this.menuItem10,
																																										 this.menuItemSched,
																																										 this.menuItemPracDef,
																																										 this.menuItemPracSched,
																																										 this.menuItem4,
																																										 this.menuItemPrefs,
																																										 this.menuItemDataPath,
																																										 this.menuItemPractice,
																																										 this.menuItemRecall});
			this.menuItemSettings.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuItemSettings.Text = "&Setup";
			// 
			// menuItemAutoCodes
			// 
			this.menuItemAutoCodes.Index = 0;
			this.menuItemAutoCodes.Text = "Auto Codes";
			this.menuItemAutoCodes.Click += new System.EventHandler(this.menuItemAutoCodes_Click);
			// 
			// menuItemProcedureButtons
			// 
			this.menuItemProcedureButtons.Index = 1;
			this.menuItemProcedureButtons.Text = "Procedure Buttons";
			this.menuItemProcedureButtons.Click += new System.EventHandler(this.menuItemProcedureButtons_Click);
			// 
			// menuItemDefinitions
			// 
			this.menuItemDefinitions.Index = 2;
			this.menuItemDefinitions.Text = "Definitions";
			this.menuItemDefinitions.Click += new System.EventHandler(this.menuItemDefinitions_Click);
			// 
			// menuItemInsCats
			// 
			this.menuItemInsCats.Index = 3;
			this.menuItemInsCats.Text = "Insurance Categories";
			this.menuItemInsCats.Click += new System.EventHandler(this.menuItemInsCats_Click);
			// 
			// menuItemPermissions
			// 
			this.menuItemPermissions.Index = 4;
			this.menuItemPermissions.Text = "Permissions";
			this.menuItemPermissions.Click += new System.EventHandler(this.menuItemPermissions_Click);
			// 
			// menuItemLinks
			// 
			this.menuItemLinks.Index = 5;
			this.menuItemLinks.Text = "Program Links";
			this.menuItemLinks.Click += new System.EventHandler(this.menuItemLinks_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 6;
			this.menuItem10.Text = "-";
			// 
			// menuItemSched
			// 
			this.menuItemSched.Index = 7;
			this.menuItemSched.Text = "SCHEDULES";
			// 
			// menuItemPracDef
			// 
			this.menuItemPracDef.Index = 8;
			this.menuItemPracDef.Text = "   Practice Default";
			this.menuItemPracDef.Click += new System.EventHandler(this.menuItemPracDef_Click);
			// 
			// menuItemPracSched
			// 
			this.menuItemPracSched.Index = 9;
			this.menuItemPracSched.Text = "   Practice Schedule";
			this.menuItemPracSched.Click += new System.EventHandler(this.menuItemPracSched_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 10;
			this.menuItem4.Text = "-";
			// 
			// menuItemPrefs
			// 
			this.menuItemPrefs.Index = 11;
			this.menuItemPrefs.Text = "PREFERENCES";
			// 
			// menuItemDataPath
			// 
			this.menuItemDataPath.Index = 12;
			this.menuItemDataPath.Text = "   Data Paths";
			this.menuItemDataPath.Click += new System.EventHandler(this.menuItemDataPath_Click);
			// 
			// menuItemPractice
			// 
			this.menuItemPractice.Index = 13;
			this.menuItemPractice.Text = "   Practice";
			this.menuItemPractice.Click += new System.EventHandler(this.menuItemPractice_Click);
			// 
			// menuItemRecall
			// 
			this.menuItemRecall.Index = 14;
			this.menuItemRecall.Text = "   Recall";
			this.menuItemRecall.Click += new System.EventHandler(this.menuItemRecall_Click);
			// 
			// menuItemLists
			// 
			this.menuItemLists.Index = 2;
			this.menuItemLists.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									this.menuItemEmployees,
																																									this.menuItemInsTemplates,
																																									this.menuItemProviders,
																																									this.menuItemPrescriptions,
																																									this.menuItemReferrals,
																																									this.menuItemZipCodes,
																																									this.menuItem5,
																																									this.menuItemProcCode,
																																									this.menuItemEditCode,
																																									this.menuItemViewCode});
			this.menuItemLists.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
			this.menuItemLists.Text = "&Lists";
			// 
			// menuItemEmployees
			// 
			this.menuItemEmployees.Index = 0;
			this.menuItemEmployees.Text = "Employees";
			this.menuItemEmployees.Click += new System.EventHandler(this.menuItemEmployees_Click);
			// 
			// menuItemInsTemplates
			// 
			this.menuItemInsTemplates.Index = 1;
			this.menuItemInsTemplates.Text = "Insurance Templates";
			this.menuItemInsTemplates.Click += new System.EventHandler(this.menuItemInsTemplates_Click);
			// 
			// menuItemProviders
			// 
			this.menuItemProviders.Index = 2;
			this.menuItemProviders.Text = "Providers";
			this.menuItemProviders.Click += new System.EventHandler(this.menuItemProviders_Click);
			// 
			// menuItemPrescriptions
			// 
			this.menuItemPrescriptions.Index = 3;
			this.menuItemPrescriptions.Text = "Prescriptions";
			this.menuItemPrescriptions.Click += new System.EventHandler(this.menuItemPrescriptions_Click);
			// 
			// menuItemReferrals
			// 
			this.menuItemReferrals.Index = 4;
			this.menuItemReferrals.Text = "Referrals";
			this.menuItemReferrals.Click += new System.EventHandler(this.menuItemReferrals_Click);
			// 
			// menuItemZipCodes
			// 
			this.menuItemZipCodes.Index = 5;
			this.menuItemZipCodes.Text = "Zip Codes";
			this.menuItemZipCodes.Click += new System.EventHandler(this.menuItemZipCodes_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 6;
			this.menuItem5.Text = "-";
			// 
			// menuItemProcCode
			// 
			this.menuItemProcCode.Index = 7;
			this.menuItemProcCode.Text = "PROCEDURE CODES";
			// 
			// menuItemEditCode
			// 
			this.menuItemEditCode.Index = 8;
			this.menuItemEditCode.Text = "   Edit Codes";
			this.menuItemEditCode.Click += new System.EventHandler(this.menuItemEditCode_Click);
			// 
			// menuItemViewCode
			// 
			this.menuItemViewCode.Index = 9;
			this.menuItemViewCode.Text = "   View Fees";
			this.menuItemViewCode.Click += new System.EventHandler(this.menuItemViewCode_Click);
			// 
			// menuItemReports
			// 
			this.menuItemReports.Index = 3;
			this.menuItemReports.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.menuItemUserQuery,
																																										this.menuItem2,
																																										this.menuItemDaily,
																																										this.menuItemRpAdj,
																																										this.menuItemRpDepSlip,
																																										this.menuItemRpPay,
																																										this.menuItemRpProc,
																																										this.menuItem3,
																																										this.menuItemMonthly,
																																										this.menuItemRpAging,
																																										this.menuItemRpClaimsNotSent,
																																										this.menuItemRpFinanceCharge,
																																										this.menuItemRpOutInsClaims,
																																										this.menuItemRpProcNoBilled,
																																										this.menuItemRpProduction,
																																										this.menuItem8,
																																										this.menuItemList,
																																										this.menuItemInsCo,
																																										this.menuItemPatList,
																																										this.menuItemRxs,
																																										this.menuItemProcCodes,
																																										this.menuItemRefs});
			this.menuItemReports.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
			this.menuItemReports.Text = "&Reports";
			// 
			// menuItemUserQuery
			// 
			this.menuItemUserQuery.Index = 0;
			this.menuItemUserQuery.Text = "User Query";
			this.menuItemUserQuery.Click += new System.EventHandler(this.menuItemUserQuery_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "-";
			// 
			// menuItemDaily
			// 
			this.menuItemDaily.Index = 2;
			this.menuItemDaily.Text = "DAILY";
			this.menuItemDaily.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.menuItemDaily_DrawItem);
			this.menuItemDaily.Click += new System.EventHandler(this.menuItemDaily_Click);
			// 
			// menuItemRpAdj
			// 
			this.menuItemRpAdj.Index = 3;
			this.menuItemRpAdj.Text = "   Adjustments";
			this.menuItemRpAdj.Click += new System.EventHandler(this.menuItemRpAdj_Click);
			// 
			// menuItemRpDepSlip
			// 
			this.menuItemRpDepSlip.Index = 4;
			this.menuItemRpDepSlip.Text = "   Deposit Slip";
			this.menuItemRpDepSlip.Click += new System.EventHandler(this.menuItemRpDepSlip_Click);
			// 
			// menuItemRpPay
			// 
			this.menuItemRpPay.Index = 5;
			this.menuItemRpPay.Text = "   Payments";
			this.menuItemRpPay.Click += new System.EventHandler(this.menuItemRpPay_Click);
			// 
			// menuItemRpProc
			// 
			this.menuItemRpProc.Index = 6;
			this.menuItemRpProc.Text = "   Procedures";
			this.menuItemRpProc.Click += new System.EventHandler(this.menuItemRpProc_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 7;
			this.menuItem3.Text = "-";
			// 
			// menuItemMonthly
			// 
			this.menuItemMonthly.Index = 8;
			this.menuItemMonthly.Text = "MONTHLY";
			// 
			// menuItemRpAging
			// 
			this.menuItemRpAging.Index = 9;
			this.menuItemRpAging.Text = "   Aging Report";
			this.menuItemRpAging.Click += new System.EventHandler(this.menuItemRpAging_Click);
			// 
			// menuItemRpClaimsNotSent
			// 
			this.menuItemRpClaimsNotSent.Index = 10;
			this.menuItemRpClaimsNotSent.Text = "   Claims Not Sent";
			this.menuItemRpClaimsNotSent.Click += new System.EventHandler(this.menuItemRpClaimsNotSent_Click);
			// 
			// menuItemRpFinanceCharge
			// 
			this.menuItemRpFinanceCharge.Index = 11;
			this.menuItemRpFinanceCharge.Text = "   Finance Charge Report";
			this.menuItemRpFinanceCharge.Click += new System.EventHandler(this.menuItemRpFinanceCharge_Click);
			// 
			// menuItemRpOutInsClaims
			// 
			this.menuItemRpOutInsClaims.Index = 12;
			this.menuItemRpOutInsClaims.Text = "   Outstanding Insurance Claims";
			this.menuItemRpOutInsClaims.Click += new System.EventHandler(this.menuItemRpOutInsClaims_Click);
			// 
			// menuItemRpProcNoBilled
			// 
			this.menuItemRpProcNoBilled.Index = 13;
			this.menuItemRpProcNoBilled.Text = "   Procedures Not Billed To Insurance";
			this.menuItemRpProcNoBilled.Click += new System.EventHandler(this.menuItemRpProcNoBilled_Click);
			// 
			// menuItemRpProduction
			// 
			this.menuItemRpProduction.Index = 14;
			this.menuItemRpProduction.Text = "   Production and Income";
			this.menuItemRpProduction.Click += new System.EventHandler(this.menuItemRpProduction_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 15;
			this.menuItem8.Text = "-";
			// 
			// menuItemList
			// 
			this.menuItemList.Index = 16;
			this.menuItemList.Text = "LISTS";
			// 
			// menuItemInsCo
			// 
			this.menuItemInsCo.Index = 17;
			this.menuItemInsCo.Text = "   Insurance Companies";
			this.menuItemInsCo.Click += new System.EventHandler(this.menuItemInsCo_Click);
			// 
			// menuItemPatList
			// 
			this.menuItemPatList.Index = 18;
			this.menuItemPatList.Text = "   Patients";
			this.menuItemPatList.Click += new System.EventHandler(this.menuItemPatList_Click);
			// 
			// menuItemRxs
			// 
			this.menuItemRxs.Index = 19;
			this.menuItemRxs.Text = "   Prescriptions";
			this.menuItemRxs.Click += new System.EventHandler(this.menuItemRxs_Click);
			// 
			// menuItemProcCodes
			// 
			this.menuItemProcCodes.Index = 20;
			this.menuItemProcCodes.Text = "   Procedure Codes";
			this.menuItemProcCodes.Click += new System.EventHandler(this.menuItemProcCodes_Click);
			// 
			// menuItemRefs
			// 
			this.menuItemRefs.Index = 21;
			this.menuItemRefs.Text = "   Referrals";
			this.menuItemRefs.Click += new System.EventHandler(this.menuItemRefs_Click);
			// 
			// menuItemTools
			// 
			this.menuItemTools.Index = 4;
			this.menuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									this.menuItemPrintScreen,
																																									this.menuItem1,
																																									this.menuItem9,
																																									this.menuItemAging,
																																									this.menuItemFinanceCharge,
																																									this.menuItemBilling,
																																									this.menuItem11,
																																									this.menuItemClaims});
			this.menuItemTools.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
			this.menuItemTools.Text = "&Tools";
			// 
			// menuItemPrintScreen
			// 
			this.menuItemPrintScreen.Index = 0;
			this.menuItemPrintScreen.Text = "Print Screen Tool";
			this.menuItemPrintScreen.Click += new System.EventHandler(this.menuItemPrintScreen_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.menuItemCheckDatabase,
																																							this.menuItemTranslation,
																																							this.menuTelephone});
			this.menuItem1.Text = "Misc Tools";
			// 
			// menuItemCheckDatabase
			// 
			this.menuItemCheckDatabase.Index = 0;
			this.menuItemCheckDatabase.Text = "Check Database Integrity";
			this.menuItemCheckDatabase.Click += new System.EventHandler(this.menuItemCheckDatabase_Click);
			// 
			// menuItemTranslation
			// 
			this.menuItemTranslation.Index = 1;
			this.menuItemTranslation.Text = "Language Translation";
			this.menuItemTranslation.Click += new System.EventHandler(this.menuItemTranslation_Click);
			// 
			// menuTelephone
			// 
			this.menuTelephone.Index = 2;
			this.menuTelephone.Text = "Telephone Numbers";
			this.menuTelephone.Click += new System.EventHandler(this.menuTelephone_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 2;
			this.menuItem9.Text = "-";
			// 
			// menuItemAging
			// 
			this.menuItemAging.Index = 3;
			this.menuItemAging.Text = "Calculate Aging";
			this.menuItemAging.Click += new System.EventHandler(this.menuItemAging_Click);
			// 
			// menuItemFinanceCharge
			// 
			this.menuItemFinanceCharge.Index = 4;
			this.menuItemFinanceCharge.Text = "Run Finance Charges";
			this.menuItemFinanceCharge.Click += new System.EventHandler(this.menuItemFinanceCharge_Click);
			// 
			// menuItemBilling
			// 
			this.menuItemBilling.Index = 5;
			this.menuItemBilling.Text = "Billing";
			this.menuItemBilling.Click += new System.EventHandler(this.menuItemBilling_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 6;
			this.menuItem11.Text = "-";
			// 
			// menuItemClaims
			// 
			this.menuItemClaims.Index = 7;
			this.menuItemClaims.Text = "Send Claims";
			this.menuItemClaims.Click += new System.EventHandler(this.menuItemClaims_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 5;
			this.menuItem12.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							 this.menuItemHelp,
																																							 this.menuItemHelpIndex,
																																							 this.menuItemAbout});
			this.menuItem12.Text = "Help";
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.Index = 0;
			this.menuItemHelp.Text = "Online Help - Contents";
			this.menuItemHelp.Click += new System.EventHandler(this.menuItemHelp_Click);
			// 
			// menuItemHelpIndex
			// 
			this.menuItemHelpIndex.Index = 1;
			this.menuItemHelpIndex.Text = "Online Help - Index";
			this.menuItemHelpIndex.Click += new System.EventHandler(this.menuItemHelpIndex_Click);
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Index = 2;
			this.menuItemAbout.Text = "&About";
			this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
			// 
			// FormOpenDental
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(880, 690);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panelLeft);
			this.Controls.Add(this.ContrAccount2);
			this.Controls.Add(this.ContrFamily2);
			this.Controls.Add(this.ContrStaff2);
			this.Controls.Add(this.ContrDocs2);
			this.Controls.Add(this.ContrChart2);
			this.Controls.Add(this.ContrTreat2);
			this.Controls.Add(this.ContrAppt2);
			this.Controls.Add(this.ContrMessage2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.Name = "FormOpenDental";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Text = "Open Dental";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormFreeDental_Closing);
			this.Load += new System.EventHandler(this.FormOpenDental_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormFreeDental_Layout);
			this.panelLeft.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	
		[STAThread]
		static void Main() {
			//Application.EnableVisualStyles();
			Application.Run(new FormOpenDental());
		}

		private void FormOpenDental_Load(object sender, System.EventArgs e){
			allNeutral();
			ExitApplicationNow.WantsToExit += new System.EventHandler(ExitApplicationNow_WantsToExit);
			FormConfig FormCfg = new FormConfig();
			FormCfg.GetConfig();
			DataClass.SetConnection();
      if(!Prefs.TryToConnect()){
				FormCfg.IsInStartup=true;
				FormCfg.ShowDialog();
				if(FormCfg.DialogResult==DialogResult.Cancel){
			    ExitApplicationNow ExitApplicationNow2=new ExitApplicationNow();
				  ExitApplicationNow2.ExitNow();
					return;
				}     
			}
			if(!RefreshLocalData()) return;
			Lan.Refresh();
			LanguageForeigns.Refresh();
			Batch.Select("graphictype,graphicassembly,graphicelement,graphicshape");
			GraphicPoints.Refresh();
			Directory.CreateDirectory("Sounds");
			string[] myFiles=Directory.GetFiles(((Pref)Prefs.HList["DocPath"]).ValueString+"Sounds");
			for(int i=0;i<myFiles.Length;i++){
				string[] splitFile=myFiles[i].Split('\\');
				File.Copy(myFiles[i],"Sounds\\"+splitFile[splitFile.Length-1],true);
			}
			buttonsShadow=imageList2x6.Images[0];  //(Image)pictButtons.Image.Clone();
			DataValid.BecameInvalid += new System.EventHandler(DataValid_BecameInvalid);
			ContrAccount2.InstantClasses();
			ContrAppt2.InstantClasses();
			ContrChart2.InstantClasses();
			ContrDocs2.InstantClasses();
			ContrFamily2.InstantClasses();
			ContrMessage2.InstantClasses();
			ContrStaff2.InstantClasses();
			ContrTreat2.InstantClasses();
			ContrAppt2.Visible=true;
			butAppt.BackColor=Color.White;
			labelAppt.BackColor=Color.White;
			this.ActiveControl=this.ContrAppt2;
			ContrAppt2.ModuleSelected();
			Thread2 = new Thread(new ThreadStart(Listen));
			if(((Pref)Prefs.HList["AutoRefreshIsDisabled"]).ValueString!="1")
				Thread2.Start();
			timerTimeIndic.Enabled=true;
			Lan.C(this, new System.Windows.Forms.Control[] {
				labelAccount,
				labelAppt,
				labelDocs,
				labelFam,
				labelMsg,
        labelTreat,
				labelChart,
				labelStaff				
			});
			//CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern="MM/dd/yyyy";
			//CultureInfo.CreateSpecificCulture(
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				CultureInfo cInfo=(CultureInfo)CultureInfo.CurrentCulture.Clone();
				cInfo.DateTimeFormat.ShortDatePattern="MM/dd/yyyy";
				Application.CurrentCulture=cInfo;
			}
			
			if(!UserPermissions.CheckUserPassword("Start Up")){
				MessageBox.Show(Lan.g(this,"You do not have permission to use this program."));
				if(Thread2!=null){
					Thread2.Abort();
					this.TcpListener2.Stop();
				}
				Application.Exit();
				return;
			}
			SecurityLogs.MakeLogEntry("Start Up","");
		}

		private bool RefreshLocalData(){
			Prefs.Refresh();
			if(!Prefs.ConvertDB()){
        return false;
      }
			//if(!BackupJobs.IsBackup()){
			//	MessageBox.Show(Lan.g(this,"Missing Pref 'IsDatabaseBackup', Must update database."));
			//}
      AutoCodes.Refresh();
      AutoCodeItems.Refresh();
      AutoCodeConds.Refresh();
			ProcButtons.Refresh();
			ProcButtonItems.Refresh();
      //BackupJobs.Refresh();
			Computers.Refresh();//gets workstation prefs
			CovCats.Refresh();
			CovSpans.Refresh();
			Employees.Refresh();
			Defs.Refresh();
			Fees.Refresh();
			Permissions.Refresh();
			Programs.Refresh();
			Providers.Refresh();
			ProcCodes.Refresh();
			Referrals.Refresh();
			SchedDefaults.Refresh();
			//Schedules.CurDate=DateTime.Today;
      //Schedules.Refresh();
			UserPermissions.Refresh();
		//	Users.Refresh();

			ContrTreat2.InstantClasses();//refreshes priority lists
			if(!Directory.Exists(((Pref)Prefs.HList["DocPath"]).ValueString)
				|| !Directory.Exists(((Pref)Prefs.HList["DocPath"]).ValueString+"A\\")){
				FormPath FormP=new FormPath();
				FormP.ShowDialog();
				if(FormP.DialogResult!=DialogResult.OK){
					return false;
				}
				else{
					Prefs.Refresh();//because listenning thread not started yet.
				}
			}
			return true;
		}

		private void FormFreeDental_Layout(object sender, System.Windows.Forms.LayoutEventArgs e){
			if(Width<200) Width=200;
			ContrAccount2.Location=new Point(this.panelLeft.Width,0);
			ContrAccount2.Width=this.ClientSize.Width-ContrAccount2.Location.X;
			ContrAccount2.Height=this.ClientSize.Height;
			ContrAppt2.Location=new Point(this.panelLeft.Width,0);
			ContrAppt2.Width=this.ClientSize.Width-ContrAppt2.Location.X;
			ContrAppt2.Height=this.ClientSize.Height;
			ContrChart2.Location=new Point(this.panelLeft.Width,0);
			ContrChart2.Width=this.ClientSize.Width-ContrChart2.Location.X;
			ContrChart2.Height=this.ClientSize.Height;
			ContrDocs2.Location=new Point(this.panelLeft.Width,0);
			ContrDocs2.Width=this.ClientSize.Width-ContrDocs2.Location.X;
			ContrDocs2.Height=this.ClientSize.Height;
			ContrFamily2.Location=new Point(this.panelLeft.Width,0);
			ContrFamily2.Width=this.ClientSize.Width-ContrFamily2.Location.X;
			ContrFamily2.Height=this.ClientSize.Height;
			ContrMessage2.Location=new Point(this.panelLeft.Width,0);
			ContrMessage2.Width=this.ClientSize.Width-ContrMessage2.Location.X;
			ContrMessage2.Height=this.ClientSize.Height;
			ContrStaff2.Location=new Point(this.panelLeft.Width,0);
			ContrStaff2.Width=this.ClientSize.Width-ContrStaff2.Location.X;
			ContrStaff2.Height=this.ClientSize.Height;
			ContrTreat2.Location=new Point(this.panelLeft.Width,0);
			ContrTreat2.Width=this.ClientSize.Width-ContrDocs2.Location.X;
			ContrTreat2.Height=this.ClientSize.Height;
		}

		private void DataValid_BecameInvalid(object sender, System.EventArgs e){
			switch(DataValid.IType){
				case InvalidType.LocalData:
					RefreshLocalData();//does local computer first
					if(((Pref)Prefs.HList["AutoRefreshIsDisabled"]).ValueString=="1")
						return;
					Messages.MessageToSend = new MessageInvalid();
					Messages.MessageToSend.Type = "LocalData";
					Messages.MessageToSend.DateViewing = DateTime.MinValue;
					Messages.SendMessage();//then other computers
					break;
				case InvalidType.Date:
					//local refresh is handled within ContrAppt, not here
					if(((Pref)Prefs.HList["AutoRefreshIsDisabled"]).ValueString=="1")
						return;
					Messages.MessageToSend = new MessageInvalid();
					Messages.MessageToSend.Type = "Date";
					Messages.MessageToSend.DateViewing = Appointments.DateSelected;
					Messages.SendMessage();
					break;
			}
		}

		private void ExitApplicationNow_WantsToExit(object sender, System.EventArgs e){
			if(Thread2!=null){
				Thread2.Abort();
				this.TcpListener2.Stop();
			}
			Application.Exit();
		}
		

		private void SendButtonMsg(object sender, System.EventArgs e){
			if(((Pref)Prefs.HList["AutoRefreshIsDisabled"]).ValueString=="1")
				return;
			switch(DataValid.IType){
				case InvalidType.LocalData:
					RefreshLocalData();
					Messages.MessageToSend = new MessageInvalid();
					Messages.MessageToSend.Type = "LocalData";
					Messages.MessageToSend.DateViewing = DateTime.MinValue;
					Messages.SendMessage();
					//RefreshLocalData();
					break;
				case InvalidType.Date:
					ContrAppt2.RefreshModuleScreen();
					Messages.MessageToSend = new MessageInvalid();
					Messages.MessageToSend.Type = "Date";
					Messages.MessageToSend.DateViewing = Appointments.DateSelected;
					Messages.SendMessage();
					//ContrAppt2.ModuleSelected();
					break;
			}
		}

		public void Listen(){//separate thread
			TcpListener2 = new TcpListener(2123);//was 2112
			TcpListener2.Start();
			while (true){
				TcpClient TcpClientRec = TcpListener2.AcceptTcpClient();
				NetworkStream ns = TcpClientRec.GetStream();
				StreamReader StreamReader2 = new StreamReader(ns);
				string strReceived = StreamReader2.ReadToEnd();
				Invoke(new ProcessMessageDelegate(ProcessMessage), new object[] {strReceived});
			}	
			
			//TcpClientRec.Close();
			//TcpListener2.Stop();
		}//end listen

	

		public void ProcessMessage(string text){
			//MessageBox.Show("A message has been received on TCP");
			Messages.RecMessage(text);//parses the xml
			//MessageBox.Show(text);
			if(Messages.RecdMessage.Type!=null){
				switch(Messages.RecdMessage.Type){
					case "LocalData":
						RefreshLocalData();
						break;
					case "Date":
						if(Appointments.DateSelected.Date==Messages.RecdMessage.DateViewing.Date
							&& ContrAppt2.Visible){
							ContrAppt2.RefreshModuleScreen();
						}
						break;
				}
			}
			else{//Button or text message
				switch(Messages.RecdMsgBut.Type){
					case "Button":
						Graphics grfx=Graphics.FromImage(buttonsShadow);
						int row=Messages.RecdMsgBut.Row;
						int col=Messages.RecdMsgBut.Col;
						if(col==0 && Messages.RecdMsgBut.Pushed){//button in first col pushed
							buttonDown[0,row]=true;
							grfx.FillRectangle(new SolidBrush(Color.Red),col*18+1,row*18+1,17,17);
							pictButtons.Image=buttonsShadow;
							pictButtons.Refresh();
							PlaySoundFunct(col,row,false);
						}
						else if(col==1 && Messages.RecdMsgBut.Pushed){//button in second col pushed
							grfx.FillRectangle(new SolidBrush(Color.Red),col*18+1,row*18+1,17,17);
							pictButtons.Image=buttonsShadow;
							pictButtons.Refresh();
							PlaySoundFunct(col,row,false);
							grfx.FillRectangle(new SolidBrush(Color.White),col*18+1,row*18+1,17,17);
							pictButtons.Image=buttonsShadow;
							pictButtons.Refresh();
						}
						else{//button was already down
							buttonDown[col,row]=false;
							grfx.FillRectangle(new SolidBrush(Color.White),col*18+1,row*18+1,17,17);
							pictButtons.Image=buttonsShadow;
							pictButtons.Refresh();
						}
						break;
					case "Text":
						PlaySoundFunct(0,0,true);
						FormMessageText FormMT=new FormMessageText();
						FormMT.Text2.Text=Messages.RecdMsgBut.Text;
						FormMT.ShowDialog();
						ContrMessage2.LogMsg(Messages.RecdMsgBut.Text);
						break;
				}
			}
		}

		[DllImport("winmm.dll")]
    private static extern int PlaySound(string name, IntPtr hmod, int flags);

		private void PlaySoundFunct(int col, int row, bool isChime){
			if(isChime){
				PlaySound(@"Sounds\Chime.wav", IntPtr.Zero, (int)0x00020000 );//last is flag for file-based wav
			}
			else{
				string fileName;
				if(col==0) fileName="DrTo";
				else fileName="AsstTo";
				fileName+=(row+1).ToString()+".wav";
				PlaySound(@"Sounds\"+fileName, IntPtr.Zero, (int)0x00020000 );
			}
		}

		protected delegate void ProcessMessageDelegate(string text);

		private void button1_Click(object sender, System.EventArgs e) {
			
		}

		private void allNeutral(){
			ContrAppt2.Visible=false;
			ContrFamily2.Visible=false;
			ContrAccount2.Visible=false;
			ContrTreat2.Visible=false;
			ContrChart2.Visible=false;
			ContrMessage2.Visible=false;
			ContrDocs2.Visible=false;
			ContrStaff2.Visible=false;
			butAccount.BackColor=Color.FromArgb(224,224,224);
			labelAccount.BackColor=Color.FromArgb(224,224,224);
			butAppt.BackColor=Color.FromArgb(224,224,224);
			labelAppt.BackColor=Color.FromArgb(224,224,224);
			butChart.BackColor=Color.FromArgb(224,224,224);
			labelChart.BackColor=Color.FromArgb(224,224,224);
			butFamily.BackColor=Color.FromArgb(224,224,224);
			labelFam.BackColor=Color.FromArgb(224,224,224);
			butMessage.BackColor=Color.FromArgb(224,224,224);
			labelMsg.BackColor=Color.FromArgb(224,224,224);
			butTreat.BackColor=Color.FromArgb(224,224,224);
			labelTreat.BackColor=Color.FromArgb(224,224,224);
			butDoc.BackColor=Color.FromArgb(224,224,224);
			labelDocs.BackColor=Color.FromArgb(224,224,224);
			butStaff.BackColor=Color.FromArgb(224,224,224);
			labelStaff.BackColor=Color.FromArgb(224,224,224);
		}

		private void butAppt_Click(object sender, System.EventArgs e){
			allNeutral();
			ContrAppt2.Visible=true;
			butAppt.BackColor=Color.White;
			labelAppt.BackColor=Color.White;
			this.ActiveControl=this.ContrAppt2;
			ContrAppt2.ModuleSelected();
		}

		private void butAccount_Click(object sender, System.EventArgs e){
			allNeutral();
			ContrAccount2.Visible=true;
			butAccount.BackColor=Color.White;
			labelAccount.BackColor=Color.White;
			this.ActiveControl=this.ContrAccount2;
			ContrAccount2.ModuleSelected();
		}

		private void butFamily_Click(object sender, System.EventArgs e) {
			allNeutral();
			ContrFamily2.Visible=true;
			butFamily.BackColor=Color.White;
			labelFam.BackColor=Color.White;
			this.ActiveControl=this.ContrFamily2;
			ContrFamily2.ModuleSelected();
		}

		private void butTreat_Click(object sender, System.EventArgs e){
			allNeutral();
			ContrTreat2.Visible=true;
			butTreat.BackColor=Color.White;
			labelTreat.BackColor=Color.White;
			this.ActiveControl=this.ContrTreat2;
			ContrTreat2.ModuleSelected();
		}

		private void butChart_Click(object sender, System.EventArgs e){
			allNeutral();
			ContrChart2.Visible=true;
			butChart.BackColor=Color.White;
			labelChart.BackColor=Color.White;
			this.ActiveControl=this.ContrChart2;
			ContrChart2.ModuleSelected();
		}

		private void butDoc_Click(object sender, System.EventArgs e){
			allNeutral();
			ContrDocs2.Visible=true;
			butDoc.BackColor=Color.White;
			labelDocs.BackColor=Color.White;
			this.ActiveControl=this.ContrDocs2;
			ContrDocs2.ModuleSelected();
		}

		private void butMessage_Click(object sender, System.EventArgs e) {
			allNeutral();
			ContrMessage2.Visible=true;
			butMessage.BackColor=Color.White;
			labelMsg.BackColor=Color.White;
			ActiveControl=ContrMessage2;
			ContrMessage2.ModuleSelected();
		}

		private void butStaff_Click(object sender, System.EventArgs e) {
			allNeutral();
			ContrStaff2.Visible=true;
			butStaff.BackColor=Color.White;
			labelStaff.BackColor=Color.White;
			this.ActiveControl=this.ContrStaff2;
			ContrStaff2.ModuleSelected();
		}

		private void RefreshCurrentModule(){
			if(ContrAppt2.Visible)
				ContrAppt2.ModuleSelected();
			if(ContrFamily2.Visible)
				ContrFamily2.ModuleSelected();
			if(ContrAccount2.Visible)
				ContrAccount2.ModuleSelected();
			if(ContrTreat2.Visible)
				ContrTreat2.ModuleSelected();
			if(ContrChart2.Visible)
				ContrChart2.ModuleSelected();
			//ContrMessage2.Visible=false;
			//ContrTools2.Visible=false;
			if(ContrDocs2.Visible)
				ContrDocs2.ModuleSelected();
			//ContrStaff2.Visible=false;
		}

		private void pictButtons_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//MessageBox.Show((e.X/18).ToString()+","+(e.Y/18).ToString());
			Messages Messages=new Messages();
			Messages.ButtonsToSend=new MessageButtons();
			Messages.MessageToSend=new MessageInvalid();//because this value is tested when processing
			Graphics grfx=Graphics.FromImage(buttonsShadow);
			//Color buttonColor;
			int row=e.Y/18;
			int col=e.X/18;
			if(row>5) row=5;
			if(col>1) col=1;
			Messages.ButtonsToSend.Type="Button";
			Messages.ButtonsToSend.Text=" ";
			Messages.ButtonsToSend.Row=row;
			Messages.ButtonsToSend.Col=col;
			if(col==0 && !buttonDown[0,row]){//button in first col, currently not down
				buttonDown[0,row]=true;
				grfx.FillRectangle(new SolidBrush(Color.Red),col*18+1,row*18+1,17,17);
				pictButtons.Image=buttonsShadow;
				pictButtons.Refresh();
				Messages.ButtonsToSend.Pushed=true;
				Messages.SendButtons();
				PlaySoundFunct(col,row,false);
			}
			else if(col==1 && !buttonDown[1,row]){//button in second col, currently not down
				grfx.FillRectangle(new SolidBrush(Color.Red),col*18+1,row*18+1,17,17);
				pictButtons.Image=buttonsShadow;
				pictButtons.Refresh();
				Messages.ButtonsToSend.Pushed=true;
				Messages.SendButtons();
				PlaySoundFunct(col,row,false);
				grfx.FillRectangle(new SolidBrush(Color.White),col*18+1,row*18+1,17,17);
				pictButtons.Image=buttonsShadow;
				pictButtons.Refresh();
			}
			else{//button was already down
				buttonDown[col,row]=false;
				grfx.FillRectangle(new SolidBrush(Color.White),col*18+1,row*18+1,17,17);
				pictButtons.Image=buttonsShadow;
				pictButtons.Refresh();
				Messages.ButtonsToSend.Pushed=false;
				Messages.SendButtons();
			}
		}

		private void FormFreeDental_Closing(object sender, System.ComponentModel.CancelEventArgs e){
			Thread2.Abort();
			if(this.TcpListener2!=null){
				this.TcpListener2.Stop();  
			}
			Application.Exit();
		}

		private void timerTimeIndic_Tick(object sender, System.EventArgs e){
			if(!TimeBarIsDisabled){
				if(WindowState!=FormWindowState.Minimized
					&& ContrAppt2.Visible){
					ContrAppt2.TickRefresh();
				}
			}
      //BackupJobs.CheckForBackupJobs();      			
		}

		#region MenuEvents
		//File
		private void menuItemPrinter_Click(object sender, System.EventArgs e) {
			FormPrinterSetup FormPS=new FormPrinterSetup();
			FormPS.ShowDialog();
		}

		private void menuItemScanner_Click(object sender, System.EventArgs e) {
			FormScannerSetup FormSS=new FormScannerSetup();
			FormSS.ShowDialog();		
		}

		private void menuItemConfig_Click(object sender, System.EventArgs e) {
			FormConfig FormCfg = new FormConfig();
			FormCfg.ShowDialog();	
		}

		private void menuItemExit_Click(object sender, System.EventArgs e) {
			Thread2.Abort();
			if(this.TcpListener2!=null){
				this.TcpListener2.Stop();  
			}
			Application.Exit();
		}

		//FormBackupJobsSelect FormBJS=new FormBackupJobsSelect();
		//FormBJS.ShowDialog();	

		//Setup
		private void menuItemAutoCodes_Click(object sender, System.EventArgs e) {
			FormAutoCode FormAC=new FormAutoCode();
			FormAC.ShowDialog();		
		}

		private void menuItemProcedureButtons_Click(object sender, System.EventArgs e) {
			FormProcButtons FormPB=new FormProcButtons();
			FormPB.ShowDialog();	
		}

		private void menuItemDefinitions_Click(object sender, System.EventArgs e) {
			FormDefinitions formDefinitions2 = new FormDefinitions();
			formDefinitions2.ShowDialog();		
		}

		private void menuItemInsCats_Click(object sender, System.EventArgs e) {
			FormInsCatsSetup FormE=new FormInsCatsSetup();
			FormE.ShowDialog();	
		}

		private void menuItemPermissions_Click(object sender, System.EventArgs e) {
			FormPermissionsManage FormPM=new FormPermissionsManage(); 
			FormPM.ShowDialog();
		}

		private void menuItemLinks_Click(object sender, System.EventArgs e) {
			FormProgramLinks FormPL=new FormProgramLinks();
			FormPL.ShowDialog();	
		}
		
		//Setup-Schedules
		private void menuItemPracDef_Click(object sender, System.EventArgs e) {
			FormSchedDefault FormSD=new FormSchedDefault();
			FormSD.ShowDialog();		
		}

		private void menuItemPracSched_Click(object sender, System.EventArgs e) {
			FormSchedPractice FormSP=new FormSchedPractice();
			FormSP.ShowDialog(); 		
		}

		//Setup-Preferences
		private void menuItemDataPath_Click(object sender, System.EventArgs e) {
			FormPath FormP=new FormPath();
			FormP.ShowDialog();	
		}

		private void menuItemPractice_Click(object sender, System.EventArgs e) {
			FormPractice FormPr=new FormPractice();
			FormPr.ShowDialog();		
		}

		private void menuItemRecall_Click(object sender, System.EventArgs e) {
			FormRecallSetup FormRS=new FormRecallSetup();
			FormRS.ShowDialog();	
		}

		//Lists
		private void menuItemEmployees_Click(object sender, System.EventArgs e) {
			FormEmployee FormEmp=new FormEmployee();
			FormEmp.ShowDialog();		
		}

		private void menuItemInsTemplates_Click(object sender, System.EventArgs e) {
			FormInsTemplates FormInsTemplates = new FormInsTemplates();
			FormInsTemplates.ViewOnly=true;
			FormInsTemplates.ShowDialog();
			//FillPlanData();
		}

		private void menuItemProviders_Click(object sender, System.EventArgs e) {
			FormProviderSelect FormPS=new FormProviderSelect();
			FormPS.ShowDialog();		
		}

		private void menuItemPrescriptions_Click(object sender, System.EventArgs e) {
			FormRxSetup FormRxSetup2=new FormRxSetup();
			FormRxSetup2.ShowDialog();		
		}

		private void menuItemReferrals_Click(object sender, System.EventArgs e) {
			FormReferralSelect FormReferralSelect2=new FormReferralSelect();
			FormReferralSelect2.ViewOnly=true;  
			FormReferralSelect2.ShowDialog();		
		}

		private void menuItemZipCodes_Click(object sender, System.EventArgs e) {
			FormZipCodes FormZ=new FormZipCodes();
			FormZ.ShowDialog();
		}

		//Lists-ProcedureCodes
		private void menuItemEditCode_Click(object sender, System.EventArgs e) {
			FormProcedures FormProcedures = new FormProcedures();
			FormProcedures.Mode=FormProcMode.Edit;
			if(!UserPermissions.CheckUserPassword("Procedure Code Edit")){
				MessageBox.Show(Lan.g(this,"You do not have permission for this feature."));
				return;
			}
			FormProcedures.ShowDialog();
			SecurityLogs.MakeLogEntry("Procedure Code Edit","Proc Codes Edited");
		}

		private void menuItemViewCode_Click(object sender, System.EventArgs e) {
			FormProcedures FormProcedures = new FormProcedures();
			FormProcedures.Mode=FormProcMode.View;
			FormProcedures.ShowDialog();	
		}

		//Reports
		private void menuItemUserQuery_Click(object sender, System.EventArgs e) {
			FormQuery FormQuery2=new FormQuery();
			FormQuery2.ShowDialog();	
		}

		//Reports-Daily
		private void menuItemRpAdj_Click(object sender, System.EventArgs e) {
			FormRpAdjSheet FormAdjSheet=new FormRpAdjSheet();
			FormAdjSheet.ShowDialog();
		}

		private void menuItemRpDepSlip_Click(object sender, System.EventArgs e) {
			FormRpDepositSlip FormDS=new FormRpDepositSlip();
			FormDS.ShowDialog();
		}

		private void menuItemRpPay_Click(object sender, System.EventArgs e) {
			FormRpPaySheet FormPaySheet=new FormRpPaySheet();
			FormPaySheet.ShowDialog();
		}
		
		private void menuItemRpProc_Click(object sender, System.EventArgs e) {
			FormRpProcSheet FormProcSheet=new FormRpProcSheet();
			FormProcSheet.ShowDialog();	
		}

		//Reports-Monthly
		private void menuItemRpAging_Click(object sender, System.EventArgs e) {
			FormRpAging FormA=new FormRpAging();
			FormA.ShowDialog();
		}

		private void menuItemRpClaimsNotSent_Click(object sender, System.EventArgs e) {
			FormRpClaimNotSent FormClaim=new FormRpClaimNotSent();
			FormClaim.ShowDialog();
		}

		private void menuItemRpFinanceCharge_Click(object sender, System.EventArgs e) {
			FormRpFinanceCharge FormRpFinance=new FormRpFinanceCharge();
			FormRpFinance.ShowDialog();
		}

		private void menuItemRpOutInsClaims_Click(object sender, System.EventArgs e) {
			FormRpOutInsClaims FormOut=new FormRpOutInsClaims();
			FormOut.ShowDialog();	
		}

		private void menuItemRpProcNoBilled_Click(object sender, System.EventArgs e) {
			FormRpProcNotBilledIns FormProc=new FormRpProcNotBilledIns();
			FormProc.ShowDialog();
		}

		private void menuItemRpProduction_Click(object sender, System.EventArgs e) {
			FormRpProduction FormProduct=new FormRpProduction();
			FormProduct.ShowDialog();
		}

		//Reports-Lists
		private void menuItemInsCo_Click(object sender, System.EventArgs e) {
			FormRpInsCo FormInsCo=new FormRpInsCo();
			FormInsCo.ShowDialog();			
		}

		private void menuItemPatList_Click(object sender, System.EventArgs e) {
			FormRpPatients FormPatients=new FormRpPatients();
			FormPatients.ShowDialog();				
		}

		private void menuItemRxs_Click(object sender, System.EventArgs e) {
			FormRpPrescriptions FormPrescript=new FormRpPrescriptions();
			FormPrescript.ShowDialog();
		}

		private void menuItemProcCodes_Click(object sender, System.EventArgs e) {
			FormRpProcCodes FormProcCodes=new FormRpProcCodes();
			FormProcCodes.ShowDialog();		
		}

		private void menuItemRefs_Click(object sender, System.EventArgs e) {
			FormRpReferrals FormReferral=new FormRpReferrals();
			FormReferral.ShowDialog();		
		}

		//Tools
		private void menuItemPrintScreen_Click(object sender, System.EventArgs e) {
			FormPrntScrn FormPS=new FormPrntScrn();
			FormPS.ShowDialog();
		}

		private void menuItemCheckDatabase_Click(object sender, System.EventArgs e) {
			FormCheckDatabase FormCDB=new FormCheckDatabase();
			FormCDB.ShowDialog();
		}

		private void menuItemTranslation_Click(object sender, System.EventArgs e) {
			FormTranslationCat FormTC=new FormTranslationCat();
			FormTC.ShowDialog();
			if(FormTC.DialogResult==DialogResult.Cancel){

			}
		}

		private void menuTelephone_Click(object sender, System.EventArgs e) {
			FormTelephone FormT=new FormTelephone();
			FormT.ShowDialog();
		}

		private void menuItemAging_Click(object sender, System.EventArgs e) {
			FormAging FormAge=new FormAging();
			FormAge.ShowDialog();
		}

		private void menuItemFinanceCharge_Click(object sender, System.EventArgs e) {
			FormFinanceCharges FormFC=new FormFinanceCharges();
			FormFC.ShowDialog();
		}

		private void menuItemBilling_Click(object sender, System.EventArgs e) {
			FormBillingOptions FormBO=new FormBillingOptions();
			FormBO.ShowDialog();
		}

		private void menuItemClaims_Click(object sender, System.EventArgs e) {
			int	oldPatNum=Patients.Cur.PatNum;
			FormClaimsSend FormCS=new FormClaimsSend();
			FormCS.ShowDialog();
			Patients.Cur.PatNum=oldPatNum;
			RefreshCurrentModule();
		}

		//Help
		private void menuItemAbout_Click(object sender, System.EventArgs e) {
			FormAbout FormA = new FormAbout();
			FormA.ShowDialog();
		}

		private void menuItemHelp_Click(object sender, System.EventArgs e) {
			try{
				Process.Start(@"http://www.open-dent.com/manual/toc.html");
			}
			catch{
				MessageBox.Show("Could not find file.");
			}
		}

		private void menuItemHelpIndex_Click(object sender, System.EventArgs e) {
			try{
				Process.Start(@"http://www.open-dent.com/manual/alphabetical.html");
			}
			catch{
				MessageBox.Show("Could not find file.");
			}
		}

		

		

		

		

		private void menuItemDaily_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e) {
			//MessageBox.Show(e.Bounds.ToString());
			e.Graphics.DrawString("Dailyyyy",new Font("Microsoft Sans Serif",8),Brushes.Black,e.Bounds.X,e.Bounds.Y);
		}

		

		

		private void menuItemDaily_Click(object sender, System.EventArgs e) {
		
		}

		#endregion

		

	

		

		

		

	

		

		

	}
}
