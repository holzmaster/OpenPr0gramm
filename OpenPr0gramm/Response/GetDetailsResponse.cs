
using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
    [Serializable]
    public class GetDetailsResponse : Pr0grammResponse
    {
        public IReadOnlyList<ItemTagDetails> Tags { get; set; }
    }
}
