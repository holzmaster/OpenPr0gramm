using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class Tag : IPr0grammTag
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "tag")]
        public string Content { get; set; }
        public float Confidence { get; set; }

        public override string ToString() => $"{Content} ({Id}, {Confidence})";
    }

#if FW
    [Serializable]
#endif
    public class ItemTagDetails : Tag
    {
        public string User { get; set; }
        [JsonProperty(PropertyName = "up")]
        public int Upvotes { get; set; }
        [JsonProperty(PropertyName = "down")]
        public int Downvotes { get; set; }
        public IReadOnlyList<ItemTagVote> Votes { get; set; }
    }

    public interface IPr0grammTag
    {
        int Id { get; }
    }
}
