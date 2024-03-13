using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BuberDinner.Domain.MenuAggregate;

using Microsoft.EntityFrameworkCore;
using BuberDinner.Domain.MenuAggregate.ValueObjects;
using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.MenuAggregate.Entities;

namespace BuberDinner.Infrastructure.Persistence.Configurations;

public class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        ConfigureMenuTable(builder);
        ConfigureMenuSectionTable(builder);
        ConfigureMenuDinnerIdsTable(builder);
        ConfigureMenuReviewIdsTable(builder);
    }

    private void ConfigureMenuReviewIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.MenuReviewIds, ri =>
        {
            builder.OwnsMany(m => m.MenuReviewIds, dib =>
            {
                dib.ToTable("MenuReviewIds");

                dib.WithOwner().HasForeignKey("MenuId");

                dib.HasKey("Id");

                dib
                    .Property(d => d.Value)
                    .HasColumnName("Reviewd")
                    .ValueGeneratedNever();
            });

            builder.Metadata.FindNavigation(nameof(Menu.MenuReviewIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private void ConfigureMenuDinnerIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.DinnerIds, dib =>
        {
            dib.ToTable("MenuDinnerIds");

            dib.WithOwner().HasForeignKey("MenuId");

            dib.HasKey("Id");

            dib
                .Property(d => d.Value)
                .HasColumnName("DinnerId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenuSectionTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.Sections, sb =>
        {
            sb.ToTable("MenuSections");

            sb.WithOwner().HasForeignKey("MenuId");

            sb.HasKey("Id", "MenuId");

            sb.Property(m => m.Id)
                .HasColumnName("MenuSectionId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MenuSectionId.Create(value));

            sb.Property(m => m.Name)
                .HasMaxLength(100);

            sb.Property(m => m.Description)
                .HasMaxLength(100);

            sb.OwnsMany(s => s.Items, ib =>
            {
                ib.ToTable("MenuItems");

                ib.WithOwner().HasForeignKey("MenuSectionId", "MenuId");

                ib.HasKey("Id", "MenuSectionId", "MenuId");

                ib.Property(i => i.Id)
                    .HasColumnName("MenuItemId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => MenuItemId.Create(value));

                ib.Property(m => m.Name)
                    .HasMaxLength(100);

                ib.Property(m => m.Description)
                    .HasMaxLength(100);
            });

            sb.Navigation(s => s.Items).Metadata.SetField("_items");
            sb.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Metadata
            .FindNavigation(nameof(Menu.Sections))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureMenuTable(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(m => m.Id);

        builder
            .Property(m => m.Id)
            .ValueGeneratedNever()
           .HasConversion(
                id => id.Value,
                value => MenuId.Create(value));

        builder
            .Property(m => m.Name)
            .HasMaxLength(100);

        builder
            .Property(m => m.Description)
            .HasMaxLength(100);

        builder.OwnsOne(m => m.AverageRating);

        builder
        .Property(m => m.HostId)
        .HasConversion(
            id => id.Value,
            value => HostId.Create(value));
    }
}
