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
    private PlayerData _player;

    private void Start()
    {
        _player = Prefs.LoadPlayer();
        field.SetUp(config);
        menu.SetUp(config);
        menu.UpdateVictoryCount(_player.VictoryCount);
        _scoreCounter = new ScoreCounter(menu);
        _scoreCounter.CardsAreOut += PlayerWin;
        _game = new Game(field, menu, _scoreCounter, config);
        
        menu.StartGame += audioController.PlayBuildCardFieldSound;
        _game.CardClicked += audioController.PlayFlipSound;
        _game.Mismatched += audioController.PlayCardMismatchedSound;
        _game.Matched += audioController.PlayCardMatchedSound;
    }


    private void PlayerWin()
    {
        _player.AddVictory();
        menu.UpdateVictoryCount(_player.VictoryCount);
        audioController.PlayWinSound();
    }
}