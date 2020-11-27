using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestaoCondominios.BLL.Models
{
    public class Apartamento
    {        
        public int ApartamentoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0, 1000, ErrorMessage = "Valor inválido")]
        [Display(Name = "Número")]

        public int Numero { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0, 10, ErrorMessage = "Valor inválido")]
        public int Andar { get; set; }
        public string Foto { get; set; } //na BD vai ser apenas guardada a URL da foto para evitar a sobrecarga de ter varias fotos

        // Chave estrangeira
        public int MoradorId { get; set; }
        // para mapear com a classe Utilizador
        public virtual Utilizador Morador { get; set; } // o morador e o propriatario é o proprio utilizador
        public int PropriatarioId { get; set; }
        public Utilizador Propriatario { get; set; }
    }
}
