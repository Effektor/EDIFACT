using EDIFACT;
using EDIFACT.Segments;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace Edifact_Test
{
    public class Serializing
    {
       

        [Test]
        public void DynamicSegmentToSTring()
        {
            var ds = new Segment("TEST").AddElement("D");
            Assert.That("TEST+D'" == ds.ToString());
        }

       [Ignore("Not implemented")]
       [Test]
       [TestCase("QTY+12:50'", ExpectedResult =50.0)]
       [TestCase("QTY+12:50.134:KGM'", ExpectedResult = 50.134)]
       public decimal ExtractQty(string segment)
        {
            Moq.Mock<Segment> mock = new Moq.Mock<Segment>();
            mock.Setup(s => s.ToString()).Returns(segment);

            return EDIFACT.Helpers.SegmentHelpers.GetQtyValue(mock.Object);
        }
       

       

        [Test]
        public void DynamicSegmentToSTringWhenEnumerating()
        {
            List<Segment> list = new List<Segment>();
            var ds = new Segment("TEST").AddElement("D");
            list.Add(ds);

            Assert.That("TEST+D'" == list.First().ToString());
        }

        [Test]
        public void DynamicSegmentLinqCast()
        {
            List<Segment> list = new List<Segment>();
            var ds = new Segment("TEST").AddElement("D");
            list.Add(ds);
            var list2 = list.Select(x => x.ToString());
            Assert.That("TEST+D'" == list2.First());
        }

    }
}
