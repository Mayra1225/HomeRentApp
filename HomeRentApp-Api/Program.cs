using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HomeRentApp_Api.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace HomeRentApp_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<HomeRentApp_ApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("HomeRentApp_ApiContext") ?? throw new InvalidOperationException("Connection string 'HomeRentApp_ApiContext' not found.")));

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeRent API", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.MapControllers();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}
