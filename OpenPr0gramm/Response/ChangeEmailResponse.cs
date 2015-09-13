
using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class ChangeUserDataResponse : Pr0grammResponse
    {
        public string Account { get; set; }
        public string Error { get; set; }
    }
}
