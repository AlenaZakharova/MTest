using UnityEngine;

namespace Interfaces
{
    public interface ICard
    {
        public void SetUp(Sprite backSide, Sprite frontSide);
        public void Hide();
        public void ShowFront();
        public void ShowBack();
    }
}