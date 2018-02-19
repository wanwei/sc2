using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 将一个策略在多个上验证
    /// </summary>
    public class StrategyExecutor_HistoryMulti
    {
        private int threadCount = 5;

        private CodePackageInfo codePackageInfo;

        private CodePackage codePackage;

        private List<StrategyExecutor_History> executors = new List<StrategyExecutor_History>();

        public StrategyExecutor_HistoryMulti(IDataCenter dataCenter, CodePackageInfo codePackageInfo)
        {
            this.codePackageInfo = codePackageInfo;
            CodePackageFactory fac = new CodePackageFactory(dataCenter.DataReader);
            this.codePackage = fac.CreateCodePackage(codePackageInfo);
        }

        public void Execute()
        {
            if (this.codePackage.IsMainContract)
            {
                Execute_MainContract();
            }
            else
            {
                Execute_Normal();
            }
        }

        private void Execute_MainContract()
        {

        }

        private void Execute_Normal()
        {
            //for(int i=0;i<)            
            //codePackage.Codes
        }
    }    
}
