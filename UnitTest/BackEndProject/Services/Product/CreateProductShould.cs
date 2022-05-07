using BackEnd.Services;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using Moq;
using Xunit;
using UnitTest.Utils;
using BackEnd.Models;

namespace UnitTest.BackEndProject.ProductServices
{
    public class CreateProductShould
    {
        [Fact]
        public async Task ReturnNullWhenCategoryIsNull()
        {
            //Arrange
            var category = MockData.DummyCreateProductDto.Category;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.GetById(category)).ReturnsAsync(MockData.NullCategory);

            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var actual = await productService.CreateProduct(MockData.DummyCreateProductDto);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task ReturnNullWhenOneOfSaveResultsIsFalse()
        {
            //Arrange
            var category = MockData.DummyCreateProductDto.Category;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.GetById(category)).ReturnsAsync(MockData.DummyCategory);
            mockRepository.Setup(r => r.Products.Add(MockData.DummyProduct)).ReturnsAsync(false);

            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<Product>(MockData.DummyCreateProductDto)).Returns(MockData.DummyProduct);

            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var actual = await productService.CreateProduct(MockData.DummyCreateProductDto);

            //Assert
            Assert.Null(actual);
        }
    }
}