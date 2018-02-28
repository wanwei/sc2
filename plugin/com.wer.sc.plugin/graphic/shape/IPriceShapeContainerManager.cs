using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic.shape
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPriceShapeContainerManager : IXmlExchange
    {
        /// <summary>
        /// 增加图形容器
        /// </summary>
        /// <param name="priceShapeContainer"></param>
        void AddContainer(IPriceShapeContainer priceShapeContainer);

        /// <summary>
        /// 删除图形容器
        /// </summary>
        /// <param name="priceShapeContainer"></param>
        void RemoveContainer(IPriceShapeContainer priceShapeContainer);

        IPriceShapeContainer GetShapeContainer(IDataKey graphicKey);

        IList<KLineKey> GetKLineKeies();

        IList<TimeLineKey> GetTimeLineKeies();

        IList<IDataKey> GetGraphicKeies();

        IList<IPriceShapeContainer> GetAllPriceShapeContainer();
    }
}
