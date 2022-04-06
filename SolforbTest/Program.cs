using Microsoft.EntityFrameworkCore;
using SolforbTest.Infrastructure.Db;
using SolforbTest.Infrastructure.Services.Contracts;
using SolforbTest.Infrastructure.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SolforbDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Db"));

});

builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IProviderService, ProviderService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=Index}/{id?}");

app.Run();
