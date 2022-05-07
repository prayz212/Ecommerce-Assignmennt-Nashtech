using System.Collections.Generic;

namespace BackEnd.Models.ViewModels
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class CategoryDetailDto : CategoryDto
    {
        public string Description { get; set; }
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }

    public class CategoryListDto {
        public IEnumerable<CategoryDto> Categories { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}