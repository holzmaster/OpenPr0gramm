using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class ItemTagVote
    {
        public string User { get; set; }
        public Vote Vote { get; set; }
    }
}
