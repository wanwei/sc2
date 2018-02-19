using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.update;
using com.wer.sc.plugin.cnstock.historydata.download.sina;
using com.wer.sc.plugin.historydata;
using com.wer.sc.plugin.historydata.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.dataupdater
{
    /// <summary>
    /// 国内期货市场历史数据的准备步骤
    /// </summary>
    public class StepPreparer
    {
        private const int DAYS_EVERYTICKSTEP = 10;

        private const int DAYS_EVERYKLINESTEP = 50;

        private string srcDataPath;

        private string targetDataPath;

        private bool updateFillUp;
        private UpdateStepGetter_CnStock updateStepGetter_CnStock;
        private PluginHelper pluginHelper;

        /// <summary>
        /// 创建一个更新准备器
        /// </summary>
        /// <param name="pluginHelper"></param>
        /// <param name="dataProvider"></param>
        /// <param name="srcDataPath"></param>
        /// <param name="targetDataPath"></param>
        /// <param name="updateFillUp"></param>
        public StepPreparer(string pluginPath, string srcDataPath, string targetDataPath, bool updateFillUp)
        {
            this.srcDataPath = srcDataPath;
            this.targetDataPath = targetDataPath;
            this.updateFillUp = updateFillUp;
        }

        public StepPreparer(UpdateStepGetter_CnStock updateStepGetter_CnStock, PluginHelper pluginHelper)
        {
            this.updateStepGetter_CnStock = updateStepGetter_CnStock;
            this.pluginHelper = pluginHelper;
        }

        public List<IStep> GetAllSteps()
        {
            List<IStep> steps = new List<IStep>();

            UpdatedDataInfo updatedDataInfo = new UpdatedDataInfo(DataConst.CSVPATH);
                        
            if (DateTime.Now.Hour > 19)
            {
                Step_SinaDownload step = new Step_SinaDownload();
                steps.Add(step);
            }

            Step_TradingDay step_TradingDay = new Step_TradingDay();
            steps.Add(step_TradingDay);

            Step_CodeInfo step_CodeInfo = new Step_CodeInfo();
            List<CodeInfo> allCodes = step_CodeInfo.GetAllCodes();
            steps.Add(step_CodeInfo);

            GetTradingTime(steps, allCodes, false);
            GetTickSteps(steps, updatedDataInfo, allCodes);
            GetKLineDataSteps(steps, updatedDataInfo, allCodes);

            ///*
            // * 在准备更新的时候会将所有更新信息索引一次
            // * 所以在准备完更新后保存一次
            // */
            //updatedDataInfo.Save();
            return steps;
        }

        private void GetTradingTime(List<IStep> steps, List<CodeInfo> allCodes, bool v)
        {
            Download_Sina downloader = new Download_Sina(DataConst.SINAPATH);
            for (int i = 0; i < allCodes.Count; i++)
            {
                Step_TradingTime step = new Step_TradingTime(downloader, allCodes[i].Code);
                steps.Add(step);
            }
        }

        private void GetTickSteps(List<IStep> steps, UpdatedDataInfo updatedDataInfo, List<CodeInfo> allCodes)
        {
            for (int i = 0; i < allCodes.Count; i++)
            {
                Step_TickData_Code step = new Step_TickData_Code(allCodes[i].Code);
                steps.Add(step);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps, UpdatedDataInfo updatedDataInfo, List<CodeInfo> allCodes)
        {
            for (int i = 0; i < allCodes.Count; i++)
            {
                Step_KLineData step = new Step_KLineData(allCodes[i].Code);
                steps.Add(step);
            }
        }

    }
}