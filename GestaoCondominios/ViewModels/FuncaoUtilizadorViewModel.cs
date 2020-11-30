using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoCondominios.ViewModels
{
    public class FuncaoUtilizadorViewModel
    {
        public string FuncaoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool isSelecionado { get; set; } // para verificar qual funçao esta selecionada
    }
}
