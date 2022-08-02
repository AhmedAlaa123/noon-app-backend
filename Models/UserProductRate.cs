using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noone.Models
{
    [Table(nameof(UserProductRate))]
    public class UserProductRate
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int? Rate { get; set; }
        public string? comment { get; set; }

        [ForeignKey("Product")]
        public Guid Product_Id { get; set; }
        public Product Product { get; set; }

        [ForeignKey("User")]
        public string User_Id { get; set; }
        public ApplicationUser User { get; set; }

    }
}
