using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class AccountInfo
    {
        public bool LikesArePublic { get; set; }
        public string Email { get; set; }
        [JsonProperty(PropertyName = "invites")]
        public int InviteCount { get; set; }
        public UserMark Mark { get; set; }
        public UserMark MarkDefault { get; set; }
        [JsonProperty(PropertyName = "paidUntil")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime PaidUntil { get; set; }

        [JsonIgnore]
        public bool IsPaidMembership => PaidUntil > DateTime.Now;
    }
}
