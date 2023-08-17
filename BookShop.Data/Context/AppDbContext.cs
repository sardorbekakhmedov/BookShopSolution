using BookShop.Domain.Entities;
using BookShop.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.Context;

public sealed class AppDbContext : DbContext
{
    private readonly Guid _roleId = Guid.NewGuid();
    private readonly Guid _userId = Guid.NewGuid();

    public DbSet<User> Users => Set<User>();
    public DbSet<Image> Images => Set<Image>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Discount> Discounts => Set<Discount>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = _roleId,
                    Name = UserRoles.SuperAdmin,
                    CreatedDate = DateTime.UtcNow
                },
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = UserRoles.Admin,
                    CreatedDate = DateTime.UtcNow
                },
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = UserRoles.Manager,
                    CreatedDate = DateTime.UtcNow
                },
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = UserRoles.User,
                    CreatedDate = DateTime.UtcNow
                });

        modelBuilder.Entity<User>()
            .HasData (new User()
            {
                Id = _userId,
                Firstname = "Admin",
                Username = "SuperAdmin",
                PasswordHash = "zxcvasdfqwer1234",

                CreatedDate = DateTime.UtcNow,
                Roles = new List<Role>(),
                Books = new List<Book>(),
                UserImages = new List<Image>()
            });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateTimeStampForBaseEntityClass();
        return base.SaveChangesAsync(cancellationToken);
    }

    public void UpdateTimeStampForBaseEntityClass()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if(entry.Entity is not BaseEntity entity)
                continue;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.CreatedDate = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entity.UpdatedDate = DateTime.UtcNow;
                    break;
            }
        }
    }
}