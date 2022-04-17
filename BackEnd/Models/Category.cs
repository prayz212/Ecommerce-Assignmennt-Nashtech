using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
  [Table("Category")]
  public class Category
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(150)]
    public string DisplayName { get; set; }

    [Required]
    [StringLength(1000)]
    public string Description { get; set; }

    public bool IsDeleted { get; set; }

    public ICollection<Product> Product { get; set; }
  }
}
