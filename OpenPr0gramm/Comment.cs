using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;

namespace OpenPr0gramm
{
    // TODO: Check this
    [Serializable]
    public abstract class Comment : IPr0grammComment
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "up")]
        public int Upvotes { get; set; }
        [JsonProperty(PropertyName = "down")]
        public int Downvotes { get; set; }
        [JsonProperty(PropertyName = "created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public override string ToString() => $"{Id}, {Upvotes}/{Downvotes}, {Content}";
    }

    [Serializable]
    public class ProfileComment : Comment
    {
        public int ItemId { get; set; }
        [JsonProperty(PropertyName = "thumb")]
        [JsonConverter(typeof(ThumbnailUrlConverter))]
        public string ThumbnailUrl { get; set; }
        public override string ToString() => "Profile: " + base.ToString();
    }

    [Serializable]
    public class ItemComment : Comment
    {
        [JsonProperty(PropertyName = "parent")]
        public int ParentId { get; set; }
        public float Confidence { get; set; }
        public string Name { get; set; }
        public UserMark Mark { get; set; }
        public override string ToString() => "Item: " + base.ToString();
    }

    public interface IPr0grammComment
    {
        int Id { get; }
    }
}
