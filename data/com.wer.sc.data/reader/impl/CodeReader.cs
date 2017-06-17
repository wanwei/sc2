using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.impl
{
    public class CodeReader : ICodeReader
    {
        private List<CodeInfo> codes = new List<CodeInfo>();

        private Dictionary<String, CodeInfo> dicCodes = new Dictionary<string, CodeInfo>();

        private List<String> catelogs = new List<string>();

        private Dictionary<String, List<String>> dicCatelogCode = new Dictionary<string, List<string>>();

        private IInstrumentStore instrumentStore;

        internal CodeReader(IInstrumentStore instrumentStore)
        {
            this.instrumentStore = instrumentStore;
            init();
        }

        private void init()
        {
            List<CodeInfo> codeInfos = instrumentStore.Load();
            for (int i = 0; i < codeInfos.Count; i++)
            {
                CodeInfo codeInfo = codeInfos[i];
                codes.Add(codeInfo);
                dicCodes.Add(codeInfo.Code.ToUpper(), codeInfo);
                String catelog = codeInfo.Catelog;
                if (!catelogs.Contains(catelog))
                    catelogs.Add(catelog);
                String code = codeInfo.Code;
                AddDicCodeCatelog(code, catelog);
            }
        }

        private void AddDicCodeCatelog(String code, String catelog)
        {
            List<String> codes;
            if (dicCatelogCode.ContainsKey(catelog))
            {
                codes = dicCatelogCode[catelog];
            }
            else
            {
                codes = new List<string>();
                dicCatelogCode.Add(catelog, codes);
            }
            codes.Add(code);
        }

        public List<CodeInfo> GetAllCodes()
        {
            return codes;
        }

        public bool Contain(String code)
        {
            return dicCodes.ContainsKey(code.ToUpper());
        }

        public CodeInfo GetCodeInfo(String code)
        {
            return dicCodes[code.ToUpper()];
        }

        public List<String> GetAllCatelogs()
        {
            return catelogs;
        }

        public List<String> GetCodesByCatelog(String catelog)
        {
            return dicCatelogCode[catelog];
        }

        public void Refresh()
        {
            codes.Clear();
            dicCodes.Clear();
            catelogs.Clear();
            dicCatelogCode.Clear();
            init();
        }
    }
}