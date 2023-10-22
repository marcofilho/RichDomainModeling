using Flunt.Validations;
using RichDomainModeling.Shared.ValueObjects;

namespace RichDomainModeling.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract<Email>()
                .Requires()
                .IsEmail(Address, "Email.Address", "Invalid e-mail"));
        }
        public string Address { get; private set; }

    }
}
