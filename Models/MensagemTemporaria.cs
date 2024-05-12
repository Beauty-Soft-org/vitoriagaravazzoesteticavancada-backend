using Beautysoft.Enum;

namespace Beautysoft.Models
{
    public class MensagemTemporaria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool Habilitado { get; set; }
        public TipoMensagemTemporaria TipoMensagemTemporaria { get; set; }
    }
}
