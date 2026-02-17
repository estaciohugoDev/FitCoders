using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FitCoders.Infrastructure.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string [] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            string apiPath;

            if(currentDirectory.Contains("WebApi"))
            {
                apiPath = currentDirectory;
            }
            else if (currentDirectory.Contains("Infrastructure"))
            {
                apiPath = Path.Combine(currentDirectory, "../FitCoders.WebApi");
            }
            else
            {
                apiPath = FindWebApiPath(currentDirectory);
            }

            var appSettingsPath = Path.Combine(apiPath, "appsettings.json");
            var appSettingsPathDev = Path.Combine(apiPath, "appSettings.Development.json");

            Console.WriteLine($"📁 Searching appsettings at: {apiPath}");
            Console.WriteLine($"📄 AppSettings exists: {File.Exists(appSettingsPath)}");
            Console.WriteLine($"📄 AppSettings.Dev exists: {File.Exists(appSettingsPathDev)}");

            var config = new ConfigurationBuilder().SetBasePath(apiPath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.Development.json", optional: true) 
                .Build();

            var connString = config.GetConnectionString("mysql");

            if(string.IsNullOrEmpty(connString))
                throw new InvalidOperationException("MySql connection string is empty at appsettings.json!");
            
            Console.WriteLine($"Connection string found: {connString.Split(';')[0]}...");
            
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseMySql(connString,
                                    ServerVersion.AutoDetect(connString),
                                    options => options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            
            return new ApplicationDbContext(optionsBuilder.Options);
        }

        private static string FindWebApiPath(string path)
        {
            var directory = new DirectoryInfo(path);

            while(directory is not null)
            {
                var apiDir = Path.Combine(directory.FullName, "src/FitCoders.WebApi");

                if(Directory.Exists(apiDir)) return apiDir;
                
                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException("WebApi directory not found!");
        }
    }
}