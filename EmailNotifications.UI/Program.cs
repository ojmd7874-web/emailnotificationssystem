using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Services;
using MassTransit;
using EmailNotifications.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"] ?? "localhost", host =>
        {
            host.Username(builder.Configuration["RabbitMq:Username"] ?? "guest");
            host.Password(builder.Configuration["RabbitMq:Password"] ?? "guest");
        });
    });
});

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IXmlPublishService, XmlPublishService>();
builder.Services.AddScoped<ITemplateService, TemplatesService>();    

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!) });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
