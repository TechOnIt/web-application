using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechOnIt.Domain.UnitTests.ValueObjects
{
    public class FullNameTests
    {
        #region Constracture
        private FullName FullNameObj = null;
        public FullNameTests()
        {
            FullNameObj = new FullName("TestName", "TestSurname");
        }
        #endregion

        [Fact]
        public void Should_Can_Set_Name_From_Constracture()
        {
            // Act
            var resultName = FullNameObj.Name;
            var resultSurname = FullNameObj.Surname;

            // Assert
            Assert.NotNull(resultName);
            Assert.NotNull(resultSurname);
            Assert.True(!string.IsNullOrWhiteSpace(resultName));
            Assert.True(!string.IsNullOrWhiteSpace(resultSurname));
        }

        [Fact]
        public void Sould_Return_Name_With_No_Exception()
        {
            // Act
            var resultName = FullNameObj.GetFullName();
            var exceptionResult = Record.Exception(() => FullNameObj.GetFullName());

            // Assert
            resultName.ShouldNotBeNull();
            exceptionResult.ShouldBeNull();
            Assert.True(!string.IsNullOrWhiteSpace(resultName));
        }

        [Fact]
        public void Sould_Return_Surname_With_No_Exception()
        {
            // Act
            var resultSureName = FullNameObj.GetSurname();
            var exceptionResult = Record.Exception(() => FullNameObj.GetSurname());

            // Assert
            resultSureName.ShouldNotBeNull();
            exceptionResult.ShouldBeNull();
            Assert.True(!string.IsNullOrWhiteSpace(resultSureName));
        }

        [Fact]
        public void Sould_Return_FullName_With_No_Exception()
        {
            // Act
            var resultFullName = FullNameObj.GetFullName();
            var exceptionResult = Record.Exception(() => FullNameObj.GetFullName());

            // Assert
            resultFullName.ShouldNotBeNull();
            exceptionResult.ShouldBeNull();
            Assert.True(!string.IsNullOrWhiteSpace(resultFullName));
        }

        [Fact]
        public void Sould_Return_Equals_True_When_Parameter_Is_Type_FullName_With_No_Exception()
        {
            // Arrange
            var newFullNameObj = FullNameObj;

            // Act
            var result = FullNameObj.Equals(newFullNameObj);
            var exceptionResult = Record.Exception(() => FullNameObj.Equals(newFullNameObj));

            result.ShouldBeTrue();
            exceptionResult.ShouldBeNull();
        }

        [Fact]
        public void Sould_Return_Equals_False_When_Parameter_Is_Null_With_No_Exception()
        {
            // Act
            var result = FullNameObj.Equals(null);
            var exceptionResult = Record.Exception(() => FullNameObj.Equals(null));

            result.ShouldBeFalse();
            exceptionResult.ShouldBeNull();
        }

        [Fact]
        public void Sould_GetHashCodes_Returns_True_When_Parameters_Are_Equal()
        {
            // Arrange
            bool result = default;
            var newFullNameObject = new FullName("TestName", "TestSurname");

            // Act
            if (FullNameObj.GetHashCode() == newFullNameObject.GetHashCode()) result = true;

            // Assert
            result.ShouldBe(true);
        }

        [Fact]
        public void Sould_GetHashCodes_Returns_False_When_Parameters_Are_Not_Equal()
        {
            // Arrange
            bool result = default;
            var newFullNameObject = new FullName("TestName", "");

            // Act
            if (FullNameObj.GetHashCode() == newFullNameObject.GetHashCode()) result = true;

            // Assert
            result.ShouldBe(false);
        }

        [Theory]
        [InlineData("asfasfdsgsdgfhdfhdghflgshiuseyiusbdbvksjdgksdhkgsdgbdskjgsdhgkdshg", "surname")]
        public void Should_Throw_ArgumentOutOfRangeException_When_Name_Has_More_Than_50_Chars(string name, string surname)
        {
            var exception = Record.Exception(() => new FullName(name, surname));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData("name", "asfasfdsgsdgfhdfhdghflgshiuseyiusbdbvksjdgksdhkgsdgbdskjgsdhgkdshg")]
        public void Should_Throw_ArgumentOutOfRangeException_When_Surname_Has_More_Than_50_Chars(string name, string surname)
        {
            var exception = Record.Exception(() => new FullName(name, surname));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ArgumentOutOfRangeException>();
        }
    }
}
