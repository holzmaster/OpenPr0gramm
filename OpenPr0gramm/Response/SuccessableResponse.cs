using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class SuccessableResponse : Pr0grammResponse
    {
        public bool Success { get; set; }
    }
}
