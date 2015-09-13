using Refit;

namespace OpenPr0gramm
{
    public class EditCommentData : PostFormData
    {
        [AliasAs("comment")]
        public string NewContent { get; }
        [AliasAs("commentId")]
        public int CommentId { get; }
        public EditCommentData(string nonce, int commentId, string newContent)
            : base(nonce)
        {
            CommentId = commentId;
            NewContent = newContent;
        }
    }
}
