using DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;

// Add services to the container.

service.AddControllers();
service.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<DataContext>();
context.Database.Migrate();

app.Run();