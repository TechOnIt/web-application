using TechOnIt.Domain.Entities.Identity;
using TechOnIt.Domain.Entities.Identity.UserAggregate;
using TechOnIt.Domain.ValueObjects;
using TechOnIt.Infrastructure.Repositories.UnitOfWorks;

namespace TechOnIt.Infrastructure.Persistence.SeedInitializer.Identities;

internal class UserDataInitializer : IDataInitializer
{
    #region DI & Ctor
    private readonly IUnitOfWorks _uow;

    public UserDataInitializer(IUnitOfWorks uow)
    {
        _uow = uow;
    }
    #endregion

    public async Task InitializeDataAsync()
    {
        await CreateUserAsync("RezaAmd", "09058089095", "rezaahmadidvlp@gmail.com",
            roles: new List<string> { "Admin" }, name: "Reza", surname: "Ahmadi");

        await CreateUserAsync("MohsenMahv", "09128395645", "mohsen.mahv@gmail.com",
            roles: new List<string> { "Admin" }, name: "Mohsen", surname: "Heydari");
    }

    private async Task CreateUserAsync(string username, string phoneNumber, string email, string? password = null,
        List<string>? roles = null, string? name = null, string? surname = null)
    {
        bool haveSaveChange = false;
        if (roles != null)
        {
            foreach (var role in roles)
            {
                if (!await _uow.RoleRepository.IsExistsRoleNameAsync(role))
                {
                    var newRole = new Role(role);
                    await _uow.RoleRepository.CreateRoleAsync(newRole);
                    haveSaveChange = true;
                    await Task.Delay(500);
                }
            }
        }
        if (!await _uow.UserRepository.IsExistsByQueryAsync(u => u.Username == username || u.PhoneNumber == username || u.Email == username))
        {
            var newUser = new User(email: email, phoneNumber: phoneNumber);
            newUser.SetFullName(new FullName(name, surname));
            newUser.ConfirmPhoneNumber();
            newUser.ConfirmEmail();

            // Set password for new user.
            if (!string.IsNullOrEmpty(password))
                newUser.SetPassword(new PasswordHash(password));

            // Create new user in entities.
            await _uow.UserRepository.CreateAsync(newUser);
            haveSaveChange = true;
        }
        if (haveSaveChange)
            await _uow.SaveAsync();
    }
}