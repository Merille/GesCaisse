using EasytransitCaisse.Data;
using EasytransitCaisse.Services;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
builder.Services.AddScoped<AuthService>();

// MVC
builder.Services.AddControllersWithViews();

// Connexion SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


app.UseStaticFiles();

RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");


// Seeder DB
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(context);
}

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

RotativaConfiguration.Setup(
    builder.Environment.WebRootPath,
    "Rotativa"
);

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();