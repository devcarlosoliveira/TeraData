using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Web.Mvc.Data;

using Web.Mvc.Domain;
using Web.Mvc.Domain.Base;
using Web.Mvc.Extesions.IdentityUser;

namespace Web.Mvc.Configuration;

public static class DependencyInjectionConfiguration
{
    public static WebApplicationBuilder AddCustomDependencyInjectionConfiguration(this WebApplicationBuilder builder)
    {
        //builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        //builder.Services.AddTransient<IEmailSender, EmailSender>(); // Implementação do envio de e-mail
        builder.Services.AddScoped<IAppIdentityUser, AppIdentityUser>();

        return builder;
    }
}
