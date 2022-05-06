using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Models.ViewModels;
using Shared.Clients;

namespace BackEnd.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;    
            _mapper = mapper;
        }

        public async Task<CategoryListDto> AdminGetCategories(int page, int size)
        {
            var count = await _unitOfWork.Categories.CountAll();
            var totalPage = this.GetTotalPage(count, size);

            //first condition: total page = -1 mean GetTotalPage function occurred error
            //second condition: count > 0 but total page less than passing page
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var rawCategories = await _unitOfWork.Categories.GetAll(page: page, size: size);
            var categories = _mapper.Map<IEnumerable<CategoryDto>>(rawCategories);

            return new CategoryListDto
            {
                Categories = categories,
                TotalPage = totalPage,
                CurrentPage = totalPage > 0 ? page : 0,
            };
        }

        public async Task<CategoryDetailDto> CreateCategory(CreateCategoryDto dto)
        {
            var newCategory = _mapper.Map<Category>(dto);
            var saveResult = await _unitOfWork.Categories.Add(newCategory);
            await _unitOfWork.SaveChangeAsync();

            return saveResult 
                ? _mapper.Map<CategoryDetailDto>(newCategory)
                : null;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _unitOfWork.Categories.GetById(id);
            if (category is null) return false;

            var deleteProducts = await _unitOfWork.Products.GetAll(filter: p => p.CategoryId == id);
            var deleteProductIds = deleteProducts.Select(p => p.Id);
            var deleteImages = await _unitOfWork.Images.GetAll(i => deleteProductIds.Contains(i.ProductId));
            var deleteRatings = await _unitOfWork.Ratings.GetAll(r => deleteProductIds.Contains(r.ProductID));

            var deleteResult = _unitOfWork.Categories.Delete(category) 
                && _unitOfWork.Products.DeleteRange(deleteProducts)
                && _unitOfWork.Images.DeleteRange(deleteImages)
                && _unitOfWork.Ratings.DeleteRange(deleteRatings);

            await _unitOfWork.SaveChangeAsync();
            return deleteResult;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetCategories()
        {
            var categories = await _unitOfWork.Categories.GetAll();
            var result = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            return result;
        }

        public async Task<CategoryDetailDto> GetCategory(int id)
        {
            var category = await _unitOfWork.Categories.GetById(id);
            return category is null
                ? null
                : _mapper.Map<CategoryDetailDto>(category);
        }

        public async Task<CategoryDetailDto> UpdateCategory(CategoryDetailDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            var updateResult = await _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangeAsync();

            return updateResult 
                ? _mapper.Map<CategoryDetailDto>(category)
                : null;
        }
    }
}