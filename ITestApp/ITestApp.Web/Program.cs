using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ITestApp.Data;
using ITestApp.Data.DataSeed;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ITestApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            //using (var scope = host.Services.CreateScope())
            //{
            //    var serviceScope = scope.ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            //    var context = serviceScope.ServiceProvider.GetService<ITestAppDbContext>();
            //    context.Database.EnsureCreated();
            //    DataSeeder.SeedTests(context);
            //}

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
