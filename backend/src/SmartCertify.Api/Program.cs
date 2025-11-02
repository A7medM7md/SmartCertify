
// This API Layer Depends On Other 3 Projects Only For Configuration Purpose
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SmartCertify.Infrastructure;

namespace SmartCertify.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<SmartCertifyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"),
                    providerOptions => providerOptions.EnableRetryOnFailure());
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi(); // from .net core 9, Microsoft goes with OpenAPI instead of Swagger

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi("/apenapi/v1.json"); // I Can Change The Path Of OpenApi JSON File

                // Scalar
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("My API");
                    options.WithTheme(ScalarTheme.DeepSpace);
                    options.WithSidebar(true);
                });

                // NSwag
                app.UseSwaggerUi(options =>
                {
                    options.DocumentPath = "/apenapi/v1.json";
                });

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
