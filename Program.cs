using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LeiChenMidTermTest.Data;
using MidTest.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MidTerm8945274Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MidTerm8945274Context") ?? throw new InvalidOperationException("Connection string 'MidTerm8945274Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<IBook, Librarian>();


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
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
