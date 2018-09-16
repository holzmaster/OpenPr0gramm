using Refit;
using System.Collections.Generic;

namespace OpenPr0gramm
{
    public class DeleteTagsData : PostFormData
    {
        [AliasAs("tags[]")]
        public IReadOnlyList<int> TagIds { get; }
        [AliasAs("banUsers")]
        public bool BanUsers { get; }
        [AliasAs("itemId")]
        public int ItemId { get; }
        [AliasAs("days")]
        public int Days { get; }
        public DeleteTagsData(string nonce, int itemId, bool banUsers, int[] tagIds)
        : this(nonce, itemId, banUsers, 1, tagIds)
        { }
        public DeleteTagsData(string nonce, int itemId, bool banUsers, int days, int[] tagIds)
        : base(nonce)
        {
            ItemId = itemId;
            BanUsers = banUsers;
            Days = days;
            TagIds = tagIds;
        }
    }
}
