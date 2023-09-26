using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;

using IT.DigitalCompany.Data;
using IT.DigitalCompany.Models;
using IT.DigitalCompany.Infrastructure;
using System.Collections.ObjectModel;
using IT.DigitalCompany.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityModel;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {        
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddIdentityServer(options =>
{    
})
    .AddApiAuthorization<ApplicationUser, AppIdentityDbContext>(options =>
    {           
        var clients = options.Clients;
        foreach(var c in clients)
        {
            //c.AlwaysIncludeUserClaimsInIdToken = true;
        }
        var ir = options.IdentityResources;        
        var apiResources = options.ApiResources;
        
        foreach(var resource in apiResources)
        {
            resource.UserClaims = (resource.UserClaims?? Enumerable.Empty<String>())
            .Union(IdentityExtensions.GettWellknowUserClaims()).ToArray();
            
           
        }
        var apiScopes = options.ApiScopes;
        foreach(var scope in apiScopes)
        {
            scope.UserClaims = (scope.UserClaims?? Enumerable.Empty<String>())
            .Union(IdentityExtensions.GettWellknowUserClaims()).ToArray();
        }        
    });

builder.Services.AddDbContext<CompanyRegistrationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<CompanyRegistrationManager>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();
builder.Services.Configure<JwtBearerOptions>(options =>
{
    options.TokenValidationParameters.NameClaimType = JwtClaimTypes.Name;
    options.TokenValidationParameters.RoleClaimType = JwtClaimTypes.Role;
});

builder.Services.Configure<JwtBearerOptions>(IdentityServerJwtConstants.IdentityServerJwtBearerScheme, 
    options =>
{
    options.MapInboundClaims = false;
    options.ClaimsIssuer = JwtClaimTypes.Issuer;
    options.TokenValidationParameters.NameClaimType = JwtClaimTypes.Name;
    options.TokenValidationParameters.RoleClaimType = JwtClaimTypes.Role;
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});


app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.Run();
