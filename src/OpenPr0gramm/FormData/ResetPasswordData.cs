using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class ResetPasswordData : AnonymousFormData
    {
        [AliasAs("name")]
        public string Name { get; }
        [AliasAs("token")]
        public string Token { get; }
        [AliasAs("password")]
        public string NewPassword { get; }
        public ResetPasswordData(string token, string name, string newPassword)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(token));
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            Debug.Assert(!string.IsNullOrEmpty(newPassword));
            Debug.Assert(newPassword.Length >= 6);
            Token = token;
            Name = name;
            NewPassword = newPassword;
        }
    }
}
