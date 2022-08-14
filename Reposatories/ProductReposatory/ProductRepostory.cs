using Microsoft.EntityFrameworkCore;
using noone.ApplicationDTO.ProductDTO;
using noone.Helpers;
using noone.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace noone.Reposatories.ProductReposatory
{
    public class ProductRepostory: IProductReposatory
    {
        readonly NoonEntities context;
            public ProductRepostory(NoonEntities _context)
        {
            context= _context;
        }
      public bool Insert(PoductAddDto item)
        {
            if (item != null)
            {
                Product product=new Product();
                product.Name=item.Name;
                product.Description=item.Description;
                product.Price = item.Price;
                JwtSecurityToken token =TokenConverter.ConvertToken(@"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJFYnJhaGVtIiwianRpIjoiMTVkYmFhODUtMGY0MS00YjU2LTg2NTctNjU2OGUzOTQyMTk2IiwiZW1haWwiOiJlZUBnbS5jb20iLCJyb2xlcyI6IlVTRVIiLCJleHAiOjE2NjI1OTUzODAsImlzcyI6Ik5vb25TZWN1cmVBcGkiLCJhdWQiOiJOb29uU2VjdXJlQXBpVXNlciJ9.MDGt7Jcnz79dR8fz_sU3NzMBq3Z2Xhu_cEEKV36tvSU");
               
                product.UserId = context.Users.FirstOrDefault(c => c.UserName == username).Id;
                product.CompanyId = context.Companies.FirstOrDefault(c => c.Name == item.CompanyName).Id;
                product.Category_Id = context.Categories.FirstOrDefault(c => c.Name == item.CategoryName).Id;
                product.SucCategory_Id = context.SubCategories.FirstOrDefault(s => s.Name == item.SupCategoryName).Id;
            
                context.Products.Add(product);
                context.SaveChanges();
                Product productAddImage = context.Products.FirstOrDefault(p => p.Name == item.Name);
                foreach (string img in item.ProductImages)
                    productAddImage.ProductImages.Add(new ProductImage() { Image = img, Product_Id = productAddImage.Id });
                context.SaveChanges();
                return true;
            }
            return false;
        }
       public bool Delete(Guid Id)
        {
            Product oldproduct= context.Products.FirstOrDefault(p => p.Id == Id);
            if (oldproduct != null)
            {
                context.Products.Remove(oldproduct);
                context.SaveChanges();
                return true;
            }
            return false;
        }
      public bool Update(Guid Id, PoductAddDto Item)
        {
            if (Item != null)
            {
                Product oldproduct =  context.Products.FirstOrDefault(c => c.Id == Id);
                if (oldproduct != null)
                {
                    oldproduct.Name = Item.Name;
                    oldproduct.Description = Item.Description;
                
                    oldproduct.Price = Item.Price;
                    oldproduct.CompanyId = context.Companies.FirstOrDefault(c => c.Name == Item.CompanyName).Id;
                    oldproduct.SucCategory_Id = context.SubCategories.FirstOrDefault(c => c.Name == Item.SupCategoryName).Id;
                    oldproduct.Category_Id = context.Categories.FirstOrDefault(c => c.Name == Item.CategoryName).Id;

                    oldproduct.ProductImages=new List<ProductImage>();
                    context.SaveChanges();
                    foreach (var img in Item.ProductImages)
                        oldproduct.ProductImages.Add(new ProductImage() { Image = img, Product_Id = Id });
                    context.SaveChanges();
                    return true;
                }
               
            }
            
                return false;
            

        }
       
    public ProductInfoDto GetById(Guid Id)
        {
            ProductInfoDto temp = new ProductInfoDto();
            Product product = context.Products.Include(p => p.Company).Include(p => p.ProductImages).FirstOrDefault(p => p.Id == Id);
            temp.Id = product.Id;
            temp.Name = product.Name;
            temp.Description = product.Description;
            temp.Price = product.Price;
            temp.CompanyName = context.Companies.FirstOrDefault(c => c.Id == product.CompanyId).Name;

            foreach (ProductImage img in product.ProductImages)
            {
                temp.ProductImages.Add(img.Image);
            }
            return temp;

        }
        public ICollection<ProductInfoDto> GetAll()
        {
            List<Product> products = context.Products.Include(c => c.Company).Include(c => c.ProductImages).ToList();
           List<ProductInfoDto> productDto = new List<ProductInfoDto>();
            foreach(Product product in products)
            {
                ProductInfoDto temp = new ProductInfoDto();
                temp.Id=product.Id;
                temp.Name=product.Name;
                temp.Description = product.Description;
                temp.Price = product.Price;
                temp.CompanyName = context.Companies.FirstOrDefault(c => c.Id == product.CompanyId).Name;

                foreach (ProductImage img in product.ProductImages)
                {
                    temp.ProductImages.Add(img.Image);
                }
                productDto.Add(temp);
            }
            return productDto;
        }
    }
}
