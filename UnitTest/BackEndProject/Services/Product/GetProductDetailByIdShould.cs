using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using Xunit;
using AutoMapper;
using UnitTest.Utils;
using Shared.Clients;

namespace UnitTest.BackEndProject.Services.Product
{
    public class GetProductDetailByIdShould
    {
        [Fact]
        public async Task ReturnNullWhenProductIdInvalid()
        {
            //Arrange
            var productId = -1;

            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);
            
            //Act
            var result = await productService.GetProductDetailById(productId);
            
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnValueWhenQuerySuccess()
        {
            //Arrange
            var productId = 1;

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetProduct(productId)).ReturnsAsync(MockData.DummyProduct);

            var mockRatingRepository = new Mock<IRatingRepository>();

            var mockAutoMapper = new Mock<IMapper>();            
            mockAutoMapper.Setup(m => m.Map<ProductDetailReadDto>(MockData.DummyProduct)).Returns(MockData.DummyProductDetailReadDto);

            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);
            
            //Act
            var result = await productService.GetProductDetailById(productId);
            
            //Assert
            Assert.True(string.Equals(JsonConvert.SerializeObject(MockData.DummyProductDetailReadDto), JsonConvert.SerializeObject(result)));
        }
    }
}