using System.Collections.Generic;

namespace OpenPr0gramm.Inbox.Response
{
#if FW
    [Serializable]
#endif
    public abstract class GetGenericInboxMessagesResponse<T> : Pr0grammResponse
    {
        public IReadOnlyList<T> Messages { get; private set; }
    }
}
