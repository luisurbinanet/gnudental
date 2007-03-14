/*=============================================================================================================
Open Dental is a dental practice management program.
Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  http://www.docsparks.com

This program is free software; you can redistribute it and/or modify it under the terms of the
GNU General Public License as published by the Free Software Foundation; either version 2 of the License,
or (at your option) any later version.

This program is distributed in the hope that it will be useful, but without any warranty. See the GNU General Public License
for more details, available at http://www.opensource.org/licenses/gpl-license.php

Any changes to this program must follow the guidelines of the GPL license if a modified version is to be
redistributed.
===============================================================================================================*/
//For now, all screens are assumed to have available 990x734.  That would be a screen resolution of 1024x768 with a single width taskbar docked to any one of the four sides of the screen.  Later, the forms will be dynamic to adjust automatically down to 800x600 screens.

//The 7 main controls are slightly narrower due to menu bar on left of 42(new51). Max size 948(new939)x708

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
	///<summary></summary>
	public class FormOpenDental : System.Windows.Forms.Form{
		private System.Windows.Forms.ImageList imageList1;
		private OpenDental.ContrAppt ContrAppt2;
		private System.Windows.Forms.Button button1;
		private System.ComponentModel.IContainer components;
		private OpenDental.ContrFamily ContrFamily2;
		private OpenDental.ContrTreat ContrTreat2;
		private OpenDental.ContrChart ContrChart2;
		private OpenDental.ContrAccount ContrAccount2;
		private Thread Thread2;
		//private OpenDental.ContrStaff ContrStaff2;
		private TcpListener TcpListener2;
		private System.Windows.Forms.PictureBox pictButtons;
		private System.Windows.Forms.ImageList imageList2x6;
		private bool[,] buttonDown=new bool[2,6];
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
		private System.Windows.Forms.MenuItem menuItemRpFinanceCharge;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItemUserQuery;
		private System.Windows.Forms.MenuItem menuItemList;
		private System.Windows.Forms.MenuItem menuItemTranslation;
		private System.Windows.Forms.MenuItem menuItemPatList;
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
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItemPermissions;
		private System.Windows.Forms.MenuItem menuItemCheckDatabase;
		private System.Windows.Forms.MenuItem menuItemProcedureButtons;
		private System.Windows.Forms.MenuItem menuItemZipCodes;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuTelephone;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItemClaims;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private OpenDental.ContrDocs ContrDocs2;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItemHelpIndex;
		private System.Windows.Forms.MenuItem menuItemClaimForms;
		private System.Windows.Forms.MenuItem menuItemContacts;
		private System.Windows.Forms.MenuItem menuItemMedications;
		private OpenDental.OutlookBar myOutlookBar;
		private System.Windows.Forms.ImageList imageList32;
		private System.Windows.Forms.MenuItem menuItemApptViews;
		private System.Windows.Forms.MenuItem menuItemPaymentPlans;
		private System.Windows.Forms.MenuItem menuItemRpCapitation;
		private System.Windows.Forms.MenuItem menuItemPracticeWebReports;
		private System.Windows.Forms.MenuItem menuItemComputers;
		private System.Windows.Forms.MenuItem menuItemEmployers;
		private OpenDental.ContrStaff ContrStaff2;
		private System.Windows.Forms.MenuItem menuItemEasy;
		private System.Windows.Forms.MenuItem menuItemCarriers;
		private System.Windows.Forms.MenuItem menuItemSchools;
		private System.Windows.Forms.MenuItem menuItemCounties;
		private System.Windows.Forms.MenuItem menuItemScreening;
		private System.Windows.Forms.MenuItem menuItemPublicHealth;
		private System.Windows.Forms.MenuItem menuItemPHSep;
		private System.Windows.Forms.MenuItem menuItemPHRawScreen;
		private System.Windows.Forms.MenuItem menuItemPHRawPop;
		private System.Windows.Forms.MenuItem menuItemPHScreen;
		private System.Windows.Forms.MenuItem menuItemInsCarriers;
		private System.Windows.Forms.MenuItem menuItemEmail;
		private System.Windows.Forms.MenuItem menuItemHelpContents;
		private System.Windows.Forms.MenuItem menuItemHelp;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItemRpProdInc;
		private System.Windows.Forms.MenuItem menuAppointments;
		private Image buttonsShadow;

		///<summary></summary>
		public FormOpenDental(){
			InitializeComponent();
		}

		///<summary></summary>
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
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.pictButtons = new System.Windows.Forms.PictureBox();
			this.ContrTreat2 = new OpenDental.ContrTreat();
			this.ContrChart2 = new OpenDental.ContrChart();
			this.ContrDocs2 = new OpenDental.ContrDocs();
			this.ContrAppt2 = new OpenDental.ContrAppt();
			this.ContrAccount2 = new OpenDental.ContrAccount();
			this.button1 = new System.Windows.Forms.Button();
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
			this.menuItemApptViews = new System.Windows.Forms.MenuItem();
			this.menuItemAutoCodes = new System.Windows.Forms.MenuItem();
			this.menuItemClaimForms = new System.Windows.Forms.MenuItem();
			this.menuItemComputers = new System.Windows.Forms.MenuItem();
			this.menuItemDataPath = new System.Windows.Forms.MenuItem();
			this.menuItemDefinitions = new System.Windows.Forms.MenuItem();
			this.menuItemEasy = new System.Windows.Forms.MenuItem();
			this.menuItemEmail = new System.Windows.Forms.MenuItem();
			this.menuItemInsCats = new System.Windows.Forms.MenuItem();
			this.menuItemPermissions = new System.Windows.Forms.MenuItem();
			this.menuItemPractice = new System.Windows.Forms.MenuItem();
			this.menuItemProcedureButtons = new System.Windows.Forms.MenuItem();
			this.menuItemLinks = new System.Windows.Forms.MenuItem();
			this.menuItemRecall = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItemSched = new System.Windows.Forms.MenuItem();
			this.menuItemPracDef = new System.Windows.Forms.MenuItem();
			this.menuItemPracSched = new System.Windows.Forms.MenuItem();
			this.menuItemLists = new System.Windows.Forms.MenuItem();
			this.menuItemContacts = new System.Windows.Forms.MenuItem();
			this.menuItemCounties = new System.Windows.Forms.MenuItem();
			this.menuItemEmployees = new System.Windows.Forms.MenuItem();
			this.menuItemEmployers = new System.Windows.Forms.MenuItem();
			this.menuItemCarriers = new System.Windows.Forms.MenuItem();
			this.menuItemInsTemplates = new System.Windows.Forms.MenuItem();
			this.menuItemMedications = new System.Windows.Forms.MenuItem();
			this.menuItemProviders = new System.Windows.Forms.MenuItem();
			this.menuItemPrescriptions = new System.Windows.Forms.MenuItem();
			this.menuItemReferrals = new System.Windows.Forms.MenuItem();
			this.menuItemSchools = new System.Windows.Forms.MenuItem();
			this.menuItemZipCodes = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItemProcCode = new System.Windows.Forms.MenuItem();
			this.menuItemEditCode = new System.Windows.Forms.MenuItem();
			this.menuItemViewCode = new System.Windows.Forms.MenuItem();
			this.menuItemReports = new System.Windows.Forms.MenuItem();
			this.menuItemUserQuery = new System.Windows.Forms.MenuItem();
			this.menuItemPracticeWebReports = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItemRpProdInc = new System.Windows.Forms.MenuItem();
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
			this.menuItemRpCapitation = new System.Windows.Forms.MenuItem();
			this.menuItemRpFinanceCharge = new System.Windows.Forms.MenuItem();
			this.menuItemRpOutInsClaims = new System.Windows.Forms.MenuItem();
			this.menuItemRpProcNoBilled = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItemList = new System.Windows.Forms.MenuItem();
			this.menuAppointments = new System.Windows.Forms.MenuItem();
			this.menuItemInsCarriers = new System.Windows.Forms.MenuItem();
			this.menuItemPatList = new System.Windows.Forms.MenuItem();
			this.menuItemRxs = new System.Windows.Forms.MenuItem();
			this.menuItemProcCodes = new System.Windows.Forms.MenuItem();
			this.menuItemRefs = new System.Windows.Forms.MenuItem();
			this.menuItemPHSep = new System.Windows.Forms.MenuItem();
			this.menuItemPublicHealth = new System.Windows.Forms.MenuItem();
			this.menuItemPHRawScreen = new System.Windows.Forms.MenuItem();
			this.menuItemPHRawPop = new System.Windows.Forms.MenuItem();
			this.menuItemPHScreen = new System.Windows.Forms.MenuItem();
			this.menuItemTools = new System.Windows.Forms.MenuItem();
			this.menuItemPrintScreen = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemCheckDatabase = new System.Windows.Forms.MenuItem();
			this.menuItemTranslation = new System.Windows.Forms.MenuItem();
			this.menuTelephone = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItemPaymentPlans = new System.Windows.Forms.MenuItem();
			this.menuItemAging = new System.Windows.Forms.MenuItem();
			this.menuItemFinanceCharge = new System.Windows.Forms.MenuItem();
			this.menuItemBilling = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItemScreening = new System.Windows.Forms.MenuItem();
			this.menuItemClaims = new System.Windows.Forms.MenuItem();
			this.menuItemHelp = new System.Windows.Forms.MenuItem();
			this.menuItemHelpContents = new System.Windows.Forms.MenuItem();
			this.menuItemHelpIndex = new System.Windows.Forms.MenuItem();
			this.menuItemAbout = new System.Windows.Forms.MenuItem();
			this.myOutlookBar = new OpenDental.OutlookBar();
			this.imageList32 = new System.Windows.Forms.ImageList(this.components);
			this.ContrStaff2 = new OpenDental.ContrStaff();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(22, 22);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// pictButtons
			// 
			this.pictButtons.Image = ((System.Drawing.Image)(resources.GetObject("pictButtons.Image")));
			this.pictButtons.Location = new System.Drawing.Point(6, 476);
			this.pictButtons.Name = "pictButtons";
			this.pictButtons.Size = new System.Drawing.Size(37, 109);
			this.pictButtons.TabIndex = 17;
			this.pictButtons.TabStop = false;
			this.pictButtons.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictButtons_MouseDown);
			// 
			// ContrTreat2
			// 
			this.ContrTreat2.Location = new System.Drawing.Point(143, 4);
			this.ContrTreat2.Name = "ContrTreat2";
			this.ContrTreat2.Size = new System.Drawing.Size(880, 690);
			this.ContrTreat2.TabIndex = 5;
			// 
			// ContrChart2
			// 
			this.ContrChart2.DataValid = false;
			this.ContrChart2.Location = new System.Drawing.Point(149, 3);
			this.ContrChart2.Name = "ContrChart2";
			this.ContrChart2.Size = new System.Drawing.Size(880, 690);
			this.ContrChart2.TabIndex = 6;
			this.ContrChart2.Visible = false;
			// 
			// ContrDocs2
			// 
			this.ContrDocs2.Location = new System.Drawing.Point(146, 3);
			this.ContrDocs2.Name = "ContrDocs2";
			this.ContrDocs2.Size = new System.Drawing.Size(812, 678);
			this.ContrDocs2.TabIndex = 8;
			this.ContrDocs2.Visible = false;
			// 
			// ContrAppt2
			// 
			this.ContrAppt2.DockPadding.Left = 56;
			this.ContrAppt2.Location = new System.Drawing.Point(51, 1);
			this.ContrAppt2.Name = "ContrAppt2";
			this.ContrAppt2.Size = new System.Drawing.Size(880, 690);
			this.ContrAppt2.TabIndex = 9;
			// 
			// ContrAccount2
			// 
			this.ContrAccount2.Location = new System.Drawing.Point(145, 0);
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
			// ContrFamily2
			// 
			this.ContrFamily2.Location = new System.Drawing.Point(150, 1);
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
																																						 this.menuItemHelp});
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
																																										 this.menuItemApptViews,
																																										 this.menuItemAutoCodes,
																																										 this.menuItemClaimForms,
																																										 this.menuItemComputers,
																																										 this.menuItemDataPath,
																																										 this.menuItemDefinitions,
																																										 this.menuItemEasy,
																																										 this.menuItemEmail,
																																										 this.menuItemInsCats,
																																										 this.menuItemPermissions,
																																										 this.menuItemPractice,
																																										 this.menuItemProcedureButtons,
																																										 this.menuItemLinks,
																																										 this.menuItemRecall,
																																										 this.menuItem10,
																																										 this.menuItemSched,
																																										 this.menuItemPracDef,
																																										 this.menuItemPracSched});
			this.menuItemSettings.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuItemSettings.Text = "&Setup";
			// 
			// menuItemApptViews
			// 
			this.menuItemApptViews.Index = 0;
			this.menuItemApptViews.Text = "Appointment Views";
			this.menuItemApptViews.Click += new System.EventHandler(this.menuItemApptViews_Click);
			// 
			// menuItemAutoCodes
			// 
			this.menuItemAutoCodes.Index = 1;
			this.menuItemAutoCodes.Text = "Auto Codes";
			this.menuItemAutoCodes.Click += new System.EventHandler(this.menuItemAutoCodes_Click);
			// 
			// menuItemClaimForms
			// 
			this.menuItemClaimForms.Index = 2;
			this.menuItemClaimForms.Text = "Claim Forms";
			this.menuItemClaimForms.Click += new System.EventHandler(this.menuItemClaimForms_Click);
			// 
			// menuItemComputers
			// 
			this.menuItemComputers.Index = 3;
			this.menuItemComputers.Text = "Computers";
			this.menuItemComputers.Click += new System.EventHandler(this.menuItemComputers_Click);
			// 
			// menuItemDataPath
			// 
			this.menuItemDataPath.Index = 4;
			this.menuItemDataPath.Text = "Data Paths";
			this.menuItemDataPath.Click += new System.EventHandler(this.menuItemDataPath_Click);
			// 
			// menuItemDefinitions
			// 
			this.menuItemDefinitions.Index = 5;
			this.menuItemDefinitions.Text = "Definitions";
			this.menuItemDefinitions.Click += new System.EventHandler(this.menuItemDefinitions_Click);
			// 
			// menuItemEasy
			// 
			this.menuItemEasy.Index = 6;
			this.menuItemEasy.Text = "Easy Options";
			this.menuItemEasy.Click += new System.EventHandler(this.menuItemEasy_Click);
			// 
			// menuItemEmail
			// 
			this.menuItemEmail.Index = 7;
			this.menuItemEmail.Text = "E-mail";
			this.menuItemEmail.Click += new System.EventHandler(this.menuItemEmail_Click);
			// 
			// menuItemInsCats
			// 
			this.menuItemInsCats.Index = 8;
			this.menuItemInsCats.Text = "Insurance Categories";
			this.menuItemInsCats.Click += new System.EventHandler(this.menuItemInsCats_Click);
			// 
			// menuItemPermissions
			// 
			this.menuItemPermissions.Index = 9;
			this.menuItemPermissions.Text = "Permissions";
			this.menuItemPermissions.Click += new System.EventHandler(this.menuItemPermissions_Click);
			// 
			// menuItemPractice
			// 
			this.menuItemPractice.Index = 10;
			this.menuItemPractice.Text = "Practice";
			this.menuItemPractice.Click += new System.EventHandler(this.menuItemPractice_Click);
			// 
			// menuItemProcedureButtons
			// 
			this.menuItemProcedureButtons.Index = 11;
			this.menuItemProcedureButtons.Text = "Procedure Buttons";
			this.menuItemProcedureButtons.Click += new System.EventHandler(this.menuItemProcedureButtons_Click);
			// 
			// menuItemLinks
			// 
			this.menuItemLinks.Index = 12;
			this.menuItemLinks.Text = "Program Links";
			this.menuItemLinks.Click += new System.EventHandler(this.menuItemLinks_Click);
			// 
			// menuItemRecall
			// 
			this.menuItemRecall.Index = 13;
			this.menuItemRecall.Text = "Recall";
			this.menuItemRecall.Click += new System.EventHandler(this.menuItemRecall_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 14;
			this.menuItem10.Text = "-";
			// 
			// menuItemSched
			// 
			this.menuItemSched.Index = 15;
			this.menuItemSched.Text = "SCHEDULES";
			// 
			// menuItemPracDef
			// 
			this.menuItemPracDef.Index = 16;
			this.menuItemPracDef.Text = "   Practice Default";
			this.menuItemPracDef.Click += new System.EventHandler(this.menuItemPracDef_Click);
			// 
			// menuItemPracSched
			// 
			this.menuItemPracSched.Index = 17;
			this.menuItemPracSched.Text = "   Practice Schedule";
			this.menuItemPracSched.Click += new System.EventHandler(this.menuItemPracSched_Click);
			// 
			// menuItemLists
			// 
			this.menuItemLists.Index = 2;
			this.menuItemLists.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									this.menuItemContacts,
																																									this.menuItemCounties,
																																									this.menuItemEmployees,
																																									this.menuItemEmployers,
																																									this.menuItemCarriers,
																																									this.menuItemInsTemplates,
																																									this.menuItemMedications,
																																									this.menuItemProviders,
																																									this.menuItemPrescriptions,
																																									this.menuItemReferrals,
																																									this.menuItemSchools,
																																									this.menuItemZipCodes,
																																									this.menuItem5,
																																									this.menuItemProcCode,
																																									this.menuItemEditCode,
																																									this.menuItemViewCode});
			this.menuItemLists.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
			this.menuItemLists.Text = "&Lists";
			// 
			// menuItemContacts
			// 
			this.menuItemContacts.Index = 0;
			this.menuItemContacts.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftC;
			this.menuItemContacts.Text = "&Contacts";
			this.menuItemContacts.Click += new System.EventHandler(this.menuItemContacts_Click);
			// 
			// menuItemCounties
			// 
			this.menuItemCounties.Index = 1;
			this.menuItemCounties.Text = "Counties";
			this.menuItemCounties.Click += new System.EventHandler(this.menuItemCounties_Click);
			// 
			// menuItemEmployees
			// 
			this.menuItemEmployees.Index = 2;
			this.menuItemEmployees.Text = "&Employees";
			this.menuItemEmployees.Click += new System.EventHandler(this.menuItemEmployees_Click);
			// 
			// menuItemEmployers
			// 
			this.menuItemEmployers.Index = 3;
			this.menuItemEmployers.Text = "Employers";
			this.menuItemEmployers.Click += new System.EventHandler(this.menuItemEmployers_Click);
			// 
			// menuItemCarriers
			// 
			this.menuItemCarriers.Index = 4;
			this.menuItemCarriers.Text = "Insurance Carriers";
			this.menuItemCarriers.Click += new System.EventHandler(this.menuItemCarriers_Click);
			// 
			// menuItemInsTemplates
			// 
			this.menuItemInsTemplates.Index = 5;
			this.menuItemInsTemplates.Text = "&Insurance Plans";
			this.menuItemInsTemplates.Click += new System.EventHandler(this.menuItemInsTemplates_Click);
			// 
			// menuItemMedications
			// 
			this.menuItemMedications.Index = 6;
			this.menuItemMedications.Text = "&Medications";
			this.menuItemMedications.Click += new System.EventHandler(this.menuItemMedications_Click);
			// 
			// menuItemProviders
			// 
			this.menuItemProviders.Index = 7;
			this.menuItemProviders.Text = "&Providers";
			this.menuItemProviders.Click += new System.EventHandler(this.menuItemProviders_Click);
			// 
			// menuItemPrescriptions
			// 
			this.menuItemPrescriptions.Index = 8;
			this.menuItemPrescriptions.Text = "Pre&scriptions";
			this.menuItemPrescriptions.Click += new System.EventHandler(this.menuItemPrescriptions_Click);
			// 
			// menuItemReferrals
			// 
			this.menuItemReferrals.Index = 9;
			this.menuItemReferrals.Text = "&Referrals";
			this.menuItemReferrals.Click += new System.EventHandler(this.menuItemReferrals_Click);
			// 
			// menuItemSchools
			// 
			this.menuItemSchools.Index = 10;
			this.menuItemSchools.Text = "Schools";
			this.menuItemSchools.Click += new System.EventHandler(this.menuItemSchools_Click);
			// 
			// menuItemZipCodes
			// 
			this.menuItemZipCodes.Index = 11;
			this.menuItemZipCodes.Text = "&Zip Codes";
			this.menuItemZipCodes.Click += new System.EventHandler(this.menuItemZipCodes_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 12;
			this.menuItem5.Text = "-";
			// 
			// menuItemProcCode
			// 
			this.menuItemProcCode.Index = 13;
			this.menuItemProcCode.Text = "PROCEDURE CODES";
			// 
			// menuItemEditCode
			// 
			this.menuItemEditCode.Index = 14;
			this.menuItemEditCode.Text = "   E&dit Codes";
			this.menuItemEditCode.Click += new System.EventHandler(this.menuItemEditCode_Click);
			// 
			// menuItemViewCode
			// 
			this.menuItemViewCode.Index = 15;
			this.menuItemViewCode.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftF;
			this.menuItemViewCode.Text = "   View &Fees";
			this.menuItemViewCode.Click += new System.EventHandler(this.menuItemViewCode_Click);
			// 
			// menuItemReports
			// 
			this.menuItemReports.Index = 3;
			this.menuItemReports.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.menuItemUserQuery,
																																										this.menuItemPracticeWebReports,
																																										this.menuItem12,
																																										this.menuItemRpProdInc,
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
																																										this.menuItemRpCapitation,
																																										this.menuItemRpFinanceCharge,
																																										this.menuItemRpOutInsClaims,
																																										this.menuItemRpProcNoBilled,
																																										this.menuItem8,
																																										this.menuItemList,
																																										this.menuAppointments,
																																										this.menuItemInsCarriers,
																																										this.menuItemPatList,
																																										this.menuItemRxs,
																																										this.menuItemProcCodes,
																																										this.menuItemRefs,
																																										this.menuItemPHSep,
																																										this.menuItemPublicHealth,
																																										this.menuItemPHRawScreen,
																																										this.menuItemPHRawPop,
																																										this.menuItemPHScreen});
			this.menuItemReports.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
			this.menuItemReports.Text = "&Reports";
			// 
			// menuItemUserQuery
			// 
			this.menuItemUserQuery.Index = 0;
			this.menuItemUserQuery.Text = "&User Query";
			this.menuItemUserQuery.Click += new System.EventHandler(this.menuItemUserQuery_Click);
			// 
			// menuItemPracticeWebReports
			// 
			this.menuItemPracticeWebReports.Index = 1;
			this.menuItemPracticeWebReports.Text = "Other Reports";
			this.menuItemPracticeWebReports.Click += new System.EventHandler(this.menuItemPracticeWebReports_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 2;
			this.menuItem12.Text = "-";
			// 
			// menuItemRpProdInc
			// 
			this.menuItemRpProdInc.Index = 3;
			this.menuItemRpProdInc.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftP;
			this.menuItemRpProdInc.Text = "&Production and Income";
			this.menuItemRpProdInc.Click += new System.EventHandler(this.menuItemRpProdInc_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.Text = "-";
			// 
			// menuItemDaily
			// 
			this.menuItemDaily.Index = 5;
			this.menuItemDaily.Text = "DAILY";
			// 
			// menuItemRpAdj
			// 
			this.menuItemRpAdj.Index = 6;
			this.menuItemRpAdj.Text = "   &Adjustments";
			this.menuItemRpAdj.Click += new System.EventHandler(this.menuItemRpAdj_Click);
			// 
			// menuItemRpDepSlip
			// 
			this.menuItemRpDepSlip.Index = 7;
			this.menuItemRpDepSlip.Text = "   &Deposit Slip";
			this.menuItemRpDepSlip.Click += new System.EventHandler(this.menuItemRpDepSlip_Click);
			// 
			// menuItemRpPay
			// 
			this.menuItemRpPay.Index = 8;
			this.menuItemRpPay.Text = "   Pa&yments";
			this.menuItemRpPay.Click += new System.EventHandler(this.menuItemRpPay_Click);
			// 
			// menuItemRpProc
			// 
			this.menuItemRpProc.Index = 9;
			this.menuItemRpProc.Text = "   P&rocedures";
			this.menuItemRpProc.Click += new System.EventHandler(this.menuItemRpProc_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 10;
			this.menuItem3.Text = "-";
			// 
			// menuItemMonthly
			// 
			this.menuItemMonthly.Index = 11;
			this.menuItemMonthly.Text = "MONTHLY";
			// 
			// menuItemRpAging
			// 
			this.menuItemRpAging.Index = 12;
			this.menuItemRpAging.Text = "   Aging Report";
			this.menuItemRpAging.Click += new System.EventHandler(this.menuItemRpAging_Click);
			// 
			// menuItemRpClaimsNotSent
			// 
			this.menuItemRpClaimsNotSent.Index = 13;
			this.menuItemRpClaimsNotSent.Text = "   Claims Not Sent";
			this.menuItemRpClaimsNotSent.Click += new System.EventHandler(this.menuItemRpClaimsNotSent_Click);
			// 
			// menuItemRpCapitation
			// 
			this.menuItemRpCapitation.Index = 14;
			this.menuItemRpCapitation.Text = "   Capitation Utilization";
			this.menuItemRpCapitation.Click += new System.EventHandler(this.menuItemRpCapitation_Click);
			// 
			// menuItemRpFinanceCharge
			// 
			this.menuItemRpFinanceCharge.Index = 15;
			this.menuItemRpFinanceCharge.Text = "   Finance Charge Report";
			this.menuItemRpFinanceCharge.Click += new System.EventHandler(this.menuItemRpFinanceCharge_Click);
			// 
			// menuItemRpOutInsClaims
			// 
			this.menuItemRpOutInsClaims.Index = 16;
			this.menuItemRpOutInsClaims.Text = "   Outstanding Insurance Claims";
			this.menuItemRpOutInsClaims.Click += new System.EventHandler(this.menuItemRpOutInsClaims_Click);
			// 
			// menuItemRpProcNoBilled
			// 
			this.menuItemRpProcNoBilled.Index = 17;
			this.menuItemRpProcNoBilled.Text = "   Procedures Not Billed To Insurance";
			this.menuItemRpProcNoBilled.Click += new System.EventHandler(this.menuItemRpProcNoBilled_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 18;
			this.menuItem8.Text = "-";
			// 
			// menuItemList
			// 
			this.menuItemList.Index = 19;
			this.menuItemList.Text = "LISTS";
			// 
			// menuAppointments
			// 
			this.menuAppointments.Index = 20;
			this.menuAppointments.Text = "   Appointments";
			this.menuAppointments.Click += new System.EventHandler(this.menuAppointments_Click);
			// 
			// menuItemInsCarriers
			// 
			this.menuItemInsCarriers.Index = 21;
			this.menuItemInsCarriers.Text = "   Insurance Plans";
			this.menuItemInsCarriers.Click += new System.EventHandler(this.menuItemInsCarriers_Click);
			// 
			// menuItemPatList
			// 
			this.menuItemPatList.Index = 22;
			this.menuItemPatList.Text = "   Patients";
			this.menuItemPatList.Click += new System.EventHandler(this.menuItemPatList_Click);
			// 
			// menuItemRxs
			// 
			this.menuItemRxs.Index = 23;
			this.menuItemRxs.Text = "   Prescriptions";
			this.menuItemRxs.Click += new System.EventHandler(this.menuItemRxs_Click);
			// 
			// menuItemProcCodes
			// 
			this.menuItemProcCodes.Index = 24;
			this.menuItemProcCodes.Text = "   Procedure Codes";
			this.menuItemProcCodes.Click += new System.EventHandler(this.menuItemProcCodes_Click);
			// 
			// menuItemRefs
			// 
			this.menuItemRefs.Index = 25;
			this.menuItemRefs.Text = "   Referrals";
			this.menuItemRefs.Click += new System.EventHandler(this.menuItemRefs_Click);
			// 
			// menuItemPHSep
			// 
			this.menuItemPHSep.Index = 26;
			this.menuItemPHSep.Text = "-";
			// 
			// menuItemPublicHealth
			// 
			this.menuItemPublicHealth.Index = 27;
			this.menuItemPublicHealth.Text = "PUBLIC HEALTH";
			// 
			// menuItemPHRawScreen
			// 
			this.menuItemPHRawScreen.Index = 28;
			this.menuItemPHRawScreen.Text = "   Raw Screening Data";
			this.menuItemPHRawScreen.Click += new System.EventHandler(this.menuItemPHRawScreen_Click);
			// 
			// menuItemPHRawPop
			// 
			this.menuItemPHRawPop.Index = 29;
			this.menuItemPHRawPop.Text = "   Raw Population Data";
			this.menuItemPHRawPop.Click += new System.EventHandler(this.menuItemPHRawPop_Click);
			// 
			// menuItemPHScreen
			// 
			this.menuItemPHScreen.Index = 30;
			this.menuItemPHScreen.Text = "   Screening Report";
			this.menuItemPHScreen.Click += new System.EventHandler(this.menuItemPHScreen_Click);
			// 
			// menuItemTools
			// 
			this.menuItemTools.Index = 4;
			this.menuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									this.menuItemPrintScreen,
																																									this.menuItem1,
																																									this.menuItem9,
																																									this.menuItemPaymentPlans,
																																									this.menuItemAging,
																																									this.menuItemFinanceCharge,
																																									this.menuItemBilling,
																																									this.menuItem11,
																																									this.menuItemScreening,
																																									this.menuItemClaims});
			this.menuItemTools.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
			this.menuItemTools.Text = "&Tools";
			// 
			// menuItemPrintScreen
			// 
			this.menuItemPrintScreen.Index = 0;
			this.menuItemPrintScreen.Text = "&Print Screen Tool";
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
			// menuItemPaymentPlans
			// 
			this.menuItemPaymentPlans.Index = 3;
			this.menuItemPaymentPlans.Text = "&Update Payment Plans";
			this.menuItemPaymentPlans.Click += new System.EventHandler(this.menuItemPaymentPlans_Click);
			// 
			// menuItemAging
			// 
			this.menuItemAging.Index = 4;
			this.menuItemAging.Text = "Calculate &Aging";
			this.menuItemAging.Click += new System.EventHandler(this.menuItemAging_Click);
			// 
			// menuItemFinanceCharge
			// 
			this.menuItemFinanceCharge.Index = 5;
			this.menuItemFinanceCharge.Text = "Run &Finance Charges";
			this.menuItemFinanceCharge.Click += new System.EventHandler(this.menuItemFinanceCharge_Click);
			// 
			// menuItemBilling
			// 
			this.menuItemBilling.Index = 6;
			this.menuItemBilling.Text = "&Billing";
			this.menuItemBilling.Click += new System.EventHandler(this.menuItemBilling_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 7;
			this.menuItem11.Text = "-";
			// 
			// menuItemScreening
			// 
			this.menuItemScreening.Index = 8;
			this.menuItemScreening.Text = "Public Health Screening";
			this.menuItemScreening.Click += new System.EventHandler(this.menuItemScreening_Click);
			// 
			// menuItemClaims
			// 
			this.menuItemClaims.Index = 9;
			this.menuItemClaims.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuItemClaims.Text = "Send &Claims";
			this.menuItemClaims.Click += new System.EventHandler(this.menuItemClaims_Click);
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.Index = 5;
			this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.menuItemHelpContents,
																																								 this.menuItemHelpIndex,
																																								 this.menuItemAbout});
			this.menuItemHelp.Text = "Help";
			// 
			// menuItemHelpContents
			// 
			this.menuItemHelpContents.Index = 0;
			this.menuItemHelpContents.Text = "Online Help - Contents";
			this.menuItemHelpContents.Click += new System.EventHandler(this.menuItemHelpContents_Click);
			// 
			// menuItemHelpIndex
			// 
			this.menuItemHelpIndex.Index = 1;
			this.menuItemHelpIndex.Shortcut = System.Windows.Forms.Shortcut.ShiftF1;
			this.menuItemHelpIndex.Text = "Online Help - Index";
			this.menuItemHelpIndex.Click += new System.EventHandler(this.menuItemHelpIndex_Click);
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Index = 2;
			this.menuItemAbout.Text = "&About";
			this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
			// 
			// myOutlookBar
			// 
			this.myOutlookBar.Dock = System.Windows.Forms.DockStyle.Left;
			this.myOutlookBar.ImageList = this.imageList32;
			this.myOutlookBar.Location = new System.Drawing.Point(0, 0);
			this.myOutlookBar.Name = "myOutlookBar";
			this.myOutlookBar.Size = new System.Drawing.Size(51, 690);
			this.myOutlookBar.TabIndex = 18;
			this.myOutlookBar.Text = "outlookBar1";
			this.myOutlookBar.ButtonClicked += new OpenDental.ButtonClickedEventHandler(this.myOutlookBar_ButtonClicked);
			// 
			// imageList32
			// 
			this.imageList32.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
			this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ContrStaff2
			// 
			this.ContrStaff2.Location = new System.Drawing.Point(177, 0);
			this.ContrStaff2.Name = "ContrStaff2";
			this.ContrStaff2.Size = new System.Drawing.Size(732, 548);
			this.ContrStaff2.TabIndex = 19;
			// 
			// FormOpenDental
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(880, 690);
			this.Controls.Add(this.pictButtons);
			this.Controls.Add(this.myOutlookBar);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.ContrAppt2);
			this.Controls.Add(this.ContrStaff2);
			this.Controls.Add(this.ContrAccount2);
			this.Controls.Add(this.ContrFamily2);
			this.Controls.Add(this.ContrDocs2);
			this.Controls.Add(this.ContrChart2);
			this.Controls.Add(this.ContrTreat2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Menu = this.mainMenu;
			this.Name = "FormOpenDental";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Text = "Open Dental";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormOpenDental_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormFreeDental_Closing);
			this.Load += new System.EventHandler(this.FormOpenDental_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormFreeDental_Layout);
			this.ResumeLayout(false);

		}
		#endregion
	
		[STAThread]
		static void Main() {
			if(Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length>1){
				//if an instance of the program is already running.
				return;//then don't open another
			}
			Application.EnableVisualStyles();//changes appearance to XP
			Application.DoEvents();//workaround for a known MS bug in the line above
			Application.Run(new FormOpenDental());
		}

		private void FormOpenDental_Load(object sender, System.EventArgs e){
			allNeutral();
			ExitApplicationNow.WantsToExit+=new System.EventHandler(ExitApplicationNow_WantsToExit);
			FormConfig FormCfg=new FormConfig();
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
			Lan.Refresh();//automatically skips if current language is English
			LanguageForeigns.Refresh();//automatically skips if current language is English
			Batch.Select("graphictype,graphicassembly,graphicelement,graphicshape");
			GraphicPoints.Refresh();
			try{
				Directory.CreateDirectory("Sounds");
				string[] myFiles=Directory.GetFiles(((Pref)Prefs.HList["DocPath"]).ValueString+"Sounds");
				for(int i=0;i<myFiles.Length;i++){
					string[] splitFile=myFiles[i].Split('\\');
					File.Copy(myFiles[i],"Sounds\\"+splitFile[splitFile.Length-1],true);
				}
			}
			catch{
				MessageBox.Show("Error.  Your sound files are set to read only.  Please change them first.");
				Application.Exit();
				return;
			}
			buttonsShadow=imageList2x6.Images[0];  //(Image)pictButtons.Image.Clone();
			DataValid.BecameInvalid += new System.EventHandler(DataValid_BecameInvalid);
			ContrAccount2.InstantClasses();
			ContrAppt2.InstantClasses();
			ContrChart2.InstantClasses();
			ContrDocs2.InstantClasses();
			ContrFamily2.InstantClasses();
			ContrStaff2.InstantClasses();
			ContrTreat2.InitializeOnStartup();
			ContrAppt2.Visible=true;
			//butAppt.BackColor=Color.White;
			//labelAppt.BackColor=Color.White;
			this.ActiveControl=this.ContrAppt2;
			ContrAppt2.ModuleSelected();
			Thread2=new Thread(new ThreadStart(Listen));
			if(((Pref)Prefs.HList["AutoRefreshIsDisabled"]).ValueString!="1")
				Thread2.Start();
			timerTimeIndic.Enabled=true;
			myOutlookBar.Buttons[0].Caption=Lan.g(this,"Appts");
			myOutlookBar.Buttons[1].Caption=Lan.g(this,"Family");
			myOutlookBar.Buttons[2].Caption=Lan.g(this,"Account");
			myOutlookBar.Buttons[3].Caption=Lan.g(this,"Treat' Plan");
			//myOutlookBar.Buttons[4].Caption=Lan.g(this,"Chart");done in RefreshLocalData
			myOutlookBar.Buttons[5].Caption=Lan.g(this,"Images");
			myOutlookBar.Buttons[6].Caption=Lan.g(this,"Staff");
			foreach(MenuItem menuItem in mainMenu.MenuItems){
				TranslateMenuItem(menuItem);
			}
			//Lan.C(this, new System.Windows.Forms.Control[] {
				//labelAccount,
				//labelAppt,
				//labelDocs,
				//labelFam,
				//labelMsg,
        //labelTreat,
				//labelChart,
				//labelStaff				
			//});
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
			//Messages.StartTheWaitingThread();
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
			ApptViews.Refresh();
			ApptViewItems.Refresh();
      AutoCodes.Refresh();
      AutoCodeItems.Refresh();
      AutoCodeConds.Refresh();
			ProcButtons.Refresh();
			ProcButtonItems.Refresh();
      //BackupJobs.Refresh();
			Carriers.Refresh();
			Computers.Refresh();//gets workstation prefs
			CovCats.Refresh();
			CovSpans.Refresh();
			Employees.Refresh();
			Employers.Refresh();//really only needed when opening the prog.
			Defs.Refresh();
			EmailTemplates.Refresh();
			Fees.Refresh();
			Letters.Refresh();
			QuickPasteNotes.Refresh();
			QuickPasteCats.Refresh();
			Permissions.Refresh();
			Programs.Refresh();
			ProgramProperties.Refresh();
			Reports.Refresh();
			ToolButItems.Refresh();
			Providers.Refresh();
			ProcedureCodes.Refresh();
			Referrals.Refresh();//Referrals are also refreshed dynamically.  May rework so they don't consume mem.
			SchedDefaults.Refresh();//SchedDefaults are assumed to change rarely.
      //Schedules.Refresh();//Schedules are refreshed as needed.  Not here.
			UserPermissions.Refresh();
			//Users.Refresh();
			ClaimFormItems.Refresh();
			ClaimForms.Refresh();
			ContrAccount2.LayoutToolBar();
			ContrAppt2.LayoutToolBar();
			ContrChart2.LayoutToolBar();
			ContrDocs2.LayoutToolBar();
			ContrFamily2.LayoutToolBar();
			ContrTreat2.InitializeLocalData();
			AddLettersToMenu();
			menuItemPracticeWebReports.Visible=Programs.IsEnabled("PracticeWebReports");
			ContrAppt2.FillViews();
			if(!Directory.Exists(((Pref)Prefs.HList["DocPath"]).ValueString)
				|| !Directory.Exists(((Pref)Prefs.HList["DocPath"]).ValueString+"A")){
				FormPath FormP=new FormPath();
				FormP.ShowDialog();
				if(FormP.DialogResult!=DialogResult.OK){
					return false;
				}
				else{
					Prefs.Refresh();//because listening thread not started yet.
				}
			}
			if(((Pref)Prefs.HList["EasyHidePublicHealth"]).ValueString=="1"){
				menuItemSchools.Visible=false;
				menuItemCounties.Visible=false;
				menuItemScreening.Visible=false;
				menuItemPHSep.Visible=false;
				menuItemPublicHealth.Visible=false;
				menuItemPHRawScreen.Visible=false;
				menuItemPHRawPop.Visible=false;
				menuItemPHScreen.Visible=false;
			}
			else{
				menuItemSchools.Visible=true;
				menuItemCounties.Visible=true;
				menuItemScreening.Visible=true;
				menuItemPHSep.Visible=true;
				menuItemPublicHealth.Visible=true;
				menuItemPHRawScreen.Visible=true;
				menuItemPHRawPop.Visible=true;
				menuItemPHScreen.Visible=true;
			}
			if(((Pref)Prefs.HList["EasyHideClinical"]).ValueString=="1"){
				myOutlookBar.Buttons[4].Caption=Lan.g(this,"Procs");
			}
			else{
				myOutlookBar.Buttons[4].Caption=Lan.g(this,"Chart");
			}
			if(((Pref)Prefs.HList["EasyBasicModules"]).ValueString=="1"){
				myOutlookBar.Buttons[3].Visible=false;
				myOutlookBar.Buttons[5].Visible=false;
				myOutlookBar.Buttons[6].Visible=false;
				pictButtons.Visible=false;
			}
			else{
				myOutlookBar.Buttons[3].Visible=true;
				myOutlookBar.Buttons[5].Visible=true;
				myOutlookBar.Buttons[6].Visible=true;
				pictButtons.Visible=true;
			}
			myOutlookBar.Invalidate();
			return true;
		}

		private void AddLettersToMenu(){
			/*
			menuItemLetters.MenuItems.Clear();
			menuItemLetters.MenuItems.Add(menuItemLetterSetup);//it was already created in the generated code
			MenuItem[] menuItemLetter=new MenuItem[Reports.List.Length];
			for(int i=0;i<Reports.List.Length;i++){
				menuItemLetter[i]=new MenuItem();
				menuItemLetters.MenuItems.Add(menuItemLetter[i]);
				menuItemLetter[i].Index = i+1;
				menuItemLetter[i].Text = Reports.List[i].ReportName;
				menuItemLetter[i].Click += new System.EventHandler(this.menuItemLetter_Click);
			}
			*/
		}

		private void FormFreeDental_Layout(object sender, System.Windows.Forms.LayoutEventArgs e){
			if(Width<200) Width=200;
			ContrAccount2.Location=new Point(myOutlookBar.Width,0);
			ContrAccount2.Width=this.ClientSize.Width-ContrAccount2.Location.X;
			ContrAccount2.Height=this.ClientSize.Height;
			ContrAppt2.Location=new Point(myOutlookBar.Width,0);
			ContrAppt2.Width=this.ClientSize.Width-ContrAppt2.Location.X;
			ContrAppt2.Height=this.ClientSize.Height;
			ContrChart2.Location=new Point(myOutlookBar.Width,0);
			ContrChart2.Width=this.ClientSize.Width-ContrChart2.Location.X;
			ContrChart2.Height=this.ClientSize.Height;
			ContrDocs2.Location=new Point(myOutlookBar.Width,0);
			ContrDocs2.Width=this.ClientSize.Width-ContrDocs2.Location.X;
			ContrDocs2.Height=this.ClientSize.Height;
			ContrFamily2.Location=new Point(myOutlookBar.Width,0);
			ContrFamily2.Width=this.ClientSize.Width-ContrFamily2.Location.X;
			ContrFamily2.Height=this.ClientSize.Height;
			ContrStaff2.Location=new Point(myOutlookBar.Width,0);
			ContrStaff2.Width=this.ClientSize.Width-ContrStaff2.Location.X;
			ContrStaff2.Height=this.ClientSize.Height;
			//ContrStaff2.Location=new Point(myOutlookBar.Width,0);
			//ContrStaff2.Width=this.ClientSize.Width-ContrStaff2.Location.X;
			//ContrStaff2.Height=this.ClientSize.Height;
			ContrTreat2.Location=new Point(myOutlookBar.Width,0);
			ContrTreat2.Width=this.ClientSize.Width-ContrDocs2.Location.X;
			ContrTreat2.Height=this.ClientSize.Height;
		}

		private void DataValid_BecameInvalid(object sender, System.EventArgs e){
			MessageInvalid msg;
			switch(DataValid.IType){
				case InvalidType.LocalData:
					RefreshLocalData();//does local computer first
					if(((Pref)Prefs.HList["AutoRefreshIsDisabled"]).ValueString=="1")
						return;
					msg=new MessageInvalid();
					msg.Type="LocalData";
					msg.DateViewing=DateTime.MinValue;
					Messages.SendMessage(msg);//then other computers
					break;
				case InvalidType.Date:
					//local refresh is handled within ContrAppt, not here
					if(((Pref)Prefs.HList["AutoRefreshIsDisabled"]).ValueString=="1")
						return;
					msg=new MessageInvalid();
					msg.Type="Date";
					msg.DateViewing=Appointments.DateSelected;
					Messages.SendMessage(msg);
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

		///<summary>separate thread</summary>
		public void Listen(){
			TcpListener2=new TcpListener(2123);
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

	
		///<summary></summary>
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
						grfx.Dispose();
						break;
					case "Text":
						PlaySoundFunct(0,0,true);
						FormMessageText FormMT=new FormMessageText();
						FormMT.Text2.Text=Messages.RecdMsgBut.Text;
						FormMT.ShowDialog();
						ContrStaff2.LogMsg(Messages.RecdMsgBut.Text);
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

		///<summary></summary>
		protected delegate void ProcessMessageDelegate(string text);

		private void myOutlookBar_ButtonClicked(object sender, OpenDental.ButtonClicked_EventArgs e) {
			UnselectActive();
			allNeutral();
			switch(myOutlookBar.SelectedIndex){
				case 0:
					ContrAppt2.Visible=true;
					this.ActiveControl=this.ContrAppt2;
					ContrAppt2.ModuleSelected();
					break;
				case 1:
					ContrFamily2.Visible=true;
					this.ActiveControl=this.ContrFamily2;
					ContrFamily2.ModuleSelected();
					break;
				case 2:
					ContrAccount2.Visible=true;
					this.ActiveControl=this.ContrAccount2;
					ContrAccount2.ModuleSelected();
					break;
				case 3:
					ContrTreat2.Visible=true;
					this.ActiveControl=this.ContrTreat2;
					ContrTreat2.ModuleSelected();
					break;
				case 4:
					ContrChart2.Visible=true;
					this.ActiveControl=this.ContrChart2;
					ContrChart2.ModuleSelected();
					break;
				case 5:
					ContrDocs2.Visible=true;
					this.ActiveControl=this.ContrDocs2;
					ContrDocs2.ModuleSelected();
					break;
				case 6:
					ContrStaff2.Visible=true;
					this.ActiveControl=this.ContrStaff2;
					ContrStaff2.ModuleSelected();
					break;
				case 7:
					//ContrAppt2.Visible=true;
					//this.ActiveControl=this.ContrAppt2;
					//ContrAppt2.ModuleSelected();
					break;
			}
		}

		private void button1_Click(object sender, System.EventArgs e) {
			
		}

		private void allNeutral(){
			ContrAppt2.Visible=false;
			ContrFamily2.Visible=false;
			ContrAccount2.Visible=false;
			ContrTreat2.Visible=false;
			ContrChart2.Visible=false;
			ContrDocs2.Visible=false;
			ContrStaff2.Visible=false;
			/*butAccount.BackColor=Color.FromArgb(224,224,224);
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
			labelStaff.BackColor=Color.FromArgb(224,224,224);*/
		}

		private void UnselectActive(){
			if(ContrAppt2.Visible)
				ContrAppt2.ModuleUnselected();
			if(ContrFamily2.Visible)
				ContrFamily2.ModuleUnselected();
			if(ContrAccount2.Visible)
				ContrAccount2.ModuleUnselected();
			if(ContrTreat2.Visible)
				ContrTreat2.ModuleUnselected();
			if(ContrChart2.Visible)
				ContrChart2.ModuleUnselected();
			//ContrMessage2.Visible=false;
			if(ContrDocs2.Visible)
				ContrDocs2.ModuleUnselected();
			//ContrStaff2.Visible=false;
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
			MessageButtons msg=new MessageButtons();
			//Messages.MessageToSend=new MessageInvalid();//because this value is tested when processing
			Graphics grfx=Graphics.FromImage(buttonsShadow);
			//Color buttonColor;
			int row=e.Y/18;
			int col=e.X/18;
			if(row>5) row=5;
			if(col>1) col=1;
			msg.Type="Button";
			msg.Text=" ";
			msg.Row=row;
			msg.Col=col;
			if(col==0 && !buttonDown[0,row]){//button in first col, currently not down
				buttonDown[0,row]=true;
				grfx.FillRectangle(new SolidBrush(Color.Red),col*18+1,row*18+1,17,17);
				pictButtons.Image=buttonsShadow;
				pictButtons.Refresh();
				msg.Pushed=true;
				Messages.SendMessage(msg);
				PlaySoundFunct(col,row,false);
			}
			else if(col==1 && !buttonDown[1,row]){//button in second col, currently not down
				grfx.FillRectangle(new SolidBrush(Color.Red),col*18+1,row*18+1,17,17);
				pictButtons.Image=buttonsShadow;
				pictButtons.Refresh();
				msg.Pushed=true;
				Messages.SendMessage(msg);
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
				msg.Pushed=false;
				Messages.SendMessage(msg);
			}
		}

		/// <summary>sends function key presses to the appointment module</summary>
		private void FormOpenDental_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(ContrAppt2.Visible && e.KeyCode>=Keys.F1 && e.KeyCode<=Keys.F12)
				ContrAppt2.FunctionKeyPress(e.KeyCode);
		}

		private void FormFreeDental_Closing(object sender, System.ComponentModel.CancelEventArgs e){
			Thread2.Abort();
			if(this.TcpListener2!=null){
				this.TcpListener2.Stop();  
			}
			Application.Exit();
		}

		private void timerTimeIndic_Tick(object sender, System.EventArgs e){
			if(WindowState!=FormWindowState.Minimized
				&& ContrAppt2.Visible){
				ContrAppt2.TickRefresh();
      }
      //BackupJobs.CheckForBackupJobs();      			
		}

		///<summary>This is recursive</summary>
		private void TranslateMenuItem(MenuItem menuItem){
			//first translate any child menuItems
			foreach(MenuItem mi in menuItem.MenuItems){
				TranslateMenuItem(mi);
			}
			//then this menuitem
			Lan.C("MainMenu",menuItem);
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
			FormConfig FormC=new FormConfig();
			FormC.ShowDialog();
			if(FormC.DialogResult==DialogResult.OK){
				RefreshLocalData();
				RefreshCurrentModule();
			}
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
		private void menuItemApptViews_Click(object sender, System.EventArgs e) {
			FormApptViews FormAV=new FormApptViews();
			FormAV.ShowDialog();
			RefreshCurrentModule();
		}

		private void menuItemAutoCodes_Click(object sender, System.EventArgs e) {
			FormAutoCode FormAC=new FormAutoCode();
			FormAC.ShowDialog();		
		}

		private void menuItemClaimForms_Click(object sender, System.EventArgs e) {
			FormClaimForms FormCF=new FormClaimForms();
			FormCF.ShowDialog();
		}

		private void menuItemComputers_Click(object sender, System.EventArgs e) {
			FormComputers FormC=new FormComputers();
			FormC.ShowDialog();
		}

		private void menuItemDataPath_Click(object sender, System.EventArgs e) {
			FormPath FormP=new FormPath();
			FormP.ShowDialog();	
		}

		private void menuItemDefinitions_Click(object sender, System.EventArgs e) {
			FormDefinitions formDefinitions2 = new FormDefinitions();
			formDefinitions2.ShowDialog();
			RefreshCurrentModule();
		}

		private void menuItemEasy_Click(object sender, System.EventArgs e) {
			FormEasy FormE=new FormEasy();
			FormE.ShowDialog();
			RefreshCurrentModule();
		}

		private void menuItemEmail_Click(object sender, System.EventArgs e) {
			FormEmailSetup FormE=new FormEmailSetup();
			FormE.ShowDialog();
		}

		private void menuItemInsCats_Click(object sender, System.EventArgs e) {
			FormInsCatsSetup FormE=new FormInsCatsSetup();
			FormE.ShowDialog();	
		}

		private void menuItemPermissions_Click(object sender, System.EventArgs e) {
			FormPermissionsManage FormPM=new FormPermissionsManage(); 
			FormPM.ShowDialog();
		}

		private void menuItemPractice_Click(object sender, System.EventArgs e) {
			FormPractice FormPr=new FormPractice();
			FormPr.ShowDialog();		
		}

		private void menuItemProcedureButtons_Click(object sender, System.EventArgs e) {
			FormProcButtons FormPB=new FormProcButtons();
			FormPB.ShowDialog();	
		}

		private void menuItemLinks_Click(object sender, System.EventArgs e) {
			FormProgramLinks FormPL=new FormProgramLinks();
			FormPL.ShowDialog();	
		}

		private void menuItemRecall_Click(object sender, System.EventArgs e) {
			FormRecallSetup FormRS=new FormRecallSetup();
			FormRS.ShowDialog();	
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

		//Lists
		
		private void menuItemContacts_Click(object sender, System.EventArgs e) {
			FormContacts FormC=new FormContacts();
			FormC.ShowDialog();
		}

		private void menuItemCounties_Click(object sender, System.EventArgs e) {
			FormCounties FormC=new FormCounties();
			FormC.ShowDialog();
		}

		private void menuItemEmployees_Click(object sender, System.EventArgs e) {
			FormEmployee FormEmp=new FormEmployee();
			FormEmp.ShowDialog();		
		}

		private void menuItemEmployers_Click(object sender, System.EventArgs e) {
			FormEmployers FormE=new FormEmployers();
			FormE.ShowDialog();
		}

		private void menuItemCarriers_Click(object sender, System.EventArgs e) {
			FormCarriers FormC=new FormCarriers();
			FormC.ShowDialog();
			RefreshCurrentModule();
		}

		private void menuItemInsTemplates_Click(object sender, System.EventArgs e) {
			FormInsPlans FormIP = new FormInsPlans();
			FormIP.ShowDialog();
			RefreshCurrentModule();
		}

		private void menuItemMedications_Click(object sender, System.EventArgs e) {
			FormMedications FormM=new FormMedications();
			FormM.ShowDialog();
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

		private void menuItemSchools_Click(object sender, System.EventArgs e) {
			FormSchools FormS=new FormSchools();
			FormS.ShowDialog();
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

		private void menuItemPracticeWebReports_Click(object sender, System.EventArgs e) {
			try{
				Process.Start("PWReports.exe");
			}
			catch{
				MessageBox.Show("PracticeWeb Reports module unavailable.");
			}
		}

		private void menuItemRpProdInc_Click(object sender, System.EventArgs e) {
			FormRpProdInc FormPI=new FormRpProdInc();
			FormPI.ShowDialog();
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

		private void menuItemRpCapitation_Click(object sender, System.EventArgs e) {
			FormRpCapitation FormC=new FormRpCapitation();
			FormC.ShowDialog();
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

		//Reports-Lists
		private void menuAppointments_Click(object sender, System.EventArgs e) {
			FormRpAppointments FormA=new FormRpAppointments();
			FormA.ShowDialog();
		}

		private void menuItemInsCarriers_Click(object sender, System.EventArgs e) {
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

		//Public Health
		private void menuItemPHRawScreen_Click(object sender, System.EventArgs e) {
			FormRpPHRawScreen FormPH=new FormRpPHRawScreen();
			FormPH.ShowDialog();
		}

		private void menuItemPHRawPop_Click(object sender, System.EventArgs e) {
			FormRpPHRawPop FormPH=new FormRpPHRawPop();
			FormPH.ShowDialog();
		}

		private void menuItemPHScreen_Click(object sender, System.EventArgs e) {
			MessageBox.Show("This report is incomplete.");
		}

		//Letters
		/*
		private void menuItemLetterSetup_Click(object sender, System.EventArgs e) {
			FormLetterSetup FormLS=new FormLetterSetup();
			FormLS.ShowDialog();
		}*/

		/*
		private void menuItemLetter_Click(object sender, System.EventArgs e) {
			MessageBox.Show(((MenuItem)sender).Index.ToString());
		}*/

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

		private void menuItemPaymentPlans_Click(object sender, System.EventArgs e) {
			FormPayPlanUpdate FormPPU=new FormPayPlanUpdate();
			FormPPU.ShowDialog();
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

		private void menuItemScreening_Click(object sender, System.EventArgs e) {
			FormScreenings FormS=new FormScreenings();
			FormS.ShowDialog();
		}

		private void menuItemClaims_Click(object sender, System.EventArgs e) {
			int	oldPatNum=Patients.Cur.PatNum;
			FormClaimsSend FormCS=new FormClaimsSend();
			FormCS.ShowDialog();
			Patient oldPatient=new Patient();
			oldPatient.PatNum=oldPatNum;
			Patients.Cur=oldPatient;
			RefreshCurrentModule();
		}

		//Help
		private void menuItemAbout_Click(object sender, System.EventArgs e) {
			FormAbout FormA = new FormAbout();
			FormA.ShowDialog();
		}

		private void menuItemHelpContents_Click(object sender, System.EventArgs e) {
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

		

		



		/*private void menuItemDaily_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e) {
			//MessageBox.Show(e.Bounds.ToString());
			e.Graphics.DrawString("Dailyyyy",new Font("Microsoft Sans Serif",8),Brushes.Black,e.Bounds.X,e.Bounds.Y);
		}

		private void menuItemDaily_Click(object sender, System.EventArgs e) {
		
		}*/

		#endregion

		
		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

	

		

		

		

	

		

		

	}
}
