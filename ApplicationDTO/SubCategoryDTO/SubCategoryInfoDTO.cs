using System.ComponentModel.DataAnnotations;

namespace noone.ApplicationDTO.SubCategoryDto
{
    public class SubCategoryInfoDTO
    {
        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryImage { get; set; }
    }
}
