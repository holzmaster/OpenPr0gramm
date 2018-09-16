using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class BanInfo
    {
        [JsonProperty(PropertyName = "banned")]
        public bool IsBanned { get; set; }
        [JsonProperty(PropertyName = "till")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Until { get; set; }
        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }
    }
}
