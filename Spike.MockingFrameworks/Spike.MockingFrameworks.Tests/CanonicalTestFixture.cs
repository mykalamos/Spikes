using Moq;
using NSubstitute;
using FakeItEasy;
using NUnit.Framework;
using Spike.MockingFrameworks.Example;

namespace Spike.MockingFrameworks.Tests
{
    [TestFixture]
    public class CanonicalTestFixture
    {
        [Test]
        public void Canonical_Moq()
        {
            var c = Mock.Of<IC>();
            var d = Mock.Of<ID>();
            var b = Mock.Of<IB>(x => x.GetC() == c && x.GetD() == d);
            var a = new MyA(b);
            Assert.That(a.GetDependencies(), Is.EqualTo((b, c, d)));
            Mock.Get(b).Verify(x => x.GetC());
            Mock.Get(b).Verify(x => x.GetD());
        }

        [Test]
        public void Canonical_NSubstitute()
        {
            var d = Substitute.For<ID>();
            var c = Substitute.For<IC>();
            var b = Substitute.For<IB>();
            b.GetC().Returns(c);
            b.GetD().Returns(d);
            var a = new MyA(b);
            Assert.That(a.GetDependencies(), Is.EqualTo((b, c, d)));
            b.Received().GetC();
            b.Received().GetD();
        }

        [Test]
        public void Canonical_FakeItEasy()
        {
            var d = A.Fake<ID>();
            var c = A.Fake<IC>();
            var b = A.Fake<IB>();
            A.CallTo(() => b.GetC()).Returns(c);
            A.CallTo(() => b.GetD()).Returns(d);
            var a = new MyA(b);
            Assert.That(a.GetDependencies(), Is.EqualTo((b, c, d)));
            A.CallTo(() => b.GetC()).MustHaveHappened();
            A.CallTo(() => b.GetD()).MustHaveHappened();
        }
    }
}

// 01. Argument verification - Done
// 02. Sequencing - Done
// 03. Raising Events - Done
// 04. Out and ref - Done
// 05. Exception - Done
// 06. Stricts - Done
// 07. Method is not called / called a certain number of times. - Done
// 08. Async - Done
// 09. Argument access - Done
// 10. Thread-safety - Done
