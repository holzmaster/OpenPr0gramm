using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class ContactData : PostFormData
    {
        [AliasAs("email")]
        public string EmailAddress { get; }
        [AliasAs("subject")]
        public string Subject { get; }
        [AliasAs("message")]
        public string Message { get; }

        public ContactData(string nonce, string email, string subject, string message)
            : base(nonce)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(email));
            Debug.Assert(!string.IsNullOrWhiteSpace(subject));
            Debug.Assert(!string.IsNullOrWhiteSpace(message));
            EmailAddress = email;
            Subject = subject;
            Message = message;
        }
    }
}
