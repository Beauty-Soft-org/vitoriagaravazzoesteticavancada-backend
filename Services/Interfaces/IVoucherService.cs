using Beautysoft.DTOs;
using Beautysoft.Models;
using BeautySoftAPI.DTOs;
using BeautySoftAPI.Models;

namespace Beautysoft.Services.Interfaces
{
    public interface IVoucherService
    {
        Task<List<Voucher>> BuscarTodosVouchersAsync();
        Task<Voucher> BuscarVoucherPorIdAsync(int VoucherId);
        Task AdicionarVoucherAsync(Voucher Voucher);
        Task AtualizarVoucherAsync(int VoucherId, VoucherDto voucherDto);
        Task DeletarVoucherAsync(int VoucherId);
    }
}
