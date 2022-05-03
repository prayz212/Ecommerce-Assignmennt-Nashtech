using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Services;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.Services.Product
{
    public class GetFeatureProductsShould
    {
        [Fact]
        public async Task ReturnNullWhenHavingInvalidPage()
        {
            //Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProducts(0, 6);

            //Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public async Task ReturnNullWhenHavingInvalidSize()
        {
            //Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProducts(1, -1);

            //Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public async Task ReturnNullWhenTotalPageEqualMinusOne()
        {
            //Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.CountFeatureProducts()).ReturnsAsync(-1);

            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProducts(1, 6);

            //Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public async Task ReturnValueWhenQuerySuccess()
        {
            //Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(r => r.GetFeatureProducts(1, 8)).ReturnsAsync(MockData.DummyListProductReadDto);
            mockProductRepository.Setup(r => r.CountFeatureProducts()).ReturnsAsync(MockData.DummyListProductReadDto.Count);

            var mockRatingRepository = new Mock<IRatingRepository>();
            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockProductRepository.Object, mockRatingRepository.Object, mockAutoMapper.Object);
            
            //Act
            var actualResult = await productService.GetFeatureProducts(1, 8);

            //Assert
            Assert.Equal(MockData.DummyProductListReadDto.CurrentPage, actualResult.CurrentPage);
            Assert.Equal(MockData.DummyProductListReadDto.TotalPage, actualResult.TotalPage);
            Assert.Equal(MockData.DummyProductListReadDto.Products.Count, actualResult.Products.Count);
        }
    }
}