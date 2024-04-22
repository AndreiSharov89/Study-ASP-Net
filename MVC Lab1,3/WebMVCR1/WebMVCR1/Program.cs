using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(
    name:"defailt",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();