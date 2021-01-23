using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Interfaces;
using TinyCRM.Infra.Data.Context;

namespace TinyCRM.Infra.Data.Repositories
{
    public class LegalPersonRepository : BaseRepository, ILegalPersonRepository
    {
        public LegalPersonRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<LegalPerson>> AllAsync()
        {
            return await _context.LegalPeople
                .Include(p => p.Address)
                .ToListAsync();
        }

        public async Task<LegalPerson> GetAsync(int id)
        {
            return await _context.LegalPeople
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Add(LegalPerson person)
        {
            _context.LegalPeople.Add(person);
        }

        public void Update(LegalPerson person)
        {
            _context.LegalPeople.Update(person);
        }

        public bool IsIdDocumentUnique(string idDocument)
        {
            int ammount = _context.LegalPeople
                .Count(p => p.IdDocument == idDocument);

            return ammount == 0;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
