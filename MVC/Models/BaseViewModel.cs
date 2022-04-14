

using System.Collections.Generic;
using ViewModels.Clients;

namespace MVC.Models
{
    public class BaseViewModel
    {
        public IEnumerable<CategoryReadDto> categories { get; set; }
    }
}
