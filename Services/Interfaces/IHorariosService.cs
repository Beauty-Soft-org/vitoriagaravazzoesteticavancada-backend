using Beautysoft.DTOs;
using Beautysoft.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beautysoft.Services.Interfaces
{
    public interface IHorariosService
    {
        Task<Horarios> BuscarHorarioPorIdAsync(int horarioId);
        Task AdicionarHorarioAsync(Horarios horario);
        Task DeletarHorarioAsync(int horarioId);
        Task<List<Horarios>> BuscarTodosHorariosAsync();
        Task<Horarios> BuscarHorarioPorDataAsync(DateTime date);
        Task<List<Horarios>> ObterHorariosFiltrados(DateTime? dataInicial, DateTime? dataFinal);
    }
}
