using System;
using System.IO;
using System.Linq;
using EDIFACT.ESAP20;
using EDIFACT.Validation;
using EDIFACT.Validation.Schemas;
using NUnit.Framework;

namespace EDIFACT.Tests.Validation
{
    public class SchemaValidationTests
    {
        private static string GetSchemaPath(string relative) =>
            Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "..", relative));

        private static EdifactMessageSchema LoadDesadvSchema()
        {
            var schemaPath = GetSchemaPath(Path.Combine("schemas", "D96A", "DESADV.json"));
            using var stream = File.OpenRead(schemaPath);
            return JsonMessageSchemaLoader.Load(stream);
        }

        private static DESADVMessage CreateValidDesadv()
        {
            var message = new DESADVMessage();
            message.SetMessageHeder("ME000001", "DESADV", "D", "96A", "UN", "EAN006");

            message.AddSegment("BGM").AddElements("351", "DES587441", "9");
            message.AddSegment("DTM").AddComposite("137", "20020401", "102");
            message.AddSegment("NAD").AddComposite("BY", "7300015200048", "", "9");
            message.AddSegment("LIN").AddElements("1", "EN", "12345");
            message.AddSegment("QTY").AddComposite("12", "50", "KGM");

            return message;
        }

        [Test]
        public void ValidDesadv_Message_PassesSchemaValidation()
        {
            var schema = LoadDesadvSchema();
            var message = CreateValidDesadv();
            var validator = new SchemaValidator();

            var report = validator.Validate(message, schema);

            Assert.That(report.IsValid, Is.True, "Expected DESADV message to satisfy schema requirements.");
            Assert.That(report.Issues, Is.Empty);
        }

        [Test]
        public void MissingMandatorySegment_IsReported()
        {
            var schema = LoadDesadvSchema();
            var message = CreateValidDesadv();

            // Remove the mandatory BGM segment by rebuilding a fresh message without it.
            var invalid = new DESADVMessage();
            invalid.SetMessageHeder("ME000002", "DESADV", "D", "96A", "UN", "EAN006");
            invalid.AddSegment("DTM").AddComposite("137", "20020401", "102");
            invalid.AddSegment("NAD").AddComposite("BY", "7300015200048", "", "9");
            invalid.AddSegment("LIN").AddElements("1", "EN", "12345");

            var validator = new SchemaValidator();
            var report = validator.Validate(invalid, schema);

            Assert.That(report.IsValid, Is.False);
            Assert.That(report.Issues.Any(i => i.Code == "SEGMENT_MISSING" && string.Equals(i.SegmentTag, "BGM", StringComparison.OrdinalIgnoreCase)),
                "Expected validation issues to include missing BGM segment.");
        }
    }
}
