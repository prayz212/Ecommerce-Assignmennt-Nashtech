using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ISharedService _sharedService;

        public NavbarViewComponent(ISharedService sharedService)
        {
            _sharedService = sharedService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _sharedService.GetCategoryData();
            return View("Navbar", categories);
        }
    }
}