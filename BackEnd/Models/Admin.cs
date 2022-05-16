using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string Fullname { get; set; }
        public ApplicationUser Account { get; set; }
        public string AccountId { get; set; }
    }
}