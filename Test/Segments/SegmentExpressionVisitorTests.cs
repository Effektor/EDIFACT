using EDIFACT;
using NUnit.Framework;

namespace EDIFACT.Tests.Segments
{
    public class SegmentExpressionVisitorTests
    {
        [Test]
        public void Indexer_ReturnsFirstElement()
        {
            var segment = new Segment("TAG")
                .AddElement("first");

            var value = segment[1];

            Assert.That(value.ToString(), Is.EqualTo("first"));
        }

        [Test]
        public void Indexer_ReturnsSecondElement()
        {
            var segment = new Segment("TAG")
                .AddElement("first")
                .AddElement("second");

            var value = segment[1];

            Assert.That(value.ToString(), Is.EqualTo("second"));
        }

        [Test]
        [Ignore("Not implemented")]
        public void Indexer_FirstOfMany_NotYetSupported()
        {
            var segment = new Segment("TAG")
                .AddElement("first")
                .AddElement("second")
                .AddElement("third");

            var value = segment[1];

            Assert.That(value.ToString(), Is.EqualTo("first"));
        }

        [Test]
        [Ignore("Not implemented")]
        public void Indexer_SecondOfMany_NotYetSupported()
        {
            var segment = new Segment("TAG")
                .AddElement("first")
                .AddElement("second")
                .AddElement("third");

            var value = segment[1];

            Assert.That(value.ToString(), Is.EqualTo("second"));
        }
    }
}
