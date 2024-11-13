
using E_Commerce.API.Data.Db;
using E_Commerce.API.Validators;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using E_Commerce.API.Models.RequestModels;
using E_Commerce.API.Services;
using E_Commerce.API.Data.Repositories;

namespace E_Commerce.API
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

            builder.Services.AddSqlServer<ECommerceDbContext>(builder.Configuration.GetConnectionString("Default Connection"));

            ConfigureServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<RegistrationModelValidator>();
            services.AddScoped<PasswordHashingService>();
            services.AddScoped<RoleManagementService>();
            services.AddScoped<UserDataRepository>();
        }
    }
}
