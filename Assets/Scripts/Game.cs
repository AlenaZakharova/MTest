using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Game: IGame
{
    private readonly GameConfig _config;
    private readonly IMenu _menu;
    private readonly IField _field;
    private readonly IScoreCounter _scoreCounter;
    private int _selectedCardValue;
    private int _selectedCardIndex;
    private readonly List<int> _cardValues = new List<int>();

    public bool GameIsOn { get; private set; }

    public Game(IField field, IMenu menu, IScoreCounter scoreCounter, GameConfig config)
    {
        _config = config;
        _menu = menu;
        _field = field;
        _scoreCounter = scoreCounter;
        _scoreCounter.CardsAreOut += StopGame;
        _menu.StartGame += OnStartGame;
        _menu.StopGame += OnStopGameButtonClicked;
        _field.RebuildField(_menu.FieldWidth, _menu.FieldHeight);
    }

    private void OnStartGame()
    {
        _scoreCounter.StartCountCards(_menu.FieldWidth * _menu.FieldHeight);
        _menu.ShowGameMenu();
        _field.RebuildField(_menu.FieldWidth, _menu.FieldHeight);
        SetUpCards();
        _selectedCardValue = -1;
        _selectedCardIndex = -1;
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
        _scoreCounter.AddTurn();
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
                _scoreCounter.AddMatch();
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