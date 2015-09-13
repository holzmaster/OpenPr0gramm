using Refit;
using System.Threading.Tasks;

namespace OpenPr0gramm
{
    public interface IPr0grammBitcoinService
    {
        [Post("/bitcoin/getpaymentaddress")]
        Task<GetPaymentAddressResponse> GetPaymentAddress([Body(BodySerializationMethod.UrlEncoded)]PaymentData data);
    }
}
