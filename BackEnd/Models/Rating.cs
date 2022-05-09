using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    [Table("Rating")]
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5)]
        public int Stars { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string UserId { get; set;}
        public ApplicationUser User { get; set; }

        public bool IsDeleted { get; set; }
    }
}
