using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Library.Infrastructure.Services;
using Library.Infrastructure.Repository;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using Library.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LibraryContext>();


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
