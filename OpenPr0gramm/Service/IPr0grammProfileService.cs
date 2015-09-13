using Refit;
using System.Threading.Tasks;

namespace OpenPr0gramm
{
    public interface IPr0grammProfileService
    {
        [Get("/profile/comments")]
        Task<GetCommentsResponse> GetCommentsBefore(string name, ItemFlags flags, int before);

        [Get("/profile/comments")]
        Task<GetCommentsResponse> GetCommentsAfter(string name, ItemFlags flags, int after);

        [Post("/profile/follow")]
        Task<Pr0grammResponse> Follow([Body(BodySerializationMethod.UrlEncoded)]FollowData data);

        [Get("/profile/info")]
        Task<GetProfileInfoResponse> GetInfo(string name, ItemFlags flags);

        [Post("/profile/unfollow")]
        Task<Pr0grammResponse> Unfollow([Body(BodySerializationMethod.UrlEncoded)]FollowData data);
    }
}
