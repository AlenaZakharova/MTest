using System;

namespace Interfaces
{
    public interface IMainMenu
    {
        public event Action StartGame;
        
        public int FieldWidth { get; }
        public int FieldHeight { get; }
    }
}