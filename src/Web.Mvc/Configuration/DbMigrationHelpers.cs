using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Web.Mvc.Data;
using Web.Mvc.Domain;
using Web.Mvc.Domain.Enums;

namespace Web.Mvc.Configuration
{
    public static class DbMigrationHelperExtesion
    {
        public static void UseCustomEnsureSeedData(this WebApplication app)
        {
            DbMigrationHelpers.EnsureSeedData(app).Wait();
        }
    }
    public static class DbMigrationHelpers
    {

        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                // uso apenas em 1 contexto
                //context.Database.EnsureCreated();

                // uso com 1 ou mais contextos
                await context.Database.MigrateAsync(); //context.Database.EnsureCreated();

                await EnsureSeedRolesAsync(context);
                await EnsureSeedInitialUserAsync(context, userManager);

                await EnsureSeedCustumersAsync(context);
                await EnsureSeedChannelsAsync(context);
                await EnsureSeedCardsAsync(context);

            }
        }

        private static async Task EnsureSeedCustumersAsync(ApplicationDbContext context)
        {
            // Verifica se já existem cartas no contexto
            if (context.Customers.Any()) return;

            var user = context.Users.FirstAsync(filter => filter.UserName == "admin@admin.com");
            if (user == null) return;

            await context.Customers.AddAsync(new Customer()
            {
                Id = Guid.NewGuid(),
                Name = "Cliente Fictício",
                Avatar = "Avatar",
                IsActive = true,
            });

            await context.Customers.AddAsync(new Customer()
            {
                Id = Guid.NewGuid(),
                Name = "Cliente XPTO ",
                Avatar = "Avatar",
                IsActive = true,
            });

            await context.SaveChangesAsync();
        }

        private static async Task EnsureSeedCardsAsync(ApplicationDbContext context)
        {
            // Verifica se já existem cartas no contexto
            if (context.Cards.Any()) return;

            var user = context.Users.FirstAsync(filter => filter.UserName == "admin@admin.com");
            if (user == null) return;

            List<string> cardNameElementaryList = [
                "Gráfico", "Passo a Passo", "Comparação (A xB)", "Check List (Lista de Checagem)",
                "Galeria de Imagens", "Lista Ilustrada", "MapaTemático", "Pergunta(s) Frequente(s)"];

            foreach (var cardName in cardNameElementaryList)
            {
                await context.Cards.AddAsync(new Card()
                {
                    Id = Guid.NewGuid(),
                    Name = cardName,
                    CardType = "Elementary",
                    Score = 3,
                });
            }

            List<string> cardNameIntermediateList = [
                "Linha do Tempo", "Números Ilustrados", "Selos e Logos", "Mini Bio", "Simulador", "Diagrama"];

            foreach (var cardName in cardNameIntermediateList)
            {
                await context.Cards.AddAsync(new Card()
                {
                    Id = Guid.NewGuid(),
                    Name = cardName,
                    CardType = "Intermediate",
                    Score = 5,
                });
            }

            List<string> cardNameAdvancedList = [
                "Fluxograma", "Melhor Escolha", "Prós x Contras", "Aspas", "Infográfico", "Tabela Associativa"];

            foreach (var cardName in cardNameAdvancedList)
            {
                await context.Cards.AddAsync(new Card()
                {
                    Id = Guid.NewGuid(),
                    Name = cardName,
                    CardType = "Advanced",
                    Score = 7,
                });
            }
            await context.SaveChangesAsync();
        }

        private static async Task EnsureSeedChannelsAsync(ApplicationDbContext context)
        {
            // Verifica se já existem canal no contexto
            if (context.Channels.Any()) return;

            var user = context.Users.FirstAsync(filter => filter.UserName == "admin@admin.com");
            if (user == null) return;

            List<string> nameList = ["Facebook", "Instagram", "Blog", "LinkdIn"];

            foreach (var name in nameList)
            {
                var channel = new Channel()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    IsDeleted = false,
                    CreateBy = user.Result.Id,
                    UpdateBy = user.Result.Id,
                };

                context.Channels.Add(channel);
            }
            await context.SaveChangesAsync();
        }

        private static async Task EnsureSeedRolesAsync(ApplicationDbContext identityContext)
        {
            // Verifica se já existem roles no contexto
            if (identityContext.Roles.Any()) return;

            // Itera sobre os valores do enum EnumAccessPriority
            foreach (var item in Enum.GetValues(typeof(EnumAccessPriority.UserRole)))
            {
                var roleId = Guid.NewGuid();
                var roleName = item.ToString()?.ToUpperInvariant();

                var role = new IdentityRole<Guid>
                {
                    Id = roleId,
                    Name = roleName,
                    NormalizedName = roleName
                };

                identityContext.Roles.Add(role);
            }

            try
            {
                // Salva as mudanças no banco de dados
                await identityContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Loga a exceção ou trata conforme necessário
                // Por exemplo: _logger.LogError(ex, "Erro ao salvar roles no banco de dados.");
                throw; // Re-throw ou trate a exceção conforme a lógica do seu aplicativo
            }
        }

        private static async Task EnsureSeedInitialUserAsync(ApplicationDbContext identityContext, UserManager<User> userManager)
        {
            if (identityContext.Users.Any()) return;

            var userId = Guid.NewGuid();
            var userEmail = "admin@admin.com";
            var userNormalizedEmail = userEmail.ToUpperInvariant();
            User user = new()
            {
                Id = userId,
                Email = userEmail,
                NormalizedEmail = userNormalizedEmail,
                UserName = userEmail,
                EmailConfirmed = true,
            };

            var password = "admin";

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var roleName = EnumAccessPriority.UserRole.Admin.ToString().ToUpperInvariant();
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}
