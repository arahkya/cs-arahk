using Arahk.Domain.Core.Common.ValueObjects;
using Arahk.Domain.Identity.Entities;
using Arahk.Domain.Identity.ValueObjects;
using Arahk.Domain.Membership.Entities;
using Arahk.Domain.Membership.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Arahk.Infrastructure.Data;

public class ArahkDbContext(DbContextOptions<ArahkDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserProfileEntity> UserProfiles { get; set; }
    public DbSet<SalutationEntity> Salutations { get; set; }

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

        modelBuilder.Entity<SalutationEntity>().ToTable("Salutations");
        modelBuilder.Entity<SalutationEntity>().HasKey(p => p.Id);
        modelBuilder.Entity<SalutationEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<SalutationEntity>().HasData(new[]
        {
            new SalutationEntity { Id = Guid.Parse("1E60B16C-F3A6-4029-AF97-8E644C158D3B"), Name = "Mr" },
            new SalutationEntity { Id = Guid.Parse("8164009B-952E-4F0B-822E-FB40F1BD08F2"), Name = "Ms" },
            new SalutationEntity { Id = Guid.Parse("1030D779-5C7F-489F-A473-516CA163E2CA"), Name ="Mrs" }
        });
        
        modelBuilder.Entity<UserProfileEntity>().ToTable("UserProfiles");
        modelBuilder.Entity<UserProfileEntity>().HasKey(p => p.Id);
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.Firstname).IsRequired().HasMaxLength(50).HasConversion(f => f.Value, t => new PersonNameValueObject(t)).HasColumnName("Firstname");
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.Lastname).IsRequired().HasMaxLength(50).HasConversion(f => f.Value, t => new PersonNameValueObject(t)).HasColumnName("Lastname");
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.Gender).IsRequired().HasMaxLength(10).HasConversion(f => f.Value, t => new GenderValueObject(t)).HasColumnName("Gender");
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.PrimaryMobilePhone).IsRequired().HasMaxLength(15).HasConversion(f => f.Value, t => new MobilePhoneValueObject(t)).HasColumnName("PrimaryMobilePhone");
        modelBuilder.Entity<UserProfileEntity>().HasOne<UserEntity>().WithOne().HasForeignKey<UserProfileEntity>(k => k.UserIdentityId);
        modelBuilder.Entity<UserProfileEntity>().Property(p => p.Salutation).IsRequired().HasMaxLength(50).HasConversion(f => f.Value, t => new SalutationValueObject(t)).HasColumnName("Salutation");
    }
}
