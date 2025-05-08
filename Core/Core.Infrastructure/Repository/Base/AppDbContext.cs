using Core.Domain.Entities;
using Core.Infrastructure.Util;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Repository.Base
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<Account>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>().ToTable("Transaction");

            modelBuilder.Entity<User>()
                .Property(e => e.CreationDate)
                .HasConversion(UtcDateTimeConversions.NonNullable);

            modelBuilder.Entity<User>()
                .Property(e => e.UpdateDate)
                .HasConversion(UtcDateTimeConversions.Nullable);

            modelBuilder.Entity<Account>()
                .Property(e => e.CreationDate)
                .HasConversion(UtcDateTimeConversions.NonNullable);

            modelBuilder.Entity<Account>()
                .Property(e => e.UpdateDate)
                .HasConversion(UtcDateTimeConversions.Nullable);
        }
    }
}
