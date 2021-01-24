using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Exceptions;
using Xunit;

namespace TinyCRM.Domain.UnitTest
{
    public class AddressTest
    {
        private const string COUNTRY = "Brazil";
        private const string STATE = "São Paulo";
        private const string CITY = "São Paulo";
        private const string ADDRESSLINE1 = "Av. Paulista, 1000";

        [Fact]
        public void Add_Address_With_Empty_Values()
        {
            Assert.Throws<BusinessRuleException>(() => new Address("", "", "", "", "", ""));
        }

        [Fact]
        public void Add_Address_With_Null_Values()
        {
            Assert.Throws<BusinessRuleException>(() => new Address(null, null, null, null, null, null));
        }

        [Fact]
        public void Add_Address_Only_Country()
        {
            var address = new Address(COUNTRY, null, null, null, null, null);
            Assert.True(address.Country == COUNTRY);
        }

        [Fact]
        public void Add_Address_Only_Country_State()
        {
            var address = new Address(COUNTRY, STATE, null, null, null, null);
            Assert.True(address.Country == COUNTRY);
            Assert.True(address.State == STATE);
        }

        [Fact]
        public void Add_Address_Only_Country_State_City()
        {
            var address = new Address(COUNTRY, STATE, CITY, null, null, null);
            Assert.True(address.Country == COUNTRY);
            Assert.True(address.State == STATE);
            Assert.True(address.City == CITY);
        }

        [Fact]
        public void Add_Address_Only_Country_State_City_AddressLine1()
        {
            var address = new Address(COUNTRY, STATE, CITY, null, ADDRESSLINE1, null);
            Assert.True(address.Country == COUNTRY);
            Assert.True(address.State == STATE);
            Assert.True(address.City == CITY);
            Assert.True(address.AddressLine1 == ADDRESSLINE1);
        }

        [Fact]
        public void Add_Invalid_Incomplete_Addresses()
        {
            Assert.Throws<BusinessRuleException>(() => new Address(null, STATE, null, null, null, null));

            Assert.Throws<BusinessRuleException>(() => new Address(COUNTRY, null, CITY, null, null, null));

            Assert.Throws<BusinessRuleException>(() => new Address(COUNTRY, null, CITY, null, null, null));

            Assert.Throws<BusinessRuleException>(() => new Address(COUNTRY, null, null, null, ADDRESSLINE1, null));

            Assert.Throws<BusinessRuleException>(() => new Address(COUNTRY, null, CITY, null, ADDRESSLINE1, null));

            Assert.Throws<BusinessRuleException>(() => new Address(COUNTRY, STATE, null, null, ADDRESSLINE1, null));
        }


        [Fact]
        public void Add_Complete_Address()
        {
            var address = new Address(COUNTRY, STATE, CITY, "0123456", ADDRESSLINE1, "Apto 10");
            Assert.True(address.Country == COUNTRY);
            Assert.True(address.State == STATE);
            Assert.True(address.City == CITY);
            Assert.True(address.AddressLine1 == ADDRESSLINE1);
            Assert.True(address.ZipCode == "0123456");
            Assert.True(address.AddressLine2 == "Apto 10");
        }

        [Fact]
        public void Change_Valid_Address_to_Invalid_Address()
        {
            var address = new Address(COUNTRY, STATE, CITY, "0123456", ADDRESSLINE1, "Apto 10");

            Assert.Throws<BusinessRuleException>(() => 
                address.ChangeAddress("", STATE, CITY, "0123456", ADDRESSLINE1, "Apto 10"));
        }

        [Fact]
        public void Change_Valid_Address()
        {
            var address = new Address(COUNTRY, STATE, CITY, "0123456", ADDRESSLINE1, "Apto 10");

            address.ChangeAddress(COUNTRY, STATE, CITY, "9876543", ADDRESSLINE1, "Apto 101");

            Assert.True(address.ZipCode == "9876543");
            Assert.True(address.AddressLine2 == "Apto 101");
        }
    }
}
