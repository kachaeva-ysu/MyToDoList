﻿using System;
using System.Threading;

namespace FaultTolerance.Timeout
{
    public class TimeoutStrategy : Strategy
    {
        int timeoutInMilliseconds;

        public TimeoutStrategy(int timeoutInMilliseconds)
        {
            TimeoutInMilliseconds = timeoutInMilliseconds;
        }

        public int TimeoutInMilliseconds
        {
            get => timeoutInMilliseconds;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(timeoutInMilliseconds),
                        "Timeout should be positive");
                }
                else
                {
                    timeoutInMilliseconds = value;
                }
            }
        }

        public override void Execute(Action<CancellationToken> action)
        {
            TimeoutProcessor.Execute<object>(
                (ct) => { action(ct); return null; },
                TimeoutInMilliseconds);
        }
    }
}
