# Examples

## Create A Standalone Segment
The `Segment` type allows you to compose EDIFACT segments fluently and render them with the correct terminator.

```csharp
using EDIFACT;

var dtm = new Segment("DTM")
    .AddComposite("137", "20240401", "102");

Console.WriteLine(dtm.ToString());
// DTM+137:20240401:102'
```

`AddElement` appends single values, while `AddComposite` joins values with the component separator (`:`). Trailing empty composite components are trimmed automatically.

## Build A DESADV Message
`DESADVMessage` extends `EDIMessage` and emits the surrounding UNH/UNT segments as well as control counters. You can populate it with the segments that belong to the delivery advice payload.

```csharp
using EDIFACT.ESAP20;

var desadv = new DESADVMessage();
desadv.SetMessageHeder("ME000001", "DESADV", "D", "96A", "UN", "EAN006");

desadv.AddSegment("BGM").AddElements("351", "DES587441", "9");
desadv.AddSegment("DTM").AddComposite("137", "20020401", "102");
desadv.AddSegment("NAD").AddComposite("BY", "7300015200048", "", "9");

string messageText = desadv.ToString();
// Starts with UNH+ME000001+DESADV:D:96A:UN:EAN006' ...
```

Every call to `AddSegment` returns the created segment, so you can chain `AddElement`/`AddComposite` calls before adding the next segment. The `ToString` output already includes the UNH header, message body, control totals, and the final UNT.

## Generate An Interchange
Wrap one or more messages in `EDIDocument` to produce a full interchange with UNA/UNB and UNZ segments. Populate `InterchangeValues` with the fields required by the current helpers.

```csharp
using System;
using EDIFACT;
using EDIFACT.Helpers;
using EDIFACT.ESAP20;

var desadv = new DESADVMessage();
desadv.SetMessageHeder("ME000001", "DESADV", "D", "96A", "UN", "EAN006");
desadv.AddSegment("BGM").AddElements("351", "DES587441", "9");
desadv.AddSegment("DTM").AddComposite("137", "20020401", "102");

var interchange = new EDIDocument();
interchange.SetInterchangeHeader(new InterchangeValues
{
    SenderGLN = "7300015200048",
    SenderIdentificationQualifier = "14",
    RecipientGLN = "7350000001266",
    RecipientIdentificationQualifier = "14",
    PreparationTime = new DateTime(2018, 04, 01, 09, 53, 00, DateTimeKind.Utc),
    InterchangeControlReference = "964775",
    SyntaxIdentifier = "UNOC",
    SyntaxVersionNumber = 3,
    ApplicationReference = "DESADV",
    TestIndicator = 0
});

interchange.AddMessage(desadv);

string ediText = interchange.ToString();
// Begins with UNA:+.?'
// Followed by UNB, the DESADV message, and UNZ.
```

`EDIDocument` uses `Interchange.DefaultServiceStringAdvice` automatically when `ServiceStringAdvice` is not set. The generated string can be persisted as-is or written to an EDIFACT file for exchange with partners.
