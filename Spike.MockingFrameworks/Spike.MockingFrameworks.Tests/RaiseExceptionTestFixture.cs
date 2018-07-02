using System;
using FakeItEasy;
using Moq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using Spike.MockingFrameworks.Example;

namespace Spike.MockingFrameworks.Tests
{
    [TestFixture]
    public class RaiseExceptionTestFixture
    {
        [Test]
        public void RaiseException_Moq()
        {
            var b = new Mock<IB>();
            b.Setup(x => x.GetEmailAddress(It.IsAny<UserCredentials>())).Throws<ApplicationException>();
            Assert.Throws<ApplicationException>(() => new MyA(b.Object).IsAuthenticated("a"));
        }

        [Test]
        public void RaiseException_NSubstitute()
        {
            var b = Substitute.For<IB>();
            b.GetEmailAddress(Arg.Any<UserCredentials>()).Throws(new ApplicationException());
            Assert.Throws<ApplicationException>(() => new MyA(b).IsAuthenticated("a"));
        }

        [Test]
        public void RaiseException_FakeIt()
        {
            var b = A.Fake<IB>();
            A.CallTo(() => b.GetEmailAddress(A<UserCredentials>._)).Throws<ApplicationException>();
            Assert.Throws<ApplicationException>(() => new MyA(b).IsAuthenticated("a"));
        }
    }
}