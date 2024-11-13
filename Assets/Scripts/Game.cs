using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Game: IGame
{
    private GameConfig _config;
    private IMainMenu _menu;
    private IField _field;
    
    private List<int> _cardValues = new List<int>();

    public bool GameIsOn { get; private set; }

    public Game(IField field, IMainMenu menu, GameConfig config)
    {
        _config = config;
        _menu = menu;
        _field = field;

        _menu.StartGame += OnStartGame;
        _menu.StopGame += OnStopGame;
        _field.RebuildField(_menu.FieldWidth, _menu.FieldHeight);
    }

    private void OnStartGame()
    {
        _field.RebuildField(_menu.FieldWidth, _menu.FieldHeight);
        SetUpCards();

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
            _field.Cards[i].SetUp(_config.CardBackSprite, _config.CardImageSprites[_cardValues[i]], this);
    }

    private void OnStopGame()
    {
        GameIsOn = false;
        _field.RebuildField(_menu.FieldWidth, _menu.FieldHeight);
    }

    ~Game()
    {
        if (_menu != null)
        {
            _menu!.StartGame -= OnStartGame;
            _menu!.StopGame -= OnStopGame;
        }
    }
}