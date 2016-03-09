using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class TokenInfoResponse : Pr0grammResponse
    {
        public Token Token { get; private set; }
    }
}
