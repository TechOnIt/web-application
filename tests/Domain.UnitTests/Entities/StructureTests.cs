using iot.Domain.Entities.Product.StructureAggregate;
using iot.Domain.Enums;
using Shouldly;

namespace iot.Domain.UnitTests.Entities;

public class StructureTests
{

    [Fact]
    public void Should_Set_StructureType_By_StructureType_Instance()
    {
        // arrange
        var structure = new Structure();
        var sType = new StuctureType();

        // act
        structure.SetStructureType(StuctureType.Home);

        // assert
        structure.Type.ShouldNotBeNull();
        structure.Type.ShouldBe(StuctureType.Home);
    }

    [Fact]
    public void Should_Set_StructureType_By_StructureType_Name()
    {
        // arrange
        var structure = new Structure();

        // act
        structure.SetStructureType("Farm");

        // assert
        structure.Type.ShouldNotBeNull();
        structure.Type.DisplayName.ShouldBe("Farm");
    }

    [Fact]
    public void Should_Set_StructureType_By_StructureType_Value()
    {
        // arrange
        var structure = new Structure();

        // act
        structure.SetStructureType(2);

        // assert
        structure.Type.ShouldNotBeNull();
        structure.Type.DisplayName.ShouldBe("Garden");
        structure.Type.Value.ShouldBe(2);
    }

    [Fact]
    public void Should_Returns_StructureType_When_StructureType_Not_Null()
    {
        // arrange
        var structure = new Structure();

        // act
        structure.SetStructureType("Garden");
        var result = structure.GetStuctureType();

        // assert
        result.ShouldNotBeNull();
        result.Value.ShouldBe(2);
        result.ShouldBe(StuctureType.Home);
    }

    [Fact]
    public void Should_Returns_StructureType_Equal_ExistingType_StructureType()
    {
        // arrange
        var structure = new Structure();

        // act
        structure.SetStructureType(1);
        var result = structure.GetStuctureType();

        // assert
        result.ShouldNotBeNull();
        result.Value.ShouldBe(1);
        result.DisplayName.ShouldBe("Farm");
    }

    [Fact]
    public void Should_Set_New_Token()
    {
        // arrange
        var structure = new Structure();

        // act
        structure.SetApiKey();
        var result = structure.ApiKey;

        // assert
        result.ShouldNotBeNull();
    }
}
