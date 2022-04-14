namespace ViewModels.Clients
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

  public class ProductCreateDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Prices { get; set; }
    public string Description { get; set; }
  }
}