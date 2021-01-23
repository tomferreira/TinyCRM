using System.Threading.Tasks;
using TinyCRM.Application.ViewModels.LegalPerson;

namespace TinyCRM.Application.Interfaces
{
    public interface ILegalPersonService
    {
        Task<ListLegalPersonViewModel> AllAsync();

        Task<LegalPersonViewModel> GetAsync(int id);

        void Add(LegalPersonViewModel model);

        Task UpdateAsync(LegalPersonViewModel model);
    }
}
