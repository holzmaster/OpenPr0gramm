using System.Collections.Generic;
using OpenPr0gramm.Generic.Response;
using OpenPr0gramm.Inbox.Model;
using OpenPr0gramm.Model;

namespace OpenPr0gramm.Inbox.Response
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
