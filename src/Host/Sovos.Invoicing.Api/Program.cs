using Microsoft.EntityFrameworkCore;

using Serilog;

using Sovos.Invoicing.Application;
using Sovos.Invoicing.BackgroundTasks;
using Sovos.Invoicing.Infrastructure;
using Sovos.Invoicing.Infrastructure.Logging;
using Sovos.Invoicing.Persistence;

StaticLogger.EnsureInitialized();
Log.Information("Server booting up 🚀");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Host.UseSerilog((_, config) => config
        .WriteTo.Console()
        .ReadFrom.Configuration(builder.Configuration));

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddApplication();
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddInfrastructure();
    builder.Services.AddBackgroundTasks(builder.Configuration);

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseInfrastructure();

    app.MapControllers();

    using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<InvoicingDbContext>();
    await dbContext.Database.MigrateAsync();

    app.UseBackgroundTasks();

    app.Run();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception 💥");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server Shutting down... 👇");
    Log.CloseAndFlush();
}