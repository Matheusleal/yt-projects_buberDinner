using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.MenuAggregate.ValueObjects;

namespace BuberDinner.Domain.MenuAggregate.Entities;
public sealed class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _items = [];
    public string Name { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

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

    // only for efCore mapping
#pragma warning disable CS8618
    public MenuSection()
    {
    }
#pragma warning restore CS8618
}
