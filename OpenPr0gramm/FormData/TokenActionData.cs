using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class TokenActionData : PostFormData
    {
        [AliasAs("token")]
        public string Token { get; }
        public TokenActionData(string nonce, string token)
            : base(nonce)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(token));
            Token = token;
        }
    }
}
