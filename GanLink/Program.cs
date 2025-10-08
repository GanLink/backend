using GanLink.FarmManagement.Application.Internal.QueryServices;
using GanLink.FarmManagement.Domain.Services;
using GanLink.FarmManagement.Infraestructure.Persistence.EF.Repositories;
using GanLink.IAM.Application.Internal.CommandServices;
using GanLink.IAM.Application.Internal.OutboundServices;
using GanLink.IAM.Application.Internal.QueryServices;
using GanLink.IAM.Domain.Repositories;
using GanLink.IAM.Domain.Services;
using GanLink.IAM.Infraestructure.Hashing.BCrypt.Services;
using GanLink.IAM.Infraestructure.Persistence.EF.Repositories;
using GanLink.IAM.Infraestructure.Pipeline.Middleware.Extensions;
using GanLink.IAM.Infraestructure.Token.JWT.Configuration;
using GanLink.IAM.Infraestructure.Token.JWT.Services;
using GanLink.Shared.Domain.Repositories;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;
using GanLink.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//Adding Swagger as a Service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "GanLink.API",
            Version = "v1",
            Description = "ACME.GanLink.API",
            TermsOfService = new Uri("https://ganlink.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "GanLink",
                Email = "GanLink@gmail.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            []
        }
    });
    options.EnableAnnotations();
});

//Add Controllers for manage our classes
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connection))
{
    throw new Exception("Database connection string not set");
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null) throw new InvalidOperationException("Connection string not found");

//Add our DbContext connection to Dependency Injector
builder.Services.AddDbContext<GanLinkDBContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<TimestampAudit>();

//IAM
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();

// IAM Bounded Context Dependency Injection Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

// Bovine System Bounded Context Dependency Injection Configuration
// Repositories
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Repositories.IBovinueRepository, 
    GanLink.BovinueSystem.Infrastructure.Persistence.EF.Repositories.BovinueRepository>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Repositories.IBovinueHealthRecordRepository, 
    GanLink.BovinueSystem.Infrastructure.Persistence.EF.Repositories.BovinueHealthRecordRepository>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Repositories.IBovinueMetricRepository, 
    GanLink.BovinueSystem.Infrastructure.Persistence.EF.Repositories.BovinueMetricRepository>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Repositories.IBovinueCattleHealthRecordRepository, 
    GanLink.BovinueSystem.Infrastructure.Persistence.EF.Repositories.BovinueCattleHealthRecordRepository>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Repositories.IBovinueMetricCategoryRepository, 
    GanLink.BovinueSystem.Infrastructure.Persistence.EF.Repositories.BovinueMetricCategoryRepository>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Repositories.IBovinueMetricParameterRepository, 
    GanLink.BovinueSystem.Infrastructure.Persistence.EF.Repositories.BovinueMetricParameterRepository>();

// Command Services
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Services.IBovinueCommandService, 
    GanLink.BovinueSystem.Application.Internal.CommandServices.BovinueCommandService>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Services.IBovinueHealthRecordCommandService, 
    GanLink.BovinueSystem.Application.Internal.CommandServices.BovinueHealthRecordCommandService>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Services.IBovinueMetricCommandService, 
    GanLink.BovinueSystem.Application.Internal.CommandServices.BovinueMetricCommandService>();

// Query Services
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Services.IBovinueQueryService, 
    GanLink.BovinueSystem.Application.Internal.QueryServices.BovinueQueryService>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Services.IBovinueHealthRecordQueryService, 
    GanLink.BovinueSystem.Application.Internal.QueryServices.BovinueHealthRecordQueryService>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Services.IBovinueMetricQueryService, 
    GanLink.BovinueSystem.Application.Internal.QueryServices.BovinueMetricQueryService>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Services.IBovinueCattleHealthRecordQueryService, 
    GanLink.BovinueSystem.Application.Internal.QueryServices.BovinueCattleHealthRecordQueryService>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Services.IBovinueMetricCategoryQueryService, 
    GanLink.BovinueSystem.Application.Internal.QueryServices.BovinueMetricCategoryQueryService>();
builder.Services.AddScoped<GanLink.BovinueSystem.Domain.Services.IBovinueMetricParameterQueryService, 
    GanLink.BovinueSystem.Application.Internal.QueryServices.BovinueMetricParameterQueryService>();

var app = builder.Build();

//Add scope for our DbContext
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider.GetRequiredService<GanLinkDBContext>();
    services.Database.EnsureCreated();
}

app.UseCors("AllowAll");

// Add Swagger for use on Development

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
//Mapping Controllers EndPoints
app.MapControllers();

app.Run();