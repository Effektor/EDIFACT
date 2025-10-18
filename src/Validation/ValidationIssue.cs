using System;

namespace EDIFACT.Validation
{
    public sealed class ValidationIssue
    {
        public ValidationSeverity Severity { get; }
        public string Code { get; }
        public string Message { get; }
        public string SegmentTag { get; }
        public int? SegmentIndex { get; }
        public string Path { get; }

        public ValidationIssue(ValidationSeverity severity, string code, string message, string segmentTag = null, int? segmentIndex = null, string path = null)
        {
            Severity = severity;
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            SegmentTag = segmentTag;
            SegmentIndex = segmentIndex;
            Path = path;
        }
    }
}
