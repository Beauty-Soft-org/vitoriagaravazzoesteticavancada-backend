using Beautysoft.DTOs;
using Beautysoft.Models;

namespace Beautysoft.Services.Interfaces
{
    public interface IMensagemTemporariaService
    {
        Task<List<MensagemTemporaria>> BuscarTodasMensagensTemporariasAsync();
        Task<MensagemTemporaria> BuscarMensagemTemporariaPorIdAsync(int mensagemTemporariaId);
        Task AdicionarMensagemTemporariaAsync(MensagemTemporaria mensagemTemporaria);
        Task AtualizarMensagemTemporariaAsync(int mensagemTemporariaId, MensagemTemporariaDTO mensagemTemporariaDto);
        Task DeletarMensagemTemporariaAsync(int mensagemTemporariaId);
    }
}
