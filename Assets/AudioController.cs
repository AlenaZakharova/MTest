using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource flipCard;
    [SerializeField] private AudioSource setUpCards;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource cardMatched;

    public void PlayFlipSound()
    {
        flipCard.Play();
    }
    public void PlayBuildCardFieldSound()
    {
        setUpCards.Play();
    }
    public void PlayWinSound()
    {
        win.Play();
    }

    public void PlayCardMatchedSound()
    {
        cardMatched.Play();
    }
}
