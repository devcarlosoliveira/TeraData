using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Web.Mvc.Data;

namespace Web.Mvc.Configuration;

public static class MvcConfiguration
{
    public static WebApplicationBuilder AddCustomMvcConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        });

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


        builder.Services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromDays(60);
            options.ExcludedHosts.Add("example.com");
            options.ExcludedHosts.Add("www.example.com");

        });

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddRazorPages();

        return builder;
    }

    public static WebApplication UseCustomMvcConfiguration(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();

        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        /**Exemplos 
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller:slugify=Home}/{action:slugify=Home=Index}/{id?}");

        app.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        //Rota de areas especializadas
        app.MapControllerRoute("AreaProdutos","Produtos", "Produtos/{controller=Cadastro}/{action=Index}/{id?}");
        app.MapControllerRoute("AreaVendas","Vendas", "Vendas/{controller=Gestao}/{action=Index}/{id?}"); 
         
         */


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        return app;
    }
}
