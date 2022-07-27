using DAL;
using GraphQLEngine;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var connectionString = builder.Configuration.GetConnectionString("Local");
ArgumentNullException.ThrowIfNull(connectionString);

builder.Services.AddDALServices(connectionString);
builder.Services.AddGraphQLEngineServices();

var app = builder.Build();

app.UseGraphQLEngine();
app.UseHttpsRedirection();

app.Run();