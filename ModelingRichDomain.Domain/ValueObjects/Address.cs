using Flunt.Validations;
using RichDomainModeling.Shared.ValueObjects;

namespace RichDomainModeling.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(new Contract<Name>()
               .Requires()
               .IsGreaterThan(Street, 3, "Address.Street", "Street must contain at least 3 characters")
               .IsNotNull(Number, "Address.LastName", "Street must not be null"));
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }

}
