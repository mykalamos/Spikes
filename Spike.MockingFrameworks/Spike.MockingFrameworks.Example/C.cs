using System;

namespace Spike.MockingFrameworks.Example
{
    internal class C : IC
    {
        private readonly Random random;

        public C()
        {
            random = new Random(DateTime.Now.Millisecond);
        }

        public int GetSomething()
        {
            return random.Next();
        }
    }
}