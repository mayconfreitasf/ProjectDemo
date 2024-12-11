
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using WebCrudAdvanced.Endpoints;
using WebCrudAdvanced.Models.MProduct;
using WebCrudAdvanced.Models.MUser;
using WebCrudAdvanced.Repositories.RProduct;
using WebCrudAdvanced.Repositories.RUser;
using WebCrudAdvanced.Services;
using WebCrudAdvanced.Services.SProduct;
using WebCrudAdvanced.Services.SUser;
using WebCrudAdvanced.Repositories.ROrder;
using WebCrudAdvanced.Services.SOrder;
using WebCrudAdvanced.Models.MOrder;
using WebCrudAdvanced.Models;


/***************************
 * Dear recruiter not all the resources were necessary to solve the problems. 
 * However, I developed this project specifically for the selection process, 
 * so I focused my efforts on demonstrating the manipulation of patterns and advanced features, 
 * rather than building multiple screens that would be limited to CRUD operations.
 * 
 * 
 * Simple demonstration project applying the following concepts, design patterns, and tools.
        SOLID
        Repository
        Singleton
        Dependency Injection
        Multithreading / Task Parallel
        Semaphore
        Garbage Collection - manual manipulation
        Minimal API
        Authentication JWT
        Frontend React
****************************/

namespace WebCrudAdvanced
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<User>();
            builder.Services.AddScoped<Product>();
            builder.Services.AddScoped<Order>();
            builder.Services.AddScoped<JwtSettings>();
            builder.Services.AddScoped<JwtService>();
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<OrderRepository>(); 
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            //*******************************   JWT  *******************************
            // Config de JWT Bearer
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.
                            SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                    };
                });
            //*******************************   JWT  *******************************

            var app = builder.Build();
            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapUserEndpoints();
            app.MapProductEndpoints();
            

            app.UseHttpsRedirection();
            app.UseAuthentication(); 
            app.UseAuthorization();


            app.Run();
        }
    }
}