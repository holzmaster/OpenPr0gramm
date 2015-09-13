using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class ProfileUpload : IPr0grammItem
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "thumb")]
        [JsonConverter(typeof(ThumbnailUrlConverter))]
        public string ThumbnailUrl { get; set; }

        public override string ToString() => $"{Id}, {ThumbnailUrl}";
    }
}
