using FakeItEasy;
using Moq;
using NSubstitute;
using NUnit.Framework;
using Spike.MockingFrameworks.Example;

namespace Spike.MockingFrameworks.Tests
{
    [TestFixture]
    public class ArgumentVerificationTestFixture
    {
        const string Username = "me";
        const string EmailAddress = "email@address.com";

        [Test]
        public void ArgumentVerification_Moq()
        {
            var b = Mock.Of<IB>(x => x.GetEmailAddress(It.Is<UserCredentials>(y => y.UserName == Username)) == EmailAddress);
            Assert.That(new MyA(b).IsAuthenticated(Username), Is.True);
            Mock.Get(b).Verify(x => x.GetEmailAddress(It.Is<UserCredentials>(y => y.UserName == Username)));
        }

        [Test]
        public void ArgumentVerification_NSubstitute()
        {
            var b = Substitute.For<IB>();
            b.GetEmailAddress(Arg.Is<UserCredentials>(c => c.UserName == Username)).Returns(EmailAddress);
            Assert.That(new MyA(b).IsAuthenticated(Username), Is.True);
            b.Received().GetEmailAddress(Arg.Is<UserCredentials>(c => c.UserName == Username));
        }

        [Test]
        public void ArgumentVerification_FakeItEasy()
        {
            var b = A.Fake<IB>();
            A.CallTo(() => b.GetEmailAddress(A<UserCredentials>.That.Matches(x => x.UserName == Username))).Returns(Username);
            Assert.That(new MyA(b).IsAuthenticated(Username), Is.True);
            A.CallTo(() => b.GetEmailAddress(A<UserCredentials>.That.Matches(x => x.UserName == Username))).MustHaveHappened();
        }
    }
}
