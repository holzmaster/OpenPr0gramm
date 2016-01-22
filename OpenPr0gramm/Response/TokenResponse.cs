using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class TokenResponse : Pr0grammResponse
    {
        public string TokenError { get; set; }
        public string Error { get; set; }
    }
}
