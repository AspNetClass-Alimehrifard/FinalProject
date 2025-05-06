using FinalProject.WebApi.ApplicationServices.Contracts;
using FinalProject.WebApi.ApplicationServices.Services.OrderServices;
using FinalProject.WebApi.ApplicationServices.Services.PersonServices;
using FinalProject.WebApi.ApplicationServices.Services.ProductServices;
using FinalProject.WebApi.Models;
using FinalProject.WebApi.Models.Services.Contracts;
using FinalProject.WebApi.Models.Services.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [-AddDbContext-]
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ProjectDbContext>(options => options.UseSqlServer(connectionString));
#endregion

#region [-AddScopedRepositories-]
builder.Services.AddScoped<IPersonRepository, PersonRepositry>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
#endregion

#region [-AddScopedServices-]
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
