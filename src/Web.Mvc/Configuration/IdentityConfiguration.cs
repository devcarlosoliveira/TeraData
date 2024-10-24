using Microsoft.AspNetCore.Identity;

using Web.Mvc.Data;

using Web.Mvc.Domain;

namespace Web.Mvc.Configuration;

public static class IdentityConfiguration
{
    public static WebApplicationBuilder AddCustomIdentityConfiguration(this WebApplicationBuilder builder)
    {

        //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        //    .AddEntityFrameworkStores<ApplicationDbContext>();

        // Configuração do Identity
        builder.Services
                .AddIdentity<User, IdentityRole<Guid>>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 3;
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddDefaultTokenProviders()
                .AddDefaultUI() // Adiciona as páginas padrão do Identity
                .AddEntityFrameworkStores<ApplicationDbContext>();

        //builder.Services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("Delete", policy => policy.RequireRole("Admin"));
        //    options.AddPolicy("GetPosts", policy => policy.RequireClaim("Posts","C"));
        //});

        return builder;
    }
}
