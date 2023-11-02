using Flunt.Notifications;
using RichDomainModeling.Domain.Command;
using RichDomainModeling.Domain.Entities;
using RichDomainModeling.Domain.Enums;
using RichDomainModeling.Domain.Repositories;
using RichDomainModeling.Domain.Services;
using RichDomainModeling.Domain.ValueObjects;
using RichDomainModeling.Shared.Commands;
using RichDomainModeling.Shared.Handlers;

namespace RichDomainModeling.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>,
        IHandler<CreateBoletoSubscriptionCommand>
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();

            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "We cannot complete your signature.");
            }

            // Verificar se Documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se E-mail já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificações
            if (!IsValid)
                return new CommandResult(false, "We cannot complete your signature.");

            // Salvar as Informações
            _studentRepository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.SendEmail(student.Name.ToString(), student.Email.Address, "Welcome to our Rich Domain Model", "Your subscription was created!");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
