using System.Collections.Generic;

namespace OpenPr0gramm.Inbox.Response
{
#if FW
    [Serializable]
#endif
    public class GetInboxBlockedUserResponse : Pr0grammResponse
    {
        public IReadOnlyList<MarkedUser> BlockedUsers { get; private set; }
    }
}
