using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoCondominios.Extensions
{
    public static class ConfiguracaoIdentityExtension
    {
        public static void ConfigurarNomeUtilizador(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(opcoes =>
            {
                opcoes.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; // caracteres aceites no nome do user
                opcoes.User.RequireUniqueEmail = true; // nao ha emails repetidos na BD
            });
        }

        public static void ConfigurarPasswordUtilizador(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(opcoes =>
            {
                opcoes.Password.RequireDigit = true;
                opcoes.Password.RequireLowercase = true;
                opcoes.Password.RequiredLength = 8; // minimo de 8 caracteres
                opcoes.Password.RequireNonAlphanumeric = true; // deve conter caracteres especiais
                opcoes.Password.RequireUppercase = true;
                opcoes.Password.RequiredUniqueChars = 0; // pode ter caracteres repetidos
            });
        }

    }
}
