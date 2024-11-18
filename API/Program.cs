
using API.Behaviors;
using API.Data.Db;
using API.Data.Repositories;
using API.Services.Control;
using API.Services.DataServices;
using API.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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

            ConfigureAuthentication(builder);
            ConfigureServices(builder);
            ConfigureSql(builder);
            ConfigureValidators(builder);

            var app = builder.Build();

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

        public static IServiceCollection ConfigureServices(WebApplicationBuilder builder)
        {
            return builder.Services.AddScoped<UserDataRepository>()
                .AddScoped<JwtService>()
                .AddScoped<PasswordHashingService>()
                .AddScoped<RoleDataService>()
                .AddScoped<UserDataService>();
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
                            ValidateIssuer = true,
                            ValidateAudience = true,
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
