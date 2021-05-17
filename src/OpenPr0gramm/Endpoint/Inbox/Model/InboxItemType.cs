namespace OpenPr0gramm.Endpoint.Inbox.Model
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
