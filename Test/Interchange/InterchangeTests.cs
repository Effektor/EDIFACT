using System;
using System.IO;
using System.Linq;
using EDIFACT;
using EDIFACT.ESAP20;
using EDIFACT.Validation;
using EDIFACT.Validation.Schemas;
using NUnit.Framework;

namespace EDIFACT.Tests.Interchange
{
    public class InterchangeTests
    {
        [Test]
        public void InterchangeHeader_UsesDefaultServiceStringAdvice()
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

            Assert.That(header, Is.EqualTo("UNA:+.? '"));
        }

        [Test]
        public void DesadvMessage_ConstructedWithBuilders_PassesSchemaValidation()
        {
            var schemaPath = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "..", "schemas", "D96A", "DESADV.json"));
            using var stream = File.OpenRead(schemaPath);

            var schema = JsonMessageSchemaLoader.Load(stream);
            var message = new DESADVMessage();
            message.SetMessageHeder("ME000010", "DESADV", "D", "96A", "UN", "EAN006");
            message.AddSegment("BGM").AddElements("351", "DES123456", "9");
            message.AddSegment("DTM").AddComposite("137", "20240401", "102");
            message.AddSegment("NAD").AddComposite("BY", "7300015200048", string.Empty, "9");
            message.AddSegment("LIN").AddElements("1", "EN", "12345");

            var validator = new SchemaValidator();
            var report = validator.Validate(message, schema);

            Assert.That(report.IsValid, Is.True, () => string.Join(Environment.NewLine, report.Issues.Select(i => $"{i.Severity}:{i.Code}:{i.Message}")));
        }
    }
}
