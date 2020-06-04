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

        public byte[] GetCaptchaImageBytes()
        {
            return Convert.FromBase64String(Captcha.Substring(Captcha.IndexOf(',') + 1));
        }
    }
}
