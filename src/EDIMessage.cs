using EDIFACT.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDIFACT
{
    public class EDIMessage : SegmentCollection
    {
        public string MessageReferenceNumber { get; set; }
        string MessageTypeIdentifier;
        string MessageTypeVersionNumber;
        string MessageTypeReleaseNumber;
        string ControllingAgency;
        string AssociationAssignedCode;
        public void SetMessageHeder(string MessageReferenceNumber,
            string MessageTypeIdentifier,
                string MessageTypeVersionNumber,
                string MessageTypeReleaseNumber,
                string ControllingAgency,
                string AssociationAssignedCode)
        {
            this.MessageReferenceNumber = MessageReferenceNumber;
            this.MessageTypeIdentifier = MessageTypeIdentifier;
            this.MessageTypeVersionNumber = MessageTypeVersionNumber;
            this.MessageTypeReleaseNumber = MessageTypeReleaseNumber;
            this.ControllingAgency = ControllingAgency;
            this.AssociationAssignedCode = AssociationAssignedCode;
        }

        /*
        public virtual IEnumerable<Segment> CreateMessage()
        {
            return CreateMessage(this);
        }*/

        /*
        protected virtual IEnumerable<Segment> CreateMessage(IEnumerable<Segment> segments)
        {
            if (string.IsNullOrWhiteSpace(MessageReferenceNumber)) throw new InvalidOperationException(nameof(MessageReferenceNumber));
            if (string.IsNullOrWhiteSpace(MessageTypeIdentifier)) throw new InvalidOperationException(nameof(MessageTypeIdentifier));
            if (string.IsNullOrWhiteSpace(MessageTypeReleaseNumber)) throw new InvalidOperationException(nameof(MessageTypeReleaseNumber));

            yield return GetUNH();
            foreach (Segment s in segments) yield return s;
            yield return GetUNT(segments);

        }*/

        public Segment GetUNH()
        {
            return Helpers.Interchange.GetUNH(MessageReferenceNumber,
                MessageTypeIdentifier,
                MessageTypeVersionNumber,
                MessageTypeReleaseNumber,
                ControllingAgency,
                AssociationAssignedCode);
        }


        internal virtual IEnumerable<Segment> FullMessageEnumerator()
        {
            foreach(Segment segment in this)
            {
                yield return segment;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetUNH().ToString());
            foreach (var str in FullMessageEnumerator())
                sb.Append(str);
            sb.Append(Interchange.GetUNT(this.Count() +2 , MessageReferenceNumber) .ToString());
            return sb.ToString();
        }

        //public EDIDocument CrateInterchange(InterchangeValues interchangeValues)
        //{
        //    EDIDocument doc = new EDIDocument();
        //    doc.SetInterchangeHeader(interchangeValues);
        //    doc.AddMessage(this);
        //    return doc;
        //}

        //public EDIDocument CrateInterchange(string SenderGLN,
        //    string RecipientGLN,
        //    DateTime PreparationTime,
        //    string InterchangeControlReference)
        //{            
            
        //    EDIDocument doc = new EDIDocument();
        //    doc.SetInterchangeHeader(SenderGLN, RecipientGLN, PreparationTime, InterchangeControlReference);
        //    doc.AddMessage(this);
        //    return doc;
        //}
    }
}
