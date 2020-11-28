using GestaoCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.DAL.Mapeamentos
{
    public class HistoricoRecursosMap : IEntityTypeConfiguration<HistoricoRecursos>
    {
        public void Configure(EntityTypeBuilder<HistoricoRecursos> builder)
        {
            builder.HasKey(hr => hr.HistoricoRecursosId);
            builder.Property(hr => hr.Valor).IsRequired();
            builder.Property(hr => hr.Tipo).IsRequired();
            builder.Property(hr => hr.Dia).IsRequired();
            builder.Property(hr => hr.MesId).IsRequired();
            builder.Property(hr => hr.Ano).IsRequired();

            // um HR pode ter apenas um mes, mas um mes pode estar relacionado a varios HR's
            builder.HasOne(hr => hr.Mes).WithMany(hr => hr.HistoricoRecursos).HasForeignKey(hr => hr.MesId);

            builder.ToTable("HistoricoRecursos");
        }
    }
}
