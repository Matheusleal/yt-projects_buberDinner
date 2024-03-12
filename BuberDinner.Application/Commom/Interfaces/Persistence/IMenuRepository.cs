using BuberDinner.Domain.MenuAggregate;

namespace BuberDinner.Application;

public interface IMenuRepository
{
    void Add(Menu menu);
}
