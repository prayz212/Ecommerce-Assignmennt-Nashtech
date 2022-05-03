using System.Collections.Generic;
using BackEnd.Models;
using BackEnd.Models.ViewModels;
using Shared.Clients;

namespace UnitTest.Utils
{
    public static class MockData
    {
        //Category
        public static IList<CategoryDto> EmptyListCategoryDto = new List<CategoryDto>();
        public static IList<CategoryDto> DummyListCategoryDto = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "category 1", DisplayName = "display name" },
            new CategoryDto { Id = 2, Name = "category 2", DisplayName = "display name" },
            new CategoryDto { Id = 3, Name = "category 3", DisplayName = "display name" },
        };
        public static CategoryDetailDto NullCategoryDetailDto = null;
        public static CategoryDetailDto DummyCategoryDetailDto = new CategoryDetailDto
        {
            Id = 1,
            Name = "Dummy name",
            DisplayName = "Dummy display name",
            Description = "Dummy description"
        };
        public static CreateCategoryDto DummyCreateCategoryDto = new CreateCategoryDto
        {
            Name = "dummy name",
            DisplayName = "dummy display name",
            Description = "dummy description"
        };
        public static IList<Category> EmptyListCategory = new List<Category>();
        public static IList<Category> DummyListCategory = new List<Category>
        {
            new Category { Id = 1, Name = "category 1", DisplayName = "display name", Description = "description", IsDeleted = false},
            new Category { Id = 2, Name = "category 2", DisplayName = "display name", Description = "description", IsDeleted = false},
            new Category { Id = 3, Name = "category 3", DisplayName = "display name", Description = "description", IsDeleted = false},
        };
        public static Category NullCategory = null;
        public static Category DummyCategory = new Category
        {
            Id = 1,
            Name = "dummy name",
            DisplayName = "dummy display name",
            Description = "dummy description",
            IsDeleted = false,
        };
        public static IList<CategoryReadDto> EmptyListCategoryReadDto = new List<CategoryReadDto>();
        public static IList<CategoryReadDto> DummyListCategoryReadDto = new List<CategoryReadDto>
        {
            new CategoryReadDto { Id = 1, Name = "category 1", DisplayName = "display name", Description = "description" },
            new CategoryReadDto { Id = 2, Name = "category 2", DisplayName = "display name", Description = "description" },
            new CategoryReadDto { Id = 3, Name = "category 3", DisplayName = "display name", Description = "description" },
        };

        //Product
        public static IList<ProductReadDto> DummyListProductReadDto = new List<ProductReadDto>
        {
            new ProductReadDto() { Id = 1, Name = "product 1", Prices = 120000, AverageRate = 5, ThumbnailName = "image 1", ThumbnailUri = "uri 1" },
            new ProductReadDto() { Id = 2, Name = "product 2", Prices = 120000, AverageRate = 5, ThumbnailName = "image 2", ThumbnailUri = "uri 2" },
            new ProductReadDto() { Id = 3, Name = "product 3", Prices = 120000, AverageRate = 5, ThumbnailName = "image 3", ThumbnailUri = "uri 3" },
            new ProductReadDto() { Id = 4, Name = "product 4", Prices = 120000, AverageRate = 5, ThumbnailName = "image 4", ThumbnailUri = "uri 4" },
        };
        public static ProductListReadDto NullProductListReadDto = null;
        public static ProductListReadDto DummyProductListReadDto = new ProductListReadDto
        {
            Products = DummyListProductReadDto,
            CurrentPage = 1,
            TotalPage = 1
        };
        public static ProductDetailReadDto DummyProductDetailReadDto = new ProductDetailReadDto
        {
            Id = 1,
            Name = "product 1",
            Description = "description of product 1",
            AverageRate = 4,
            Prices = 120000,
            Images = new List<ImageReadDto>()
            {
                new ImageReadDto() { Name = "image 1", Uri = "uri 1" },
                new ImageReadDto() { Name = "image 2", Uri = "uri 2" },
            }
        };
        public static ProductDetailReadDto NullProductDetailReadDto = null;
        public static ProductRatingWriteDto IncorrectDummyProductRating = new ProductRatingWriteDto
        {
            ProductID = -1,
            Star = 4
        };
        public static ProductRatingWriteDto CorrectDummyProductRating = new ProductRatingWriteDto
        {
            ProductID = 1,
            Star = 4
        };
    }
}