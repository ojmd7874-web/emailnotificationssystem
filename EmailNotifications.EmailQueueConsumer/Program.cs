using EmailNotifications.Application.DependencyInjection;
using EmailNotifications.Application.Interfaces;
using EmailNotifications.Application.Services;
using EmailNotifications.EmailQueueConsumer.Consumers;
using EmailNotifications.Infrastructure.DependencyInjection;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<EmailNotificationConsumer>();
    options.SetKebabCaseEndpointNameFormatter();

    options.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMq:Host"]!, host =>
        {
            host.Username(builder.Configuration["RabbitMq:Username"]!);
            host.Password(builder.Configuration["RabbitMq:Password"]!);
        });

        config.ConfigureEndpoints(context);
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
