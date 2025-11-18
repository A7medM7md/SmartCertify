
// This API Layer Depends On Other 3 Projects Only For Configuration Purpose
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SmartCertify.Api.Filters;
using SmartCertify.Application;
using SmartCertify.Application.DTOValidations.Courses;
using SmartCertify.Application.Interfaces.Courses;
using SmartCertify.Application.Interfaces.QuestionsChoice;
using SmartCertify.Application.Interfaces.QuestionsChoises;
using SmartCertify.Application.Services;
using SmartCertify.Application.Services.Courses;
using SmartCertify.Application.Services.Questions;
using SmartCertify.Infrastructure;
using SmartCertify.Infrastructure.Repositories;

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


            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();

            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi(); // from .net core 9, Microsoft goes with OpenAPI instead of Swagger

            // configure automapper
            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            // configure validator
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCourseValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateCourseValidator>();

            // configure app services
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            builder.Services.AddScoped<IChoiceRepository, ChoiceRepository>();
            builder.Services.AddScoped<IChoiceService, ChoiceService>();

            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IQuestionService, QuestionService>();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
                });
            });


            var app = builder.Build();

            app.UseCors("default");

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi("/apenapi/v1.json"); // I Can Change The Path Of OpenApi JSON File
                //app.MapOpenApi();

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
