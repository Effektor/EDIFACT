using System.Collections.Generic;
using System.Linq;

namespace EDIFACT.Validation
{
    public sealed class ValidationReport
    {
        private readonly List<ValidationIssue> _issues = new List<ValidationIssue>();

        public IReadOnlyList<ValidationIssue> Issues => _issues.AsReadOnly();

        public bool IsValid => _issues.All(i => i.Severity != ValidationSeverity.Error);

        public void AddIssue(ValidationIssue issue)
        {
            _issues.Add(issue);
        }
    }
}
