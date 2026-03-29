using BKU.ProjectManagement.Repositories.Context;
using BKU.ProjectManagement.Repositories.Repositories.Implements;
using BKU.ProjectManagement.Repositories.Repositories.Interfaces;
using BKU.ProjectManagement.Services.Implements;
using BKU.ProjectManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register Repositories
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();

// Register Services
builder.Services.AddScoped<ISemesterService, SemesterService>();

builder.Services.AddDbContext<ProjectManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BKUConnection")
        ?? throw new InvalidOperationException("Connection string 'BKUConnection' was not found.")
    ));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Auto-migrate database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ProjectManagementDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
