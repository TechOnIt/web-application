using iot.Application.Commands.Users;
using iot.Application.Common.Interfaces.Context;

namespace Application.UnitTests.MediatRTests.CommandsTests;

public class CreateUserCommandHandlerTest
{
    [Fact]
    public async void Should_Insert_New_User_When_All_Data_Are_Valid()
    {
        // arrange
        var _mediator = new Mock<IMediator>();
        var _context = new Mock<IIdentityContext>();

        UserCreateCommand createCommand=new UserCreateCommand();
        UserCreateCommandHandler handler = new UserCreateCommandHandler(_context.Object);

        // act
        var result = await handler.Handle(createCommand,new CancellationToken());
        
        // assert
        
    }
}
