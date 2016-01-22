using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class GetItemsResponse : Pr0grammResponse
    {
        public bool AtEnd { get; set; }
        public bool AtStart { get; set; }
        public object Error { get; set; }
        public IReadOnlyList<Item> Items { get; set; }
    }
}
