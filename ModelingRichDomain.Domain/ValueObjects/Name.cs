using RichDomainModeling.Shared.ValueObjects;

namespace RichDomainModeling.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            if (string.IsNullOrEmpty(FirstName))
                AddNotification("Name.FirstName", "Invalid name!");

            if (string.IsNullOrEmpty(LastName))
                AddNotification("Name.LastName", "Invalid last name!");
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
