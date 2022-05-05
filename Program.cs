using CRUD_BankTransactions.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
//DI for the Dbcontext

builder.Services.AddDbContext<TransactionDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));
//dipendency injection so that this instance is used on everywhere wherever required like in the constructor of controller

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transaction}/{action=Index}/{id?}");//changing default route to the transactions

app.Run();
