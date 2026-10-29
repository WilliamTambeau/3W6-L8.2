namespace ZombieParty.Models.Data
{
    public interface IZombiePartyDbContextSeed
    {
        Task SeedAsync(int retry = 0);
    }
}