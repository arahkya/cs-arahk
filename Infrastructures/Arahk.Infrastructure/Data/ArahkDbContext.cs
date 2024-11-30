using Arahk.Domain.Core.Common.ValueObjects;
using Arahk.Domain.Identity.Entities;
using Arahk.Domain.Identity.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Arahk.Infrastructure.Data;

public class ArahkDbContext(DbContextOptions<ArahkDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=arahk-db;User ID=sa;Password=vkiydKN@8986;TrustServerCertificate=True;MultipleActiveResultSets=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().ToTable("Users");
        modelBuilder.Entity<UserEntity>().HasKey(p => p.Id);
        modelBuilder.Entity<UserEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<UserEntity>().Property(p => p.Username).IsRequired().HasConversion(f => f.Value, t => new UsernameValueObject(t)).HasColumnName("Username");
        modelBuilder.Entity<UserEntity>().Property(p => p.HashedPassword).IsRequired().HasConversion(f => f.Value, t => new PasswordValueObject(t)).HasColumnName("HashedPassword");
        modelBuilder.Entity<UserEntity>().Property(p => p.Email).IsRequired().HasConversion(f => f.Value, t => new EmailValueObject(t)).HasColumnName("Email");
    }
}
