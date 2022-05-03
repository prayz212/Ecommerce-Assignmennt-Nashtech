using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Models.ViewModels;
using Shared.Clients;

namespace BackEnd.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> AdminGetCategories(int page, int size)
        {
            var categories = await _categoryRepository.GetCategories(page, size);
            var result = _mapper.Map<IEnumerable<CategoryDto>>(categories);
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
                ? _mapper.Map<CategoryDetailDto>(newCategory)
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
            var result = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            return result;
        }

        public async Task<CategoryDetailDto> GetCategory(int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            return category is null
                ? null
                : _mapper.Map<CategoryDetailDto>(category);
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
                ? _mapper.Map<CategoryDetailDto>(category)
                : null;
        }
    }
}