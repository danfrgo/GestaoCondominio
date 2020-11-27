using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestaoCondominios.BLL.Models
{
    public class Veiculo
    {
        public int VeiculoId { get; set; }
        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [StringLength(20,ErrorMessage = "Máximo de 20 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(20, ErrorMessage = "Máximo de 20 caracteres")]
        public string Marca { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(20, ErrorMessage = "Máximo de 20 caracteres")]
        public string Cor { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Matricula { get; set; }

        //Chave estrangeira
        public string UtilizadorId { get; set; }
        // para mapear com a classe Utilizador
        public Utilizador Utilizador { get; set; }
    }
}
