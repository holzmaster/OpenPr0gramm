using System.Collections.Generic;

namespace OpenPr0gramm
{
    public class SyncResponse : Pr0grammResponse
    {
        public int InboxCount { get; private set; }
        public IReadOnlyList<object> Log { get; private set; } // TODO
        public int LastId { get; private set; }
    }
}
