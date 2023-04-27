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
        public DbSet<Song> Songs { get; set; }
        public DbSet<Emotion> Emotions { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }

    }
}
