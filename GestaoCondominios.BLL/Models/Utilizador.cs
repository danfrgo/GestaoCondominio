using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.BLL.Models
{
    public class Utilizador: IdentityUser<string>
    {
        public string CodigoPostal { get; set; }
        public string Foto { get; set; } //na BD vai ser apenas guardada a URL da foto para evitar a sobrecarga de ter varias fotos
        public bool PrimeiroAcesso { get; set; } // para redefinir a propria password
        public StatusConta Status { get; set; } // para definir se o utilizador foi aprovado ou rejeitado na plataforma

        public virtual ICollection<Apartamento> MoradoresApartamentos { get; set; }
        public virtual ICollection<Apartamento> ProprietariosApartamentos { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }
        public virtual ICollection<Evento> Eventos { get; set; }
        public virtual ICollection<Servico> Servicos { get; set; }
        public virtual ICollection<Pagamento> Pagamentos { get; set; }
    }
    public enum StatusConta
    {
        Analisando, Aprovado, Reprovado
    }
}
