using Microsoft.EntityFrameworkCore;
using noone.Models;
<<<<<<< Updated upstream
=======
using noone.Reposatories;
using noone.Reposatories.SubCategoryReposatory;
using noone.Reposatories.CateegoryReposatory;
using noone.Reposatories.AuthenticationReposatory;
using noone.Reposatories.DeliverCompanyReposatory;

using System.Text;
using noone.Reposatories.CompanyReposatory;
using noone.Reposatories.ProductReposatory;
>>>>>>> Stashed changes

namespace noone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
<<<<<<< Updated upstream
=======
            //add cors policy
            builder.Services.AddCors(corsoptions =>
            {
                corsoptions.AddPolicy("Mypolicy", corsploicy =>
                {
                    corsploicy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            //  Register Category Reposatory
            builder.Services.AddScoped<IReposatory<Category>, CategoryReposatory>();
            // Register DeliverComponyReposatory
            builder.Services.AddScoped<IReposatory<DeliverCompany>, DeliverComponyReposatory>();
            builder.Services.AddScoped<IProductReposatory, ProductRepostory>();

            //add custom sevices
            builder.Services.AddScoped<IReposatory<SubCategory>, SubCategoryReposatory>();

            //add custom sevices
            builder.Services.AddScoped<IReposatory<Company>, ComponyReposatory>();


>>>>>>> Stashed changes

            // add dbcontext to service
            //get connection string
            string connectionString = builder.Configuration.GetConnectionString("Ahmed Alaa");
            builder.Services.AddDbContext<NoonEntities>(optionsBuilde =>
            {
                optionsBuilde.UseSqlServer(connectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}