using RichDomainModeling.Domain.Entities;
using RichDomainModeling.Domain.ValueObjects;

namespace RichDomainModeling.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        private readonly Student _student;
        private readonly Subscription _subscription;
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;

        public StudentTests()
        {
            _name = new Name("Marco", "Filho");
            _document = new Document("35111507795", Domain.Enums.EDocumentType.CPF);
            _email = new Email("m.barceloslima@gmail.com");
            _address = new Address("Rua Icatu", "300", "Parque Industrial", "São José dos Campos", "SP", "BR", "12237010");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PayPalPayment("12312321", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(!_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);

            Assert.IsTrue(!_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var payment = new PayPalPayment("12312321", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.IsValid);
        }
    }
}
