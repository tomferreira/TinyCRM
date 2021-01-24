using System.Threading.Tasks;
using TinyCRM.Application.ViewModels.NaturalPerson;

namespace TinyCRM.Application.Interfaces
{
    public interface INaturalPersonService
    {
        Task<ListNaturalPersonViewModel> AllAsync();

        Task<NaturalPersonViewModel> GetAsync(int id);

        void Add(NaturalPersonViewModel model);

        Task UpdateAsync(NaturalPersonViewModel model);

        Task DeleteAsync(int id);
    }
}
