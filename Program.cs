using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProdApi.Context;
using ProdApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Konfiguracja swagger z obsługą JWT
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Wpisz: Bearer {token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Konfiguracja serializacji enumów jako stringi w JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Konfiguracja DbContext z SQLite
builder.Services.AddDbContext<TaskDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

// Rejestracja repozytorium
builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();

// Rejestruje wszystkie walidatory z tego assembly
builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<Program>();


var app = builder.Build();

// Dodanie swagger
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Prod API v1");
    opt.RoutePrefix = "swagger"; // UI pod /swagger
});

app.UseHttpsRedirection();
app.MapControllers();


app.Run();

