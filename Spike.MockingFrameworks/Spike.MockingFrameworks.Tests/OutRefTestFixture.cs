using FakeItEasy;
using Moq;
using NSubstitute;
using NUnit.Framework;
using Spike.MockingFrameworks.Example;

namespace Spike.MockingFrameworks.Tests
{
    [TestFixture]
    public class OutRefTestFixture
    {
        delegate void MyCallback(ref int i, out int j);

        [Test]
        public void OutRef_Moq()
        {
            var b = new Mock<IB>();
            var k2 = 12;
            b.Setup(x => x.MutateOperands(ref It.Ref<int>.IsAny, out It.Ref<int>.IsAny))
                .Callback(new MyCallback((ref int i, out int j) =>
                {
                    i = k2;
                    j = 0;
                }));
            var a = new MyA(b.Object);
            var id = a.GetNextId();
            Assert.That(id, Is.EqualTo(k2));
        }
        
        [Test]
        public void OutRef_NSubstitute()
        {
            var b = Substitute.For<IB>();
            var k2 = 12;
            int i = 0;
            int j = 0;
            b.When(x => x.MutateOperands(ref i, out j))
                .Do(x => { x[i] = k2; });
            var a = new MyA(b);
            var id = a.GetNextId();
            Assert.That(id, Is.EqualTo(k2));
        }

        [Test]
        public void OutRef_FakeItEasy()
        {
            var b = A.Fake<IB>();
            var k2 = 12;
            int i = 0;
            int j = 0;
            A.CallTo(() => b.MutateOperands(ref i, out j)).AssignsOutAndRefParameters(k2, 0);
;
            var a = new MyA(b);
            var id = a.GetNextId();
            Assert.That(id, Is.EqualTo(k2));
        }
    }
}