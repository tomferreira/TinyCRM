using Maoli;
using System;
using TinyCRM.Domain.Exceptions;

namespace TinyCRM.Domain.Entities
{
    public class NaturalPerson : Person
    {
        public enum GenderType
        {
            Male,
            Female,
            NotInformed
        };

        public NaturalPerson(string name, string idDocument, DateTime birthday, GenderType gender, string email)
        {
            Name = name;
            Gender = gender;
            Email = email;

            SetIdDocument(idDocument);
            SetBirthday(birthday); 
        }

        public string Name { get; set; }

        public DateTime Birthday { get; private set; }

        public GenderType Gender { get; set; }

        public string Email { get; set; }

        public override void SetIdDocument(string value)
        {
            if (!Cpf.Validate(value))
                throw new BusinessRuleException("IdDocument", "CPF is invalid.");

            IdDocument = value;
        }

        public void SetBirthday(DateTime value)
        {
            if (value.Date > DateTime.Now.Date)
                throw new BusinessRuleException("Birthday", "Birthday can't be greater than today");

            Birthday = value;
        }
    }
}
