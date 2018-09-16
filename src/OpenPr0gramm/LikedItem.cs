using Newtonsoft.Json;
using OpenPr0gramm.Json;

namespace OpenPr0gramm
{
    public class LikedItem : IPr0grammItem
    {
        public int Id { get; set; }
        /// <summary> Use the BaseAddress property of your HttpClient to prepend the protocol and host name. </summary>
        [JsonProperty(PropertyName = "thumb")]
        public string ThumbnailUrl { get; set; }

        public override string ToString() => $"{Id}, {ThumbnailUrl}";
    }
}
