using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Exceptions;
using Xunit;

namespace TinyCRM.Domain.UnitTest
{
    public class LegalPersonTest
    {
        [Fact]
        public void Add_Invalid_IdDocument()
        {
            Assert.Throws<BusinessRuleException>(
                () => new LegalPerson("Varig", "Varig do Brasil", "03.309.337/0001-83"));
        }

        [Fact]
        public void Add_Valid_IdDocument()
        {
            var person = new LegalPerson("Varig", "Varig do Brasil", "03.309.337/0001-73");

            Assert.True(person.IdDocument == "03.309.337/0001-73");
        }

        [Fact]
        public void Add_Valid_IdDocument_Without_Puctuation()
        {
            Assert.Throws<BusinessRuleException>(
                () => new LegalPerson("Varig", "Varig do Brasil", "03309337000173"));
        }

        [Fact]
        public void Add_Valid_Person_With_Address()
        {
            var person = new LegalPerson("Varig", "Varig do Brasil", "03.309.337/0001-73");

            person.Address = new Address("Brazil", "Sao Paulo", "Sao Paulo", null, null, null);

            Assert.True(person.Address.Country == "Brazil");
            Assert.True(person.Address.State == "Sao Paulo");
        }

        [Fact]
        public void Add_Valid_Person_Without_Address()
        {
            var person = new LegalPerson("Varig", "Varig do Brasil", "03.309.337/0001-73");

            Assert.True(person.CompanyName == "Varig");
        }
    }
}
