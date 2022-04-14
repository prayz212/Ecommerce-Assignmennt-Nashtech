using System.Collections.Generic;
using ViewModels.Clients;

namespace MVC.Models
{
    public class HomeViewModel : BaseViewModel
    {
        public IEnumerable<ProductReadDto> products { get; set; }
    }
}
