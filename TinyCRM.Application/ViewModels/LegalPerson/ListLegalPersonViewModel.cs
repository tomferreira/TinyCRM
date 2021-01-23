using System.Collections.Generic;

namespace TinyCRM.Application.ViewModels.LegalPerson
{
    public class ListLegalPersonViewModel
    {
        public IEnumerable<LegalPersonViewModel> People { get; set; }
    }
}
