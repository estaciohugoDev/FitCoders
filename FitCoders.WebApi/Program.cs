using FitCoders.Infrastructure;
using FitCoders.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("mysql");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
   options.UseMySql(
        connString, 
        ServerVersion.AutoDetect(connString),
        sqlOptions =>
        {
            sqlOptions.MigrationsAssembly("FitCoders.Infrastructure");
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);  
        });

    if(builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
        options.LogTo(Console.WriteLine, LogLevel.Information);
    }
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "FitCoders API",
        Version = "v1",
        Description = "API for managing the gym."
    });
}
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
    builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json","FitCoders API v1");
        options.RoutePrefix = string.Empty;
    });

    using(var scope = app.Services.CreateScope())
    {
        try
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if(!context.Database.CanConnect())
            {
                Console.WriteLine("⚠️ Database doesnt exist. Applying migrations...");
                context.Database.Migrate();
                Console.WriteLine("Database successfully created!");
                
            }
            else
            {
                var pendingMigrations = context.Database.GetPendingMigrations();
                if(pendingMigrations.Any())
                {
                    Console.WriteLine($"Applying {pendingMigrations.Count()} pending migrations");
                    context.Database.Migrate();
                    Console.WriteLine("Migrations applied!");
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error applying migrations: {ex.Message}");   
        }
        
    }
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

Console.WriteLine("🚀 FitCoders API initiating...");
Console.WriteLine($"📊 Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"🔗 MySQL: {connString!.Split(';')[0]}...");

app.Run();

