using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Library.Infrastructure.Services;
using Library.Infrastructure.Repository;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using Library.Infrastructure.Context;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Library.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library API",
        Version = "v1",
        Description = "Libray REST API"
    });
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
   {
    {
        new OpenApiSecurityScheme
        {
            Name = "Bearer",
            In = ParameterLocation.Header,
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        },
        new List<string>()
    }
    });
    var path = Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, ".xml");
    if (File.Exists(path))
    {
        options.IncludeXmlComments(path);
    }
  
    options.EnableAnnotations();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServer:url"];
        options.Audience = "library-api";
        options.RequireHttpsMetadata = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Customer", policy => policy.RequireRole("Customer"));
});

builder.Services.AddDbContext<LibraryContext>();

builder.Services.Configure<LibrarySetting>(
    builder.Configuration.GetSection(LibrarySetting.SectionName));

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((container) =>
    {

        container.RegisterType<BookRepository>()
           .As<IBookRepository>()
           .InstancePerLifetimeScope();

        container.RegisterType<CustomerRepository>()
        .As<ICustomerRepository>()
        .InstancePerLifetimeScope();

        container.RegisterType<NotificationRepository>()
        .As<INotificationRepository>()
        .InstancePerLifetimeScope();

        container.RegisterType<BorrowBookRepository>()
           .As<IBorrowBookRepository>()
           .InstancePerLifetimeScope();


        container.RegisterType<ReserveBookRepository>()
           .As<IReserveBookRepository>()
           .InstancePerLifetimeScope();

        container.RegisterType<NotificationService>()
               .As<INotificationService>()
               .InstancePerLifetimeScope();

        container.RegisterType<CustomerService>()
               .As<ICustomerService>()
               .InstancePerLifetimeScope();

        container.RegisterType<BookService>()
                .As<IBookService>()
                .InstancePerLifetimeScope();


    });
var app = builder.Build();



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

