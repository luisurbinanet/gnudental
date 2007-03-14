/*
using System;
using System.Collections;
using System.Windows.Forms;

namespace OpenDental{
	
	///<summary>Corresponds to the instemplate table in the database.  All insPlans MUST be linked to a template. InsTemplates may very well be eliminated in a future version, since the info is all duplicate, but it is not easy to make a major change like that, and the user wouldn't even notice.</summary>
	public struct InsTemplate{
		///<summary>Primary key.</summary>
		public int TemplateNum;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string Carrier;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string Address;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string Address2;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string City;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string State;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string Zip;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string Phone;
		///<summary>Do not usually send claims to this ins carrier electronically.</summary>
		public bool NoSendElect;
		///<summary>No longer used. See CarrierNum column instead.</summary>
		public string ElectID;
		///<summary>No longer used. This may be replaced later by a note in the carrier table.</summary>
		public string Note;
		///<summary>""=percentage(the default),"f"=flatCopay,"c"=capitation.</summary>
		public string PlanType;
		///<summary>Foreign key to claimform.ClaimFormNum</summary>
		public int ClaimFormNum;
		///<summary>0=no,1=yes.  could later be extended if more alternates required</summary>
		public bool UseAltCode;
		///<summary>Fee on claims should be the UCR fee for the patient's provider.</summary>
		public bool ClaimsUseUCR;
		///<summary>Foreign key to Definition.DefNum.</summary>
		public int FeeSched;
		///<summary>Not used at all.</summary>
		public bool IsWrittenOff;
		///<summary>Foreign key to Definition.DefNum. This fee schedule holds only co-pays(patient portions).</summary>
		public int CopayFeeSched;
		///<summary>Foreign key to employer.EmployerNum.</summary>
		public int EmployerNum;
		///<summary>Optional</summary>
		public string GroupName;
		///<summary></summary>
		public string GroupNum;
		///<summary></summary>
		public int CarrierNum;
		///<summary>This is NOT a database column.  It is used internally in this program to display the number of plans that are linked to each template.</summary>
		public int Plans;
	}


	//=========================================================================================
	//=================================== Class InsTemplates ===========================================
	///<summary></summary>
	public class InsTemplates:DataClass{
		///<summary></summary>
		public static InsTemplate[] List;
		///<summary></summary>
		public static InsTemplate Cur;

		/// <summary>Gets templates from the db. You can order by employer, or specify only a single templateNum to retrieve.</summary>
		/// <param name="byEmployer">Set true to order by employer then carrier. False to just order by carrier.</param>
		/// <param name="templateNum">Specify a single templateNum to retrieve or 0 to retrieve all.</param>
		public static void Refresh(bool byEmployer,int templateNum){
			if(templateNum>0){
				cmd.CommandText =
					"SELECT * FROM instemplate "
					+"WHERE TemplateNum = '"+templateNum.ToString()+"'";
			}
			else if(byEmployer){
				cmd.CommandText =
					"SELECT instemplate.*,COUNT(insplan.PlanNum),employer.EmpName,carrier.CarrierName "
					+"FROM instemplate "
					+"LEFT JOIN employer ON employer.EmployerNum = instemplate.EmployerNum "
					+"LEFT JOIN carrier ON carrier.CarrierNum = instemplate.CarrierNum "
					+"LEFT JOIN insplan ON insplan.TemplateNum = instemplate.TemplateNum "
					+"GROUP BY instemplate.TemplateNum "
					+"ORDER BY EmpName IS NULL,EmpName,CarrierName ASC";
				//MessageBox.Show(cmd.CommandText);
			}
			else{
				cmd.CommandText =
					"SELECT instemplate.*,COUNT(insplan.PlanNum),carrier.CarrierName FROM instemplate "
					+"LEFT JOIN carrier USING(CarrierNum) "
					+"LEFT JOIN insplan ON insplan.TemplateNum = instemplate.TemplateNum "
					+"GROUP BY instemplate.TemplateNum "
					+"ORDER BY CarrierName ASC";
			}
			FillTable();
			InsTemplate tempTemplate=new InsTemplate();
			List = new InsTemplate[table.Rows.Count];//this would have a length of one for a single template
			for (int i=0;i<List.Length;i++){
				tempTemplate.TemplateNum  = PIn.PInt   (table.Rows[i][0].ToString());
				tempTemplate.Carrier      = PIn.PString(table.Rows[i][1].ToString());
				tempTemplate.Address      = PIn.PString(table.Rows[i][2].ToString());
				tempTemplate.Address2     = PIn.PString(table.Rows[i][3].ToString());
				tempTemplate.City         = PIn.PString(table.Rows[i][4].ToString());
				tempTemplate.State        = PIn.PString(table.Rows[i][5].ToString());
				tempTemplate.Zip          = PIn.PString(table.Rows[i][6].ToString());
				tempTemplate.Phone        = PIn.PString(table.Rows[i][7].ToString());
				tempTemplate.NoSendElect  = PIn.PBool  (table.Rows[i][8].ToString());
				tempTemplate.ElectID      = PIn.PString(table.Rows[i][9].ToString());
				tempTemplate.Note         = PIn.PString(table.Rows[i][10].ToString());
				tempTemplate.PlanType     = PIn.PString(table.Rows[i][11].ToString());
				tempTemplate.ClaimFormNum = PIn.PInt   (table.Rows[i][12].ToString());
				tempTemplate.UseAltCode   = PIn.PBool  (table.Rows[i][13].ToString());
				tempTemplate.ClaimsUseUCR = PIn.PBool  (table.Rows[i][14].ToString());
				tempTemplate.FeeSched     = PIn.PInt   (table.Rows[i][15].ToString());
				tempTemplate.IsWrittenOff = PIn.PBool  (table.Rows[i][16].ToString());
				tempTemplate.CopayFeeSched= PIn.PInt   (table.Rows[i][17].ToString());
				tempTemplate.EmployerNum  = PIn.PInt   (table.Rows[i][18].ToString());
				tempTemplate.GroupName    = PIn.PString(table.Rows[i][19].ToString());
				tempTemplate.GroupNum     = PIn.PString(table.Rows[i][20].ToString());
				tempTemplate.CarrierNum   = PIn.PInt   (table.Rows[i][21].ToString());
				if(templateNum==0){//ie selecting all templates
					tempTemplate.Plans      = PIn.PInt   (table.Rows[i][22].ToString());
				}
				List[i]=tempTemplate;
			}
			if(templateNum>0){
				Cur=List[0];
			}
		}

		///<summary>Gets template list from the db ordered as specified.</summary>
		public static void Refresh(bool byEmployer){	
			Refresh(byEmployer,0);
		}

		///<summary>Gets a single template from the database. Stores in Cur.</summary>
		public static void Refresh(int templateNum){
			Refresh(false,templateNum);
		}

		///<summary></summary>
		public static void InsertCur(){
			cmd.CommandText = "INSERT INTO instemplate (carrier,address,address2,city,state,zip,"
				+"phone,nosendelect,electid,note,plantype,claimformnum"
				+",usealtcode,claimsuseucr,feesched"
				+",iswrittenoff,copayfeesched,EmployerNum,GroupName,GroupNum"
				+",CarrierNum) VALUES("
				+"'"+POut.PString(Cur.Carrier)+"', "
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PString(Cur.Phone)+"', "
				+"'"+POut.PBool  (Cur.NoSendElect)+"', "
				+"'"+POut.PString(Cur.ElectID)+"', "
				+"'"+POut.PString(Cur.Note)+"', "
				+"'"+POut.PString(Cur.PlanType)+"', "
				+"'"+POut.PInt   (Cur.ClaimFormNum)+"', "
				+"'"+POut.PBool  (Cur.UseAltCode)+"', "
				+"'"+POut.PBool  (Cur.ClaimsUseUCR)+"', "
				+"'"+POut.PInt   (Cur.FeeSched)+"', "
				+"'"+POut.PBool  (Cur.IsWrittenOff)+"', "
				+"'"+POut.PInt   (Cur.CopayFeeSched)+"', "
				+"'"+POut.PInt   (Cur.EmployerNum)+"', "
				+"'"+POut.PString(Cur.GroupName)+"', "
				+"'"+POut.PString(Cur.GroupNum)+"', "
				+"'"+POut.PInt   (Cur.CarrierNum)+"')";
			//MessageBox.Show(cmd.CommandText);
			NonQ(true);
			Cur.TemplateNum=InsertID;
		}

		///<summary>Updates the current instemplate and also updates all insplans that are linked to it</summary>
		public static void UpdateCur(){
			cmd.CommandText = "UPDATE instemplate SET " 
				+ "Carrier = '"       +POut.PString(Cur.Carrier)+"'"
				+ ",Address = '"      +POut.PString(Cur.Address)+"'"
				+ ",Address2 = '"     +POut.PString(Cur.Address2)+"'"
				+ ",City = '"         +POut.PString(Cur.City)+"'"
				+ ",State = '"        +POut.PString(Cur.State)+"'"
				+ ",Zip = '"          +POut.PString(Cur.Zip)+"'"
				+ ",Phone = '"        +POut.PString(Cur.Phone)+"'"
				+ ",NoSendElect='"    +POut.PBool  (Cur.NoSendElect)+"'"
				+ ",ElectID = '"      +POut.PString(Cur.ElectID)+"'"
				+ ",Note = '"         +POut.PString(Cur.Note)+"'"//this is not part of carrier info
				+ ",PlanType = '"     +POut.PString(Cur.PlanType)+"'"
				+ ",claimformnum = '" +POut.PInt   (Cur.ClaimFormNum)+"'"
				+ ",usealtcode = '"   +POut.PBool  (Cur.UseAltCode)+"'"
				+ ",claimsuseucr = '" +POut.PBool  (Cur.ClaimsUseUCR)+"'"
				+ ",feesched = '"     +POut.PInt   (Cur.FeeSched)+"'"
				+ ",iswrittenoff = '" +POut.PBool  (Cur.IsWrittenOff)+"'"//neither is this
				+ ",copayfeesched = '"+POut.PInt   (Cur.CopayFeeSched)+"'"
				+ ",EmployerNum = '"  +POut.PInt   (Cur.EmployerNum)+"'"
				+ ",GroupName = '"    +POut.PString(Cur.GroupName)+"'"
				+ ",GroupNum = '"     +POut.PString(Cur.GroupNum)+"'"
				+ ",CarrierNum = '"   +POut.PInt   (Cur.CarrierNum)+"'"
				+" WHERE TemplateNum = '" +POut.PInt(Cur.TemplateNum)+"'";
			NonQ();
			cmd.CommandText = "UPDATE insplan SET " 
				+ "Carrier = '"       +POut.PString(Cur.Carrier)+"'"
				+ ",Address = '"      +POut.PString(Cur.Address)+"'"
				+ ",Address2 = '"     +POut.PString(Cur.Address2)+"'"
				+ ",City = '"         +POut.PString(Cur.City)+"'"
				+ ",State = '"        +POut.PString(Cur.State)+"'"
				+ ",Zip = '"          +POut.PString(Cur.Zip)+"'"
				+ ",Phone = '"        +POut.PString(Cur.Phone)+"'"
				+ ",NoSendElect='"    +POut.PBool  (Cur.NoSendElect)+"'"
				+ ",ElectID = '"      +POut.PString(Cur.ElectID)+"'"
				+ ",PlanType = '"     +POut.PString(Cur.PlanType)+"'"
				+ ",claimformnum = '" +POut.PInt   (Cur.ClaimFormNum)+"'"
				+ ",usealtcode = '"   +POut.PBool  (Cur.UseAltCode)+"'"
				+ ",claimsuseucr = '" +POut.PBool  (Cur.ClaimsUseUCR)+"'"
				+ ",feesched = '"     +POut.PInt   (Cur.FeeSched)+"'"
				+ ",copayfeesched = '"+POut.PInt   (Cur.CopayFeeSched)+"'"
				+ ",EmployerNum = '"  +POut.PInt   (Cur.EmployerNum)+"'"
				+ ",GroupName = '"    +POut.PString(Cur.GroupName)+"'"
				+ ",GroupNum = '"     +POut.PString(Cur.GroupNum)+"'"
				+ ",CarrierNum = '"   +POut.PInt   (Cur.CarrierNum)+"'"
				+" WHERE TemplateNum = '" +POut.PInt(Cur.TemplateNum)+"'";
			NonQ();
		}

		///<summary>Dependencies should have already been checked.</summary>
		public static void DeleteCur(){
			cmd.CommandText = "DELETE from instemplate WHERE templatenum = '"+Cur.TemplateNum.ToString()+"'";
			NonQ();
		}

		///<summary>Dependencies should have already been checked.</summary>
		public static void Delete(int templateNum){
			cmd.CommandText = "DELETE from instemplate WHERE templatenum = '"+templateNum.ToString()+"'";
			NonQ();
		}

		///<summary>Returns a list of insplans that are linked to the Cur template. Used before deleting a template to make sure template is not in use.</summary>
		public static string[] LinkedPlans(){
			cmd.CommandText="SELECT CONCAT(LName,', ',FName) FROM patient,insplan" 
				+" WHERE patient.PatNum=insplan.Subscriber"
				+" && insplan.TemplateNum = '"+POut.PInt(Cur.TemplateNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			FillTable();
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

	}

	
	


}

*/











