using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonAssets.Data;
using PersonAssets.Interfaces;
using PersonAssets.Mapper;
using PersonAssets.Middlewares;
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
builder.Services.AddScoped<ICarRepository, CarRepository>();
//DI Middleware : IMiddleware
// builder.Services.AddTransient<CustomMiddleware>();

var app = builder.Build(); // Build
//Minimal APi
app.MapGet("/products/{id:int}", async (HttpContext context) =>
{
    var id = context.Request.RouteValues["id"];
    await context.Response.WriteAsync($"Get Hello {id}");
});
#region MyRegion

//Middleware => 1
// app.Use(async (HttpContext context,RequestDelegate next) =>
// {
//     context.Response.ContentType = "text/html";
//     context.Request.Headers["username"] = "ali"; // username : ali
//     await context.Response.WriteAsync("<h1>Hello Neda</h1>");
//     await next(context);
// });
// app.UseWhen(context => context.Request.Headers.ContainsKey("username"), applicationBuilder =>
// {
//     applicationBuilder.UseMyCustomMiddleware();
//     applicationBuilder.UseMyMiddleware();
// });
// // Middleware => 2
// app.Run(async (HttpContext context) =>
// {
//     await context.Response.WriteAsync("Hello Neda Again!"); //end point
// }); //End

#endregion

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

#region MyRegion

// app.UseRouting(); //access to get points
// //Map
// app.Map("map1", async (context) =>
// {
//     await context.Response.WriteAsync("map1 called");
// }).RequireAuthorization();
// //Maps
// app.UseEndpoints(endpoints =>
// {
//     //Map : Delete Put Get Post
//     //http://localhost:5254/files/sample.txt
//     endpoints.Map("files/{filename}.{extension}", async (context) =>
//     {
//         var filename = context.Request.RouteValues["filename"];
//         var extension = context.Request.RouteValues["extension"] as string;
//         await context.Response.WriteAsync($"In Files {filename} {extension}");
//     });
//     endpoints.Map("employee/profile/{id:int?}", async httpContext =>
//     {
//         var id = httpContext.Request.RouteValues["id"];
//         await httpContext.Response.WriteAsync("employee" + id);
//     });
// });
// //Middleware
// app.Use(async (context, next) =>
// {
//     var endPoint = context.GetEndpoint();
//     await context.Response.WriteAsync($"after : {endPoint?.DisplayName}");
//     await next(context);
// });
// //Run
// app.Run(async (HttpContext context) =>
// {
//     await context.Response.WriteAsync(""); //end point
// }); //End

#endregion
//TODO: View Components => Invoke

app.MapStaticAssets(); //Compress static files
//app.UseStaticFiles(); //wwwroot

app.UseAuthentication();
app.UseAuthorization(); //request

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

await app.RunAsync();