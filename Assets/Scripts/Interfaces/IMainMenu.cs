using System;

namespace Interfaces
{
    public interface IMainMenu
    {
        public void SetUp(GameConfig gameConfig);
        public event Action StartGame;
        public event Action StopGame;
        
        public int FieldWidth { get; }
        public int FieldHeight { get; }
    }
}