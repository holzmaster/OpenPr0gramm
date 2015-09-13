using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class ItemTagVote
    {
        public string User { get; set; }
        public Vote Vote { get; set; }
    }
}
