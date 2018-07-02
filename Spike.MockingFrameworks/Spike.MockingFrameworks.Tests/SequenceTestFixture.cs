using FakeItEasy;
using Moq;
using NSubstitute;
using NUnit.Framework;
using Spike.MockingFrameworks.Example;

namespace Spike.MockingFrameworks.Tests
{
    [TestFixture]
    class SequenceTestFixture
    {
        [Test]
        public void Sequence_Moq()
        {
            var c = new Mock<IC>();
            c.SetupSequence(x => x.GetSomething()).Returns(1).Returns(2).Returns(4);
            var b = new MyB(c.Object, null);
            Assert.That(b.GetRandomNumber(), Is.EqualTo(1));
            Assert.That(b.GetRandomNumber(), Is.EqualTo(2));
            Assert.That(b.GetRandomNumber(), Is.EqualTo(4));
        }

        [Test]
        public void Sequence_NSubstitute()
        {
            var c = Substitute.For<IC>();
            c.GetSomething().Returns(1, 2, 4);
            var b = new MyB(c, null);
            Assert.That(b.GetRandomNumber(), Is.EqualTo(1));
            Assert.That(b.GetRandomNumber(), Is.EqualTo(2));
            Assert.That(b.GetRandomNumber(), Is.EqualTo(4));
        }

        [Test]
        public void Sequence_FakeItEasy()
        {
            var c = A.Fake<IC>();
            A.CallTo(() => c.GetSomething()).ReturnsNextFromSequence(1, 2, 4);
            var b = new MyB(c, null);
            Assert.That(b.GetRandomNumber(), Is.EqualTo(1));
            Assert.That(b.GetRandomNumber(), Is.EqualTo(2));
            Assert.That(b.GetRandomNumber(), Is.EqualTo(4));
        }
    }
}
