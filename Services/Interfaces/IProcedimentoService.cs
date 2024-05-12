using Beautysoft.DTOs;
using Beautysoft.Models;
using BeautySoftAPI.DTOs;
using BeautySoftAPI.Models;

namespace Beautysoft.Services.Interfaces
{
    public interface IProcedimentoService
    {
        Task<List<Procedimento>> BuscarTodosProcedimentosAsync();
        Task<Procedimento> BuscarProcedimentoPorIdAsync(int ProcedimentoId);
        Task AdicionarProcedimentoAsync(Procedimento procedimento);
        Task AtualizarProcedimentoAsync(int procedimento, ProcedimentoDto procedimentoDto);
        Task DeletarProcedimentoAsync(int ProcedimentoId);
    }
}
