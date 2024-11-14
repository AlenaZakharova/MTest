using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Game: IGame
{
    private GameConfig _config;
    private IMainMenu _menu;
    private IField _field;
    private int _selectedCardValue;
    private int _selectedCardIndex;
    private int _cardsLeft;
    private List<int> _cardValues = new List<int>();
    public bool GameIsOn { get; private set; }

    public Game(IField field, IMainMenu menu, GameConfig config)
    {
        _config = config;
        _menu = menu;
        _field = field;
        _menu.StartGame += OnStartGame;
        _menu.StopGame += OnStopGameButtonClicked;
        _field.RebuildField(_menu.FieldWidth, _menu.FieldHeight);
    }

    private void OnStartGame()
    {
        _menu.ShowGameMenu();
        _field.RebuildField(_menu.FieldWidth, _menu.FieldHeight);
        SetUpCards();
        _selectedCardValue = -1;
        _selectedCardIndex = -1;
        _cardsLeft = _menu.FieldWidth * _menu.FieldHeight;
        GameIsOn = true;
    }

    private void SetUpCards()
    {
        _cardValues.Clear();
        var cardsCount = _field.Cards.Count;
        for (var i = 0; i < cardsCount / 2; i++)
        {
            var spriteIndex = Random.Range(0, _config.CardImageSprites.Length - 1);
            _cardValues.Add(spriteIndex);
            _cardValues.Add(spriteIndex);
        }
        _cardValues.Shuffle();
        for (var i = 0; i < cardsCount; i++)
        {
            _field.Cards[i].SetUp(i, _config.CardBackSprite, _config.CardImageSprites[_cardValues[i]], _config, this);
            _field.Cards[i].CardClicked += OnCardClicked;
        }
    }

    private void OnCardClicked(int cardIndex)
    {
        // first card selected
        if (_selectedCardValue == -1)
        {
            _selectedCardValue = _cardValues[cardIndex];
            _selectedCardIndex = cardIndex;
        }
        else
        { // second card selected
            if (_selectedCardValue == _cardValues[cardIndex])
            {
                //correctly matched
                _field.Cards[_selectedCardIndex].Hide();
                _field.Cards[cardIndex].Hide();
                _cardsLeft -= 2;
                CheckWin();
            }
            else
            {
                // incorrectly matched
                _field.Cards[_selectedCardIndex].Flip();
                _field.Cards[cardIndex].Flip();
            }
            _selectedCardIndex = _selectedCardValue = -1;
        }
    }

    private void CheckWin()
    {
        if (_cardsLeft != 0) return;
        StopGame();
        _menu.ShowWin();
    }

    private void OnStopGameButtonClicked()
    {
        StopGame();
        _menu.ShowMainMenu();
    }

    private void StopGame()
    {
        GameIsOn = false;
        foreach (var card in _field.Cards)
            card.CardClicked -= OnCardClicked;
        _field.RebuildField(_menu.FieldWidth, _menu.FieldHeight);
    }
}