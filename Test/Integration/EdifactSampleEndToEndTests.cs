using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EDIFACT;
using NUnit.Framework;

namespace EDIFACT.Tests.Integration
{
    public class EdifactSampleEndToEndTests
    {
        private static readonly string FixtureRoot = Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "Fixtures", "edifact");

        private static IEnumerable<TestCaseData> HappySamples()
        {
            var happyPath = Path.Combine(FixtureRoot, "happy");
            foreach (var file in Directory.EnumerateFiles(happyPath, "*.edi", SearchOption.AllDirectories))
            {
                yield return new TestCaseData(file).SetName($"HappySample_RoundTrip_{Path.GetFileNameWithoutExtension(file)}");
            }
        }

        private static IEnumerable<TestCaseData> SamplesMissingTerminators()
        {
            var folders = new[]
            {
                Path.Combine(FixtureRoot, "negative"),
                Path.Combine(FixtureRoot, "edge-cases"),
            };

            foreach (var folder in folders)
            {
                if (!Directory.Exists(folder))
                {
                    continue;
                }

                foreach (var file in Directory.EnumerateFiles(folder, "*.edi", SearchOption.AllDirectories))
                {
                    yield return new TestCaseData(file).SetName($"SampleMissingTerminator_{Path.GetFileNameWithoutExtension(file)}");
                }
            }
        }

        [TestCaseSource(nameof(HappySamples))]
        public void HappySamples_RoundTripUsingSegmentBuilders(string ediPath)
        {
            var originalSegments = ReadSegments(ediPath);
            var segmentObjects = originalSegments.Select(BuildSegment).ToList();

            var roundTrip = string.Concat(segmentObjects.Select(s => s.ToString()));
            var expected = string.Concat(originalSegments.Select(s => s + "'"));

            Assert.That(roundTrip, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(HappySamples))]
        public void HappySamples_ParsingProducesSegments(string ediPath)
        {
            var originalSegments = ReadSegments(ediPath);

            Assert.That(originalSegments, Is.Not.Empty);
            Assert.That(originalSegments[0], Does.StartWith("UNB").IgnoreCase);
        }

        [TestCaseSource(nameof(SamplesMissingTerminators))]
        public void SamplesWithoutSegmentTerminators_AreRejected(string ediPath)
        {
            var ex = Assert.Throws<InvalidOperationException>(() => ReadSegments(ediPath));
            Assert.That(ex?.Message, Does.Contain("terminator"));
        }

        private static IReadOnlyList<string> ReadSegments(string path)
        {
            var content = File.ReadAllText(path);
            var normalised = content.Replace("\r\n", "\n").Replace("\r", "\n").Trim();

            var rawSegments = normalised.Split('\'');
            if (rawSegments.Length <= 1)
            {
                throw new InvalidOperationException($"The sample '{Path.GetFileName(path)}' does not contain EDIFACT segment terminators (').");
            }

            var segments = new List<string>();
            foreach (var rawSegment in rawSegments)
            {
                var value = rawSegment.Trim();
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                segments.Add(value);
            }

            return segments;
        }

        private static Segment BuildSegment(string segmentText)
        {
            var parts = segmentText.Split('+');
            if (parts.Length == 0 || string.IsNullOrWhiteSpace(parts[0]))
            {
                throw new InvalidOperationException($"Segment '{segmentText}' is missing a tag.");
            }

            var segment = new Segment(parts[0]);
            for (var i = 1; i < parts.Length; i++)
            {
                segment.AddElement(parts[i]);
            }

            return segment;
        }
    }
}
