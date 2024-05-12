using Beautysoft.DTOs;
using Beautysoft.Models;
using Beautysoft.Services.Interfaces;
using BeautySoftAPI.Data;
using BeautySoftAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Beautysoft.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly DataContext _context;

        public VoucherService(DataContext context)
        {
            _context = context;
        }

        public async Task AdicionarVoucherAsync(Voucher Voucher)
        {
            _context.Vouchers.Add(Voucher);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarVoucherAsync(int VoucherId, VoucherDto voucherDto)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Id == VoucherId);
            if (voucher != null)
            {
                voucher.Nome = voucherDto.Nome;
                voucher.Descricao = voucherDto.Descricao;
                voucher.Campo = voucherDto.Campo;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Voucher>> BuscarTodosVouchersAsync()
        {
            return await _context.Vouchers.ToListAsync();
        }

        public async Task<Voucher> BuscarVoucherPorIdAsync(int VoucherId) =>
              await _context.Vouchers.FindAsync(VoucherId);

        public async Task DeletarVoucherAsync(int VoucherId)
        {
            var voucher = await _context.Vouchers.FindAsync(VoucherId);

            _context.Vouchers.Remove(voucher);
            await _context.SaveChangesAsync();
        }
    }
}
