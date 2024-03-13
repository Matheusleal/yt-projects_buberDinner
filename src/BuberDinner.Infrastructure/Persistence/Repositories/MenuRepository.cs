using BuberDinner.Application.Commom.Interfaces.Persistence;
using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Infrastructure.Persistence;

namespace BuberDinner.Infrastructure.Persistence.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly BuberDinnerDbContext _dbContext;

    public MenuRepository(BuberDinnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Menu menu)
    {
        await _dbContext.Menus.AddAsync(menu);

        _dbContext.SaveChanges();
    }
}
