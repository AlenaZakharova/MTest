using UnityEngine;

namespace Interfaces
{
    public interface ICard
    {
        public void SetUp(Sprite backSide, Sprite frontSide, IGame game);
        public void Hide();
    }
}