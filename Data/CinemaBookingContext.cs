using Microsoft.EntityFrameworkCore;
using cinema.Models;

namespace cinema.Data
{
    public class CinemaBookingContext : DbContext
    {
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; } // New User DbSet
        public DbSet<Layout> Layouts { get; set; } // New Layout DbSet

        public CinemaBookingContext(DbContextOptions<CinemaBookingContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cinema>()
                .HasMany(c => c.Theaters)
                .WithOne(t => t.Cinema)
                .HasForeignKey(t => t.CinemaId);

            modelBuilder.Entity<Theater>()
                .HasMany(t => t.Seats)
                .WithOne(s => s.Theater)
                .HasForeignKey(s => s.TheaterId);

            modelBuilder.Entity<Theater>()
                .HasMany(t => t.Screenings)
                .WithOne(s => s.Theater)
                .HasForeignKey(s => s.TheaterId);

            modelBuilder.Entity<Layout>()
                .HasMany(l => l.Seats)
                .WithOne(s => s.Layout)
                .HasForeignKey(s => s.LayoutId);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Screenings)
                .WithOne(s => s.Movie)
                .HasForeignKey(s => s.MovieId);

            modelBuilder.Entity<Screening>()
                .HasMany(s => s.Tickets)
                .WithOne(t => t.Screening)
                .HasForeignKey(t => t.ScreeningId);

            modelBuilder.Entity<Seat>()
                .HasMany(s => s.Tickets)
                .WithOne(t => t.Seat)
                .HasForeignKey(t => t.SeatId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Tickets)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Layout>()
                .HasOne(l => l.Theater)
                .WithOne(t => t.Layout)
                .HasForeignKey<Layout>(l => l.TheaterId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust based on your requirements

            modelBuilder.Entity<Movie>()
           .Property(m => m.Genre)
           .HasConversion<string>();

            modelBuilder.Entity<Seat>()
                .Property(s => s.SeatType)
                .HasConversion<string>();

            modelBuilder.Entity<Ticket>()
                .Property(t => t.Status)
                .HasConversion<string>();
        }
    }
}
