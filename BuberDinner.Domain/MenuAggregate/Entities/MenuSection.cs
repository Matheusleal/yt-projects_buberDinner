using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.MenuAggregate.ValueObjects;

namespace BuberDinner.Domain.MenuAggregate.Entities;
public sealed class MenuSection : Entity<MenuSectionId>
{
    private List<MenuItem> _items { get; } = [];
    public string Name { get; }
    public string Description { get; }
    public IReadOnlyList<MenuItem> Items() => _items.AsReadOnly();

    private MenuSection(
        MenuSectionId id,
        string name,
        string description,
        List<MenuItem>? items)
        : base(id)
    {
        Name = name;
        Description = description;
        _items = items ?? [];
    }

    public static MenuSection Create(
        string name,
        string description,
        List<MenuItem>? items)
    {
        return new (
            MenuSectionId.CreateUnique(),
            name,
            description,
            items);
    }
}
