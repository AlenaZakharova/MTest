using System;

namespace Interfaces
{
    public interface IGame
    {
        public bool GameIsOn { get; }
        public event Action Matched;
        public event Action Mismatched;
        public event Action CardClicked;
    }
}