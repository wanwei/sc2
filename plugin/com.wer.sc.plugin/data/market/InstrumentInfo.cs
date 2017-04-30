using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{

    /// <summary>
    /// 品种信息
    /// </summary>
    public class InstrumentInfo
    {
        /// <summary>
        /// 品种名称
        /// </summary>
        public string InstrumentName;

        /// <summary>
        /// 品种ID
        /// </summary>
        public string Symbol;

        /// <summary>
        /// 该品种在数据中心保存时的ID
        /// 这个是为期货准备的，因为期货请求ID带年，如rb1705
        /// 但是保存时是不带年的，如rb05
        /// </summary>
        public string SaveID;

        /// <summary>
        /// 交易所ID
        /// </summary>
        public string ExchangeID;

        /// <summary>
        /// 合约数量乘数
        /// </summary>
        public int VolumeMultiple;

        /// <summary>
        /// 最小变动价位
        /// </summary>
        public double PriceTick;

        /// <summary>
        /// 到期日
        /// </summary>
        public int ExpireDate;

        /// <summary>
        /// 执行价
        /// </summary>
        public double StrikePrice;

        /// <summary>
        /// 产品代码
        /// </summary>        
        public string ProductID;

        /// <summary>
        /// 基础商品代码
        /// </summary>
        public string UnderlyingInstrID;

        ///合约生命周期状态
        public InstLifePhaseType InstLifePhase;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(InstrumentName).Append(",");
            sb.Append(Symbol).Append(",");
            sb.Append(ExchangeID).Append(",");
            sb.Append(VolumeMultiple).Append(",");
            sb.Append(PriceTick).Append(",");
            sb.Append(ExpireDate).Append(",");
            sb.Append(StrikePrice).Append(",");
            sb.Append(ProductID).Append(",");
            sb.Append(UnderlyingInstrID).Append(",");
            sb.Append(Enum.GetName(typeof(InstLifePhaseType), InstLifePhase));
            return sb.ToString();
        }

        public void LoadFromString(String content)
        {
            string[] strs = content.Split(',');

            this.InstrumentName = strs[0];
            this.Symbol = strs[1];
            this.ExchangeID = strs[2];
            this.VolumeMultiple = int.Parse(strs[3]);
            this.PriceTick = double.Parse(strs[4]);
            this.ExpireDate = int.Parse(strs[5]);
            this.StrikePrice = double.Parse(strs[6]);
            this.ProductID = strs[7];
            this.UnderlyingInstrID = strs[8];
            this.InstLifePhase = (InstLifePhaseType)Enum.Parse(typeof(InstLifePhaseType), strs[9]);
        }
    }
}
