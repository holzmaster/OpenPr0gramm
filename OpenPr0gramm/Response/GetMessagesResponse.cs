using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
    [Serializable]
    public class GetMessagesResponse<T> : Pr0grammResponse
    {
        public IReadOnlyList<T> Messages { get; set; }
        public bool HasOlder { get; set; }
        public bool HasNewer { get; set; }
    }
}
