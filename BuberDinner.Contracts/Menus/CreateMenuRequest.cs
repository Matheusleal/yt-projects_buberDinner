namespace BuberDinner.Contracts.Menus;
public record CreateMenuRequest(
    string Name,
    string Description,
    List<MenuSection> Sections);

public record MenuSection(
    string Name,
    string Description,
    List<MenuItem> Items);

public record MenuItem(
    string Name,
    string Description);

// Remover depois do bug do mapster com tuple for corrigido
public record FullCreateMenuRequest(
    CreateMenuRequest Request,
    string HostId);