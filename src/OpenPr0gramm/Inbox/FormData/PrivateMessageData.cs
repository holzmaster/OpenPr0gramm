using Refit;

namespace OpenPr0gramm.Inbox.FormData
{
    public class PrivateMessageData : PostFormData
    {
        [AliasAs("recipientId")]
        public int RecipientId { get; }
        [AliasAs("comment")]
        public string Content { get; }

        public PrivateMessageData(string nonce, int recipientId, string message)
            : base(nonce)
        {
            RecipientId = recipientId;
            Content = message;
        }
    }
}
