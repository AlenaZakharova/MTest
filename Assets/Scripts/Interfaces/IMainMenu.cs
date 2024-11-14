using System;

namespace Interfaces
{
    public interface IMainMenu
    {
        public void SetUp(GameConfig gameConfig);
        public void ShowWin();
        public void ShowGameMenu();
        public void ShowMainMenu();
        public event Action StartGame;
        public event Action StopGame;
        
        public int FieldWidth { get; }
        public int FieldHeight { get; }
    }
}