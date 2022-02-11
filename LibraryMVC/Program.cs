

using LibraryMVC.BL;
using LibraryMVC.Data;
using LibraryMVC.Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<LibraryDbContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddScoped<IService<Book>, BooksService>();

builder.Services.AddScoped<IService<Author>, AuthorsService>();

builder.Services.AddScoped<IService<Member>, MembersService>();

builder.Services.AddScoped<CheckOutService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
