using Beautysoft.DTOs;
using Beautysoft.Models;
using Beautysoft.Services.Interfaces;
using BeautySoftAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Beautysoft.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly DataContext _context;

        public AgendamentoService(DataContext context)
        {
            _context = context;
        }

        public async Task<AgendamentoDto> CadastrarAgendamento(AgendamentoDto agendamentoDto)
        {
            try
            {
                var agendamento = new Agendamento
                {
                    DataHoraAgendada = agendamentoDto.DataHoraAgendada,
                    Nome = agendamentoDto.Nome,
                    Email = agendamentoDto.Email,
                    Descricao = agendamentoDto.Descricao,
                    Tempo = agendamentoDto.Tempo,
                    Valor = agendamentoDto.Valor,
                    TipoProcedimento = agendamentoDto.TipoProcedimento
                };

                _context.Agendamentos.Add(agendamento);
                await _context.SaveChangesAsync();

                return agendamentoDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar agendamento: {ex.Message}");
                throw; 
            }
        }


        public async Task<List<AgendamentoDto>> ObterTodosAgendamentos()
        {
            var agendamentos = await _context.Agendamentos
                .ToListAsync();

            var agendamentosDto = agendamentos.Select(a => new AgendamentoDto
            {
                Id = a.Id,
                DataHoraAgendada = a.DataHoraAgendada,
                Email = a.Email,
                Nome = a.Nome,
                Descricao = a.Descricao,
                Tempo = a.Tempo,
                Valor = a.Valor,
                TipoProcedimento = a.TipoProcedimento
            }).ToList();

            return agendamentosDto;
        }

        public async Task<AgendamentoDto> ObterAgendamentoPorId(int id)
        {
            var agendamento = await _context.Agendamentos
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agendamento == null)
                return null;

            var agendamentoDto = new AgendamentoDto
            {
                Id = agendamento.Id,
                DataHoraAgendada = agendamento.DataHoraAgendada,
                Email = agendamento.Email,
                Nome = agendamento.Nome,
                Descricao = agendamento.Descricao,
                Tempo = agendamento.Tempo,
                Valor = agendamento.Valor,
                TipoProcedimento = agendamento.TipoProcedimento
            };

            return agendamentoDto;
        }

        public async Task<AgendamentoDto> AtualizarAgendamento(int id, AgendamentoDto agendamentoDto)
        {
            var agendamentoExistente = await _context.Agendamentos.FindAsync(id);

            if (agendamentoExistente == null)
                return null;

            agendamentoExistente.DataHoraAgendada = agendamentoDto.DataHoraAgendada;
            agendamentoExistente.Nome = agendamentoDto.Nome;
            agendamentoExistente.Email = agendamentoDto.Email;
            agendamentoExistente.Descricao = agendamentoDto.Descricao;
            agendamentoExistente.Tempo = agendamentoDto.Tempo;
            agendamentoExistente.Valor = agendamentoDto.Valor;
            agendamentoExistente.TipoProcedimento = agendamentoDto.TipoProcedimento;

            await _context.SaveChangesAsync();

            return agendamentoDto;
        }

        public async Task<bool> DeletarAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);

            if (agendamento == null)
                return false;

            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
