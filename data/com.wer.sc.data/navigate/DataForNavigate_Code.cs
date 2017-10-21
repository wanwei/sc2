using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.data.navigate
{
    public class DataForNavigate_Code : DataForForward_Code
    {
        public DataForNavigate_Code(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods) : base(dataPackage, referedPeriods)
        {
        }

        public void NavigateTo(double time)
        {

        }
    }
}
