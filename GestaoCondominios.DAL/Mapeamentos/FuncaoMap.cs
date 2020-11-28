using GestaoCondominios.BLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.DAL.Mapeamentos
{
    // mapeamento da classe funcao
    public class FuncaoMap : IEntityTypeConfiguration<Funcao>
    {
        public void Configure(EntityTypeBuilder<Funcao> builder)
        {
            // .Property -> para acessar uma propiedade
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Property(f => f.Descricao).IsRequired().HasMaxLength(30);

            builder.HasData(
                new Funcao
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Morador",
                    NormalizedName = "MORADOR", // para comparar o nome da funcao em caso de add utilizadores ou exlcusao - uppercase
                    Descricao = "Morador do Prédio"

                },
                 new Funcao
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = "Responsavel",
                     NormalizedName = "RESPONSAVEL", // para comparar o nome da funcao em caso de add utilizadores ou exlcusao - uppercase
                     Descricao = "Responsavel do Prédio"
                 },

                  new Funcao
                  {
                      Id = Guid.NewGuid().ToString(),
                      Name = "Administrador",
                      NormalizedName = "ADMINISTRADOR", // para comparar o nome da funcao em caso de add utilizadores ou exlcusao - uppercase
                      Descricao = "Administrador do Prédio"
                  });
            builder.ToTable("Funcoes");
        }
    }
}
