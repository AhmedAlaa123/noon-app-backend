using Microsoft.AspNetCore.Hosting;
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
        readonly IWebHostEnvironment webHostEnvironment;
            public ProductRepostory(NoonEntities _context, IWebHostEnvironment _webHostEnvironment)
        {
            context= _context;
            webHostEnvironment = _webHostEnvironment;
        }
        public async void uploadImage(IFormFile image,Guid imageId)
        {
            string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images", "ProductsImages");
            string imageName = imageId.ToString() + "_" + image.FileName;
            string filePath = Path.Combine(uploadFolder, imageName);
            using(var fileStream=new FileStream(filePath, FileMode.Create))
            {
               await image.CopyToAsync(fileStream);
                fileStream.Close();
            }
        }
      public bool Insert(PoductAddDto item,string token)
        {
            if (item != null)
            {
                Product product=new Product();
                product.Name=item.Name;
                product.Description=item.Description;
                product.Price = item.Price;
                var tokenData =TokenConverter.ConvertToken(token);

                //product.CompanyId = context.Companies.FirstOrDefault(c => c.Name == item.CompanyName).Id;
                //product.Category_Id = context.Categories.FirstOrDefault(c => c.Name == item.CategoryName).Id;
                //product.SucCategory_Id = context.SubCategories.FirstOrDefault(s => s.Name == item.SupCategoryName).Id;

                product.UserId = context.Users.FirstOrDefault(c => c.UserName == tokenData.Subject).Id;
                product.CompanyId = item.CompanyId;
                product.Category_Id = item.CategoryId;
                product.SucCategory_Id = item.SupCategoryId;
                
                Guid image_id = Guid.NewGuid();
       
                uploadImage(item.ProductImage, image_id);
                product.Image = image_id.ToString() + "_" + item.ProductImage.FileName;
       
                context.Products.Add(product);
                context.SaveChanges();
                
                //Product productAddImage = context.Products.FirstOrDefault(p => p.Name == item.Name);
                //foreach (var img in item.ProductImages)
                //{
                 
                //    productAddImage.ProductImages.Add(new ProductImage() { Image = image_id.ToString()+"_"+img.FileName, Product_Id = productAddImage.Id,Id=image_id });
                
                //}  context.SaveChanges();
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
                    //oldproduct.CompanyId = context.Companies.FirstOrDefault(c => c.Name == Item.CompanyName).Id;
                    //oldproduct.SucCategory_Id = context.SubCategories.FirstOrDefault(c => c.Name == Item.SupCategoryName).Id;
                    //oldproduct.Category_Id = context.Categories.FirstOrDefault(c => c.Name == Item.CategoryName).Id;
                    oldproduct.CompanyId = Item.CompanyId;
                    oldproduct.SucCategory_Id = Item.SupCategoryId;
                    oldproduct.Category_Id = Item.CategoryId;
                    Guid image_id = Guid.NewGuid();
                    oldproduct.Image = image_id.ToString() + "_" + Item.ProductImage.FileName;
                    uploadImage(Item.ProductImage, image_id);
                    //foreach (var img in oldproduct.ProductImages)
                    //{
                    //    context.ProductImages.Remove(img);

                    //}
                    context.SaveChanges();
                    //foreach (var img in Item.ProductImages)
                    //{
                    //    Guid image_id = Guid.NewGuid();
                    //    uploadImage(img, image_id);
                    //    oldproduct.ProductImages.Add(new ProductImage() { Image = image_id.ToString()+"_"+img.FileName, Product_Id = Id,Id=image_id });
                       
                    //}
                    //context.SaveChanges();
                    return true;
                }
               
            }
            
                return false;
            

        }
       
    public ProductInfoDto GetById(Guid Id)
        {
            ProductInfoDto temp = new ProductInfoDto();
            Product product = context.Products.Include(p => p.Company).FirstOrDefault(p => p.Id == Id);
            temp.Id = product.Id;
            temp.Name = product.Name;
            temp.Description = product.Description;
            temp.Price = product.Price;
            temp.CompanyName = context.Companies.FirstOrDefault(c => c.Id == product.CompanyId).Name;
            temp.CategoryId = product.Category_Id;
            temp.SubCategoryId = product.SucCategory_Id;
            temp.ProductImage = product.Image;

            //foreach (ProductImage img in product.ProductImages)
            //{
            //    temp.ProductImages.Add(img.Image);
            //}
            return temp;

        }
        public ICollection<ProductInfoDto> GetAll()
        {
            List<Product> products = context.Products.Include(c => c.Company).ToList();
           List<ProductInfoDto> productDto = new List<ProductInfoDto>();
            foreach(Product product in products)
            {
                ProductInfoDto temp = new ProductInfoDto();
                temp.Id=product.Id;
                temp.Name=product.Name;
                temp.Description = product.Description;
                temp.Price = product.Price;
                temp.ProductImage = product.Image;
                //temp.CompanyName = context.Companies.FirstOrDefault(c => c.Id == product.CompanyId).Name;
                temp.CompanyName = product.Company.Name;
                temp.CategoryId = product.Category_Id;
                temp.SubCategoryId = product.SucCategory_Id;


                //foreach (ProductImage img in product.ProductImages)
                //{
                //    temp.ProductImages.Add(img.Image);
                //}
                productDto.Add(temp);
            }
            return productDto;
        }
    }
}
