using Microsoft.EntityFrameworkCore;
using WillMyWay.Models;

namespace WillMyWay.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}