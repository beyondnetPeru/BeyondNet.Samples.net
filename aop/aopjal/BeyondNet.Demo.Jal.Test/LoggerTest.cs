using System;
using System.Collections.Generic;
using Jal.Factory.Interface;
using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Impl.Factories;
using BeyondNet.Demo.Jal.Library.Impl.Loggers;
using BeyondNet.Demo.Jal.Library.Interface;
using BeyondNet.Demo.Jal.Library.Model;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BeyondNet.Demo.Jal.Test
{
    [TestFixture]
    public class LoggerTest
    {
        [Test]
        public void WhenLogLevelIsNotPermittedThenFailure()
        {
            var stubLogMessage = new LogMessage(LogLevel.Warning, "foo", DateTime.UtcNow);
            var stubListLevelPermitted =  new List<LogLevel>() {LogLevel.Message};

            var mockFormatter =  new Mock<IFormatter>();
            mockFormatter.Setup(m => m.Apply(stubLogMessage)).Returns(Result.Success("foo"));

            var subject =  new ConsoleLogger(mockFormatter.Object);
            subject.SetRules(stubListLevelPermitted);

            var result = subject.Log(stubLogMessage);

            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void WhenLogLevelIsPermittedThenSuccessful()
        {
            var stubLogMessage = new LogMessage(LogLevel.Message, "foo", DateTime.UtcNow);
            var stubListLevelPermitted = new List<LogLevel>() { LogLevel.Message };

            var mockFormatter = new Mock<IFormatter>();
            mockFormatter.Setup(m => m.Apply(stubLogMessage)).Returns(Result.Success("foo"));

            var subject = new ConsoleLogger(mockFormatter.Object);
            subject.SetRules(stubListLevelPermitted);

            var result = subject.Log(stubLogMessage);

            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void WhenFactoryReceiveStringThenCreateConsoleLogger()
        {
            var stubLogMessage = new LogMessage(LogLevel.Warning, "foo", DateTime.UtcNow);

            var mockFormatter = new Mock<IFormatter>();
            mockFormatter.Setup(m => m.Apply(stubLogMessage)).Returns(Result.Success("foo"));

            var mockObjectFactory = new Mock<IObjectFactory>();
            mockObjectFactory.Setup(m => m.Create<string, ILogger>(It.IsAny<string>()))
                .Returns(new ILogger[] {new ConsoleLogger(mockFormatter.Object),});

            var subject = new LoggerFactory(mockObjectFactory.Object).Create(It.IsAny<string>());

            subject.Content.GetType().ShouldBe(typeof(ConsoleLogger));
        }
    }
}
