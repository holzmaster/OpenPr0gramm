using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class TokenResponse : Pr0grammResponse
    {
        public string TokenError { get; set; }
        public string Error { get; set; }
    }
}
