using Microsoft.EntityFrameworkCore;
using BeautySoftAPI.Data;
using BeautySoftAPI.DTOs;
using BeautySoftAPI.Models;
using BeautySoftAPI.Services.Interfaces;
using Beautysoft.DTOs;
using Beautysoft.Models;

namespace BeautySoftAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DataContext _context;

        public UsuarioService(DataContext context)
        {
            _context = context;
        }


        public async Task<Usuario> BuscarUsuarioPorIdAsync(int Id) =>
              await _context.Usuarios.FindAsync(Id);



        public async Task AtualizarUsuarioAsync(int usuarioId, UsuarioDto usuarioDto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(h => h.Id == usuarioId);
            if (usuario != null)
            {
                usuario.NomeUsuario = usuarioDto.NomeUsuario;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletarUsuarioAsync(int usuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Usuario>> BuscarTodosUsuariosAsync()
        {
            return await _context.Usuarios
                .Select(u => new Usuario
                {
                    Id = u.Id,
                    NomeUsuario = u.NomeUsuario ?? string.Empty,
                    DataNascimento = u.DataNascimento,
                    Genero = u.Genero ?? string.Empty,
                    Status = u.Status ?? string.Empty,
                    CPF = u.CPF ?? string.Empty,
                    Telefone = u.Telefone ?? string.Empty,
                    EnderecoEmail = u.EnderecoEmail ?? string.Empty,
                    SenhaHash = u.SenhaHash ?? string.Empty,
                    ConfirmSenhaHash = u.ConfirmSenhaHash ?? string.Empty
                })
                .ToListAsync();
        }

        public async Task<RegistroDto> RegistrarUsuarioAsync(RegistroDto registro)
        {
            Usuario registro1 = new Usuario();
            registro1.NomeUsuario = registro.NomeUsuario;
            registro1.SenhaHash = BCrypt.Net.BCrypt.HashPassword(registro.Senha);
            registro1.ConfirmSenhaHash = BCrypt.Net.BCrypt.HashPassword(registro.ConfirmSenha);
            registro1.EnderecoEmail = registro.EnderecoEmail;

            _context.Usuarios.Add(registro1);
            await _context.SaveChangesAsync();
            return registro;

        }

        public async Task<Usuario> AutenticarUsuario(string email, string senha)
        {
            // Verificar se o usuário com o e-mail fornecido existe no banco de dados
            var usuario = await _context.Usuarios
                .Where(u => u.EnderecoEmail == email)
                .Select(u => new Usuario
                {
                    Id = u.Id,
                    NomeUsuario = u.NomeUsuario ?? string.Empty,
                    DataNascimento = u.DataNascimento, // Certifique-se de que DataNascimento pode ser null
                    Genero = u.Genero ?? string.Empty,
                    Status = u.Status ?? string.Empty,
                    CPF = u.CPF ?? string.Empty,
                    Telefone = u.Telefone ?? string.Empty,
                    EnderecoEmail = u.EnderecoEmail ?? string.Empty,
                    SenhaHash = u.SenhaHash ?? string.Empty,
                    ConfirmSenhaHash = u.ConfirmSenhaHash ?? string.Empty
                })
                .FirstOrDefaultAsync();

            // Verificar se a senha fornecida corresponde à senha armazenada (utilize um algoritmo de hash aqui)
            if (usuario != null && VerificarSenhaHash(senha, usuario.SenhaHash))
            {
                return usuario;
            }

            return null; // Autenticação falhou
        }


        private bool VerificarSenhaHash(string senha, string senhaHash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaHash);
        }

    }
}
