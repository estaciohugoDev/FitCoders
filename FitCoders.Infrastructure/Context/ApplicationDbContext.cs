using System.Reflection;
using FitCoders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCoders.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Gym> Gym { get; set; }
    public DbSet<Instructor> Instructor { get; set; }
    public DbSet<Member> Member { get; set; }
    public DbSet<Workout> Workout { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
        ConfigureForMySql(modelBuilder);
    }

    private static void ConfigureForMySql(ModelBuilder modelBuilder) 
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach(var property in entityType.GetProperties())
            {
                if(property.ClrType == typeof(Guid) || property.ClrType ==typeof(Guid?))
                {
                    property.SetColumnType("char(36)");
                    property.SetCollation("ascii_general_ci");
                }
            }

            var createdAtProperty = entityType.FindProperty("CreatedAt");

            createdAtProperty?.SetDefaultValueSql("CURRENT_TIMESTAMP");

            var updatedAtProperty = entityType.FindProperty("UpdatedAt");

            updatedAtProperty?.SetDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
        }
    }
}
