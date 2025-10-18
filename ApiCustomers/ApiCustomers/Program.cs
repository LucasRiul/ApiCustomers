using ApiCustomers.Data;
using ApiCustomers.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using ApiCustomers.Interfaces;
using ApiCustomers.Client;
using ApiCustomers.Repositories.Interfaces;
using ApiCustomers.Services;
using Microsoft.OpenApi.Models;
using ApiCustomers.Middlewares;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Banco InMemory
builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseInMemoryDatabase("CustomerDb"));

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injetar Dependência
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddHttpClient<IViaCepClient, ViaCepClient>(client =>
{
    client.BaseAddress = new Uri("https://viacep.com.br/ws/");
    client.Timeout = TimeSpan.FromSeconds(20);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Customers v1");
        c.RoutePrefix = string.Empty; // faz o swagger abrir direto na raiz "/"
    });
}
//Middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
