using GestaoCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.DAL.Mapeamentos
{
    public class VeiculoMap : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.HasKey(v => v.VeiculoId); // primary key
            builder.Property(v => v.Nome).IsRequired().HasMaxLength(40);
            builder.Property(v => v.Cor).IsRequired().HasMaxLength(20);
            builder.Property(v => v.Marca).IsRequired().HasMaxLength(20);
            builder.Property(v => v.Matricula).IsRequired().HasMaxLength(20);
            builder.HasIndex(v => v.Matricula).IsUnique();
            builder.Property(v => v.UtilizadorId).IsRequired(); // chave estrangeira

            // relacionamento - veiculo tem um user, mas um user pode ter varios veiculos
            builder.HasOne(v => v.Utilizador).WithMany(v => v.Veiculos).HasForeignKey(v => v.UtilizadorId);

            builder.ToTable("Veiculos");    
        }
    }
}
