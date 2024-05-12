

using Beautysoft.Enum;

namespace Beautysoft.DTOs
{
    public class MensagemTemporariaDTO
    {
        public required string Nome { get; set; } = string.Empty;
        public required string Descricao { get; set; } = string.Empty;
        public required bool Habilitado { get; set; }
        public required TipoMensagemTemporaria TipoMensagemTemporaria { get; set; }
    }
}
