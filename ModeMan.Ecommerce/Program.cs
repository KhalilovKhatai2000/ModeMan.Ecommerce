using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Data;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.IOptions;
using ModeMan.Ecommerce.Services.Abstract;
using ModeMan.Ecommerce.Services.Concreete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings")
    );

builder.Services.AddDbContext<ModeManDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequiredLength = 6;

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
    options.Lockout.AllowedForNewUsers = true;
})
    .AddEntityFrameworkStores<ModeManDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", options =>
    {
        options.RequireRole("Admin");
    });
});

builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses()
    .AsImplementedInterfaces()
    .WithScopedLifetime());

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
    name: "shop",
    pattern: "shop/{id?}",
    defaults: new { controller = "Product", action = "Index"});

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
