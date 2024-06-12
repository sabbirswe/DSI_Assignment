using DSI_Assignment.Application;
using DSI_Assignment.Application.IService;
using DSI_Assignment.Application.Service;
using DSI_Assignment.Domain.IRepository;
using DSI_Assignment.Infrastructure;
using DSI_Assignment.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
    builder => builder.WithOrigins("https://localhost:7218")
    .AllowAnyMethod()
    .AllowAnyHeader());
});

Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/error_logs.txt", rollingInterval: RollingInterval.Year)
            .CreateLogger();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
//Register Dapper Connection
builder.Services.AddScoped<IDapperDbConnection, DapperDbConnection>();

//Register Repositories & Services 
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
