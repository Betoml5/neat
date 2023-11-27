using Microsoft.EntityFrameworkCore;
using neat.Models.Entities;
using neat.Repositories;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
string CadenaConexion = "server=localhost;user=root;password=root;database=neat";

builder.Services.AddDbContext<NeatContext>(
    optionBuilder => optionBuilder.UseMySql(CadenaConexion,
    ServerVersion.AutoDetect(CadenaConexion)));

builder.Services.AddTransient<Repository<Menu>>();
builder.Services.AddTransient<Repository<Clasificacion>>();
builder.Services.AddTransient<MenuRepository>();


var app = builder.Build();
app.UseStaticFiles();

// app.MapControllerRoute(
//     name: "areas",
//     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//     );
app.MapDefaultControllerRoute();
app.Run();
