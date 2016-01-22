using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class TokenInfoResponse : Pr0grammResponse
    {
        public Token Token { get; set; }
    }
}
