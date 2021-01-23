using System.Collections.Generic;
using System.Threading.Tasks;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Interfaces
{
    public interface INaturalPersonRepository
    {
        Task<IEnumerable<NaturalPerson>> AllAsync();
        Task<NaturalPerson> GetAsync(int id);
        void Add(NaturalPerson person);
        void Update(NaturalPerson person);

        bool IsIdDocumentUnique(string idDocument);
        bool IsEmailUnique(string email);

        void SaveChanges();
    }
}
