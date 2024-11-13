using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, ICard
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Image cardBackImage;
    [SerializeField] private Button cardButton;

    private Sprite _frontSideSprite;
    private Sprite _backSideSprite;

    private bool _cardUpSide = false;


    public void SetUp(Sprite backSide, Sprite frontSide)
    {
        _frontSideSprite = frontSide;
        _backSideSprite = backSide;
        
        cardBackImage.sprite = _backSideSprite;
        cardImage.sprite = _frontSideSprite;
    }

    public void OnCardClicked()
    {
        if (_cardUpSide)
            ShowBack();
        else
            ShowFront();
    }

    public void Hide()
    {
        //play vfx
        cardImage.enabled = false;
    }

    public void ShowFront()
    {
        //rotate card
        cardBackImage.gameObject.SetActive(false);
        cardImage.gameObject.SetActive(true);
        _cardUpSide = true;
    }

    public void ShowBack()
    {
        //rotate card
        cardBackImage.gameObject.SetActive(true);
        cardImage.gameObject.SetActive(false);
        _cardUpSide = false;
    }
}
