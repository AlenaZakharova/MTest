using System.Collections.Generic;

namespace Interfaces
{
    public interface IField
    {
        public void SetUp(GameConfig gameConfig);
        public void RebuildField(int width, int height);
        public void ShowAllCardsBeforePlaying();
        public IReadOnlyList<ICard> Cards { get; }
    }
}