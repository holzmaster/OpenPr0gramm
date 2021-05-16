namespace OpenPr0gramm.Inbox.Model
{
#if FW
    [Serializable]
#endif
    public enum InboxItemType
    {
        Message,
        Comment,
        Notification,
    }
}
