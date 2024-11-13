using Interfaces;
using UnityEngine;

public class Meta: MonoBehaviour
{
    [SerializeField] private Field field;
    [SerializeField] private Menu menu;
    [SerializeField] private GameConfig config;

    private Game game;

    private void Start()
    {
        field.Init(config);
        game = new Game(field, menu, config);
    }
}