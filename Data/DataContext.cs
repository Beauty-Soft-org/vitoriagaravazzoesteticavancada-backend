using Beautysoft.DTOs;
using Beautysoft.Models;
using BeautySoftAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySoftAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<MensagemTemporaria> MensagensTemporarias { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<PerfilUsuario> PerfisUsuarios { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

    }
}
