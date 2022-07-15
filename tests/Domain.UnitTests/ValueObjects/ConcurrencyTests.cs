using FluentAssertions;
using iot.Domain.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Domain.UnitTests.ValueObjects
{
    public class ConcurrencyTests
    {
        [Fact]
        public void Should_Not_Be_Null_Or_Empty()
        {
            var resultConcurrency = Concurrency.NewToken();

            Assert.NotEmpty(resultConcurrency);
            Assert.NotNull(resultConcurrency);
        }

        [Fact]
        public void Should_Token_Has_Sixteen_Characters()
        {
            var resultConcurrency = Concurrency.NewToken();

            Assert.True(resultConcurrency.Length <17);
            Assert.True(resultConcurrency.Length >15);
        }


        //[Theory]
        //[MemberData(nameof(TokenTest))]
        //public static IEnumerable<object[]> TokenTest()
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        yield return new object[] { Concurrency.NewToken() };
        //        yield return new object[] { Concurrency.NewToken() };
        //        yield return new object[] { Concurrency.NewToken() };
        //    }
        //}
    }
}
