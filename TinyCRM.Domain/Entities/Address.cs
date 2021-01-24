using TinyCRM.Domain.Exceptions;

namespace TinyCRM.Domain.Entities
{
    public class Address
    {
        public int Id { get; private set; }
        public string ZipCode { get; private set; }
        public string Country { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }

        public Address(string country, string state, string city, string zipCode, string addressLine1, string addressLine2)
        {
            ChangeAddress(country, state, city, zipCode, addressLine1, addressLine2);
        }

        public void ChangeAddress(string country, string state, string city, string zipCode, string addressLine1, string addressLine2)
        {
            CheckConsistency(country, state, city, zipCode, addressLine1, addressLine2);

            Country = country;
            State = state;
            City = city;
            ZipCode = zipCode;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
        }

        private void CheckConsistency(string country, string state, string city, string zipCode, string addressLine1, string addressLine2)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new BusinessRuleException("Country", "Country must be informed");

            if (string.IsNullOrWhiteSpace(state) && city != null)
                throw new BusinessRuleException("State", "State must be informed");

            if (string.IsNullOrWhiteSpace(city) && addressLine1 != null)
                throw new BusinessRuleException("City", "City must be informed");

            if (string.IsNullOrWhiteSpace(addressLine1) && addressLine2 != null)
                throw new BusinessRuleException("AddressLine1", "AddressLine1 must be informed");
        }

        public int PersonId { get; set; }
        public Person Person { get; set; }

    }
}
