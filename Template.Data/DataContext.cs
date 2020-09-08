namespace Template.DAL
{
    using Microsoft.EntityFrameworkCore;

    using Template.DAL.Models;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>().HasIndex(table => table.Email);

            // Claims
            modelBuilder.Entity<UserClaim>().HasKey(table => new { table.UserId, table.Claim });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }
    }
}