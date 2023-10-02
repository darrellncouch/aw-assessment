
using AdventureWorks.Data.Production.EntityFramework;
using AdventureWorks.Data.Production.Interfaces.Query;
using AdventureWorks.Data.Production.Query;
using AdventureWorks.Web.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using AdventureWorks.Logic.Core;
using AdventureWorks.Logic.Core.Interfaces;
using AdventureWorks.Data.Production.Interfaces.Repository;
using AdventureWorks.Data.Production.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedOrigins = new List<string>();

var policyName = "CorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});


builder.Services.AddScoped<IObjectMapper, ObjectMapper>();

builder.Services.AddDbContext<AdventureWorks2022Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("ProductionDb"))
);

builder.Services.AddScoped<IProductQuery, ProductQuery>();
builder.Services.AddScoped<IProductModelQuery, ProductModelQuery>();
builder.Services.AddScoped<IProductReviewQuery, ProductReviewQuery>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IProductManager, ProductManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
