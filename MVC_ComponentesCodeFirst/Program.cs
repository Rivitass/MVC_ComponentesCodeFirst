using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Data;
using MVC_ComponentesCodeFirst.Services;
using MVC_ComponentesCodeFirst.Services.Componentes;
using MVC_ComponentesCodeFirst.Services.Ordenadores;
using MVC_ComponentesCodeFirst.Services.Pedidos;
using NLog;

namespace MVC_ComponentesCodeFirst;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Logging
        LogManager.Setup().LoadConfigurationFromFile(configFile: string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpClient();

        // SQL Lite
        builder.Services.AddSqlServer<ComponenteContext>(builder.Configuration.GetConnectionString("connectionString"));            
        builder.Services.AddScoped<IComponenteRepository, ApiComponenteRepository>();
        builder.Services.AddScoped<IOrdenadorRepository, ApiOrdenadorRepository>();
        builder.Services.AddScoped<IPedidoRepository, ApiPedidoRepository>();

        builder.Services.AddScoped<FormattingService, FormattingService>();

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
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}