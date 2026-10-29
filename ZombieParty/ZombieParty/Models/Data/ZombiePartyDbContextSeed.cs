using Bogus;
using Microsoft.EntityFrameworkCore;

namespace ZombieParty.Models.Data
{

    #region ZombiePartyDbContextSeed
    public class ZombiePartyDbContextSeed : IZombiePartyDbContextSeed
    {
        private ILogger<ZombiePartyDbContextSeed> logger;
        protected ZombiePartyDbContext context;

        public ZombiePartyDbContextSeed(ILogger<ZombiePartyDbContextSeed> logger, ZombiePartyDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task SeedAsync(int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;
                retryForAvailability++;
                logger.LogError(ex.Message);
                await this.SeedAsync(retryForAvailability);
                throw;
            }
            try
            {
                var zombieTypes = GetZombieTypes();
                if (await context.ZombieType.CountAsync() != zombieTypes.Count)
                {
                    context.ZombieType.RemoveRange(context.ZombieType);
                    await context.ZombieType.AddRangeAsync(zombieTypes);
                    await context.SaveChangesAsync();
                }
                var zombies = GetZombies();
                if (await context.Zombie.CountAsync() != zombies.Count)
                {
                    context.Zombie.RemoveRange(context.Zombie);
                    await context.Zombie.AddRangeAsync(zombies);
                    await context.SaveChangesAsync();
                }
                var weapons = GetWeapons();
                if (await context.Weapon.CountAsync() != weapons.Count)
                {
                    context.Weapon.RemoveRange(context.Weapon);
                    await context.Weapon.AddRangeAsync(weapons);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
        protected virtual List<ZombieType> GetZombieTypes() => new List<ZombieType>();
        protected virtual List<Zombie> GetZombies() => new List<Zombie>();
        protected virtual List<Weapon> GetWeapons() => new List<Weapon>();
    }

    #endregion

    #region ZombiePartyDbContextSeedDev
    public class ZombiePartyDbContextSeedDev : ZombiePartyDbContextSeed
    {
        protected int numberZombieType = 15;
        protected int numberZombie = 100;
        protected int numberWeapon = 50;

        public ZombiePartyDbContextSeedDev(ILogger<ZombiePartyDbContextSeed> logger, ZombiePartyDbContext context) : base(logger, context)
        {
            Randomizer.Seed = new Random(2546);
        }

        protected override List<ZombieType> GetZombieTypes()
        {
            var zombieType = new Faker<ZombieType>()
                .RuleFor(x => x.Point, (f, o) => f.Random.Int(2, 5))
                .RuleFor(x => x.TypeName, (f, o) => f.Hacker.Noun());
            return zombieType.Generate(numberZombieType);
        }

        protected override List<Zombie> GetZombies()
        {
            var listZombieTypeIds = this.context.ZombieType.Select(x => x.Id).ToList();
            var zombie = new Faker<Zombie>()
                .RuleFor(x => x.Point, (f, o) => f.Random.Int(1, 10))
                .RuleFor(x => x.ZombieTypeId, (f, o) => f.Random.CollectionItem(listZombieTypeIds))
                .RuleFor(x => x.Image, (f, o) => null)
                .RuleFor(x => x.Name, (f, o) => f.Name.FirstName())
                .RuleFor(x => x.Force, (f, o) => f.Random.Int(0, 10000))
                .RuleFor(x => x.ShortDesc, (f, o) => f.Lorem.Text());
            return zombie.Generate(numberZombie);
        }

        protected override List<Weapon> GetWeapons()
        {
            var weapons = new List<string>() {
            "Saber",
            "Sword",
            "Knife",
            "shuriken",
            "Foil",
            "Bow",
            "Crossbow"
        };

            var weapon = new Faker<Weapon>()
                .RuleFor(x => x.Name, (f, o) => f.Random.CollectionItem(weapons) + " " + f.Company.CompanyName())
                .RuleFor(x => x.Force, (f, o) => f.Random.Decimal(0, 10000))
                .RuleFor(x => x.Description, (f, o) => f.Company.CatchPhrase())
                .RuleFor(x => x.QtyBought, (f, o) => f.Random.Int(0, 200))
                .RuleFor(x => x.Qty, (f, o) => f.Random.Int(0, 200))
                .RuleFor(x => x.Price, (f, o) => decimal.Parse(f.Commerce.Price()))
                .RuleFor(x => x.Lifespan, (f, o) => f.Random.Decimal(0, 5000))
                .RuleFor(x => x.Resistance, (f, o) => f.Random.Decimal(0, 2000))
                .RuleFor(x => x.UpdatedDate, (f, o) => f.Date.Between(new DateTime(2020, 01, 01), new DateTime(2022, 07, 01)))
                .RuleFor(x => x.CreatedDate, (f, o) => f.Date.Past(3, o.UpdatedDate))
                .RuleFor(x => x.Image, (f, o) => f.Image.PicsumUrl());

            return weapon.Generate(numberWeapon);
        }
    }

    #endregion

    #region ZombiePartyDbContextSeedDev
    public class ZombiePartyDbContextSeedProd : ZombiePartyDbContextSeed
    {
        public ZombiePartyDbContextSeedProd(ILogger<ZombiePartyDbContextSeed> logger, ZombiePartyDbContext context) : base(logger, context)
        {
        }

        protected override List<ZombieType> GetZombieTypes()
        {
            return new List<ZombieType>()
        {
            new ZombieType(){TypeName= "Virus", Point=1},
            new ZombieType(){TypeName= "Contact", Point=3},
            new ZombieType(){TypeName= "Fiction", Point=3},
            new ZombieType(){TypeName= "Légendaire", Point= 5}
        };
        }
    }

    #endregion

    #region ZombiePartyDbContextSeedStagingPerf
    public class ZombiePartyDbContextSeedStagingPerf : ZombiePartyDbContextSeedDev
    {
        public ZombiePartyDbContextSeedStagingPerf(ILogger<ZombiePartyDbContextSeed> logger, ZombiePartyDbContext context) : base(logger, context)
        {
            this.numberZombieType = 1000;
            this.numberZombie = 1000;
            this.numberWeapon = 1000;
        }
    }

    #endregion
}
