using System.Collections.Generic;
using System.Threading.Tasks;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Interfaces
{
    public interface ILegalPersonRepository
    {
        Task<IEnumerable<LegalPerson>> AllAsync();
        Task<LegalPerson> GetAsync(int id);
        void Add(LegalPerson person);
        void Update(LegalPerson person);
        void Delete(LegalPerson person);

        bool IsIdDocumentUnique(string idDocument);

        void SaveChanges();
    }
}
