using System.Reflection;

using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Infrastructure.Persistence.Interceptors;

using Microsoft.EntityFrameworkCore;

namespace BuberDinner.Infrastructure.Persistence;

public class BuberDinnerDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public DbSet<Menu> Menus { get; set; } = null!;

    public BuberDinnerDbContext(
        DbContextOptions<BuberDinnerDbContext> options,
        PublishDomainEventsInterceptor publishDomainEventsInterceptor)
        : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .AddInterceptors(_publishDomainEventsInterceptor);

        base.OnConfiguring(optionsBuilder);
    }
}
