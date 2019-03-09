using System;
using System.Threading;

namespace CatMash.Core
{
    public class ScopedLock : IDisposable
    {
        public object Lock { get; }

        public ScopedLock(object locker)
        {
            Monitor.Enter(locker);
            Lock = locker;
        }

        public void Dispose()
        {
            Monitor.Exit(Lock);
        }
    }
}