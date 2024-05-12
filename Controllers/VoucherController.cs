using Beautysoft.DTOs;
using Beautysoft.Models;
using Beautysoft.Services;
using Beautysoft.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Beautysoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            this._voucherService = voucherService;

        }


        [HttpGet]
        public async Task<ActionResult<List<Voucher>>> BuscarVouchers()
        {
            var vouchers = await _voucherService.BuscarTodosVouchersAsync();
            return Ok(vouchers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Voucher>> BuscarVoucherPorId(int id)
        {
            var voucher = await _voucherService.BuscarVoucherPorIdAsync(id);
            if (voucher == null) return NotFound("Id não encontrado.");
            return Ok(voucher);
        }

        [HttpPost]
        public async Task<ActionResult<Voucher>> AdicionarProcedimento([FromBody] Voucher voucher)
        {
            await _voucherService.AdicionarVoucherAsync(voucher);
            return CreatedAtAction(nameof(BuscarVoucherPorId), new { id = voucher.Id }, voucher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarVoucher(int id, [FromBody] VoucherDto voucherDto)
        {
            if (voucherDto == null) return BadRequest("Dados inválidos para o Voucher.");

            await _voucherService.AtualizarVoucherAsync(id, voucherDto);

            return Ok(new { message = "O Voucher foi atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarVoucher(int id)
        {
            if (id == null) return BadRequest("Id não encontrado.");
            await _voucherService.DeletarVoucherAsync(id);

            return Ok("Voucher deletado com Sucesso!");
        }
    }
}
