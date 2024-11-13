using Interfaces;

public class Game
{
    private GameConfig _config;
    private IMainMenu _menu;
    private IField _field;
        
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
    }
    
    private void OnStopGame()
    {
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