using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic.shape
{
    public interface IShape : IXmlExchange
    {
        ShapeType GetShapeType();
    }
}