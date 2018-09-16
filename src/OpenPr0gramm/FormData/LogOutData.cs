using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class LogOutData : PostFormData
    {
        [AliasAs("id")]
        public string SessionId { get; }
        public LogOutData(string nonce, string sessionId)
            : base(nonce)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(sessionId));
            Debug.Assert(sessionId.Length == 32);
            SessionId = sessionId;
        }
    }
}
