using com.wer.sc.data;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.config.test
{
    [TestClass]
    public class TestCodeInfoGenerator
    {
        [TestMethod]
        public void TestCodeInfoGenerate()
        {
            DataLoader_InstrumentInfo generator = new DataLoader_InstrumentInfo("");
            List<CodeInfo> codes = generator.GetAllInstruments();
            AssertUtils.PrintLineList(codes);
            //for (int i = 0; i < codes.Count; i++)
            //{
            //    Console.WriteLine(codes[i] + generator.GetOldCodeInfo(codes[i].Code).Code);
            //}
        }

        string srcPath = @"E:\FUTURES\CSV\TICKADJUSTED\";
        string targetPath = @"E:\FUTURES\CSV\DATACENTERSOURCE\";

        [TestMethod]
        public void TestAdjust()
        {
            DataLoader_InstrumentInfo gen = new DataLoader_InstrumentInfo("");
            List<CodeInfo> codes = gen.GetAllInstruments();
            //codes.Count
            //for (int i = 0; i < codes.Count; i++)
            for (int i = 0; i < 2; i++)
            {
                CodeInfo code = codes[i];
                Adjust(code, gen.GetOldCodeInfo(code.Code).Code);
            }
        }

        private void Adjust(CodeInfo code, string oldCodeId)
        {
            if (code.Code.EndsWith("MI"))
            {
                return;
            }
            if (code.Code.EndsWith("0000"))
            {
                AdjustIndex(code, oldCodeId);
                return;
            }
            int startDate = code.Start;
            int endDate = code.End;
            for (int i = startDate; i <= endDate; i++)
            {
                string srcPath = GetSrcPath(code, oldCodeId, i);
                string targetPath = GetTargetPath(code, oldCodeId, i);
                Console.WriteLine(srcPath + "|" + targetPath);
                //if (File.Exists(srcPath))
                //    File.Move(srcPath, targetPath);
            }
        }

        private void AdjustIndex(CodeInfo code, string oldCodeId)
        {
            string srcPath = this.srcPath + oldCodeId + "\\tick\\";
            string targetPath = this.targetPath + code.Code + "\\tick\\";
            Console.WriteLine(srcPath + "|" + targetPath);
            //Directory.Move(srcDir, targetDir);
        }

        private string GetSrcPath(CodeInfo code, string oldCodeId, int date)
        {
            //return srcPath + oldCodeId + "\\tick\\" + oldCodeId + "_" + date + ".csv";
            return GetTickPath(srcPath, oldCodeId, date);
        }

        private string GetTargetPath(CodeInfo code, string oldCodeId, int date)
        {
            return GetTickPath(targetPath, code.Code, date);
        }

        private string GetTickPath(string path, string codeId, int date)
        {
            return path + codeId + "\\tick\\" + codeId + "_" + date + ".csv";
        }
    }
}
