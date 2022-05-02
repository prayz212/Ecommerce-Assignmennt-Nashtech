using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Models.ViewModels;
using Shared.Clients;

namespace BackEnd.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> AdminGetCategories(int page, int size)
        {
            var categories = await _categoryRepository.GetCategories(page, size);
            var result = new List<CategoryDto>();

            if (categories is not null && categories.Count > 0) 
            {
                foreach(Category category in categories)
                {
                    var element = new CategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name,
                        DisplayName = category.DisplayName
                    };

                    result.Add(element);
                }
            }

            return result;
        }

        public async Task<CategoryDetailDto> CreateCategory(CreateCategoryDto dto)
        {
            var newCategory = new Category
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                Description = dto.Description,
            };

            var saveResult = await _categoryRepository.NewCategory(newCategory);
            return saveResult 
                ? new CategoryDetailDto
                    {
                        Id = newCategory.Id,
                        Name = newCategory.Name,
                        DisplayName = newCategory.DisplayName,
                        Description = newCategory.Description,
                    }
                : null;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            if (category is null) return false;

            category.IsDeleted = true;

            return await _categoryRepository.UpdateCategory(category);
        }

        public async Task<IEnumerable<CategoryReadDto>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            var result = new List<CategoryReadDto>();

            if (categories is not null && categories.Count > 0) 
            {
                foreach(Category category in categories)
                {
                    var element = new CategoryReadDto
                    {
                        Id = category.Id,
                        Name = category.Name,
                        DisplayName = category.DisplayName,
                        Description = category.Description
                    };

                    result.Add(element);
                }
            }

            return result;
        }

        public async Task<CategoryDetailDto> GetCategory(int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            return category is null
                ? null
                : new CategoryDetailDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    DisplayName = category.DisplayName,
                    Description = category.Description
                };
        }

        public async Task<CategoryDetailDto> UpdateCategory(CategoryDetailDto dto)
        {
            Category category = await _categoryRepository.GetCategory(dto.Id);
            if (category is null) return null;

            category.Name = dto.Name;
            category.DisplayName = dto.DisplayName;
            category.Description = dto.Description;

            var updateResult = await _categoryRepository.UpdateCategory(category);
            return updateResult 
                ? new CategoryDetailDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    DisplayName = category.DisplayName,
                    Description = category.Description
                }
                : null;
        }
    }
}