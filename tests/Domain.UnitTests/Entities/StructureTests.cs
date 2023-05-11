using Shouldly;
using System;
using TechOnIt.Domain.Enums;

namespace TechOnIt.Domain.UnitTests.Entities;

public class StructureTests
{

    [Fact]
    public void Should_Set_New_Token()
    {
        // arrange
        var structure = new Structure("Sample", PasswordHash.Parse("1234"), Guid.Empty, StructureType.Aviculture);

        // act
        structure.GenerateApiKey();
        var result = structure.ApiKey;

        // assert
        result.ShouldNotBeNull();
    }
}
