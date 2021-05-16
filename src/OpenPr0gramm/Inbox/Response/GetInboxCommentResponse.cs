using System.Collections.Generic;
using OpenPr0gramm.Generic.Response;
using OpenPr0gramm.Inbox.Model;

namespace OpenPr0gramm.Inbox.Response
{
#if FW
    [Serializable]
#endif
    public class GetInboxCommentResponse : Pr0grammResponse, IMessageResponse<InboxItem>
    {
        public IReadOnlyList<InboxItem> Messages { get; private set; }
    }
}
