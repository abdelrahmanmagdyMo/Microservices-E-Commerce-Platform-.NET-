using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(GetProductByIdQuery)));
});
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IBrandRepository, ProductRepository>();
builder.Services.AddScoped<ITypeRepository, ProductRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Catalog Api",
        Description = "This Is Api For Catalog Microservice In E-Commerce Application",
        Version = "v1",
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "Abdelrahman",
            Email = "a@example.com",
            Url = new Uri("https://www.your-website.com")
        }
    });
});
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;

});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ICatalogContext>();
    await BrandContextSeed.SeedDataAsync(context.Brands);
    await TypeContextSeed.SeedDataAsync(context.Types);
    await ProductContextSeed.SeedDataAsync(context.Products);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
