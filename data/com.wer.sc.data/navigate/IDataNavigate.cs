using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    public interface IDataNavigate : IDataNavigate_Code
    {
        void ChangeCode(string code);
    }
}
