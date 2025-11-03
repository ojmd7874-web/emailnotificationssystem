using EmailNotifications.Application.DependencyInjection;
using EmailNotifications.Infrastructure.DependencyInjection;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddMassTransit(options =>
{
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

app.UseInfrastructurePolicy();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
