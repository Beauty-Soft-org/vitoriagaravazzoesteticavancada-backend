using System;

namespace Beautysoft.Models
{
    public class Horarios
    {
        public int Id { get; set; }
        public required string HorarioInicioPrimeiroPeriodo { get; set; }
        public required string HorarioFimPrimeiroPeriodo { get; set; }
        public required string HorarioInicioSegundoPeriodo { get; set; }
        public required string HorarioFimSegundoPeriodo { get; set; }
        public DateTime Data { get; set; }
    }
}
