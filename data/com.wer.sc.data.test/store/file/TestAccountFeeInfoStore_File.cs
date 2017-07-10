using com.wer.sc.data.market;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    [TestClass]
    public class TestAccountFeeInfoStore_File
    {
        [TestMethod]
        public void TestAccountFeeStore()
        {
            String outputPath = TestCaseManager.GetTestCasePath(GetType(), "accountfee");
            string instrumentPath = TestCaseManager.GetTestCasePath(GetType(), "Store_AccountFee");

            AccountFeeInfoStore_File store = new AccountFeeInfoStore_File(instrumentPath);
            List<AccountFeeInfo> codes = store.LoadAllAccountFee();

            AccountFeeInfoStore_File store2 = new AccountFeeInfoStore_File(outputPath);
            store2.SaveAccountFee(codes);
            List<AccountFeeInfo> codes2 = store2.LoadAllAccountFee();

            AssertUtils.AssertEqual_List_ToString(codes, codes2);
            File.Delete(outputPath);
        }

       // [TestMethod]
        public void PrintAccountFee()
        {
            AccountFeeInfo feeInfo1 = new AccountFeeInfo("m1705", 1, 1, 0, CommissionChargeCalcType.Fix);
            AccountFeeInfo feeInfo2 = new AccountFeeInfo("m1709", 5, 5, 3, CommissionChargeCalcType.Fix);
            AccountFeeInfo feeInfo3 = new AccountFeeInfo("m1701", 5, 5, 3, CommissionChargeCalcType.Fix);
            AccountFeeInfo feeInfo4 = new AccountFeeInfo("m1707", 10, 5, 0.0001, CommissionChargeCalcType.Percent);
            AccountFeeInfo feeInfo5 = new AccountFeeInfo("m1703", 10, 10, 0.0001, CommissionChargeCalcType.Percent);
            List<AccountFeeInfo> feeInfos = new List<AccountFeeInfo>();
            feeInfos.Add(feeInfo1);
            feeInfos.Add(feeInfo2);
            feeInfos.Add(feeInfo3);
            feeInfos.Add(feeInfo4);
            feeInfos.Add(feeInfo5);
            AssertUtils.PrintLineList(feeInfos);
        }
    }
}
