using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public abstract class PostFormData
    {
        [AliasAs("_nonce")]
        public string Nonce { get; }
        protected PostFormData(string nonce)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(nonce));
            Debug.Assert(nonce.Length == 16);
            Nonce = nonce;
        }
    }
    public abstract class AnonymousFormData
    {
        // No Nonce
    }
}
