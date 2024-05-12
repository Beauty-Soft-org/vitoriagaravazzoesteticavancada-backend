using Beautysoft.Models;
using Beautysoft.Services.Interfaces;
using BeautySoftAPI.Data;
using BeautySoftAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Beautysoft.Services
{
    public class PerfilUsuarioService : IPerfilUsuarioService
    {
        private readonly DataContext _context;

        public PerfilUsuarioService(DataContext context)
        {
            _context = context;
        }


        public async Task AtualizarPerfilUsuarioAsync(int perfiUsuarioId, PerfilUsuario perfilUsuario)
        {
            var Pusuario = await _context.PerfisUsuarios.FirstOrDefaultAsync(h => h.Id == perfiUsuarioId);

            if (Pusuario == null)
            {
                throw new InvalidOperationException("Perfil de usuário não encontrado.");
            }

            Pusuario.Nome = perfilUsuario.Nome;
            Pusuario.Email = perfilUsuario.Email;
            Pusuario.DataNascimento = perfilUsuario.DataNascimento;
            Pusuario.Telefone = perfilUsuario.Telefone;
            Pusuario.CPF = perfilUsuario.CPF;
            Pusuario.CEP = perfilUsuario.CEP;
            Pusuario.UF = perfilUsuario.UF;
            Pusuario.Municipio = perfilUsuario.Municipio;
            Pusuario.Bairro = perfilUsuario.Bairro;
            Pusuario.Logradouro = perfilUsuario.Logradouro;
            Pusuario.NumeroCasa = perfilUsuario.NumeroCasa;
            Pusuario.Complemento = perfilUsuario.Complemento;

            await _context.SaveChangesAsync();
        }

        public async Task CadastrarPerfilUsuarioAsync(PerfilUsuario perfilUsuario)
        {
            // Verifica se já existe um perfil de usuário com o mesmo email
            bool emailExistente = await _context.PerfisUsuarios.AnyAsync(u => u.Email == perfilUsuario.Email);

            if (emailExistente)
            {
                throw new InvalidOperationException("Já existe um perfil de usuário cadastrado com este email.");
            }

            _context.PerfisUsuarios.Add(perfilUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task<PerfilUsuario> BuscarPerfilUsuarioPorEmailAsync(string email) =>
            await _context.PerfisUsuarios.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<List<PerfilUsuario>> BuscarTodosPerfisUsuariosAsync()
        {
            return await _context.PerfisUsuarios
               .Select(u => new PerfilUsuario
               {
                   Id = u.Id,
                   Nome = u.Nome ?? string.Empty,
                   Email = u.Email ?? string.Empty,
                   DataNascimento = u.DataNascimento,
                   Telefone = u.Telefone,
                   CPF = u.CPF ?? string.Empty,
                   CEP = u.CEP,
                   UF = u.UF ?? string.Empty,
                   Municipio = u.Municipio ?? string.Empty,
                   Bairro = u.Bairro ?? string.Empty,
                   Logradouro = u.Logradouro?? string.Empty,
                   NumeroCasa = u.NumeroCasa,
                   Complemento = u.Complemento ?? string.Empty,
               })
               .ToListAsync();
        }

        public async Task DeletarPerfilUsuarioAsync(int perfilUsuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(perfilUsuarioId);

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

        }

       
    }
}
