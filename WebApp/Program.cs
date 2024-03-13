using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositoties;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<DataContext>();

builder.Services.AddAuthentication().AddFacebook(x =>
{
    x.AppId = "275363742281466";
    x.AppSecret = "6c8a21c909aff171b1fd9fd93857f013";
    x.Fields.Add("first_name");
    x.Fields.Add("last_name");
});

builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = "806272839932-ugmlohevjcgqcsa7cj6kkgucjtpeu663.apps.googleusercontent.com";
    options.ClientSecret = "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";
    options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
    options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
    options.SaveTokens = true;
});



builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<FeatureReposotory>();
builder.Services.AddScoped<FeatureItemRepository>();

builder.Services.AddScoped<FeatureService>();
builder.Services.AddScoped<AddressManager>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<AddressManager>();
//builder.Services.AddScoped<BasicManager>();


var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "defaultWithCatchAll",
    pattern: "{*url}",
    defaults: new { controller = "Default", action = "Index" });

app.Run();
