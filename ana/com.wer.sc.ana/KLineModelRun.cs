using com.wer.sc.data;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{
    public class KLineModelRun
    {
        /**
         * 要引用的模型
         */
        public IPlugin_KLineModel Model;

        public KLineData Data;

        public void Execute()
        {
            Model.ModelStart();
            while (true)
            {
                try
                {
                    Model.ModelLoop();
                }
                catch (Exception e)
                {
                    //e.StackTrace();
                }
                if (Model.BarPos == Data.Length - 1)
                    break;
                else
                    Model.NextBarPos();
            }
            //		for (int i = 0; i < data.getLength(); i++) {
            //			
            //		}
            Model.ModelEnd();
        }
    }
}
