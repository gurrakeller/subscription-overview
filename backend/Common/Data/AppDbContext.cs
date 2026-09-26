using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using backend.Common;
using Common.Entities;
using Common.Data;
namespace backend.Common.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Streaming & Media" },
            new Category { Id = 2, Name = "Software & Work" },
            new Category { Id = 3, Name = "Health & Fitness" },
            new Category { Id = 4, Name = "Utilities & Bills" }
        );

        // Seed default payment methods
        builder.Entity<PaymentMethod>().HasData(
            new PaymentMethod { Id = 1, Name = "Credit Card" },
            new PaymentMethod { Id = 2, Name = "PayPal" },
            new PaymentMethod { Id = 3, Name = "Swish" },
            new PaymentMethod { Id = 4, Name = "Bank Transfer" }
        );
    }
}