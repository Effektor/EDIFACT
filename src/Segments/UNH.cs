using System;
using System.Collections.Generic;
using System.Text;
using EDIFACT;

namespace EDIFACT
{
    /// <summary>
    /// UNH: Message Header
    /// 
    /// </summary>    
    /// 
    [EdiSegment("UNH")]
   public struct UNH : IEdifactSegment
    {
        /// <summary>
        /// Message Reference Number. <0062>
        /// </summary>
        [DataElement("0")]
        public string MessageReferenceNumber { get; private set; }

        /// <value>
        /// Message Type Identifier. SG: S009 E: 0065
        /// </value>
        [DataElement("1/0")]
        public string MessageTypeIdentifier { get; private set; }
        
        [DataElement("1/1")]
        public string MessageTypeVersionNumber { get; set; }
        [DataElement("1/2")]
        public string MessageTypeReleaseNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reff"></param>
        /// <param name="typeId"></param>
        public UNH(string reff, string typeId)
        {
            this.MessageReferenceNumber = reff;
            this.MessageTypeIdentifier = typeId;
            this.MessageTypeVersionNumber = "";
            this.MessageTypeReleaseNumber = "";
        }

        public override string ToString()
        {
            return $"UNH+{MessageReferenceNumber}+{MessageTypeIdentifier}+";
        }


    }
}
