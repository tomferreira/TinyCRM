using System;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Exceptions;
using Xunit;

namespace TinyCRM.Domain.UnitTest
{
    public class NaturalPersonTest
    {
        [Fact]
        public void Add_Birthday_Greater_Today()
        {
            Assert.Throws<BusinessRuleException>(() => new NaturalPerson(
                "Jorge Amado", "719.032.860-26", DateTime.Today.AddDays(1), NaturalPerson.GenderType.Male, null));
        }

        [Fact]
        public void Add_Invalid_IdDocument()
        {
            Assert.Throws<BusinessRuleException>(() => new NaturalPerson(
                "Jorge Amado", "719.032.860-06", DateTime.Today, NaturalPerson.GenderType.Male, null));
        }

        [Fact]
        public void Add_Valid_IdDocument()
        {
            var person = new NaturalPerson(
                "Jorge Amado", "719.032.860-26", DateTime.Today, NaturalPerson.GenderType.Male, null);

            Assert.True(person.IdDocument == "719.032.860-26");
        }

        [Fact]
        public void Add_Valid_IdDocument_Without_Puctuation()
        {
            Assert.Throws<BusinessRuleException>(() => new NaturalPerson(
                "Jorge Amado", "71903286026", DateTime.Today, NaturalPerson.GenderType.Male, null));
        }

        [Fact]
        public void Add_Valid_Person_With_Address()
        {
            var person = new NaturalPerson(
                "Jorge Amado", "719.032.860-26", DateTime.Today, NaturalPerson.GenderType.Male, null);

            person.Address = new Address("Brazil", "Sao Paulo", "Sao Paulo", null, null, null);

            Assert.True(person.Address.Country == "Brazil");
            Assert.True(person.Address.State == "Sao Paulo");
        }

        [Fact]
        public void Add_Valid_Person_Without_Address()
        {
            var person = new NaturalPerson(
                "Jorge Amado", "719.032.860-26", DateTime.Today, NaturalPerson.GenderType.Male, null);

            Assert.True(person.Name == "Jorge Amado");
        }
    }
}
