using Flunt.Validations;
using RichDomainModeling.Shared.ValueObjects;

namespace RichDomainModeling.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract<Name>()
                .Requires()
                .IsGreaterThan(FirstName, 3, "Name.FirstName", "Name must contain at least 3 characters")
                .IsGreaterThan(LastName, 3, "Name.LastName", "Last Name must contain at least 3 caracters")
                .IsLowerThan(FirstName, 40, "Name.FirstName", "Name must contain a maximum of 40 caracters"));

        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
