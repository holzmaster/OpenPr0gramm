using Refit;
using System.Threading.Tasks;

namespace OpenPr0gramm
{
    public interface IPr0grammTagsService
    {
        [Post("/tags/add")]
        Task<Pr0grammResponse> Add([Body(BodySerializationMethod.UrlEncoded)]AddTagsData data);

        [Post("/tags/delete")]
        Task<Pr0grammResponse> Delete([Body(BodySerializationMethod.UrlEncoded)]DeleteTagsData data);

        [Get("/tags/details")]
        Task<GetDetailsResponse> GetDetails(int itemId);

        [Post("/tags/vote")]
        Task<Pr0grammResponse> Vote([Body(BodySerializationMethod.UrlEncoded)]VoteData data);
    }
}
