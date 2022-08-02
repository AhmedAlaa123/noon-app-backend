using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace noone.Models
{
    public class NoonEntities: IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<DeliverCompany> DeliverCompanies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<RelatedSaledProduct> RelatedSaledProducts { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<UserProductRate> UserProductRates { get; set; }

        public NoonEntities()
        {
                
        }

        public NoonEntities(DbContextOptions options):base(options)
        {
                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // base.OnConfiguring(optionsBuilder);
            var builder = WebApplication.CreateBuilder();
            optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Ahmed Alaa"));
        }

    }
}
