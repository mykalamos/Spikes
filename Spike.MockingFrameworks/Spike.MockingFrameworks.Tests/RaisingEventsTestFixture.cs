using System;
using FakeItEasy;
using Moq;
using NSubstitute;
using NUnit.Framework;
using Spike.MockingFrameworks.Example;
using Raise = NSubstitute.Raise;

namespace Spike.MockingFrameworks.Tests
{
    [TestFixture]
    public class RaisingEventsTestFixture
    {
        [Test]
        public void RaisingEvents_Moq()
        {
            var b = new Mock<IB>();
            var a = new MyA(b.Object);

            b.Raise(x => x.MyEvent += null, b.Object,  new EventArgs());

            Assert.That(a.EventRaised, Is.True);
        }

        [Test]
        public void RaisingEvents_NSubstitute()
        {
            var b = Substitute.For<IB>();
            var a = new MyA(b);

            b.MyEvent += Raise.Event<Action<IB, EventArgs>>(b, new EventArgs());

            Assert.That(a.EventRaised, Is.True);
        }

        [Test]
        public void RaisingEvents_FakeItEasy()
        {
            var b = A.Fake<IB>();
            var a = new MyA(b);

            b.MyEvent += FakeItEasy.Raise.FreeForm<Action<IB, EventArgs>>.With(b, new EventArgs());

            Assert.That(a.EventRaised, Is.True);
        }
    }
}