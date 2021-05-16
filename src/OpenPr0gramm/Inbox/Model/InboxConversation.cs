using System;
using Newtonsoft.Json;
using OpenPr0gramm.Json;

namespace OpenPr0gramm.Inbox.Model
{
#if FW
    [Serializable]
#endif
    public class InboxConversation
    {
        public UserMark Mark { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "lastMessage")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastMessageAt { get; set; }

        public int UnreadCount { get; set; }

        public bool Blocked { get; set; }
    }
}
