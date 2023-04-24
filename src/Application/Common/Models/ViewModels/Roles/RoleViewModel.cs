namespace TechOnIt.Application.Common.Models.ViewModels.Roles;

public record RoleViewModel(Guid Id, string Name);
public record RoleWithUsersCountViewModel(Guid Id, string Name, int UsersCount);