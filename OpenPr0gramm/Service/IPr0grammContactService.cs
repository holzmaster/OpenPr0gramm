using Refit;
using System.Threading.Tasks;

namespace OpenPr0gramm
{
    public interface IPr0grammContactService
    {
        [Post("/contact/send")]
        Task<Pr0grammResponse> Send([Body(BodySerializationMethod.UrlEncoded)]ContactData data);
    }
}
