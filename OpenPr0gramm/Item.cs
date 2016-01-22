using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class Item : IPr0grammItem
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "promoted")]
        public int PromotedId { get; set; }
        [JsonProperty(PropertyName = "up")]
        public int Upvotes { get; set; }
        [JsonProperty(PropertyName = "down")]
        public int Downvotes { get; set; }
        [JsonProperty(PropertyName = "created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "image")]
        [JsonConverter(typeof(ImageUrlConverter))]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "thumb")]
        [JsonConverter(typeof(ThumbnailUrlConverter))]
        public string ThumbnailUrl { get; set; }
        [JsonProperty(PropertyName = "fullsize")]
        [JsonConverter(typeof(FullUrlConverter))]
        public string FullSizeUrl { get; set; }
        public string Source { get; set; }
        public ItemFlags Flags { get; set; }
        public string User { get; set; }
        public UserMark Mark { get; set; }
    }

    public interface IPr0grammItem
    {
        int Id { get; }
    }
}
