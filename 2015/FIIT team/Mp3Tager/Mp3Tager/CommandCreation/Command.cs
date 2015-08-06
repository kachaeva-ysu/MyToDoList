﻿using System;
using System.Linq;

namespace CommandCreation
{
    public abstract class Command
    {
        public abstract void Execute();
        protected abstract int[] GetNumberOfArguments();
        public abstract string GetCommandName();

        protected void CheckIfCanBeExecuted(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Length == 0)
                throw new ArgumentException("You haven't passed any argument!");
            if (GetCommandName() != args[0])
                throw new InvalidOperationException("Invalid operation: there is no such command!");
            if (!GetNumberOfArguments().Contains(args.Length))
                throw new ArgumentException("Not enough arguments for this command!");
        }
    }
}
