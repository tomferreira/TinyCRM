using Maoli;
using TinyCRM.Domain.Exceptions;

namespace TinyCRM.Domain.Entities
{
    public class LegalPerson : Person
    {
        public string CompanyName { get; set; }
        public string TradeName { get; set; }

        public LegalPerson(string companyName, string tradeName, string idDocument)
        {
            CompanyName = companyName;
            TradeName = tradeName;
            SetIdDocument(idDocument);
        }

        public override void SetIdDocument(string value)
        {
            if (!Cnpj.Validate(value, CnpjPunctuation.Strict))
                throw new BusinessRuleException("IdDocument", "CNPJ is invalid.");

            IdDocument = value;
        }
    }
}
