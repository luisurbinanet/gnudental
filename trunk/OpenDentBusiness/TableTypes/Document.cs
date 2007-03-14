using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
	///<summary>It should be called image, but the name is for historical reasons.  Represents a single document in the images module.  This table indicates in which patient's folder the image can be found, but the actual attaching of the image to the patient is done by the docattach table.</summary>
	public class Document {
		///<summary>Primary key.</summary>
		public int DocNum;
		///<summary>Description of the document.</summary>
		public string Description;
		///<summary>Date created.</summary>
		public DateTime DateCreated;
		///<summary>FK to definition.DefNum. Cateories for documents.</summary>
		public int DocCategory;
		///<summary>FK to patient.PatNum.  Patient folder that document is in.(for sharing situations later)</summary>
		public int WithPat;
		///<summary>The name of the file. Does not include any directory info.</summary>
		public string FileName;
		///<summary>Enum:ImageType eg. document, radiograph, photo, file</summary>
		public ImageType ImgType;
		///<summary>True if flipped horizontally. A vertical flip would be stored as a horizontal flip plus a 180 rotation.</summary>
		public bool IsFlipped;
		///<summary>Only allowed 0,90,180, and 270.</summary>
		public int DegreesRotated;
		///<summary>Incomplete.  An optional list of tooth numbers separated by commas.  The tooth numbers will be in American format and must be processed for display.  When displayed, dashes will be used for sequences of 3 or more tooth numbers.</summary>
		public string ToothNumbers;
		///<summary></summary>
		public string Note;
		///<summary>True if the signature is in Topaz format rather than OD format.</summary>
		public bool SigIsTopaz;
		///<summary>The encrypted and bound signature in base64 format.  The signature is bound to the byte sequence</summary>
		public string Signature;

		///<summary>Returns a copy of this Clinic.</summary>
		public Document Copy() {
			Document d=new Document();
			d.DocNum=DocNum;
			d.Description=Description;
			d.DateCreated=DateCreated;
			d.DocCategory=DocCategory;
			d.WithPat=WithPat;
			d.FileName=FileName;
			d.ImgType=ImgType;
			d.IsFlipped=IsFlipped;
			d.DegreesRotated=DegreesRotated;
			d.ToothNumbers=ToothNumbers;
			d.Note=Note;
			d.SigIsTopaz=SigIsTopaz;
			d.Signature=Signature;
			return d;
		}
	}

	public class DtoDocumentInsert:DtoCommandBase {
		public Document Doc;
		public string PatLF;
		public int PatNum;
	}

	public class DtoDocumentUpdate:DtoCommandBase {
		public Document Doc;
	}

}
