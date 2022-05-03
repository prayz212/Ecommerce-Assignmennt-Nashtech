using System.Threading.Tasks;
using CustomerSite.Interfaces;
using CustomerSite.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.ViewComponents
{
    public class RelativeProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public RelativeProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int size = ConstantVariable.NUMBER_OF_RELATIVE_PRODUCTS)
        {
            var products = await _productService.GetRelativeProductData(id, size);
            return View("RelativeProducts", products);
        }
    }
}