
using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class GetDetailsResponse : Pr0grammResponse
    {
        public IReadOnlyList<ItemTagDetails> Tags { get; private set; }
    }
}
