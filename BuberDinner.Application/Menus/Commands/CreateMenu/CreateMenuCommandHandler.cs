﻿using ErrorOr;
using MediatR;

using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Domain.MenuAggregate.Entities;
using BuberDinner.Domain.HostAggregate.ValueObjects;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public sealed class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // Create Menu
        var menu = Menu.Create(
            hostId: HostId.Create(request.HostId),
            name: request.Name,
            description: request.Description,
            sections: request.Sections.ConvertAll(section =>
                MenuSection.Create(
                    name: section.Name,
                    description: section.Description,
                    items: section.Items.ConvertAll(item =>
                        MenuItem.Create(
                            name: item.Name,
                            description: item.Description)))));

        // Persist Menu
        _menuRepository.Add(menu);

        // Return Menu

        return menu;
    }
}
