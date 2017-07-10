using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.impl
{
    public class AccountFee
    {
        private AccountFeeInfo defaultFeeInfo = new AccountFeeInfo("", 1, 1, 0, CommissionChargeCalcType.Fix);

        private Dictionary<string, AccountFeeInfo> dic_Code_Fee = new Dictionary<string, AccountFeeInfo>();

        /// <summary>
        /// 创建交易费用类，交易费用默认从
        /// </summary>
        public AccountFee()
        {

        }

        public AccountFee(List<AccountFeeInfo> fees)
        {
            for (int i = 0; i < fees.Count; i++)
            {
                AccountFeeInfo fee = fees[i];
                if (StringUtils.IsEmpty(fee.Code))
                    continue;
                if (dic_Code_Fee.ContainsKey(fee.Code))
                    continue;
                dic_Code_Fee.Add(fee.Code, fee);
            }
        }

        public AccountFeeInfo GetAccountFee(string code)
        {
            if (dic_Code_Fee.ContainsKey(code))
                return dic_Code_Fee[code];
            return defaultFeeInfo;
        }

        public static double CalcMoney_Open(OrderInfo order, AccountFeeInfo accountFee, int mount)
        {
            if (accountFee.FeeCalcType == CommissionChargeCalcType.Fix)
                return (order.Price + accountFee.CommissionCharge) * accountFee.VolumeMultiple / accountFee.MarginRatio * mount;
            else if (accountFee.FeeCalcType == CommissionChargeCalcType.Percent)
            {
                return (order.Price * accountFee.VolumeMultiple / accountFee.MarginRatio) * mount * (1 + accountFee.CommissionCharge);
            }
            return 0;
        }

        public static double CalcMoney_Close(OrderInfo order, AccountFeeInfo accountFee, int mount)
        {
            if (accountFee.FeeCalcType == CommissionChargeCalcType.Fix)
                return (order.Price - accountFee.CommissionCharge) * accountFee.VolumeMultiple / accountFee.MarginRatio * mount;
            else if (accountFee.FeeCalcType == CommissionChargeCalcType.Percent)
            {
                return (order.Price * accountFee.VolumeMultiple / accountFee.MarginRatio) * mount * (1 - accountFee.CommissionCharge);
            }
            return 0;
        }
    }
}