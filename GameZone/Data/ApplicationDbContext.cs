using GameZone.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameZone.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<UpcomingGame> UpcomingGames {  get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevice>()
                .HasKey(e => new { e.GameId, e.DeviceId });
            modelBuilder.Entity<Category>()
                .HasData(new Category[]
                {
                    new Category { Id = 1,Name="Sports" },
                    new Category { Id = 2,Name="Action" },
                    new Category { Id = 3,Name="strategy" },
                    new Category { Id = 4,Name="Adventure" },
                    new Category { Id = 5,Name="Racing" },
                    new Category { Id = 6,Name="Film" },
                    new Category { Id = 7,Name="Fighting" }
                });
            modelBuilder.Entity<Device>()
                .HasData(new Device[] {
                    new Device { Id = 1,Name="Playstation",Icon="bi bi-playstation" },
                    new Device {Id =2,Name="Xbox",Icon="bi bi-xbox"},
                    new Device {Id =3,Name="Nitendo",Icon="bi bi-nitendoo-switch" },
                    new Device {Id =4,Name="PC",Icon="bi bi-pc-display" }
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
