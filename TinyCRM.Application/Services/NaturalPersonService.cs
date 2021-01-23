using System.Linq;
using System.Threading.Tasks;
using TinyCRM.Application.Interfaces;
using TinyCRM.Application.ViewModels.NaturalPerson;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Exceptions;
using TinyCRM.Domain.Interfaces;

namespace TinyCRM.Application.Services
{
    public class NaturalPersonService : INaturalPersonService
    {
        private INaturalPersonRepository _personRepository;

        public NaturalPersonService(INaturalPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ListNaturalPersonViewModel> AllAsync()
        {
            var people = await _personRepository.AllAsync();

            return new ListNaturalPersonViewModel()
            {
                People = people.Select(person => new NaturalPersonViewModel()
                {
                    Id = person.Id,
                    Name = person.Name,
                    Birthday = person.Birthday,
                    Gender = person.Gender,
                    Email = person.Email,
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

        public async Task<NaturalPersonViewModel> GetAsync(int id)
        {
            var person = await _personRepository.GetAsync(id);

            if (person == null)
                return null;

            return new NaturalPersonViewModel()
            {
                Id = person.Id,
                Name = person.Name,
                Birthday = person.Birthday,
                Gender = person.Gender,
                Email = person.Email,
                IdDocument = person.IdDocument,
                ZipCode = person.Address.ZipCode,
                Country = person.Address.Country,
                State = person.Address.State,
                City = person.Address.City,
                AddressLine1 = person.Address.AddressLine1,
                AddressLine2 = person.Address.AddressLine2
            };
        }

        public void Add(NaturalPersonViewModel model)
        {
            if (!_personRepository.IsIdDocumentUnique(model.IdDocument))
                throw new BusinessRuleException("IdDocument", "ID Document already informed.");

            if (!_personRepository.IsEmailUnique(model.Email))
                throw new BusinessRuleException("Email", "Email already informed.");

            var person = new NaturalPerson(
                model.Name, model.IdDocument, model.Birthday, model.Gender.Value, model.Email)
            {
                Address = new Address()
                {
                    ZipCode = model.ZipCode,
                    Country = model.Country,
                    State = model.State,
                    City = model.City,
                    AddressLine1 = model.AddressLine1,
                    AddressLine2 = model.AddressLine2
                }
            };

            _personRepository.Add(person);

            _personRepository.SaveChanges();
        }

        public async Task UpdateAsync(NaturalPersonViewModel model)
        {
            var person = await _personRepository.GetAsync(model.Id);

            if (person == null)
                return;

            person.Name = model.Name;
            person.Gender = model.Gender.Value;
            person.Email = model.Email;
            person.SetBirthday(model.Birthday);
            person.SetIdDocument(model.IdDocument);
            
            // Address attributes
            person.Address.ZipCode = model.ZipCode;
            person.Address.Country = model.Country;
            person.Address.State = model.State;
            person.Address.City = model.City;
            person.Address.AddressLine1 = model.AddressLine1;
            person.Address.AddressLine2 = model.AddressLine2;

            _personRepository.Update(person);

            _personRepository.SaveChanges();
        }
    }
}
