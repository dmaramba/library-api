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
    var path = Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, ".xml");
    if (File.Exists(path))
    {
        options.IncludeXmlComments(path);
    }
    /*
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "Open Id" }
                },
                AuthorizationUrl = new Uri(configuration["Authentication:Domain"] + "authorize?audience=" +
                                           configuration["Authentication:Audience"])
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] { "openid" }
        }
    });
    */

    options.EnableAnnotations();
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


        container.RegisterType<BorrowBookRepository>()
           .As<IBorrowBookRepository>()
           .InstancePerLifetimeScope();


        container.RegisterType<ReserveBookRepository>()
           .As<IReserveBookRepository>()
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

