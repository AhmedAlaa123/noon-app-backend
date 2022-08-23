namespace noone.ApplicationDTO.ProductDTO
{
    public class ProductInfoDto
    {
       public Guid Id { get; set; }
        public string Name { get; set; }

         public string CompanyName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }

        public string ProductImage { get; set; }
        //public List<String> ProductImages { get; set; } = new List<string>();

    }
}
