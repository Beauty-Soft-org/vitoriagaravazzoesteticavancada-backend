using Beautysoft.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beautysoft.Services.Interfaces
{
    public interface IAgendamentoService
    {
        Task<List<AgendamentoDto>> ObterTodosAgendamentos();
        Task<AgendamentoDto> CadastrarAgendamento(AgendamentoDto agendamento);
        Task<AgendamentoDto> ObterAgendamentoPorId(int id);
        Task<AgendamentoDto> AtualizarAgendamento(int id, AgendamentoDto agendamento);
        Task<bool> DeletarAgendamento(int id);

    }
}
