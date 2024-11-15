using System;

namespace Interfaces
{
    public interface IGame
    {
        public bool GameIsOn { get; }
        public event Action Mismatched;
    }
}