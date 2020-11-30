using GestaoCondominios.DAL.Interface;
using GestaoCondominios.DAL.Interfaces;
using GestaoCondominios.DAL.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.DAL
{
    public static class ConfiguracaoRepositoriosExtension
    {
        public static void ConfigurarRepositorios(this IServiceCollection services)
        {
            services.AddTransient<IUtilizadorRepositorio, UtilizadorRepositorio>(); // para utilizador o UtilizadorRepositorio
            services.AddTransient<IFuncaoRepositorio, FuncaoRepositorio>();


        }
    }
}
