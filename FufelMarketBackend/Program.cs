using FufelMarketBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;
using AutoMapper;
using AutoMapper.EquivalencyExpression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
}); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 7151);
});

var mediatorAssembly = Assembly.Load(nameof(FufelMarketBackend));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatorAssembly));

var mapperCfg = new MapperConfiguration(cfg =>
{
    cfg.AllowNullDestinationValues = false;
    cfg.AddMaps(nameof(FufelMarketBackend));
    cfg.AddCollectionMappers();
});
var mapper = mapperCfg.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
    
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
    
await context.Database.MigrateAsync();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
