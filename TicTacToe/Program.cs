using Microsoft.Extensions.PlatformAbstractions;
using TicTacToe.Application;
using TicTacToe.Infrastructure;
using TicTacToe.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    // XML Documentation
    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
    var xmlFileName = typeof(Program).Assembly.GetName().Name + ".xml";
    var xmlPath = Path.Combine(basePath, xmlFileName);
    opt.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Database initialization (applying migrations)
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await initializer.ApplyMigrations();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
