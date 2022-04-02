using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmFresh.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FarmFresh
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new Context();

            ContextSeeder.Seed(context);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
