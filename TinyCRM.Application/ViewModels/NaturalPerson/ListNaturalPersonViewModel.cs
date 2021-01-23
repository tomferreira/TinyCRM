using System.Collections.Generic;

namespace TinyCRM.Application.ViewModels.NaturalPerson
{
    public class ListNaturalPersonViewModel
    {
        public IEnumerable<NaturalPersonViewModel> People { get; set; }
    }
}
