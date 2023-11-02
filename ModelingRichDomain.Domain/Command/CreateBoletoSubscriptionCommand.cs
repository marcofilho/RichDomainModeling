using Flunt.Notifications;
using Flunt.Validations;
using RichDomainModeling.Domain.Enums;
using RichDomainModeling.Shared.Commands;

namespace RichDomainModeling.Domain.Command
{
    public class CreateBoletoSubscriptionCommand : Notifiable<Notification>, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }

        public string BarCode { get; set; }
        public string BoletoNumber { get; set; }

        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string PayerEmail { get; set; }

        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<CreateBoletoSubscriptionCommand>()
                .Requires()
                .IsGreaterThan(FirstName, 3, "Name.FirstName", "Name must contain at least 3 characters")
                .IsGreaterThan(LastName, 3, "Name.LastName", "Last Name must contain at least 3 caracters")
                .IsLowerThan(FirstName, 40, "Name.FirstName", "Name must contain a maximum of 40 caracters"));
        }
    }
}
