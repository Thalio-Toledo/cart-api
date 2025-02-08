using carrinho_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace carrinho_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Witness> Witness { get; set; }
        public DbSet<Local> Locals { get; set; }
    }
}
