using EDIFACT.Helpers;
using EDIFACT.Segments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDIFACT
{
    public class EDIDocument
    {
        public Segment ServiceStringAdvice { get; set; } //UNA

        private Segment UNB;
        protected Segment UNH;
        protected List<EDIMessage> Messages;
        protected Segment UNT;
        private Segment UNZ;
        private InterchangeValues interchangeHeader;

        public EDIDocument()
        {
            this.Messages = new List<EDIMessage>();
        }

        public void SetInterchangeHeader(InterchangeValues interchangeHeader)
        {
            this.interchangeHeader = interchangeHeader;            
        }

        public void SetInterchangeHeader(string SenderGLN,
            string RecipientGLN,
            DateTime PreparationTime,
            string InterchangeControlReference
            )
        {
            this.interchangeHeader = new InterchangeValues
            {
                SenderGLN = SenderGLN,
                RecipientGLN = RecipientGLN,
                PreparationTime = PreparationTime,
                InterchangeControlReference = InterchangeControlReference,
            };
            
        }

        public void AddMessage(EDIMessage message)
        {
#if DEBUG
            if (!message.GetType().IsSubclassOf(typeof(EDIMessage)))
                throw new InvalidOperationException("tttt");
#endif
            this.Messages.Add(message);
        }


        // EDIDocument should probably be Interchange-agnostic. We don't know what level were working with here.
        public SegmentCollection CreateInterchange()
        {
            SegmentCollection interchange = new SegmentCollection();
            interchange.Add(ServiceStringAdvice ?? Helpers.Interchange.DefaultServiceStringAdvice);

            
            Segment unb = EDIFACT.Helpers.Interchange.GetUNB(interchangeHeader);
            interchange.Add(unb);

            foreach(var message in Messages)
            {
                interchange.AddSegments(message.FullMessageEnumerator());
            }

            int InterchangeControlCount = 1;

            var sg = Helpers.Interchange.GetInterchangeFooter(
                InterchangeControlCount,
                this.interchangeHeader.InterchangeControlReference);
            interchange.AddSegments(sg);
            return interchange;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var seg in CreateInterchange()) sb.Append(seg.ToString());
            return sb.ToString();
        }
    }



}
