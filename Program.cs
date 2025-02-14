using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonAssets.Data;
using PersonAssets.Interfaces;
using PersonAssets.Mapper;
using PersonAssets.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // Handle 
//configuration of Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireDigit = true; //1525458
        options.Password.RequireLowercase = false;
    }
).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews(); //MVC
builder.Services.AddAutoMapper(typeof(ProfileMapper)); //AutoMapper
builder.Services.AddScoped<IPersonRepository, PersonRepository>(); //Dependency Injection

var app = builder.Build(); // Build

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // Home = Controller 
    // Error = Action
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

//TODO : will check
// area/controller/action/parameter
app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

app.Run();