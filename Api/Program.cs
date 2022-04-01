using Api.Extensions;
using Api.Interfaces;
using Api.Settings;
using Api.Sevices;
using Common.Middlewares;
using Cqrs;
using DataAccess;
using Domain.Entity;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;
var settings = builder.Configuration.Get<MainSettings>();

// Add services to the container.

service.AddSingleton<ISettings>(settings);

service.AddHttpContextAccessor();
service.AddControllers();
service.AddFluentValidation();
service.AddValidatorsFromAssembly(typeof(Program).Assembly);
service.AddDbContext<DataContext>();
service.AddMediatR(typeof(CqrsAssembly));
service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
service.AddJwtAuthentication(settings);

service.AddScoped<ErrorHandlingMiddleware>();
service.AddScoped<IAuthenticationService, AuthenticationService>();
service.AddScoped<ICookieService, CookieService>();
service.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
service.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200"));

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<DataContext>();
context.Database.Migrate();

app.Run();