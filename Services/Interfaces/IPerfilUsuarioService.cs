using Beautysoft.Models;
using BeautySoftAPI.Models;

namespace Beautysoft.Services.Interfaces
{
    public interface IPerfilUsuarioService
    {
        Task<List<PerfilUsuario>> BuscarTodosPerfisUsuariosAsync();
        Task<PerfilUsuario> BuscarPerfilUsuarioPorEmailAsync(string EmailUsuario);
        Task AtualizarPerfilUsuarioAsync(int perfiUsuario, PerfilUsuario perfilUsuario);
        Task DeletarPerfilUsuarioAsync(int perfilUsuarioId);
        Task CadastrarPerfilUsuarioAsync(PerfilUsuario perfilUsuario);
    }
}
