using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Tao.Platform.Windows;
using SparksToothChart;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormToothChartingBig:System.Windows.Forms.Form {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private bool ShowBySelectedTeeth;
		private ToothInitial[] ToothInitialList;
		private GraphicalToothChart toothChart;
		private ArrayList ProcAL;

		///<summary></summary>
		public FormToothChartingBig(bool showBySelectedTeeth,ToothInitial[] toothInitialList,ArrayList procAL)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			ShowBySelectedTeeth=showBySelectedTeeth;
			ToothInitialList=toothInitialList;
			ProcAL=procAL;
			toothChart.TaoRenderEnabled=true;
			toothChart.TaoInitializeContexts();
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
			this.toothChart = new SparksToothChart.GraphicalToothChart();
			this.SuspendLayout();
			// 
			// toothChart
			// 
			this.toothChart.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(95)))),((int)(((byte)(95)))),((int)(((byte)(130)))));
			this.toothChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toothChart.Location = new System.Drawing.Point(0,0);
			this.toothChart.Name = "toothChart";
			this.toothChart.Size = new System.Drawing.Size(719,564);
			this.toothChart.TabIndex = 0;
			this.toothChart.TaoRenderEnabled = false;			
			this.toothChart.Text = "graphicalToothChart1";
			this.toothChart.UseInternational = false;
			// 
			// FormToothChartingBig
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(719,564);
			this.Controls.Add(this.toothChart);
			this.Name = "FormToothChartingBig";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResizeEnd += new System.EventHandler(this.FormToothChartingBig_ResizeEnd);
			this.Load += new System.EventHandler(this.FormToothChartingBig_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormToothChartingBig_Load(object sender,EventArgs e) {
			FillToothChart();
			//toothChart.Refresh();
		}

		private void FormToothChartingBig_ResizeEnd(object sender,EventArgs e) {
			FillToothChart();
			//toothChart.Refresh();
		}

		///<summary>This is, of course, called when module refreshed.  But it's also called when user sets missing teeth or tooth movements.  In that case, the Progress notes are not refreshed, so it's a little faster.</summary>
		private void FillToothChart() {//ArrayList procedures){
			Cursor=Cursors.WaitCursor;
			toothChart.SuspendLayout();
			toothChart.UseInternational=PrefB.GetBool("UseInternationalToothNumbers");
			toothChart.ColorBackground=Defs.Long[(int)DefCat.ChartGraphicColors][10].ItemColor;
			toothChart.ColorText=Defs.Long[(int)DefCat.ChartGraphicColors][11].ItemColor;
			toothChart.ColorTextHighlight=Defs.Long[(int)DefCat.ChartGraphicColors][12].ItemColor;
			toothChart.ColorBackHighlight=Defs.Long[(int)DefCat.ChartGraphicColors][13].ItemColor;
			//remember which teeth were selected
			ArrayList selectedTeeth=new ArrayList();//integers 1-32
			for(int i=0;i<toothChart.SelectedTeeth.Length;i++) {
				selectedTeeth.Add(Tooth.ToInt(toothChart.SelectedTeeth[i]));
			}
			toothChart.ResetTeeth();
			//if(PatCur==null) {
			if(ShowBySelectedTeeth) {
				for(int i=0;i<selectedTeeth.Count;i++) {
					toothChart.SetSelected((int)selectedTeeth[i],true);
				}
			}
			//first, primary.  That way, you can still set a primary tooth missing afterwards.
			for(int i=0;i<ToothInitialList.Length;i++) {
				if(ToothInitialList[i].InitialType==ToothInitialType.Primary) {
					toothChart.SetToPrimary(ToothInitialList[i].ToothNum);
				}
			}
			for(int i=0;i<ToothInitialList.Length;i++) {
				switch(ToothInitialList[i].InitialType) {
					case ToothInitialType.Missing:
						toothChart.SetInvisible(ToothInitialList[i].ToothNum);
						break;
					//case ToothInitialType.Primary:
					//	break;
					case ToothInitialType.Rotate:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,ToothInitialList[i].Movement,0,0,0,0,0);
						break;
					case ToothInitialType.TipM:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,ToothInitialList[i].Movement,0,0,0,0);
						break;
					case ToothInitialType.TipB:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,ToothInitialList[i].Movement,0,0,0);
						break;
					case ToothInitialType.ShiftM:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,0,ToothInitialList[i].Movement,0,0);
						break;
					case ToothInitialType.ShiftO:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,0,0,ToothInitialList[i].Movement,0);
						break;
					case ToothInitialType.ShiftB:
						toothChart.MoveTooth(ToothInitialList[i].ToothNum,0,0,0,0,0,ToothInitialList[i].Movement);
						break;
				}
			}
			DrawProcsOfStatus(ProcStat.EO);
			DrawProcsOfStatus(ProcStat.EC);
			DrawProcsOfStatus(ProcStat.C);
			DrawProcsOfStatus(ProcStat.R);
			DrawProcsOfStatus(ProcStat.TP);
			toothChart.ResumeLayout();
			Cursor=Cursors.Default;
		}

		private void DrawProcsOfStatus(ProcStat procStat) {
			Procedure proc;
			string[] teeth;
			Color cLight=Color.White;
			Color cDark=Color.White;
			for(int i=0;i<ProcAL.Count;i++) {
				proc=(Procedure)ProcAL[i];
				if(proc.ProcStatus!=procStat) {
					continue;
				}
				//if(proc.HideGraphical) {
					//We don't care about HideGraphical anymore.  It will be enhanced later to a 3-state.
					//continue;
				//}
				if(ProcedureCodes.GetProcCode(proc.ADACode).PaintType==ToothPaintingType.Extraction && (
					proc.ProcStatus==ProcStat.C
					|| proc.ProcStatus==ProcStat.EC
					|| proc.ProcStatus==ProcStat.EO
					)) {
					continue;//prevents the red X. Missing teeth already handled.
				}
				if(ProcedureCodes.GetProcCode(proc.ADACode).GraphicColor==Color.FromArgb(0)){
					switch(proc.ProcStatus) {
						case ProcStat.C:
							cDark=Defs.Short[(int)DefCat.ChartGraphicColors][1].ItemColor;
							cLight=Defs.Short[(int)DefCat.ChartGraphicColors][6].ItemColor;
							break;
						case ProcStat.TP:
							cDark=Defs.Short[(int)DefCat.ChartGraphicColors][0].ItemColor;
							cLight=Defs.Short[(int)DefCat.ChartGraphicColors][5].ItemColor;
							break;
						case ProcStat.EC:
							cDark=Defs.Short[(int)DefCat.ChartGraphicColors][2].ItemColor;
							cLight=Defs.Short[(int)DefCat.ChartGraphicColors][7].ItemColor;
							break;
						case ProcStat.EO:
							cDark=Defs.Short[(int)DefCat.ChartGraphicColors][3].ItemColor;
							cLight=Defs.Short[(int)DefCat.ChartGraphicColors][8].ItemColor;
							break;
						case ProcStat.R:
							cDark=Defs.Short[(int)DefCat.ChartGraphicColors][4].ItemColor;
							cLight=Defs.Short[(int)DefCat.ChartGraphicColors][9].ItemColor;
							break;
					}
				}
				else {
					cDark=ProcedureCodes.GetProcCode(proc.ADACode).GraphicColor;
					cLight=ProcedureCodes.GetProcCode(proc.ADACode).GraphicColor;
				}
				switch(ProcedureCodes.GetProcCode(proc.ADACode).PaintType) {
					case ToothPaintingType.BridgeDark:
						if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,proc.ToothNum)) {
							toothChart.SetPontic(proc.ToothNum,cDark);
						}
						else {
							toothChart.SetCrown(proc.ToothNum,cDark);
						}
						break;
					case ToothPaintingType.BridgeLight:
						if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,proc.ToothNum)) {
							toothChart.SetPontic(proc.ToothNum,cLight);
						}
						else {
							toothChart.SetCrown(proc.ToothNum,cLight);
						}
						break;
					case ToothPaintingType.CrownDark:
						toothChart.SetCrown(proc.ToothNum,cDark);
						break;
					case ToothPaintingType.CrownLight:
						toothChart.SetCrown(proc.ToothNum,cLight);
						break;
					case ToothPaintingType.DentureDark:
						if(proc.Surf=="U") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+2).ToString();
							}
						}
						else if(proc.Surf=="L") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+18).ToString();
							}
						}
						else {
							teeth=proc.ToothRange.Split(new char[] { ',' });
						}
						for(int t=0;t<teeth.Length;t++) {
							if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,teeth[t])) {
								toothChart.SetPontic(teeth[t],cDark);
							}
							else {
								toothChart.SetCrown(teeth[t],cDark);
							}
						}
						break;
					case ToothPaintingType.DentureLight:
						if(proc.Surf=="U") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+2).ToString();
							}
						}
						else if(proc.Surf=="L") {
							teeth=new string[14];
							for(int t=0;t<14;t++) {
								teeth[t]=(t+18).ToString();
							}
						}
						else {
							teeth=proc.ToothRange.Split(new char[] { ',' });
						}
						for(int t=0;t<teeth.Length;t++) {
							if(ToothInitials.ToothIsMissingOrHidden(ToothInitialList,teeth[t])) {
								toothChart.SetPontic(teeth[t],cLight);
							}
							else {
								toothChart.SetCrown(teeth[t],cLight);
							}
						}
						break;
					case ToothPaintingType.Extraction:
						toothChart.SetBigX(proc.ToothNum,cDark);
						break;
					case ToothPaintingType.FillingDark:
						toothChart.SetSurfaceColors(proc.ToothNum,proc.Surf,cDark);
						break;
					case ToothPaintingType.FillingLight:
						toothChart.SetSurfaceColors(proc.ToothNum,proc.Surf,cLight);
						break;
					case ToothPaintingType.Implant:
						toothChart.SetImplant(proc.ToothNum,cDark);
						break;
					case ToothPaintingType.PostBU:
						toothChart.SetBU(proc.ToothNum,cDark);
						break;
					case ToothPaintingType.RCT:
						toothChart.SetRCT(proc.ToothNum,cDark);
						break;
					case ToothPaintingType.Sealant:
						toothChart.SetSealant(proc.ToothNum,cDark);
						break;
				}
			}
		}

	

		


	}
}





















