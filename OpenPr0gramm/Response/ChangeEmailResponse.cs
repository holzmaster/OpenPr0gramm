
using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class ChangeUserDataResponse : Pr0grammResponse
    {
        public string Account { get; set; }
        public string Error { get; set; }
    }
}
