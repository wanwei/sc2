using com.wer.sc.data.market;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    public class AccountFeeInfoStore_File
    {
        private string path;

        public AccountFeeInfoStore_File(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// 装载所有的交易费用数据
        /// </summary>
        /// <returns></returns>
        public List<AccountFeeInfo> LoadAllAccountFee()
        {
            List<AccountFeeInfo> feeInfos = new List<AccountFeeInfo>();
            string[] contents = File.ReadAllLines(path);
            for (int i = 0; i < contents.Length; i++)
            {
                AccountFeeInfo feeinfo = LoadAccountFeeInfo(contents[i]);
                feeInfos.Add(feeinfo);
            }
            return feeInfos;
        }

        private AccountFeeInfo LoadAccountFeeInfo(string content)
        {
            string[] arr = content.Split(',');
            AccountFeeInfo feeInfo = new AccountFeeInfo();
            feeInfo.Code = arr[0];
            feeInfo.VolumeMultiple = int.Parse(arr[1]);
            feeInfo.MarginRatio = double.Parse(arr[2]);
            feeInfo.CommissionCharge = double.Parse(arr[3]);
            feeInfo.FeeCalcType = (CommissionChargeCalcType)Enum.Parse(typeof(CommissionChargeCalcType), arr[4]);
            return feeInfo;
        }

        public void SaveAccountFee(List<AccountFeeInfo> accountFeeInfo)
        {
            string[] contents = new string[accountFeeInfo.Count];
            for (int i = 0; i < accountFeeInfo.Count; i++)
            {
                contents[i] = accountFeeInfo[i].ToString();
            }
            File.WriteAllLines(path, contents);
        }
    }
}
