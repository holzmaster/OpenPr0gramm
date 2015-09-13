using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class SendPasswordResetMailData : AnonymousFormData
    {
        [AliasAs("email")]
        public string Email { get; }
        public SendPasswordResetMailData(string email)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(email));
            Email = email;
        }
    }
}
