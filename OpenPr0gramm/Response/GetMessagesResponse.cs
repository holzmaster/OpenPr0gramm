using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
    [Serializable]
    public class GetMessagesResponse<T> : Pr0grammResponse
    {
        public IReadOnlyList<T> Messages { get; private set; }
        public bool HasOlder { get; private set; }
        public bool HasNewer { get; private set; }
    }
}
