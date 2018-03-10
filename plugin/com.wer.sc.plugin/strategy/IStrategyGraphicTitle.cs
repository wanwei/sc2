using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyGraphicTitle : IXmlExchange
    {
        int X { get; set; }

        string Text { get; set; }

        Color Color { get; set; }
    }
}
