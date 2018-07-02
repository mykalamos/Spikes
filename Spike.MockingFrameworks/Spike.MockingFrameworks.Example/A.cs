using System;

namespace Spike.MockingFrameworks.Example
{
    class MyA : IA
    {
        public IB MyB { get; }

        public MyA(IB b)
        {
            MyB = b;
            MyB.MyEvent += MyB_MyEvent;
        }

        private void MyB_MyEvent(IB arg1, EventArgs arg2)
        {
            EventRaised = true;
        }

        public bool EventRaised { get; private set; }

        public (IB, IC, ID) GetDependencies()
        {
            Console.WriteLine(ToString());
            return (MyB, MyB.GetC(), MyB.GetD());
        }

        public bool IsAuthenticated(string userName)
        {
            var emailAddress = MyB.GetEmailAddress(new UserCredentials(userName));
            return emailAddress != null;
        }

        public int GetNextId()
        {
            int i = 0;
            int j;
            MyB.MutateOperands(ref i, out j);
            return i;
        }

    }
}
