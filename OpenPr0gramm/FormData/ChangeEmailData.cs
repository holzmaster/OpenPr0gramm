using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class ChangeEmailData : PostFormData
    {
        [AliasAs("token")]
        public string Token { get; set; }
        public ChangeEmailData(string nonce, string token)
            : base(nonce)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(token));
            Token = token;
        }
    }
}
