
namespace OpenPr0gramm
{
    public class GetPaymentAddressResponse : Pr0grammResponse
    {
        public string Address { get; set; }
        public decimal Amount { get; set; }
    }
}
