using Arahk.Domain.Core.Common.ValueObjects;
using Arahk.Domain.Identity.Entities;
using Arahk.Domain.Identity.ValueObjects;
using Arahk.Domain.Membership.Entities;
using Arahk.Domain.Membership.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Arahk.Infrastructure.Data;

public class ArahkDbContext(DbContextOptions<ArahkDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserProfileEntity> UserProfiles { get; set; }
    public DbSet<SalutationValueObject> Salutations { get; set; }

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
        
        modelBuilder.Entity<SalutationValueObject>().ToTable("Salutations");
        modelBuilder.Entity<SalutationValueObject>().HasKey(p => p.Value);
        modelBuilder.Entity<SalutationValueObject>().Property(p => p.Value).IsRequired().HasMaxLength(20).HasColumnName("Name");
        modelBuilder.Entity<SalutationValueObject>().HasData(new[]
        {
            new SalutationValueObject("Mr"),
            new SalutationValueObject("Ms"),
            new SalutationValueObject("Mrs")
        });
        
        modelBuilder.Entity<UserProfileEntity>().ToTable("UserProfiles");
        modelBuilder.Entity<UserProfileEntity>().HasKey(p => p.Id);
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.Firstname).IsRequired().HasMaxLength(50).HasConversion(f => f.Value, t => new PersonNameValueObject(t)).HasColumnName("Firstname");
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.Lastname).IsRequired().HasMaxLength(50).HasConversion(f => f.Value, t => new PersonNameValueObject(t)).HasColumnName("Lastname");
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.Salutation).HasConversion(f => f.Value, t => new SalutationValueObject(t)).HasColumnName("Salutation");
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.Gender).IsRequired().HasMaxLength(10).HasConversion(f => f.Value, t => new GenderValueObject(t)).HasColumnName("Gender");
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.PrimaryMobilePhone).IsRequired().HasMaxLength(15).HasConversion(f => f.Value, t => new MobilePhoneValueObject(t)).HasColumnName("PrimaryMobilePhone");
        modelBuilder.Entity<UserProfileEntity>().HasOne<UserEntity>().WithOne().HasForeignKey<UserProfileEntity>(k => k.UserIdentityId);
        modelBuilder.Entity<UserProfileEntity>().HasOne<SalutationValueObject>().WithMany().HasForeignKey("SalutationId");
    }
}
