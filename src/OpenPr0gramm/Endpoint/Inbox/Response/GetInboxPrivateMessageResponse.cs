using System.Collections.Generic;
using OpenPr0gramm.Endpoint.Inbox.Model;
using OpenPr0gramm.Generic.Response;

namespace OpenPr0gramm.Endpoint.Inbox.Response
{
#if FW
    [Serializable]
#endif
    public class GetInboxPrivateMessageResponse : Pr0grammResponse, IHasEndResponse, IMessageResponse<InboxPrivateMessage>
    {
        public IReadOnlyList<InboxPrivateMessage> Messages { get; set; }
        public bool AtEnd { get; set; }
    }
}
