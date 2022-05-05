using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
  [Table("Image")]
  public class Image
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(1000)]
    public string Uri { get; set; }

    public string Format { get; set; }
    public long Width { get; set; }
    public long Height { get; set; }
    public long Size { get; set; }

    public Product Product { get; set; }
    public int ProductId { get; set; }

    public bool IsDeleted { get; set; }
  }
}
