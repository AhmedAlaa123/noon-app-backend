using noone.DTO.categoryDTO;
using noone.Models;

namespace noone.Reposatories.CateegoryReposatory
{
    public interface ICategoryReposatory
    {
        bool Insert(AddcategoryDTO category);
        bool Delete(Guid Id);
        bool Update(Guid Id,AddcategoryDTO category);
        ListofCategoryDTO GetById(Guid Id);
        ICollection<ListofCategoryDTO> GetAll();
    }
}
