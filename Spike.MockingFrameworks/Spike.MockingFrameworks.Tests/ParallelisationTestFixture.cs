using FakeItEasy;
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace Spike.MockingFrameworks.Tests
{
    [TestFixture, Parallelizable(ParallelScope.Children)]
    public class ParallelisationTestFixture
    {
        [Test, Repeat(1000)]
        public void Test_Moq()
        {
            new MyA(Mock.Of<IMyB>()).DoSomething();
        }

        [Test, Repeat(1000)]
        public void Test_Moq2()
        {
            new MyA(Mock.Of<IMyB>()).DoSomething();
        }

        [Test, Repeat(1000)]
        public void Test_Substitute()
        {
            new MyA(Substitute.For<IMyB>()).DoSomething();
        }

        [Test, Repeat(1000)]
        public void Test_Substitute2()
        {
            new MyA(Substitute.For<IMyB>()).DoSomething();
        }

        [Test, Repeat(1000)]
        public void Test_FakeItEasy()
        {
            new MyA(A.Fake<IMyB>()).DoSomething();
        }

        [Test, Repeat(1000)]
        public void Test_FakeItEasy2()
        {
            new MyA(A.Fake<IMyB>()).DoSomething();
        }

        private class MyA
        {
            private readonly IMyB myB;
            public MyA(IMyB myB)
            {
                this.myB = myB;
            }

            public void DoSomething()
            {
                System.Threading.Thread.Sleep(5);
                myB.DoSomething();               
            }
        }

        public interface IMyB
        {
            void DoSomething();
        }
    }
}