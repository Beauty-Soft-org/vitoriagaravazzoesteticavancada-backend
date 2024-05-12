using Beautysoft.DTOs;
using Beautysoft.Models;
using Beautysoft.Services.Interfaces;
using BeautySoftAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Beautysoft.Services
{
    public class ProcedimentoService : IProcedimentoService
    {
        private readonly DataContext _context;

        public ProcedimentoService(DataContext context)
        {
            _context = context;
        }

        public async Task<Procedimento> BuscarProcedimentoPorIdAsync(int procedimentoId) =>
              await _context.Procedimentos.FindAsync(procedimentoId);

        public async Task AdicionarProcedimentoAsync(Procedimento procedimento)
        {
            _context.Procedimentos.Add(procedimento);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarProcedimentoAsync(int ProcedimentoId, ProcedimentoDto procedimentoDto)
        {
            var procedimento = await _context.Procedimentos.FirstOrDefaultAsync(h => h.Id == ProcedimentoId);
            if (procedimento != null)
            {
                procedimento.Nome = procedimentoDto.Nome;
                procedimento.Descricao = procedimentoDto.Descricao;
                procedimento.Imagem = procedimentoDto.Imagem;
                procedimento.Valor = procedimentoDto.Valor;
                procedimento.Tempo = procedimentoDto.Tempo;
                procedimento.TipoProcedimento = procedimentoDto.TipoProcedimento;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletarProcedimentoAsync(int ProcedimentoId)
        {
            var procedimento = await _context.Procedimentos.FindAsync(ProcedimentoId);

            _context.Procedimentos.Remove(procedimento);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Procedimento>> BuscarTodosProcedimentosAsync()
        {
            return await _context.Procedimentos.ToListAsync();
        }

    }
}
