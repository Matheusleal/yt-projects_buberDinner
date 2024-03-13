using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.MenuAggregate.ValueObjects;

namespace BuberDinner.Domain.MenuAggregate.Entities;
public sealed class MenuItem : Entity<MenuItemId>
{
    public string Name { get; private set; }

    public string Description { get; private set; }

    private MenuItem(MenuItemId id, string name, string description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public static MenuItem Create(string name, string description)
    {
        return new (MenuItemId.CreateUnique(), name, description);
    }

    // only for efCore mapping
#pragma warning disable CS8618
    public MenuItem()
    {
    }
#pragma warning restore CS8618
}
