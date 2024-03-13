using BuberDinner.Domain.MenuAggregate;

namespace BuberDinner.Application.Commom.Interfaces.Persistence;

public interface IMenuRepository
{
    Task AddAsync(Menu menu);
}
