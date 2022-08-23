using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.ProductDTO
{
    public class PoductAddDto
    {
        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
        public string Description { get; set; }
       
        public Guid CompanyId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SupCategoryId { get; set; }
        public IFormFile ProductImage { get; set; }
    
      
    }
}
