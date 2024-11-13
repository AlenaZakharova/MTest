using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, ICard
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Button cardButton;

    private Sprite _frontSideSprite;
    private Sprite _backSideSprite;


    public void SetUp(Sprite backSide, Sprite frontSide)
    {
        this._frontSideSprite = frontSide;
        this._backSideSprite = backSide;
    }

    public void Hide()
    {
        //play vfx
        cardImage.enabled = false;
    }

    public void ShowFront()
    {
        cardButton.interactable = false;
        //rotate card
        cardImage.sprite = _frontSideSprite;
        cardButton.interactable = true;
    }

    public void ShowBack()
    {
        cardButton.interactable = false;
        //rotate card
        cardImage.sprite = _frontSideSprite;
        cardButton.interactable = true;
    }
}
