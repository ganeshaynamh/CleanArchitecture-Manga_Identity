namespace Manga.Infrastructure.EntityFrameworkDataAccess
{
    using System;
    using Manga.Domain.Accounts;
    using Manga.Domain.Customers;
    using Manga.Domain.UserModel;
    using Manga.Domain.ValueObjects;
    
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public sealed class MangaContext : IdentityDbContext<ApplicationUser>
    {
        public MangaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Users { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Debit> Debits { get; set; }
        
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Account>()
                .ToTable("Account");

            modelBuilder.Entity<Customer>()
                .ToTable("Users")
                .Property(b => b.SSN)
                .HasConversion(
                    v => v.ToString(),
                    v => (v));

            modelBuilder.Entity<Customer>()
                .ToTable("Users")
                .Property(b => b.UserName)
                .HasConversion(
                    v => v.ToString(),
                    v => (v));

            modelBuilder.Entity<Debit>()
                .ToTable("Debit")
                .Property(b => b.Amount)
                .HasConversion(
                    v => v.ToAmount().ToDouble(),
                    v => new PositiveAmount(v));

            modelBuilder.Entity<Credit>()
                .ToTable("Credit")
                .Property(b => b.Amount)
                .HasConversion(
                    v => v.ToAmount().ToDouble(),
                    v => new PositiveAmount(v));

            modelBuilder.Entity<Customer>().HasData(
                new { Id = new Guid("197d0438-e04b-453d-b5de-eca05960c6ae"), Name = "Test User", SSN = "19860817-9999" }
            );

            modelBuilder.Entity<Account>().HasData(
                new { Id = new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"), CustomerId = new Guid("197d0438-e04b-453d-b5de-eca05960c6ae") }
            );

            modelBuilder.Entity<Credit>().HasData(
                new { 
                    Id = new Guid("f5117315-e789-491a-b662-958c37237f9b"),
                    AccountId = new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"),
                    Amount = new PositiveAmount(400),
                    Description = "Credit",
                    TransactionDate = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<Debit>().HasData(
                new { 
                    Id = new Guid("3d6032df-7a3b-46e6-8706-be971e3d539f"),
                    AccountId = new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"),
                    Amount = new PositiveAmount(400),
                    Description = "Debit",
                    TransactionDate = DateTime.UtcNow
                }
            );

            //modelBuilder.Entity<ApplicationUser>(e => e.ToTable("Users").HasKey(x => x.Id));

            //modelBuilder.Entity<IdentityRole<string>>(e => e.ToTable("Roles").HasKey(x => x.Id));

            //modelBuilder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaim").HasKey(x => x.Id));

            //modelBuilder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles").HasKey(x => x.RoleId));

            //modelBuilder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogin").HasKey(x => x.UserId));

            //modelBuilder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims").HasKey(x => x.Id));

            //modelBuilder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens").HasKey(x => x.UserId));


        }
    }
}