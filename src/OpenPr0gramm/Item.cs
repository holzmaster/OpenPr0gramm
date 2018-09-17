using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;
using System.Diagnostics;

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
        /// <summary> Use the BaseAddress property of your HttpClient to prepend the protocol and host name. </summary>
        [JsonProperty(PropertyName = "image")]
        public string ImageUrl { get; set; }
        /// <summary> Use the BaseAddress property of your HttpClient to prepend the protocol and host name. </summary>
        [JsonProperty(PropertyName = "thumb")]
        public string ThumbnailUrl { get; set; }
        /// <summary> Use the BaseAddress property of your HttpClient to prepend the protocol and host name. </summary>
        [JsonProperty(PropertyName = "fullsize")]
        public string FullSizeUrl { get; set; }
        public string Source { get; set; }
        public ItemFlags Flags { get; set; }
        public string User { get; set; }
        public UserMark Mark { get; set; }

        public ItemType GetItemType()
        {
            var url = ImageUrl;
            if (string.IsNullOrWhiteSpace(url))
                return ItemType.Unknown;
            return url.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) ? ItemType.Video : ItemType.Image;
        }

        public string GetAbsoluteThumbnailUrl(bool secure) => ClientConstants.GetThumbnailUrlPrefix(secure) + "/" + ThumbnailUrl;
        public string GetAbsoluteFullSizeUrl(bool secure) => FullSizeUrl == null ? GetAbsoluteImageUrl(secure) : ClientConstants.GetFullSizeUrlPrefix(secure) + "/" + FullSizeUrl;
        public string GetAbsoluteImageUrl(bool secure) => ClientConstants.GetImageUrlPrefix(secure) + "/" + ImageUrl;
    }

    public enum ItemType
    {
        Unknown, // TODO consider
        Image,
        Video
    }

    public interface IPr0grammItem
    {
        int Id { get; }
    }
}
