using OpenPr0gramm.Inbox.Model;

namespace OpenPr0gramm.Inbox.Response
{
#if FW
    [Serializable]
#endif
    public class GetInboxCommentResponse : GetGenericInboxMessagesResponse<InboxItem>
    {

    }
}
