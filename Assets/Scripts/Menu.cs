using System;
using System.Globalization;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu: MonoBehaviour, IMainMenu
{
    [SerializeField] private Slider heightSlider;
    [SerializeField] private Slider widthSlider;
    [SerializeField] private TextMeshProUGUI heightValue;
    [SerializeField] private TextMeshProUGUI widthValue;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button stopGameButton;
    [SerializeField] private GameObject startMenuParent;

    private GameConfig _config;

    public event Action StartGame;
    public event Action StopGame;

    public int FieldWidth => (int)widthSlider.value;
    public int FieldHeight => (int)heightSlider.value;

    public void SetUp(GameConfig gameConfig)
    {
        _config = gameConfig;
        heightSlider.maxValue = _config.MaxFieldSideDimension;
        widthSlider.maxValue = _config.MaxFieldSideDimension;
    }

    private void OnEnable()
    {
        startGameButton.onClick.AddListener(startGameButtonOnClick);
        stopGameButton.onClick.AddListener(stopGameButtonOnClick);
        heightSlider.onValueChanged.AddListener(UpdateHeight); 
        widthSlider.onValueChanged.AddListener(UpdateWidth); 
        
        UpdateHeight(FieldHeight);
        UpdateWidth(FieldWidth);
    }

    private void startGameButtonOnClick()
    {
        startMenuParent.SetActive(false);
        StartGame!.Invoke();
    }
    private void stopGameButtonOnClick()
    {
        startMenuParent.SetActive(true);
        StopGame!.Invoke();
    }

    private void OnDisable()
    {
        startGameButton.onClick.RemoveListener(startGameButtonOnClick);
    }

    private void UpdateWidth(float count)
    {
        widthValue.text = count.ToString(CultureInfo.InvariantCulture);
    }

    private void UpdateHeight(float count)
    {
        heightValue.text = count.ToString(CultureInfo.InvariantCulture);
    }

}