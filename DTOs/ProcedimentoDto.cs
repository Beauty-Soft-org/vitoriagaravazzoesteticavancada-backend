using Beautysoft.Enum;


namespace Beautysoft.DTOs
{
    public class ProcedimentoDto
    {
        public required string Nome { get; set; } = string.Empty;
        public required string Descricao { get; set; } = string.Empty;
        public required string Imagem { get; set; } = string.Empty;
        public required string Tempo { get; set; } = string.Empty;
        public required double Valor { get; set; }
        public required TipoProc TipoProcedimento { get; set; }
    }
}
