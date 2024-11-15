using System;
using UnityEngine;

namespace Interfaces
{
    public interface ICard
    {
        public void SetUp(int indexInField, Sprite backSide, Sprite frontSide, GameConfig gameConfig, IGame game);
        public void Hide();
        public void Flip(bool withDelay);
        public event Action<int> CardClicked;
    }
}