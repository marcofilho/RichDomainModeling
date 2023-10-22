using Flunt.Validations;
using RichDomainModeling.Domain.ValueObjects;
using RichDomainModeling.Shared.Entities;

namespace RichDomainModeling.Domain.Entities
{
    public abstract class Payment : BaseEntity
    {
        protected Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, Document document, Address address, Email email)
        {
            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
            PaidDate = paidDate;
            ExpireDate = expireDate;
            Total = total;
            TotalPaid = totalPaid;
            Document = document;
            Address = address;
            Email = email;

            AddNotifications(new Contract<Payment>()
                .Requires()
                .IsGreaterThan(0, Total, "Payment.Total", "The total value must not be zero")
                .IsLowerOrEqualsThan(Total, TotalPaid, "Payment.TotalPaid", "The paid value is lower than the payment value"));
        }

        public string Number { get; private set; }
        public DateTime PaidDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPaid { get; private set; }
        public Document Document { get; private set; }
        public Address Address { get; private set; }
        public Email Email { get; private set; }
    }
}
