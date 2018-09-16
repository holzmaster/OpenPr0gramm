using Refit;
using System.Threading.Tasks;

namespace OpenPr0gramm
{
    public interface IPr0grammPaypalService
    {
        [Post("/paypal/getcheckouturl")]
        Task<GetCheckoutUrlResponse> GetCheckoutUrl([Body(BodySerializationMethod.UrlEncoded)]PaymentData data);
    }
}
