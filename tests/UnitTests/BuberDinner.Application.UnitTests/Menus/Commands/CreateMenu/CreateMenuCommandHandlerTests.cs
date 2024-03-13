using BuberDinner.Application.Commom.Interfaces.Persistence;
using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Application.UnitTests.Menus.Commands.TestUtils;
using BuberDinner.Application.UnitTests.Menus.TestUtils.Menus.Extensions;

using FluentAssertions;

using Moq;

namespace BuberDinner.Application.UnitTests.Menus.Commands.CreateMenu;
public class CreateMenuCommandHandlerTests
{
    private readonly CreateMenuCommandHandler _handler;
    private readonly Mock<IMenuRepository> _mockRepository;

    public CreateMenuCommandHandlerTests()
    {
        _mockRepository = new Mock<IMenuRepository>();
        _handler = new CreateMenuCommandHandler(_mockRepository.Object);
    }

    // T1: SUT(system under test) - logical component we're testing
    // T2: Scenario - what we're testing
    // T3: Expected outcome

    //public void T1_T2_T3(){}
    //public void HandleCreateMenuCommand_WhenMenuIsValid_ShouldAndReturnMenu(){}

    [Theory]
    [MemberData(nameof(ValidCreateMenuCommands))]
    public async Task HandleCreateMenuCommand_WhenMenuIsValid_ShouldAndReturnMenu(CreateMenuCommand createMenuCommand)
    {
        // Act
        // Invoke the handler
        var result = await _handler.Handle(createMenuCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.ValidateCreatedFrom(createMenuCommand);

        _mockRepository.Verify(m => m.AddAsync(result.Value), Times.Once);
    }

    public static IEnumerable<object[]> ValidCreateMenuCommands()
    {
        yield return new object[] { CreateMenuCommandUtils.CreateCommand() };

        yield return new object[]
        {
            CreateMenuCommandUtils.CreateCommand(
                CreateMenuCommandUtils.CreateSectionsCommand(sectionCount: 2)),
        };

        yield return new object[]
        {
            CreateMenuCommandUtils.CreateCommand(
                CreateMenuCommandUtils.CreateSectionsCommand(
                    sectionCount: 4,
                    items: CreateMenuCommandUtils.CreateItemsCommand(6))),
        };
    }
}
