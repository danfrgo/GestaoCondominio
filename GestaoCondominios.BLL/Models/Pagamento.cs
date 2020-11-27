using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.BLL.Models
{
    public class Pagamento
    {
        public int PagamentoId { get; set; }

        // chave estrangeira
        public string UtilizadorId { get; set; }
        // para mapear com a classe Aluguer
        public virtual Utilizador Utilizador { get; set; }

        // chave estrangeira
        public int AluguerId { get; set; } // para saber quanto o aluguer/montante que o utilizador está a pagar
        // para mapear com a classe Aluguer
        public Aluguer Aluguer { get; set; } 

        public DateTime? DataPagamento { get; set; } // "?" para ser possivel introduir valures nulos caso o utilizador nao tenha efetuado o pagamento

        public StatusPagamento Status { get; set; } // definir os estados do pagamento Pago - Pendente e Atrasado (Enum
    }

    public enum StatusPagamento
    {
        Pago, Pendente, Atrasado
    }
}
