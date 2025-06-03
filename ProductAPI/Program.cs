using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using ProductAPI.Data;
using ProductAPI.Services.Categories;
using ProductAPI.Services.Products;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddScoped<IProductAppService, ProductAppService>();
        builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<ProductDbContext>(option => option.UseSqlServer(
            builder.Configuration.GetConnectionString("ProductConnection")));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("ProductUI",
            b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Product UI",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        var app = builder.Build();
        app.UseCors("ProductUI");
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Client UI v1");
            c.RoutePrefix = "";
        });
        app.UseHttpsRedirection();
        app.UseDeveloperExceptionPage();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}