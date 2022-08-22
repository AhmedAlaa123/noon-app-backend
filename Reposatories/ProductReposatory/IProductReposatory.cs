using noone.ApplicationDTO.ProductDTO;

namespace noone.Reposatories.ProductReposatory
{
    public interface IProductReposatory
    {
         bool Insert(PoductAddDto item,string token);
        bool Delete(Guid Id);
        bool Update(Guid Id, PoductAddDto Item);
        ProductInfoDto GetById(Guid Id);
        ICollection<ProductInfoDto> GetAll();
    }
}
