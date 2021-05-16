using System;
using Newtonsoft.Json;
using OpenPr0gramm.Json;

namespace OpenPr0gramm.Inbox.Model
{
#if FW
    [Serializable]
#endif
    public class InboxPrivateMessage
    {
        public int Id { get; set; }

        public bool Sent { get; set; }

        public int Read { get; set; }

        public string Name { get; set; }

        public UserMark Mark { get; set; }

        [JsonProperty(PropertyName = "created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        public string Message { get; set; }
    }
}
