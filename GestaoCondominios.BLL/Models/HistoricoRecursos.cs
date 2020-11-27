using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCondominios.BLL.Models
{
    public class HistoricoRecursos
    {
        public int HistoricoRecursosId { get; set; }

        public decimal Valor { get; set; } // registar todas as entradas (alugueres) e saidas -> é o custo para executar um serviço

        public Tipos Tipo { get; set; } // entrada ou saida, declarada no Enum

        public int Dia { get; set; }

        //chave estrangeira mes
        public int MesId { get; set; }
        // para mapear com a classe Mes
        public virtual Mes Mes { get; set; }

        public int Ano { get; set; }
    }
    public enum Tipos
    {
        Entrada, Saida
    }
}
