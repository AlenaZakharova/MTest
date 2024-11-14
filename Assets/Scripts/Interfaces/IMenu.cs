using System;

namespace Interfaces
{
    public interface IMenu
    {
        public void SetUp(GameConfig gameConfig);
        public void ShowWin();
        public void ShowGameMenu();
        public void ShowMainMenu();
        public event Action StartGame;
        public event Action StopGame;
        
        public int FieldWidth { get; }
        public int FieldHeight { get; }

        public void UpdatePlayerCountValues(int turnsCount, int matchesCount);

    }
}