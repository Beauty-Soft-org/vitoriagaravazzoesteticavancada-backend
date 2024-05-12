using Beautysoft.Enum;

namespace Beautysoft.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public DateTime DataHoraAgendada { get; set; }
        public required string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public required string Descricao { get; set; } = string.Empty;
        public required string Tempo { get; set; } = string.Empty;
        public required double Valor { get; set; }
        public required TipoProc TipoProcedimento { get; set; }

    }
}
