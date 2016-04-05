using Refit;
using System.Threading.Tasks;

namespace OpenPr0gramm
{
    public interface IPr0grammItemsService
    {
        [Post("/items/delete")]
        Task<Pr0grammResponse> Delete([Body(BodySerializationMethod.UrlEncoded)]DeleteItemData data);

        [Get("/items/get")]
        Task<GetItemsResponse> GetItems(ItemFlags flags, int promoted, int following, string tags, string user, string likes, int self);
        [Get("/items/get")]
        Task<GetItemsResponse> GetItemsNewer(ItemFlags flags, int promoted, int following, string tags, string user, string likes, int self, int newer);
        [Get("/items/get")]
        Task<GetItemsResponse> GetItemsOlder(ItemFlags flags, int promoted, int following, string tags, string user, string likes, int self, int older);
        [Get("/items/get")]
        Task<GetItemsResponse> GetItemsAround(ItemFlags flags, int promoted, int following, string tags, string user, string likes, int self, [AliasAs("id")] int aroundId);

        [Get("/items/info")]
        Task<GetItemsInfoResponse> GetInfo(int itemId);

        /*
        [Post("/items/post")]
        Task Post([Body(BodySerializationMethod.UrlEncoded)]PostItemData data);

        [Multipart]
        [Post("/items/upload")]
        Task<UploadResponse> Upload(FileInfo file);
        */

        [Post("/items/ratelimited")]
        Task<Pr0grammResponse> RateLimited();

        [Post("/items/vote")]
        Task<Pr0grammResponse> Vote([Body(BodySerializationMethod.UrlEncoded)]VoteData data);
    }

    /*
#if FW
    [Serializable]
#endif
    public class UploadResponse
    {
        public string Key { get; set; }
    }

    public class PostItemData : PostFormData
    {
        [AliasAs("sfwstatus")]
        public string SfwStatus { get; }
        [AliasAs("tags")]
        public string Tags { get; }
        [AliasAs("checkSimilar")]
        public int CheckSimilar { get; }
        [AliasAs("key")]
        public string Key { get; }

        public PostItemData(string nonce)
            : base(nonce)
        {
            //TODO
        }
    }
    */

}
