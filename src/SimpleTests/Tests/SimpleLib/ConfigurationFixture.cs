using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Simple.Tests.SimpleLib.Sample;
using Simple.Configuration;
using System.Xml;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class ConfigurationFixture
    {
        protected XmlElement ElementFromString(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement xmlElement = doc.DocumentElement;
            return xmlElement;
        }

        [Test]
        public void BasicTypesTest()
        {
            BasicTypesElement e = new BasicTypesElement();
            
            (e as IConfigElement).LoadFromElement(ElementFromString(
                @"<test>
                    <intValue>1</intValue>
                    <doubleValue>1.5</doubleValue>
                    <decimalValue>1.6</decimalValue>
                    <dateValue>2008-11-12 12:13:14</dateValue>
                </test>"));

            Assert.AreEqual(1, e.IntValue);
            Assert.AreEqual(1.5, e.DoubleValue);
            Assert.AreEqual(1.6, e.DecimalValue);
            Assert.AreEqual(new DateTime(2008, 11, 12, 12, 13, 14), e.DateValue);
        }

        [Test]
        public void ComplexTypesTest()
        {
            ComplexElement e = new ComplexElement();

            string s1 = @"<intValue>1</intValue>
                    <doubleValue>1.5</doubleValue>
                    <decimalValue>1.6</decimalValue>
                    <dateValue>2008-11-12 12:13:14</dateValue>";

            (e as IConfigElement).LoadFromElement(ElementFromString(
                @"<test><list1>%s</list1><list1>%s</list1><list1>%s</list1>
                  <dic1 key='1'>%s</dic1><dic1 key='2'>%s</dic1><dic1 key='3'>%s</dic1></test>".Replace("%s",s1)));

            foreach (BasicTypesElement b in e.List1)
            {
                Assert.AreEqual(1, b.IntValue);
                Assert.AreEqual(1.5, b.DoubleValue);
                Assert.AreEqual(1.6, b.DecimalValue);
                Assert.AreEqual(new DateTime(2008, 11, 12, 12, 13, 14), b.DateValue);
            }

            for (int i = 1; i <= 3; i++)
            {
                Assert.AreEqual(1, e.Dic1[i].IntValue);
                Assert.AreEqual(1.5, e.Dic1[i].DoubleValue);
                Assert.AreEqual(1.6, e.Dic1[i].DecimalValue);
                Assert.AreEqual(new DateTime(2008, 11, 12, 12, 13, 14), e.Dic1[i].DateValue);

            }
        }
    }
}
