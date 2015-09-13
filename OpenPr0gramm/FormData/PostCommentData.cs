using Refit;

namespace OpenPr0gramm
{
    public class PostCommentData : PostFormData
    {
        public const int NoParentId = 0;
        [AliasAs("comment")]
        public string Content { get; }
        [AliasAs("parentId")]
        public int ParentId { get; }
        [AliasAs("itemId")]
        public int ItemId { get; }
        public PostCommentData(string nonce, int itemId, string content)
            : this(nonce, itemId, content, NoParentId)
        { }
        public PostCommentData(string nonce, int itemId, string content, int parentId)
            : base(nonce)
        {
            Content = content;
            ItemId = itemId;
            ParentId = parentId;
        }
    }
}
