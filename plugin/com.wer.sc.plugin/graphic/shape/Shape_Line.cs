using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.graphic.shape
{
    public class Shape_Line : IShape
    {
        public ShapeType GetShapeType()
        {
            return ShapeType.Line;
        }

        public void Save(XmlElement xmlElem)
        {

        }

        public void Load(XmlElement xmlElem)
        {

        }

    }
}
