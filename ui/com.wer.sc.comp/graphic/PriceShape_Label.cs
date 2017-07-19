using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class PriceShape_Label : PriceShape
    {
        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Label;
        }
    }
}
