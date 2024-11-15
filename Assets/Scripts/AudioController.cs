using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource flipCard;
    [SerializeField] private AudioSource setUpCards;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource cardMatched;
    [SerializeField] private AudioSource cardMismatched;

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
    public void PlayCardMismatchedSound()
    {
        cardMismatched.Play();
    }
}
