using UnityEngine;

public class Meta: MonoBehaviour
{
    [SerializeField] private Field field;
    [SerializeField] private Menu menu;
    [SerializeField] private GameConfig config;

    private Game _game;

    private void Start()
    {
        field.SetUp(config);
        menu.SetUp(config);
        _game = new Game(field, menu, config);
    }
}