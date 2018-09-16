using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class GetItemsInfoResponse : Pr0grammResponse
    {
        public IReadOnlyList<Tag> Tags { get; private set; }
        public IReadOnlyList<ItemComment> Comments { get; private set; }
    }
}
