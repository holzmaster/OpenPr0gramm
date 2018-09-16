using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class InviteData : PostFormData
    {
        [AliasAs("email")]
        public string Email { get; }
        public InviteData(string nonce, string email)
            : base(nonce)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(email));
            Email = email;
        }
    }
}
