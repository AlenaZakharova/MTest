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
    

    public event Action StartGame;
        
    public int FieldWidth => (int)widthSlider.value;
    public int FieldHeight => (int)heightSlider.value;

    private void OnEnable()
    {
        startGameButton.onClick.AddListener(startGameButtonOnClick);
        heightSlider.onValueChanged.AddListener(UpdateHeight); 
        widthSlider.onValueChanged.AddListener(UpdateWidth); 
        
        UpdateHeight(FieldHeight);
        UpdateWidth(FieldWidth);
    }

    private void startGameButtonOnClick()
    {
        StartGame!.Invoke();
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