using GestaoCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.DAL.Mapeamentos
{
    // mapeamento da classe Utilizador
    public class UtilizadorMap : IEntityTypeConfiguration<Utilizador>
    {
        public void Configure(EntityTypeBuilder<Utilizador> builder)
        { //ValueGeneratedOnAdd -> autoincremental
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.CodigoPostal).IsRequired().HasMaxLength(30);
            // builder.HasIndex(u => u.CodigoPostal).IsUnique();
            builder.Property(u => u.Foto).IsRequired();
            builder.Property(u => u.PrimeiroAcesso).IsRequired();
            builder.Property(u => u.Status).IsRequired();

            // um proprietario pode ter um ou mais apartamentos
            builder.HasMany(u => u.PropriatariosApartamentos).WithOne(u => u.Propriatario);
            // um apartamento pode ter um morador, mas um morador pode ter varios apartamentos em seu nome alugados
            builder.HasMany(u => u.MoradoresApartamentos).WithOne(u => u.Morador);
            // um user pode ter varios veiculos, mas aquele veiculo pode estar vinculado apenas a um user
            builder.HasMany(u => u.Veiculos).WithOne(u => u.Utilizador);
            builder.HasMany(u => u.Eventos).WithOne(u => u.Utilizador);
            builder.HasMany(u => u.Pagamentos).WithOne(u => u.Utilizador);
            // um user pode requisitar varios serviços, mas aquele serviço está destinado apenas a um utilizador
            builder.HasMany(u => u.Servicos).WithOne(u => u.Utilizador);

            builder.ToTable("Utilizadores");
        }
    }
}
