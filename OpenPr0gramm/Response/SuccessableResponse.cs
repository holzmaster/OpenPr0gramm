using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class SuccessableResponse : Pr0grammResponse
    {
        public bool Success { get; set; }
    }
}
