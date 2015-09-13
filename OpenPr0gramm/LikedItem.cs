using Newtonsoft.Json;
using OpenPr0gramm.Json;

namespace OpenPr0gramm
{
    public class LikedItem : IPr0grammItem
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "thumb")]
        [JsonConverter(typeof(ThumbnailUrlConverter))]
        public string ThumbnailUrl { get; set; }

        public override string ToString() => $"{Id}, {ThumbnailUrl}";
    }
}
