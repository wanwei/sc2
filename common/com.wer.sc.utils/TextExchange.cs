using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{

    /// <summary>
    /// 本接口专用于实体类和文本交互，如将BonusInfo保存成","分隔的字符串，并且能从该
    /// </summary>
    public interface TextExchange
    {
        String SaveToString();

        void LoadFromString(String content);
    }
}
