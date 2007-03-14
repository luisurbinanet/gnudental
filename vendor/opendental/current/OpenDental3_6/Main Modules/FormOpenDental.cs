/*=============================================================================================================
Open Dental is a dental practice management program.
Copyright (C) 2003-2004  Jordan Sparks, DMD.  http://www.open-dent.com,  http://www.docsparks.com

This program is free software; you can redistribute it and/or modify it under the terms of the
GNU General Public License as published by the Free Software Foundation; either version 2 of the License,
or (at your option) any later version.

This program is distributed in the hope that it will be useful, but without any warranty. See the GNU General Public License
for more details, available at http://www.opensource.org/licenses/gpl-license.php

Any changes to this program must follow the guidelines of the GPL license if a modified version is to be
redistributed.
===============================================================================================================*/
//For now, all screens are assumed to have available 990x734.  That would be a screen resolution of 1024x768 with a single width taskbar docked to any one of the four sides of the screen.

//The 7 main controls are slightly narrower due to menu bar on left of 51. Max size 939x708

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
using System.Security.Principal;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace OpenDental{
	///<summary></summary>
	public class FormOpenDental : System.Windows.Forms.Form{
		private System.Windows.Forms.ImageList imageList1;
		private OpenDental.ContrAppt ContrAppt2;
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
		private System.Windows.Forms.MenuItem menuItemReferrals;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItemCheckDatabase;
		private System.Windows.Forms.MenuItem menuItemProcedureButtons;
		private System.Windows.Forms.MenuItem menuItemZipCodes;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuTelephone;
		private System.Windows.Forms.MenuItem menuItem9;
		private OpenDental.ContrDocs ContrDocs2;
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
		private System.Windows.Forms.MenuItem menuItemEasy;
		private System.Windows.Forms.MenuItem menuItemCarriers;
		private System.Windows.Forms.MenuItem menuItemSchools;
		private System.Windows.Forms.MenuItem menuItemCounties;
		private System.Windows.Forms.MenuItem menuItemScreening;
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
		private int CurPatNum;
		private System.Windows.Forms.MenuItem menuItemClearinghouses;
		private System.Windows.Forms.MenuItem menuItemUpdate;
		private System.Windows.Forms.MenuItem menuItemRpProcNote;
		private System.Windows.Forms.MenuItem menuItemHelpWindows;
		private System.Windows.Forms.MenuItem menuItemMisc;
		private System.Windows.Forms.MenuItem menuItemBirthdays;
		private System.Windows.Forms.MenuItem menuItemProvDefault;
		private System.Windows.Forms.MenuItem menuItemProvSched;
		private System.Windows.Forms.MenuItem menuItemBlockoutDefault;
		private System.Windows.Forms.MenuItem menuItemRemote;
		private OpenDental.ContrStaff ContrManage2;
		private System.Windows.Forms.MenuItem menuItemInstructors;
		private System.Windows.Forms.MenuItem menuItemSchoolClass;
		private System.Windows.Forms.MenuItem menuItemSchoolCourses;
		private System.Windows.Forms.MenuItem menuItemCourseGrades;
		private System.Windows.Forms.MenuItem menuItemPatientImport;
		private System.Windows.Forms.MenuItem menuItemSecurity;
		private System.Windows.Forms.MenuItem menuItemLogOff;
		///<summary>Will be true if this is the second instance of Open Dental running on this computer. This might happen with terminal services or fast user switching.  If true, then the message listening is disabled.  This might cause synchronisation issues if used extensively.  A timed synchronization is planned for this situation.</summary>
		private static bool IsSecondInstance;
		private System.Windows.Forms.MenuItem menuItemInsPlans;
		private System.Windows.Forms.MenuItem menuItemClinics;
		///<summary>When user logs out, this keeps track of where they were for when they log back in.</summary>
		private int LastModule;

		///<summary></summary>
		public FormOpenDental(){
			InitializeComponent();
			ContrAppt2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrFamily2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrAccount2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrTreat2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrChart2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrDocs2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			ContrManage2.PatientSelected+=new PatientSelectedEventHandler(Contr_PatientSelected);
			GotoModule.ModuleSelected+=new ModuleEventHandler(GotoModule_ModuleSelected);
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
			this.ContrFamily2 = new OpenDental.ContrFamily();
			this.imageList2x6 = new System.Windows.Forms.ImageList(this.components);
			this.timerTimeIndic = new System.Windows.Forms.Timer(this.components);
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItemLogOff = new System.Windows.Forms.MenuItem();
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
			this.menuItemClearinghouses = new System.Windows.Forms.MenuItem();
			this.menuItemComputers = new System.Windows.Forms.MenuItem();
			this.menuItemDataPath = new System.Windows.Forms.MenuItem();
			this.menuItemDefinitions = new System.Windows.Forms.MenuItem();
			this.menuItemEasy = new System.Windows.Forms.MenuItem();
			this.menuItemEmail = new System.Windows.Forms.MenuItem();
			this.menuItemInsCats = new System.Windows.Forms.MenuItem();
			this.menuItemMisc = new System.Windows.Forms.MenuItem();
			this.menuItemPractice = new System.Windows.Forms.MenuItem();
			this.menuItemProcedureButtons = new System.Windows.Forms.MenuItem();
			this.menuItemLinks = new System.Windows.Forms.MenuItem();
			this.menuItemRecall = new System.Windows.Forms.MenuItem();
			this.menuItemSecurity = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItemSched = new System.Windows.Forms.MenuItem();
			this.menuItemPracDef = new System.Windows.Forms.MenuItem();
			this.menuItemPracSched = new System.Windows.Forms.MenuItem();
			this.menuItemProvDefault = new System.Windows.Forms.MenuItem();
			this.menuItemProvSched = new System.Windows.Forms.MenuItem();
			this.menuItemBlockoutDefault = new System.Windows.Forms.MenuItem();
			this.menuItemLists = new System.Windows.Forms.MenuItem();
			this.menuItemClinics = new System.Windows.Forms.MenuItem();
			this.menuItemContacts = new System.Windows.Forms.MenuItem();
			this.menuItemCounties = new System.Windows.Forms.MenuItem();
			this.menuItemSchoolClass = new System.Windows.Forms.MenuItem();
			this.menuItemSchoolCourses = new System.Windows.Forms.MenuItem();
			this.menuItemEmployees = new System.Windows.Forms.MenuItem();
			this.menuItemEmployers = new System.Windows.Forms.MenuItem();
			this.menuItemInstructors = new System.Windows.Forms.MenuItem();
			this.menuItemCarriers = new System.Windows.Forms.MenuItem();
			this.menuItemInsPlans = new System.Windows.Forms.MenuItem();
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
			this.menuItemRpProcNote = new System.Windows.Forms.MenuItem();
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
			this.menuItemBirthdays = new System.Windows.Forms.MenuItem();
			this.menuItemInsCarriers = new System.Windows.Forms.MenuItem();
			this.menuItemPatList = new System.Windows.Forms.MenuItem();
			this.menuItemRxs = new System.Windows.Forms.MenuItem();
			this.menuItemProcCodes = new System.Windows.Forms.MenuItem();
			this.menuItemRefs = new System.Windows.Forms.MenuItem();
			this.menuItemPHSep = new System.Windows.Forms.MenuItem();
			this.menuItemPHRawScreen = new System.Windows.Forms.MenuItem();
			this.menuItemPHRawPop = new System.Windows.Forms.MenuItem();
			this.menuItemPHScreen = new System.Windows.Forms.MenuItem();
			this.menuItemCourseGrades = new System.Windows.Forms.MenuItem();
			this.menuItemTools = new System.Windows.Forms.MenuItem();
			this.menuItemPrintScreen = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemCheckDatabase = new System.Windows.Forms.MenuItem();
			this.menuTelephone = new System.Windows.Forms.MenuItem();
			this.menuItemPatientImport = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItemPaymentPlans = new System.Windows.Forms.MenuItem();
			this.menuItemAging = new System.Windows.Forms.MenuItem();
			this.menuItemFinanceCharge = new System.Windows.Forms.MenuItem();
			this.menuItemBilling = new System.Windows.Forms.MenuItem();
			this.menuItemTranslation = new System.Windows.Forms.MenuItem();
			this.menuItemScreening = new System.Windows.Forms.MenuItem();
			this.menuItemHelp = new System.Windows.Forms.MenuItem();
			this.menuItemHelpWindows = new System.Windows.Forms.MenuItem();
			this.menuItemHelpContents = new System.Windows.Forms.MenuItem();
			this.menuItemHelpIndex = new System.Windows.Forms.MenuItem();
			this.menuItemRemote = new System.Windows.Forms.MenuItem();
			this.menuItemUpdate = new System.Windows.Forms.MenuItem();
			this.myOutlookBar = new OpenDental.OutlookBar();
			this.imageList32 = new System.Windows.Forms.ImageList(this.components);
			this.ContrManage2 = new OpenDental.ContrStaff();
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
			this.ContrAppt2.Visible = false;
			// 
			// ContrAccount2
			// 
			this.ContrAccount2.Location = new System.Drawing.Point(145, 0);
			this.ContrAccount2.Name = "ContrAccount2";
			this.ContrAccount2.Size = new System.Drawing.Size(880, 690);
			this.ContrAccount2.TabIndex = 11;
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
																																						 this.menuItemLogOff,
																																						 this.menuItemFile,
																																						 this.menuItemSettings,
																																						 this.menuItemLists,
																																						 this.menuItemReports,
																																						 this.menuItemTools,
																																						 this.menuItemHelp});
			// 
			// menuItemLogOff
			// 
			this.menuItemLogOff.Index = 0;
			this.menuItemLogOff.Text = "Log &Off";
			this.menuItemLogOff.Click += new System.EventHandler(this.menuItemLogOff_Click);
			// 
			// menuItemFile
			// 
			this.menuItemFile.Index = 1;
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
			this.menuItemPrinter.Text = "&Printers";
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
			this.menuItemConfig.Text = "&Choose Database";
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
			this.menuItemSettings.Index = 2;
			this.menuItemSettings.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										 this.menuItemApptViews,
																																										 this.menuItemAutoCodes,
																																										 this.menuItemClaimForms,
																																										 this.menuItemClearinghouses,
																																										 this.menuItemComputers,
																																										 this.menuItemDataPath,
																																										 this.menuItemDefinitions,
																																										 this.menuItemEasy,
																																										 this.menuItemEmail,
																																										 this.menuItemInsCats,
																																										 this.menuItemMisc,
																																										 this.menuItemPractice,
																																										 this.menuItemProcedureButtons,
																																										 this.menuItemLinks,
																																										 this.menuItemRecall,
																																										 this.menuItemSecurity,
																																										 this.menuItem10,
																																										 this.menuItemSched,
																																										 this.menuItemPracDef,
																																										 this.menuItemPracSched,
																																										 this.menuItemProvDefault,
																																										 this.menuItemProvSched,
																																										 this.menuItemBlockoutDefault});
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
			// menuItemClearinghouses
			// 
			this.menuItemClearinghouses.Index = 3;
			this.menuItemClearinghouses.Text = "Clearinghouses";
			this.menuItemClearinghouses.Click += new System.EventHandler(this.menuItemClearinghouses_Click);
			// 
			// menuItemComputers
			// 
			this.menuItemComputers.Index = 4;
			this.menuItemComputers.Text = "Computers";
			this.menuItemComputers.Click += new System.EventHandler(this.menuItemComputers_Click);
			// 
			// menuItemDataPath
			// 
			this.menuItemDataPath.Index = 5;
			this.menuItemDataPath.Text = "Data Paths";
			this.menuItemDataPath.Click += new System.EventHandler(this.menuItemDataPath_Click);
			// 
			// menuItemDefinitions
			// 
			this.menuItemDefinitions.Index = 6;
			this.menuItemDefinitions.Text = "Definitions";
			this.menuItemDefinitions.Click += new System.EventHandler(this.menuItemDefinitions_Click);
			// 
			// menuItemEasy
			// 
			this.menuItemEasy.Index = 7;
			this.menuItemEasy.Text = "Easy Options";
			this.menuItemEasy.Click += new System.EventHandler(this.menuItemEasy_Click);
			// 
			// menuItemEmail
			// 
			this.menuItemEmail.Index = 8;
			this.menuItemEmail.Text = "E-mail";
			this.menuItemEmail.Click += new System.EventHandler(this.menuItemEmail_Click);
			// 
			// menuItemInsCats
			// 
			this.menuItemInsCats.Index = 9;
			this.menuItemInsCats.Text = "Insurance Categories";
			this.menuItemInsCats.Click += new System.EventHandler(this.menuItemInsCats_Click);
			// 
			// menuItemMisc
			// 
			this.menuItemMisc.Index = 10;
			this.menuItemMisc.Text = "Miscellaneous";
			this.menuItemMisc.Click += new System.EventHandler(this.menuItemMisc_Click);
			// 
			// menuItemPractice
			// 
			this.menuItemPractice.Index = 11;
			this.menuItemPractice.Text = "Practice";
			this.menuItemPractice.Click += new System.EventHandler(this.menuItemPractice_Click);
			// 
			// menuItemProcedureButtons
			// 
			this.menuItemProcedureButtons.Index = 12;
			this.menuItemProcedureButtons.Text = "Procedure Buttons";
			this.menuItemProcedureButtons.Click += new System.EventHandler(this.menuItemProcedureButtons_Click);
			// 
			// menuItemLinks
			// 
			this.menuItemLinks.Index = 13;
			this.menuItemLinks.Text = "Program Links";
			this.menuItemLinks.Click += new System.EventHandler(this.menuItemLinks_Click);
			// 
			// menuItemRecall
			// 
			this.menuItemRecall.Index = 14;
			this.menuItemRecall.Text = "Recall";
			this.menuItemRecall.Click += new System.EventHandler(this.menuItemRecall_Click);
			// 
			// menuItemSecurity
			// 
			this.menuItemSecurity.Index = 15;
			this.menuItemSecurity.Text = "Security";
			this.menuItemSecurity.Click += new System.EventHandler(this.menuItemSecurity_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 16;
			this.menuItem10.Text = "-";
			// 
			// menuItemSched
			// 
			this.menuItemSched.Index = 17;
			this.menuItemSched.Text = "SCHEDULES";
			// 
			// menuItemPracDef
			// 
			this.menuItemPracDef.Index = 18;
			this.menuItemPracDef.Text = "   Practice Default";
			this.menuItemPracDef.Click += new System.EventHandler(this.menuItemPracDef_Click);
			// 
			// menuItemPracSched
			// 
			this.menuItemPracSched.Index = 19;
			this.menuItemPracSched.Text = "   Practice";
			this.menuItemPracSched.Click += new System.EventHandler(this.menuItemPracSched_Click);
			// 
			// menuItemProvDefault
			// 
			this.menuItemProvDefault.Index = 20;
			this.menuItemProvDefault.Text = "   Provider Default";
			this.menuItemProvDefault.Click += new System.EventHandler(this.menuItemProvDefault_Click);
			// 
			// menuItemProvSched
			// 
			this.menuItemProvSched.Index = 21;
			this.menuItemProvSched.Text = "   Provider";
			this.menuItemProvSched.Click += new System.EventHandler(this.menuItemProvSched_Click);
			// 
			// menuItemBlockoutDefault
			// 
			this.menuItemBlockoutDefault.Index = 22;
			this.menuItemBlockoutDefault.Text = "   Blockout Default";
			this.menuItemBlockoutDefault.Click += new System.EventHandler(this.menuItemBlockoutDefault_Click);
			// 
			// menuItemLists
			// 
			this.menuItemLists.Index = 3;
			this.menuItemLists.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									this.menuItemClinics,
																																									this.menuItemContacts,
																																									this.menuItemCounties,
																																									this.menuItemSchoolClass,
																																									this.menuItemSchoolCourses,
																																									this.menuItemEmployees,
																																									this.menuItemEmployers,
																																									this.menuItemInstructors,
																																									this.menuItemCarriers,
																																									this.menuItemInsPlans,
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
			// menuItemClinics
			// 
			this.menuItemClinics.Index = 0;
			this.menuItemClinics.Text = "Clinics";
			this.menuItemClinics.Click += new System.EventHandler(this.menuItemClinics_Click);
			// 
			// menuItemContacts
			// 
			this.menuItemContacts.Index = 1;
			this.menuItemContacts.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftC;
			this.menuItemContacts.Text = "&Contacts";
			this.menuItemContacts.Click += new System.EventHandler(this.menuItemContacts_Click);
			// 
			// menuItemCounties
			// 
			this.menuItemCounties.Index = 2;
			this.menuItemCounties.Text = "Counties";
			this.menuItemCounties.Click += new System.EventHandler(this.menuItemCounties_Click);
			// 
			// menuItemSchoolClass
			// 
			this.menuItemSchoolClass.Index = 3;
			this.menuItemSchoolClass.Text = "Dental School Classes";
			this.menuItemSchoolClass.Click += new System.EventHandler(this.menuItemSchoolClass_Click);
			// 
			// menuItemSchoolCourses
			// 
			this.menuItemSchoolCourses.Index = 4;
			this.menuItemSchoolCourses.Text = "Dental School Courses";
			this.menuItemSchoolCourses.Click += new System.EventHandler(this.menuItemSchoolCourses_Click);
			// 
			// menuItemEmployees
			// 
			this.menuItemEmployees.Index = 5;
			this.menuItemEmployees.Text = "&Employees";
			this.menuItemEmployees.Click += new System.EventHandler(this.menuItemEmployees_Click);
			// 
			// menuItemEmployers
			// 
			this.menuItemEmployers.Index = 6;
			this.menuItemEmployers.Text = "Employers";
			this.menuItemEmployers.Click += new System.EventHandler(this.menuItemEmployers_Click);
			// 
			// menuItemInstructors
			// 
			this.menuItemInstructors.Index = 7;
			this.menuItemInstructors.Text = "Instructors";
			this.menuItemInstructors.Click += new System.EventHandler(this.menuItemInstructors_Click);
			// 
			// menuItemCarriers
			// 
			this.menuItemCarriers.Index = 8;
			this.menuItemCarriers.Text = "Insurance Carriers";
			this.menuItemCarriers.Click += new System.EventHandler(this.menuItemCarriers_Click);
			// 
			// menuItemInsPlans
			// 
			this.menuItemInsPlans.Index = 9;
			this.menuItemInsPlans.Text = "&Insurance Plans";
			this.menuItemInsPlans.Click += new System.EventHandler(this.menuItemInsPlans_Click);
			// 
			// menuItemMedications
			// 
			this.menuItemMedications.Index = 10;
			this.menuItemMedications.Text = "&Medications";
			this.menuItemMedications.Click += new System.EventHandler(this.menuItemMedications_Click);
			// 
			// menuItemProviders
			// 
			this.menuItemProviders.Index = 11;
			this.menuItemProviders.Text = "&Providers";
			this.menuItemProviders.Click += new System.EventHandler(this.menuItemProviders_Click);
			// 
			// menuItemPrescriptions
			// 
			this.menuItemPrescriptions.Index = 12;
			this.menuItemPrescriptions.Text = "Pre&scriptions";
			this.menuItemPrescriptions.Click += new System.EventHandler(this.menuItemPrescriptions_Click);
			// 
			// menuItemReferrals
			// 
			this.menuItemReferrals.Index = 13;
			this.menuItemReferrals.Text = "&Referrals";
			this.menuItemReferrals.Click += new System.EventHandler(this.menuItemReferrals_Click);
			// 
			// menuItemSchools
			// 
			this.menuItemSchools.Index = 14;
			this.menuItemSchools.Text = "Sites";
			this.menuItemSchools.Click += new System.EventHandler(this.menuItemSchools_Click);
			// 
			// menuItemZipCodes
			// 
			this.menuItemZipCodes.Index = 15;
			this.menuItemZipCodes.Text = "&Zip Codes";
			this.menuItemZipCodes.Click += new System.EventHandler(this.menuItemZipCodes_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 16;
			this.menuItem5.Text = "-";
			// 
			// menuItemProcCode
			// 
			this.menuItemProcCode.Index = 17;
			this.menuItemProcCode.Text = "PROCEDURE CODES";
			// 
			// menuItemEditCode
			// 
			this.menuItemEditCode.Index = 18;
			this.menuItemEditCode.Text = "   E&dit Codes";
			this.menuItemEditCode.Click += new System.EventHandler(this.menuItemEditCode_Click);
			// 
			// menuItemViewCode
			// 
			this.menuItemViewCode.Index = 19;
			this.menuItemViewCode.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftF;
			this.menuItemViewCode.Text = "   View &Fees";
			this.menuItemViewCode.Click += new System.EventHandler(this.menuItemViewCode_Click);
			// 
			// menuItemReports
			// 
			this.menuItemReports.Index = 4;
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
																																										this.menuItemRpProcNote,
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
																																										this.menuItemBirthdays,
																																										this.menuItemInsCarriers,
																																										this.menuItemPatList,
																																										this.menuItemRxs,
																																										this.menuItemProcCodes,
																																										this.menuItemRefs,
																																										this.menuItemPHSep,
																																										this.menuItemPHRawScreen,
																																										this.menuItemPHRawPop,
																																										this.menuItemPHScreen,
																																										this.menuItemCourseGrades});
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
			// menuItemRpProcNote
			// 
			this.menuItemRpProcNote.Index = 10;
			this.menuItemRpProcNote.Text = "   Incomplete Procedure Notes";
			this.menuItemRpProcNote.Click += new System.EventHandler(this.menuItemRpProcNote_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 11;
			this.menuItem3.Text = "-";
			// 
			// menuItemMonthly
			// 
			this.menuItemMonthly.Index = 12;
			this.menuItemMonthly.Text = "MONTHLY";
			// 
			// menuItemRpAging
			// 
			this.menuItemRpAging.Index = 13;
			this.menuItemRpAging.Text = "   Aging Report";
			this.menuItemRpAging.Click += new System.EventHandler(this.menuItemRpAging_Click);
			// 
			// menuItemRpClaimsNotSent
			// 
			this.menuItemRpClaimsNotSent.Index = 14;
			this.menuItemRpClaimsNotSent.Text = "   Claims Not Sent";
			this.menuItemRpClaimsNotSent.Click += new System.EventHandler(this.menuItemRpClaimsNotSent_Click);
			// 
			// menuItemRpCapitation
			// 
			this.menuItemRpCapitation.Index = 15;
			this.menuItemRpCapitation.Text = "   Capitation Utilization";
			this.menuItemRpCapitation.Click += new System.EventHandler(this.menuItemRpCapitation_Click);
			// 
			// menuItemRpFinanceCharge
			// 
			this.menuItemRpFinanceCharge.Index = 16;
			this.menuItemRpFinanceCharge.Text = "   Finance Charge Report";
			this.menuItemRpFinanceCharge.Click += new System.EventHandler(this.menuItemRpFinanceCharge_Click);
			// 
			// menuItemRpOutInsClaims
			// 
			this.menuItemRpOutInsClaims.Index = 17;
			this.menuItemRpOutInsClaims.Text = "   Outstanding Insurance Claims";
			this.menuItemRpOutInsClaims.Click += new System.EventHandler(this.menuItemRpOutInsClaims_Click);
			// 
			// menuItemRpProcNoBilled
			// 
			this.menuItemRpProcNoBilled.Index = 18;
			this.menuItemRpProcNoBilled.Text = "   Procedures Not Billed To Insurance";
			this.menuItemRpProcNoBilled.Click += new System.EventHandler(this.menuItemRpProcNoBilled_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 19;
			this.menuItem8.Text = "-";
			// 
			// menuItemList
			// 
			this.menuItemList.Index = 20;
			this.menuItemList.Text = "LISTS";
			// 
			// menuAppointments
			// 
			this.menuAppointments.Index = 21;
			this.menuAppointments.Text = "   Appointments";
			this.menuAppointments.Click += new System.EventHandler(this.menuAppointments_Click);
			// 
			// menuItemBirthdays
			// 
			this.menuItemBirthdays.Index = 22;
			this.menuItemBirthdays.Text = "   Birthdays";
			this.menuItemBirthdays.Click += new System.EventHandler(this.menuItemBirthdays_Click);
			// 
			// menuItemInsCarriers
			// 
			this.menuItemInsCarriers.Index = 23;
			this.menuItemInsCarriers.Text = "   Insurance Plans";
			this.menuItemInsCarriers.Click += new System.EventHandler(this.menuItemInsCarriers_Click);
			// 
			// menuItemPatList
			// 
			this.menuItemPatList.Index = 24;
			this.menuItemPatList.Text = "   Patients";
			this.menuItemPatList.Click += new System.EventHandler(this.menuItemPatList_Click);
			// 
			// menuItemRxs
			// 
			this.menuItemRxs.Index = 25;
			this.menuItemRxs.Text = "   Prescriptions";
			this.menuItemRxs.Click += new System.EventHandler(this.menuItemRxs_Click);
			// 
			// menuItemProcCodes
			// 
			this.menuItemProcCodes.Index = 26;
			this.menuItemProcCodes.Text = "   Procedure Codes";
			this.menuItemProcCodes.Click += new System.EventHandler(this.menuItemProcCodes_Click);
			// 
			// menuItemRefs
			// 
			this.menuItemRefs.Index = 27;
			this.menuItemRefs.Text = "   Referrals";
			this.menuItemRefs.Click += new System.EventHandler(this.menuItemRefs_Click);
			// 
			// menuItemPHSep
			// 
			this.menuItemPHSep.Index = 28;
			this.menuItemPHSep.Text = "-";
			// 
			// menuItemPHRawScreen
			// 
			this.menuItemPHRawScreen.Index = 29;
			this.menuItemPHRawScreen.Text = "Raw Screening Data";
			this.menuItemPHRawScreen.Click += new System.EventHandler(this.menuItemPHRawScreen_Click);
			// 
			// menuItemPHRawPop
			// 
			this.menuItemPHRawPop.Index = 30;
			this.menuItemPHRawPop.Text = "Raw Population Data";
			this.menuItemPHRawPop.Click += new System.EventHandler(this.menuItemPHRawPop_Click);
			// 
			// menuItemPHScreen
			// 
			this.menuItemPHScreen.Index = 31;
			this.menuItemPHScreen.Text = "Screening Report";
			this.menuItemPHScreen.Click += new System.EventHandler(this.menuItemPHScreen_Click);
			// 
			// menuItemCourseGrades
			// 
			this.menuItemCourseGrades.Index = 32;
			this.menuItemCourseGrades.Text = "Dental School Course Grades";
			this.menuItemCourseGrades.Click += new System.EventHandler(this.menuItemCourseGrades_Click);
			// 
			// menuItemTools
			// 
			this.menuItemTools.Index = 5;
			this.menuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									this.menuItemPrintScreen,
																																									this.menuItem1,
																																									this.menuItem9,
																																									this.menuItemPaymentPlans,
																																									this.menuItemAging,
																																									this.menuItemFinanceCharge,
																																									this.menuItemBilling,
																																									this.menuItemTranslation,
																																									this.menuItemScreening});
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
																																							this.menuTelephone,
																																							this.menuItemPatientImport});
			this.menuItem1.Text = "Misc Tools";
			// 
			// menuItemCheckDatabase
			// 
			this.menuItemCheckDatabase.Index = 0;
			this.menuItemCheckDatabase.Text = "Check Database Integrity";
			this.menuItemCheckDatabase.Click += new System.EventHandler(this.menuItemCheckDatabase_Click);
			// 
			// menuTelephone
			// 
			this.menuTelephone.Index = 1;
			this.menuTelephone.Text = "Telephone Numbers";
			this.menuTelephone.Click += new System.EventHandler(this.menuTelephone_Click);
			// 
			// menuItemPatientImport
			// 
			this.menuItemPatientImport.Index = 2;
			this.menuItemPatientImport.Text = "Patient Import";
			this.menuItemPatientImport.Click += new System.EventHandler(this.menuItemPatientImport_Click);
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
			// menuItemTranslation
			// 
			this.menuItemTranslation.Index = 7;
			this.menuItemTranslation.Text = "Language Translation";
			this.menuItemTranslation.Click += new System.EventHandler(this.menuItemTranslation_Click);
			// 
			// menuItemScreening
			// 
			this.menuItemScreening.Index = 8;
			this.menuItemScreening.Text = "Public Health Screening";
			this.menuItemScreening.Click += new System.EventHandler(this.menuItemScreening_Click);
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.Index = 6;
			this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.menuItemHelpWindows,
																																								 this.menuItemHelpContents,
																																								 this.menuItemHelpIndex,
																																								 this.menuItemRemote,
																																								 this.menuItemUpdate});
			this.menuItemHelp.Text = "&Help";
			// 
			// menuItemHelpWindows
			// 
			this.menuItemHelpWindows.Index = 0;
			this.menuItemHelpWindows.Text = "Local Help-Windows";
			this.menuItemHelpWindows.Click += new System.EventHandler(this.menuItemHelpWindows_Click);
			// 
			// menuItemHelpContents
			// 
			this.menuItemHelpContents.Index = 1;
			this.menuItemHelpContents.Text = "Online Help - Contents";
			this.menuItemHelpContents.Click += new System.EventHandler(this.menuItemHelpContents_Click);
			// 
			// menuItemHelpIndex
			// 
			this.menuItemHelpIndex.Index = 2;
			this.menuItemHelpIndex.Shortcut = System.Windows.Forms.Shortcut.ShiftF1;
			this.menuItemHelpIndex.Text = "Online Help - Index";
			this.menuItemHelpIndex.Click += new System.EventHandler(this.menuItemHelpIndex_Click);
			// 
			// menuItemRemote
			// 
			this.menuItemRemote.Index = 3;
			this.menuItemRemote.Text = "Remote Connection";
			this.menuItemRemote.Click += new System.EventHandler(this.menuItemRemote_Click);
			// 
			// menuItemUpdate
			// 
			this.menuItemUpdate.Index = 4;
			this.menuItemUpdate.Text = "&Update";
			this.menuItemUpdate.Click += new System.EventHandler(this.menuItemUpdate_Click);
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
			this.imageList32.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList32.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
			this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ContrManage2
			// 
			this.ContrManage2.Location = new System.Drawing.Point(177, 0);
			this.ContrManage2.Name = "ContrManage2";
			this.ContrManage2.Size = new System.Drawing.Size(732, 548);
			this.ContrManage2.TabIndex = 19;
			// 
			// FormOpenDental
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(880, 690);
			this.Controls.Add(this.pictButtons);
			this.Controls.Add(this.myOutlookBar);
			this.Controls.Add(this.ContrAppt2);
			this.Controls.Add(this.ContrManage2);
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
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormOpenDental_Closing);
			this.Load += new System.EventHandler(this.FormOpenDental_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormOpenDental_Layout);
			this.ResumeLayout(false);

		}
		#endregion
	
		[STAThread]
		static void Main() {
			//Process[] processes=Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
			if(Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length>1){
			//if(processes.Length>1){
				//MessageBox.Show("Already running");
				//if an instance of the program is already running.
				IsSecondInstance=true;
				//MessageBox.Show(WindowsIdentity.GetCurrent().Name);
				//for(int i=0;i<processes.Length;i++){
				//	MessageBox.Show(processes[i].StartInfo.
				//}
				//return;//then don't open another
			}
			Application.EnableVisualStyles();//changes appearance to XP
			Application.DoEvents();//workaround for a known MS bug in the line above
			Application.Run(new FormOpenDental());
		}

		private void FormOpenDental_Load(object sender, System.EventArgs e){
			allNeutral();
			ExitApplicationNow.WantsToExit+=new GenericEventHandler(ExitApplicationNow_WantsToExit);
			FormChooseDatabase FormCfg=new FormChooseDatabase();
			FormCfg.GetConfig();
			DataClass.SetConnection();
      if(!Prefs.TryToConnect()){
				FormCfg.IsInStartup=true;
				FormCfg.ShowDialog();
				if(FormCfg.DialogResult==DialogResult.Cancel){
			    //ExitApplicationNow ExitApplicationNow2=new ExitApplicationNow();
				  ExitApplicationNow.ExitNow();
					return;
				}     
			}
			if(!RefreshLocalData(InvalidTypes.AllLocal,true))
				return;
			Lan.Refresh();//automatically skips if current language is English
			LanguageForeigns.Refresh();//automatically skips if current language is English
			if(IsSecondInstance){
				if(!Prefs.GetBool("AllowMultipleCopiesOfProgram")){
					MsgBox.Show(this,"Open Dental is already running");
					//ExitApplicationNow ExitApplicationNow2=new ExitApplicationNow();
				  ExitApplicationNow.ExitNow();
					return;
				}
				if(!MsgBox.Show(this,true,"You will have multiple copies of Open Dental running off the same computer. Only the first copy will have synchronization enabled. Continue?"))
				{
					//ExitApplicationNow ExitApplicationNow2=new ExitApplicationNow();
				  ExitApplicationNow.ExitNow();
					return;
				}
			}
			GraphicTypes.Refresh();
			GraphicAssemblies.Refresh();
			GraphicElements.Refresh();
			GraphicShapes.Refresh();
			//Batch.Select("graphictype,graphicassembly,graphicelement,graphicshape");
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
			DataValid.BecameInvalid += new OpenDental.ValidEventHandler(DataValid_BecameInvalid);

			ContrAccount2.InstantClasses();
			ContrAppt2.InstantClasses();
			ContrChart2.InstantClasses();
			ContrDocs2.InstantClasses();
			ContrFamily2.InstantClasses();
			ContrManage2.InstantClasses();
			ContrTreat2.InitializeOnStartup();
			//ContrAppt2.Visible=true;
			//this.ActiveControl=this.ContrAppt2;
			//ContrAppt2.ModuleSelected(0);
			Thread2=new Thread(new ThreadStart(Listen));
			if(!IsSecondInstance){
				if(!Prefs.GetBool("AutoRefreshIsDisabled"))
					Thread2.Start();
			}
			timerTimeIndic.Enabled=true;
			myOutlookBar.Buttons[0].Caption=Lan.g(this,"Appts");
			myOutlookBar.Buttons[1].Caption=Lan.g(this,"Family");
			myOutlookBar.Buttons[2].Caption=Lan.g(this,"Account");
			myOutlookBar.Buttons[3].Caption=Lan.g(this,"Treat' Plan");
			//myOutlookBar.Buttons[4].Caption=Lan.g(this,"Chart");done in RefreshLocalData
			myOutlookBar.Buttons[5].Caption=Lan.g(this,"Images");
			myOutlookBar.Buttons[6].Caption=Lan.g(this,"Manage");
			foreach(MenuItem menuItem in mainMenu.MenuItems){
				TranslateMenuItem(menuItem);
			}
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				CultureInfo cInfo=(CultureInfo)CultureInfo.CurrentCulture.Clone();
				cInfo.DateTimeFormat.ShortDatePattern="MM/dd/yyyy";
				Application.CurrentCulture=cInfo;
			}
			if(CultureInfo.CurrentCulture.TwoLetterISOLanguageName=="en"){
				menuItemTranslation.Visible=false;
			}
			if(!File.Exists("Help.chm")){
				menuItemHelpWindows.Visible=false;
			}
			if(!File.Exists("remoteclient.exe")){
				menuItemRemote.Visible=false;
			}
			FormLogOn FormL=new FormLogOn();
			FormL.ShowDialog();
			if(FormL.DialogResult==DialogResult.Cancel){
				ExitApplicationNow.ExitNow();
				return;
			}
			myOutlookBar.SelectedIndex=Security.GetModule(0);
			myOutlookBar.Invalidate();
			SetModuleSelected();
			if(myOutlookBar.SelectedIndex==-1){
				MsgBox.Show(this,"You do not have permission to use any modules.");
			}
		}

		///<summary>Refreshes certain rarely used data from database.  Must supply the types of data to refresh as flags.  Also performs a few other tasks that must be done when local data is changed.</summary>
		private bool RefreshLocalData(InvalidTypes itypes,bool isStartingUp){
			if((itypes & InvalidTypes.Prefs)==InvalidTypes.Prefs){
				Prefs.Refresh();
				if(isStartingUp){
					if(!Prefs.CheckMySqlVersion()){
						return false;
					}
					if(!Prefs.ConvertDB()){
						return false;
					}
					if(!Directory.Exists(((Pref)Prefs.HList["DocPath"]).ValueString)
						|| !Directory.Exists(((Pref)Prefs.HList["DocPath"]).ValueString+"A"))
					{
						//PermissionsOld.Refresh();
						//UserPermissions.Refresh();
						//Providers.Refresh();
						//Employees.Refresh();
						Users.Refresh();
						FormPath FormP=new FormPath();
						FormP.ShowDialog();
						if(FormP.DialogResult!=DialogResult.OK){
							return false;
						}
						else{
							Prefs.Refresh();//because listening thread not started yet.
						}
					}
					if(!Prefs.CheckProgramVersion()){
						return false;
					}
				}
				if(((Pref)Prefs.HList["EasyHidePublicHealth"]).ValueString=="1"){
					menuItemSchools.Visible=false;
					menuItemCounties.Visible=false;
					menuItemScreening.Visible=false;
					menuItemPHSep.Visible=false;
					menuItemPHRawScreen.Visible=false;
					menuItemPHRawPop.Visible=false;
					menuItemPHScreen.Visible=false;
				}
				else{
					menuItemSchools.Visible=true;
					menuItemCounties.Visible=true;
					menuItemScreening.Visible=true;
					menuItemPHSep.Visible=true;
					//menuItemPublicHealth.Visible=true;
					menuItemPHRawScreen.Visible=true;
					menuItemPHRawPop.Visible=true;
					menuItemPHScreen.Visible=true;
				}
				if(Prefs.GetBool("EasyNoClinics")){
					menuItemClinics.Visible=false;
				}
				else{
					menuItemClinics.Visible=true;
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
				if(Prefs.GetBool("EasyHideDentalSchools")){
					menuItemSchoolClass.Visible=false;
					menuItemSchoolCourses.Visible=false;
					menuItemInstructors.Visible=false;
					menuItemCourseGrades.Visible=false;
				}
				else{
					menuItemSchoolClass.Visible=true;
					menuItemSchoolCourses.Visible=true;
					menuItemInstructors.Visible=true;
					menuItemCourseGrades.Visible=true;
				}
			}//if(InvalidTypes.Prefs)
			//if(!BackupJobs.IsBackup()){
			//	MessageBox.Show(Lan.g(this,"Missing Pref 'IsDatabaseBackup', Must update database."));
			//}
			if((itypes & InvalidTypes.AutoCodes)==InvalidTypes.AutoCodes){
				AutoCodes.Refresh();
				AutoCodeItems.Refresh();
				AutoCodeConds.Refresh();
			}
			//BackupJobs.Refresh();
			if((itypes & InvalidTypes.Carriers)==InvalidTypes.Carriers){
				Carriers.Refresh();//run on startup, after telephone reformat, after list edit.
			}
			if((itypes & InvalidTypes.ClaimForms)==InvalidTypes.ClaimForms){
				ClaimFormItems.Refresh();
				ClaimForms.Refresh();
			}
			
			if((itypes & InvalidTypes.ClearHouses)==InvalidTypes.ClearHouses){
				Clearinghouses.Refresh();
			}
			if((itypes & InvalidTypes.Computers)==InvalidTypes.Computers){
				Computers.Refresh();
				Printers.Refresh();
			}
			if((itypes & InvalidTypes.Defs)==InvalidTypes.Defs){
				Defs.Refresh();
			}
			if((itypes & InvalidTypes.DentalSchools)==InvalidTypes.DentalSchools){
				Instructors.Refresh();
				SchoolClasses.Refresh();
				SchoolCourses.Refresh();
			}
			if((itypes & InvalidTypes.Email)==InvalidTypes.Email){
				EmailTemplates.Refresh();
			}
			if((itypes & InvalidTypes.Employees)==InvalidTypes.Employees){
				Employees.Refresh();
			}
			if((itypes & InvalidTypes.Fees)==InvalidTypes.Fees){
				Fees.Refresh();
			}
			if((itypes & InvalidTypes.InsCats)==InvalidTypes.InsCats){
				CovCats.Refresh();
				CovSpans.Refresh();
			}
			if((itypes & InvalidTypes.Letters)==InvalidTypes.Letters){
				Letters.Refresh();
			}
			if((itypes & InvalidTypes.LetterMerge)==InvalidTypes.LetterMerge){
				LetterMergeFields.Refresh();
				LetterMerges.Refresh();
			}
			if((itypes & InvalidTypes.ProcButtons)==InvalidTypes.ProcButtons){
				ProcButtons.Refresh();
				ProcButtonItems.Refresh();
			}
			if((itypes & InvalidTypes.ProcCodes)==InvalidTypes.ProcCodes){
				ProcedureCodes.Refresh();
			}
			if((itypes & InvalidTypes.Programs)==InvalidTypes.Programs){
				Programs.Refresh();
				ProgramProperties.Refresh();
				menuItemPracticeWebReports.Visible=Programs.IsEnabled("PracticeWebReports");
			}
			if((itypes & InvalidTypes.Providers)==InvalidTypes.Providers){
				Providers.Refresh();
				ProviderIdents.Refresh();
				Clinics.Refresh();
			}
			if((itypes & InvalidTypes.QuickPaste)==InvalidTypes.QuickPaste){
				QuickPasteNotes.Refresh();
				QuickPasteCats.Refresh();
			}
			//if((itypes & InvalidTypes)==InvalidTypes){
			//	Reports.Refresh();
			//}
			if((itypes & InvalidTypes.Security)==InvalidTypes.Security){
				Users.Refresh();
				UserGroups.Refresh();
				GroupPermissions.Refresh();
			}
			if((itypes & InvalidTypes.Sched)==InvalidTypes.Sched){
				SchedDefaults.Refresh();//assumed to change rarely
				//Schedules.Refresh();//Schedules are refreshed as needed.  Not here.
			}
			if((itypes & InvalidTypes.Startup)==InvalidTypes.Startup){
				Employers.Refresh();//only needed when opening the prog. After that, automated.
				ElectIDs.Refresh();//only run on startup
				Referrals.Refresh();//Referrals are also refreshed dynamically.
			}
			if((itypes & InvalidTypes.ToolBut)==InvalidTypes.ToolBut){
				ToolButItems.Refresh();
				ContrAccount2.LayoutToolBar();
				ContrAppt2.LayoutToolBar();
				ContrChart2.LayoutToolBar();
				ContrDocs2.LayoutToolBar();
				ContrFamily2.LayoutToolBar();
			}
			if((itypes & InvalidTypes.Views)==InvalidTypes.Views){
				ApptViews.Refresh();
				ApptViewItems.Refresh();
				ContrAppt2.FillViews();
			}
			if((itypes & InvalidTypes.ZipCodes)==InvalidTypes.ZipCodes){
				ZipCodes.Refresh();
			}
			ContrTreat2.InitializeLocalData();//easier to leave this here for now than to split it.
			return true;
		}

		//private void AddLettersToMenu(){
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
		//}

		private void FormOpenDental_Layout(object sender, System.Windows.Forms.LayoutEventArgs e){
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
			ContrManage2.Location=new Point(myOutlookBar.Width,0);
			ContrManage2.Width=this.ClientSize.Width-ContrManage2.Location.X;
			ContrManage2.Height=this.ClientSize.Height;
			//ContrStaff2.Location=new Point(myOutlookBar.Width,0);
			//ContrStaff2.Width=this.ClientSize.Width-ContrStaff2.Location.X;
			//ContrStaff2.Height=this.ClientSize.Height;
			ContrTreat2.Location=new Point(myOutlookBar.Width,0);
			ContrTreat2.Width=this.ClientSize.Width-ContrDocs2.Location.X;
			ContrTreat2.Height=this.ClientSize.Height;
		}

		
		///<summary>This is called when any local data becomes outdated.  It's purpose is to tell the other computers to update certain local data.</summary>
		private void DataValid_BecameInvalid(OpenDental.ValidEventArgs e){
			if(e.OnlyLocal){
				RefreshLocalData(InvalidTypes.AllLocal,true);//does local computer only
			}
			if(e.ITypes!=InvalidTypes.Date){
				//local refresh for dates is handled within ContrAppt, not here
				RefreshLocalData(e.ITypes,false);//does local computer
			}
			if(Prefs.GetBool("AutoRefreshIsDisabled"))
				return;
			ODMessage msg=new ODMessage(
				e.ITypes,
				Appointments.DateSelected,//ignored if ITypes not InvalidTypes.Date
				"Invalid","",0,0,false);
			Messages.SendMessage(msg);
		}

		///<summary>Happens when any of the modules changes the current patient.  The calling module should then refresh itself.  The current patNum is stored here in the parent form so that when switching modules, the parent form knows which patient to call up for that module.</summary>
		private void Contr_PatientSelected(object sender, PatientSelectedEventArgs e){
			CurPatNum=e.PatNum;
		}

		private void GotoModule_ModuleSelected(ModuleEventArgs e){
			if(e.DateSelected.Year>1880){
				Appointments.DateSelected=e.DateSelected;
			}
			if(e.SelectedAptNum>0){
				ContrApptSingle.SelectedAptNum=e.SelectedAptNum;
			}
			//patient would have been set separately ahead of time
			//CurPatNum=Appointments.Cur.PatNum;
			UnselectActive();
			allNeutral();
			if(e.IModule!=-1){
				myOutlookBar.SelectedIndex=e.IModule;
			}
			SetModuleSelected();
			myOutlookBar.Invalidate();
			
		}

		///<summary>Needs to be rewritten. Use the Application.ApplicationExit event instead.</summary>
		private void ExitApplicationNow_WantsToExit(System.EventArgs e){
			if(Thread2!=null){
				Thread2.Abort();
				this.TcpListener2.Stop();
			}
			Application.Exit();
		}

		///<summary>separate thread</summary>
		public void Listen(){
			IPAddress ipAddress=Dns.Resolve(Dns.GetHostName()).AddressList[0];
			string addrStr=ipAddress.ToString();
			TcpListener2=new TcpListener(ipAddress,2123);
			TcpListener2.Start();
			while(true){
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
			ODMessage msg=Messages.RecMessage(text);//parses the xml
			if(msg.MessageType=="Invalid"){
				switch(msg.ITypes){
					case InvalidTypes.Date:
						if(Appointments.DateSelected.Date==msg.DateViewing.Date
							&& ContrAppt2.Visible){
							ContrAppt2.RefreshModuleScreen();
						}
						break;
					default://local data
						RefreshLocalData((InvalidTypes)msg.ITypes,false);
						break;
				}
			}
			else if(msg.MessageType=="Button"){
				Graphics grfx=Graphics.FromImage(buttonsShadow);
				int row=msg.Row;
				int col=msg.Col;
				if(col==0 && msg.Pushed){//button in first col pushed
					buttonDown[0,row]=true;
					grfx.FillRectangle(new SolidBrush(Color.Red),col*18+1,row*18+1,17,17);
					pictButtons.Image=buttonsShadow;
					pictButtons.Refresh();
					PlaySoundFunct(col,row,false);
				}
				else if(col==1 && msg.Pushed){//button in second col pushed
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
			}
			else{//Text
				PlaySoundFunct(0,0,true);
				FormMessageText FormMT=new FormMessageText();
				FormMT.Text2.Text=msg.Text;
				FormMT.ShowDialog();
				ContrManage2.LogMsg(msg.Text);
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

		private void myOutlookBar_ButtonClicked(object sender, OpenDental.ButtonClicked_EventArgs e){
			switch(myOutlookBar.SelectedIndex){
				case 0:
					if(!Security.IsAuthorized(Permissions.AppointmentsModule)){
						e.Cancel=true;
						return;
					}
					break;
				case 1:
					if(!Security.IsAuthorized(Permissions.FamilyModule)){
						e.Cancel=true;
						return;
					}
					break;
				case 2:
					if(!Security.IsAuthorized(Permissions.AccountModule)){
						e.Cancel=true;
						return;
					}
					break;
				case 3:
					if(!Security.IsAuthorized(Permissions.TPModule)){
						e.Cancel=true;
						return;
					}
					break;
				case 4:
					if(!Security.IsAuthorized(Permissions.ChartModule)){
						e.Cancel=true;
						return;
					}
					break;
				case 5:
					if(!Security.IsAuthorized(Permissions.ImagesModule)){
						e.Cancel=true;
						return;
					}
					break;
				case 6:
					if(!Security.IsAuthorized(Permissions.ManageModule)){
						e.Cancel=true;
						return;
					}
					break;
			}
			UnselectActive();
			allNeutral();
			SetModuleSelected();
		}

		///<summary>Sets the currently selected module based on the selectedIndex of the outlook bar. If selectedIndex is -1, which might happen if user does not have permission to any module, then this does nothing.</summary>
		private void SetModuleSelected(){
			switch(myOutlookBar.SelectedIndex){
				case 0:
					ContrAppt2.Visible=true;
					this.ActiveControl=this.ContrAppt2;
					ContrAppt2.ModuleSelected(CurPatNum);
					break;
				case 1:
					ContrFamily2.Visible=true;
					this.ActiveControl=this.ContrFamily2;
					ContrFamily2.ModuleSelected(CurPatNum);
					break;
				case 2:
					ContrAccount2.Visible=true;
					this.ActiveControl=this.ContrAccount2;
					ContrAccount2.ModuleSelected(CurPatNum);
					break;
				case 3:
					ContrTreat2.Visible=true;
					this.ActiveControl=this.ContrTreat2;
					ContrTreat2.ModuleSelected(CurPatNum);
					break;
				case 4:
					ContrChart2.Visible=true;
					this.ActiveControl=this.ContrChart2;
					ContrChart2.ModuleSelected(CurPatNum);
					break;
				case 5:
					ContrDocs2.Visible=true;
					this.ActiveControl=this.ContrDocs2;
					ContrDocs2.ModuleSelected(CurPatNum);
					break;
				case 6:
					ContrManage2.Visible=true;
					this.ActiveControl=this.ContrManage2;
					ContrManage2.ModuleSelected();
					break;
				case 7:
					//ContrAppt2.Visible=true;
					//this.ActiveControl=this.ContrAppt2;
					//ContrAppt2.ModuleSelected();
					break;
			}
		}

		private void allNeutral(){
			ContrAppt2.Visible=false;
			ContrFamily2.Visible=false;
			ContrAccount2.Visible=false;
			ContrTreat2.Visible=false;
			ContrChart2.Visible=false;
			ContrDocs2.Visible=false;
			ContrManage2.Visible=false;
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
			if(ContrDocs2.Visible)
				ContrDocs2.ModuleUnselected();
			//ContrStaff2.Visible=false;
		}

		private void RefreshCurrentModule(){
			if(ContrAppt2.Visible)
				ContrAppt2.ModuleSelected(CurPatNum);
			if(ContrFamily2.Visible)
				ContrFamily2.ModuleSelected(CurPatNum);
			if(ContrAccount2.Visible)
				ContrAccount2.ModuleSelected(CurPatNum);
			if(ContrTreat2.Visible)
				ContrTreat2.ModuleSelected(CurPatNum);
			if(ContrChart2.Visible)
				ContrChart2.ModuleSelected(CurPatNum);
			if(ContrDocs2.Visible)
				ContrDocs2.ModuleSelected(CurPatNum);
		}

		private void pictButtons_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			int row=e.Y/18;
			int col=e.X/18;
			if(row>5) row=5;
			if(col>1) col=1;
			bool pushed;
			Graphics grfx=Graphics.FromImage(buttonsShadow);
			if(col==0 && !buttonDown[0,row]){//button in first col, currently not down
				buttonDown[0,row]=true;
				grfx.FillRectangle(new SolidBrush(Color.Red),col*18+1,row*18+1,17,17);
				pictButtons.Image=buttonsShadow;
				pictButtons.Refresh();
				pushed=true;
				PlaySoundFunct(col,row,false);
			}
			else if(col==1 && !buttonDown[1,row]){//button in second col, currently not down
				grfx.FillRectangle(new SolidBrush(Color.Red),col*18+1,row*18+1,17,17);
				pictButtons.Image=buttonsShadow;
				pictButtons.Refresh();
				pushed=true;
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
				pushed=false;
			}
			ODMessage msg=new ODMessage(InvalidTypes.None,DateTime.MinValue,"Button"," ",row,col,pushed);
			Messages.SendMessage(msg);
		}

		/// <summary>sends function key presses to the appointment module</summary>
		private void FormOpenDental_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(ContrAppt2.Visible && e.KeyCode>=Keys.F1 && e.KeyCode<=Keys.F12)
				ContrAppt2.FunctionKeyPress(e.KeyCode);
		}

		private void FormOpenDental_Closing(object sender, System.ComponentModel.CancelEventArgs e){
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
		private void menuItemLogOff_Click(object sender, System.EventArgs e) {
			LastModule=myOutlookBar.SelectedIndex;
			myOutlookBar.SelectedIndex=-1;
			myOutlookBar.Invalidate();
			UnselectActive();
			allNeutral();
			FormLogOn FormL=new FormLogOn();
			FormL.ShowDialog();
			if(FormL.DialogResult==DialogResult.Cancel){
				ExitApplicationNow.ExitNow();
				return;
			}
			myOutlookBar.SelectedIndex=Security.GetModule(LastModule);
			myOutlookBar.Invalidate();
			SetModuleSelected();
			if(myOutlookBar.SelectedIndex==-1){
				MsgBox.Show(this,"You do not have permission to use any modules.");
			}
		}

		//File
		private void menuItemPrinter_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormPrinterSetup FormPS=new FormPrinterSetup();
			FormPS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Printers");
		}

		private void menuItemScanner_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormScannerSetup FormSS=new FormScannerSetup();
			FormSS.ShowDialog();		
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Scanner");
		}

		private void menuItemConfig_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.ChooseDatabase)){
				return;
			}
			FormChooseDatabase FormC=new FormChooseDatabase();
			FormC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.ChooseDatabase,"");
			if(FormC.DialogResult==DialogResult.Cancel){
				return;
			}
			CurPatNum=0;
			//RefreshCurrentModule();//clumsy but necessary. Sets local PatNums to null.
			RefreshLocalData(InvalidTypes.AllLocal,true);
			//RefreshCurrentModule();
			menuItemLogOff_Click(this,e);//this is a quick shortcut.
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
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormApptViews FormAV=new FormApptViews();
			FormAV.ShowDialog();
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Appointment Views");
		}

		private void menuItemAutoCodes_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormAutoCode FormAC=new FormAutoCode();
			FormAC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Auto Codes");
		}

		private void menuItemClaimForms_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormClaimForms FormCF=new FormClaimForms();
			FormCF.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Claim Forms");
		}

		private void menuItemClearinghouses_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormClearinghouses FormC=new FormClearinghouses();
			FormC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Clearinghouses");
		}

		private void menuItemComputers_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormComputers FormC=new FormComputers();
			FormC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Computers");
		}

		private void menuItemDataPath_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormPath FormP=new FormPath();
			FormP.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Data Path");	
		}

		private void menuItemDefinitions_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormDefinitions FormD=new FormDefinitions(DefCat.AccountColors);//just the first cat.
			FormD.ShowDialog();
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Definitions");
		}

		private void menuItemEasy_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormEasy FormE=new FormEasy();
			FormE.ShowDialog();
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Easy Options");
		}

		private void menuItemEmail_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormEmailSetup FormE=new FormEmailSetup();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Email");
		}

		private void menuItemInsCats_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormInsCatsSetup FormE=new FormInsCatsSetup();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Insurance Categories");
		}

		private void menuItemMisc_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormMisc FormM=new FormMisc();
			FormM.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Misc");
		}

		private void menuItemPractice_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormPractice FormPr=new FormPractice();
			FormPr.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Practice Info");
		}

		private void menuItemProcedureButtons_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormProcButtons FormPB=new FormProcButtons();
			FormPB.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Procedure Buttons");	
		}

		private void menuItemLinks_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormProgramLinks FormPL=new FormProgramLinks();
			FormPL.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Program Links");
		}

		private void menuItemRecall_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormRecallSetup FormRS=new FormRecallSetup();
			FormRS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Recall");	
		}

		private void menuItemSecurity_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.SecurityAdmin)){
				return;
			}
			FormSecurity FormS=new FormSecurity(); 
			FormS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.SecurityAdmin,"");
		}
		
		//Setup-Schedules
		private void menuItemPracDef_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Schedules)){
				return;
			}
			FormSchedDefault FormSD=new FormSchedDefault(ScheduleType.Practice);
			FormSD.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Schedules,"Practice Default");
		}

		private void menuItemPracSched_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Schedules)){
				return;
			}
			FormSchedPractice FormSP=new FormSchedPractice(ScheduleType.Practice);
			FormSP.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Schedules,"Practice");
		}

		private void menuItemProvDefault_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Schedules)){
				return;
			}
			FormSchedDefault FormSD=new FormSchedDefault(ScheduleType.Provider);
			FormSD.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Schedules,"Provider Default");	
		}

		private void menuItemProvSched_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Schedules)){
				return;
			}
			FormSchedPractice FormSP=new FormSchedPractice(ScheduleType.Provider);
			FormSP.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Schedules,"Provider");
		}

		private void menuItemBlockoutDefault_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Blockouts)){
				return;
			}
			FormSchedDefault FormSD=new FormSchedDefault(ScheduleType.Blockout);
			FormSD.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Blockouts,"Default");	
		}

		//Lists
		
		private void menuItemClinics_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormClinics FormC=new FormClinics();
			FormC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Clinics");
		}
		
		private void menuItemContacts_Click(object sender, System.EventArgs e) {
			FormContacts FormC=new FormContacts();
			FormC.ShowDialog();
		}

		private void menuItemCounties_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormCounties FormC=new FormCounties();
			FormC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Counties");
		}

		private void menuItemSchoolClass_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormSchoolClasses FormS=new FormSchoolClasses();
			FormS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Dental School Classes");
		}

		private void menuItemSchoolCourses_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormSchoolCourses FormS=new FormSchoolCourses();
			FormS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Dental School Courses");
		}

		private void menuItemEmployees_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormEmployee FormEmp=new FormEmployee();
			FormEmp.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Employees");	
		}

		private void menuItemEmployers_Click(object sender, System.EventArgs e) {
			FormEmployers FormE=new FormEmployers();
			FormE.ShowDialog();
		}

		private void menuItemInstructors_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormInstructors FormI=new FormInstructors();
			FormI.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Dental School Instructors");
		}

		private void menuItemCarriers_Click(object sender, System.EventArgs e) {
			FormCarriers FormC=new FormCarriers();
			FormC.ShowDialog();
			RefreshCurrentModule();
		}

		private void menuItemInsPlans_Click(object sender, System.EventArgs e) {
			FormInsPlans FormIP = new FormInsPlans();
			FormIP.ShowDialog();
			RefreshCurrentModule();
		}

		private void menuItemMedications_Click(object sender, System.EventArgs e) {
			FormMedications FormM=new FormMedications();
			FormM.ShowDialog();
		}

		private void menuItemProviders_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormProviderSelect FormPS=new FormProviderSelect();
			FormPS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Providers");		
		}

		private void menuItemPrescriptions_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormRxSetup FormRxSetup2=new FormRxSetup();
			FormRxSetup2.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Rx");		
		}

		private void menuItemReferrals_Click(object sender, System.EventArgs e) {
			FormReferralSelect FormRS=new FormReferralSelect();
			FormRS.ShowDialog();		
		}

		private void menuItemSchools_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormSchools FormS=new FormSchools();
			FormS.ShowDialog();
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Dental Schools");
		}

		private void menuItemZipCodes_Click(object sender, System.EventArgs e) {
			//if(!Security.IsAuthorized(Permissions.Setup)){
			//	return;
			//}
			FormZipCodes FormZ=new FormZipCodes();
			FormZ.ShowDialog();
			//SecurityLogs.MakeLogEntry(Permissions.Setup,"Zip Codes");
		}

		//Lists-ProcedureCodes
		private void menuItemEditCode_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormProcedures FormProcedures = new FormProcedures();
			FormProcedures.Mode=FormProcMode.Edit;
			FormProcedures.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Procedure Codes");
		}

		private void menuItemViewCode_Click(object sender, System.EventArgs e) {
			FormProcedures FormProcedures = new FormProcedures();
			FormProcedures.Mode=FormProcMode.View;
			FormProcedures.ShowDialog();	
		}

		//Reports
		private void menuItemUserQuery_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.UserQuery)){
				return;
			}
			FormQuery FormQuery2=new FormQuery();
			FormQuery2.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.UserQuery,"");
		}

		private void menuItemPracticeWebReports_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			try{
				Process.Start("PWReports.exe");
			}
			catch{
				MessageBox.Show("PracticeWeb Reports module unavailable.");
			}
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Practice Web");
		}

		private void menuItemRpProdInc_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpProdInc FormPI=new FormRpProdInc();
			FormPI.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Production and Income");
		}

		//Reports-Daily
		private void menuItemRpAdj_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpAdjSheet FormAdjSheet=new FormRpAdjSheet();
			FormAdjSheet.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Adjustments");
		}

		private void menuItemRpDepSlip_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpDepositSlip FormDS=new FormRpDepositSlip();
			FormDS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Deposit Slip");
		}

		private void menuItemRpPay_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpPaySheet FormPaySheet=new FormRpPaySheet();
			FormPaySheet.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Daily Payments");
		}
		
		private void menuItemRpProc_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpProcSheet FormProcSheet=new FormRpProcSheet();
			FormProcSheet.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Daily Procedures");	
		}

		private void menuItemRpProcNote_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpProcNote FormPN=new FormRpProcNote();
			FormPN.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Daily Procedure Notes");
		}

		//Reports-Monthly
		private void menuItemRpAging_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpAging FormA=new FormRpAging();
			FormA.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Aging");
		}

		private void menuItemRpClaimsNotSent_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpClaimNotSent FormClaim=new FormRpClaimNotSent();
			FormClaim.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Claims Not Sent");
		}

		private void menuItemRpCapitation_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpCapitation FormC=new FormRpCapitation();
			FormC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Capitation");
		}

		private void menuItemRpFinanceCharge_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpFinanceCharge FormRpFinance=new FormRpFinanceCharge();
			FormRpFinance.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Finance Charges");
		}

		private void menuItemRpOutInsClaims_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpOutInsClaims FormOut=new FormRpOutInsClaims();
			FormOut.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Outstanding Insurance Claims");
		}

		private void menuItemRpProcNoBilled_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpProcNotBilledIns FormProc=new FormRpProcNotBilledIns();
			FormProc.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Procedures not billed to insurance.");
		}

		//Reports-Lists
		private void menuAppointments_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpAppointments FormA=new FormRpAppointments();
			FormA.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Appointments");
		}

		private void menuItemBirthdays_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpBirthday FormB=new FormRpBirthday();
			FormB.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Birthdays");
		}

		private void menuItemInsCarriers_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpInsCo FormInsCo=new FormRpInsCo();
			FormInsCo.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Insurance Carriers");	
		}

		private void menuItemPatList_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpPatients FormPatients=new FormRpPatients();
			FormPatients.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Patient List");				
		}

		private void menuItemRxs_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpPrescriptions FormPrescript=new FormRpPrescriptions();
			FormPrescript.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Rx");
		}

		private void menuItemProcCodes_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpProcCodes FormProcCodes=new FormRpProcCodes();
			FormProcCodes.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Procedure Codes");
		}

		private void menuItemRefs_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpReferrals FormReferral=new FormRpReferrals();
			FormReferral.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Referrals");	
		}

		//Public Health
		private void menuItemPHRawScreen_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpPHRawScreen FormPH=new FormRpPHRawScreen();
			FormPH.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"PH Raw Screening");
		}

		private void menuItemPHRawPop_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpPHRawPop FormPH=new FormRpPHRawPop();
			FormPH.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"PH Raw population");
		}

		private void menuItemPHScreen_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			MessageBox.Show("This report is incomplete.");
			//SecurityLogs.MakeLogEntry(Permissions.Reports,"");
		}

		private void menuItemCourseGrades_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Reports)){
				return;
			}
			FormRpCourseGrades FormR=new FormRpCourseGrades();
			FormR.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Reports,"Dental School Grades");
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
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormCheckDatabase FormCDB=new FormCheckDatabase();
			FormCDB.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Check Database Integrity");
		}

		private void menuTelephone_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormTelephone FormT=new FormTelephone();
			FormT.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Telephone");
		}

		private void menuItemPatientImport_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.SecurityAdmin)){
				return;
			}
			FormImport FormI=new FormImport();
			FormI.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.SecurityAdmin,"Patient Import Tool");
		}

		private void menuItemPaymentPlans_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormPayPlanUpdate FormPPU=new FormPayPlanUpdate();
			FormPPU.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Payment Plan Update");
		}

		private void menuItemAging_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormAging FormAge=new FormAging();
			FormAge.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Aging Update");
		}

		private void menuItemFinanceCharge_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormFinanceCharges FormFC=new FormFinanceCharges();
			FormFC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Run Finance Charges");
		}

		private void menuItemBilling_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormBillingOptions FormBO=new FormBillingOptions();
			FormBO.ShowDialog();
			RefreshCurrentModule();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Billing");
		}

		private void menuItemTranslation_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormTranslationCat FormTC=new FormTranslationCat();
			FormTC.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Translations");
		}

		private void menuItemScreening_Click(object sender, System.EventArgs e) {
			FormScreenings FormS=new FormScreenings();
			FormS.ShowDialog();
		}

		//Help
		private void menuItemHelpWindows_Click(object sender, System.EventArgs e) {
			try{
				Process.Start("Help.chm");
			}
			catch{
				MsgBox.Show(this,"Could not find file.");
			}
		}

		private void menuItemHelpContents_Click(object sender, System.EventArgs e) {
			try{
				Process.Start("http://www.open-dent.com/manual/toc.html");
			}
			catch{
				MsgBox.Show(this,"Could not find file.");
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

		private void menuItemRemote_Click(object sender, System.EventArgs e) {
			if(!MsgBox.Show(this,true,"A remote connection will now be attempted. Do NOT continue unless you are already on the phone with us.  Do you want to continue?"))
			{
				return;
			}
			try{
				Process.Start("remoteclient.exe");//Network streaming remote client or any other similar client
			}
			catch{
				MsgBox.Show(this,"Could not find file.");
			}
		}

		private void menuItemUpdate_Click(object sender, System.EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)){
				return;
			}
			FormUpdate FormU = new FormUpdate();
			FormU.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,"Update Version");
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
