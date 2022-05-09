using System.Collections.Generic;

namespace Shared.Clients
{
  public class ProductReadDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Prices { get; set; }
    public int AverageRate { get; set; }
    public string ThumbnailName { get; set; }
    public string ThumbnailUri { get; set; }
  }

  public class ProductDetailReadDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Prices { get; set; }
    public int AverageRate { get; set; }
    public IEnumerable<ImageReadDto> Images { get; set; }
  }

  public class ProductListReadDto
  {
    public IEnumerable<ProductReadDto> Products { get; set; }
    public int TotalPage { get; set; }
    public int CurrentPage { get; set; }
  }

  public class ProductRatingWriteDto
  {
    public int ProductId { get; set; }
    public int Stars { get; set; }
  }
}