using BuberDinner.Application;
using BuberDinner.Domain.MenuAggregate;

namespace BuberDinner.Infrastructure;

public class MenuRepository : IMenuRepository
{
    private readonly List<Menu> _menus = [];

    public void Add(Menu menu)
    {
        _menus.Add(menu);
    }
}
