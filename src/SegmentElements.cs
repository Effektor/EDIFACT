using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT
{
    [EdiSegment("BGM")]
    public class BGM : IEdifactSegment
    {
        [DataElement("BGM/0")]
        public string DocumentNameCode { get; set; }

        [DataElement("BGM/1")]
        public string DocumentNumber { get; set; }
        
        [DataElement("BGM/2")]
        public string MessageFunction { get; set; }

        public static implicit operator BGM(string s)
        {
            return new BGM()
            {
                DocumentNumber = s
            };
        }
    }


    [EdiSegment("RFF")]
    public class RFF
    {
        [DataElement("RFF/0")]
        public string ReferenceCode { get; set; }

        [DataElement("RFF/0/1")]
        public string ReferenceIdentifier { get; set; }

        public static implicit operator RFF(string s)
        {
            return new RFF()
            {
                ReferenceIdentifier = s
            };
        }
    }

    public class NAD
    {
        [DataElement("NAD/0")]
        public string PartyQualifier { get; set; }
        [DataElement("NAD/1/0")]
        public string PartyIdentification { get; set; }

        [DataElement("NAD/1/1")]
        public string CodeListQualifier { get; set; }

        [DataElement( "NAD/1/2")]
        public string AgencyCode { get; set; }

        public static implicit operator NAD(string s)
        {
            return new NAD()
            {
                PartyIdentification = s
            };
        }
    }

    [EdiSegment("TDT")]
    public class TDT
    {
        [DataElement("TDT/0")]
        public string TransportStageQualifier { get; set; }
        
        /*[DataElement("TDT/1")]
        public string ConveyanceReferenceNumber { get; set; }*/

        [DataElement("TDT/2")]
        public string ModeOfTransportation { get; set; }
    }

    [EdiSegment("CPS")]
    public class CPS
    {
        [DataElement("CPS/0")]
        public string SequenceNumber { get; set; }

        public static implicit operator CPS(string s)
        {
            return new CPS
            {
                SequenceNumber = s
            };
        }
    }

    [EdiSegment("PAC")]
    public class PAC
    {
        [DataElement("PAC/0/0")]
        public string NumberOfPackages { get; set; }

        [DataElement("PAC/2/0")]
        public string PackageTypeIdentification { get; set; }
    }

    [EdiSegment("GIN")]
    public class GIN
    {
        [DataElement("GIN/0")]
        public string Qualifier { get; set; }
        
        [DataElement("GIN/1")]
        public string IdentityNumber { get; set; }
    }

    [EdiSegment("MEA")]
    public class MEA
    {
        [DataElement("MEA/0")]
        public string Qualifier { get; set; }

        [DataElement("MEA/1/0")]
        public string DimensionCode { get; set; }

        [DataElement("MEA/2/0")]
        public string UnitQualifier { get; set; }

        [DataElement("MEA/2/1")]
        public string Value { get; set; }

    }
}
