using BeautySoftAPI.DTOs;
using BeautySoftAPI.Models;
using BeautySoftAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Beautysoft.DTOs;
using Beautysoft.Services;

namespace BeautySoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        private readonly IConfiguration _config;

        private readonly EmailService _emailService;

        public UsuariosController(IUsuarioService usuarioService, IConfiguration configuration, EmailService emailService)
        {
            _usuarioService = usuarioService;
            _config = configuration;
            _emailService = emailService;
        }


        [HttpPost("Registrar")]
        public async Task<ActionResult<Usuario>> RegistrarUsuario([FromBody] RegistroDto request)
        {
            await _usuarioService.RegistrarUsuarioAsync(request);

            // Enviar email de Verificação
            var subject = "Confirme seu email";
            var body = $"Olá {request.NomeUsuario},\n\nPor favor, confirme seu email clicando no link a seguir: <a href=\"\">Confirmar Email</a>";
            await _emailService.SendEmailAsync(request.EnderecoEmail, subject, body);

            return Ok(request);
        }
        [HttpPost("email/{email}")]
        public async Task<IActionResult> BuscarUsuarioPorEmail([FromBody] ResetarSenhaDto model)
        {
            var user = await _usuarioService.BuscarUsuarioPorEmailAsync(model.EnderecoEmail);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            // Gere um token de redefinição de senha
            if (user == null)
                return Unauthorized();

            var token = GerarToken(user);

            return Ok(new
            {
                Status = user.Status,
                Email = user.EnderecoEmail,
                Token = token
            });

            // Aqui você pode adicionar um serviço de email para enviar o token de redefinição para o usuário
            // await _emailService.SendPasswordResetEmail(user.Email, resetToken);
        }
        [HttpPost]
        public async Task<IActionResult> ResetarSenha([FromBody] ResetarSenhaDto model)
        {
            var resetTokenIsValid = await _usuarioService.ValidaReseteToken(model.EnderecoEmail, model.Token);

            if (!resetTokenIsValid)
                return BadRequest("Token de redefinição de senha inválido.");

            await _usuarioService.ResetarSenha(model.EnderecoEmail, model.NovaSenha);

            return Ok("Senha redefinida com sucesso.");
        }

        [HttpPost("Login")]
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
