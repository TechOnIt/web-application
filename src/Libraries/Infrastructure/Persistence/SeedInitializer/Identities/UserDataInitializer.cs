using TechOnIt.Domain.Entities.Catalogs;
using TechOnIt.Domain.Entities.Controllers;
using TechOnIt.Domain.Entities.Identities;
using TechOnIt.Domain.Entities.Identities.UserAggregate;

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
        UserEntity user1 = new(email: "rezaahmadidvlp@gmail.com", phoneNumber: "09058089095");
        user1.SetFullName(new FullName("Reza", "Ahmadi"));
        user1.SetPassword(PasswordHash.Parse("123456"));
        user1.ConfirmEmail();
        user1.ConfirmPhoneNumber();
        #region Structure
        user1.Structures = new List<StructureEntity>();
        StructureEntity newStructure = new("My Structure", PasswordHash.Parse("123456"), user1.Id, StructureType.Agriculture);
        #region Group
        GroupEntity newGroup = new("Hall", newStructure.Id);
        newGroup.Relays = new List<RelayEntity>();
        newGroup.Relays.Add(new RelayEntity(13, RelayType.Light, newGroup.Id));
        newStructure.AddGroup(newGroup);

        #endregion
        user1.Structures.Add(newStructure);
        #endregion
        await CreateUserAsync(user1, new string[] { "Admin" });

        UserEntity user2 = new(email: "ashnoori@gmail.com", phoneNumber: "09124133486");
        user2.SetFullName(new FullName("Ashkan", "Noori"));
        user2.SetPassword(PasswordHash.Parse("123456"));
        user2.ConfirmEmail();
        user2.ConfirmPhoneNumber();
        await CreateUserAsync(user2, new string[] { "Admin" });
    }

    private async Task CreateUserAsync(UserEntity newUser, string[]? roles = null)
    {
        bool haveSaveChange = false;
        if (roles != null)
        {
            foreach (var role in roles)
            {
                if (!await _uow.RoleRepository.IsExistsRoleNameAsync(role))
                {
                    var newRole = new RoleEntity(role);
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