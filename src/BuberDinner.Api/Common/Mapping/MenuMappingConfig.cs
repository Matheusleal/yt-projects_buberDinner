using Mapster;

using BuberDinner.Contracts.Menus;
using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Application.Menus.Commands.CreateMenu;

using MenuItem = BuberDinner.Domain.MenuAggregate.Entities.MenuItem;
using MenuSection = BuberDinner.Domain.MenuAggregate.Entities.MenuSection;

namespace BuberDinner.Api.Common.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Tá com bug pra mapear tuple
        //config.NewConfig<(CreateMenuRequest Request, string HostId), CreateMenuCommand>()
        //    .Map(dest => dest.HostId, src => src.HostId)
        //    .Map(dest => dest, src => src.Request);

        config.NewConfig<FullCreateMenuRequest, CreateMenuCommand>()
            .Map(dest => dest.HostId, src => src.HostId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<Menu, MenuResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.AverageRating, src => src.AverageRating.NumRatings > 0 ? src.AverageRating.Value.ToString() : null)
            .Map(dest => dest.HostId, src => src.HostId.Value)
            .Map(dest => dest.DinnersIds, src => src.DinnerIds.Select(dinnerId => dinnerId.Value))
            .Map(dest => dest.MenuReviewIds, src => src.MenuReviewIds.Select(menuReviewId => menuReviewId.Value));

        config.NewConfig<MenuSection, MenuSectionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<MenuItem, MenuItemResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}
