using TechOnIt.Domain.Entities.Identity;
using TechOnIt.Domain.Entities.Identity.UserAggregate;
using TechOnIt.Domain.Entities.StructureAggregate;
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
        User user1 = new(email: "rezaahmadidvlp@gmail.com", phoneNumber: "09058089095");
        user1.SetFullName(new FullName("Reza", "Ahmadi"));
        user1.SetPassword(PasswordHash.Parse("123456"));
        user1.ConfirmEmail();
        user1.ConfirmPhoneNumber();
        #region Structure
        user1.Structures = new List<Structure>();
        Structure newStructure = new ("My Structure", PasswordHash.Parse("123456"), user1.Id, StructureType.Agriculture);
        #region Place
        Place newPlace = new("Hall", newStructure.Id);
        newPlace.Devices = new List<Device>();
        newPlace.Devices.Add(new Device(13, DeviceType.Light, newPlace.Id));
        newStructure.AddPlace(newPlace);
        
        #endregion
        user1.Structures.Add(newStructure);
        #endregion
        await CreateUserAsync(user1, new string[] { "Admin" });

        User user2 = new(email: "ashnoori@gmail.com", phoneNumber: "09124133486");
        user2.SetFullName(new FullName("Ashkan", "Noori"));
        user2.SetPassword(PasswordHash.Parse("123456"));
        user2.ConfirmEmail();
        user2.ConfirmPhoneNumber();
        await CreateUserAsync(user2, new string[] { "Admin" });
    }

    private async Task CreateUserAsync(User newUser, string[]? roles = null)
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
        if (!await _uow.UserRepository.IsExistsByQueryAsync(u => u.Username == newUser.Username ||
        u.PhoneNumber == newUser.PhoneNumber || u.Email == newUser.Email))
        {
            // Create new user in entities.
            await _uow.UserRepository.CreateAsync(newUser);
            haveSaveChange = true;
        }
        if (haveSaveChange)
            await _uow.SaveAsync();
    }
}