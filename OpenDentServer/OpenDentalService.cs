using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using OpenDentBusiness;

namespace OpenDentServer {
	partial class OpenDentalService:ServiceBase {
		private Thread workerThread = null;
		public static TcpListener server;
		private static XPathNavigator Navigator;

		public OpenDentalService() {
			InitializeComponent();
		}

		static void Main(string[] args) {
			System.ServiceProcess.ServiceBase.Run(new OpenDentalService());
		}

		protected override void OnStart(string[] args) {
			//base.OnStart(args);
			//EventLog.WriteEntry("SimpleService.OnStart", DateTime.Now.ToLongTimeString());
			if((workerThread==null) || 
				((workerThread.ThreadState & (System.Threading.ThreadState.Unstarted | System.Threading.ThreadState.Stopped)) != 0))
			{
				//#if LOGEVENTS
        //EventLog.WriteEntry("SimpleService starting worker thread", DateTime.Now.ToLongTimeString() +
        //" - Starting the service worker thread.");
				//#endif
				workerThread = new Thread(new ThreadStart(ServiceWorkerMethod));
				workerThread.Start();
			}
			if(workerThread != null) {
				//#if LOGEVENTS
        //EventLog.WriteEntry("SimpleService worker thread state", DateTime.Now.ToLongTimeString() +
        //" - Worker thread state = " + workerThread.ThreadState.ToString());
				//#endif
			}
			//myServiceStatus.currentState = (int)State.SERVICE_RUNNING;
			//SetServiceStatus(handle,myServiceStatus);
			//EventLog.WriteEntry("SimpleService.OnStart ending");
		}

		protected override void OnStop() {
			// TODO: Add code here to perform any tear-down necessary to stop your service.
			//EventLog.WriteEntry("SimpleService.OnStop");
			base.OnStop();
		}

		public static int Login(string database,string oduser,string odpasshash) {
			//Get the database server, user, and password from the config file
			//make sure there's an entry for the requested database:
			XPathNavigator navConn=Navigator.SelectSingleNode("//DatabaseConnection[Database='"+database+"']");
			if(navConn==null) {
				throw new Exception(database+" is not an allowed database.");
			}
			//return navOne.SelectSingleNode("summary").Value;
			//now, get the values for this connection
			string server=navConn.SelectSingleNode("ComputerName").Value;
			string mysqlUser=navConn.SelectSingleNode("User").Value;
			string mysqlPassword=navConn.SelectSingleNode("Password").Value;
			string mysqlUserLow=navConn.SelectSingleNode("UserLow").Value;
			string mysqlPasswordLow=navConn.SelectSingleNode("PasswordLow").Value;
			XPathNavigator dbTypeNav=navConn.SelectSingleNode("DatabaseType");
			DatabaseType dbtype=DatabaseType.MySql;
			if(dbTypeNav!=null){
				if(dbTypeNav.Value=="Oracle"){
					dbtype=DatabaseType.Oracle;
				}
			}
			DataConnection dcon=new DataConnection();
			//Try to connect to the database
			try {
				dcon.SetDb(server,database,mysqlUser,mysqlPassword,mysqlUserLow,mysqlPasswordLow,dbtype);
				Console.WriteLine(oduser);
			}
			catch {
				throw new Exception(@"Connection to database failed.  Check the values in the config file on the server, usually at C:\Program Files\Open Dental Server\OpenDentServerConfig.xml");
			}
			//Then, check username and password
			if(!UserodB.CheckUserAndPassword(oduser,odpasshash)) {
				throw new Exception("Invalid username or password.");
			}
			return 0;//meaningless
		}

		// Define a simple method that runs as the worker thread for the service.  
		public void ServiceWorkerMethod() {
			try {
				do {
					//ServiceController sc=new ServiceController("OpenDental");
					//sc.
					//string appPath=;
					string configfile
						=Application.StartupPath+Path.DirectorySeparatorChar+"OpenDentServerConfig.xml";
					if(!File.Exists(configfile)){
						throw new Exception("Could not find "+configfile);
					}
					XmlDocument doc=new XmlDocument();
					try {
						doc.Load(configfile);
					}
					catch {
						throw new Exception(configfile+" is not a valid format.");
					}
					Navigator=doc.CreateNavigator();
					XPathNavigator navport=Navigator.SelectSingleNode("//ServerPort");
					if(navport==null) {
						throw new Exception("ServerPort element not found in config file.");
					}
					int port=PIn.PInt(navport.Value);
					server=null;
					try {
						//Int32 port = 9390;
						//IPAddress localAddr=Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
						server=new TcpListener(port);//localAddr,port);//but this is the only way that works!
						server.Start();
						//String datablock = null;
						Console.Write("Waiting...");
						while(true) {// Enter the listening loop.
							//Each loop will pick up a new client computer, and pass off the connection to a worker thread. 
							//Console.Write("Waiting...");
							// Perform a blocking call to accept requests.
							// You could also use server.AcceptSocket() here.
							TcpClient client = server.AcceptTcpClient();
							NetworkStream netStream=client.GetStream();
							Console.Write("Connecting...");
							WorkerClass worker=new WorkerClass(netStream);
							Thread workerThread=new Thread(new ThreadStart(worker.DoWork));
							workerThread.Start();
							//
							//client.Close();
							//Console.WriteLine("Closed.");
						}
					}
					catch(SocketException e) {
						Console.WriteLine("SocketException: {0}",e);
					}
					finally {
						// Stop listening for new clients.
						server.Stop();
						Console.WriteLine("Server stopped");
					}
					//Console.WriteLine("\nHit enter to exit...");
					//Console.Read();
					//goto StartingPoint;
				}
				while(true);
			}
			catch(ThreadAbortException) {
				// Another thread has signalled that this worker
				// thread must terminate.  Typically, this occurs when
				// the main service thread receives a service stop 
				// command.

				// Write a trace line indicating that the worker thread
				// is exiting.  Notice that this simple thread does
				// not have any local objects or data to clean up.
			}
		}
	}

	public class WorkerClass {
		private NetworkStream netStream;

		// The constructor obtains the state information.
		public WorkerClass(NetworkStream stream) {
			netStream=stream;
		}

		public void DoWork() {
			while(true) {//Each loop gets and returns one message pair.
				Byte[] buffer = new Byte[1024];// Buffer for reading data
				MemoryStream memStream=new MemoryStream();
				int numberOfBytesRead = 0;
				try {
					do {
						numberOfBytesRead = netStream.Read(buffer,0,buffer.Length);
						//myCompleteMessage.AppendFormat("{0}",Encoding.UTF8.GetString(buffer,0,numberOfBytesRead));
						memStream.Write(buffer,0,numberOfBytesRead);
					}
					while(netStream.DataAvailable);
				}
				catch {//if connection was closed by client.
					break;
				}
				//Console.Write("Processing...");
				//string debugMessage=Encoding.UTF8.GetString(memStream.GetBuffer());
				//memStream.Position=0;
				//if(debugMessage.StartsWith("SELECT * FROM claimformitem")){
				//	Debug.WriteLine(debugMessage);
				//}
				DataTransferObject dto=DataTransferObject.Deserialize(memStream.GetBuffer());//myCompleteMessage.ToString());
				memStream.Close();
				//Process and send response to client--------------------------------------------------------------
				byte[] data;
				XmlSerializer serializer;
				memStream=new MemoryStream();
				try {
					if(dto.GetType().BaseType==typeof(DtoCommandBase)) {
						int result=BusinessLayer.ProcessCommand((DtoCommandBase)dto);
						DtoServerAck ack=new DtoServerAck();
						ack.IDorRows=result;
						serializer=new XmlSerializer(typeof(DtoServerAck));
						serializer.Serialize(memStream,ack);
					}
					else if(dto.GetType().BaseType==typeof(DtoQueryBase)) {
						DataSet ds=BusinessLayer.ProcessQuery((DtoQueryBase)dto);
						serializer=new XmlSerializer(typeof(DataSet));
						serializer.Serialize(memStream,ds);
					}
				}
				catch(Exception e) {
					DtoException exception=new DtoException();
					exception.Message=e.Message;
					serializer=new XmlSerializer(typeof(DtoException));
					serializer.Serialize(memStream,exception);
				}
				data=Encoding.UTF8.GetBytes("<EOF>");
				memStream.Write(data,0,data.Length);
				data=memStream.ToArray();
				//#if DEBUG
				//	string dataString=Encoding.UTF8.GetString(data);
				//	Debug.WriteLine("Server Sent: "+dataString);
				//#endif
				memStream.Close();
				netStream.Write(data,0,data.Length);
			}//wait for the next message
			//connection was lost.  Client probably closed program
			netStream.Close();
		}








	}



}
