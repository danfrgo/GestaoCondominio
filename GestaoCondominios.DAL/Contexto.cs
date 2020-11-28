using GestaoCondominios.BLL;
using GestaoCondominios.BLL.Models;
using GestaoCondominios.DAL.Mapeamentos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.DAL
{
   public class Contexto : IdentityDbContext<Utilizador, Funcao, string>
    {
        public DbSet<Aluguer> Alugueres { get; set; }
        public DbSet<Apartamento> Apartamentos { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Funcao> Funcoes { get; set; }
        public DbSet<HistoricoRecursos> HistoricoRecursos { get; set; }
        public DbSet<Mes> Meses { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<ServicoPredio> ServicoPredios { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }

        // subscrever a funcao de criaçao dos dados para ser aplicado todas as configs que estao nos nossos ficheiros de mapeamento
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AluguerMap());
            builder.ApplyConfiguration(new ApartamentoMap());
            builder.ApplyConfiguration(new EventoMap());
            builder.ApplyConfiguration(new FuncaoMap());
            builder.ApplyConfiguration(new HistoricoRecursosMap());
            builder.ApplyConfiguration(new MesMap());
            builder.ApplyConfiguration(new ServicoMap());
            builder.ApplyConfiguration(new ServicoPrediosMap());
            builder.ApplyConfiguration(new UtilizadorMap());
            builder.ApplyConfiguration(new VeiculoMap());
        }

    }
}
