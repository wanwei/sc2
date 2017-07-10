using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 账户交易成本
    /// </summary>
    public class AccountFeeInfo
    {
        public AccountFeeInfo()
        {
        }

        public AccountFeeInfo(string code, int volumeMultiple, double marginRatio, double commissionCharge, CommissionChargeCalcType feeCalcType)
        {
            this.Code = code;
            this.VolumeMultiple = volumeMultiple;
            this.MarginRatio = marginRatio;
            this.CommissionCharge = commissionCharge;
            this.FeeCalcType = feeCalcType;
        }

        /// <summary>
        /// 合约ID
        /// </summary>
        public String Code;

        /// <summary>
        /// 每手数量
        /// </summary>
        public int VolumeMultiple;

        /// <summary>
        /// 保证金比率
        /// </summary>
        public double MarginRatio;

        /// <summary>
        /// 手续费
        /// </summary>
        public double CommissionCharge;

        /// <summary>
        /// 手续费计算方式
        /// </summary>
        public CommissionChargeCalcType FeeCalcType;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Code).Append(",");
            sb.Append(VolumeMultiple).Append(",");
            sb.Append(MarginRatio).Append(",");
            sb.Append(CommissionCharge).Append(",");
            sb.Append(FeeCalcType);
            return sb.ToString();
        }
    }

    /// <summary>
    /// 账户手续费计算方式
    /// </summary>
    public enum CommissionChargeCalcType
    {
        /// <summary>
        /// 固定手续费
        /// </summary>
        Fix,

        /// <summary>
        /// 百分比手续费
        /// </summary>
        Percent
    }
}
