using TinyCRM.Infra.Data.Context;

namespace TinyCRM.Infra.Data.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context) => _context = context;
    }
}
