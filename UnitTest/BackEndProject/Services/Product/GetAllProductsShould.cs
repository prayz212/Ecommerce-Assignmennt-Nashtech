using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Shared.Clients;
using Xunit;
using AutoMapper;
using UnitTest.Utils;

namespace UnitTest.BackEndProject.Services.Product
{
    public class GetAllProductsShould
    {
        [Fact]
        public async Task ReturnNullWhenPassingInvalidParams()
        {
            //Arrange
            int page = 1;
            int size = 0;

            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);

            //Act
            var result = await productService.GetAllProducts(page, size);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnOkWithValueWhenHavingProducts()
        {
            //Arrange
            int page = 1;
            int size = 6;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProducts(page, size)).ReturnsAsync(MockData.DummyListProduct);
            mockProductRepository.Setup(r => r.CountAllProducts()).ReturnsAsync(MockData.DummyListProduct.Count);

            var mockRatingRepository = new Mock<IRatingRepository>();

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<IEnumerable<ProductReadDto>>(MockData.DummyListProduct)).Returns(MockData.DummyListProductReadDto);

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);
            
            //Act
            var result = await productService.GetAllProducts(page, size);

            //Assert
            Assert.Equal(MockData.DummyProductListReadDto.Products.Count , result.Products.Count);
            Assert.True(MockData.DummyProductListReadDto.Products.All(expected => result.Products.Any(actual => 
            { 
                var objExpected = JsonConvert.SerializeObject(expected);
                var objActual = JsonConvert.SerializeObject(actual);

                return string.Equals(objExpected, objActual);
            })));
            Assert.Equal(MockData.DummyProductListReadDto.CurrentPage, result.CurrentPage);
            Assert.Equal(MockData.DummyProductListReadDto.TotalPage, result.TotalPage);
        }
    }
}