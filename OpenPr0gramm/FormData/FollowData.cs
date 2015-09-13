using Refit;
using System.Diagnostics;

namespace OpenPr0gramm
{
    public class FollowData : PostFormData
    {
        [AliasAs("name")]
        public string Name { get; }
        public FollowData(string nonce, string name)
            : base(nonce)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            Name = name;
        }
    }
}
