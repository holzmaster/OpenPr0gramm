using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
    [Serializable]
    public class GetItemsResponse : Pr0grammResponse
    {
        public bool AtEnd { get; set; }
        public bool AtStart { get; set; }
        public object Error { get; set; }
        public IReadOnlyList<Item> Items { get; set; }
    }
}
