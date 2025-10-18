using EDIFACT;
using EDIFACT.Segments;
using NUnit.Framework;
using System;

namespace Edifact_Test
{
    public class FluentSegmentTests
    {
        [Test]
        public void CPS()
        {
            var cps = new Segment("CPS")
                .AddElement("2");
            Assert.That("CPS+2'" == cps.ToString());
        }

        [Test]
        public void PAC()
        {
            var cps = new Segment("PAC")
                .AddElement()
                .AddElement()
                .AddElement("CW");
            Assert.That("PAC+++CW'" == cps.ToString());
        }

        [Test]
        public void MEA()
        {
            var cps = new Segment("MEA")
                .AddElement("PD")
                .AddElement("AAD")
                .AddComposite("KGM", 500);

            Assert.That("MEA+PD+AAD+KGM:500'" == cps.ToString());
        }

        [Test]
        public void PCI()
        {
            var cps = new Segment("PCI")
                .AddElement("39");
            Assert.That("PCI+39'" == cps.ToString());
        }

        [Test]
        public void GIN()
        {
            var cps = new Segment("GIN")
                .AddElement("SRV")
                .AddElement("07300015200017");
            Assert.That("GIN+SRV+07300015200017'" == cps.ToString());
        }

        [Test]
        public void LIN()
        {
            var cps = new Segment("LIN")
                .AddElement("1")
                .AddElement()
                .AddComposite("07300015200154", "SRV");
            Assert.That("LIN+1++07300015200154:SRV'" == cps.ToString());
        }

        [Test]
        public void PIA_SA()
        {
            var cps = new Segment("PIA")
                .AddElement("1")
                .AddComposite(1250, "SA");
            Assert.That("PIA+1+1250:SA'" == cps.ToString());
        }

        [Test]
        public void PIA_NB()
        {
            var cps = new Segment("PIA")
                .AddElement("1").AddComposite("AB152715", "NB");

            Assert.That("PIA+1+AB152715:NB'" == cps.ToString());
        }

        [Test]
        public void PIA_SN()
        {
            var cps = new Segment("PIA")
                .AddElement("1")
                .AddComposite("878498987656", "SN");
            Assert.That("PIA+1+878498987656:SN'" == cps.ToString());
        }

        [Test]
        public void PIA_SRV()
        {
            var cps = new Segment("PIA")
                .AddElement("4")
                .AddComposite("07300015200161", "SRV");
            Assert.That("PIA+4+07300015200161:SRV'" == cps.ToString());
        }

        [Test]
        public void PIA_4SA()
        {
            var cps = new Segment("PIA")
                .AddElement("4")
                .AddComposite(8954, "SA");
            Assert.That("PIA+4+8954:SA'" == cps.ToString());
        }



        [Test]
        public void MEA_PD()
        {
            var cps = new Segment("MEA")
                .AddElement("PD")
                .AddElement("AAC")
                .AddComposite("KGM", 200);
            Assert.That("MEA+PD+AAC+KGM:200'" == cps.ToString());
        }

        [Test]
        public void QTY()
        {
            var cps = new Segment("QTY")
                .AddComposite("12", "50");
            Assert.That("QTY+12:50'" == cps.ToString());
        }

        [Test]
        public void DTM_361()
        {
            var cps = new DTM()
                .WithDateformat("yyyyMMdd")
                .AddComposite(361, new DateTime(2020, 12, 24), 102);
            Assert.That("DTM+361:20201224:102'" == cps.ToString());
        }

        [Test]
        public void DTM_NullComponent()
        {
            var cps = new DTM()
                .WithDateformat("yyyyMMdd")
                .AddComposite(361, DataNull.Value, 102);
            Assert.That("DTM+361::102'"== cps.ToString());
        }

        [Test]
        public void DTM_36()
        {
            var cps = new Segment("DTM")
                .AddComposite(36, new DateTime(2020, 12, 24).ToString("yyyyMMdd"), 102);

            Assert.That("DTM+36:20201224:102'"== cps.ToString());
        }

        [Test]
        public void RFF()
        {
            var cps = new Segment("RFF")
                .AddComposite("ON", 73500010009921111, 10);
            Assert.That("RFF+ON:73500010009921111:10'" == cps.ToString());
        }

        [Test]
        public void QVR()
        {
            var cps = new Segment("QVR")
                .AddComposite(-5, 21)
                .AddElements("CP", "AV");
            Assert.That("QVR+-5:21+CP+AV'" == cps.ToString());
        }

        [Test]
        public void DTM_64()
        {
            var cps = new Segment("DTM")
                .AddComposite(64, new DateTime(2018, 04, 03).ToString("yyyyMMdd"), 102);
            Assert.That("DTM+64:20180403:102'" == cps.ToString());
        }

        [Test]
        public void QVR_9()
        {
            var cps = new Segment("QVR")
                .AddComposite(9, "21")
                .AddElement("AC")
                .AddElement("PC");
            Assert.That("QVR+9:21+AC+PC'" == cps.ToString());
        }

        [Test]
        public void CNT_1()
        {
            var cps = new Segment("CNT")
                .AddComposite(1, 62);
            Assert.That("CNT+1:62'" ==  cps.ToString());
        }

        [Test]
        public void CNT_2()
        {
            var cps = new Segment("CNT")
                .AddComposite(2, 2);
            Assert.That("CNT+2:2'" == cps.ToString());
        }

        [Test]
        public void UNT()
        {
            var cps = new Segment("UNT")
                .AddElements(35, 564535);
            Assert.That("UNT+35+564535'" ==  cps.ToString());
        }

        [Test]
        public void UNZ()
        {
            var cps = new Segment("UNZ")
                .AddElement("1")
                .AddElement(964775);
            Assert.That("UNZ+1+964775'" == cps.ToString());
        }

        [Test]
        public void NAD()
        {
            var cps = new Segment("NAD")
                .AddElement("DEQ")
                .AddComposite("7300015200024", DataNull.Value, 9);
            Assert.That("NAD+DEQ+7300015200024::9'" == cps.ToString());
        }

      

    }
}
