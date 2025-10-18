using System;
using EDIFACT;
using EDIFACT.Segments;
using NUnit.Framework;

namespace EDIFACT.Tests.Segments
{
    public class SegmentBuilderTests
    {
        [Test]
        public void CpsSegment_RendersSingleElement()
        {
            var cps = new Segment("CPS").AddElement("2");

            Assert.That(cps.ToString(), Is.EqualTo("CPS+2'"));
        }

        [Test]
        public void PacSegment_RendersTrailingEmptyElements()
        {
            var pac = new Segment("PAC")
                .AddElement()
                .AddElement()
                .AddElement("CW");

            Assert.That(pac.ToString(), Is.EqualTo("PAC+++CW'"));
        }

        [Test]
        public void MeaSegment_WithCompositeValues()
        {
            var mea = new Segment("MEA")
                .AddElement("PD")
                .AddElement("AAD")
                .AddComposite("KGM", 500);

            Assert.That(mea.ToString(), Is.EqualTo("MEA+PD+AAD+KGM:500'"));
        }

        [Test]
        public void PciSegment_WithSingleElement()
        {
            var pci = new Segment("PCI").AddElement("39");

            Assert.That(pci.ToString(), Is.EqualTo("PCI+39'"));
        }

        [Test]
        public void GinSegment_WithQualifierAndValue()
        {
            var gin = new Segment("GIN")
                .AddElement("SRV")
                .AddElement("07300015200017");

            Assert.That(gin.ToString(), Is.EqualTo("GIN+SRV+07300015200017'"));
        }

        [Test]
        public void LinSegment_WithComposite()
        {
            var lin = new Segment("LIN")
                .AddElement("1")
                .AddElement()
                .AddComposite("07300015200154", "SRV");

            Assert.That(lin.ToString(), Is.EqualTo("LIN+1++07300015200154:SRV'"));
        }

        [Test]
        public void PiaSegment_WithSaQualifier()
        {
            var pia = new Segment("PIA")
                .AddElement("1")
                .AddComposite(1250, "SA");

            Assert.That(pia.ToString(), Is.EqualTo("PIA+1+1250:SA'"));
        }

        [Test]
        public void PiaSegment_WithNbQualifier()
        {
            var pia = new Segment("PIA")
                .AddElement("1")
                .AddComposite("AB152715", "NB");

            Assert.That(pia.ToString(), Is.EqualTo("PIA+1+AB152715:NB'"));
        }

        [Test]
        public void PiaSegment_WithSnQualifier()
        {
            var pia = new Segment("PIA")
                .AddElement("1")
                .AddComposite("878498987656", "SN");

            Assert.That(pia.ToString(), Is.EqualTo("PIA+1+878498987656:SN'"));
        }

        [Test]
        public void PiaSegment_WithSrvQualifier()
        {
            var pia = new Segment("PIA")
                .AddElement("4")
                .AddComposite("07300015200161", "SRV");

            Assert.That(pia.ToString(), Is.EqualTo("PIA+4+07300015200161:SRV'"));
        }

        [Test]
        public void PiaSegment_WithSaQualifierOnFourthElement()
        {
            var pia = new Segment("PIA")
                .AddElement("4")
                .AddComposite(8954, "SA");

            Assert.That(pia.ToString(), Is.EqualTo("PIA+4+8954:SA'"));
        }

        [Test]
        public void MeaSegment_AlternateQualifier()
        {
            var mea = new Segment("MEA")
                .AddElement("PD")
                .AddElement("AAC")
                .AddComposite("KGM", 200);

            Assert.That(mea.ToString(), Is.EqualTo("MEA+PD+AAC+KGM:200'"));
        }

        [Test]
        public void QtySegment_WithCompositeQuantity()
        {
            var qty = new Segment("QTY").AddComposite("12", "50");

            Assert.That(qty.ToString(), Is.EqualTo("QTY+12:50'"));
        }

        [Test]
        public void DtmSegment_BuildsFromDtmHelper()
        {
            var dtm = new DTM()
                .WithDateformat("yyyyMMdd")
                .AddComposite(361, new DateTime(2020, 12, 24), 102);

            Assert.That(dtm.ToString(), Is.EqualTo("DTM+361:20201224:102'"));
        }

        [Test]
        public void DtmSegment_AllowsNullComponent()
        {
            var dtm = new DTM()
                .WithDateformat("yyyyMMdd")
                .AddComposite(361, DataNull.Value, 102);

            Assert.That(dtm.ToString(), Is.EqualTo("DTM+361::102'"));
        }

        [Test]
        public void DtmSegment_CompositeFromString()
        {
            var dtm = new Segment("DTM")
                .AddComposite(36, new DateTime(2020, 12, 24).ToString("yyyyMMdd"), 102);

            Assert.That(dtm.ToString(), Is.EqualTo("DTM+36:20201224:102'"));
        }

        [Test]
        public void RffSegment_WithReference()
        {
            var rff = new Segment("RFF")
                .AddComposite("ON", 73500010009921111, 10);

            Assert.That(rff.ToString(), Is.EqualTo("RFF+ON:73500010009921111:10'"));
        }

        [Test]
        public void QvrSegment_WithDelta()
        {
            var qvr = new Segment("QVR")
                .AddComposite(-5, 21)
                .AddElements("CP", "AV");

            Assert.That(qvr.ToString(), Is.EqualTo("QVR+-5:21+CP+AV'"));
        }

        [Test]
        public void DtmSegment_WithDate64()
        {
            var dtm = new Segment("DTM")
                .AddComposite(64, new DateTime(2018, 4, 3).ToString("yyyyMMdd"), 102);

            Assert.That(dtm.ToString(), Is.EqualTo("DTM+64:20180403:102'"));
        }

        [Test]
        public void QvrSegment_WithPositiveDelta()
        {
            var qvr = new Segment("QVR")
                .AddComposite(9, "21")
                .AddElement("AC")
                .AddElement("PC");

            Assert.That(qvr.ToString(), Is.EqualTo("QVR+9:21+AC+PC'"));
        }

        [Test]
        public void CntSegment_WithTotals()
        {
            var cnt = new Segment("CNT").AddComposite(1, 62);

            Assert.That(cnt.ToString(), Is.EqualTo("CNT+1:62'"));
        }
    }
}
