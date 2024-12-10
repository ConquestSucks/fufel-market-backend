using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;
using System.Text;
using FufelMarketBackend.Data;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FufelMarketBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Чтение конфигурации для окружений
var environment = builder.Environment.EnvironmentName;

// Конфигурация CORS
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

// Настройка CORS для разных окружений
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        // Для продакшн-среды разрешаем только указанные домены
        if (builder.Environment.IsProduction())
        {
            policy.WithOrigins(allowedOrigins) // Разрешаем только указанные домены
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }
        else
        {
            // Для дев-среды разрешаем все домены
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }
    });
});

// Конфигурация аутентификации через JWT
var jwtKey = builder.Configuration.GetValue<string>("Jwt:Key");
var jwtIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer");
var jwtAudience = builder.Configuration.GetValue<string>("Jwt:Audience");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // options.TokenValidationParameters = new TokenValidationParameters
        // {
        //     ValidateIssuer = true,
        //     ValidateAudience = true,
        //     ValidateLifetime = true,
        //     ValidIssuer = jwtIssuer,
        //     ValidAudience = jwtAudience,
        //     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        // };
    });

// Регистрация почтового сервиса
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddSingleton(new EmailTemplateService(Path.Combine(Directory.GetCurrentDirectory(), "HtmlTemplates")));

// Настройка подключения к базе данных
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавление Mediator
var mediatorAssembly = Assembly.Load(nameof(FufelMarketBackend));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatorAssembly));

// Настройка AutoMapper
var mapperCfg = new MapperConfiguration(cfg =>
{
    cfg.AllowNullDestinationValues = false;
    cfg.AddMaps(nameof(FufelMarketBackend));
    cfg.AddCollectionMappers();
});
var mapper = mapperCfg.CreateMapper();
builder.Services.AddSingleton(mapper);

// Добавление контроллеров
builder.Services.AddControllers();

// Добавление Swagger для разработки
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Включение CORS
app.UseCors("AllowSpecificOrigins");

// Включение Swagger в разработке
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Миграции
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
await context.Database.MigrateAsync();

// Использование HTTPS в продакшн-среде
if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

// Аутентификация и авторизация
app.UseAuthentication();
app.UseAuthorization();

// Маршруты контроллеров
app.MapControllers();

app.Run();
