using Movie.Extensions;
using Movie.Options;
using Movie.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IRecentMoiveStorage, RecentMoiveStorage>(); 
builder.Services.AddControllersWithViews();   

builder.Services.AddMovieService(options =>
{
    options.ApiKey = builder.Configuration["ConnectionStrings:ApiKey"];
    options.BaseUrl = builder.Configuration["ConnectionStrings:BaseUrl"];
});

builder.Services.AddHttpClient();
 
var app = builder.Build();
 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
