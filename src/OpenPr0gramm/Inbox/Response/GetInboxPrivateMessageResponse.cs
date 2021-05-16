using System.Collections.Generic;
using OpenPr0gramm.Generic.Response;
using OpenPr0gramm.Inbox.Model;
using OpenPr0gramm.Model;

namespace OpenPr0gramm.Inbox.Response
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
