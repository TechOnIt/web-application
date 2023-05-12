namespace TechOnIt.Domain.UnitTests.Entities;

public class StructureTests
{
    #region Id
    [Fact]
    /// <summary>
    /// When object created, it should have id.
    /// </summary>
    public void Should_Have_Id_When_Created()
    {
        // arrange
        var newStructure = new Structure("My Structure", PasswordHash.Parse("123456"), Guid.NewGuid(), StructureType.Home);
        // assert
        newStructure.Id.ToString().ShouldNotBeNull();
    }
    #endregion

    #region Name
    [Fact]
    public void Should_Have_Name_When_Created()
    {
        // arrange
        string structureName = "My Structure";
        // act
        var newStructure = new Structure(structureName, PasswordHash.Parse("123456"), Guid.NewGuid(), StructureType.Home);
        // assert
        newStructure.Name.ShouldBe(structureName);
    }

    [Fact]
    public void Should_Set_Name()
    {
        // Arrange
        string initName = "First Name";
        string newName = "Second Name";
        var newStructure = new Structure(name: initName, password: PasswordHash.Parse("123456"),
            userId: Guid.NewGuid(), type: StructureType.Agriculture);
        // Act
        newStructure.SetName(newName);
        // Assert
        newStructure.Name.ShouldBe(newName);
    }
    #endregion

    #region Description
    [Fact]
    public void Should_Set_Description()
    {
        // Arrange
        string structureDescription = "This is sample description";
        Structure newStructure = new(name: "My Structure", password: PasswordHash.Parse("123456"),
            userId: Guid.NewGuid(), type: StructureType.Home);
        // Act
        newStructure.Description = structureDescription;
        // Assert
        newStructure.Description.ShouldBe(structureDescription);
    }
    #endregion

    #region Type
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Should_Initialize_Type(int type)
    {
        // Arrange
        StructureType structureType = Enumeration.FromValue<StructureType>(type);
        // Act
        Structure newStructure = new(name: "My Structure", password: PasswordHash.Parse("123456"),
            userId: Guid.NewGuid(), type: structureType);
        // Assert
        newStructure.Type.ShouldBe(structureType);
    }
    #endregion

    #region Password
    [Theory]
    [InlineData("123456")]
    [InlineData("s9$4h^6W2*Fm!f8")]
    public void Should_Initialize_Password(string password)
    {
        // Arrange
        PasswordHash hashedPassword = PasswordHash.Parse(password);
        // Act
        Structure newStructure = new(name: "My Structure", password: hashedPassword,
            userId: Guid.NewGuid(), type: StructureType.Aviculture);
        // Assert
        newStructure.Password.ShouldBe(hashedPassword);
    }
    #endregion

    #region CreatedAt
    [Fact]
    public void Should_Have_CreatedAt()
    {
        // Arrange & Act
        Structure newStructure = new(name: "My Structure", password: PasswordHash.Parse("123456"),
            userId: Guid.NewGuid(), type: StructureType.Aviculture);
        // Assert
        newStructure.CreatedAt.ToString().ShouldNotBeNull();
    }
    #endregion

    #region ApiKey
    /// <summary>
    /// Structure should have api key when object created.
    /// </summary>
    [Fact]
    public void Should_Have_ApiKey_When_Created()
    {
        // Arrange
        var newStructure = new Structure(name: "My Home", password: PasswordHash.Parse("123456"),
            userId: Guid.NewGuid(), type: StructureType.Home);
        // Assert
        newStructure.ApiKey.ShouldNotBeNull();
    }

    /// <summary>
    /// Generate new api key for structure.
    /// </summary>
    [Fact]
    public void Should_Generate_New_ApiKey()
    {
        // Arrange
        var structure = new Structure(name: "My Structure", password: PasswordHash.Parse("123456"),
            userId: Guid.NewGuid(), type: StructureType.Aviculture);
        // Act
        structure.GenerateApiKey();
        var result = structure.ApiKey;
        // Assert
        result.ShouldNotBeNull();
    }
    #endregion
}
