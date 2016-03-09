using System;

namespace OpenPr0gramm
{
    [Serializable]
    public class LogInResponse : SuccessableResponse
    {
        public BanInfo Ban { get; private set; }
    }
}
