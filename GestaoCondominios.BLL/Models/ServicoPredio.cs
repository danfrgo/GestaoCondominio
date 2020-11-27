using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.BLL.Models
{
    public class ServicoPredio
    {
        public int ServicoPredioId { get; set; }
        // chave estrangeira
        public int ServicoId { get; set; }
        // para mapear com a classe Servico
        public virtual Servico Servico { get; set; }
        public DateTime DataExecucao { get; set; }
    }
}
