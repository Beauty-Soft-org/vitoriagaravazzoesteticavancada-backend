using Beautysoft.Models;
using Beautysoft.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beautysoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorariosController : ControllerBase
    {
        private readonly IHorariosService _horariosService;

        public HorariosController(IHorariosService horariosService)
        {
            _horariosService = horariosService;
        }

        [HttpPost]
        public async Task<ActionResult<Horarios>> AddHorario(Horarios horario)
        {
            await _horariosService.AdicionarHorarioAsync(horario);
            return CreatedAtAction(nameof(GetHorarioByDate), new { date = horario.Data }, horario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHorario(int id)
        {
            try
            {
                var horario = await _horariosService.BuscarHorarioPorIdAsync(id);
                if (horario == null)
                {
                    return NotFound($"Horário com ID {id} não encontrado.");
                }

                await _horariosService.DeletarHorarioAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar horário: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horarios>>> GetAllHorarios()
        {
            var horarios = await _horariosService.BuscarTodosHorariosAsync();
            return Ok(horarios);
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<Horarios>> GetHorarioByDate(DateTime date)
        {
            var horario = await _horariosService.BuscarHorarioPorDataAsync(date);
            if (horario == null)
            {
                return NotFound();
            }
            return Ok(horario);
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<List<Horarios>>> FiltrarHorarios(
            [FromQuery] DateTime? dataInicial,
            [FromQuery] DateTime? dataFinal)
        {
            try
            {
                var horarios = await _horariosService.ObterHorariosFiltrados(dataInicial, dataFinal);

                if (horarios == null || horarios.Count == 0)
                {
                    return NotFound("Nenhum horario encontrado com os filtros fornecidos.");
                }

                return Ok(horarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
