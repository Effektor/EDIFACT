using System;
using System.Collections.Generic;

namespace EDIFACT.Validation.Schemas
{
    /// <summary>
    /// Represents the structural requirements for an EDIFACT message
    /// according to a specific directory (e.g., D96A).
    /// </summary>
    public sealed class EdifactMessageSchema
    {
        private readonly List<SegmentRequirement> _segments;

        public string MessageType { get; }
        public string Version { get; }
        public string Description { get; }
        public IReadOnlyList<SegmentRequirement> Segments => _segments;

        public EdifactMessageSchema(string messageType, string version, IEnumerable<SegmentRequirement> segments, string description = null)
        {
            if (string.IsNullOrWhiteSpace(messageType))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(messageType));
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(version));
            if (segments == null)
                throw new ArgumentNullException(nameof(segments));

            MessageType = messageType;
            Version = version;
            Description = description;
            _segments = new List<SegmentRequirement>(segments);
            if (_segments.Count == 0)
                throw new ArgumentException("Message schema requires at least one segment requirement.", nameof(segments));
        }
    }
}
