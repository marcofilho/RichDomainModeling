using Flunt.Validations;
using RichDomainModeling.Shared.Entities;
using System.Diagnostics.Contracts;

namespace RichDomainModeling.Domain.Entities
{
    public class Subscription : BaseEntity
    {
        private IList<Payment> _payments;

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public bool IsActive { get; private set; }

        public IReadOnlyCollection<Payment> Payments
        {
            get
            {
                return _payments.ToArray();
            }
        }

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract<Subscription>().
                Requires().
                IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "The payment date must has be in the future."));

            _payments.Add(payment);
        }

        public void Activate()
        {
            IsActive = true;
            LastUpdateDate = DateTime.Now;
        }

        public void Deactivate()
        {
            IsActive = false;
            LastUpdateDate = DateTime.Now;
        }
    }
}
