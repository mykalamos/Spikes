using System;

namespace Spike.MockingFrameworks.Example
{
    internal interface IB
    {
        IC GetC();
        ID GetD();
        int GetRandomNumber();
        string GetEmailAddress(UserCredentials userCredentials);
        event Action<IB, EventArgs> MyEvent;
        void MutateOperands(ref int i, out int j);
    }
}