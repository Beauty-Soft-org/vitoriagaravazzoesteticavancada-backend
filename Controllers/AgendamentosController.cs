using Beautysoft.DTOs;
using Beautysoft.Models;  // Certifique-se de importar o namespace correto para a classe Agendamento
using Beautysoft.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beautysoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentosController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentosController(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        [HttpPost]
        public async Task<ActionResult<AgendamentoDto>> CadastrarAgendamento([FromBody] AgendamentoDto agendamento)
        {
            try
            {
                var agendamentoCadastrado = await _agendamentoService.CadastrarAgendamento(agendamento);
                return Ok(agendamentoCadastrado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<AgendamentoDto>>> ObterTodosAgendamentos()
        {
            try
            {
                var agendamentos = await _agendamentoService.ObterTodosAgendamentos();
                return Ok(agendamentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgendamentoDto>> ObterAgendamentoPorId(int id)
        {
            try
            {
                var agendamento = await _agendamentoService.ObterAgendamentoPorId(id);

                if (agendamento == null)
                    return NotFound();

                return Ok(agendamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<AgendamentoDto>> AtualizarAgendamento(int id, [FromBody] AgendamentoDto agendamento)
        {
            try
            {
                var agendamentoAtualizado = await _agendamentoService.AtualizarAgendamento(id, agendamento);

                if (agendamentoAtualizado == null)
                    return NotFound();

                return Ok(agendamentoAtualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarAgendamento(int id)
        {
            try
            {
                var agendamentoDeletado = await _agendamentoService.DeletarAgendamento(id);

                if (!agendamentoDeletado)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
