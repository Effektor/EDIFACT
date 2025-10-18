using System;
using System.Collections.Generic;
using System.Linq;
using EDIFACT.Validation.Schemas;

namespace EDIFACT.Validation
{
    /// <summary>
    /// Validates messages built with the existing segment builders against a directory schema.
    /// </summary>
    public sealed class SchemaValidator
    {
        public ValidationReport Validate(EDIMessage message, EdifactMessageSchema schema)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));

            var report = new ValidationReport();
            var segments = message.FullMessageEnumerator().ToList();

            var segmentPositions = BuildPositionLookup(segments);
            var lastSeenIndex = -1;

            foreach (var requirement in schema.Segments)
            {
                segmentPositions.TryGetValue(requirement.Tag, out var positions);
                var count = positions?.Count ?? 0;

                if (count < requirement.MinOccurs)
                {
                    report.AddIssue(new ValidationIssue(
                        ValidationSeverity.Error,
                        "SEGMENT_MISSING",
                        $"Segment {requirement.Tag} must appear at least {requirement.MinOccurs} time(s).",
                        requirement.Tag));
                }

                if (requirement.MaxOccurs != -1 && count > requirement.MaxOccurs)
                {
                    report.AddIssue(new ValidationIssue(
                        ValidationSeverity.Error,
                        "SEGMENT_TOO_MANY",
                        $"Segment {requirement.Tag} appears {count} time(s) but must not exceed {requirement.MaxOccurs}.",
                        requirement.Tag));
                }

                if (positions != null && positions.Count > 0)
                {
                    var firstIndex = positions[0];
                    if (firstIndex < lastSeenIndex)
                    {
                        report.AddIssue(new ValidationIssue(
                            ValidationSeverity.Error,
                            "SEGMENT_ORDER",
                            $"Segment {requirement.Tag} appears out of order.",
                            requirement.Tag,
                            firstIndex));
                    }

                    var lastIndex = positions[positions.Count - 1];
                    lastSeenIndex = Math.Max(lastSeenIndex, lastIndex);
                }
            }

            return report;
        }

        private static Dictionary<string, List<int>> BuildPositionLookup(IReadOnlyList<Segment> segments)
        {
            var map = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < segments.Count; i++)
            {
                var tag = segments[i]?.Tag ?? string.Empty;
                if (!map.TryGetValue(tag, out var list))
                {
                    list = new List<int>();
                    map[tag] = list;
                }

                list.Add(i);
            }

            return map;
        }
    }
}
