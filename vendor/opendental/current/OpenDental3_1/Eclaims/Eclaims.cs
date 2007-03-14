using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OpenDental.Eclaims
{
	/// <summary>
	/// Summary description for Eclaims.
	/// </summary>
	public class Eclaims{
		/// <summary></summary>
		public Eclaims()
		{
			
		}

		///<summary>Supply an arrayList of type ClaimSendQueueItem. Called from FormClaimSend.  Can send to multiple clearinghouses simultaneously.</summary>
		public static void SendBatches(ArrayList queueItems){
			//claimsByCHouse is of type ClaimSendQueueItem
			ArrayList[] claimsByCHouse=new ArrayList[Clearinghouses.List.Length];
			for(int i=0;i<claimsByCHouse.Length;i++){
				claimsByCHouse[i]=new ArrayList();
			}
			//divide the items by clearinghouse:
			for(int i=0;i<queueItems.Count;i++){
				claimsByCHouse[Clearinghouses.GetIndex(((ClaimSendQueueItem)queueItems[i]).ClearinghouseNum)]
					.Add(queueItems[i]);
			}
			//for any clearinghouses with claims, send them:
			int batchNum;
			bool result=true;
			for(int i=0;i<claimsByCHouse.Length;i++){
				if(claimsByCHouse[i].Count==0){
					continue;
				}
				//get next batch number for this clearinghouse
				batchNum=Clearinghouses.List[i].GetNextBatchNumber();
				//---------------------------------------------------------------------------------------
				//Create the claim file(s) for this clearinghouse
				if(Clearinghouses.List[i].Eformat==ElectronicClaimFormat.X12){
					result=X12.SendBatch(claimsByCHouse[i],batchNum);
				}
				else if(Clearinghouses.List[i].Eformat==ElectronicClaimFormat.Renaissance){
					result=Renaissance.SendBatch(claimsByCHouse[i],batchNum);
				}
				else{
					result=false;//(ElectronicClaimFormat.None does not get sent)
				}
				if(!result){//if failed to create claim file properly,
					continue;//don't launch program or change claim status
				}
				//----------------------------------------------------------------------------------------
				//Launch Client Program for this clearinghouse if applicable
				if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.None){
					AttemptLaunch(Clearinghouses.List[i],batchNum);
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.WebMD){
					if(!WebMD.Launch(Clearinghouses.List[i],batchNum)){
						MessageBox.Show(Lan.g("Eclaims","Error sending."));
						continue;
					}
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.BCBSGA){
					if(!BCBSGA.Launch(Clearinghouses.List[i],batchNum)){
						MessageBox.Show(Lan.g("Eclaims","Error sending."));
						continue;
					}
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.Renaissance){
					AttemptLaunch(Clearinghouses.List[i],batchNum);
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.WebClaim){
					if(!WebClaim.Launch(Clearinghouses.List[i],batchNum)){
						MessageBox.Show(Lan.g("Eclaims","Error sending."));
						continue;
					}
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.RECS){
					if(!RECS.Launch(Clearinghouses.List[i],batchNum)){
						MessageBox.Show("Claim file created, but could not launch RECS client.");
						//continue;
					}
				}
				//----------------------------------------------------------------------------------------
				//finally, change the claim statuses to Probably sent.
				for(int j=0;j<claimsByCHouse[i].Count;j++){
					Claims.UpdateStatus(((ClaimSendQueueItem)claimsByCHouse[i][j]).ClaimNum,"P");
				}
			}//for(int i=0;i<claimsByCHouse.Length;i++){
		}

		///<summary>If no comm bridge is selected for a clearinghouse, this launches any client program the user has entered.  We do not want to cause a rollback, so no return value.</summary>
		private static void AttemptLaunch(Clearinghouse clearhouse,int batchNum){
			if(clearhouse.ClientProgram==""){
				return;
			}
			if(!File.Exists(clearhouse.ClientProgram)){
				MessageBox.Show(clearhouse.ClientProgram+" "+Lan.g("Eclaims","does not exist."));
				return;
			}
			try{
				Process.Start(clearhouse.ClientProgram);
			}
			catch{
				MessageBox.Show(Lan.g("Eclaims","Client program could not be started.  It may already be running. You must open your client program to finish sending claims."));
			}
		}

		///<summary>Returns a string describing all missing data on this claim.  Claim will not be allowed to be sent electronically unless this string comes back empty.</summary>
		public static string GetMissingData(ClaimSendQueueItem queueItem){
			Clearinghouse clearhouse=Clearinghouses.GetClearinghouse(queueItem.ClearinghouseNum);
			if(clearhouse.Eformat==ElectronicClaimFormat.X12){
				return X12.GetMissingData(queueItem);
			}
			else if(clearhouse.Eformat==ElectronicClaimFormat.Renaissance){
				return Renaissance.GetMissingData(queueItem);
			}
			return "";
		}


	}
}



























