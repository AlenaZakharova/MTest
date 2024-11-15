using System;
using Interfaces;

public class ScoreCounter: IScoreCounter
{
    private int _turnsCount = 0;
    private int _matchesCount = 0;
    private IMenu _menu;
    private int _cardsLeft;

    public event Action CardsAreOut;
    
    public ScoreCounter(IMenu menu)
    {
        _menu = menu;
    }

    public void StartCountCards(int totalCardsOnField)
    {
        _turnsCount = 0;
        _matchesCount = 0;
        _cardsLeft = totalCardsOnField;
        _menu.UpdatePlayerCountValues(0, 0);
    }

    public void AddTurn()
    {
        _turnsCount++;
        _menu.UpdatePlayerCountValues(_turnsCount, _matchesCount);
    }

    public void AddMatch()
    {
        _matchesCount++;
        _cardsLeft -= 2;
        _menu.UpdatePlayerCountValues(_turnsCount, _matchesCount);
        if (_cardsLeft != 0) return;
        CardsAreOut?.Invoke();
        _menu.ShowWin();
    }
}