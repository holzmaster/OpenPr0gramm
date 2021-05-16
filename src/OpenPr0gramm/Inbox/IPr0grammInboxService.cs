using System.Threading.Tasks;
using OpenPr0gramm.Inbox.FormData;
using OpenPr0gramm.Inbox.Response;
using Refit;

namespace OpenPr0gramm.Inbox
{
    public interface IPr0grammInboxService
    {
        [Get("/inbox/all")]
        Task<GetInboxOverviewResponse> GetOverview();

        [Get("/inbox/comments")]
        Task<GetInboxCommentResponse> GetComments();

        [Get("/inbox/notifications")]
        Task<GetInboxNotificationResponse> GetNotifications();

        //TODO [Get("/inbox/follows")]

        [Get("/inbox/conversations")]
        Task<GetInboxConversationResponse> GetConversations(long? older);

        [Get("/inbox/messages")]
        Task<GetInboxPrivateMessageResponse> GetMessages(string with, long? older);

        [Post("/inbox/post")]
        Task<Pr0grammResponse> PostMessage([Body(BodySerializationMethod.UrlEncoded)]PrivateMessageData data);

    }
}
