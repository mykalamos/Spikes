using System.Threading.Tasks;
using FakeItEasy;
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace Spike.MockingFrameworks.Tests
{
    [TestFixture]
    public class AsyncTestFixture
    {
        const string AsyncReturnValue = "hello";

        [Test]
        public async Task Async_Moq()
        {
            var b = new Mock<IMyB>();
            b.SetupSequence(x => x.DoSomethingAsync()).Returns(Task.FromResult(AsyncReturnValue));
            var s = await new MyA(b.Object).SetupAsyncCall();
            Assert.That(s, Is.EqualTo(AsyncReturnValue));
        }

        [Test]
        public async Task Async_NSubstitute()
        {
            var b = Substitute.For<IMyB>();
            b.DoSomethingAsync().Returns(AsyncReturnValue);
            var s = await new MyA(b).SetupAsyncCall();
            Assert.That(s, Is.EqualTo(AsyncReturnValue));
        }

        [Test]
        public async Task Async_FakeItEasy()
        {
            var b = A.Fake<IMyB>();
            A.CallTo(() => b.DoSomethingAsync()).Returns(AsyncReturnValue);
            var s = await new MyA(b).SetupAsyncCall();
            Assert.That(s, Is.EqualTo(AsyncReturnValue));
        }

        private class MyA
        {
            private readonly IMyB myB;

            public MyA(IMyB myB)
            {
                this.myB = myB;
            }

            public async Task<string> SetupAsyncCall()
            {
                return await myB.DoSomethingAsync();
            }
        }

        public interface IMyB
        {
            Task<string> DoSomethingAsync();
        }

    }
}