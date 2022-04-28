namespace BackEnd.Models.ViewModels
{
    public class CategoryDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
    }

    public class CategoryDetailDto : CategoryDto
    {
        public string description { get; set; }
    }

    public class CreateCategoryDto
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
    }
}