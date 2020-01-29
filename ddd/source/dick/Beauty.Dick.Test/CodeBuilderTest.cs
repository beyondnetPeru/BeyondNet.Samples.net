using Beauty.Dick.Helpers.Builders.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace Beauty.Dick.Test
{
    [TestClass]
    public class CodeBuilderTest
    {
        [TestMethod]
        public void WhenBuildThenReturnString()
        {
            var mock = new Mock<ICodeBuilder>();
            mock.Setup(x => x.Build(It.IsAny<string>(),It.IsAny<string>())).Returns("foo");

            var result = mock.Object.Build("foo","foo").Content;

            result.ShouldNotBeEmpty();
            result.ShouldBe<string>("foo");
        }

    }
}
