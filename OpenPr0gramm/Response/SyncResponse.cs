using System.Collections.Generic;

namespace OpenPr0gramm
{
    public class SyncResponse : Pr0grammResponse
    {
        public int InboxCount { get; set; }
        public IReadOnlyList<object> Log { get; set; } // TODO
        public int LastId { get; set; }
    }
}
