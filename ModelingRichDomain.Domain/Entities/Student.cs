using Flunt.Validations;
using RichDomainModeling.Domain.ValueObjects;
using RichDomainModeling.Shared.Entities;
using System.Diagnostics.Contracts;

namespace RichDomainModeling.Domain.Entities
{
    public class Student : BaseEntity
    {
        private IList<Subscription> _subscriptions;

        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;

            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions
        {
            get
            {
                return _subscriptions.ToArray();
            }
        }

        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = false;

            foreach (var sub in _subscriptions)
            {
                if (sub.IsActive)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract<Student>()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "You already have an active subscription")
                .AreEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "This signature has no payments")
            );
        }
    }
}
