using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Services;
using Moq;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.ProductServices
{
    public class UpdateProductShould
    {
        [Fact]
        public async Task ReturnNullWhenCategoryIsNull()
        {
            //Arrange
            var category = MockData.DummyUpdateProductDto.Category;

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.GetById(category)).ReturnsAsync(MockData.NullCategory);

            var mockAutoMapper = new Mock<IMapper>();
            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var actual = await productService.UpdateProduct(MockData.DummyUpdateProductDto);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task ReturnNullWhenDeleteImagesNotValid()
        {
            //Arrange
            var id = MockData.DummyUpdateProductDto.Id;
            var category = MockData.DummyUpdateProductDto.Category;
            var deleteImageList = MockData.DummyUpdateProductDto.DeletedImages.Select(i => i.Uri);

            var mockRepository = new Mock<IUnitOfWork>();
            mockRepository.Setup(r => r.Categories.GetById(category)).ReturnsAsync(MockData.DummyCategory);
            mockRepository.Setup(r => r.Products.Update(MockData.DummyProduct)).ReturnsAsync(true);
            mockRepository.Setup(r => r.Images.GetAll(i => i.ProductId == id, 0, 0, null, "")).ReturnsAsync(MockData.DummyListImage);


            var mockAutoMapper = new Mock<IMapper>();
            mockAutoMapper.Setup(m => m.Map<Product>(MockData.DummyUpdateProductDto)).Returns(MockData.DummyProduct);

            var productService = new ProductService(mockRepository.Object, mockAutoMapper.Object);

            //Act
            var actual = await productService.UpdateProduct(MockData.DummyUpdateProductDto);

            //Assert
            Assert.Null(actual);
        }
    }
}