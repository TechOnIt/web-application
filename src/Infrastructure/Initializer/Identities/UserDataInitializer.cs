using iot.Domain.Entities.Identity.UserAggregate;
using iot.Domain.ValueObjects;
using iot.Infrastructure.Repositories.UnitOfWorks;

namespace iot.Infrastructure.Initializer.Identities;

internal class UserDataInitializer : IDataInitializer
{
    #region DI & Ctor
    private readonly IUnitOfWorks _uow;

    public UserDataInitializer(IUnitOfWorks uow)
    {
        _uow = uow;
    }
    #endregion

    public async Task InitializeData()
    {
        await CreateUser("RezaAmd", "rezaahmadidvlp@gmail.com",
            roles: new List<string> { "Admin" }, name: "Reza", surname: "Ahmadi");
    }

    private async Task CreateUser(string username, string email, string? password = null,
        List<string>? roles = null, string? name = null, string? surname = null)
    {
        if (!await _uow.UserRepository.IsExistsUserByPhoneNumberAsync(username))
        {
            var newUser = User.CreateNewInstance(email, username);
            newUser.SetFullName(new FullName(name, surname));
            newUser.ConfirmPhoneNumber();
            newUser.ConfirmEmail();

            // Set password for new user.
            if (!string.IsNullOrEmpty(password))
                newUser.SetPassword(new PasswordHash(password));

            // Create new user in entities.
            await _uow.UserRepository.CreateNewUser(newUser);
        }
    }
}