using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class JoinWithTokenData : AnonymousFormData
    {
        [AliasAs("token")]
        public string Token { get; }
        [AliasAs("password")]
        public string Password { get; }
        [AliasAs("email")]
        public string Email { get; }
        [AliasAs("name")]
        public string Name { get; }

        public JoinWithTokenData(string email, string name, string password, string token)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(token));
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            Debug.Assert(name.Length >= 2);
            Debug.Assert(!string.IsNullOrWhiteSpace(email));
            Debug.Assert(!string.IsNullOrEmpty(password));
            Debug.Assert(password.Length >= 6);
            Token = token;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
