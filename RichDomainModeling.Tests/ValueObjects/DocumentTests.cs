using RichDomainModeling.Domain.Enums;
using RichDomainModeling.Domain.ValueObjects;

namespace RichDomainModeling.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(!doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {
            var doc = new Document("12345678911234", EDocumentType.CNPJ);
            Assert.IsTrue(doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(!doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCPFIsValid()
        {
            var doc = new Document("12345678910", EDocumentType.CPF);
            Assert.IsTrue(doc.IsValid);
        }
    }
}
