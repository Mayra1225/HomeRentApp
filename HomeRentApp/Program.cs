using HomeRentApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HomeRentContextSQLServer>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HomeRentContextSQLServer") ?? throw new InvalidOperationException("Connection string 'HomeRentContextSQLServer' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // <-- HABILITAR sesiones

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // <-- ACTIVAR sesiones en el flujo

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
