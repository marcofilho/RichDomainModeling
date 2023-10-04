using RichDomainModeling.Domain.ValueObjects;

namespace RichDomainModeling.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(string transactionCode,
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            Document document,
            Address address,
            Email email) : base(
                paidDate,
                expireDate,
                total,
                totalPaid,
                document,
                address,
                email)
        {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; set; }
    }
}
