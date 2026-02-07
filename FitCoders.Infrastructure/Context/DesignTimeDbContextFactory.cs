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
            var config = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),"../FitCoders.WebApi"))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .Build();

            var connString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseMySql(connString,
                                    new MySqlServerVersion(new Version(8,0,35)),
                                    options => options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}