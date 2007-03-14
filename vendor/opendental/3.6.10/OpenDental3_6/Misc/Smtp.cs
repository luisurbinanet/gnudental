//We now use Indy for all networking functions.
/*using System;
using System.Collections;
using System.Net.Sockets;

namespace OpenDental{
	///<summary>Handles sending email.</summary>
	public class Smtp : System.Net.Sockets.TcpClient{
    public string from=null;
    public ArrayList to;
    public ArrayList cc;
    public ArrayList bcc;
    public string subject=null;
    public string bodyText=null;
    public string bodyHtml=null;
    public string server=null;

		public Smtp(){
			to=new ArrayList();
			cc=new ArrayList();
			bcc=new ArrayList();
		}

		public void Send(){
			string message;
			string response;
			Connect(server, 25);
			response=Response();
			if(response.Substring(0,3)!="220"){
				throw new SmtpException(response);
			}
			message="HELO me\r\n";
			Write(message);
			response=Response();
			if(response.Substring(0,3)!="250"){
				throw new SmtpException(response);
			}
			message="MAIL FROM:<"+from+">\r\n";
			Write(message);
			response=Response();
			if(response.Substring(0,3)!="250"){
				throw new SmtpException(response);
			}
			foreach(string address in to){
				try{
					message="RCPT TO:<"+address+">\r\n";
					Write(message);
					response=Response();
					if(response.Substring(0,3)!="250"){
						throw new SmtpException(response);
					}
				}
				catch(SmtpException e){
					System.Console.WriteLine("{0}",e.What());
				}
			}
			foreach(string address in cc){
				try{
					message="RCPT TO:<"+address+">\r\n";
					Write(message);
					response=Response();
					if(response.Substring(0,3)!="250"){
						throw new SmtpException(response);
					}
				}
				catch(SmtpException e){
					System.Console.WriteLine("{0}",e.What());
				}
			}
			foreach(string address in bcc){
				try{
					message="RCPT TO:<"+address+">\r\n";
					Write(message);
					response=Response();
					if(response.Substring(0,3)!="250"){
						throw new SmtpException(response);
					}
				}
				catch( SmtpException e){
					System.Console.WriteLine("{0}",e.What());
				}
			}
			message="DATA\r\n";
			Write(message);
			response=Response();
			if(response.Substring(0,3)!="354"){
				throw new SmtpException(response);
			}
			message="Subject: "+subject+"\r\n";
			foreach(string address in to){
				message+="To: "+address+"\r\n";
			}
			foreach(string address in cc ){
				message+="Cc: "+address+"\r\n";
			}
			message+="From: "+from+"\r\n";
			if(bodyHtml!=null && bodyHtml.Length>0){
				message+="MIME-Version: 1.0\r\n"
					+"Content-Type: text/html;\r\n"
					+"      charset=\"iso-8859-1\"\r\n";
				message+="\r\n"+bodyHtml; 
			}
			else{
				message+="\r\n"+bodyText; 
			}
			message+="\r\n.\r\n";
			Write(message);
			response=Response();
			if(response.Substring(0,3)!="250"){
				throw new SmtpException(response);
			}
			message="QUIT\r\n";
			Write(message);
			response=Response();
			if(response.IndexOf("221")==-1){
				throw new SmtpException(response);
			}
		}//end of Send

		public void Write(string message){
			System.Text.ASCIIEncoding en=new System.Text.ASCIIEncoding();
			byte[] WriteBuffer=en.GetBytes(message);//new byte[1024] ;
			NetworkStream stream=GetStream();
			stream.Write(WriteBuffer,0,WriteBuffer.Length);
		}

		public string Response(){
			System.Text.ASCIIEncoding enc=new System.Text.ASCIIEncoding();
			byte[] serverbuff=new Byte[1024];
			NetworkStream stream=GetStream();
			int count=stream.Read(serverbuff,0,1024);            
			if(count==0){
				return "";
			}
			return enc.GetString(serverbuff,0,count);            
		}



		
	}

	public class SmtpException : System.Exception{
    private string message;

    public SmtpException(string str){
			message=str;
    }

    public string What(){
			return message;
    }
	}

}
*/