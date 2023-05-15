using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PBL3.Models.Domain
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts)
        {}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Quan hệ nhiều - nhiều giữa Song và Emotion
            builder.Entity<Song>()
                .HasMany(song => song.Emotions)
                .WithMany(emotion => emotion.Songs)
                .UsingEntity(
                    "SongEmotions",
                    r => r.HasOne(typeof(Emotion)).WithMany().HasForeignKey("EmotionId"),
                    l => l.HasOne(typeof(Song)).WithMany().HasForeignKey("SongId"));

            // Quan hệ nhiều - nhiều giữa Song và Genre
            builder.Entity<Song>()
                .HasMany(song => song.Genres)
                .WithMany(genre => genre.Songs)
                .UsingEntity(
                    "SongGenres",
                    r => r.HasOne(typeof(Genre)).WithMany().HasForeignKey("GenreId"),
                    l => l.HasOne(typeof(Song)).WithMany().HasForeignKey("SongId"));

            // Quan hệ nhiều - nhiều giữa User và Story
            builder.Entity<AppUser>()
                .HasMany(user => user.Stories)
                .WithMany(story => story.Users)
                .UsingEntity(
                    "AppUserStories",
                    r => r.HasOne(typeof(Story)).WithMany().HasForeignKey("StoryId"),
                    l => l.HasOne(typeof(AppUser)).WithMany().HasForeignKey("UserId"));
        }
        public static async Task SeedData(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            UserManager<AppUser> userMgr = 
                serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleMgr = 
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            // seed roles
            if (!await roleMgr.RoleExistsAsync(Role.Admin))
            {
                await roleMgr.CreateAsync(new IdentityRole(Role.Admin));
            }
            if (!await roleMgr.RoleExistsAsync(Role.Member))
            {
                await roleMgr.CreateAsync(new IdentityRole(Role.Member));
            }
            // seed admin account
            string email = configuration["Data:Admin:Email"];
            if (await userMgr.FindByEmailAsync(email) == null)
            {
                AppUser admin = new AppUser()
                {
                    Email = email,
                    UserName = email
                };
                await userMgr.CreateAsync(admin, configuration["Data:Admin:Password"]);
                await userMgr.AddToRoleAsync(admin, Role.Admin);
            }
        }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Emotion> Emotions { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }

    }
}
