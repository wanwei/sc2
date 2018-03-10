using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略绘图容器
    /// </summary>
    public interface IStrategyGraphicContainer : IXmlExchange
    {
        IStrategyGraphic GetGraphic(IDataKey graphicKey);

        IList<KLineKey> GetKLineKeies();

        IList<TimeLineKey> GetTimeLineKeies();

        IList<IDataKey> GetGraphicKeies();

        IList<IStrategyGraphic> GetAllGraphics();
    }
}