﻿using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    [TestClass]
    public class TestCodeReader
    {
        [TestMethod]
        public void TestGetAllInstruments()
        {
            IDataReader dataReader = DataReaderFactory.CreateDataReader(DataCenterUri.URI);
            List<CodeInfo> instruments = dataReader.CodeReader.GetAllCodes();
            AssertUtils.PrintLineList(instruments);
            AssertUtils.AssertEqual_List("instruments", GetType(), instruments);

            CodeInfo instrument = dataReader.CodeReader.GetCodeInfo("m05");
            Assert.AreEqual("M05", instrument.Code);
            Assert.AreEqual("豆粕05", instrument.Name);
            Assert.AreEqual("M", instrument.Catelog);
        }

        [TestMethod]
        public void TestGetAllCatelogs()
        {
            IDataReader dataReader = DataReaderFactory.CreateDataReader(DataCenterUri.URI);
            List<string> catelogs = dataReader.CodeReader.GetAllCatelogs();
            Assert.AreEqual(1, catelogs.Count);
            Assert.AreEqual("M", catelogs[0]);
        }
    }
}
