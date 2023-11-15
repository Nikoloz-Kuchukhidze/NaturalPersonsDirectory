using NaturalPersonsDirectory.API;
using NaturalPersonsDirectory.API.Middlewares;
using NaturalPersonsDirectory.Application;
using NaturalPersonsDirectory.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services
    .AddAPI()
    .AddApplication()
    .AddInfrastructure(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseRequestLocalization();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
