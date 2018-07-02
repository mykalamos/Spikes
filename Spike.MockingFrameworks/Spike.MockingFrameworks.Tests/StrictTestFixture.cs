using FakeItEasy;
using Moq;
using NUnit.Framework;
using Spike.MockingFrameworks.Example;

namespace Spike.MockingFrameworks.Tests
{
    [TestFixture]
    public class StrictTestFixture
    {
        [Test]
        public void Strict_Moq()
        {
            var b = new Mock<IB>(MockBehavior.Strict);
            b.Setup(x => x.GetEmailAddress(It.IsAny<UserCredentials>())).Returns("me@me.com");
            var a = new MyA(b.Object);
            a.IsAuthenticated("userName");
        }

        [Test]
        public void Strict_NSubstitute()
        {
            Assert.Inconclusive("NSubstitute does not support strick mocks");
        }

        [Test]
        public void Strict_FakeItEasy()
        {
            var b = A.Fake<IB>(x => x.Strict());
            A.CallTo(() => b.GetEmailAddress(A<UserCredentials>._)).Returns("me@me.com");
            var a = new MyA(b);
            a.IsAuthenticated("userName");
        }
    }

}