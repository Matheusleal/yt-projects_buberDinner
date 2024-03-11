using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;

namespace BuberDinner.Domain.Menu;
public sealed class Menu : AggregateRoot<MenuId>
{
    private readonly List<MenuSection> _sections = [];
    private readonly List<DinnerId> dinnerIds = [];
    private readonly List<MenuReviewId> _menuReviewIds = [];
    public string Name { get; }
    public string Description { get; }
    public float AverageRating { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }
    public HostId HostId { get; }
    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();

    private Menu(
        MenuId id,
        string name,
        string description,
        HostId hostId,
        DateTime createdDateTime,
        DateTime updatedDateTime) : base(id)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Menu Create(
        string name,
        string description,
        HostId hostId)
    {
        return new Menu(
            MenuId.CreateUnique(),
            name,
            description,
            hostId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
