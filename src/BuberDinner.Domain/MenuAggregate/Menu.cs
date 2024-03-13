using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.DinnerAggregate.ValueObjects;
using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.MenuAggregate.Entities;
using BuberDinner.Domain.MenuAggregate.Events;
using BuberDinner.Domain.MenuAggregate.ValueObjects;
using BuberDinner.Domain.MenuReviewAggregate.ValueObjects;

namespace BuberDinner.Domain.MenuAggregate;
public sealed class Menu : AggregateRoot<MenuId, Guid>
{
    private readonly List<MenuSection> _sections = [];
    private readonly List<DinnerId> dinnerIds = [];
    private readonly List<MenuReviewId> _menuReviewIds = [];
    public string Name { get; private set; }
    public string Description { get; private set; }
    public AverageRating AverageRating { get; private set; } = null!;
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }
    public HostId HostId { get; private set; }
    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();

    private Menu(
        MenuId id,
        HostId hostId,
        string name,
        string description,
        AverageRating averageRating,
        List<MenuSection>? sections)
        : base(id)
    {
        HostId = hostId;
        Name = name;
        Description = description;
        AverageRating = averageRating;
        _sections = sections ?? [];
    }

    public static Menu Create(
        HostId hostId,
        string name,
        string description,
        AverageRating averageRating,
        List<MenuSection>? sections)
    {
        var menu = new Menu(
            MenuId.CreateUnique(),
            hostId,
            name,
            description,
            averageRating,
            sections);

        menu.AddDomainEvent(new MenuCreated(menu));

        return menu;
    }

// only for efCore mapping
#pragma warning disable CS8618
    public Menu()
    {
    }
#pragma warning restore CS8618

}
