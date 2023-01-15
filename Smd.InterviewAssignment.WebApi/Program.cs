using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Smd.InterviewAssignment.WebApi.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;

namespace Smd.InterviewAssignment.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();
           using var scope = host.Services.CreateScope();
           var context = scope.ServiceProvider.GetRequiredService<BookContext>();
           var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
           try
           {
               context.Database.Migrate();
               DbInitializer.Initialize(context);
           }
           catch (Exception ex)
           {
                logger.LogError(ex, "Problem with migrating data");
           }

           host.Run();
        }    

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}