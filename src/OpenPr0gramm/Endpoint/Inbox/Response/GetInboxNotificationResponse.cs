using System.Collections.Generic;
using OpenPr0gramm.Endpoint.Inbox.Model;
using OpenPr0gramm.Generic.Response;

namespace OpenPr0gramm.Endpoint.Inbox.Response
{
#if FW
    [Serializable]
#endif
    public class GetInboxNotificationResponse : Pr0grammResponse, IMessageResponse<InboxItem>
    {
        public IReadOnlyList<InboxItem> Messages { get; private set; }
    }
}
