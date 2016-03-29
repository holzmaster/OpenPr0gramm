using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class PrivateMessage
    {
        public int Id { get; set; }
        public bool Sent { get; set; }
        public string SenderName { get; set; }
        public int SenderMark { get; set; }
        public int SenderId { get; set; }
        public string RecipientName { get; set; }
        public int RecipientMark { get; set; }
        public int RecipientId { get; set; }
        [JsonProperty(PropertyName = "created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }

        public override string ToString() => $"{SenderName} ({SenderId}) -> {RecipientName} ({RecipientId}): {Message}";
    }
}
