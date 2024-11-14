using System;
using Interfaces;

public class ScoreCounter: IScoreCounter
{
    private int _turnsCount = 0;
    private int _matchesCount = 0;
    private IMenu _menu;
    private int _cardsLeft;
    private readonly AudioController _audioController;

    public event Action CardsAreOut;
    
    public ScoreCounter(IMenu menu, AudioController audio)
    {
        _menu = menu;
        _audioController = audio;
    }

    public void StartCountCards(int totalCardsOnField)
    {
        _audioController.PlayBuildCardFieldSound();
        _turnsCount = 0;
        _matchesCount = 0;
        _cardsLeft = totalCardsOnField;
        _menu.UpdatePlayerCountValues(0, 0);
    }

    public void AddTurn()
    {
        _audioController.PlayFlipSound();
        _turnsCount++;
        _menu.UpdatePlayerCountValues(_turnsCount, _matchesCount);
    }

    public void AddMatch()
    {
        _audioController.PlayCardMatchedSound();
        _matchesCount++;
        _cardsLeft -= 2;
        _menu.UpdatePlayerCountValues(_turnsCount, _matchesCount);
        if (_cardsLeft != 0) return;
        _audioController.PlayWinSound();
        CardsAreOut?.Invoke();
        _menu.ShowWin();
    }
}