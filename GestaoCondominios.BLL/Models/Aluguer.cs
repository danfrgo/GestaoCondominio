using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestaoCondominios.BLL.Models
{
    public class Aluguer
    {
        public int AluguerId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0,int.MaxValue, ErrorMessage ="Valor inválido")]
        public decimal Valor { get; set; }

        // Chave estrangeira
        [Display(Name = "Mês")]
        public int MesId { get; set; }
        // para mapear com a classe Mes
        public Mes Mes { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(2020, 2030, ErrorMessage = "Valor inválido")]
        public int Ano { get; set; }
        public virtual ICollection<Pagamento> Pagamento { get; set; }
    }
}
