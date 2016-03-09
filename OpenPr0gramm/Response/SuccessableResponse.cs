using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class SuccessableResponse : Pr0grammResponse
    {
        public bool Success { get; private set; }

        public SuccessableResponse()
        { }
        public SuccessableResponse(bool success)
        {
            Success = Success;
        }
    }
}
