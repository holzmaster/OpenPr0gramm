using Refit;

namespace OpenPr0gramm
{
    public class DeleteItemData : PostFormData
    {
        [AliasAs("itemId")]
        public int ItemId { get; }
        [AliasAs("days")]
        public int Days { get; }
        [AliasAs("banUser")]
        public bool BanUser { get; }
        [AliasAs("notifyUser")]
        public bool NotifyUser { get; }
        [AliasAs("reason")]
        public string Reason { get; }
        [AliasAs("customReason")]
        public string CustomReason { get; }

        public DeleteItemData(string nonce, int itemId, string reason, string customReason, bool notifyUser, bool banUser, int days)
            : base(nonce)
        {
            ItemId = itemId;
            Reason = reason;
            CustomReason = customReason;
            NotifyUser = notifyUser;
            BanUser = banUser;
            Days = days;
        }
    }
}
