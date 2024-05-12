using Beautysoft.DTOs;
using Beautysoft.Models;
using Beautysoft.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Beautysoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemTemporariaController : ControllerBase
    {
        private readonly IMensagemTemporariaService _mtService;

        public MensagemTemporariaController(IMensagemTemporariaService mtService)
        {
            this._mtService = mtService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MensagemTemporaria>>> BuscarTodasMensagensTemporarias()
        {
            var mensagemTemporarias = await _mtService.BuscarTodasMensagensTemporariasAsync();
            return Ok(mensagemTemporarias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MensagemTemporaria>> BuscarMensagemTemporariaPorId(int id)
        {
            var usuario = await _mtService.BuscarMensagemTemporariaPorIdAsync(id);
            if (usuario == null) return NotFound("Id não encontrado.");
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<MensagemTemporaria>> AdicionarMensagemTemporaria([FromBody] MensagemTemporaria mensagemTemporaria)
        {
            await _mtService.AdicionarMensagemTemporariaAsync(mensagemTemporaria);
            return CreatedAtAction(nameof(BuscarMensagemTemporariaPorId), new { id = mensagemTemporaria.Id }, mensagemTemporaria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarMensagemTemporaria(int id, [FromBody] MensagemTemporariaDTO mensagemTemporariaDto)
        {
            if (mensagemTemporariaDto == null) return BadRequest("Dados inválidos para a Mensagem Temporaria.");

            await _mtService.AtualizarMensagemTemporariaAsync(id, mensagemTemporariaDto);

            return Ok(new { message = "A Mensagem Temporaria foi atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarMensagemTemporaria(int id)
        {
            if (id == null) return BadRequest("Id não encontrado.");
            await _mtService.DeletarMensagemTemporariaAsync(id);

            return Ok("Mensagem Temporaria deletado com Sucesso!");
        }
    }
}
