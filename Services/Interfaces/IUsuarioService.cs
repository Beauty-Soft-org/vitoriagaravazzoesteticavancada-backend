using Beautysoft.DTOs;
using Beautysoft.Models;
using BeautySoftAPI.DTOs;
using BeautySoftAPI.Models;

namespace BeautySoftAPI.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> BuscarTodosUsuariosAsync();
        Task<Usuario> BuscarUsuarioPorIdAsync(int usuarioId);
        Task AtualizarUsuarioAsync(int usuario, UsuarioDto usuarioDTO);
        Task DeletarUsuarioAsync(int usuarioId);

        Task<RegistroDto> RegistrarUsuarioAsync(RegistroDto registro);
        Task<Usuario> AutenticarUsuario(string email, string senha);
    }
}
