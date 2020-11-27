using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestaoCondominios.BLL.Models
{
    class Evento
    {
        public int EventoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "Use ´no máximo 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Data { get; set; }

        // Chave estrangeira
        public string UtilizadorId { get; set; }

        // para mapear com a classe Utilizador
        public virtual Utilizador Utilizador { get; set; }
    }
}
