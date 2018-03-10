using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyArguments_DataPackageLink : StrategyArgumentsAbstract
    {
        private bool closeOnPackageChanged = true;

        /// <summary>
        /// 
        /// </summary>
        public bool CloseOnPackageChanged
        {
            get
            {
                return closeOnPackageChanged;
            }

            set
            {
                closeOnPackageChanged = value;
            }
        }

        private List<IDataPackage_Code> dataPackages = new List<IDataPackage_Code>();

        public List<IDataPackage_Code> DataPackages
        {
            get
            {
                return dataPackages;
            }
        }
    }
}
