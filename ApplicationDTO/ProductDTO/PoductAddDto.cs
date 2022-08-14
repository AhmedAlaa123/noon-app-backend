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
       
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
        public string SupCategoryName { get; set; }
        public List<IFormFile> ProductImages { get; set; } = new List<IFormFile>();
    
      public  string token { get; set; }
    }
}
