using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.BillDTO
{
    public class BillCreateDTO
    {
    
        [Required]
        public double Price { get; set; }
        [Required]
        public Guid Order_Id { get; set; }
    
    }
}
