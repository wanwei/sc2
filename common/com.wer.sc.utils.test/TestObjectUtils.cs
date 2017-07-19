using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.test
{

    [TestClass]
    public class TestObjectUtils
    {

        public void testString2Object()
        {
            Assert.AreEqual(123, ObjectUtils.String2Object("123", ObjectUtils.TYPE_INTEGER));
            Assert.AreEqual(123l, ObjectUtils.String2Object("123", ObjectUtils.TYPE_LONG));
            Assert.AreEqual(123.0f, ObjectUtils.String2Object("123", ObjectUtils.TYPE_FLOAT));
            Assert.AreEqual(123.0, ObjectUtils.String2Object("123", ObjectUtils.TYPE_DOUBLE));
            Assert.AreEqual("123", ObjectUtils.String2Object("123", ObjectUtils.TYPE_STRING));
            Assert.AreEqual(true, ObjectUtils.String2Object("true", ObjectUtils.TYPE_BOOLEAN));
            Assert.AreEqual(true, ObjectUtils.String2Object("1", ObjectUtils.TYPE_BOOLEAN));
            Assert.AreEqual(false, ObjectUtils.String2Object("false", ObjectUtils.TYPE_BOOLEAN));
            Assert.AreEqual(false, ObjectUtils.String2Object("0", ObjectUtils.TYPE_BOOLEAN));
            Assert.AreEqual("123", ObjectUtils.String2Object("123", "c"));
            Assert.AreEqual("123", ObjectUtils.String2Object("123", " c "));

            ObjectUtilsMock mock = new ObjectUtilsMock("123");
            Assert.AreEqual(mock, ObjectUtils.String2Object(mock.SaveToString(), ObjectUtilsMock.class));

        Assert.AreEqual(mock, ObjectUtils.String2Object(mock.saveToString(), "com.wer.commons.utils.ObjectUtilsMock"));
	}

    public void testObjectType()
    {
        Assert.AreEqual('i', ObjectUtils.objectType(123));
        Assert.AreEqual(new ObjectUtilsMock().getClass(), ObjectUtils.objectType(new ObjectUtilsMock()));
    }
}

class ObjectUtilsMock : TextExchange
{

    private String str;

    public ObjectUtilsMock()
    {

    }

    public ObjectUtilsMock(String str)
    {
        this.str = str;
    }

    public String saveToString()
    {
        return str;
    }

    public void loadFromString(String content)
    {
        this.str = content;
    }

    public Boolean equals(Object obj)
    {
        if (!(obj is ObjectUtilsMock))
            return false;
        return this.str.Equals(((ObjectUtilsMock)obj).str);
    }
}
}