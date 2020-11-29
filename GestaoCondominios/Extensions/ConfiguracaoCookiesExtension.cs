using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoCondominios.Extensions
{
    public static class ConfiguracaoCookiesExtension
    {
        public static void ConfigurarCookies(this IServiceCollection service)
        {
            service.ConfigureApplicationCookie(opcoes =>
            {
                opcoes.Cookie.Name = "IdentityCookie";
                opcoes.Cookie.HttpOnly = true;
                opcoes.ExpireTimeSpan = TimeSpan.FromMinutes(60); // expira em 60 minutos
                opcoes.LoginPath = "/Utilizadores/Login";
                opcoes.AccessDeniedPath = "/Utilizadores/AcessoNegado";
            });
        }
    }
}
