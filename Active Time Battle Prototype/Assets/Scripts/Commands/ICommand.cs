using System;

namespace Commands
{
    public interface ICommand
    {
        event Action CommandComplete;
        void Execute();
    }
}