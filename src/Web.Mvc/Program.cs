using Web.Mvc.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddCustomMvcConfiguration()
    .AddCustomIdentityConfiguration()
    .AddCustomDependencyInjectionConfiguration();




var app = builder.Build();

app.UseCustomMvcConfiguration();

app.Run();
