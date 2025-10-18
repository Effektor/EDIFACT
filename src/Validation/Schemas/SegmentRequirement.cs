using System;

namespace EDIFACT.Validation.Schemas
{
    /// <summary>
    /// Defines minimum and maximum occurrences for a segment tag within a message schema.
    /// </summary>
    public sealed class SegmentRequirement
    {
        public string Tag { get; }
        public int MinOccurs { get; }
        public int MaxOccurs { get; }

        public SegmentRequirement(string tag, int minOccurs, int maxOccurs)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentException("Segment tag cannot be null or whitespace.", nameof(tag));
            if (minOccurs < 0)
                throw new ArgumentOutOfRangeException(nameof(minOccurs), "Minimum occurrences cannot be negative.");
            if (maxOccurs != -1 && maxOccurs < minOccurs)
                throw new ArgumentOutOfRangeException(nameof(maxOccurs), "Maximum occurrences must be greater than or equal to minimum occurrences, or -1 for unbounded.");

            Tag = tag;
            MinOccurs = minOccurs;
            MaxOccurs = maxOccurs;
        }
    }
}
