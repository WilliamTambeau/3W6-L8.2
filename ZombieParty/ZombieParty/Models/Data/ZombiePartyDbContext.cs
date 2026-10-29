using Microsoft.EntityFrameworkCore;

namespace ZombieParty.Models.Data
{
    public class ZombiePartyDbContext : DbContext
    {
        public ZombiePartyDbContext(DbContextOptions<ZombiePartyDbContext> options) : base(options)
        {

        }

        public DbSet<Zombie> Zombie { get; set; }
        public DbSet<ZombieType> ZombieType { get; set; }
        public DbSet<Weapon> Weapon { get; set; }
    }
}
