using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenDental{

	///<summary>Corresponds to the emailtemplate table in the database.</summary>
	public class EmailTemplate{
		///<summary>Primary key.</summary>
		public int EmailTemplateNum;
		///<summary>Default subject line.</summary>
		public string Subject;
		///<summary>Body of the email</summary>
		public string BodyText;

		///<summary>Returns a copy of this EmailTemplate.</summary>
		public EmailTemplate Copy(){
			EmailTemplate t=new EmailTemplate();
			t.EmailTemplateNum=EmailTemplateNum;
			t.Subject=Subject;
			t.BodyText=BodyText;
			return t;
		}

		///<summary></summary>
		public void Insert(){
			string command= "INSERT INTO emailtemplate(Subject,BodyText"
				+") VALUES("
				+"'"+POut.PString(Subject)+"', "
				+"'"+POut.PString(BodyText)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command,true);
			EmailTemplateNum=dcon.InsertID;
		}

		///<summary></summary>
		public void Update(){
			string command= "UPDATE emailtemplate SET "
				+ "Subject = '"  +POut.PString(Subject)+"' "
				+ ",BodyText = '"+POut.PString(BodyText)+"' "
				+"WHERE EmailTemplateNum = '"+POut.PInt(EmailTemplateNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		///<summary></summary>
		public void Delete(){
			string command= "DELETE from emailtemplate WHERE EmailTemplateNum = '"
				+EmailTemplateNum.ToString()+"'";
			DataConnection dcon=new DataConnection();
 			dcon.NonQ(command);
		}

		
	}
	
	/*=========================================================================================
		=================================== class EmailTemplates ===========================================*/
	///<summary>emailtemplates are refreshed as local data.</summary>
	public class EmailTemplates{
		///<summary>List of all email templates</summary>
		public static EmailTemplate[] List;

		///<summary></summary>
		public static void Refresh(){
			string command=
				"SELECT * from emailtemplate ORDER BY Subject";
			DataConnection dcon=new DataConnection();
 			DataTable table=dcon.GetTable(command);
			List=new EmailTemplate[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new EmailTemplate();
				List[i].EmailTemplateNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Subject         =PIn.PString(table.Rows[i][1].ToString());
				List[i].BodyText        =PIn.PString(table.Rows[i][2].ToString());
			}
		}

		

		

		
		
	}

	
	

}













