using System;

namespace Interfaces
{
    public interface IScoreCounter
    {
        public event Action CardsAreOut;
        public void StartCountCards(int totalCardsOnField);
        public void AddTurn();
        public void AddMatch();
    }
}