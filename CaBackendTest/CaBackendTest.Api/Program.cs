using CaBackendTest.Application.Interfaces;
using CaBackendTest.Application.Services.Billings;
using CaBackendTest.Application.Services.Customers;
using CaBackendTest.Application.Services.Products;
using CaBackendTest.Domain.Interfaces.Repositories.Billings;
using CaBackendTest.Domain.Interfaces.Repositories.Cutomers;
using CaBackendTest.Domain.Interfaces.Repositories.Products;
using CaBackendTest.Domain.Interfaces.Services.Billings;
using CaBackendTest.Domain.Interfaces.Services.Customers;
using CaBackendTest.Domain.Interfaces.Services.Products;
using CaBackendTest.Infrastructure.Persistence.Contexts;
using CaBackendTest.Infrastructure.Repositories.Billings;
using CaBackendTest.Infrastructure.Repositories.Customers;
using CaBackendTest.Infrastructure.Repositories.Products;
using CaBackendTest.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBillingRepository, BillingRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBillingService, BillingService>();

builder.Services.AddHttpClient<IBillingExternalService, BillingExternalService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Billing API", 
        Version = "v1" ,
        Description = "API for managing products, customers, and importing billings via external API.",
        Contact = new OpenApiContact
        {
            Name = "Anderson",
            Email = "anderson@gmail.com",
            Url = new Uri("https://github.com/Anderson-Junior") // Pode ser site da empresa ou portfólio
        },
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Automatically apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
