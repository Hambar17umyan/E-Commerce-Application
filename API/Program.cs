using API.Behaviors;
using API.Controllers;
using API.Data.Db;
using API.Data.Repositories.Concrete;
using API.Data.Repositories.Interfaces;
using API.Models.Domain;
using API.RequestHandlers;
using API.Services.Concrete.Control;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using API.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            ConfigureSql(builder); //chotki
            ConfigureAuthentication(builder); //chotki
            ConfigureMediatR(builder);
            ConfigureServices(builder);
            ConfigureValidators(builder);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static IServiceCollection ConfigureServices(WebApplicationBuilder builder)
        {
            return builder.Services.AddScoped<InventoryDataService>()
                .AddScoped<IOrderDataService, OrderDataService>()
                .AddScoped<IProductDataService, ProductDataService>()
                .AddScoped<IRoleDataService, RoleDataService>()
                .AddScoped<IUserDataService, UserDataService>()
                .AddScoped<IInventoryDataRepository, InventoryDataRepository>()
                .AddScoped<IOrderDataRepository, OrderDataRepository>()
                .AddScoped<IProductDataRepository, ProductDataRepository>()
                .AddScoped<IRoleDataRepository, RoleDataRepository>()
                .AddScoped<IUserDataRepository, UserDataRepository>()
                .AddScoped<IJwtService, JwtService>()
                .AddScoped<IPasswordHashingService, PasswordHashingService>();
        }
        public static IServiceCollection ConfigureMediatR(WebApplicationBuilder builder)
        {
            return builder.Services.AddMediatR(typeof(RegistrationRequestHandler).Assembly);
        }
        public static IServiceCollection ConfigureSql(WebApplicationBuilder builder)
        {
            return builder.Services.
                AddSqlServer<ECommerceDbContext>(builder.Configuration.GetConnectionString("Default Connection"));
        }
        public static void ConfigureValidators(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            builder.Services.AddValidatorsFromAssemblyContaining<RegistrationModelValidator>();
        }
        public static AuthenticationBuilder ConfigureAuthentication(WebApplicationBuilder builder)
        {
            return builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            RoleClaimType = ClaimTypes.Role,
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["JWT:Audiance"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                        };
                    });
        }
    }
}
