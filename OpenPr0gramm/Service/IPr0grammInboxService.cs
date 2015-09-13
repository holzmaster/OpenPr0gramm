using Refit;
using System.Threading.Tasks;

namespace OpenPr0gramm
{
    public interface IPr0grammInboxService
    {
        [Get("/inbox/all")]
        Task<GetMessagesResponse<InboxItem>> GetAll();

        [Get("/inbox/messages")]
        Task<GetMessagesResponse<PrivateMessage>> GetPrivateMessages();

        [Post("/inbox/post")]
        Task<Pr0grammResponse> PostMessage([Body(BodySerializationMethod.UrlEncoded)]PrivateMessageData data);

        [Get("/inbox/unread")]
        Task<GetMessagesResponse<InboxItem>> GetUnreadMessages();
    }
}
