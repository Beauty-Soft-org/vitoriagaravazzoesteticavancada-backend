using BeautySoftAPI.DTOs;
using BeautySoftAPI.Models;
using BeautySoftAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Beautysoft.DTOs;

namespace BeautySoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        private readonly IConfiguration _config;

        public UsuariosController(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _config = configuration;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<Usuario>> RegistrarUsuario([FromBody] RegistroDto request)
        {
            await _usuarioService.RegistrarUsuarioAsync(request);
            return Ok(request);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _usuarioService.AutenticarUsuario(model.EnderecoEmail, model.Senha);

            if (user == null)
                return Unauthorized();

            var token = GerarToken(user);

            return Ok(new
            {
                Status = user.Status,
                Email = user.EnderecoEmail,
                Token = token
            });
        }

        private string GerarToken(Usuario user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.EnderecoEmail),
                new Claim(ClaimTypes.Name, user.NomeUsuario)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> BuscarUsuarios()
        {
            var usuarios = await _usuarioService.BuscarTodosUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> BuscarUsuarioPorId(int id)
        {
            var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(id);
            if (usuario == null) return NotFound("Id não encontrado.");
            return Ok(usuario);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarUsuario(int id, [FromBody] UsuarioDto usuarioDTO)
        {
            if (usuarioDTO == null) return BadRequest("Dados inválidos para o usuário.");

            await _usuarioService.AtualizarUsuarioAsync(id, usuarioDTO);

            return Ok(new { message = "O usuário foi atualizado com êxito." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarUsuario(int id)
        {
            if (id == null) return BadRequest("Id não encontrado.");
            await _usuarioService.DeletarUsuarioAsync(id);

            return Ok("Usuário deletado com Sucesso!");
        }
        

    }
}
