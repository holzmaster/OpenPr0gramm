using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class LogInData
    {
        [AliasAs("name")]
        public string Name { get; set; }
        [AliasAs("password")]
        public string Password { get; set; }
        [AliasAs("token")]
        public string Token { get; set; }
        [AliasAs("captcha")]
        public string Captcha { get; set; }
        public LogInData(string name, string password, string token, string captcha)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            Name = name;
            Password = password;
            Token = token;
            Captcha = captcha;
        }
    }
}
