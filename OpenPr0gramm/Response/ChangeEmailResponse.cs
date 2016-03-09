
using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class ChangeUserDataResponse : Pr0grammResponse
    {
        public string Account { get; private set; }
        public string Error { get; private set; }
    }
}
