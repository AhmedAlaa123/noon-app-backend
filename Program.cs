using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using noone.Helpers;
using noone.Models;
<<<<<<< HEAD
=======
<<<<<<< Updated upstream
=======
>>>>>>> ProductController
using noone.Reposatories;
using noone.Reposatories.SubCategoryReposatory;
using noone.Reposatories.CateegoryReposatory;
using noone.Reposatories.AuthenticationReposatory;
using noone.Reposatories.DeliverCompanyReposatory;

using System.Text;
<<<<<<< HEAD
using noone.Reposatories.OrderReposatory;
using noone.Reposatories.BillReposatory;
=======
using noone.Reposatories.CompanyReposatory;
using noone.Reposatories.ProductReposatory;
>>>>>>> Stashed changes
>>>>>>> ProductController

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

            //  Register Category Reposatory
            builder.Services.AddScoped<IReposatory<Category>, CategoryReposatory>();
            // Register DeliverComponyReposatory
            builder.Services.AddScoped<IReposatory<DeliverCompany>, DeliverComponyReposatory>();
            //add custom sevices
            builder.Services.AddScoped<IReposatory<SubCategory>, SubCategoryReposatory>();

            // Register OrderRposatory
            builder.Services.AddScoped<IReposatory<Order>, OrderReposatory>();
            // Register BillReposatory
            builder.Services.AddScoped<IReposatory<Bill>, BillReposatory>();

            // add dbcontext to service


            //configer JWT

            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            //add Identity User
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<NoonEntities>();
            // add dbcontext to service--

            //get connection string
            string connectionString = builder.Configuration.GetConnectionString("Ahmed Alaa");
            builder.Services.AddDbContext<NoonEntities>(optionsBuilde =>
            {
                optionsBuilde.UseSqlServer(connectionString);
            });

            // Register IAuthenticationReposatory
            builder.Services.AddScoped<IAuthenticationReposatory, AuthenticationReposatory>();

            // Add Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(o =>
              {
                  o.RequireHttpsMetadata = false;
                  o.SaveToken = false;
                  o.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidIssuer = builder.Configuration["JWT:Issuer"],
                      ValidAudience = builder.Configuration["JWT:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                  };
              });

            // configer core policy
            
            builder.Services.AddCors(corsOptions =>
            {
                
                corsOptions.AddPolicy("NoonPolicy", corsPolicyBuilder =>
                {
                    corsPolicyBuilder.WithOrigins("*").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("NoonPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            
            
            app.MapControllers();

            app.Run();
        }
    }
}