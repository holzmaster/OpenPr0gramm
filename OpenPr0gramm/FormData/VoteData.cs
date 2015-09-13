using Refit;

namespace OpenPr0gramm
{
    public class VoteData : PostFormData
    {
        [AliasAs("id")]
        public int Id { get; }
        [AliasAs("vote")]
        public int AbsoluteVote { get; }
        public VoteData(string nonce, int id, int absoluteVote)
            : base(nonce)
        {
            Id = id;
            AbsoluteVote = absoluteVote;
        }
    }
}
