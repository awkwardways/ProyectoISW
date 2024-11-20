using Microsoft.EntityFrameworkCore;
using PROYECTOISW.Models;
using PROYECTOISW.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ProyectoiswContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MiDB"));
});
builder.Services.AddScoped<IServicioCorreo, ServicioCorreo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=IniciarSesion}/{id?}");

app.Run();
