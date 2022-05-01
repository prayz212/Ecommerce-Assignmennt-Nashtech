using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSite.Interfaces;
using CustomerSite.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ISharedService _sharedService;

        public CategoriesViewComponent(ISharedService sharedService)
        {
            _sharedService = sharedService;
        }

        public async Task<IViewComponentResult> InvokeAsync(String currentCategory = ConstantVariable.DEFAULT_CATEGORY)
        {
            var categories = await _sharedService.GetCategoryData();
            ViewData["Current"] = currentCategory;
            
            return View(categories);
        }
    }
}