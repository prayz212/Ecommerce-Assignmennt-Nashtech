using System.Collections.Generic;

namespace Shared.Clients
{
  public class ProductReadDto
  {
    public int id { get; set; }
    public string name { get; set; }
    public int prices { get; set; }
    public double averageRate { get; set; }
    public string thumbnailName { get; set; }
    public string thumbnailUri { get; set; }
  }

  public class ProductDetailReadDto
  {
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int prices { get; set; }
    public double averageRate { get; set; }
    public IList<ImageReadDto> images { get; set; }
  }

  public class ProductListReadDto
  {
    public IList<ProductReadDto> products { get; set; }
    public int totalPage { get; set; }
    public int currentPage { get; set; }
  }
}