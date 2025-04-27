using GestionLigasDeportivas.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registro de servicios 
builder.Services.AddDbContext<LigaDeportivaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

// Habilitar las sesiones
builder.Services.AddDistributedMemoryCache();  // Usamos la memoria para almacenar sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Define el tiempo de inactividad para las sesiones
    options.Cookie.HttpOnly = true; // Solo accesible por HTTP
    options.Cookie.IsEssential = true; // Necesario para el funcionamiento de la app
});

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Usuario/Login"; // o donde tengas tu login
        options.AccessDeniedPath = "/Usuario/AccessDenied"; // si quieres una vista de acceso denegado
    });


var app = builder.Build();

// Configurar el middleware para usar las sesiones
app.UseSession();

//auth
app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
