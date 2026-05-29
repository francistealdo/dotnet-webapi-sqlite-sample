
using Microsoft.EntityFrameworkCore;
using ProductAPI.Application;
using ProductAPI.Application.Interface;
using ProductAPI.Application.Interface.Mapper;
using ProductAPI.Application.Mapper;
using ProductAPI.Domain.Core.Interface.Repository;
using ProductAPI.Domain.Core.Interface.Service;
using ProductAPI.Domain.Service;
using ProductAPI.Infrastructure.Data;
using ProductAPI.Infrastructure.Data.Repository;

namespace ProductAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API Product", Version = "v1" });
            });

            builder.Services.AddScoped<IProductApplicationService, ProductApplicationService>();
            builder.Services.AddScoped<ICategoryApplicationService, CategoryApplicationService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductMapper, ProductMapper>();
            builder.Services.AddScoped<ICategoryMapper, CategoryMapper>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Product");
                });
            }

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
