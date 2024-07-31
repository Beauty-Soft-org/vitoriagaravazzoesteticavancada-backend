using Beautysoft.DTOs;
using Beautysoft.Models;
using Beautysoft.Services.Interfaces;
using BeautySoftAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beautysoft.Services
{
    public class HorariosService : IHorariosService
    {
        private readonly DataContext _context;

        public HorariosService(DataContext context)
        {
            _context = context;
        }

        public async Task<Horarios> BuscarHorarioPorIdAsync(int horarioId)
        {
            return await _context.Horarios.FindAsync(horarioId);
        }

        public async Task AdicionarHorarioAsync(Horarios horario)
        {
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarHorarioAsync(int horarioId)
        {
            var horario = await _context.Horarios.FindAsync(horarioId);
            if (horario != null)
            {
                _context.Horarios.Remove(horario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Horarios>> BuscarTodosHorariosAsync()
        {
            return await _context.Horarios.ToListAsync();
        }

        public async Task<Horarios> BuscarHorarioPorDataAsync(DateTime data)
        {
            return await _context.Horarios.FirstOrDefaultAsync(h => h.Data.Date == data.Date);
        }

        public async Task<List<Horarios>> ObterHorariosFiltrados(DateTime? dataInicial, DateTime? dataFinal)
        {
            var query = _context.Horarios.AsQueryable();

            if (dataInicial.HasValue && dataFinal.HasValue)
            {
                query = query.Where(h => h.Data.Date >= dataInicial.Value.Date && h.Data.Date <= dataFinal.Value.Date);
            }
            else if (dataInicial.HasValue)
            {
                query = query.Where(h => h.Data.Date >= dataInicial.Value.Date);
            }
            else if (dataFinal.HasValue)
            {
                query = query.Where(h => h.Data.Date <= dataFinal.Value.Date);
            }

            return await query.ToListAsync();
        }
    }
}
