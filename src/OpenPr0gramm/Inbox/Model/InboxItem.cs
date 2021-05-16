using System;
using Newtonsoft.Json;
using OpenPr0gramm.Json;

namespace OpenPr0gramm.Inbox.Model
{
#if FW
    [Serializable]
#endif
    public class InboxItem
    {
        public InboxItemType Type { get; set; }

        public int Id { get; set; }

        public int ItemId { get; set; }

        [JsonConverter(typeof(ImageUrlConverter))]
        [JsonProperty(PropertyName = "image")]
        public string ImageUrl { get; set; }

        [JsonConverter(typeof(ThumbUrlConverter))]
        [JsonProperty(PropertyName = "thumb")]
        public string ThumbnailUrl { get; set; }

        public ItemFlags Flags { get; set; }

        public string Name { get; set; }

        public UserMark Mark { get; set; }

        public int SenderId { get; set; }

        public int Score { get; set; }

        public object Collection { get; set; } // TODO

        public string Owner { get; set; }

        public UserMark OwnerMark { get; set; }

        public string Keyword { get; set; }

        [JsonProperty(PropertyName = "created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        public string Message { get; set; }

        public bool Read { get; set; }

        public bool Blocked { get; set; }
    }
}
