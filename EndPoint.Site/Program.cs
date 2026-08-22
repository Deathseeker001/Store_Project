using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Store_Project.Persistence.Context;
using Store_Project_Application.Interfaces.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();

string contectionString = @"Data Source= GHAZALEH\SQLEXPRESS; Initial Catalog=Store_ProjectDb; Integrated Security=True; TrustServerCertificate=True;";
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(contectionString)
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
  name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
