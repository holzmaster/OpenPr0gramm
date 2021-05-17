using System.Collections.Generic;
using OpenPr0gramm.Endpoint.Inbox.Model;
using OpenPr0gramm.Generic.Response;

namespace OpenPr0gramm.Endpoint.Inbox.Response
{
#if FW
    [Serializable]
#endif
    public class GetInboxConversationResponse : Pr0grammResponse, IHasEndResponse
    {
        public IReadOnlyList<InboxConversation> Conversations { get; private set; }
        public bool AtEnd { get; set; }
    }
}
