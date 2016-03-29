using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class InboxItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        /// <summary> Use the BaseAddress property of your HttpClient to prepend the protocol and host name. </summary>
        [JsonProperty(PropertyName = "thumb")]
        public string ThumbnailUrl { get; set; }
        public string Name { get; set; }
        public UserMark Mark { get; set; }
        public int SenderId { get; set; }
        public int Score { get; set; }
        [JsonProperty(PropertyName = "created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }
    }
}
