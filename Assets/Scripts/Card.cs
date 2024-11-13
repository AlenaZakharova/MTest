using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, ICard
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Button cardButton;

    private Sprite frontSideSprite;
    private Sprite backSideSprite;


    public void SetUp(Sprite backSide, Sprite frontSide)
    {
        this.frontSideSprite = frontSide;
        this.backSideSprite = backSide;
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
        cardImage.sprite = frontSideSprite;
        cardButton.interactable = true;
    }

    public void ShowBack()
    {
        cardButton.interactable = false;
        //rotate card
        cardImage.sprite = frontSideSprite;
        cardButton.interactable = true;
    }
}
