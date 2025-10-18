using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT.Helpers
{
    public class InterchangeValues
    {
        public string SenderGLN { get; set; }
        public string RecipientGLN { get; set; }
        public DateTime PreparationTime { get; set; }
        public string InterchangeControlReference { get; set; }
        public bool AcknowledgementRequest { get; set; }
        public string MessageReferenceNumber { get; set; }
        public string MessageTypeIdentifier { get; set; }
        public string MessageTypeVersionNumber { get; set; }
        public string MessageTypeReleaseNumber { get; set; }
        public string ControllingAgency { get; set; }
        public string AssociationAssignedCode { get; set; }
        public string ApplicationReference { get; set; }
        public int? TestIndicator { get; set; }
        public string RecipientReference { get; set; }
        public string ProcessingPriorityCode { get; set; }
        public string CommunicationsAgreementID { get; set; }
        public string RecipientIdentificationQualifier { get; set; }
        public string SenderIdentificationQualifier { get; set; }
        public string SyntaxIdentifier { get;  set; }
        public int SyntaxVersionNumber { get; set; }

        public Segment GetUNA()
        {

            Segment una = EDIFACT.Helpers.Interchange.GetUNA(":", "+", ".", "?", " ", "'");
            return una;
        }
        public Segment GetUNB()
        {
            Segment unb = EDIFACT.Helpers.Interchange.GetUNB("UNOC", 3, SenderGLN, "14",
                RecipientGLN, "14", PreparationTime,
                InterchangeControlReference,
                "",
                ApplicationReference,
                "",
                AcknowledgementRequest ? 1 : (Nullable<int>)null,
                "",
                TestIndicator);
            return unb;
        }
        public Segment GetUNH()
        {
            Segment unh = EDIFACT.Helpers.Interchange.GetUNH(
                MessageReferenceNumber,
                MessageTypeIdentifier,
                MessageTypeVersionNumber,
                MessageTypeReleaseNumber,
                ControllingAgency,
                AssociationAssignedCode);

            return unh;
        }
    }
}
