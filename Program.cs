using TimetableHelper.Components;
using Microsoft.EntityFrameworkCore;
using TimetableHelper.Data;
using Microsoft.AspNetCore.Identity;
using TimetableHelper.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<TimetableHelperContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TimetableHelperContext") ?? throw new InvalidOperationException("Connection string 'TimetableHelperContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();

builder.Services.AddScoped<TableData>();

builder.Services.AddRazorPages();
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true; 
    options.Password.RequireLowercase = false;      
    options.Password.RequireUppercase = false;      
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequiredLength = 6;           
})
    .AddEntityFrameworkStores<TimetableHelperContext>()
    .AddDefaultTokenProviders();

// Configure Cookie Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";  // Redirect to login if unauthorized
    options.AccessDeniedPath = "/login"; // Redirect if access denied
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthenticationStateProvider, TimetableAuthenticationStateProvider>();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
