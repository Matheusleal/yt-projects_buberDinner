using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Entities;
public sealed class MenuSection : Entity<MenuSectionId>
{
    private List<MenuItem> _items { get; } = [];
    public string Name { get; }
    public string Description { get; }
    public IReadOnlyList<MenuItem> Items() => _items.AsReadOnly();

    private MenuSection(MenuSectionId id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    private static MenuSection Create(string name, string description)
    {
        return new(MenuSectionId.CreateUnique(), name, description);
    }
}
