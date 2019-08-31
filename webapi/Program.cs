using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using webapi.Data;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
          var host = CreateWebHostBuilder(args).Build();
          using (var scope = host.Services.CreateScope())
          {
              var Services =scope.ServiceProvider;
              try{
                  var context = Services.GetRequiredService<DataContext>();
                  context.Database.Migrate();
                  Semilla.SemillaUsuario(context);

              }catch(Exception ex)
              {
                  var logger = Services.GetRequiredService<ILogger<Program>>();
                  logger.LogError(ex , "Ocurrio un error durante la migracion");
              }
          }
          host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
