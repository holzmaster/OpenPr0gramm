using Refit;

namespace OpenPr0gramm
{
    public class PaymentData : PostFormData
    {
        [AliasAs("email")]
        public string EmailAddress { get; }
        [AliasAs("tos")]
        public bool TermsAccepted { get; }
        [AliasAs("product")]
        public string Product { get; }

        public PaymentData(string nonce, string emailAddress, string product, bool termsAccepted)
            : base(nonce)
        {
            EmailAddress = emailAddress;
            Product = product;
            TermsAccepted = termsAccepted;
        }
    }
}
