using System.Collections.Generic;
using System.Linq;
using EDIFACT;
using NUnit.Framework;

namespace EDIFACT.Tests.Serialization
{
    public class SegmentSerializationTests
    {
        [Test]
        public void Segment_ToString_RendersElements()
        {
            var segment = new Segment("TEST").AddElement("D");

            Assert.That(segment.ToString(), Is.EqualTo("TEST+D'"));
        }

        [Test]
        public void Segment_ToString_FromEnumeration()
        {
            var list = new List<Segment>
            {
                new Segment("TEST").AddElement("D")
            };

            Assert.That(list.First().ToString(), Is.EqualTo("TEST+D'"));
        }

        [Test]
        public void Segment_LinqProjection_RendersStrings()
        {
            var list = new List<Segment>
            {
                new Segment("TEST").AddElement("D")
            };

            var projected = list.Select(x => x.ToString()).First();

            Assert.That(projected, Is.EqualTo("TEST+D'"));
        }

        [Test]
        [Ignore("Not implemented")]
        [TestCase("QTY+12:50'", ExpectedResult = 50.0)]
        [TestCase("QTY+12:50.134:KGM'", ExpectedResult = 50.134)]
        public decimal ExtractQty_ReturnsNumericValue(string segment)
        {
            var mock = new Moq.Mock<Segment>();
            mock.Setup(s => s.ToString()).Returns(segment);

            return EDIFACT.Helpers.SegmentHelpers.GetQtyValue(mock.Object);
        }
    }
}
