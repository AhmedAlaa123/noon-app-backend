using noone.ApplicationDTO.ProductDTO;

namespace noone.ApplicationDTO.SubCategoryDTO
{
    public class SubCategoryDetailsDTO
    {
        public Guid SubCategoryId { get; set; }
   
        public string SubCategoryName { get; set; }
       
        public string SubCategoryImage { get; set; }
        public List<ProductInfoDto> Products { get; set; }
    }
}
