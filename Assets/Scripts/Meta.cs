using Interfaces;
using UnityEngine;

public class Meta: MonoBehaviour
{
    [SerializeField] private Field field;
    [SerializeField] private Menu menu;
    [SerializeField] private GameConfig config;
    [SerializeField] private AudioController audioController;

    private IGame _game;
    private IScoreCounter _scoreCounter;

    private void Start()
    {
        field.SetUp(config);
        menu.SetUp(config);
        _scoreCounter = new ScoreCounter(menu, audioController);
        _game = new Game(field, menu, _scoreCounter, config);
    }
}