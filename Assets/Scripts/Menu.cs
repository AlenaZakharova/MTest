using System;
using System.Collections;
using System.Globalization;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu: MonoBehaviour, IMenu
{
    [SerializeField] private Slider heightSlider;
    [SerializeField] private Slider widthSlider;
    [SerializeField] private TextMeshProUGUI heightValue;
    [SerializeField] private TextMeshProUGUI widthValue;
    [SerializeField] private RectTransform instructionsRectTransform;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button stopGameButton;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private TextMeshProUGUI turnsValue;
    [SerializeField] private TextMeshProUGUI matchesValue;
    [SerializeField] private GameObject startMenuParent;
    [SerializeField] private GameObject winPanelParent;
    [SerializeField] private GameObject gamePanelParent;

    private GameConfig _config;

    public event Action StartGame;
    public event Action StopGame;
    public int FieldWidth => (int)widthSlider.value;
    public int FieldHeight => (int)heightSlider.value;
    
    public void UpdatePlayerCountValues(int turnsCount, int matchesCount)
    {
        turnsValue.text = turnsCount.ToString();
        matchesValue.text = matchesCount.ToString();
    }

    public void ShowWin()
    {
        winPanelParent.SetActive(true);
        gamePanelParent.SetActive(false);
    }
    public void ShowMainMenu()
    {
        startMenuParent.SetActive(true);
        winPanelParent.SetActive(false);
        gamePanelParent.gameObject.SetActive(false);
    }

    public void ShowGameMenu()
    {
        startMenuParent.SetActive(false);
        winPanelParent.SetActive(false);
        gamePanelParent.gameObject.SetActive(true);
    }

    public void SetUp(GameConfig gameConfig)
    {
        _config = gameConfig;
        heightSlider.maxValue = _config.MaxFieldSideDimension;
        widthSlider.maxValue = _config.MaxFieldSideDimension;
    }

    private void OnEnable()
    {
        startGameButton.onClick.AddListener(StartGameButtonOnClick);
        stopGameButton.onClick.AddListener(StopGameButtonOnClick);
        returnToMenuButton.onClick.AddListener(StopGameButtonOnClick);
        heightSlider.onValueChanged.AddListener(UpdateHeight); 
        widthSlider.onValueChanged.AddListener(UpdateWidth); 
        
        UpdateHeight(FieldHeight);
        UpdateWidth(FieldWidth);

        ShowMainMenu();
    }

    private void StartGameButtonOnClick()
    {
        if (FieldWidth * FieldHeight % 2 != 0)
            StartCoroutine(BounceInstructions());
        else
        {
            startMenuParent.SetActive(false);
            StartGame?.Invoke();
        }
    }

    private IEnumerator BounceInstructions()
    {
        var t = 0.0f;
        while (t <= _config.BounceTime)
        {
            t += Time.deltaTime/_config.BounceTime;
            var scaledValue = Mathf.Sin(Mathf.Lerp(0.0f, Mathf.PI, t * Mathf.PI))/3.0f;
            instructionsRectTransform.localScale = Vector3.one + new Vector3(scaledValue, scaledValue, 1);
            yield return null;
        }
        instructionsRectTransform.localScale = Vector3.one;
    }

    private void StopGameButtonOnClick()
    {
        StopGame?.Invoke();
    }

    private void UpdateWidth(float count)
    {
        widthValue.text = count.ToString(CultureInfo.InvariantCulture);
    }

    private void UpdateHeight(float count)
    {
        heightValue.text = count.ToString(CultureInfo.InvariantCulture);
    }

    private void OnDisable()
    {
        startGameButton.onClick.RemoveListener(StartGameButtonOnClick);
        stopGameButton.onClick.RemoveListener(StopGameButtonOnClick);
        returnToMenuButton.onClick.RemoveListener(StopGameButtonOnClick);
        heightSlider.onValueChanged.RemoveListener(UpdateHeight); 
    }
}