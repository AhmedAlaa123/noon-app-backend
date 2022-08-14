using noone.ApplicationDTO.SubCategoryDto;

namespace noone.ApplicationDTO.categoryDTO
{
    public class CategoryInfoDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SubCategoryInfoDTO> SubCategories { get; set; }
    }
}
