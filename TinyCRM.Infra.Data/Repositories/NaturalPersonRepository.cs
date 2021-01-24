using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Interfaces;
using TinyCRM.Infra.Data.Context;

namespace TinyCRM.Infra.Data.Repositories
{
    public class NaturalPersonRepository : BaseRepository, INaturalPersonRepository
    {
        public NaturalPersonRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<NaturalPerson>> AllAsync()
        {
            return await _context.NaturalPeople
                .Include(p => p.Address)
                .ToListAsync();
        }

        public async Task<NaturalPerson> GetAsync(int id)
        {
            return await _context.NaturalPeople
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Add(NaturalPerson person)
        {
            _context.NaturalPeople.Add(person);
        }

        public void Update(NaturalPerson person)
        {
            _context.NaturalPeople.Update(person);
        }

        public void Delete(NaturalPerson person)
        {
            _context.NaturalPeople.Remove(person);
        }

        public bool IsIdDocumentUnique(string idDocument)
        {
            int ammount = _context.NaturalPeople
                .Count(p => p.IdDocument == idDocument);

            return ammount == 0;
        }

        public bool IsEmailUnique(string email)
        {
            int ammount = _context.NaturalPeople
                .Count(p => p.Email.ToUpperInvariant() == email.ToUpperInvariant());

            return ammount == 0;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
