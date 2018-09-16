using System;

namespace OpenPr0gramm
{
#if FW
    [Serializable]
#endif
    public class LogInResponse : SuccessableResponse
    {
        public BanInfo Ban { get; private set; }
    }
}
