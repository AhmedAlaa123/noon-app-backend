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
        public List<String> ProductImages { get; set; } = new List<string>();
    

    }
}
