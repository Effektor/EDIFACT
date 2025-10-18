using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EDIFACT.Validation.Schemas
{
    /// <summary>
    /// Loads <see cref="EdifactMessageSchema"/> instances from JSON documents.
    /// </summary>
    public static class JsonMessageSchemaLoader
    {
        public static EdifactMessageSchema Load(Stream jsonStream)
        {
            if (jsonStream == null)
                throw new ArgumentNullException(nameof(jsonStream));

            string json;
            using (var reader = new StreamReader(jsonStream))
            {
                json = reader.ReadToEnd();
            }
            var model = JsonConvert.DeserializeObject<MessageSchemaModel>(json, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include
            });
            if (model == null)
                throw new InvalidDataException("Schema JSON did not produce a valid schema model.");

            var segmentRequirements = new List<SegmentRequirement>();
            if (model.Segments != null)
            {
                foreach (var seg in model.Segments)
                {
                    segmentRequirements.Add(new SegmentRequirement(
                        seg.Tag ?? throw new InvalidDataException("Segment tag is required."),
                        seg.Min,
                        seg.Max));
                }
            }

            return new EdifactMessageSchema(
                model.MessageType ?? throw new InvalidDataException("messageType is required."),
                model.Version ?? throw new InvalidDataException("version is required."),
                segmentRequirements,
                model.Description);
        }

        private sealed class MessageSchemaModel
        {
            [JsonProperty("messageType")]
            public string MessageType { get; set; }

            [JsonProperty("version")]
            public string Version { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("segments")]
            public List<SegmentModel> Segments { get; set; }
        }

        private sealed class SegmentModel
        {
            [JsonProperty("tag")]
            public string Tag { get; set; }

            [JsonProperty("min")]
            public int Min { get; set; }

            [JsonProperty("max")]
            public int Max { get; set; }
        }
    }
}
