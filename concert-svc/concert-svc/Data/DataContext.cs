using concert_svc.Entity;
using Microsoft.EntityFrameworkCore;

namespace concert_svc.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Concert> Concert { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
    }
}
