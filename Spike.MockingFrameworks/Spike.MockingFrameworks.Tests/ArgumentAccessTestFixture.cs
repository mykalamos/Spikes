using FakeItEasy;
using Moq;
using NSubstitute;
using NUnit.Framework;
using Spike.MockingFrameworks.Example;

namespace Spike.MockingFrameworks.Tests
{
    [TestFixture]
    public class ArgumentAccessTestFixture
    {
        const string UserName = "a";
        [Test]
        public void ArgumentAccess_Moq()
        {
            string userNameCallback = null;
            var b = new Mock<IB>();
            b.Setup(x => x.GetEmailAddress(It.IsAny<UserCredentials>())).Callback<UserCredentials>(c => userNameCallback = c.UserName);
            new MyA(b.Object).IsAuthenticated(UserName);
            Assert.That(userNameCallback,Is.EqualTo(UserName));
        }

        [Test]
        public void ArgumentAccess_NSubstitute()
        {
            string userNameCallback = null;
            var b = Substitute.For<IB>();
            b.GetEmailAddress(Arg.Do<UserCredentials>(c => userNameCallback = c.UserName));
            new MyA(b).IsAuthenticated(UserName);
            Assert.That(userNameCallback, Is.EqualTo(UserName));
        }

        [Test]
        public void ArgumentAccess_FakeItEasy()
        {
            string userNameCallback = null;
            var b = A.Fake<IB>();
            
            A.CallTo(() => b.GetEmailAddress(A<UserCredentials>._)).Invokes((UserCredentials s) => userNameCallback = s.UserName);
            new MyA(b).IsAuthenticated(UserName);
            Assert.That(userNameCallback, Is.EqualTo(UserName));
        }
    }
}