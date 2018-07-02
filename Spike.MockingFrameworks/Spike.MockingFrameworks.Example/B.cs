using System;

namespace Spike.MockingFrameworks.Example
{
    class MyB : IB
    {
        private readonly IC c;
        private readonly ID d;
        public IC GetC() => c;
        public ID GetD() => d;

        public MyB(IC c, ID d)
        {
            this.c = c;
            this.d = d;
        }

        public int GetRandomNumber()
        {
            return c.GetSomething();
        }

        public string GetEmailAddress(UserCredentials userCredentials)
        {
            return $"{userCredentials.UserName}@mydomain.com";
        }

        public event Action<IB, EventArgs> MyEvent;
        public void MutateOperands(ref int i, out int j)
        {
            throw new NotImplementedException();
        }
    }
}
