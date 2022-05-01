using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Controllers.Client;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Clients;
using UnitTest.Utils;
using Xunit;

namespace UnitTest.BackEndProject.Controllers.ClientSite.Category
{
    public class GetAllCategoriesShould
    {
        [Fact]
        public async Task ReturnOkWithValueWhenHavingData()
        {
            //Arrange
            var data = new List<CategoryReadDto>()
            {
                new CategoryReadDto() { id = 1, name = "TraiCayDaLat", displayName = "Trái cây Đà Lạt", description = "Trái cây được trồng tại nông sản sạch" },
                new CategoryReadDto() { id = 2, name = "TraiCayQuyNhon", displayName = "Trái cây Quy Nhơn", description = "Trái cây được trồng tại nông trại thuỷ phân" },
            };
            
            var mock = new Mock<ICategoryService>();
            mock.Setup(s => s.GetCategories()).ReturnsAsync(data);

            var categoryController = new CategoryController(mock.Object);

            //Act
            var result = await categoryController.GetAllCategories();
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(data, objectResult.Value);
        }

        [Fact]
        public async Task ReturnOkWithEmptyValueWhenNotHavingData()
        {
            //Arrange
            var data = new List<CategoryReadDto>();
            
            var mock = new Mock<ICategoryService>();
            mock.Setup(s => s.GetCategories()).ReturnsAsync(data);

            var categoryController = new CategoryController(mock.Object);

            //Act
            var result = await categoryController.GetAllCategories();
            var objectResult = result as OkObjectResult;

            //Assert
            Assert.Equal(ConstantVariable.OK_STATUS_CODE, objectResult.StatusCode);
            Assert.Equal(data, objectResult.Value);
        }
    }
}