var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;

// Add services to the container.

service.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();