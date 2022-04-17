using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
  [Table("Product")]
  public class Product
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Description { get; set; }
    
    [Required]
    public int Prices { get; set; }
    
    public bool IsFeatured { get; set; } = false;

    [Required]
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime UpdatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public Category Category { get; set; }
    public int CategoryId { get; set; }

    public ICollection<Rating> Ratings { get; set; }

    public ICollection<Image> Images { get; set; }
  }
}