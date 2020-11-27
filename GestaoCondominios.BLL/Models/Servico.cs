using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.BLL.Models
{
    public class Servico
    {
        public int ServicoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; } // montante do serviço

        public StatusServico Status { get; set; }
        // chave estrangeira
        public string UtilizadorId { get; set; } // para identificar quem requisitou este serviço
        // para mapear com a classe Utilizador
        public virtual Utilizador Utilizador { get; set; }

        public ICollection<ServicoPredio> ServicoPredios { get; set; }
    }

    public enum StatusServico
    {
        Pendente, Recusado, Aceite
    }
}
