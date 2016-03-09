using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
    [Serializable]
    public class GetItemsResponse : Pr0grammResponse
    {
        public bool AtEnd { get; private set; }
        public bool AtStart { get; private set; }
        public object Error { get; private set; }
        public IReadOnlyList<Item> Items { get; private set; }
    }
}
