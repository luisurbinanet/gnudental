using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace UnitTests {
	public partial class FormUnitTests:Form {
		public FormUnitTests() {
			InitializeComponent();
		}

		private void FormUnitTests_Load(object sender,EventArgs e) {
			BenefitComputeRenewDate();
			textResults.Text+="Done.";
			textResults.SelectionStart=textResults.Text.Length;
		}

		private void BenefitComputeRenewDate(){
			DateTime asofDate=new DateTime(2006,3,19);
			bool isCalendarYear=true;
			DateTime insStartDate=new DateTime(2003,3,1);
			DateTime result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2006,1,1)){
				textResults.Text+="BenefitComputeRenewDate 1 failed.\r\n";
			}
			isCalendarYear=false;//for the remaining tests
			//earlier in same month
			result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 2 failed.\r\n";
			}
			//earlier month in year
			asofDate=new DateTime(2006,5,1);
			result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 3 failed.\r\n";
			}
			asofDate=new DateTime(2006,12,1);
			result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 4 failed.\r\n";
			}
			//later month in year
			asofDate=new DateTime(2006,2,1);
			result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2005,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 5 failed.\r\n";
			}
			asofDate=new DateTime(2006,2,12);
			result=BenefitB.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2005,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 6 failed.\r\n";
			}
		}

		


	}
}