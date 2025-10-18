using EDIFACT;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Edifact_Test
{
    public class InterchangeTests
    {
        /*
        [Test]
        public void EDIInterchangeFooter()
        {
            var footer = EDIFACT.Helpers.Interchange.GetInterchangeFooter(62, 2, 35, "564535", 1, "964775")
                .Select(x => x.ToString()).ToList();

            Assert.That("CNT+1:62'", footer[0]);
            Assert.That("CNT+2:2'", footer[1]);
            Assert.That("UNT+35+564535'", footer[2]);
            Assert.That("UNZ+1+964775'", footer[3]);
        }*/

        [Test]
        public void EDIInterchangeHeader()
        {
            var doc = new EDIDocument();
            doc.SetInterchangeHeader(new EDIFACT.Helpers.InterchangeValues
            {                
                SenderGLN = "964775",
                SenderIdentificationQualifier = "14",
                RecipientGLN = "964775",
                RecipientIdentificationQualifier = "14",
                MessageTypeReleaseNumber = "2",
                ControllingAgency = "UN",
                AssociationAssignedCode = "964775",
                InterchangeControlReference = "964775"
            });


            var header = doc.CreateInterchange()[0].ToString();
            
            Assert.That("UNA:+.? '" == header, header);
        }
    }
}
