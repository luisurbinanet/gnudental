using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	
	///<summary>A list setup ahead of time with all the procedure codes used by the office.  Every procedurelog entry which is attached to a patient is also linked to this table.</summary>
	public class ProcedureCode{
		///<summary>Primary key.  Does not have to be a valid 'ADACode'. Can also include ADACodes with suffixes which get automatically trimmed when sending claims.  In foreign countries, the codes may look very much unlike adacodes, and that's ok.  Can also hold medical codes.</summary>
		public string ADACode;
		///<summary>The main description.</summary>
		public string Descript;
		///<summary>Abbreviated description.</summary>
		public string AbbrDesc;
		///<summary>X's and /'s describe Dr's time and assistant's time in 10 minute increments.</summary>
		public string ProcTime;
		///<summary>FK to definition.DefNum.  The category that this code will be found under in the search window.  Has nothing to do with insurance categories.</summary>
		public int ProcCat;
		///<summary>Enum:TreatmentArea</summary>
		public TreatmentArea TreatArea;
		///<summary>No longer used. Extraction paint type is used instead to show missing teeth.</summary>
		public bool RemoveToothOld;
		///<summary>Triggers recall in 6 months or as defined.</summary>
		public bool SetRecall;
		///<summary>If true, do not usually bill this procedure to insurance.</summary>
		public bool NoBillIns;
		///<summary>True if Crown,Bridge,Denture, or RPD. Forces user to enter Initial or Replacement and Date.</summary>
		public bool IsProsth;
		///<summary>The default procedure note to copy when marking complete.</summary>
		public string DefaultNote;
		///<summary>Identifies hygiene procedures so that the correct provider can be selected.</summary>
		public bool IsHygiene;
		///<summary>No longer used.</summary>
		public int GTypeNum;
		///<summary>For Medicaid.  There may be more later.</summary>
		public string AlternateCode1;
		///<summary>FK to procedurecode.ADACode.  The actual medical code that is being referenced must be setup first.  Anytime a procedure it added, this medical code will also be added to that procedure.  The user can change it in procedurelog.</summary>
		public string MedicalCode;
		///<summary>Used by some offices even though no user interface built yet.  SalesTaxPercentage has been added to the preference table to store the amount of sales tax to apply as an adjustment attached to a procedurelog entry.</summary>
		public bool IsTaxed;
		///<summary>Enum:ToothPaintingType</summary>
		public ToothPaintingType PaintType;
		///<summary>If set to anything but 0, then this will override the graphic color for all procedures of this code, regardless of the status.</summary>
		public Color GraphicColor;
		///<summary>When creating treatment plans, this description will be used instead of the technical description.</summary>
		public string LaymanTerm;

		///<summary>Returns a copy of this Procedurecode.</summary>
		public ProcedureCode Copy(){
			ProcedureCode p=new ProcedureCode();
			p.ADACode=ADACode;
			p.Descript=Descript;
			p.AbbrDesc=AbbrDesc;
			p.ProcTime=ProcTime;
			p.ProcCat=ProcCat;
			p.TreatArea=TreatArea;
			//p.RemoveTooth=RemoveTooth;
			p.SetRecall=SetRecall;
			p.NoBillIns=NoBillIns;
			p.IsProsth=IsProsth;
			p.DefaultNote=DefaultNote;
			p.IsHygiene=IsHygiene;
			p.GTypeNum=GTypeNum;
			p.AlternateCode1=AlternateCode1;
			p.MedicalCode=MedicalCode;
			p.IsTaxed=IsTaxed;
			p.PaintType=PaintType;
			p.GraphicColor=GraphicColor;
			p.LaymanTerm=LaymanTerm;
			return p;
		}


	}

	
	
	


}










