using Microsoft.EntityFrameworkCore;
using noone.Models;
using noone.Reposatories;
using noone.Reposatories.SubCategoryReposatory;

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
            //add custom sevices
            builder.Services.AddScoped<IReposatory<SubCategory>, SubCategoryReposatory>();
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