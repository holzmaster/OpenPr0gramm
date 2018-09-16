using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class RequestEmailChangeData : ChangeAccountSettingData
    {
        [AliasAs("email")]
        public string NewEmail { get; }

        public RequestEmailChangeData(string nonce, string currentPassword, string newEmail) : base(nonce, currentPassword)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(newEmail));
            NewEmail = newEmail;
        }
    }
}
