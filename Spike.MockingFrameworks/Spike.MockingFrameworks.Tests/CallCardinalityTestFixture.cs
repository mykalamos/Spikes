using FakeItEasy;
using Moq;
using NSubstitute;
using NUnit.Framework;
using Times = Moq.Times;

namespace Spike.MockingFrameworks.Tests
{
    [TestFixture]
    public class CallCardinalityTestFixture
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void CallCardinality_Moq(int times)
        {
            var b = new Mock<IMyB>();
            new MyA(b.Object).CallDependencyNTimes(times);
            b.Verify(x => x.DoSomething(), Times.Exactly(times));
            b.ResetCalls(); // Not Required
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void CallCardinality_Nsubstitute(int times)
        {
            var b = Substitute.For<IMyB>();
            new MyA(b).CallDependencyNTimes(times);
            b.Received(times).DoSomething();
            b.ClearReceivedCalls(); // Not Required
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void CallCardinality_FakeItEasy(int times)
        {
            var b = A.Fake<IMyB>();
            new MyA(b).CallDependencyNTimes(times);
            A.CallTo(() => b.DoSomething()).MustHaveHappened(times, FakeItEasy.Times.Exactly);
        }

        private class MyA
        {
            private readonly IMyB myB;
            public MyA(IMyB myB)
            {
                this.myB = myB;
            }
            public void CallDependencyNTimes(int times)
            {
                for (int i = 0; i < times; i++)
                {
                    myB.DoSomething();
                }
            }
        }

        public interface IMyB
        {
            void DoSomething();
        }
    }

    
}