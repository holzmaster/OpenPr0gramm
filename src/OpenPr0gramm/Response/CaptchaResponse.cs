using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class CaptchaResponse : Pr0grammResponse
    {
        public string Token { get; private set; }
        public string Captcha { get; private set; }
    }
}
