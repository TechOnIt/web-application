namespace iot.Application.Queries.Users.FindById.Command;

public class FindUserByIdQuery : IRequest<Result<UserViewModel>>
{
    // input parameter
    public Guid Id { get; set; }
}

