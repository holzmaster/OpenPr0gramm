using OpenPr0gramm.Inbox.Model;
using OpenPr0gramm.Model;

namespace OpenPr0gramm.Inbox.Response
{
#if FW
    [Serializable]
#endif
    public class GetInboxNotificationResponse : GetGenericInboxMessagesResponse<InboxItem>
    {
    }
}
