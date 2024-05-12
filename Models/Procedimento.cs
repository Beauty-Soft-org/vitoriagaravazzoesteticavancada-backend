using Beautysoft.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beautysoft.Models
{
    public class Procedimento
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Tempo { get; set; } = string.Empty;
        public string Imagem { get; set; } = string.Empty;
        public double Valor { get; set; } 
        public TipoProc TipoProcedimento { get; set; }

    }
}
