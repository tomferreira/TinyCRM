using System.Linq;
using System.Threading.Tasks;
using TinyCRM.Application.Interfaces;
using TinyCRM.Application.ViewModels.LegalPerson;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Exceptions;
using TinyCRM.Domain.Interfaces;

namespace TinyCRM.Application.Services
{
    public class LegalPersonService : ILegalPersonService
    {
        private ILegalPersonRepository _personRepository;

        public LegalPersonService(ILegalPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ListLegalPersonViewModel> AllAsync()
        {
            var people = await _personRepository.AllAsync();

            return new ListLegalPersonViewModel()
            {
                People = people.Select(person => new LegalPersonViewModel()
                {
                    Id = person.Id,
                    CompanyName = person.CompanyName,
                    TradeName = person.TradeName,
                    IdDocument = person.IdDocument,
                    ZipCode = person.Address.ZipCode,
                    Country = person.Address.Country,
                    State = person.Address.State,
                    City = person.Address.City,
                    AddressLine1 = person.Address.AddressLine1,
                    AddressLine2 = person.Address.AddressLine2
                })
            };
        }

        public async Task<LegalPersonViewModel> GetAsync(int id)
        {
            var person = await _personRepository.GetAsync(id);

            if (person == null)
                return null;

            return new LegalPersonViewModel()
            {
                Id = person.Id,
                CompanyName = person.CompanyName,
                TradeName = person.TradeName,
                IdDocument = person.IdDocument,
                ZipCode = person.Address.ZipCode,
                Country = person.Address.Country,
                State = person.Address.State,
                City = person.Address.City,
                AddressLine1 = person.Address.AddressLine1,
                AddressLine2 = person.Address.AddressLine2
            };
        }

        public void Add(LegalPersonViewModel model)
        {
            if (!_personRepository.IsIdDocumentUnique(model.IdDocument))
                throw new BusinessRuleException("IdDocument", "ID Document already informed.");

            var person = new LegalPerson(model.CompanyName, model.TradeName, model.IdDocument)
            {
                Address = new Address(
                    model.Country, model.State, model.City, model.ZipCode, model.AddressLine1, model.AddressLine2)
            };

            _personRepository.Add(person);

            _personRepository.SaveChanges();
        }

        public async Task UpdateAsync(LegalPersonViewModel model)
        {
            var person = await _personRepository.GetAsync(model.Id);

            if (person == null)
                return;

            person.CompanyName = model.CompanyName;
            person.TradeName = model.TradeName;
            person.SetIdDocument(model.IdDocument);

            // Address is optional, if country not informed delete address entity
            if (!string.IsNullOrWhiteSpace(model.Country))
            {
                if (person.Address == null)
                {
                    person.Address = new Address(
                        model.Country, model.State, model.City, model.ZipCode, model.AddressLine1, model.AddressLine2);
                }
                else
                {
                    person.Address.ChangeAddress(
                        model.Country, model.State, model.City, model.ZipCode, model.AddressLine1, model.AddressLine2);
                }
            }
            else
            {
                person.Address = null;
            }

            _personRepository.Update(person);

            _personRepository.SaveChanges();
        }
    }
}
