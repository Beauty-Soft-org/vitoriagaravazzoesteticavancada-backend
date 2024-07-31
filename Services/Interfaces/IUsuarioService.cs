using Beautysoft.DTOs;
using Beautysoft.Models;
using BeautySoftAPI.DTOs;
using BeautySoftAPI.Models;

namespace BeautySoftAPI.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> BuscarTodosUsuariosAsync(); 
        Task<Usuario> BuscarUsuarioPorEmailAsync(string email);
        Task<Usuario> BuscarUsuarioPorIdAsync(int usuarioId);
        Task AtualizarUsuarioAsync(int usuario, UsuarioDto usuarioDTO);
        Task DeletarUsuarioAsync(int usuarioId);

        Task<RegistroDto> RegistrarUsuarioAsync(RegistroDto registro);
        Task<Usuario> AutenticarUsuario(string email, string senha);
        Task ResetarSenha(string email, string novaSenha);
        Task<bool> ValidaReseteToken(string email, string token);
    }
}
