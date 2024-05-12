using Beautysoft.Models;
using Beautysoft.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beautysoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilUsuarioController : ControllerBase
    {
        private readonly IPerfilUsuarioService _puService;

        public PerfilUsuarioController(IPerfilUsuarioService puService)
        {
            _puService = puService;
        }


        [HttpGet]
        public async Task<ActionResult<List<PerfilUsuario>>> BuscarUsuarios()
        {
            var perfis = await _puService.BuscarTodosPerfisUsuariosAsync();
            return Ok(perfis);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<PerfilUsuario>> BuscarPerfilUsuarioPorId(string email)
        {
            var perfil = await _puService.BuscarPerfilUsuarioPorEmailAsync(email);
            if (perfil == null) return NotFound("E-mail não encontrado.");
            return Ok(perfil);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPerfil(int id, [FromBody] PerfilUsuario perfilUsuario)
        {
            if (perfilUsuario == null) return BadRequest("Dados inválidos para o Perfil.");

            await _puService.AtualizarPerfilUsuarioAsync(id, perfilUsuario);

            return Ok(new { message = "O Perfil de usuário foi atualizado com êxito." });
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarPerfil([FromBody] PerfilUsuario perfilUsuario)
        {
            if (perfilUsuario == null)
            {
                return BadRequest("Dados inválidos para o Perfil.");
            }

            try
            {
                await _puService.CadastrarPerfilUsuarioAsync(perfilUsuario);
                return Ok(new { message = "O Perfil de usuário foi cadastrado com êxito." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro durante o cadastro do perfil de usuário.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPerfil(int id)
        {
            if (id == null) return BadRequest("Id não encontrado.");
            await _puService.DeletarPerfilUsuarioAsync(id);

            return Ok("Perfil de usuário deletado com Sucesso!");
        }

    }
}
