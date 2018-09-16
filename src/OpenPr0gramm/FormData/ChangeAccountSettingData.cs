using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class ChangeAccountSettingData : PostFormData
    {
        [AliasAs("currentPassword")]
        public string CurrentPassword { get; }
        public ChangeAccountSettingData(string nonce, string currentPassword)
            : base(nonce)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(currentPassword));
            CurrentPassword = currentPassword;
        }
    }
}
