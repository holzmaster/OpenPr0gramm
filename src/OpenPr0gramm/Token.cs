using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class Token
    {
        // TODO
        public TokenProduct Product { get; set; }
    }

#if FW
    [Serializable]
#endif
    public class TokenProduct
    {
        // TODO
        public int Days { get; set; }
    }
}
