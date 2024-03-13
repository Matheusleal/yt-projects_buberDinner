using BuberDinner.Domain.HostAggregate.ValueObjects;

namespace BuberDinner.Application.UnitTests.Menus.TestUtils.Contants;
public static partial class Constants
{
    public static class Host
    {
        public static readonly HostId Id = HostId.CreateUnique();
    }
}
