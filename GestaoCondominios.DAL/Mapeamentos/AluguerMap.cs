using GestaoCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.DAL.Mapeamentos
{
    class AluguerMap : IEntityTypeConfiguration<Aluguer>
    {
        public void Configure(EntityTypeBuilder<Aluguer> builder)
        {

            builder.HasKey(a => a.AluguerId);
            builder.Property(a => a.Valor).IsRequired();
            builder.Property(a => a.MesId).IsRequired();
            builder.Property(a => a.Ano).IsRequired();

            builder.HasOne(a => a.Mes).WithMany(a => a.Algueres).HasForeignKey(a => a.MesId);
            builder.HasMany(a => a.Pagamentos).WithOne(a => a.Aluguer);

            builder.ToTable("Alugueres");
        }
    }
}
